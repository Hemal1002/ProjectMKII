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
    public class DriversController : Controller
    {
        private Sprint1DbEntities db = new Sprint1DbEntities();

        // GET: Drivers
        public ActionResult Index(string option, string search, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            var driver = from d in db.Drivers select d;
            if (option=="dname")
            {
                driver = (driver.Where(x => x.DName == search || search == null));
            }
            else if (option=="did")
            {
                driver = (driver.Where(x => x.DriverID== search || search == null));
            }
            else if (option=="lic")
            {
                driver = (driver.Where(x => x.Licen== search || search == null));
            }
            else if (option=="cnum")
            {
                driver = (driver.Where(x => x.ConNum == search || search == null));
            }
            else 
            {
                driver = driver.Where(x => x.DriverNo.StartsWith(search) || search == null);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    driver = driver.OrderByDescending(d => d.DName);
                    break;
                case "Date":
                    driver = driver.OrderBy(d => d.DOE);
                    break;
                case "date_desc":
                    driver = driver.OrderByDescending(d => d.DOE);
                    break;
                default:
                    driver = driver.OrderBy(d => d.DName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(driver.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Drivers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverNo,DName,DriverID,Licen,DOE,Adrs,ConNum,nokCNum")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driver);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverNo,DName,DriverID,Licen,DOE,Adrs,ConNum,nokCNum")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
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
