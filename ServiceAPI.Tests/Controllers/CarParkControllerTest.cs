using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ServiceAPI.Controllers;
using ACP.Business.Services.Interfaces;
using ACP.Business.Managers;
using ACP.DataAccess.Managers;
using ACP.Business.Repository;
using ACP.DataAccess.Repository;
using ACP.Business.Services;
using ServiceAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using NSubstitute;
using System.Linq;
using ACP.Business.Models;
using System.Collections.ObjectModel;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class CarParkControllerTest
    {
        private AirportController airportcontroller;
        private IRootBookingEntityService airportservice;
        private IRootBookingEntityManager airportmanager;
        
        private CarParkController carparkcontroller;
        private IBookingEntityService service;
        private IBookingEntityManager manager;
        private IACPRepository repository; 


        [TestInitialize]
        public void Setup()
        {

            repository = new ACPRepository();
            manager = new BookingEntityManager(repository);
            service = new BookingEntityService(manager);
            carparkcontroller = new CarParkController(service);

            airportmanager = new RootBookingEntityManager(repository);
            airportservice = new RootBookingEntityService(airportmanager);
        }

        [TestMethod]
        public async Task GivenACarPark_WhenIsAdded_BeSurereturnsTrue()
        {
            //Arrange
            bool value;
            var airport = await airportservice.GetAll();

            carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            carparkcontroller.Configuration = Substitute.For<HttpConfiguration>();

            BookingEntityModel model = new BookingEntityModel();
            model.RootBookEntityId = airport.FirstOrDefault().Id;
            model.Comission = 2;
            model.Created = DateTime.Now;
            model.CreatedBy = "System";
            model.Modified = DateTime.Now;
            model.ModifiedBy = "System";
            model.Name = "Local Airport" + DateTime.Now.ToString();
            model.RootBookEntityId = 1;
            model.Address = new AddressModel
            {
                Address1 = "MyAdress",
                Postcode = "SK74QW",
                Country = "UK"
            };
            model.Extras = new Collection<ExtraModel>();
            model.Extras.Add(new ExtraModel
            {
                Created = DateTime.Now,
                CreatedBy = "System",
                Modified = DateTime.Now,
                ModifiedBy = "System",
                Name = "Car Wash",
                Price = 10,
                Description = "Car Wash"

            });
            model.Prices = new Collection<BookingPricingModel>();
            model.Prices.Add(new BookingPricingModel
            {
                Created = DateTime.Now,
                CreatedBy = "System",
                Modified = DateTime.Now,
                Name = "Winter",
                Start = DateTime.Now,
                End = DateTime.Now,
                DayPrices = new Collection<DayPriceModel>{
                    new DayPriceModel
                    {
                        Created = DateTime.Now,
                        Day = 1,
                        Modified = DateTime.Now,
                        Dayprice = 660,
                        CreatedBy = "System",
                        HourPrices = new Collection<HourPriceModel>()
                        {
                            new HourPriceModel
                            {
                                 Created = DateTime.Now,
                                 HourMinute = DateTime.Now,
                                 Hourprice = 666,
                                  Modified= DateTime.Now
                            }
                        }
                    }
                }
            });
            model.Properties = new Collection<PropertyModel>
            {
                new PropertyModel { Key="Code", Value="ACP"},
                new PropertyModel { Key="Price", Value="100"}
            };

            //Act
            var result = await carparkcontroller.Add(model);

            //Assert
            Assert.IsNotNull(result);
           
            Assert.IsTrue(result.TryGetContentValue<bool>(out value));
        }

        [TestMethod]
        public async Task GivenAName_WhenCarParkIsGetIt_BeSurereturnTheCarPark()
        {
            //Arrange
            BookingEntityModel value;
            var carparks = await service.GetAllBookingEntities();

            carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            carparkcontroller.Configuration = Substitute.For<HttpConfiguration>();
            int record = Convert.ToInt32(Math.Ceiling((double)carparks.Count() / 2));
            //Act
            var result = await carparkcontroller.GettByName(carparks[record].Name);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TryGetContentValue<BookingEntityModel>(out value));
        }
    }
}
