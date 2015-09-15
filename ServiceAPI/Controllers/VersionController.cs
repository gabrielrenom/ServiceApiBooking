using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    public class VersionController : ApiController
    {
        [HttpGet]
        public string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.InternalName + " - " + versionInfo.FileVersion;
        }
    }
}
