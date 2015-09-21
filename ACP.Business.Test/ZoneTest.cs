using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.DataAccess.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.Business.Services;
using System.Threading.Tasks;
using ACP.Business.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ACP.Business.Test
{
    [TestClass]
    public class ZoneTest
    {
        private IACPRepository repository;
        private ZoneManager usermanager;
        private IZoneService service;
        private string localuser;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            usermanager = new ZoneManager(repository);
            service = new ZoneService(usermanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public async Task WhenGellAll_BeSureAllZonesWithAvailabilitiesAreReturned()
        {            

            //Act
            var result = await service.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GivenAZone_WhenAddIsCalled_BeSureItReturnsTheStuffAdded()
        {
            //Arrange
            var all = await service.GetAll();

            ZoneModel model = new ZoneModel
            {
                IsOccupied = false,
                Number = 12,
                BookingEntityId = all.FirstOrDefault().BookingEntityId,
                Created = DateTime.Now,
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Availability = new Collection<AvailabilityModel>
                    {
                                   new AvailabilityModel
                                   {
                                        Created = DateTime.Now,
                                        CreatedBy = "localuser",
                                        Modified = DateTime.Now,
                                        ModifiedBy = "localuser",
                                        StartDate = new DateTime(2015,10,2),
                                        EndDate = new DateTime(2015,10,8),
                                        StatusId = 1
                                   },
                                   new AvailabilityModel {
                                        Created = DateTime.Now,
                                        CreatedBy = "localuser",
                                        Modified = DateTime.Now,
                                        ModifiedBy = "localuser",
                                        StartDate = new DateTime(2015,10,3),
                                        EndDate = new DateTime(2015,10,7),
                                        StatusId = 2
                                   }
                    }
            };

            //Act
            var result = await service.Add(model);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
