using ACP.Business.Enums;
using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Managers;
using ACP.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceAPI.Controllers;
using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class AvailabilityControllerTest
    {
        private AvailabilityController availabilitycontroller;
        private IAvailabilityService service;
        private IQuoteService quoteservice;
        private IAvailabilityManager manager;
        private IACPRepository repository;
        private IBookingManager bookingManager;
        private IBookingPricingManager bookingPricingManager;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            manager = new AvailabilityManager(repository);
            service = new AvailabilityService(manager);
            bookingPricingManager = new BookingPricingManager(repository);
            bookingManager = new BookingManager(repository);
            quoteservice = new QuoteService(bookingManager, bookingPricingManager);
            availabilitycontroller = new AvailabilityController(service, quoteservice);
        }

        [TestMethod]
        public async Task GuivenADayInOut_WhenGetIsCalled_BeSureTheAvailabilityIsreturned()
        {
            //Arrange
            bool value;

            AvailabilityModel model = new AvailabilityModel
            {
                StartDate = new DateTime(2015, 10, 4),
                EndDate = new DateTime(2015, 10, 3),
                Status = AvailabilityStatus.Free
                  
            };

            availabilitycontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            availabilitycontroller.Configuration = Substitute.For<HttpConfiguration>();

            //Act
            var result = await availabilitycontroller.GettByAvailability(model);

            //Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.TryGetContentValue<bool>(out value));

        }

        [TestMethod]
        public async Task GuivenADayInOut_WhenGettByAvailabilityWithPriceCalled_BeSureTheAvailabilityIsreturned()
        {
            //Arrange
            List<AvailabilityViewModel> value = new List<AvailabilityViewModel>();

            AvailabilityViewModel model = new AvailabilityViewModel
            {
                StartDate = new DateTime(2015, 10, 2),
                EndDate = new DateTime(2015, 10, 8),
                StatusType = 2,                

            };

            availabilitycontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            availabilitycontroller.Configuration = Substitute.For<HttpConfiguration>();

            //Act
            var result = await availabilitycontroller.GettByAvailabilityWithPrice(model);

            //Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.TryGetContentValue(out value));

        }
    }
}
