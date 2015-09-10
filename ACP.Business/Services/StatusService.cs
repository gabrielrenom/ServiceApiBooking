using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;
using ACP.Business.Managers;

namespace ACP.Business.Services
{
    public class StatusService : IStatusService
    {
        private IStatusManager _statusmanager;

        public StatusService(IStatusManager statusmanager)
        {
            _statusmanager = statusmanager;
        }

        public async Task<StatusModel> Add(StatusModel domainModel)
        {
            return  _statusmanager.Add(domainModel);
        }

        public async Task<StatusModel> GetByName(string StatusName)
        {
            return  _statusmanager.GetByName(StatusName);
        }
    }
}
