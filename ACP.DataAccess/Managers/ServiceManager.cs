using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class ServiceManager: BaseManager<ServiceModel, Service>, IServiceManager
    {
        public ServiceManager(IGenericRepository repository)
            : base(repository)
        {
            Repository = repository;
        }
        
    }
}
