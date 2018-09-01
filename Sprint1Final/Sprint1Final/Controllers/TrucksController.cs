using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint1Final.Models;

namespace Sprint1Final.Controllers
{
    public class TrucksController : Controller
    {
        private Sprint1DbEntities db = new Sprint1DbEntities();

        // GET: Trucks
        public ActionResult Index(string search, string option)
        {
            var trucks = db.Trucks.Include(t => t.TruckID);

            if (option == "vn")
            {
                return View(db.Trucks.Where(x => x.Vin == search || search == null).ToList());
            }
            else if (option == "lp")
            {
                return View(db.Trucks.Where(x => x.LP == search || search == null).ToList());
            }
            else if (option == "ts")
            {
                return View(db.Trucks.Where(x => x.Tstat == search || search == null).ToList());
            }
            else if (option == "ch")
            {
                return View(db.Trucks.Where(x => x.Chassis == search || search == null).ToList());
            }
            else
            {
                return View(db.Trucks.Where(x => x.TruckID == search || search == null).ToList());
            }
            return View(trucks.ToList());
        }

        // GET: Trucks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // GET: Trucks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trucks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TruckID,Vin,LP,Tstat,Make,Model,Chassis,Cabin,Engine,Steering,Powert,Torque,GearBox,TankCap,Milage,Weight,MaxLoad,PDate,LSD")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Trucks.Add(truck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(truck);
        }

        // GET: Trucks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Trucks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TruckID,Vin,LP,Tstat,Make,Model,Chassis,Cabin,Engine,Steering,Powert,Torque,GearBox,TankCap,Milage,Weight,MaxLoad,PDate,LSD")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(truck);
        }

        // GET: Trucks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Truck truck = db.Trucks.Find(id);
            db.Trucks.Remove(truck);
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
