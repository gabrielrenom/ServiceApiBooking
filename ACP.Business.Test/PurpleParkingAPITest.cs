using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.APIs.PP;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class PurpleParkingAPITest
    {
        [TestMethod]
        public async Task _WhenGetAirportsIsCalled_BeSureItReturnsAllAirports()
        {
            PurpleParking pp = new PurpleParking();

            var result = await pp.GetAirports();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task _WhenGetAirportCarparksIsCalled_BeSureItReturnsAllCarparks()
        {
            PurpleParking pp = new PurpleParking();

            var result = await pp.GetCarParks();

            Assert.IsNotNull(result);
        }

    }
}
