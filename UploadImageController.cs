using Start.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class UploadImageController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: UploadImage
        [HttpGet]
        public ActionResult Index()
        {
            SaveAcsses saveacsses = getAccess().Last();
            person p = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
            return View();
        }



        [HttpPost]
        public ActionResult Index(Images image) //UploadImages
        {

            SaveAcsses saveacsses = getAccess().Last();


            person p = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            
            string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extention = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            image.UploadImages = "~/UploadImages/" + filename;
            filename = Path.Combine(Server.MapPath("~/UploadImages/"), filename);
            image.ImageFile.SaveAs(filename);
            image.PersonID = p.id;
            using (db)
            {

                db.images.Add(image);
                
                db.SaveChanges();
                    return RedirectToAction("index", "Profile", image);
                
            }
        }

        public IEnumerable<Images> getImages()
        {
            SaveAcsses saveacsses = getAccess().Last();

            var image = db.images.Where(x => x.PersonID == saveacsses.number).ToList();
            person Person = getPerson().SingleOrDefault(x => x.id == saveacsses.number);
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