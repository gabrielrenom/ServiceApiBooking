using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Managers;
using ACP.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        private BookingController bookingcontroller;
        private IBookingService service;
        private IBookingManager bookingmanager;
        private IACPRepository repository;

        private AvailabilityController availabilitycontroller;
        private IAvailabilityService availabilityservice;
        private IAvailabilityManager availabilitymanager;

        private IBookingPricingService bookingpricingservice;
        private IBookingPricingManager bookingpricingmanager;

        private IBookingEntityManager bookingentitymanager;
        private IBookingEntityService bookingentityservice;

        private IQuoteService quoteservice;

        [TestInitialize]
        public void Setup()
        {

            repository = new ACPRepository();
            bookingmanager = new BookingManager(repository);
            service = new BookingService(bookingmanager, availabilitymanager);
            bookingcontroller = new BookingController(service);

            bookingentitymanager = new BookingEntityManager(repository);
            bookingentityservice = new BookingEntityService(bookingentitymanager);

            bookingpricingmanager = new BookingPricingManager(repository);
            bookingpricingservice = new BookingPricingService(bookingpricingmanager,bookingentitymanager);

            quoteservice = new QuoteService(bookingmanager, bookingpricingmanager);

            availabilitymanager = new AvailabilityManager(repository);
            availabilityservice = new AvailabilityService(availabilitymanager);
            availabilitycontroller = new AvailabilityController(availabilityservice, quoteservice);
        }


        [TestMethod]
        public async Task GivenABooking_WhenIsAdded_BeSurereturnsTrue()
        {
            //Arrange
            //Arrange
            //var all = await slotservice.GetAll();
            var prices = await bookingpricingservice.GetAll();

            //quoteservice.GetQuoteWithPriceByBookingEntityId()
            //quote.Pickup = prices.FirstOrDefault().End.AddDays(-5);//new DateTime(2016, 07, 16, 18, 40, 00);
            //quote.Dropoff = prices.FirstOrDefault().End.AddDays(-8); //new DateTime(2016, 07, 14, 16, 30, 00);

            DateTime startdate = prices.FirstOrDefault().End.AddDays(-8);//Convert.ToDateTime("2015-10-03 00:00:00.000");
            DateTime enddate = prices.FirstOrDefault().End.AddDays(-5); //Convert.ToDateTime("2015-10-07 00:00:00.000");

            //AvailabilityModel availabilitymodel = new AvailabilityModel
            //{
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    Status = new StatusModel { StatusType = ACP.Business.Enums.StatusType.Active }
            //};
            //var findresult = await availabilitycontroller.GettByAvailabilityWithPrice(startdate, enddate, airport);

            CustomerModel customer = new CustomerModel
            {
                Forename = "Mike",
                Surname = "Smith",
                Email = "mikesmith@gmail.com",
                Mobile = "07172727272",
                Telephone = "01626363711",
                Fax = "01626363711",
                Initials = "Dr",
                Title = "Mr",
                Address = new AddressModel
                {
                    Number = 3,
                    Postcode = "W1",
                    Address1 = "10 Downing Street",
                    Address2 = "W1",
                    City = "London",
                    Country = "Uk",
                    County = "Greater London",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CreatedBy = "localuser",
                ModifiedBy = "localuser",
            };

            PaymentModel payment = new PaymentModel
            {
                //Customer = customer,                
                PaymentMethod = ACP.Business.Enums.PaymentMethod.CreditCard,
                CreditCard = new CreditCardModel
                {
                    Lock = false,
                    ExpiryDate = DateTime.Now.AddYears(2),
                    Deleted = false,
                    Name = "Mike Smith",
                    PlainNumber = "6376485484737833",
                    Type = 2,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },
                Currency = new CurrencyModel
                {
                    CountryCode = "GB",
                    Code = "GPR"
                },
                Status = ACP.Business.Enums.StatusType.Processing,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CreatedBy = "localuser",
                ModifiedBy = "localuser",
            };

            BookingModel model = new BookingModel
            {
                AgentReference = "CRT98",
                Cost = 78.90,
                StartDate = startdate,
                EndDate = enddate,
                Status = ACP.Business.Enums.StatusType.Active,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CreatedBy = "localuser",
                ModifiedBy = "localuser",
                Message = "We will arrive late",
                 SourceCode="HZG", 
                //## BookingReference TO BE ADDED BY SERVICE,
                Price = 78.9m,
                Customer = customer,
                TravelDetails = new TravelDetailsModel
                {
                    OutboundDate = startdate,
                    OutboundFlight = "FLG156",
                    OutboundTerminal = "1",
                    ReturnboundTerminal = "3",
                    ReturnDate = enddate,
                    ReturnFlight = "JKLUYU",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },

                Car = new CarModel
                {
                    Colour = "Red",
                    Make = "Audi",
                    Model = "A6",
                    Registration = "CR7",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },
                User = new UserModel
                {
                    FirstName = customer.Forename,
                    LastName = customer.Surname,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },
                //Payments = new Collection<PaymentModel>
                // {
                //     payment
                // }
            };

            //Act
            var result = await service.Add(model);

            //Assert
            Assert.IsNotNull(result);
            //Act
           // var result = await service.Add(model);

            //Assert
            Assert.IsNotNull(result);

           // Assert.IsTrue(result.TryGetContentValue<bool>(out value));
        }

    }
}
