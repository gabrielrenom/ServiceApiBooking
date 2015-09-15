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

        public async Task<RootBookingEntityModel> Add(Models.RootBookingEntityModel model)
        {         
            return _rootBookingEntityManager.Add(model);
        }

        public async Task<bool> Update(Models.RootBookingEntityModel model)
        {
            return _rootBookingEntityManager.Update(model);
        }

        public async Task<IList<Models.RootBookingEntityModel>> GetAll()
        {
            return _rootBookingEntityManager.GetAll().ToList();
        }

        public async Task<Models.RootBookingEntityModel> GetById(int Id)
        {
            return _rootBookingEntityManager.GetById(Id);
        }

        public async Task<bool> Remove(int Id)
        {
            return  _rootBookingEntityManager.DeleteById(Id);
        }

        public Task<RootBookingEntityModel> GetByName(string name)
        {
            return _rootBookingEntityManager.GetByName(name);
        }
    }
}
