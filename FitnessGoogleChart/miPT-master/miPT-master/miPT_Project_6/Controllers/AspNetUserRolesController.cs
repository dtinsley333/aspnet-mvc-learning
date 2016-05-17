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
    public class AspNetUserRolesController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: AspNetUserRoles
        public ActionResult Index()
        {
            var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetUsers);
            ViewBag.RoleName = db.AspNetRoles.Select(a => a.Id);
            return View(aspNetUserRoles.ToList());
        }

        // GET: AspNetUserRoles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.SingleOrDefault(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View();
        }

        // POST: AspNetUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AspNetUserRoles.Add(aspNetUserRoles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error");
                }
                
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.SingleOrDefault(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.SingleOrDefault(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.SingleOrDefault(m => m.UserId == id);
            db.AspNetUserRoles.Remove(aspNetUserRoles);
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

        // GET: AspNetUserRoles/Error
        public ActionResult Error()
        {
            return View();
        }
    }
}
