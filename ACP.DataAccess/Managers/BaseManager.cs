using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ACP.DataAccess.Managers
{
    public abstract class BaseManager<TD, TE> : IBaseManager<TD, TE>
        where TD : BaseModel, new()
        where TE : BaseEntity, new()
    {

        public IGenericRepository Repository { get; set; }

        protected BaseManager(IGenericRepository repository)
        {
            Repository = repository;
        }

        public virtual TD GetById(int id)
        {
            return GetByIdIncluding(id);
        }

        public virtual async Task<TD> GetByIdAsync(int id)
        {
            return await GetByIdIncludingAsync(id);
        }

        protected virtual TD GetByIdIncluding(int id, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModel = Repository.GetSingle(i => i.Id == id, navigationProperties);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            return ToDomainModelWithChildNodes(dataModel);
        }

        protected virtual async Task<TD> GetByIdIncludingAsync(int id, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModel = await Repository.GetSingleAsync(i => i.Id == id, navigationProperties);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            return ToDomainModelWithChildNodes(dataModel);
        }

        public virtual TD GetSingle(Func<TE, bool> where)
        {
            return GetSingleIncluding(where);
        }

        protected virtual TD GetSingleIncluding(Func<TE, bool> where, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModel = Repository.GetSingle(where, navigationProperties);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            return ToDomainModelWithChildNodes(dataModel);
        }

        public async virtual Task<TD> GetSingleAsync(Expression<Func<TE, bool>> where)
        {
            return await GetSingleIncludingAsync(where);
        }

        protected async virtual Task<TD> GetSingleIncludingAsync(Expression<Func<TE, bool>> where, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModel = await Repository.GetSingleAsync(where, navigationProperties);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            return ToDomainModelWithChildNodes(dataModel);
        }

        public IEnumerable<TD> GetAll()
        {
            return GetAllIncluding();
        }

        protected virtual IEnumerable<TD> GetAllIncluding(params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModelList = Repository.AllIncluding(navigationProperties).ToList();

            if (dataModelList == null)
            {
                throw new ItemNotFoundException();
            }

            return dataModelList.Select(ToDomainModelWithChildNodes).ToList();
        }

        public async virtual Task<IEnumerable<TD>> GetAllAsync()
        {
            return await GetAllIncludingAsync();
        }

        protected async virtual Task<IEnumerable<TD>> GetAllIncludingAsync(params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModelList = await Repository.AllIncluding(navigationProperties).ToListAsync();

            if (dataModelList == null)
            {
                throw new ItemNotFoundException();
            }

            return dataModelList.Select(x => ToDomainModelWithChildNodes(x)).ToList();
        }

        public IEnumerable<TD> GetList(Func<TE, bool> where)
        {
            return GetListIncluding(where);
        }

        protected virtual IEnumerable<TD> GetListIncluding(Func<TE, bool> where, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModelList = Repository.AllIncluding(navigationProperties).Where(where).ToList();

            if (dataModelList == null)
            {
                throw new ItemNotFoundException();
            }

            return dataModelList.Select(x => ToDomainModel(x)).ToList();
        }

        public async virtual Task<IEnumerable<TD>> GetListAsync(Expression<Func<TE, bool>> where)
        {
            return await GetListIncludingAsync(where);
        }

        protected virtual async Task<IEnumerable<TD>> GetListIncludingAsync(Expression<Func<TE, bool>> where, params Expression<Func<TE, object>>[] navigationProperties)
        {
            var dataModelList = await Repository.AllIncluding(navigationProperties).Where(where).ToListAsync();

            if (dataModelList == null)
            {
                throw new ItemNotFoundException();
            }

            return dataModelList.Select(ToDomainModel).ToList();
        }

        public virtual TD Add(TD domainModel)
        {
            var validationErrors = Validate(domainModel).ToList();

            if (validationErrors.Any())
            {
                throw new ValidationErrorsException(validationErrors);
            }

            var dataModel = ToDataModel(domainModel);
            dataModel.Created = DateTime.UtcNow;

            Repository.Add(dataModel);
            Repository.Commit();

            return ToDomainModel(dataModel);
        }

        public virtual async Task<TD> AddAsync(TD domainModel)
        {
            var validationErrors = Validate(domainModel).ToList();

            if (validationErrors.Any())
            {
                throw new ValidationErrorsException(validationErrors);
            }

            var dataModel = ToDataModel(domainModel);
            dataModel.Created = DateTime.UtcNow;
           

            Repository.Add(dataModel);
            await Repository.CommitAsync();

            return ToDomainModel(dataModel);
        }

        public virtual bool Update(TD domainModel)
        {
            var dataModel = Repository.GetSingle<TE>(i => i.Id == domainModel.Id);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            var validationErrors = Validate(domainModel).ToList();

            if (validationErrors.Any())
            {
                throw new ValidationErrorsException(validationErrors);
            }

            ToDataModel(domainModel, dataModel);
            dataModel.Modified = DateTime.Now;

            Repository.Update(dataModel);
            Repository.Commit();

            return true;
        }

        public virtual async Task<bool> UpdateAsync(TD domainModel)
        {
            var dataModel = Repository.GetSingle<TE>(i => i.Id == domainModel.Id);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            var validationErrors = Validate(domainModel).ToList();

            if (validationErrors.Any())
            {
                throw new ValidationErrorsException(validationErrors);
            }

            ToDataModel(domainModel, dataModel);
            dataModel.Modified = DateTime.Now;

            Repository.Update(dataModel);
            await Repository.CommitAsync();

            return true;
        }

        public virtual bool DeleteById(int id)
        {
            var dataModel = Repository.GetSingle<TE>(i => i.Id == id);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            Repository.Delete(dataModel);
            Repository.Commit();
            return true;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var dataModel = await Repository.GetSingleAsync<TE>(i => i.Id == id);

            if (dataModel == null)
            {
                throw new ItemNotFoundException();
            }

            Repository.Delete(dataModel);
            await Repository.CommitAsync();
            return true;
        }

        public virtual IEnumerable<ValidationResult> Validate(TD domainModel)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(domainModel, null, null);
            Validator.TryValidateObject(domainModel, validationContext, validationResults, true);
            return validationResults;
        }

        public virtual TD ToDomainModel(TE dataModel)
        {
            return new TD
            {
                Id = dataModel.Id,
                Created = dataModel.Created,
                Modified = dataModel.Modified,
                CreatedBy = dataModel.CreatedBy,
                ModifiedBy = dataModel.ModifiedBy
            };
        }

        public virtual TD ToDomainModelWithChildNodes(TE dataModel)
        {
            return ToDomainModel(dataModel);
        }

        public virtual TE ToDataModel(TD domainModel, TE dataModel = null)
        {
            if (dataModel == null)
                dataModel = new TE();

            dataModel.Id = domainModel.Id;
            dataModel.Created = domainModel.Created;
            dataModel.Modified = domainModel.Modified;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.ModifiedBy = domainModel.ModifiedBy;

            return dataModel;
        }

        public virtual TE ToDataModelWithChildNodes(TD domainModel, TE dataModel = null)
        {
            return ToDataModel(domainModel);
        }
    }
}
