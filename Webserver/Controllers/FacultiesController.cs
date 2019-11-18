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
    public class FacultiesController : Controller
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

        public FacultiesController()
        {

        }

        public FacultiesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Faculties
        public async Task<ActionResult> Index()
        {
            return View(await DbContext.Faculties.ToListAsync());
        }

        // GET: Faculties/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Faculty faculty = await DbContext.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }

            return View(faculty);
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
