using System.Net;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using Webserver.Models;

namespace Webserver.Controllers
{
    public class UniversitiesController : Controller
    {
        private IUniversityRepository _repository;

        public UniversitiesController(IUniversityRepository repository)
        {
            _repository = repository;
        }

        // GET: Universities
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Universities/Search/{text}
        public ActionResult Search(string text)
        {
            if (text == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var universityList = _repository.GetMany(university => university.Name.Contains(text));
            return View("Index", universityList);
        }

        // GET: Universities/Details/{id}
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            University university = _repository.GetById(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.UniversityId = id;
            return View(university);
        }
    }
}
