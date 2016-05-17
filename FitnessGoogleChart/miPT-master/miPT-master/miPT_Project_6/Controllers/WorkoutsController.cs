using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using miPT_Project_6.Models;

namespace miPT_Project_6.Controllers
{
    public class WorkoutsController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: Workouts
        public ActionResult Index()
        {
            return View(db.Workouts.ToList());
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutID,WorkoutName,Description,YouTubeLink")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                db.Workouts.Add(workouts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workouts);
        }

        // GET: Workouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutID,WorkoutName,Description,YouTubeLink")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workouts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workouts);
        }

        // GET: Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = db.Workouts.Find(id);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workouts workouts = db.Workouts.Find(id);
            db.Workouts.Remove(workouts);
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
