using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using Webserver.Data;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class RemoveReviewController : Controller
    {
        private readonly IReviewRepository _repository;

        public RemoveReviewController(IReviewRepository repository)
        {
            _repository = repository;
        }

        // GET: RemoveReview/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = await db.Reviews.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: RemoveReview/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = await db.Reviews.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: RemoveReview/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UniversityId,FacultyId,LecturerId,ProgrammeId,UserId,Text,Value")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: RemoveReview/{id}
        public ActionResult Index(int id)
        {
            Review review = _repository.GetById(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: RemoveReview/ConfirmDeletion/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeletion(int id)
        {
            Review reviewToBeDeleted = _repository.GetById(id);
            _repository.Delete(review => review.Id == id);
            
            if (reviewToBeDeleted.UniversityId != 0)
            {
                return RedirectToAction("University", "Reviews", new { id = reviewToBeDeleted.UniversityId });
            }
            else if (reviewToBeDeleted.FacultyId != 0)
            {
                return RedirectToAction("Faculty", "Reviews", new { id = reviewToBeDeleted.FacultyId });
            }
            else if (reviewToBeDeleted.ProgrammeId != 0)
            {
                return RedirectToAction("Programme", "Reviews", new { id = reviewToBeDeleted.ProgrammeId });
            }

            // If we got this far, something is horribly wrong
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
