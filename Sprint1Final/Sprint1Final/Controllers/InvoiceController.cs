using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprint1Final.Models;

namespace Sprint1Final.Controllers
{
    public class InvoiceController : Controller
    {
        Sprint1DbEntities db = new Sprint1DbEntities();
        // GET: Invoice
        public ActionResult Index()
        {
            List<Invoice> iList = new List<Invoice>();
            var datalist = (from j in db.Jobs
                            join c in db.Customers on j.CustomerID equals c.CustomerID
                            select new { j.JobID, c.CustomerID, c.CName, j.BasicCost }).ToList();
            foreach (var item in datalist)
            {
                Invoice obj = new Invoice();
                obj.InvoiceID = ""+ item.JobID + item.CustomerID;
                obj.CustID = item.CustomerID;
                obj.CustName = item.CName;
                obj.JobId = item.JobID;
                obj.BasicCost = Convert.ToDouble(item.BasicCost);
                obj.VatAmt = obj.calcVat();
                obj.FinalCost = obj.calcFinal();
                iList.Add(obj);
            }
            return View(iList);
        }
    }
}