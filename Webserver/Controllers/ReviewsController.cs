﻿using Models;
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
        public ActionResult University(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.UniversityId = id;
            IEnumerable<Review> reviews = _repository.GetMany(review => review.UniversityId == id);
            return View(reviews);
        }

        // GET: Reviews/Faculty/{id}
        public ActionResult Faculty(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.FacultyId = id;
            IEnumerable<Review> reviews = _repository.GetMany(review => review.FacultyId == id);
            return View(reviews);
        }

        // GET: Reviews/Details/{id}
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = _repository.GetById(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }
    }
}
