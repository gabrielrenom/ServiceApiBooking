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
            model.Name = "Summer";
            model.DayPrices = new Collection<DayPriceModel>();
            model.Start =DateTime.Now.AddMonths(7);
            model.End = DateTime.Now.AddMonths(11);
            model.DayPrices= new List<DayPriceModel>{
                new DayPriceModel { Day = 1, Dayprice = 10, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 2, Dayprice = 20, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 3, Dayprice = 30, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 4, Dayprice = 40, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 5, Dayprice = 40, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 6, Dayprice = 50, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 7, Dayprice = 50, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 8, Dayprice = 90, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 9, Dayprice = 90, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 10, Dayprice = 90, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 11, Dayprice = 90, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 12, Dayprice = 100, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },
                new DayPriceModel { Day = 13, Dayprice = 100, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser },

                };
            list.Add(model);

            //Act
            var results = await service.AddPricesWithDays(3, list);
            var results2 = await service.AddPricesWithDays(1, list);
            var results3 = await service.AddPricesWithDays(2, list);

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
        public async Task OnPrices_WhenGetAllPricesWithDaysIsCalled_BeSureAllThePricesAreReturned()
        {
            //Arrange
            var bookentityid = 2;
            //Act
            var results =await  service.GetAllPricesWithDays(bookentityid);

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

        [TestMethod]
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
                         Hourprice = 10,
                         Modified= DateTime.Now,
                         CreatedBy= localuser,                           
                           ModifiedBy =localuser,
                         
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
