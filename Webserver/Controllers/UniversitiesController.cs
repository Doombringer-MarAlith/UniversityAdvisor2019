using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private ApplicationDbContext _dbContext { get; set; }

        public UniversitiesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Universities
        public async Task<ActionResult> Index()
        {
            return View(await _dbContext.Universities.ToListAsync());
        }

        // GET: Universities/Search/{text}
        public async Task<ActionResult> Search(string text)
        {
            if (text == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var universityList = await _dbContext.Universities.ToListAsync();
            return View("Index", universityList.FindAll(uni => uni.Name.ToLower().Contains(text.ToLower())));
        }

        // GET: Universities/Details/{id}
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            University university = await _dbContext.Universities.FindAsync(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            // Set current university id so that front-end can navigate back to details
            ViewBag.UniversityId = id;

            return View(university);
        }
    }
}
