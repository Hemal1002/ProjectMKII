using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sprint1Final.Models
{
    public class Invoice
    {
        [Key]
        public string InvoiceID { get; set; }
        public string CustID { get; set; }
        public string CustName { get; set; }
        public string JobId { get; set; }
        public double BasicCost { get; set; }
        public double VatAmt { get; set; }
        public double FinalCost { get; set; }

       

        public double calcVat()
        {
            double v = BasicCost * 0.14;
            return v;
        }

        public double calcFinal()
        {
            double f = BasicCost + calcVat();
            return f;
        }
    }
}