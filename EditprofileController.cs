using Start.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Controllers
{
    public class EditprofileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Editprofile
        public ActionResult Index()
        {
            return View();
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
        [HttpGet]
        public ActionResult Edit()
        {
            SaveAcsses saveacsses = getAccess().Last();
            person p = getPerson().SingleOrDefault(x => x.id == saveacsses.number);

            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(person p)
        {

            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extention = Path.GetExtension(p.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            p.image = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            p.ImageFile.SaveAs(filename);   
            using (db)
            {
                if (p.id > 0)
                {
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", p);
                }
                else
                {
                    db.people.Add(p);
                }
            }
            return View(p);
            

        }
    }
}