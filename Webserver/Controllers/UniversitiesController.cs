using Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly IUniversityRepository _repository;

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
        public ActionResult Details(int id)
        {
            University university = _repository.GetById(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            return View(university);
        }

        // GET: Universities/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            University university = _repository.GetById(id);
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
                University universityToBeUpdated = _repository.GetById(university.Id);
                universityToBeUpdated.Name = university.Name;
                universityToBeUpdated.Description = university.Description;
                universityToBeUpdated.FoundingDate = university.FoundingDate;

                _repository.GetEntry(universityToBeUpdated).State = EntityState.Modified;
                await _repository.Commit();

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
                _repository.Add(university);
                await _repository.Commit();
                return RedirectToAction("Index");
            }

            return View(university);
        }
    }
}