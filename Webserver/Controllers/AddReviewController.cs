using System.Web.Mvc;
using Webserver.Data;
using Models;


namespace Webserver.Controllers
{

    public class AddReviewController : Controller
    {
        // GET: AddReview
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UniversityReview(string universityId)
        {
            ViewBag.universityId = universityId;
            return View();
        }

        public ActionResult FacultyReview()
        {
            return View();
        }
        public ActionResult ProgrammeReview()
        {
            return View();
        }

        public ActionResult ReviewUniversity(Review model, int id)
        {
            if (ModelState.IsValid)
            {
                //TODO: SubscribeUser(model.Email);
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                model.UniversityId = id;
                model.UserId = "10";
                model.Value = 5;
                dbContext.Reviews.Add(model);
                dbContext.SaveChanges();
            }
            return View("Index", model);
        }
    }
}