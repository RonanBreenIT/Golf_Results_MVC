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
            ViewBag.TotalComps = db.Competitions.Count();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

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
                comps = comps.Where(s => s.Name.Contains(searchString)); // Search box check if string contains whats entered
            }
            switch (sortOrder) // Sort the order by name
            {
                case "name_desc":
                    comps = comps.OrderByDescending(s => s.Name);
                    break;
                default:  // Name ascending 
                    comps = comps.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10; // Sets the number of records to display on the page
            int pageNumber = (page ?? 1);
            return View(comps.ToPagedList(pageNumber, pageSize));
        }

        // Purpose is to display each Comp season and so users can click season and return only results for that season in method below. 
        // GET: Competition/AllYears/5
        //public ActionResult AllYears(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Competition competition = db.Competitions.Find(id);

        //    if (competition == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(competition);
        //}

        // trying to use to spit by Year -see route config added season so year would show as param not query param. Issue as we are calling compresult from Comp. Left out for now
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

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")]Competition comp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var foundName = db.Competitions.FirstOrDefault(i => i.Name == comp.Name);

                    if (foundName != null)
                    {
                        ModelState.AddModelError(string.Empty, "Competition already exists.");
                    }
                    else
                    {
                        db.Competitions.Add(comp);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
               new string[] { "Name"}))
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
