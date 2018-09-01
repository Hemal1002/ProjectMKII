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
    public class JobsController : Controller
    {
        private Sprint1DbEntities db = new Sprint1DbEntities();

        // GET: Jobs
        public ActionResult Index(string search, string option)
        {
            var jobs = db.Jobs.Include(j => j.Cargo).Include(j => j.Customer).Include(j => j.Driver).Include(j => j.Truck);

            if (option == "js")
            {
                return View(db.Jobs.Where(x => x.jState == search || search == null).ToList());
            }
            else
            {
                if (option == "jd")
                {
                    return View(db.Jobs.Where(x => x.DriverNo == search || search == null).ToList());
                }
                else
                {
                    if (option == "JobID")
                    {
                        return View(db.Jobs.Where(x => x.JobID == search || search == null).ToList());
                    }
                }

            }


            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.CargoID = new SelectList(db.Cargoes, "CargoID", "CType");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CName");
            ViewBag.DriverNo = new SelectList(db.Drivers, "DriverNo", "DName");
            ViewBag.TruckID = new SelectList(db.Trucks.Where(t => t.Tstat != "Busy"), "TruckID", "Vin");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,jState,SLoc,STime,ELoc,ETA,Dist,BasicCost,ActArrive,CHeight,CLength,CWidth,CWeight,AbLoad,MBefore,MAfter,ExpctFuel,ActFuel,Flag,TruckID,CustomerID,CargoID,DriverNo")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.BasicCost = job.calcBCost();
                //job.ETA = job.calcETA();
                job.AbLoad = job.checkAbLoad();
                job.Flag = job.checkFlag();
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CargoID = new SelectList(db.Cargoes, "CargoID", "CType", job.CargoID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CName", job.CustomerID);
            ViewBag.DriverNo = new SelectList(db.Drivers, "DriverNo", "DName", job.DriverNo);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Vin", job.TruckID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoID = new SelectList(db.Cargoes, "CargoID", "CType", job.CargoID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CName", job.CustomerID);
            ViewBag.DriverNo = new SelectList(db.Drivers, "DriverNo", "DName", job.DriverNo);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Vin", job.TruckID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,jState,SLoc,STime,ELoc,ETA,Dist,BasicCost,ActArrive,CHeight,CLength,CWidth,CWeight,AbLoad,MBefore,MAfter,ExpctFuel,ActFuel,Flag,TruckID,CustomerID,CargoID,DriverNo")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CargoID = new SelectList(db.Cargoes, "CargoID", "CType", job.CargoID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CName", job.CustomerID);
            ViewBag.DriverNo = new SelectList(db.Drivers, "DriverNo", "DName", job.DriverNo);
            ViewBag.TruckID = new SelectList(db.Trucks, "TruckID", "Vin", job.TruckID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
