using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Golf_Results_MVC.DAL;
using Golf_Results_MVC.Models;
using PagedList;

namespace Golf_Results_MVC.Controllers
{
    public class CompResultController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: CompResult
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var comps = from c in db.Comp_Results
                        select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                comps = comps.Where(s => s.Competition.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    comps = comps.OrderByDescending(s => s.StartDate);
                    break;
                case "date_desc":
                    comps = comps.OrderBy(s => s.StartDate);
                    break;
                case "name_desc":
                    comps = comps.OrderByDescending(s => s.Competition.Name);
                    break;
                default:  
                    comps = comps.OrderByDescending(s => s.Season);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(comps.ToPagedList(pageNumber, pageSize));
        }

        // GET: CompResult/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            if (comp_Result == null)
            {
                return HttpNotFound();
            }
            return View(comp_Result);
        }

        // GET: CompResult/Create
        public ActionResult Create()
        {
            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name");
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname");
            return View();
        }

        // POST: CompResult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompResultID,CompetitionID,StartDate,EndDate,GolferID,Position,Season,GolferScore")] Comp_Result comp_Result)
        {
            if (ModelState.IsValid)
            {
                db.Comp_Results.Add(comp_Result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
            return View(comp_Result);
        }

        // GET: CompResult/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            if (comp_Result == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
            return View(comp_Result);
        }

        // POST: CompResult/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompResultID,CompetitionID,StartDate,EndDate,GolferID,Position,Season,GolferScore")] Comp_Result comp_Result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comp_Result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
            return View(comp_Result);
        }

        // GET: CompResult/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            if (comp_Result == null)
            {
                return HttpNotFound();
            }
            return View(comp_Result);
        }

        // POST: CompResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comp_Result comp_Result = db.Comp_Results.Find(id);
            db.Comp_Results.Remove(comp_Result);
            db.SaveChanges();
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
