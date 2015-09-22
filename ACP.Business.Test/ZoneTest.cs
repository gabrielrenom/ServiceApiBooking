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
        public async Task WhenGellAllOccupied_BeSureAllZonesWithAvailabilitiesAreReturned()
        {

            //Act
            var result = await service.GetAllOccupied();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GivenANameOrIdentifier_WhenGeByNameAndGetByAllOccupied_BeSureAllZonesWithAvailabilitiesAreReturned()
        {

            //Act
            var result = await service.GetByNumberIdentifier(12);

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task WhenGellAllFree_BeSureAllZonesWithAvailabilitiesAreReturned()
        {
            //Act
            var result = await service.GetAllFree();

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

        [TestMethod]
        public async Task GivenAnId_WhenRemoveIsCalled_BeSureTheRecordIsRemoved()
        {
            //Arrange
            var records = await service.GetAll();

            //Act
            var result = await service.Remove(records.FirstOrDefault().Id);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GivenATheFirstZone_WhenUpdateWhenNewAvailabilityCalled_BeSureItReturns()
        {
            //Arrange
            var all = await service.GetAll();

            ZoneModel model = all.FirstOrDefault();
            model.Number = 15;
            model.Availability.Add(new AvailabilityModel
            {
                Created = DateTime.Now,
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                StartDate = new DateTime(2016, 10, 2),
                EndDate = new DateTime(2016, 10, 8),
                StatusId = 1
            });                        

            //Act
            var result = await service.Update(model);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GivenATheFirstZone_WhenUpdateWhenWeDeleteAnAvailabilityIsCalled_BeSureItReturns()
        {
            //Arrange
            var all = await service.GetAll();

            ZoneModel model = all.FirstOrDefault();
            model.Number = 15;
            if (model.Availability.Count > 1)
            {
                model.Availability.Remove(model.Availability.FirstOrDefault());
            }

            //Act
            var result = await service.Update(model);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GivenAStartDateAndEndDate_WhenFindZoneIsCalled_BeSureItReturnsFreeZones()
        {
            //Arrange
            var all = await service.GetAll();

            DateTime startdate = all.FirstOrDefault().Availability.FirstOrDefault().StartDate;//Convert.ToDateTime("2015-10-03 00:00:00.000");
            DateTime enddate = all.FirstOrDefault().Availability.FirstOrDefault().EndDate; Convert.ToDateTime("2015-10-07 00:00:00.000");

            //Act
            var result = await service.FindZoneAvailable(startdate,enddate);

            //Assert
            Assert.IsTrue(result.Where(x=>x.Id == all.FirstOrDefault().Id).ToList().Count == 0);
        }


    }
}
