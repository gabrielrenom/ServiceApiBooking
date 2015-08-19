using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACP.Business.APIs.PP;
using System.Threading.Tasks;

namespace ACP.Business.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetRest()
        {
            PurpleParking pp = new PurpleParking();

            var result = await pp.GetAirports();

            Assert.IsNotNull(result);
        }
    }
}
