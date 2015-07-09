using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.APIs.APH;
using ACP.Business.APIs.APH.Models;
using ACP.DataAccess.Managers;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.DataAccess.Repository;

namespace ACP.Business.Test
{
    [TestClass]
    public class APHTest
    {
        IAPH aph;
        private IACPRepository repository;
        private IRootBookingEntityManager rootbookingentitymanager;
        private IBookingEntityManager bookingentitymanager;
        
        [TestInitialize]
        public void Setup()
        {
           repository = new ACPRepository();
           rootbookingentitymanager = new RootBookingEntityManager(repository);
           bookingentitymanager = new BookingEntityManager(repository);

           aph = new APH(rootbookingentitymanager, bookingentitymanager);
        }

        [TestMethod]
        public void GivenAnAvailabilitySlot_WhenCarParkAvailabilityIsCalled_BeSureItReturnsAResponse()
        {
            //Arrange
            API_Request request = new API_Request
            {
                Agent = new Agent
                {
                    ABTANumber = "WA789",
                    Initials = "thecarparksuper",
                    Password = ""
                },
                Itinerary = new Itinerary {
                    ArrivalDate = "20Nov15",
                    DepartDate = "27Nov15",
                    ArrivalTime = "0600",
                    DepartTime ="1800",
                    Location = "LGW",
                    Terminals = "ALL"
                },
                System="APH",
                Version="1.0",
                Product="CarPark",
                Customer="X",                
                Session="000000003",
                RequestCode="11"
            };

            //Act
            var response = aph.CarParkAvailability(request);
            
            //Assert
            Assert.IsTrue(response.Result == "OK");
        }

        [TestMethod]
        public void GivenASlotAndCarParkCode_WhenCarParkPriceIsCalled_BeSureItReturnsAResponseWithPrice()
        {
            //Arrange
            API_Request request = new API_Request
            {
                Agent = new Agent
                {
                    ABTANumber = "WA789",
                    Initials = "thecarparksuper",
                    Password = ""
                },
                Itinerary = new Itinerary
                {
                    ArrivalDate = "20Nov15",
                    DepartDate = "27Nov15",
                    ArrivalTime = "0600",
                    DepartTime = "1800",
                    CarParkCode = "LGW1",
                    NumberOfPax = "2"
                },
                System = "APH",
                Version = "1.0",
                Product = "CarPark",
                Customer = "X",
                Session = "000000004",
                RequestCode = "3"
            };

            //Act
            var response = aph.CarPrice(request);

            //Assert
            Assert.IsTrue(response.Result == "OK");
        }

        [TestMethod]
        public void GivenACarParkCode_WhenCarParkInformationIsCalled_BeSureItReturnsAResponseInformation()
        {
            //Arrange
            API_Request request = new API_Request
            {
                Agent = new Agent
                {
                    ABTANumber = "WA789",
                    Initials = "thecarparksuper",
                    Password = ""
                },
                Itinerary = new Itinerary
                {
                    ArrivalDate = "20Nov15",
                    CarParkCode = "LGW1",
                },
                System = "APH",
                Version = "1.0",
                Product = "CarPark",
                Customer = "X",
                Session = "000000004",
                RequestCode = "6",
                Request = new Request {  RequestType="1"}
            };

            //Act
            var response = aph.CarParkInformation(request);

            //Assert
            Assert.IsTrue(response.Result == "OK");
        }

        [TestMethod]
        public void GivenAllAirports_WhenFillBookingEnties_BeSureBookingEntitiesIsFilledWithAll()
        { 
            //Act
            var result = aph.FillBookingEnties();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
