using Start.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Start.Controllers
{
    public class SignUpController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(person p)
        {
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extention = Path.GetExtension(p.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            p.image = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            p.ImageFile.SaveAs(filename);



            if (p == null)
            {
                return View("Index", p);
            }
            else
            {
                db.people.Add(p);
                db.SaveChanges();
                var c = new SaveAcsses
                {
                    number = p.id,
                };
                db.SaveAcsses.Add(c);
                db.SaveChanges();
                db.SaveAcsses.Remove(c);
                return RedirectToAction("Index", "Home",p);                
            }

        }
    }
}