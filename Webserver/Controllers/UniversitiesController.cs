using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webserver.Data;
using Webserver.Models;

namespace Webserver.Controllers
{
    [Authorize]
    public class UniversitiesController : Controller
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

        public UniversitiesController()
        {

        }

        // GET: Universities
        public async Task<ActionResult> Index()
        {
            return View(await DbContext.Universities.ToListAsync());
        }

        // GET: Universities/Search/{text}
        public async Task<ActionResult> Search(string text)
        {
            if (text == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var universityList = await DbContext.Universities.ToListAsync();
            return View("Index", universityList.FindAll(uni => uni.Name.ToLower().Contains(text.ToLower())));
        }

        // GET: Universities/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            University university = await DbContext.Universities.FindAsync(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            // Set current university id so that front-end can navigate back to details
            ViewBag.UniversityId = id;

            return View(university);
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
