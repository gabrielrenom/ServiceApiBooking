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
            model.BookingEntityId = 12;
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;

            model.Status = new StatusModel
            {
                Created = DateTime.Now,
                CreatedBy = localuser,
                Modified = DateTime.Now,
                ModifiedBy = localuser,
                Name = "Active"                
            };


            //Act
            var result = service.Add(model);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenAnId_WhenIsGetById_BeSureTheSlotIsReturned()
        {
            //Arrange
            int id = 3;

            //Act
            var result = service.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.BookingEntityId > 0);
            Assert.IsTrue(result.StatusId > 0);
        }

        [TestMethod]
        public void _WhenGetAllIsCalled_BeSureAllTheSlotsAreReturned()
        {
            //Act
            var result = service.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.FirstOrDefault().BookingEntityId > 0);
            Assert.IsTrue(result.FirstOrDefault().StatusId > 0);
        }

        [TestMethod]
        public void GivenASlot_WhenIsUpdates_BeSureTheSlotIsUpdated()
        {
            //Arrange
            AvailabilityModel model = service.GetById(2);
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.StatusId = 7;
            
            //Act
            var result = service.Update(model);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
