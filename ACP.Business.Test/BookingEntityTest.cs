using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.Business.Services;
using ACP.DataAccess.Managers;
using ACP.Business.Models;

namespace ACP.Business.Test
{
    [TestClass]
    public class BookingEntityTest
    {
        private IACPRepository repository;
        private IBookingEntityManager bookingentitymanager;        
        private IBookingEntityService service;
        private string localuser;

        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            bookingentitymanager = new BookingEntityManager(repository);            
            service = new BookingEntityService(bookingentitymanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public void GivenAEntity_WhenIsAdded_BeSureItRestunsTrueAndRemoves()
        {
            //Arrange
            BookingEntityModel model = new BookingEntityModel();
            model.Comission = 2;
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.ModifiedBy = localuser;
            model.Name = "Local Airport" + DateTime.Now.ToString();            
            model.Address = new AddressModel
            {
                Address1 = "MyAdress",
                Postcode = "SK74QW",
                Country = "UK"
            };

            //Act
            var results = service.Add(model);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results);
            

        }

        [TestMethod]
        public void GivenAEntity_WhenIsUpadted_BeSureItReturnsUpdatedData()
        {
            var bookingentities = service.GetAllBookingEntities()[0];
            bookingentities.Price = 999;
            bookingentities.Address.Postcode = "SK74QW";

            //Act
            var results = service.Update(bookingentities);

            //Assert
            Assert.IsTrue(results);          
        }

        [TestMethod]
        public void GivenAEntity_WhenIsDeleted_BeSureItIsNotThere()
        {
            var bookingentities = service.GetAllBookingEntities()[0];          

            //Act
            var results = service.Remove(bookingentities.Id);

            //Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void GivenAEntity_WhenIsCalled_BeSureAllEntitiesAreReturned()
        { 
            //Arrange

            //Act
            var results = service.GetAllBookingEntities();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results[0].Id > 0);
            Assert.IsNotNull(results[0].Address);
            Assert.IsTrue(results[0].Address.Id > 0);
            
        }

        [TestMethod]
        public void GivenABookingEntityId_WhenIsCalled_BeSureEntityIsReturned()
        {
            //Arrange
            var bookingentities = service.GetAllBookingEntities();
            int first = bookingentities[0].Id;

            //Act
            var results = service.GetBookingEntityById(first);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Id > 0);
            Assert.IsNotNull(results.Address);
            Assert.IsTrue(results.Address.Id > 0);

        }      

    }
}
