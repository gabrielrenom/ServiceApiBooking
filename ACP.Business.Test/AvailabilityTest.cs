using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.DataAccess.Managers;
using ACP.Business.Services;
using System.Linq;
using ACP.Business.Models;
using System.Threading.Tasks;
using ACP.Business.Enums;

namespace ACP.Business.Test
{
    [TestClass]
    public class AvailabilityTest
    {
        private IACPRepository repository;
        private IAvailabilityManager availabilitymanager;
        private IAvailabilityService service;
        private string localuser;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            availabilitymanager = new AvailabilityManager(repository);
            service = new AvailabilityService(availabilitymanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public void GivenASlot_WhenIsAdded_BeSureTheSlotIsAdded()
        {
            //Arrange
            AvailabilityModel model = new AvailabilityModel();          
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.ModifiedBy = localuser;
            model.SlotId = 1;
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Status = Enums.AvailabilityStatus.Free;
            

            //Act
            var result = service.Add(model);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GivenAnId_WhenIsGetById_BeSureTheSlotIsReturned()
        {
            //Arrange
            int id = 3;

            //Act
            var result = await service.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SlotId > 0);
            Assert.IsTrue(result.Status > 0);
        }

        [TestMethod]
        public async Task _WhenGetAllIsCalled_BeSureAllTheSlotsAreReturned()
        {
            //Act
            var result = await  service.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.FirstOrDefault().SlotId > 0);
            Assert.IsTrue(result.FirstOrDefault().Status > 0);
        }

        [TestMethod]
        public async Task GivenASlot_WhenIsUpdates_BeSureTheSlotIsUpdated()
        {
            //Arrange
            AvailabilityModel model = await service.GetById(2);
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Status = AvailabilityStatus.Free;
            
            //Act
            var result =await service.Update(model);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
