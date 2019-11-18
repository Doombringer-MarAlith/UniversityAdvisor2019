using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webserver.Data;
using Webserver.Models;

namespace Webserver.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ApplicationDbContext DbContext
        {
            get
            {
                return _dbContext ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _dbContext = value;
            }
        }

        public ReviewsController()
        {

        }

        public ReviewsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Reviews/University/5
        public async Task<ActionResult> University(string id)
        {
            University university = await DbContext.Universities.FindAsync(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.UniversityId = id;
            IEnumerable<Review> reviews = DbContext.Reviews.Where(review => review.UniGuid == id);
            return View(reviews);
        }

        // GET: Reviews/Faculty/5
        public async Task<ActionResult> Faculty(string id)
        {
            Faculty university = await DbContext.Faculties.FindAsync(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.FacultyId = id;
            IEnumerable<Review> reviews = DbContext.Reviews.Where(review => review.FacultyGuid == id);
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = await DbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            ViewBag.FacultyId = id;
            return View(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }

            base.Dispose(disposing);
        }
    }
}
