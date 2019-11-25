using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _repository;

        public ReviewsController(IReviewRepository repository)
        {
            _repository = repository;
        }

        // GET: Reviews/University/{id}
        public ActionResult University(int id)
        {
            ViewBag.UniversityId = id;
            IEnumerable<Review> reviews = _repository.GetMany(review => review.UniversityId == id);
            return View(reviews);
        }

        // GET: Reviews/Faculty/{id}
        public ActionResult Faculty(int id)
        {
            ViewBag.FacultyId = id;
            IEnumerable<Review> reviews = _repository.GetMany(review => review.FacultyId == id);
            return View(reviews);
        }

        // GET: Reviews/Programme/{id}
        public ActionResult Programme(int id)
        {
            ViewBag.ProgrammeId = id;
            IEnumerable<Review> reviews = _repository.GetMany(review => review.ProgrammeId == id);
            return View(reviews);
        }

        // GET: Reviews/Details/{id}
        public ActionResult Details(int id)
        {
            Review review = _repository.GetById(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // GET: Reviews/Delete/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Review review = _repository.GetById(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmDeletion(int id)
        {
            Review reviewToBeDeleted = _repository.GetById(id);
            _repository.Delete(review => review.Id == id);
            await _repository.Commit();

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

        // GET: Reviews/Edit/{id}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Review review = _repository.GetById(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Reviews/Edit/{id}
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Text")] Review review)
        {
            if (ModelState.IsValid)
            {
                Review reviewToBeUpdated = _repository.GetById(review.Id);
                reviewToBeUpdated.Text = review.Text;

                _repository.GetEntry(reviewToBeUpdated).State = EntityState.Modified;
                await _repository.Commit();

                RedirectToAction("Details", new { id = review.Id });
            }

            return View(review);
        }
    }
}