﻿using System.Web.Mvc;

namespace Golf_Results_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Not using as yet
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        // Not using as yet
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}