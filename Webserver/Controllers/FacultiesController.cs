using Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultyRepository _repository;

        public FacultiesController(IFacultyRepository repository)
        {
            _repository = repository;
        }

        // GET: Faculties/{universityId}
        public ActionResult Index(int universityId)
        {
            ViewBag.UniversityId = universityId;
            IEnumerable<Faculty> faculties = _repository.GetMany(faculty => faculty.UniversityId == universityId);

            return View(faculties);
        }

        // GET: Faculties/Details/{id}
        public ActionResult Details(int id)
        {
            Faculty faculty = _repository.GetById(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }

            ViewBag.FacultyId = id;
            ViewBag.UniversityId = faculty.UniversityId;
            return View(faculty);
        }
    }
}
