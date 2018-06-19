using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ACP.Business.Test
{

    class Slot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    [TestClass]
    public class TimeTest
    {
        //ID,start,end
        //1,1/1/2013,1/7/2013
        //2,1/8/2013,1/15/2013

        static List<List<object>> input = new List<List<object>> {
            new List<object>{1,DateTime.Parse("1/1/2013"),DateTime.Parse("1/7/2013")},
            new List<object>{2,DateTime.Parse("1/8/2013"),DateTime.Parse("1/5/2013")}
        };

        [TestMethod]
        public void TestMethod1()
        {
            List<Slot> slots = new List<Slot> {
                    new Slot { Id=1, Name="first", StartDate = new DateTime(2015,1,1, 3,00,00) , EndDate =new DateTime(2015,1,3, 22,00,00)},
                    new Slot { Id=2, Name="second", StartDate = new DateTime(2015,1,5,3,00,00 ) , EndDate =new DateTime(2015,1,7, 22,00,00)},
                    new Slot { Id=3, Name="third", StartDate = new DateTime(2015,1,10,2,00,00) , EndDate =new DateTime(2015,1,12, 22,00,00)}
            };


            DateTime startDate1 = new DateTime(2015, 1, 1, 2, 00, 00); //NOT OK
            DateTime endDate1 = new DateTime(2015, 1, 2, 13, 00, 00);

            DateTime startDate2 = new DateTime(2015, 1, 1, 2, 00, 00); //NOT OK
            DateTime endDate2 = new DateTime(2015, 1, 4, 20, 00, 00);

            DateTime startDate3 = new DateTime(2015, 1, 4, 3, 00, 00); //OK
            DateTime endDate3 = new DateTime(2015, 1, 5, 2, 00, 00);

            DateTime startDate4 = new DateTime(2015, 1, 12, 23, 00, 00); //OK
            DateTime endDate4 = new DateTime(2015, 1, 14, 2, 00, 00);

            var result1 = slots.Where(x => x.StartDate >= startDate1 && x.StartDate <= endDate1).ToList();
            Assert.IsTrue(result1.Count > 0);

            var result2 = slots.Where(x => x.StartDate >= startDate2 && x.StartDate <= endDate2).ToList();
            Assert.IsTrue(result2.Count > 0);

            var result3 = slots.Where(x => x.StartDate >= startDate3 && x.StartDate <= endDate3).ToList();
            Assert.IsTrue(result3.Count > 0);

            var result4 = slots.Where(x => x.StartDate >= startDate4 && x.StartDate <= endDate4).ToList();
            Assert.IsTrue(result4.Count == 0);          

        }

        [TestMethod]
        public void TestConversion()
        {
            string myDate = "30-12-1899 07:50:00:AM";
            DateTime dt1 = DateTime.ParseExact(myDate, "dd-MM-yyyy HH:mm:ss:tt",
                                                       CultureInfo.InvariantCulture);


            string date = "18-06-2018 18:06:59";


            CultureInfo culture = new CultureInfo("en-UK");

            DateTime dt2 = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm:ss", culture);
            
        }
    }
}
