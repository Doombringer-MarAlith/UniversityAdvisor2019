using ASPNET_MVC_Samples.Models;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;
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

        // GET: Universities/{page}/{searchCriteria}
        public ActionResult Index(int? page, string searchCriteria = null)
        {
            var universities = searchCriteria != null 
                ? _universityRepository.GetMany(university => university.Name.Contains(searchCriteria))
                : _universityRepository.GetAll();

            var pager = new Pager(universities.Count(), page);

            var viewModel = new PagerViewModel<University>
            {
                Items = universities.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            ViewBag.SearchCriteria = searchCriteria;
            return View(viewModel);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, Description, FoundingDate")] University university)
        {
            if (ModelState.IsValid)
            {
                University universityToBeUpdated = _universityRepository.GetById(university.Id);
                universityToBeUpdated.Name = university.Name;
                universityToBeUpdated.Description = university.Description;
                universityToBeUpdated.FoundingDate = university.FoundingDate;

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
        public async Task<ActionResult> Add([Bind(Include = "Name, Description, Location, FoundingDate")] University university)
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