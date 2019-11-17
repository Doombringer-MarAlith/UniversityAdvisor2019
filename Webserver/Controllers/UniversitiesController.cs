using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Webserver.Data;
using Webserver.Models;

namespace Webserver.Controllers
{
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

        // GET: Universities/Details/5
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

            return View(university);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
