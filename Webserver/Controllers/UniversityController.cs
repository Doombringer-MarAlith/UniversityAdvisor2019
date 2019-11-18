using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webserver.Controllers
{
    public class UniversityController : Controller
    {
        // GET: University
        public ActionResult University()
        {

            ViewBag.Message = "This is the university profile";

            return View();
        
        }
    }
}