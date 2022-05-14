using Start.Models;
using Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index(string search)
        {
            SaveAcsses saveacsses = getAccess().Last();

            var s = db.people.Where(x => x.Name.Contains(search) || x.Email.Contains(search) || search == null && x.id != saveacsses.number).ToList();
            return View(s);
        }

        public ActionResult View(int id)
        {

            var images = getallImages(id).ToList();
            var person = getPerson().SingleOrDefault(x => x.id == id);
            var requests = getRequests().ToList();
            getPosts(id);

            FriendsRequestsView friendsRequestsView = new FriendsRequestsView {
                person = person,
                images = images,
                Requests = requests,
            };

            if (person == null)
                return HttpNotFound();
            return View(friendsRequestsView);
        }

        public ActionResult ViewImage(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            var Image = getImages().SingleOrDefault(x => x.id == id);

            var comment = getComments().Where(x => x.ImageId == id).ToList();
            ImageComments imageComments = new ImageComments
            {
                images = Image,
                comments = comment,
            };

            return View(imageComments);
        }
        public IEnumerable<Comment> getComments()
        {

            var comments = db.comments.ToList();
            return comments;
        }
        public IEnumerable<Images> getImages()
        {

            var image = db.images.ToList();
            return image;
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
        public IEnumerable<Images> getallImages(int id)
        {

            var image = db.images.Where(x => x.PersonID == id).ToList();
           
            person Person = getPerson().SingleOrDefault(x => x.id == id);
            return image;
        }
        public int getPosts(int id)
        {
            var image = db.images.Where(x => x.PersonID == id).ToList();
            person Person = getPerson().SingleOrDefault(x => x.id == id);

            var count = image.Count;
            Person.Posts = count;
            using (db)
            {
                db.SaveChanges();
            }
            return count;
        }
        public IEnumerable<Requests> getRequests()
        {
            SaveAcsses saveacsses = getAccess().Last();
            var Requests = db.Requests.Where(x=>x.SenderId==saveacsses.number).ToList();
            return Requests;
        }

    }

}