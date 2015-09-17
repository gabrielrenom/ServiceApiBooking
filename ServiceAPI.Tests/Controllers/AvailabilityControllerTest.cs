using ACP.Business.Managers;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Managers;
using ACP.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Tests.Controllers
{
    [TestClass]
    public class AvailabilityControllerTest
    {
        private AvailabilityController availabilitycontroller;
        private IAvailabilityService service;
        private IAvailabilityManager manager;
        private IACPRepository repository;

        [TestMethod]
        public void Setup()
        {
            repository = new ACPRepository();
            manager = new AvailabilityManager(repository);
            service = new AvailabilityService(manager);
            availabilitycontroller = new AvailabilityController(service);
        }

        //[TestMethod]
        //public GuivenADayInOut_WhenGetIsCalled_BeSureTheAvailabilityIsreturned()
        //{
        //    //Arrange
        //    bool value;

        //    availabilitycontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
        //    availabilitycontroller.Configuration = Substitute.For<HttpConfiguration>();
            
        //    //Act
        //    var result = await availabilitycontroller.g(model);

        //    //Assert
        //    Assert.IsNotNull(result);

        //    Assert.IsTrue(result.TryGetContentValue<bool>(out value));

        //}
    }
}
