using Microsoft.AspNet.Identity;
using Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Data.Repositories;

namespace Webserver.Controllers
{
    [Authorize]
    public class AddReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public AddReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: AddReview
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UniversityReview(int universityId)
        {
            ViewBag.universityId = universityId;
            return View();
        }

        public ActionResult FacultyReview(int facultyId)
        {
            ViewBag.facultyId = facultyId;
            return View();
        }

        public ActionResult ProgrammeReview(int programmeId)
        {
            ViewBag.programmeId = programmeId;
            return View();
        }

        [HttpPost]
        public ActionResult ReviewUniversity(Review model, int id)
        {
            model.UniversityId = id;
            if (model.Value > 5 || model.Value < 1)
            {
                model.Value = 5;
            }

            model.UserId = User.Identity.GetUserId();
            _reviewRepository.Add(model);
            Task task = Task.Run(async () => await _reviewRepository.Commit());
            task.Wait();
            return RedirectToAction("Details", "Universities", new { id = id });
        }

        [HttpPost]
        public ActionResult ReviewFaculty(Review model, int id)
        {
            model.FacultyId = id;
            if (model.Value > 5 || model.Value < 1)
            {
                model.Value = 5;
            }

            model.UserId = User.Identity.GetUserId();
            _reviewRepository.Add(model);
            Task task = Task.Run(async () => await _reviewRepository.Commit());
            task.Wait();
            return RedirectToAction("Details", "Faculties", new { id = id });
        }

        [HttpPost]
        public ActionResult ReviewProgramme(Review model, int id)
        {
            model.ProgrammeId = id;
            if (model.Value > 5 || model.Value < 1)
            {
                model.Value = 5;
            }

            model.UserId = User.Identity.GetUserId();
            _reviewRepository.Add(model);
            Task task = Task.Run(async () => await _reviewRepository.Commit());
            task.Wait();
            return RedirectToAction("Details", "Programmes", new { id = id });
        }
    }
}