using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UmangMicro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnimationFilm()
        {
            return View();
        }

        public ActionResult Modules()
        {
            return View();
        }

        public ActionResult Testimonials()
        {
            return View();
        }
        public ActionResult AimObjectives()
        {
            return View();
        }

        public ActionResult Design()
        {
            return View();
        }

        public ActionResult Intervention()
        {
            return View();
        }


        public ActionResult Journeysofar()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}