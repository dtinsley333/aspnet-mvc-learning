using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using miPT_Project_6.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity.Core.Objects;
using System.Collections;
using Newtonsoft.Json;

namespace miPT_Project_6.Controllers
{
  
    public class ClientDetailsController : Controller
    {
        private miPt_ProjectEntities db = new miPt_ProjectEntities();

        // GET: ClientDetails
        public ActionResult Index()
        {
            var clientDetails = db.ClientDetails.Include(c => c.AspNetUsers).Include(c => c.Trainers);
            return View(clientDetails.ToList());
        }

        // GET: ClientDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetails clientDetails = db.ClientDetails.Find(id);
            if (clientDetails == null)
            {
                return HttpNotFound();
            }
            return View(clientDetails);
        }

        // GET: ClientDetails/Create
        public ActionResult Create(string Id)
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", Id);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "FirstName");
            return View();
        }

        // POST: ClientDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address1,Address2,City,PostCode,Email,Phone,ClientID,TrainerID")] ClientDetails clientDetails)
        {
            if (ModelState.IsValid)
            {
                db.ClientDetails.Add(clientDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", clientDetails.Id);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "FirstName", clientDetails.TrainerID);
            return View(clientDetails);
        }

        // GET: ClientDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetails clientDetails = db.ClientDetails.Find(id);
            if (clientDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", clientDetails.Id);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "FirstName", clientDetails.TrainerID);
            return View(clientDetails);
        }

        // POST: ClientDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address1,Address2,City,PostCode,Email,Phone,ClientID,TrainerID")] ClientDetails clientDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", clientDetails.Id);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "FirstName", clientDetails.TrainerID);
            return View(clientDetails);
        }

        // GET: ClientDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetails clientDetails = db.ClientDetails.Find(id);
            if (clientDetails == null)
            {
                return HttpNotFound();
            }
            return View(clientDetails);
        }

        // POST: ClientDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientDetails clientDetails = db.ClientDetails.Find(id);
            db.ClientDetails.Remove(clientDetails);
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

        // GET: MyClientDetails
        [Authorize(Roles ="Trainer")]
        public ActionResult MyClients()
        {

            var user = User.Identity.Name;
            Console.WriteLine(user.ToString());
            Console.WriteLine("Testing");

            var trainerID = db.Trainers.Where(x => x.Email == user).Select(x => x.TrainerID).FirstOrDefault();

            // var user = db.AspNetUsers.Include(u => u.UserName == id);
            var clientDetails = db.ClientDetails.Include(c => c.AspNetUsers).Include(c => c.Trainers).Where(x => x.TrainerID == trainerID);
            return View(clientDetails.ToList());
        }

        // GET: ClientList
        [Authorize(Roles = "Admin")]
        public ActionResult ClientList(int Id)
        {

            var trainerID = Id;

            // var user = db.AspNetUsers.Include(u => u.UserName == id);
            var clientDetails = db.ClientDetails.Include(c => c.AspNetUsers).Include(c => c.Trainers).Where(x => x.TrainerID == trainerID);
            return View(clientDetails.ToList());
        }

        // GET: MyClientDetails
        [Authorize(Roles ="Client")]
        public ActionResult Summary()
        {
            var user = User.Identity.Name;
            var todayDate = Convert.ToDateTime(DateTime.Today);
            var Id = db.AspNetUsers.Where(x => x.Email == user).Select(x => x.Id).SingleOrDefault();
            var ClientID = db.ClientDetails.Where(x => x.Email == user).Select(x => x.ClientID).FirstOrDefault();
            ViewBag.User1 = User.Identity.Name;
            var TrainerId = db.ClientDetails.Where(x => x.ClientID == ClientID).Select(x => x.TrainerID).FirstOrDefault();
            ViewBag.TrainerName = db.Trainers.Where(x => x.TrainerID == TrainerId).Select(x => x.FirstName).FirstOrDefault();
            ViewBag.UserFirstName = db.ClientDetails.Where(x => x.Email == user).Select(x => x.FirstName).FirstOrDefault();
            ViewBag.CurrentWeight = db.Measurements.Where(x => x.ClientID == ClientID).OrderByDescending(x=>x.Date).Select(x => x.BodyWeight).FirstOrDefault();
            ViewBag.OriginalWeight = db.Measurements.Where(x => x.ClientID == ClientID).OrderBy(x => x.Date).Select(x => x.BodyWeight).FirstOrDefault();
            ViewBag.WeightChange = double.Parse(ViewBag.CurrentWeight) - double.Parse(ViewBag.OriginalWeight);
            ViewBag.CurrentBodyFat = db.Measurements.Where(x => x.ClientID == ClientID).OrderByDescending(x => x.Date).Select(x => x.BodyFat).FirstOrDefault();
            ViewBag.OriginalBodyFat = db.Measurements.Where(x => x.ClientID == ClientID).OrderBy(x => x.Date).Select(x => x.BodyFat).FirstOrDefault();
            ViewBag.FatChange = (ViewBag.CurrentBodyFat) - (ViewBag.OriginalBodyFat);
            ViewBag.CurrentAge = db.Measurements.Where(x => x.ClientID == ClientID).OrderByDescending(x => x.Date).Select(x => x.MetabolicAge).FirstOrDefault();
            ViewBag.OriginalAge = db.Measurements.Where(x => x.ClientID == ClientID).OrderBy(x => x.Date).Select(x => x.MetabolicAge).FirstOrDefault();
            ViewBag.AgeChange = (ViewBag.CurrentAge) - (ViewBag.OriginalAge);
            ViewBag.NextWorkoutDate = db.UserWorkouts.Where(x => x.Id == Id).OrderBy(x=>x.Date).Where(x =>DbFunctions.TruncateTime(x.Date) > DateTime.Today.Date).Select(x=>x.Date).FirstOrDefault();
            var NextWorkoutNameLookUp = db.UserWorkouts.Where(x => x.Id == Id).OrderBy(x=>x.Date).Where(x =>DbFunctions.TruncateTime(x.Date) > DateTime.Today.Date).Select(x=>x.WorkoutID).FirstOrDefault();
            ViewBag.NextWorkoutName = db.Workouts.Where(x => x.WorkoutID == NextWorkoutNameLookUp).Select(x => x.WorkoutName).FirstOrDefault();



            var trainerID = db.Trainers.Where(x => x.Email == user).Select(x => x.TrainerID).FirstOrDefault();

            // var user = db.AspNetUsers.Include(u => u.UserName == id);
            var clientDetails = db.ClientDetails.Include(c => c.AspNetUsers).Include(c => c.Trainers).Where(x => x.TrainerID == trainerID);

            //array for weight measurements
            ArrayList weightData = new ArrayList();
            ArrayList header = new ArrayList { "Date", "Weight" };
            weightData.Add(header);

            //using anonymous item
            var weightItems = db.Measurements.OrderBy(x => x.Date).Select(i => new {
                ID = i.ClientID,
                Weight = i.BodyWeight,
                Date = i.Date
            });
            
            //loop for data for current client
            foreach (var item in weightItems)
            {
                if (item.ID == ClientID)
                {
                    ArrayList measurements = new ArrayList { (item.Date).ToShortDateString(), double.Parse(item.Weight) };
                    weightData.Add(measurements);
                }
            }
            //convert to viewbag
            string dataStr = JsonConvert.SerializeObject(weightData, Formatting.None);
            ViewBag.WeightData = new HtmlString(dataStr);

            //array for bodyfat measurements
            ArrayList bodyFatData = new ArrayList();
            ArrayList bodyfatHeader = new ArrayList { "Date", "BodyFat Percentage" };
            bodyFatData.Add(bodyfatHeader);

            //using anonymous item
            var bodyFatItems = db.Measurements.OrderBy(x => x.Date).Select(i => new {
                ID = i.ClientID,
                Fat = i.BodyFat,
                Date = i.Date
            });

            //loop for data for current client
            foreach (var item in bodyFatItems)
            {
                if (item.ID == ClientID)
                {
                    ArrayList measurements = new ArrayList { (item.Date).ToShortDateString(), (item.Fat) };
                    bodyFatData.Add(measurements);
                }
            }
            //convert to viewbag
            string bodyFatDataString = JsonConvert.SerializeObject(bodyFatData, Formatting.None);
            ViewBag.BodyFatData = new HtmlString(bodyFatDataString);

            //array for metabolic age measurements
            ArrayList metAgeData = new ArrayList();
            ArrayList metAgeHeader = new ArrayList { "Date", "Metabolic Age" };
            metAgeData.Add(metAgeHeader);

            //using anonymous item
            var metAgeItems = db.Measurements.OrderBy(x => x.Date).Select(i => new {
                ID = i.ClientID,
                MetAge = i.MetabolicAge,
                Date = i.Date
            });

            //loop for data for current client
            foreach (var item in metAgeItems)
            {
                if (item.ID == ClientID)
                {
                    ArrayList measurements = new ArrayList { (item.Date).ToShortDateString(), (item.MetAge) };
                    metAgeData.Add(measurements);
                }
            }
            //convert to viewbag
            string metAgeDataString = JsonConvert.SerializeObject(metAgeData, Formatting.None);
            ViewBag.MetAgeData = new HtmlString(metAgeDataString);


            return View();
        }
    }
}