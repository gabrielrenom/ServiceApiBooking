using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;

namespace ACP.Business.Services
{
    public class StatusService : IStatusService
    {
        private IStatusService _statuservice;

        public StatusService(IStatusService statuservice)
        {
            _statuservice = statuservice;
        }

        public StatusModel Add(StatusModel domainModel)
        {
            return _statuservice.Add(domainModel);
        }

        public StatusModel GetByName(string StatusName)
        {
            return _statuservice.GetByName(StatusName);
        }
    }
}
