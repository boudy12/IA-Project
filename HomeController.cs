using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Start.Models;

namespace Start.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(person person)
        {
            SaveAcsses saveacsses = getAccess().Last();
            ViewBag.Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            ViewBag.AllPerson = getPerson();
            ViewBag.Images = getImages();
            ViewBag.Requests = getRequests();
            return View();
        }

        public IEnumerable<Requests> getRequests()
        {
            SaveAcsses saveacsses = getAccess().Last();
            var Requests = db.Requests.Where(x => x.SenderId == saveacsses.number).ToList();
            return Requests;
        }

        public IEnumerable<Images> getImages()
        {

            var image = db.images.ToList();

            return image;
        }


        public ActionResult ListUser()
        {
            var person = getPerson().ToList();
            return View(person);
        }


        public IEnumerable<person> getPerson()
        {

            var person = db.people.ToList();
            return person;
        }


        public IEnumerable<SaveAcsses> getAccess()
        {

            var saveacsses = db.SaveAcsses.ToList();
            return saveacsses;
        }




    }
}