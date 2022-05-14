using Start.Models;
using Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index(person person)
        {
            SaveAcsses saveacsses = getAccess().Last();
            ViewBag.Images = getImages();
            ViewBag.Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            getPosts();
            return View();
        }

        public ActionResult ViewImage(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();

            var Image = getOneImages().SingleOrDefault(x => x.id == id);
            var comment = getComments().Where(x=>x.ImageId == id).ToList();
            var person = getPerson().ToList();
            ImageComments imageComments = new ImageComments {
                images = Image,
                comments = comment,
                person = person,
            };

            return View(imageComments);
        }

        public IEnumerable<person> getPerson()
        {

            var person = db.people.ToList();
            return person;
        }




        public IEnumerable<Comment> getComments()
        {

            var comments = db.comments.ToList();
            return comments;
        }



        public IEnumerable<SaveAcsses> getAccess()
        {
            var saveacsses = db.SaveAcsses.ToList();
            return saveacsses;
        }


        public IEnumerable<Images> getImages()
        {
            SaveAcsses saveacsses = getAccess().Last();

            var image = db.images.Where(x => x.PersonID == saveacsses.number).ToList();
            return image;
        }

        public IEnumerable<Images> getOneImages()
        {
            var image = db.images.ToList();
            return image;
        }


        public int getPosts()
        {
            SaveAcsses saveacsses = getAccess().Last();
            var image = db.images.Where(x => x.PersonID == saveacsses.number).ToList();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            var count = 0;
            if (Person != null)
            {
                count = image.Count;
                Person.Posts = count;
                using (db)
                {
                    db.SaveChanges();
                }
            }

            return count;
        }





    }
}