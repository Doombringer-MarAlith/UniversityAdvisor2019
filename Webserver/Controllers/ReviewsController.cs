using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using Webserver.Enums;
using Webserver.Services;

namespace Webserver.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IPaginationHandler<Review, ReviewSortOrder> _paginationHandler;

        public ReviewsController(IReviewRepository repository, IPaginationHandler<Review, ReviewSortOrder> paginationHandler)
        {
            _repository = repository;
            _paginationHandler = paginationHandler;
        }

        // GET: Reviews/University/{id}/{page}/{sortOrder}
        public ActionResult University(int id, int? page, ReviewSortOrder sortOrder = ReviewSortOrder.VALUE_DESC)
        {
            IEnumerable<Review> reviews = _repository.GetMany(review => review.UniversityId == id);

            Session["CurrentUniversityReviewPage"] = page;
            Session["UniversityReviewSortOrder"] = sortOrder;
            ViewBag.UniversityId = id;

            return View(_paginationHandler.ConstructViewModel(reviews, page, sortOrder));
        }

        // GET: Reviews/RedirectToUniversity/{id}
        public ActionResult RedirectToUniversity(int id)
        {
            int? currentPageInSessionData = (int?)Session["CurrentUniversityReviewPage"];

            ReviewSortOrder sortOrderInSessionData =
                Session["UniversityReviewSortOrder"] != null
                ? (ReviewSortOrder)Session["UniversityReviewSortOrder"]
                : ReviewSortOrder.VALUE_DESC;

            return RedirectToAction("University",
                new
                {
                    id = id,
                    page = currentPageInSessionData,
                    sortOrder = sortOrderInSessionData
                });
        }

        // GET: Reviews/Faculty/{id}/{page}/{sortOrder}
        public ActionResult Faculty(int id, int? page, ReviewSortOrder sortOrder = ReviewSortOrder.VALUE_DESC)
        {
            IEnumerable<Review> reviews = _repository.GetMany(review => review.FacultyId == id);

            Session["CurrentFacultyReviewPage"] = page;
            Session["FacultyReviewSortOrder"] = sortOrder;
            ViewBag.FacultyId = id;

            return View(_paginationHandler.ConstructViewModel(reviews, page, sortOrder));
        }

        // GET: Reviews/RedirectToFaculty/{id}
        public ActionResult RedirectToFaculty(int id)
        {
            int? currentPageInSessionData = (int?)Session["CurrentFacultyReviewPage"];

            ReviewSortOrder sortOrderInSessionData =
                Session["FacultyReviewSortOrder"] != null
                ? (ReviewSortOrder)Session["FacultyReviewSortOrder"]
                : ReviewSortOrder.VALUE_DESC;

            return RedirectToAction("Faculty",
                new
                {
                    id = id,
                    page = currentPageInSessionData,
                    sortOrder = sortOrderInSessionData
                });
        }

        // GET: Reviews/Programme/{id}/{page}/{sortOrder}
        public ActionResult Programme(int id, int? page, ReviewSortOrder sortOrder = ReviewSortOrder.VALUE_DESC)
        {
            IEnumerable<Review> reviews = _repository.GetMany(review => review.ProgrammeId == id);

            Session["CurrentProgrammeReviewPage"] = page;
            Session["ProgrammeReviewSortOrder"] = sortOrder;
            ViewBag.ProgrammeId = id;

            return View(_paginationHandler.ConstructViewModel(reviews, page, sortOrder));
        }

        // GET: Reviews/RedirectToProgramme/{id}
        public ActionResult RedirectToProgramme(int id)
        {
            int? currentPageInSessionData = (int?)Session["CurrentProgrammeReviewPage"];

            ReviewSortOrder sortOrderInSessionData =
                Session["ProgrammeReviewSortOrder"] != null
                ? (ReviewSortOrder)Session["ProgrammeReviewSortOrder"]
                : ReviewSortOrder.VALUE_DESC;

            return RedirectToAction("Programme",
                new
                {
                    id = id,
                    page = currentPageInSessionData,
                    sortOrder = sortOrderInSessionData
                });
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