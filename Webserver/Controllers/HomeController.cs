using System.Drawing;
using System.Web.Mvc;

namespace Webserver.Controllers
{
    public class HomeController : Controller
    {
        public delegate void AddMessageToViewBag(string message);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            AddMessageToViewBag messageToViewBag = delegate (string message)
            {
                ViewBag.Message = message;
            };

            messageToViewBag("Created by Julius, Rytis, Tomas and Dovile");

            return View();
        }
    }
}