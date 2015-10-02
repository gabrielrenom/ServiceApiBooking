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
using ACP.Business.Models;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class AirportControllerTest
    {
        private AirportController airportcontroller;
        private IRootBookingEntityService service;
        private IRootBookingEntityManager manager;
        private IACPRepository repository;


        [TestInitialize]
        public void Setup()
        {

            repository = new ACPRepository();
            manager = new RootBookingEntityManager(repository);
            service = new RootBookingEntityService(manager);
            airportcontroller = new AirportController(service);
            
        }

        [TestMethod]
        public async Task _WhenItsGetitAll_BeSureTheAirportItIsReturned()
        {
            //Arrange
            List<RootBookingEntityModel> value= new List<RootBookingEntityModel>();

            //Act
            airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            airportcontroller.Configuration = Substitute.For<HttpConfiguration>();
            var result = await airportcontroller.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TryGetContentValue<List<RootBookingEntityModel>>(out value));
        }


        [TestMethod]
        public async Task GivenAnAirportId_WhenItsGetit_BeSureTheAirportItIsReturned()
        {
            //Arrange
            RootBookingEntityModel value;
            var airports = await service.GetAll();

            airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            airportcontroller.Configuration = Substitute.For<HttpConfiguration>();
            int record = Convert.ToInt32(Math.Ceiling((double)airports.Count / 2));
            //Act
            var result = await airportcontroller.GetById(airports[record].Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TryGetContentValue<RootBookingEntityModel>(out value));
        }

        [TestMethod]
        public async Task GivenAnAirportName_WhenItsGetit_BeSureTheAirportItIsReturned()
        {
            //Arrange
            RootBookingEntityModel value;
            var airports = await service.GetAll();

            airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            airportcontroller.Configuration = Substitute.For<HttpConfiguration>();
            int record = Convert.ToInt32(Math.Ceiling((double)airports.Count / 2));
            //Act
            var result = await airportcontroller.GetByName(airports[record].Name);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TryGetContentValue<RootBookingEntityModel>(out value));
        }

        [TestMethod]
        public async Task GivenAnAirport_WhenIsAdded_BeSurereturnsTrue()
        {
            //Arrange
            string value;
            
            airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            airportcontroller.Configuration = Substitute.For<HttpConfiguration>();

            RootBookingPropertyViewModel model = new RootBookingPropertyViewModel();
            model.Address = new AddressViewModel
            {
                Address1 = "MyRootAddress",
                Postcode = "SK74QW",
                Country = "UK"
            };            
            model.Telephone = "07777212321";
            model.Name = "Hazel Grove Airport";
            model.BookingEntities= new List<BookingEntityViewModel>
            {
                new BookingEntityViewModel
                {
                   Comission = 2,
                   Name = "Local Hazel Grove Parking" + DateTime.Now.ToString(),
                   Address = new AddressViewModel
                   {
                        Address1 = "MyAdress",
                        Postcode = "SK74QW",
                        Country = "UK"
                   }
                }
            };
            model.Status = new StatusViewModel
            {
                 StatusType = Enums.StatusType.Active
            };

            //Act

            //Assert
        }


    }
}
