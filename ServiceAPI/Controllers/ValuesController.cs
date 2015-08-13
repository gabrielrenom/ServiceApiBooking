using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    //[Authorize]
    
        public class test
        {
          public  string mytest { get; set; }
        }

    public class ValuesController : ApiController
    {
        // GET api/values
        [Action1DebugActionWebApiFilter]
        [OverrideActionFiltersAttribute]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]test value)
        {
            String s = value.mytest;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
