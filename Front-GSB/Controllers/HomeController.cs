using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front_GSB.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] //annotation
        public ActionResult Index()
        {
            return View();
        }

        [Authorize] //nécessite une authentification
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}