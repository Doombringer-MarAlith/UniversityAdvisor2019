﻿using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
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

            return View(faculty);
        }

        // GET: Faculties/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Faculty faculty = _repository.GetById(id);
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
                Faculty facultyToBeUpdated = _repository.GetById(faculty.Id);
                facultyToBeUpdated.Name = faculty.Name;

                _repository.GetEntry(facultyToBeUpdated).State = EntityState.Modified;
                await _repository.Commit();

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
                _repository.Add(faculty);
                await _repository.Commit();
                return RedirectToAction("Index", new { universityId = faculty.UniversityId });
            }

            return View(faculty);
        }
    }
}