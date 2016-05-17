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
    [Authorize(Roles ="Trainer , Admin")]
    public class TrainersController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: Trainers
        public ActionResult Index()
        {
            var trainers = db.Trainers.Include(t => t.AspNetUsers);
            return View(trainers.ToList());
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainers trainers = db.Trainers.Find(id);
            if (trainers == null)
            {
                return HttpNotFound();
            }
            return View(trainers);
        }

        // GET: Trainers/Create
        public ActionResult Create(string Id)
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", Id);
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerID,FirstName,LastName,Email,Phone,Id")] Trainers trainers)
        {
            if (ModelState.IsValid)
            {
                db.Trainers.Add(trainers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", trainers.Id);
            return View(trainers);
        }

        // GET: Trainers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainers trainers = db.Trainers.Find(id);
            if (trainers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", trainers.Id);
            return View(trainers);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerID,FirstName,LastName,Email,Phone,Id")] Trainers trainers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", trainers.Id);
            return View(trainers);
        }

        // GET: Trainers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainers trainers = db.Trainers.Find(id);
            if (trainers == null)
            {
                return HttpNotFound();
            }
            return View(trainers);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainers trainers = db.Trainers.Find(id);
            db.Trainers.Remove(trainers);
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
