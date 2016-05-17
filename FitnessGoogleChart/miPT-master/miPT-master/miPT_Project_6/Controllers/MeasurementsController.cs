using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using miPT_Project_6.Models;
using System.Collections;
using Newtonsoft.Json;

namespace miPT_Project_6.Controllers
{
    public class MeasurementsController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: Measurements
        public ActionResult Index()
        {
            var measurements = db.Measurements.Include(m => m.ClientDetails);
            return View(measurements.ToList());
        }

        // GET: Measurements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // GET: Measurements/Create
        public ActionResult Create(int ClientID)
        {
            ViewBag.ClientID = new SelectList(db.ClientDetails, "ClientID", "ClientID", ClientID);
            ClientID = Int32.Parse(Request.QueryString["ClientID"]);
            return View();
        }

        // POST: Measurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeasurementID,Date,ClientID,BodyWeight,BodyFat,BMR,MetabolicAge,Water,Visceral,BoneMass,LeanMass,PhysiqueRating,Chest,Waist,Hips,Abdomen")] Measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.Measurements.Add(measurements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.ClientDetails, "ClientID", "Id", measurements.ClientID);
            return View(measurements);
        }

        // GET: Measurements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.ClientDetails, "ClientID", "Id", measurements.ClientID);
            return View(measurements);
        }

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeasurementID,Date,ClientID,BodyWeight,BodyFat,BMR,MetabolicAge,Water,Visceral,BoneMass,LeanMass,PhysiqueRating,Chest,Waist,Hips,Abdomen")] Measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.ClientDetails, "ClientID", "Id", measurements.ClientID);
            return View(measurements);
        }

        // GET: Measurements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Measurements measurements = db.Measurements.Find(id);
            db.Measurements.Remove(measurements);
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

        public ActionResult ProgressCharts()
        {

            // prepare array in c#
            // convert it in json
            // store in viewdata/viewbag

            //db.Measurements.OrderBy(db.Measurements)

            ArrayList data = new ArrayList();
            ArrayList header = new ArrayList { "Date", "Weight" };
            data.Add(header);

            //using anonymous item
            var items = db.Measurements.OrderBy(x=>x.Date).Select(i => new {
                ID = i.ClientID,
                Weight = i.BodyWeight,
                Date = i.Date
            });

            foreach(var item in items)
            {
                if (item.ID == 3){
                    ArrayList measurements = new ArrayList {(item.Date).ToShortDateString(), double.Parse(item.Weight) };
                    data.Add(measurements);

                }
                
            }

            string dataStr = JsonConvert.SerializeObject(data, Formatting.None);

            ViewBag.Data = new HtmlString(dataStr);



            return View();
        }
    }

}
