using Start.Models;
using Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class RequestsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ViewAllRequest()
        {

            SaveAcsses saveacsses = getAccess().Last();
            var Requests = getRequests().Where(x=>x.ReceiverId == saveacsses.number && x.Value==0).ToList();
            var person = getPerson().ToList();

            PersonRequest personRequest = new PersonRequest {
                person = person,
                Requests = Requests,

            };
            return View(personRequest);
        }

        public ActionResult NewRequest(person person)
        {
            person Person = getPerson().SingleOrDefault(x => x.id == person.id);

            SendRequest(Person.id);

            return RedirectToAction("View", "Search",new {id =  person.id });
        }


        [HttpPost]
        public int SendRequest(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            //  int id = imageId;
            Requests requests = new Requests
            {
                Value = 0,
                ReceiverId  = id,
                SenderId = Person.id
            };
            db.Requests.Add(requests);
            db.SaveChanges();
            return requests.id;
        }




        public ActionResult AcceptRequest(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            Requests request = getRequests().SingleOrDefault(x=>x.id == id);


            if (Person != null)
            {
                request.Value = 1;

                db.SaveChanges();

                var Followers = db.Requests.Where(x => x.Value == 1 && x.ReceiverId == Person.id).ToList();
                var Following = db.Requests.Where(x => x.Value == 1 && x.SenderId == Person.id).ToList();


                var CountFollowers = 0;
                var CountFollowing = 0;

                CountFollowers = Followers.Count;
                CountFollowing = Following.Count;
                Person.Followers = CountFollowers;
                Person.Following = CountFollowing;

                using (db)
                {
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ViewAllRequest", "Requests");

        }


        public ActionResult Refresh(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            Requests request = getRequests().SingleOrDefault(x => x.id == id);


            if (Person != null)
            {
                var Followers = db.Requests.Where(x => x.Value == 1 && x.ReceiverId == Person.id).ToList();
                var Following = db.Requests.Where(x => x.Value == 1 && x.SenderId == Person.id).ToList();

            
                var CountFollowers = 0;
                var CountFollowing = 0;

                CountFollowers = Followers.Count;
                CountFollowing = Following.Count;
                Person.Followers = CountFollowers;
                Person.Following = CountFollowing;

                using (db)
                {
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Profile", Person);

        }

        public ActionResult RefuseRequest(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            Requests request = getRequests().SingleOrDefault(x => x.id == id);


            if (Person != null)
            {
                request.Value = 2;
                db.Requests.Remove(request);
                using (db)
                {
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ViewAllRequest", "Requests");

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

        public IEnumerable<Requests> getRequests()
        {
            var Requests = db.Requests.ToList();
            return Requests;
        }


    }
}