using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Managers;
using ACP.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class BookingTest
    {
        private IACPRepository repository;
        private IBookingManager bookingmanager;
        private IBookingPricingManager bookingpricingmanager;
        private IQuoteService service;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            bookingmanager = new BookingManager(repository);
            bookingpricingmanager= new BookingPricingManager(repository);
            service = new QuoteService(bookingmanager, bookingpricingmanager);
        }

        [TestMethod]
        public void GivenADateAndPlace_WhenUserGetQuote_ThenEnsureAllResultsAreReturned()
        { 
            //Arrange            
            QuoteModel quote = new QuoteModel();
            quote.Pickup= new DateTime(2015, 07, 14,18,40,00);
            quote.Dropoff=new DateTime(2015, 07, 16,16,30,00);
            quote.PickupLocation = new LocationModel(){Id=1,Name="Manchester"};
            quote.DropoffLocation= new LocationModel(){Id=1,Name="Manchester"};
            quote.BookingServices.Add(new BookingServiceModel(){ Name="Carpark"});

            

              //$datediff = $edate - $sdate;
              //$days = (floor($datediff/(60*60*24))+1);
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='".$row1['id']."' AND (`sdate` <= '".$sdate."' AND `edate` >= '".$sdate."') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='8' AND (`sdate` <= '1431692800' AND `edate` >= '1441692800') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_content` WHERE `cpid` = '8'
            //SELECT * FROM `carparks_availability` WHERE `cpid`='1'")

            //Act
            var result = service.GetQuote(quote);

           //Assert
            Assert.IsNotNull(result);
           // Assert.AreEqual(result.BookingPricingItems.FirstOrDefault().Name, "Summer");
            
        }

    }
}
