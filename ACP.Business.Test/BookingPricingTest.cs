using System;
using System.Linq;
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
using System.Threading.Tasks;

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
        public async Task WhenAnEntityIsGiven_WhenOnlyDaysAreSelected_BeSureTheyAreAdded()
        {
            //Arrange
            IList<BookingPricingModel> list = new List<BookingPricingModel>();

            BookingPricingModel model = new BookingPricingModel();
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.Name = "Winter";
            model.DayPrices = new Collection<DayPriceModel>();
            model.Start = DateTime.Now;
            model.End = DateTime.Now;
            model.DayPrices.Add(new DayPriceModel
            {
                Created = DateTime.Now,
                Day = 1,
                Modified = DateTime.Now,
                Dayprice = 0,
                CreatedBy = localuser,
                //HourPrices = new Collection<HourPriceModel>() 
                //{ 
                //    new HourPriceModel
                //    {
                //         Created = DateTime.Now,                         
                //         HourMinute = DateTime.Now,
                //         Hourprice = 0,
                //          Modified= DateTime.Now
                //    }
                //}
            });
            list.Add(model);

            //Act
            var results = await service.AddPricesWithDays(2, list);

            //Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task WhenAnEntityIsGiven_WhenOnlyDaysAndHoursAreSelected_BeSureTheyAreAdded()
        {
            //Arrange
            IList<BookingPricingModel> list = new List<BookingPricingModel>();

            BookingPricingModel model = new BookingPricingModel();
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.Name = "Winter";
            model.DayPrices = new Collection<DayPriceModel>();
            model.Start = DateTime.Now;
            model.End = DateTime.Now;
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
                         HourMinute = new TimeSpan(11,00,00),
                         Hourprice = 0,
                          Modified= DateTime.Now
                    }
                }
            });
            list.Add(model);

            //Act
            var results = await service.AddPricesWithDays(2, list);

            //Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task WhenAnEntityIsGiven_WhenOnlyDaysAreSelected_BeSureTheyAreUpdated()
        {
            //Arrange
            var allprices = await service.GetAllPricesWithDays(2);
             allprices.FirstOrDefault().Name = "A new One";

            //Act
             var results = await service.UpdatePricesWithDays(2, allprices.ToList());

            //Assert
            Assert.IsTrue(results);
            var name = await service.GetAllPricesWithDays(2); ;
            Assert.AreEqual(name.FirstOrDefault().Name, "A new One");
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

        [TestMethod]
        public void OnPrices_WhenGetAllPricesWithDaysIsCalled_BeSureAllThePricesAreReturned()
        {
            //Arrange
            var bookentityid = 2;
            //Act
            var results = service.GetAllPricesWithDays(bookentityid);

            //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void OnAnEntityId_WhenDeleteByIdIsCalled_BeSureThePriceIsRemoved()
        {
            //Arrange
            var bookentityid = 6;
            //Act
            var results = service.DeleteById(bookentityid);

            //Assert
            Assert.IsNotNull(results);
        }
        public async Task ADayAndTimeIsGiven_WhenIsAdded_BeSureTheyAreReturned()
        {
            //Arrange
            IList<BookingPricingModel> list = new List<BookingPricingModel>();
          
            BookingPricingModel model = new BookingPricingModel();
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.Name = "Winter";
            model.DayPrices = new Collection<DayPriceModel>();
            model.Start = DateTime.Now;
            model.End = DateTime.Now;
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
                         HourMinute = new TimeSpan(12,00,00),
                         Hourprice = 0,
                         Modified= DateTime.Now,
                         
                    }
                }
            });
            list.Add(model);

            //Act
            var results = await service.AddPricesWithDaysAndTimes(2, list);

            //Assert
            Assert.IsTrue(results);
        }
    }
}
