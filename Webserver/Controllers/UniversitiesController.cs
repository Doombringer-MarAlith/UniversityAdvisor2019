using ASPNET_MVC_Samples.Models;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using Webserver.Enums;
using Webserver.Services;
using Webserver.Services.Api;

namespace Webserver.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapsApi _mapsApi;
        private readonly IPaginationHandler<University, UniversitySortOrder> _paginationHandler;

        public UniversitiesController
        (
            IUniversityRepository universityRepository,
            IReviewRepository reviewRepository,
            IMapsApi mapsApi,
            IPaginationHandler<University, UniversitySortOrder> paginationHandler
        )
        {
            _universityRepository = universityRepository;
            _reviewRepository = reviewRepository;
            _mapsApi = mapsApi;
            _paginationHandler = paginationHandler;
        }

        // GET: Universities/{page}/{searchCriteria}/{sortOrder}/{country}
        public ActionResult Index(int? page, string searchCriteria = null, UniversitySortOrder sortOrder = UniversitySortOrder.NAME_ASC, string country = null)
        {
            IEnumerable<University> universities;

            Session["CurrentUniversityPage"] = page;
            Session["UniversitySortOrder"] = sortOrder;
            Session["UniversitySearchCriteria"] = searchCriteria;
            Session["UniversityCountry"] = country;

            ViewBag.SearchCriteria = searchCriteria;
            ViewBag.Country = country;

            // Default value from html dropdown list
            if (country == "")
            {
                country = null;
            }

            if (country != null && searchCriteria != null)
            {
                universities = _universityRepository.GetMany(university => university.Country == country && university.Name.Contains(searchCriteria));
            }
            else if (searchCriteria != null)
            {
                universities = _universityRepository.GetMany(university => university.Name.Contains(searchCriteria));
            }
            else if (country != null)
            {
                universities = _universityRepository.GetMany(university => university.Country == country);
            }
            else
            {
                universities = _universityRepository.GetAll();
            }

            return View(_paginationHandler.ConstructViewModel(universities, page, sortOrder));
        }

        // GET: Universities/RedirectToIndex
        public ActionResult RedirectToIndex()
        {
            string searchCriteriaInSessionData = Session["UniversitySearchCriteria"]?.ToString();
            string countryInSessionData = Session["UniversityCountry"]?.ToString();
            int? currentPageInSessionData = (int?)Session["CurrentUniversityPage"];

            UniversitySortOrder sortOrderInSessionData =
                Session["UniversitySortOrder"] != null
                ? (UniversitySortOrder)Session["UniversitySortOrder"]
                : UniversitySortOrder.NAME_ASC;

            return RedirectToAction("Index",
                new
                {
                    page = currentPageInSessionData,
                    searchCriteria = searchCriteriaInSessionData,
                    sortOrder = sortOrderInSessionData,
                    country = countryInSessionData
                });
        }

        // GET: Universities/Details/{id}

        public ActionResult Details(int id)
        {
            University university = _universityRepository.GetById(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.UniversityId = id;
            var reviewList = _reviewRepository.GetMany(review => review.UniversityId.Equals(id));

            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(1, reviewList.Where(review => review.Value.Equals(1)).Count()),
                new DataPoint(2, reviewList.Where(review => review.Value.Equals(2)).Count()),
                new DataPoint(3, reviewList.Where(review => review.Value.Equals(3)).Count()),
                new DataPoint(4, reviewList.Where(review => review.Value.Equals(4)).Count()),
                new DataPoint(5, reviewList.Where(review => review.Value.Equals(5)).Count()),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.GoogleMapUri = _mapsApi.GetStaticMapUri(university.Name);

            return View(university);
        }

        // GET: Universities/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            University university = _universityRepository.GetById(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            return View(university);
        }

        // POST: Universities/Edit/{id}
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, Description")] University university)
        {
            if (ModelState.IsValid)
            {
                University universityToBeUpdated = _universityRepository.GetById(university.Id);
                universityToBeUpdated.Name = university.Name;
                universityToBeUpdated.Description = university.Description;

                _universityRepository.GetEntry(universityToBeUpdated).State = EntityState.Modified;
                await _universityRepository.Commit();

                RedirectToAction("Details", new { id = university.Id });
            }

            return View(university);
        }

        // GET: Universities/Add
        [Authorize(Roles = "Administrator")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: Universities/Add
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Name, Description")] University university)
        {
            if (ModelState.IsValid)
            {
                _universityRepository.Add(university);
                await _universityRepository.Commit();
                return RedirectToAction("Index");
            }

            return View(university);
        }
    }
}