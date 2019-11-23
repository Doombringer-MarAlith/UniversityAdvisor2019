using System.Web.Mvc;

namespace Webserver.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Created by Julius, Rytis, Tomas and Dovile";
            return View();
        }
    }
}