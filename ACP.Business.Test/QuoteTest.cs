﻿using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Managers;
using ACP.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class QuoteTest
    {
        private IACPRepository repository;
        private IBookingManager bookingmanager;
        private IBookingPricingManager bookingpricingmanager;
        private IQuoteService service;
        private SlotManager slotmanager;
        private ISlotService slotservice;
        private IBookingService bookingservice;
        private IAvailabilityManager availabilitymanager;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            bookingmanager = new BookingManager(repository);
            bookingpricingmanager = new BookingPricingManager(repository);
            availabilitymanager = new AvailabilityManager(repository);
            service = new QuoteService(bookingmanager, bookingpricingmanager);
            slotmanager = new SlotManager(repository);
            slotservice = new SlotService(slotmanager);
            bookingservice = new BookingService(bookingmanager, availabilitymanager, slotmanager, bookingpricingmanager);
        }

        [TestMethod]
        public async Task GivenADateAndPlace_WhenUserGetQuote_ThenEnsureAllResultsAreReturned()
        {
            //Arrange            
            //QuoteModel quote = new QuoteModel();
            //quote.Pickup = new DateTime(2016, 12, 22, 18, 40, 00);
            //quote.Dropoff = new DateTime(2016, 12, 28, 16, 30, 00);
            //quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            //quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            //quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            QuoteModel quote = new QuoteModel();
            quote.Pickup = new DateTime(2016, 12, 23, 16, 30, 00);
            quote.Dropoff = new DateTime(2016, 12, 28, 18, 40, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            //$datediff = $edate - $sdate;
            //$days = (floor($datediff/(60*60*24))+1);
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='".$row1['id']."' AND (`sdate` <= '".$sdate."' AND `edate` >= '".$sdate."') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='8' AND (`sdate` <= '1431692800' AND `edate` >= '1441692800') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_content` WHERE `cpid` = '8'
            //SELECT * FROM `carparks_availability` WHERE `cpid`='1'")

            //Act
            var result = await service.GetQuote(quote);


            //Assert
            Assert.IsNotNull(result);
            // Assert.AreEqual(result.BookingPricingItems.FirstOrDefault().Name, "Summer");

        }

        [TestMethod]
        public async Task GivenADateAndPlace_WhenUserGetQuoteWithPriceAnNotId_ThenEnsureAllResultsAreReturned()
        {
            //Arrange            
            //QuoteModel quote = new QuoteModel();
            //quote.Pickup = new DateTime(2016, 12, 22, 18, 40, 00);
            //quote.Dropoff = new DateTime(2016, 12, 28, 16, 30, 00);
            //quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            //quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            //quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            QuoteModel quote = new QuoteModel();
            quote.Pickup = new DateTime(2016, 12, 23, 16, 30, 00);
            quote.Dropoff = new DateTime(2016, 12, 28, 18, 40, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            //$datediff = $edate - $sdate;
            //$days = (floor($datediff/(60*60*24))+1);
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='".$row1['id']."' AND (`sdate` <= '".$sdate."' AND `edate` >= '".$sdate."') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_pricing` WHERE `cpid`='8' AND (`sdate` <= '1431692800' AND `edate` >= '1441692800') AND `price`<>'' LIMIT 1"
            //SELECT * FROM `carparks_content` WHERE `cpid` = '8'
            //SELECT * FROM `carparks_availability` WHERE `cpid`='1'")

            //Act
            var result = await service.GetQuoteWithPrice(quote);


            //Assert
            Assert.IsNotNull(result);
            // Assert.AreEqual(result.BookingPricingItems.FirstOrDefault().Name, "Summer");

        }


        [TestMethod]
        public async Task GivenADateAndPlace_WhenUserGetQuoteWithPrice_ThenEnsureAllResultsAreReturned()
        {
            //Arrange            
            QuoteModel quote = new QuoteModel();
            quote.Pickup = new DateTime(2016, 12, 23, 16, 30, 00);
            quote.Dropoff = new DateTime(2016, 12, 28, 18, 40, 00); 
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });
            
            //Act
            var result = await service.GetQuoteWithPriceByBookingEntityId(1,quote);


            //Assert
            Assert.IsNotNull(result);         

        }


        [TestMethod]
        public async Task GivenAQuote_WhenUserDoesABooking_BeSureTheBookingIsDone()
        {
            //Arrange
            var all = await slotservice.GetAll();

            DateTime startdate = DateTime.Now;//Convert.ToDateTime("2015-10-03 00:00:00.000");
            DateTime enddate = DateTime.Now; //Convert.ToDateTime("2015-10-07 00:00:00.000");
            string airport = "LGW";
            var findresult = await slotservice.FindSlotAvailable(startdate, enddate, airport);

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
                PaymentMethod = Enums.PaymentMethod.CreditCard,
                CreditCard = new CreditCardModel
                {
                    Lock = false,
                    ExpiryDate = DateTime.Now.AddYears(2),
                    Deleted = false,
                    Name = "Mike Smith",
                    PlainNumber = "6376485484737833",
                    Type = Business.Enums.CreditCardTypes.Mastercard,
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
                Status = Enums.StatusType.Processing,
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
                Status = Enums.StatusType.Processing,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CreatedBy = "localuser",
                ModifiedBy = "localuser",
                Message = "We will arrive late",
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
                Payments = new Collection<PaymentModel>
                 {
                     payment
                 }
            };

            //Act
            var result = await bookingservice.Add(model);

            //Assert
            Assert.IsNotNull(result);
        }        
    }
}
