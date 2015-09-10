using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.DataAccess.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.Business.Services;
using ACP.Business.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class UserTest
    {
        private IACPRepository repository;
        private UserManager usermanager;        
        private IUserService service;
        private string localuser;

      
        [TestInitialize]
        public void Setup()
        {
            repository = new ACPRepository();
            usermanager = new UserManager(repository);
            service = new UserService(usermanager);
            localuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        [TestMethod]
        public async Task GivenAUser_WhenTheUserIsAdded_BeSureIsConfirmed()
        { 
            //Arrange
            UserModel user = new UserModel();
            user.Address = new AddressModel();
            user.Address.Address1 = "Hazel Grove";
            user.Address.Country = "UK";
            user.Address.County = "Greater Manchester";
            user.Address.Number = 16;
            user.Email = "gabrielrenom@gmail.com";
            user.FirstName = "Gabriel";
            user.LastName = "Renom";
            user.Password = "12345";
            

            //Act
            var results = await service.Add(user);

            //Assert
            Assert.IsNotNull(results);

        }

        [TestMethod]
        public async Task GivenAUserWithCar_WhenTheUserAndCarAreAdded_BeSureBothAreConfirmed()
        {
            //Arrange
            UserModel user = new UserModel();
            user.Address = new AddressModel();
            user.Address.Address1 = "Hazel Grove";
            user.Address.Country = "UK";
            user.Address.County = "Greater Manchester";
            user.Address.Number = 16;
            user.Email = "gabrielrenom@gmail.com";
            user.FirstName = "Gabriel";
            user.LastName = "Renom";
            user.Password = "12345";
            user.Cars = new Collection<CarModel> { 
                new CarModel{
                     Colour="Black",
                 Make="Brown",
                  Model="BMW",
                   Registration="ERTRECV2"                    
                }
            };

            //Act
            var results =await service.Add(user);

            //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task _WhenGetAllIsCalled_GetAllUsersWithAllCarsAndBookings()
        {
           //Act
            var results = await service.GetAll(); 
           //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task AnUserIdIsGiven_WhenIsCalled_GetTheUserWithAllCarsAndBookings()
        { 
            //Arrange
            int Id = 6;

            //Act
            var results = await service.GetById(Id);

            //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task IfAnIdIsGiven_WhenUpdateIsCalled_BeSureAllIsUpdated()
        {
            //Arrange
            int Id = 6;
            var model = await service.GetById(Id);
            model.Email = "Anemail@gmail.com";           

            //Act
            var result = await service.Update(model);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task IfAnIdIsGiven_WhenDeleteIsCalled_BeSureTheUserWithAllCarsIsDeleted()
        {
            //Arrange
            int Id = 6;

            //Act
            var result = await service.DeleteById(Id);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
