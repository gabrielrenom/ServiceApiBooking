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
    public class RootBookingEntityManager : BaseACPManager<RootBookingEntityModel, RootBookingEntity>, IRootBookingEntityManager
    {
        public RootBookingEntityManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

    }
}
