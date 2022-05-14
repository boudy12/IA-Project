using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Start.Models;

namespace WebApplication39.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Autherize(person userModel)
        {

                var userDetails = db.people.Where(x => x.Name == userModel.Name && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    return View("Index", userModel);
                }
                else
                {
                var c = new SaveAcsses
                {
                    number = userDetails.id,
                };
                  db.SaveAcsses.Add(c);
                db.SaveChanges();
                db.SaveAcsses.Remove(c);
                return RedirectToAction("Index","Home", userDetails );
                }


        }
    
            
        
    }
    
}