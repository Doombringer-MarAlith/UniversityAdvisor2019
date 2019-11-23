using System.Collections.Generic;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using Models;

namespace Webserver.Controllers
{
    public class ProgrammesController : Controller
    {
        private IProgrammeRepository _repository;

        public ProgrammesController(IProgrammeRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int facultyId)
        {
            ViewBag.FacultyId = facultyId;
            IEnumerable<Programme> programmes = _repository.GetMany(programme => programme.FacultyId == facultyId);
            return View(programmes);
        }

        public ActionResult Details(int id)
        {
            Programme programme = _repository.GetById(id);
            if (programme == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProgrammeId = id;
            ViewBag.FacultyId = programme.FacultyId;
            return View(programme);
        }
    }
}