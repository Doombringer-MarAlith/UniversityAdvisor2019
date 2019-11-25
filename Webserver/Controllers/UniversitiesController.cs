using Models;
using System.Net;
using System.Web.Mvc;
using Webserver.Data.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using ASPNET_MVC_Samples.Models;

namespace Webserver.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly IUniversityRepository _repository;

        public UniversitiesController(IUniversityRepository repository)
        {
            _repository = repository;
        }

        // GET: Universities
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Universities/Search/{text}
        public ActionResult Search(string text)
        {
            if (text == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var universityList = _repository.GetMany(university => university.Name.Contains(text));
            return View("Index", universityList);
        }

        // GET: Universities/Details/{id}
       
        public ActionResult Details(int id)
        {
            University university = _repository.GetById(id);
            if (university == null)
            {
                return HttpNotFound();
            }

            ViewBag.UniversityId = id;

            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(1, 22),                   
                new DataPoint(2, 36),
                new DataPoint(3, 42),
                new DataPoint(4, 51),
                new DataPoint(5, 46),
            };  //TODO sioje vietoje reikia is duombazes paskaiciuot kiek yra review su value 1, kiek su value 2 ir iki 5.
            // bet bedele tame kad tai yra review controller o ne UniversitiesController

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(university);
        }

      
    }
}
