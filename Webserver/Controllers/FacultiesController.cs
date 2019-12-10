using ASPNET_MVC_Samples.Models;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IReviewRepository _reviewRepository;

        public FacultiesController(IFacultyRepository facultyRepository, IUniversityRepository universityRepository, IReviewRepository reviewRepository)
        {
            _facultyRepository = facultyRepository;
            _universityRepository = universityRepository;
            _reviewRepository = reviewRepository;
        }

        // GET: Faculties/{universityId}
        public ActionResult Index(int universityId)
        {
            University university = _universityRepository.GetById(universityId);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.UniversityName = university.Name;
            ViewBag.UniversityId = universityId;

            IEnumerable<Faculty> faculties = _facultyRepository.GetMany(faculty => faculty.UniversityId == universityId);
            return View(faculties);
        }

        // GET: Faculties/Details/{id}
        public ActionResult Details(int id)
        {
            Faculty faculty = _facultyRepository.GetById(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }

            var reviewList = _reviewRepository.GetMany(review => review.FacultyId.Equals(id));

            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(1, reviewList.Where(review => review.Value.Equals(1)).Count()),
                new DataPoint(2, reviewList.Where(review => review.Value.Equals(2)).Count()),
                new DataPoint(3, reviewList.Where(review => review.Value.Equals(3)).Count()),
                new DataPoint(4, reviewList.Where(review => review.Value.Equals(4)).Count()),
                new DataPoint(5, reviewList.Where(review => review.Value.Equals(5)).Count()),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(faculty);
        }

        // GET: Faculties/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Faculty faculty = _facultyRepository.GetById(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Edit/{id}
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                Faculty facultyToBeUpdated = _facultyRepository.GetById(faculty.Id);
                facultyToBeUpdated.Name = faculty.Name;

                _facultyRepository.GetEntry(facultyToBeUpdated).State = EntityState.Modified;
                await _facultyRepository.Commit();

                RedirectToAction("Details", new { id = faculty.Id });
            }

            return View(faculty);
        }

        // GET: Faculties/Add/{universityId}
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(int universityId)
        {
            ViewBag.UniversityId = universityId;
            return View();
        }

        // POST: Faculties/Add
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "UniversityId, Name")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _facultyRepository.Add(faculty);
                await _facultyRepository.Commit();
                return RedirectToAction("Index", new { universityId = faculty.UniversityId });
            }

            return View(faculty);
        }
    }
}