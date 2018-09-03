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
    public class CargoesController : Controller
    {
        private Sprint1DbEntities db = new Sprint1DbEntities();

        // GET: Cargoes
        public ActionResult Index(string option, string search, string sortOrder , string currentFilter , int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CargoTypeSortParm = String.IsNullOrEmpty(sortOrder) ? "cargo_desc" : "";
            ViewBag.CargoRateSortParm = sortOrder == "Rate" ? "rate_desc" : "Rate";
            ViewBag.WeightRate = sortOrder =="Weight" ? "weight_desc" : "Weight";
            ViewBag.DistanceRate = sortOrder == "Dist" ? "dist_rate" : "Dist";
            if (search!=null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            var cargo = from c in db.Cargoes
                        select c;
            if (option == "CType")
            {
                cargo = cargo.Where(x => x.CType == search || search == null);
            }
            else if (option == "Rate")
            {
                cargo = cargo.Where(x => x.HazPer.ToString() == search || search == null);
            }
            else if (option == "WeightR")
            {
                cargo = cargo.Where(x => x.WRate.ToString() == search || search == null);
            }
            else if (option == "DR")
            {
                cargo = cargo.Where(x => x.DRate.ToString() == search || search == null);
            }
            else
            {
                cargo = cargo.Where(x => x.CargoID.StartsWith(search) || search == null);
            }

            switch (sortOrder)
            {
                case "cargo_desc":
                    cargo = cargo.OrderByDescending(c => c.CType);
                    break;
                case "Rate":
                    cargo = cargo.OrderBy(c => c.HazPer);
                        break;
                case "rate_desc":
                    cargo = cargo.OrderByDescending(c => c.HazPer);
                    break;
                case "Weight":
                    cargo = cargo.OrderBy(c => c.WRate);
                    break;
                case "weight_desc":
                    cargo = cargo.OrderByDescending(c => c.WRate);
                    break;
                case "Dist":
                    cargo = cargo.OrderBy(c => c.DRate);
                    break;
                case "dist_rate":
                    cargo = cargo.OrderByDescending(c => c.DRate);
                    break;
                default:
                    cargo = cargo.OrderBy(c => c.CType);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(cargo.ToPagedList(pageNumber, pageSize));

                        
        }

        public ActionResult Reset()
        {
            return View();
        }

        // GET: Cargoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoID,CType,HazPer,WRate,DRate")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Cargoes.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoID,CType,HazPer,WRate,DRate")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cargo cargo = db.Cargoes.Find(id);
            db.Cargoes.Remove(cargo);
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
