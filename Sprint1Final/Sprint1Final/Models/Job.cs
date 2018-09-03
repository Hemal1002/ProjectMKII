//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sprint1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using System.Linq;
    using System.Data.Entity;
    
    public partial class Job
    {
        Sprint1DbEntities db = new Sprint1DbEntities();

        [Key]
        [Required]
        [DisplayName("Job ID")]
        public string JobID { get; set; }

        [Required]
        [DisplayName("Job Status")]
        public string jState { get; set; }

        [Required]
        [DisplayName("Start Location")]
        public string SLoc { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public System.DateTime STime { get; set; }

        [Required]
        [DisplayName("End Location")]
        public string ELoc { get; set; }

        
        [DisplayName("Estimated Time Of Arrival")]
        public System.DateTime ETA { get; set; }

        [Required]
        [DisplayName("Trip Distance")]
        public string Dist { get; set; }

        
        [DisplayName("Basic Cost")]
        public double BasicCost { get; set; }

        [DisplayName("Actual Arrival Time")]
        public Nullable<System.DateTime> ActArrive { get; set; }

        [Required]
        [DisplayName("Cargo Height")]
        [MaxLength(12, ErrorMessage = "Enter the height in meters")]
        public double CHeight { get; set; }

        [Required]
        [DisplayName("Cargo Length")]
        public double CLength { get; set; }

        [Required]
        [DisplayName("Cargo Width")]
        [MaxLength(3, ErrorMessage = "The cargo width has to be three meters or below")]
        public double CWidth { get; set; }

        [Required]
        [DisplayName("Cargo Weight")]
        public double CWeight { get; set; }

        
        [DisplayName("Abnormal Load")]
        public string AbLoad { get; set; }

        [Required]
        [DisplayName("Milage Before")]
        public double MBefore { get; set; }

        [DisplayName("Milage After")]
        public Nullable<double> MAfter { get; set; }

        [Required]
        [DisplayName("Expected Fuel Burn")]
        public double ExpctFuel { get; set; }

        [DisplayName("Actual Fuel Burned")]
        public Nullable<double> ActFuel { get; set; }

        
        [DisplayName("Alert")]
        public string Flag { get; set; }

        [Required]
        [DisplayName("Truck ID")]
        public string TruckID { get; set; }

        [Required]
        [DisplayName("Customer ID")]
        public string CustomerID { get; set; }

        [Required]
        [DisplayName("Cargo ID")]
        public string CargoID { get; set; }

        [Required]
        
        [DisplayName("Driver No")]
        public string DriverNo { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Truck Truck { get; set; }


        public string checkAbLoad()
        {
            string a = "Normal";

            if (CWeight > 23000 || CWidth > 260 || CLength > 1850 || CHeight > 430)
            {
                a = "Abnormal";
            }

            return a;
        }

        public string x = "";

        public DateTime calcETA()
        {
            int a = 0;

            x = checkAbLoad();

            if (x == "Abnormal")
            {
                a = int.Parse(Dist) / 40;
            }
            else
            {
                a = int.Parse(Dist) / 80;
            }


            return DateTime.Parse(a.ToString("00/00/00 00:00:00"));
        }

        public string a = "";

        public double calcBCost()
        {
            double c = 0.0;

            var dr = (from q in db.Cargoes where q.CargoID == CargoID select q.DRate).Single();
            var wr = (from q in db.Cargoes where q.CargoID == CargoID select q.WRate).Single();
            var hz = (from q in db.Cargoes where q.CargoID == CargoID select q.HazPer).Single();

            double x = 0.0;
            double cw = Convert.ToDouble(CWeight);
            int dist = Convert.ToInt32(Dist);
            x = (cw * (Convert.ToDouble(wr)) + (dist * (Convert.ToDouble(dr))));
            c = c + (x * (hz / 100));

            a = checkAbLoad();
            if (a == "Abnormal")
            {
                c = c + (x * 0.35);

            }

            return c;
        }

        public double calcMile()
        {
            double m = 0.0;

            if (MAfter != null)
            {

                m = double.Parse(MBefore - MAfter+"");

            }

            return m;
        }

        public bool checkFuel()
        {
            bool f = false;

            if (ActFuel != null)
            {
                double ex = ExpctFuel * 0.15;

                if (ActFuel > (ExpctFuel) + ex)
                {
                    f = true;
                }
            }

            return f;
        }

        public string checkFlag()
        {
            string flag = "False";

            if (jState == "Complete")
            {
                if (calcMile() > (double.Parse(Dist) + (double.Parse(Dist) * 0.15)) || checkFuel() == true)
                {
                    flag = "TRUE";
                }

            }

            return flag;
        }
    }
}
