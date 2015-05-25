using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.DataAccess.Repository;
using ACP.Business.Services.Interfaces;
using ACP.Business.Services;
using ACP.DataAccess.Managers;
using ACP.Business.Models;

namespace ACP.Business.Test
{
    [TestClass]
    public class ServiceTests
    {
        IACPRepository repository;
        IBookingServiceManager servicemanager;
        IBookingServiceService service;

        [TestInitialize]
        public void Start()
        {
            repository = new ACPRepository();
            servicemanager = new BookingServiceManager(repository);
            service = new BookingServiceService(servicemanager);
        }

        [TestMethod]
        public void GivenAServiceBookingModel_WhenTheModelIsAdded_ThenEnsureTheCorrectResultIsReturned()
        {
            // Arrange            
            BookingServiceModel model = new BookingServiceModel();
            model.Name = "Car Park";
            model.Id = 1;
            model.CreatedBy = "Gabriel";
            var now = DateTime.Now;
            model.Created = now;

            // Act
            var result = service.Add(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual("Car Park", result.Name);
            //Assert.AreEqual(now, result.Created);
            //Assert.AreEqual("Gabriel", result.CreatedBy);
        }

        [TestMethod]
        public void GivenAServiceBookingName_WhenGetIsCalled_ThenEnsureTheCorrectResultIsReturned()
        {
            // Arrange            
            BookingServiceModel model = new BookingServiceModel();
            model.Name = "Car Park";        

            // Act
            var result =  service.GetServiceByName(model.Name);

            // Assert
            Assert.IsNotNull(result);            
            Assert.AreEqual("Car Park", result.Name);
            //Assert.AreEqual(now, result.Created);
            //Assert.AreEqual("Gabriel", result.CreatedBy);
        }

        [TestMethod]
        public void GivenAServiceBookingName_WhenUpdateIsCalled_ThenEnsureTheCorrectResultIsReturned()
        {
            // Arrange            
            BookingServiceModel model = new BookingServiceModel();
            model.Name = "Car Park 3";
            model.Id = 1005;

            // Act
            var result = service.UpdateService(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);            
        }

        [TestMethod]
        public void GivenATheFirstServiceBookingName_WhenDeleteIsCalled_ThenEnsureItemIsRemoved()
        {
            // Arrange            
            BookingServiceModel model = new BookingServiceModel();

            // Act
            var item = service.GetAll().FirstOrDefault();
            var result = service.RemoveService(item.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }
    }
}
