using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.DataAccess.Managers;
using ACP.Business.Services;
using ACP.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    /// <summary>
    /// RootBookingEntityTest tests an airport with bookingentities.
    /// Every booking entity is a carpark.
    /// </summary>
    [TestClass]
    public class RootBookingEntityTest
    {
        private IACPRepository repository;
        private IRootBookingEntityManager rootbookingentitymanager;
        private IRootBookingEntityService service;
        private string localuser;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            rootbookingentitymanager = new RootBookingEntityManager(repository);
            service = new RootBookingEntityService(rootbookingentitymanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public void GivenARootBookingEntity_WhenIsAdded_BeSureItRestunsTrue()
        { 
            //Arrange
            RootBookingEntityModel model = new RootBookingEntityModel();
            model.Address= new AddressModel
            {
                Address1 = "MyRootAddress",
                Postcode = "SK74QW",
                Country = "UK"
            };
            model.CreatedBy = localuser;
            model.Telephone = "07777212321";
            model.Name = "Hazel Grove Airport";
            model.BookingEntities = new List<BookingEntityModel>
            {
                new BookingEntityModel
                {
                   Comission = 2,
                   Created = DateTime.Now,
                   CreatedBy = localuser,
                   Modified = DateTime.Now,
                   ModifiedBy = localuser,
                   Name = "Local Hazel Grove Parking" + DateTime.Now.ToString(),
                   Address = new AddressModel
                   {
                        Address1 = "MyAdress",
                        Postcode = "SK74QW",
                        Country = "UK"
                   }
                }              
            };
            model.Status= new StatusModel
            {
                Name= "Active"
            };
            
            //Act
            var result = service.Add(model);
            
            //Assert
            Assert.IsTrue(result.Id>0);
        }
    
        [TestMethod]
        public async void GetEntity_WhenIdIsPassed_BeSureItRestunsTheEntity()
        {
            //Arrange
            
            //Act
            var result = await service.GetById(7);

            //Assert
            Assert.IsTrue(result.Id > 0);
            Assert.IsTrue(result.AddressId > 0);
            Assert.IsTrue(result.StatusId > 0);
            Assert.IsTrue(result.BookingEntities.Count > 0);
        }

        [TestMethod]
        public async Task GetEntities_WhenGetAllIsCalled_BeSureReturnAllTheEntities()
        { 
            //Arrange

            //Act
            var result = await service.GetAll();

            //Assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async void WhenEntityIsPassed_WhenUpdateIsCalled_BeSureEntityIsUpdated()
        { 
            //Arrange
            var model = await service.GetAll();
            model[0].Name = "National";
            model[0].Address.Country = "Biolorrusia";
            model[0].Status.Name = "Worried";
            foreach (var item in model[0].BookingEntities)
            {
                item.Name = "Barca";
                item.Address.Address1 = "Spain";
            }

            //Act
            var result = await service.Update(model[0]);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async void WhenIdIsPassed_WhenDeleteIsCalled_BeSureEntityIsDelete()
        {
            //Arrange
            var Id = await service.GetAll();
                        
            //Act
            var result = await service.Remove(Id[0].Id);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
