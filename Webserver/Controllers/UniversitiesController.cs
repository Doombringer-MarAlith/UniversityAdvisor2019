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
using Webserver.Models.ViewModels.Pagination;
using Webserver.Services.Api;

namespace Webserver.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapsApi _mapsApi;

        public UniversitiesController(IUniversityRepository universityRepository, IReviewRepository reviewRepository, IMapsApi mapsApi)
        {
            _universityRepository = universityRepository;
            _reviewRepository = reviewRepository;
            _mapsApi = mapsApi;
        }

        // GET: Universities/{page}/{searchCriteria}/{sortOrder}
        public ActionResult Index(int? page, string searchCriteria = null, UniversitySortOrders sortOrder = UniversitySortOrders.NAME_ASC)
        {
            IEnumerable<University> universities;

            Session["CurrentUniversityPage"] = page;
            Session["UniversitySortOrder"] = sortOrder;
            Session["UniversitySearchCriteria"] = searchCriteria;
            ViewBag.SearchCriteria = searchCriteria;

            if (searchCriteria != null)
            {
                universities = _universityRepository.GetMany(university => university.Name.Contains(searchCriteria));
            }
            else
            {
                universities = _universityRepository.GetAll();
            }

            switch (sortOrder)
            {
                case UniversitySortOrders.CITY_ASC:
                    universities = universities.OrderBy(university => university.City);
                    break;
                case UniversitySortOrders.CITY_DESC:
                    universities = universities.OrderByDescending(university => university.City);
                    break;
                case UniversitySortOrders.NAME_DESC:
                    universities = universities.OrderByDescending(university => university.Name);
                    break;
                default:
                    universities = universities.OrderBy(university => university.Name);
                    break;
            }

            var pager = new Pager(universities.Count(), page);

            var viewModel = new PagerViewModel<University, UniversitySortOrders>
            {
                Items = universities.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        // GET: Universities/IndexRedirect/{navigation}
        [ActionName("IndexRedirect")]
        public ActionResult Index(bool navigation)
        {
            if (navigation)
            {
                string searchCriteriaInSessionData = Session["UniversitySearchCriteria"]?.ToString();
                int? currentPageInSessionData = (int?)Session["CurrentUniversityPage"];

                UniversitySortOrders sortOrderInSessionData =
                    Session["UniversitySortOrder"] != null
                    ? (UniversitySortOrders)Session["UniversitySortOrder"]
                    : UniversitySortOrders.NAME_ASC;

                return RedirectToAction("Index", 
                    new { 
                        page = currentPageInSessionData, 
                        searchCriteria = searchCriteriaInSessionData,
                        sortOrder = sortOrderInSessionData 
                    });
            }

            return RedirectToAction("Index");
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