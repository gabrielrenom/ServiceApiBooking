using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.APIs.PP;
using System.Threading.Tasks;
using ACP.Business.Services.Interfaces;
using ACP.Business.Repository;
using ACP.DataAccess.Repository;
using ACP.Business.Managers;
using ACP.DataAccess.Managers;
using ACP.Business.Services;
using System.Linq;

namespace ACP.Business.Test
{
    [TestClass]
    public class PurpleParkingAPITest
    {
        private IACPRepository repository;
        private IPurpleParking purpleparkingapi;
        private IRootBookingEntityService airportService;
        private IStatusService statusService;
        private IRootBookingEntityManager rootbookingentitymanager;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();         

            rootbookingentitymanager = new RootBookingEntityManager(repository);
            airportService = new RootBookingEntityService(rootbookingentitymanager);
            statusService = new StatusService(statusService);

            purpleparkingapi = new PurpleParking(airportService, statusService);
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

        [TestMethod]
        public async Task _WhenFillAirportsIsCalled_BeSureItFillsTheDatabaseWithAirportsAndCarparks()
        {
            var result = await purpleparkingapi.FillAirports();

            var allairports  = airportService.GetAll();
            var provider = allairports.Select(z=>z.Properties.Where(y => y.Key == "Provider" && y.Value == "PP").ToList());

            Assert.IsTrue(result);
            Assert.IsTrue(provider.Count()>0);
        }
    }
}
