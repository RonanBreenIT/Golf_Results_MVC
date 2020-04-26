using System.Web.Mvc;

namespace Golf_Results_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        // Not using as yet
        public ActionResult About()
        {
            ViewBag.Message = "PGA Results App";

            return View("About");
        }


        // Not using as yet
        public ActionResult Contact()
        {
            ViewBag.Message = "Find our Contact Details below";

            return View("Contact");
        }
    }
}