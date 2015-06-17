using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.DataAccess.Managers;
using ACP.Business.Services;
using ACP.Business.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ACP.Business.Test
{
    [TestClass]
    public class BookingPricingTest
    {
        private IACPRepository repository;
        private IBookingPricingManager bookingpricingmanager;
        private IBookingEntityManager bookingentitymanager;
        private IBookingPricingService service;
        private string localuser;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            bookingpricingmanager = new BookingPricingManager(repository);
            bookingentitymanager = new BookingEntityManager(repository);
            service = new BookingPricingService(bookingpricingmanager,bookingentitymanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public void WhenAnEntityIsGiven_WhenOnlyDaysAreSelected_BeSureTheyAreAdded()
        {
            //Arrange
            IList<BookingPricingModel> list = new List<BookingPricingModel>();

            BookingPricingModel model = new BookingPricingModel();
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Name = "Winter";
            model.DayPrices = new Collection<DayPriceModel>();
            model.DayPrices.Add(new DayPriceModel
            {
                Created = DateTime.Now,
                Day = 1,
                Modified = DateTime.Now,
                Dayprice = 0,
                CreatedBy = localuser,
                HourPrices = new Collection<HourPriceModel>() 
                { 
                    new HourPriceModel
                    {
                         Created = DateTime.Now,                         
                         HourMinute = DateTime.Now,
                         Hourprice = 0                       
                    }
                }
            });
            list.Add(model);

            //Act
            var results = service.AddPricesWithDays(1, list);

            //Assert
            Assert.IsTrue(results != null);
        }

        [TestMethod]
        public void OnPrices_WhenGetAllIsCalled_BeSureAllThePricesAreReturned()
        {
            //Arrange

            //Act
            var results = service.GetAll();

            //Assert
            Assert.IsNotNull(results);
        }

     
    }
}
