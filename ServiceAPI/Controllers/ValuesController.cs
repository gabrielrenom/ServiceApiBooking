﻿using ACP.Business.Services.Interfaces;
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
          public  string testdata { get; set; }
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
        public test Post([FromBody]test value)
        {
            return value;
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

//Content-MD5: jFPE4pFPAkgFnRekSViwSQ==
//Content-Type: application/json;
//X-ApiAuth-Username: username
//X-ApiAuth-Date: Thu, 13 Aug 2015 10:45:25 GMT
//Authorization: ApiAuth iYAIzzftPcaVKBxNLHYqpk+DR163KuR8hFdGHe70CfI=
//Host: localhost:52034
//Content-Length: 27


//{"testdata":"some content"}
