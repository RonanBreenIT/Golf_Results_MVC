using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Golf_Results_MVC.DAL;
using Golf_Results_MVC.Models;
using PagedList;

namespace Golf_Results_MVC.Controllers
{
    public class CompetitionController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: Competition
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           // ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var comps = from c in db.Competitions
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                comps = comps.Where(s => s.Name.Contains(searchString));                      
            }
            switch (sortOrder)
            {
                case "name_desc":
                    comps = comps.OrderByDescending(s => s.Name);
                    break;
                //case "Date":
                //    comps = comps.OrderBy(s => s.StartDate);
                //    break;
                //case "date_desc":
                //    comps= comps.OrderByDescending(s => s.StartDate);
                //    break;
                //default:  // Name ascending 
                //    comps = comps.OrderBy(s => s.StartDate);
                //    break;
                default:  // Name ascending 
                    comps = comps.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(comps.ToPagedList(pageNumber, pageSize));
        }

        // GET: Competition/AllYears/5
        public ActionResult AllYears(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competitions.Find(id);
            
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // GET: Competition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // trying to use to spit by Year -see route config added season so year would show as param not query param
        //public ActionResult DetailsforComp(int? id, int season)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var comp_Result = from i in db.Comp_Results
        //                      where i.CompetitionID == id
        //                      where i.Season == season
        //                      select i;

        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comp_Result);
        //}

        public ActionResult Create()
        {
            return View();
        }

        // POST: Competition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, StartDate, EndDate")]Competition comp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Competitions.Add(comp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(comp);
        }

        // GET: Competition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Competition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var compToUpdate = db.Competitions.Find(id);
            if (TryUpdateModel(compToUpdate, "",
               new string[] { "Name", "StartDate", "EndDate" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(compToUpdate);
        }

        // GET: Competition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Competition/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Competition comp = db.Competitions.Find(id);
                db.Competitions.Remove(comp);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
