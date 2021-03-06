﻿using Golf_Results_MVC.DAL;
using Golf_Results_MVC.Models;
using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;

namespace Golf_Results_MVC.Controllers
{
    public class GolferController : Controller
    {
        private GolfContext db = new GolfContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.TotalGolfers = db.Golfers.Count();
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

            var golfers = from g in db.Golfers
                          select g;
            if (!String.IsNullOrEmpty(searchString))
            {
                golfers = golfers.Where(s => s.Surname.Contains(searchString) // Search goes through both firstname and surname
                                       || s.Firstname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    golfers = golfers.OrderByDescending(s => s.Surname);
                    break;
                default:
                    golfers = golfers.OrderBy(s => s.Surname);
                    break;
            }
            int pageSize = 30; // Change this to increase numbers shown on the page
            int pageNumber = (page ?? 1);
            return View(golfers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Golfer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Golfer golfer = db.Golfers.Find(id);
            if (golfer == null)
            {
                return HttpNotFound();
            }
            return View(golfer);
        }

        // GET: Golfer/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Golfer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Surname, Firstname")]Golfer golfer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var foundName = db.Golfers.FirstOrDefault(i => i.Firstname == golfer.Firstname && i.Surname == golfer.Surname);
                    if (foundName != null)
                    {
                        ModelState.AddModelError(string.Empty, "Golfer already exists.");
                    }
                    else
                    {
                        db.Golfers.Add(golfer);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(golfer);
        }

        // GET: Golfer/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Golfer golfer = db.Golfers.Find(id);
            if (golfer == null)
            {
                return HttpNotFound();
            }
            return View(golfer);
        }

        // POST: Golfer/Edit/5
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
            var golferToUpdate = db.Golfers.Find(id);
            if (TryUpdateModel(golferToUpdate, "",
               new string[] { "Surname", "Firstname" }))
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
            return View(golferToUpdate);
        }

        // GET: Golfer/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Golfer golfer = db.Golfers.Find(id);
            if (golfer == null)
            {
                return HttpNotFound();
            }
            return View(golfer);
        }

        // POST: Golfer/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Golfer golfer = db.Golfers.Find(id);
                db.Golfers.Remove(golfer);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
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
