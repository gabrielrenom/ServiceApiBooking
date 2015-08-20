using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.APIs.PP;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class PurpleParkingAPITest
    {
        IPurpleParking purpleparkingapi;

        [TestInitialize]
        public void Setup()
        {
            purpleparkingapi = new PurpleParking();
        }

        [TestMethod]
        public async Task _WhenGetAirportsIsCalled_BeSureItReturnsAllAirports()
        { 
            var result = await purpleparkingapi.GetAirports();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task _WhenGetAirportCarparksIsCalled_BeSureItReturnsAllCarparks()
        {
            var result = await purpleparkingapi.GetCarParks();

            Assert.IsNotNull(result);
        }

    }
}
