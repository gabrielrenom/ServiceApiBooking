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

        private ISlotManager slotmanager;
        private ISlotService slotservice;

        [TestInitialize]
        public void Setup()
        {

            repository = new ACPRepository();
            bookingmanager = new BookingManager(repository);

            slotmanager = new SlotManager(repository);
            slotservice = new SlotService(slotmanager);

            availabilitymanager = new AvailabilityManager(repository);
            availabilityservice = new AvailabilityService(availabilitymanager);
            availabilitycontroller = new AvailabilityController(availabilityservice, quoteservice);
            bookingpricingmanager = new BookingPricingManager(repository);
            service = new BookingService(bookingmanager, availabilitymanager,slotmanager, bookingpricingmanager);
            bookingcontroller = new BookingController(service);

            bookingentitymanager = new BookingEntityManager(repository);
            bookingentityservice = new BookingEntityService(bookingentitymanager);


            bookingpricingservice = new BookingPricingService(bookingpricingmanager,bookingentitymanager);

            quoteservice = new QuoteService(bookingmanager, bookingpricingmanager);

           
        }


        [TestMethod]
        public async Task GivenABooking_WhenIsAdded_BeSurereturnsTrue()
        {
            //Arrange
            var prices = await bookingpricingservice.GetAll();
            //## To set the start/end date
            //##    1- Go to BookingPricing table
            //##    2- Then get a slot between Start and End
            //##    3- Be sure the booking entityid matches with the one in the model
            DateTime startdate = Convert.ToDateTime("2016-06-03 00:00:00.000");//prices.FirstOrDefault().End.AddDays(-12);//Convert.ToDateTime("2015-10-03 00:00:00.000");
            DateTime enddate = Convert.ToDateTime("2016-06-07 00:00:00.000");//prices.FirstOrDefault().End.AddDays(-7); //Convert.ToDateTime("2015-10-07 00:00:00.000");

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
                    Type = ACP.Business.Enums.CreditCardTypes.Visa,
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
                 SourceCode= "LGW", 
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
            };

            //Act
            var result = await service.Add(model);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GivenABooking_WhenIsAddedPassingTheBookingEntityCode_BeSurereturnsTrue()
        {
            //Arrange
            var prices = await bookingpricingservice.GetAll();
            DateTime startdate = prices.FirstOrDefault().End.AddDays(-12);//Convert.ToDateTime("2015-10-03 00:00:00.000");
            DateTime enddate = prices.FirstOrDefault().End.AddDays(-7); //Convert.ToDateTime("2015-10-07 00:00:00.000");

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
                    Type = ACP.Business.Enums.CreditCardTypes.AmericanExpress,
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
                SourceCode = "HZG2",
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
            };

            //Act
            var result = await service.Add(model,true);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
