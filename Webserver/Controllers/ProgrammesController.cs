using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using ASPNET_MVC_Samples.Models;
using Newtonsoft.Json;
using System.Linq;

namespace Webserver.Controllers
{
    public class ProgrammesController : Controller
    {
        private readonly IProgrammeRepository _programmeRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IReviewRepository _reviewRepository;

        public ProgrammesController(IProgrammeRepository programmeRepository, IFacultyRepository facultyRepository, IReviewRepository reviewRepository)
        {
            _programmeRepository = programmeRepository;
            _facultyRepository = facultyRepository;
            _reviewRepository = reviewRepository;
        }

        // GET: Programmes/{facultyId}
        public ActionResult Index(int facultyId)
        {
            Faculty faculty = _facultyRepository.GetById(facultyId);
            if (faculty == null)
            {
                return HttpNotFound();
            }

            ViewBag.FacultyName = faculty.Name;
            ViewBag.FacultyId = facultyId;

            IEnumerable<Programme> programmes = _programmeRepository.GetMany(programme => programme.FacultyId == facultyId);
            return View(programmes);
        }

        // GET: Programmes/Details/{id}
        public ActionResult Details(int id)
        {
            Programme programme = _programmeRepository.GetById(id);
            if (programme == null)
            {
                return HttpNotFound();
            }

            var reviewList = _reviewRepository.GetMany(review => review.ProgrammeId.Equals(id));

            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(1, reviewList.Where(review => review.Value.Equals(1)).Count()),
                new DataPoint(2, reviewList.Where(review => review.Value.Equals(2)).Count()),
                new DataPoint(3, reviewList.Where(review => review.Value.Equals(3)).Count()),
                new DataPoint(4, reviewList.Where(review => review.Value.Equals(4)).Count()),
                new DataPoint(5, reviewList.Where(review => review.Value.Equals(5)).Count()),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(programme);
        }

        // GET: Programmes/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Programme programme = _programmeRepository.GetById(id);
            if (programme == null)
            {
                return HttpNotFound();
            }

            return View(programme);
        }

        // POST: Programmes/Edit/{id}
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                Programme programmeToBeUpdated = _programmeRepository.GetById(programme.Id);
                programmeToBeUpdated.Name = programme.Name;

                _programmeRepository.GetEntry(programmeToBeUpdated).State = EntityState.Modified;
                await _programmeRepository.Commit();

                RedirectToAction("Details", new { id = programme.Id });
            }

            return View(programme);
        }

        // GET: Programmes/Add/{facultyId}
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(int facultyId)
        {
            ViewBag.FacultyId = facultyId;
            return View();
        }

        // POST: Programmes/Add
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "FacultyId, Name")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                _programmeRepository.Add(programme);
                await _programmeRepository.Commit();
                return RedirectToAction("Index", new { facultyId = programme.FacultyId });
            }

            return View(programme);
        }
    }
}