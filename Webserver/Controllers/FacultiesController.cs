using Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class FacultiesController : Controller
    {
        private IFacultyRepository _repository;

        public FacultiesController(IFacultyRepository repository)
        {
            _repository = repository;
        }

        // GET: Faculties/{universityId}
        public ActionResult Index(string universityId)
        {
            if (universityId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.UniversityId = universityId;
            IEnumerable<Faculty> faculties = _repository.GetMany(faculty => faculty.UniversityId == universityId);

            return View(faculties);
        }

        // GET: Faculties/Details/{id}
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
