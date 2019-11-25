using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class ProgrammesController : Controller
    {
        private readonly IProgrammeRepository _repository;

        public ProgrammesController(IProgrammeRepository repository)
        {
            _repository = repository;
        }

        // GET: Programmes/{facultyId}
        public ActionResult Index(int facultyId)
        {
            ViewBag.FacultyId = facultyId;
            IEnumerable<Programme> programmes = _repository.GetMany(programme => programme.FacultyId == facultyId);
            return View(programmes);
        }

        // GET: Programmes/Details/{id}
        public ActionResult Details(int id)
        {
            Programme programme = _repository.GetById(id);
            if (programme == null)
            {
                return HttpNotFound();
            }

            return View(programme);
        }

        // GET: Programmes/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Programme programme = _repository.GetById(id);
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
                Programme programmeToBeUpdated = _repository.GetById(programme.Id);
                programmeToBeUpdated.Name = programme.Name;

                _repository.GetEntry(programmeToBeUpdated).State = EntityState.Modified;
                await _repository.Commit();

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
                _repository.Add(programme);
                await _repository.Commit();
                return RedirectToAction("Index", new { facultyId = programme.FacultyId });
            }

            return View(programme);
        }
    }
}