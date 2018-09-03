using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint1Final.Models;
using PagedList;
using PagedList.Mvc;

namespace Sprint1Final.Controllers
{
    public class TrucksController : Controller
    {
        private Sprint1DbEntities db = new Sprint1DbEntities();

        // GET: Trucks
        public ActionResult Index(string search, string option,string sortOrder, string currentFilter, int?page)
        {
            var trucks = db.Trucks.Include(t => t.TruckID);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TruckSortParm = String.IsNullOrEmpty(sortOrder) ? "vin_desc" : "";
            ViewBag.ModelSortParm = sortOrder == "model" ? "m_desc" : "model";
            ViewBag.Mileage = sortOrder == "milage" ? "mile_desc" : "milage";
            ViewBag.Weight = sortOrder == "weight" ? "w_desc" : "weight";
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            var truck = from t in db.Trucks select t;

            if (option == "vn")
            {
                truck = truck.Where(x => x.Vin == search || search == null);
            }
            else if (option == "lp")
            {
                truck = truck.Where(x => x.LP == search || search == null);
            }
            else if (option == "ts")
            {
                truck = truck.Where(x => x.Tstat == search || search == null);
            }
            else if (option == "ch")
            {
                truck = truck.Where(x => x.Chassis == search || search == null);
            }
            else
            {
                truck = truck.Where(x => x.TruckID == search || search == null);
            }
            switch (sortOrder)
            {
                case "vin_desc":
                    truck = truck.OrderByDescending(t=>t.Vin);
                    break;
                case "model":
                    truck = truck.OrderBy(t => t.Model);
                    break;
                case "m_desc":
                    truck = truck.OrderByDescending(t=>t.Model);
                    break;
                case "milage":
                    truck = truck.OrderBy(t=>t.Milage);
                    break;
                case "mile_desc":
                    truck = truck.OrderByDescending(t=>t.Milage);
                    break;
                case "weight":
                    truck = truck.OrderBy(t=>t.Weight);
                    break;
                case "w_desc":
                    truck = truck.OrderByDescending(t=>t.Weight);
                    break;
                default:
                    truck = truck.OrderBy(t=>t.Vin);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(truck.ToPagedList(pageNumber, pageSize));
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
