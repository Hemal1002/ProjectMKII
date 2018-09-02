using Sprint1Final.Models;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sprint1Final.Models.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void checkAbLoadTest()
        {
            Job obj = new Job();

            obj.CWeight = 2000;
            obj.CWidth = 200;
            obj.CLength = 1800;
            obj.CHeight = 400;

            string exp = "Normal";

            string act = obj.checkAbLoad();

            Assert.AreEqual(exp, act);
        }

        [TestMethod()]
        public void calcETATest()
        {
            Job obj = new Job();


            obj.x = "Abnormal";
            obj.Dist = "40";

            DateTime exp = new DateTime(0,0,0,01,0,0);

            DateTime act = obj.calcETA();

            Assert.AreEqual(exp, act);


        }

        [TestMethod()]
        public void calcBasicCostTest()
        {
            Job obj = new Job();


            obj.x = "Abnormal";
            obj.Dist = "40";

            DateTime exp = new DateTime(0, 0, 0, 01, 0, 0);

            DateTime act = obj.calcETA();

            Assert.AreEqual(exp, act);


        }


    }
}

