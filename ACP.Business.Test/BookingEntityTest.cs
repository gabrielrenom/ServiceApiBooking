﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using ACP.DataAccess.Repository;
using ACP.Business.Services;
using ACP.DataAccess.Managers;
using ACP.Business.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

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
            model.RootBookEntityId = 1;
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
        public void GivenAEntityWithExtras_WhenIsAdded_BeSureItRestunsTrueAndRemoves()
        {
            //Arrange
            BookingEntityModel model = new BookingEntityModel();
            model.Comission = 2;
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.ModifiedBy = localuser;
            model.Name = "Local Airport" + DateTime.Now.ToString();
            model.RootBookEntityId = 1;
            model.Address = new AddressModel
            {
                Address1 = "MyAdress",
                Postcode = "SK74QW",
                Country = "UK"
            };
            model.Extras = new Collection<ExtraModel>();
            model.Extras.Add(new ExtraModel { 
                Created = DateTime.Now,
                CreatedBy = localuser,
                Modified = DateTime.Now,
                ModifiedBy = localuser,
                 Name = "Car Wash", 
                 Price=10,
                  Description="Car Wash"
 
            });

            //Act
            var results = service.Add(model);

            //Assert
            Assert.IsNotNull(results);           
            Assert.IsTrue(results);


        }

        [TestMethod]
        public void GivenAEntityWithExtrasAndPrices_WhenIsAdded_BeSureItRestunsTrueAndRemoves()
        {
            //Arrange
            BookingEntityModel model = new BookingEntityModel();
            model.Comission = 2;
            model.Created = DateTime.Now;
            model.CreatedBy = localuser;
            model.Modified = DateTime.Now;
            model.ModifiedBy = localuser;
            model.Name = "Local Airport" + DateTime.Now.ToString();
            model.RootBookEntityId = 1;
            model.Address = new AddressModel
            {
                Address1 = "MyAdress",
                Postcode = "SK74QW",
                Country = "UK"
            };
            model.Extras = new Collection<ExtraModel>();
            model.Extras.Add(new ExtraModel
            {
                Created = DateTime.Now,
                CreatedBy = localuser,
                Modified = DateTime.Now,
                ModifiedBy = localuser,
                Name = "Car Wash",
                Price = 10,
                Description = "Car Wash"

            });
            model.Prices = new Collection<BookingPricingModel>();
            model.Prices.Add(new BookingPricingModel
            {
                Created = DateTime.Now,
                CreatedBy = localuser,
                Modified = DateTime.Now,
                Name = "Winter",
                Start = DateTime.Now,
                End = DateTime.Now,
                DayPrices = new Collection<DayPriceModel>{
                    new DayPriceModel
                    {
                        Created = DateTime.Now,
                        Day = 1,
                        Modified = DateTime.Now,
                        Dayprice = 660,
                        CreatedBy = localuser,
                        HourPrices = new Collection<HourPriceModel>() 
                        { 
                            new HourPriceModel
                            {
                                 Created = DateTime.Now,                         
                                 HourMinute = DateTime.Now,
                                 Hourprice = 666,
                                  Modified= DateTime.Now
                            }
                        }
                    }                    
                }
            });
        

            //Act
            var results = service.Add(model);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results);


        }

        [TestMethod]
        public void GivenAEntity_WhenIsUpadted_BeSureItReturnsUpdatedData()
        {
            var bookingentities = service.GetAllBookingEntities()[7];
            bookingentities.Price = 999;
            bookingentities.Address.Postcode = "SK74QW";
            bookingentities.Prices.FirstOrDefault().DayPrices.FirstOrDefault().HourPrices.FirstOrDefault().Hourprice = 8888;
            //Act
            var results = service.Update(bookingentities);

            //Assert
            Assert.IsTrue(results);          
        }

        [TestMethod]
        public void GivenAEntity_WhenIsDeleted_BeSureItIsNotThere()
        {
            var bookingentities = service.GetAllBookingEntities()[7];          

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
