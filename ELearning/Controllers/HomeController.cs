using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        // object id = Membership.GetUser().ProviderUserKey;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
       
        [Authorize(Roles ="ADMIN")]
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "ADMIN,STUDENT")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpGet]
        //public ActionResult GetPageHeader()
        //{
        //    return PartialView(@"~/Views/Shared/_Categories.cshtml");
        //}

       
    }
}