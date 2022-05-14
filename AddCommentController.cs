using Start.Models;
using Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class AddCommentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddComment
        public ActionResult Index()
        {
            return View();
        }

 

        [HttpGet]
        public ActionResult AddComment()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddComment(Images images,Comment comment)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            Comment c = new Comment();
            c.PersonId = Person.id;
            c.ImageId = images.id;
            c.Comments = comment.Comments;
            db.comments.Add(c);
            db.SaveChanges();
            return RedirectToAction("ViewImage", "Profile",new {id =  images.id });

        }

        public ActionResult NewLike(Images images)
        {
            Images Image = getImages().SingleOrDefault(x => x.id == images.id);
            AddLike(Image.id);
            SaveLike(Image.id);
            return RedirectToAction("ViewImage", "Profile", new { id = images.id });
        }


        [HttpPost]
        public int AddLike(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            //  int id = imageId;
            Images Image = getImages().SingleOrDefault(x => x.id == id);
            Like like = new Like
            {

                ImageId = id,
                LikeValue = 1,
                DisLikeValue = 0,
                PersonId = Person.id,

            };


            db.Likes.Add(like);
            db.SaveChanges();


            return like.id;
        }


        public int SaveLike(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            Images Image = getImages().SingleOrDefault(x => x.id == id);

            var like = db.Likes.Where(x => x.LikeValue == 1 && x.ImageId == Image.id).ToList();
            var count = 0;
            if (Person != null)
            {
                count = like.Count;
                Image.Likes = count;
                using (db)
                {
                    db.SaveChanges();
                }
            }
            return count;
        }



        public ActionResult NewDisLike(Images images, Comment comment)
        {

            Images Image = getImages().SingleOrDefault(x => x.id == images.id);
            AddDisLike(Image.id);
            SaveDisLike(Image.id);
            return RedirectToAction("ViewImage", "Profile", new { id = images.id });
        }

        [HttpPost]
        public int AddDisLike(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            //  int id = imageId;
            Images Image = getImages().SingleOrDefault(x => x.id == id);
            Like like = new Like
            {

                ImageId = id,
                LikeValue = 0,
                DisLikeValue = 1,
                PersonId = Person.id,

            };


            db.Likes.Add(like);
            db.SaveChanges();


            return like.id;
        }


        public int SaveDisLike(int id)
        {
            SaveAcsses saveacsses = getAccess().Last();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            Images Image = getImages().SingleOrDefault(x => x.id == id);

            var like = db.Likes.Where(x => x.DisLikeValue == 1 && x.ImageId == Image.id).ToList();
            var count = 0;
            if (Person != null)
            {
                count = like.Count;
                Image.DisLikes = count;
                using (db)
                {
                    db.Entry(Image).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return count;
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
    }
}