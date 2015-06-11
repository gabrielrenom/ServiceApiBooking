using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceAPI.Controllers;
using ACP.Business.Services.Interfaces;
using ACP.Business.Services;
using ACP.Business.Managers;
using ACP.DataAccess.Managers;
using ACP.Business.Repository;
using ACP.DataAccess.Repository;
using System.Collections.Generic;
using ServiceAPI.Models;
using ACP.Data;
using ACP.Business.Models;
using System.Threading.Tasks;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class ServiceControllerTest
    {
        IACPRepository repository;
        IBookingServiceManager servicemanager;
        IBookingServiceService service;


        [TestInitialize]
        public void Startup()
        {
            IACPRepository repository = new ACPRepository();
            IBookingServiceManager servicemanager = new BookingServiceManager(repository);
            IBookingServiceService service = new BookingServiceService(servicemanager);

        }

        //[TestMethod]
        //public void Get()
        //{
        //    IACPRepository repository = new ACPRepository();
        //    IServiceManager servicemanager = new ServiceManager(repository);
        //    IServiceService service = new ServiceService(servicemanager);
        //    // Arrange
        //    ServiceController controller = new ServiceController(service); 

        //    // Act
        //    IEnumerable<string> result = controller.Get();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    //Assert.AreEqual(2, result.Count());
        //    //Assert.AreEqual("value1", result.ElementAt(0));
        //    //Assert.AreEqual("value2", result.ElementAt(1));
        //}

        //[TestMethod]
        //public void GetById()
        //{
        //    IACPRepository repository = new ACPRepository();
        //    IServiceManager servicemanager = new ServiceManager(repository);
        //    IServiceService service = new ServiceService(servicemanager);
            
        //    // Arrange
        //    ServiceController controller = new ServiceController(service);

        //    // Act
        //    string result = controller.Get(5);

        //    // Assert
        //    Assert.AreEqual("value", result);
        //}

        [TestMethod]
        public void Post()
        {
            IACPRepository repository = new ACPRepository();
            IBookingServiceManager servicemanager = new BookingServiceManager(repository);
            IBookingServiceService service = new BookingServiceService(servicemanager);

            BookingServiceViewModel bookingservicemodel = new BookingServiceViewModel();
            bookingservicemodel.Id = 1;
            bookingservicemodel.Name = "Car Park";        
            
            //Arrange
            ServiceController controller = new ServiceController(service);

            //var result = await Task.Run(async () =>
            //{
            //    controller.Post(bookingservicemodel);
            //    // Actual test code here.
            //}).GetAwaiter().GetResult();

            // Act
            //var result =await Task.Run(controller.Post(bookingservicemodel));

            // Assert
        }

        //[TestMethod]
        //public void Put()
        //{
        //    IACPRepository repository = new ACPRepository();
        //    IServiceManager servicemanager = new ServiceManager(repository);
        //    IServiceService service = new ServiceService(servicemanager);

        //    // Arrange
        //    ServiceController controller = new ServiceController(service);

        //    // Act
        //    controller.Put(5, "value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    IACPRepository repository = new ACPRepository();
        //    IServiceManager servicemanager = new ServiceManager(repository);
        //    IServiceService service = new ServiceService(servicemanager);

        //    // Arrange
        //    ServiceController controller = new ServiceController(service);

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
