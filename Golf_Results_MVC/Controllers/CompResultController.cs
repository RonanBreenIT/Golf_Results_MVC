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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace Golf_Results_MVC.Controllers
{
    public class CompResultController : Controller
    {
        private GolfContext db = new GolfContext();


        // GET: CompResult/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name").OrderBy(i => i.Text); // Shows the Comp Name instead of ID
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "FullName").OrderBy(i => i.Text);
            return View();
        }

        // POST: CompResult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompResultID,CompetitionID,StartDate,EndDate,GolferID,Position,Season,GolferScore")] Comp_Result comp_Result) // Bind makes sure only the variables mentioned are entered
        {
            if (ModelState.IsValid)
            {
                db.Comp_Results.Add(comp_Result);
                db.SaveChanges();
                return RedirectToAction("ResultUploaded");
                //return RedirectToAction("Index");
            }

            ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
            ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
            return View(comp_Result);
        }

        // Displays message to say upload was a success
        public ActionResult ResultUploaded()
        {
            ViewBag.Message = "Result for Comp Added Successfully";
            return View();
        }


        // Don't think needed yet but may change this. Dont have an index page so see how can be worked.
        //// GET: CompResult/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comp_Result comp_Result = db.Comp_Results.Find(id);
        //    if (comp_Result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
        //    ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
        //    return View(comp_Result);
        //}

        //// POST: CompResult/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CompResultID,CompetitionID,StartDate,EndDate,GolferID,Position,Season,GolferScore")] Comp_Result comp_Result)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(comp_Result).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CompetitionID = new SelectList(db.Competitions, "ID", "Name", comp_Result.CompetitionID);
        //    ViewBag.GolferID = new SelectList(db.Golfers, "ID", "Firstname", comp_Result.GolferID);
        //    return View(comp_Result);
        //}

        //// GET: CompResult/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comp_Result comp_Result = db.Comp_Results.Find(id);
        //    if (comp_Result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comp_Result);
        //}

        //// POST: CompResult/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Comp_Result comp_Result = db.Comp_Results.Find(id);
        //    db.Comp_Results.Remove(comp_Result);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
