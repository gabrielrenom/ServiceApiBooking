using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Managers;
using ACP.Business.Models;

namespace ACP.Business.Services
{
    public class RootBookingEntityService : IRootBookingEntityService
    {
        private readonly IRootBookingEntityManager _rootBookingEntityManager;

        public RootBookingEntityService(IRootBookingEntityManager rootBookingEntityManager)
        {
            _rootBookingEntityManager = rootBookingEntityManager;
        }

        public RootBookingEntityModel Add(Models.RootBookingEntityModel model)
        {         
            return _rootBookingEntityManager.Add(model);
        }

        public bool Update(Models.RootBookingEntityModel model)
        {
            return _rootBookingEntityManager.Update(model);
        }

        public IList<Models.RootBookingEntityModel> GetAll()
        {
            return _rootBookingEntityManager.GetAll().ToList();
        }

        public Models.RootBookingEntityModel GetById(int Id)
        {
            return _rootBookingEntityManager.GetById(Id);
        }

        public bool Remove(int Id)
        {
            return _rootBookingEntityManager.DeleteById(Id);
        }
    }
}
