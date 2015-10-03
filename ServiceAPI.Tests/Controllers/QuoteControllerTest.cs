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
    public class QuoteControllerTest
    {
        
        private IAvailabilityService service;
        private IQuoteService quoteservice;
        private IAvailabilityManager manager;
        private IACPRepository repository;
        private IBookingManager bookingManager;
        private IBookingPricingManager bookingPricingManager;
        private QuoteController quotecontroller;
        private IBookingPricingService pricingservice;
        private IBookingEntityManager bookingentitymanager;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            bookingentitymanager = new BookingEntityManager(repository);
            manager = new AvailabilityManager(repository);
            service = new AvailabilityService(manager);
            bookingPricingManager = new BookingPricingManager(repository);
            bookingManager = new BookingManager(repository);
            quoteservice = new QuoteService(bookingManager, bookingPricingManager);
            quotecontroller = new QuoteController(quoteservice);
            pricingservice = new BookingPricingService(bookingPricingManager, bookingentitymanager);

        }

        [TestMethod]
        public async Task GuivenADayInOut_WhenGetQuoteIsCalled_BeSureTheQuoteIsreturned()
        {
            //Arrange

            var prices = await pricingservice.GetAll();

            QuoteModel value = new QuoteModel();

            QuoteModel quote = new QuoteModel();
            quote.Pickup = prices.FirstOrDefault().End.AddDays(-5);//new DateTime(2016, 07, 16, 18, 40, 00);
            quote.Dropoff = prices.FirstOrDefault().End.AddDays(-8); //new DateTime(2016, 07, 14, 16, 30, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            quotecontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            quotecontroller.Configuration = Substitute.For<HttpConfiguration>();

            //Act
            var result = await quotecontroller.GetQuote(quote);

            //Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.TryGetContentValue(out value));
        }

        [TestMethod]
        public async Task GuivenADayInOut_WhenGetQuoteWithPricesIsCalled_BeSureTheQuoteIsreturned()
        {
            //Arrange

            var prices = await pricingservice.GetAll();

            QuoteModel value = new QuoteModel();

            QuoteModel quote = new QuoteModel();
            quote.Pickup = prices.FirstOrDefault().End.AddDays(-5);//new DateTime(2016, 07, 16, 18, 40, 00);
            quote.Dropoff = prices.FirstOrDefault().End.AddDays(-8); //new DateTime(2016, 07, 14, 16, 30, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            quotecontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            quotecontroller.Configuration = Substitute.For<HttpConfiguration>();

            //Act
            var result = await quotecontroller.GetQuoteWithPrice(quote);

            //Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.TryGetContentValue(out value));
        }

        [TestMethod]
        public async Task GuivenADayInOut_WhenGetQuoteWithPricesAndBookingIdIsCalled_BeSureTheQuoteIsreturned()
        {
            //Arrange

            var prices = await pricingservice.GetAll();

            QuoteModel value = new QuoteModel();

            QuoteModel quote = new QuoteModel();
            quote.Pickup = prices.FirstOrDefault().End.AddDays(-5);//new DateTime(2016, 07, 16, 18, 40, 00);
            quote.Dropoff = prices.FirstOrDefault().End.AddDays(-8); //new DateTime(2016, 07, 14, 16, 30, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Manchester" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            quotecontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            quotecontroller.Configuration = Substitute.For<HttpConfiguration>();

            //Act
            var result = await quotecontroller.GetQuoteWithPriceByCarcarkId(1,quote);

            //Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.TryGetContentValue(out value));
        }
    }
}
