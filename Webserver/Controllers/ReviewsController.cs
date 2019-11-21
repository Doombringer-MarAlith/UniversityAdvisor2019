using Models;
using System.Collections.Generic;
using System.Net;
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
    }
}
