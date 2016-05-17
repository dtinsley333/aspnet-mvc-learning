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
    public class UserWorkoutsController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: UserWorkouts
        public ActionResult Index()
        {
            var userWorkouts = db.UserWorkouts.Include(u => u.AspNetUsers).Include(u => u.Workouts);
            return View(userWorkouts.ToList());
        }

        // GET: UserWorkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWorkouts userWorkouts = db.UserWorkouts.Find(id);
            if (userWorkouts == null)
            {
                return HttpNotFound();
            }
            return View(userWorkouts);
        }

        // GET: UserWorkouts/Create
        public ActionResult Create(string ClientID)
        {

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", ClientID);
            ViewBag.WorkoutID = new SelectList(db.Workouts, "WorkoutID", "WorkoutName");
            return View();
        }

        // POST: UserWorkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkoutID,UserWorkoutID,Date")] UserWorkouts userWorkouts)
        {
            if (ModelState.IsValid)
            {
                db.UserWorkouts.Add(userWorkouts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", userWorkouts.Id);
            ViewBag.WorkoutID = new SelectList(db.Workouts, "WorkoutID", "WorkoutName", userWorkouts.WorkoutID);
            return View(userWorkouts);
        }

        // GET: UserWorkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWorkouts userWorkouts = db.UserWorkouts.Find(id);
            if (userWorkouts == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", userWorkouts.Id);
            ViewBag.WorkoutID = new SelectList(db.Workouts, "WorkoutID", "WorkoutName", userWorkouts.WorkoutID);
            return View(userWorkouts);
        }

        // POST: UserWorkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkoutID,UserWorkoutID,Date")] UserWorkouts userWorkouts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userWorkouts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", userWorkouts.Id);
            ViewBag.WorkoutID = new SelectList(db.Workouts, "WorkoutID", "WorkoutName", userWorkouts.WorkoutID);
            return View(userWorkouts);
        }

        // GET: UserWorkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWorkouts userWorkouts = db.UserWorkouts.Find(id);
            if (userWorkouts == null)
            {
                return HttpNotFound();
            }
            return View(userWorkouts);
        }

        // POST: UserWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserWorkouts userWorkouts = db.UserWorkouts.Find(id);
            db.UserWorkouts.Remove(userWorkouts);
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
