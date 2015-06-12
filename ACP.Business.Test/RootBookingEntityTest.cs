using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.Repository;
using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;

namespace ACP.Business.Test
{
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
        }


    }
}
