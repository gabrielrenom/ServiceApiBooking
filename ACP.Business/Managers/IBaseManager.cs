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

namespace ACP.Business.Managers
{
    public interface IBaseManager<TD, TE>
        where TD : BaseModel
        where TE : BaseEntity
    {
        /// <summary>
        /// Adds the domain model 
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns>The id of the newly created item</returns>
        TD Add(TD domainModel);

        /// <summary>
        /// Adds the domain model 
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns>The id of the newly created item</returns>
        Task<TD> AddAsync(TD domainModel);

        /// <summary>
        /// Gets the domain model by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The domain model.</returns>
        TD GetById(int id);

        /// <summary>
        /// Returns a domain model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An asset model</returns>
        Task<TD> GetByIdAsync(int id);

        /// <summary>
        /// Gets the domain model for the criteria
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TD GetSingle(Func<TE, bool> where);

        /// <summary>
        /// Gets the domain model for the criteria
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<TD> GetSingleAsync(Expression<Func<TE, bool>> where);

        /// <summary>
        /// Gets all items of the specified type 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TD> GetAll();

        /// <summary>
        /// Gets all items of the specified type 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TD>> GetAllAsync();

        /// <summary>
        /// Gets a list of domain models that match the criteria.
        /// </summary>
        /// <param name="where"></param>
        /// <returns>A list of specified domain objects</returns>
        IEnumerable<TD> GetList(Func<TE, bool> where);

        /// <summary>
        /// Gets a list of domain models that match the criteria.
        /// </summary>
        /// <param name="where"></param>
        /// <returns>A list of specified domain objects</returns>
        Task<IEnumerable<TD>> GetListAsync(Expression<Func<TE, bool>> where);

        /// <summary>
        /// Updates the specified domain model.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <returns>True if the update is successful.</returns>
        bool Update(TD domainModel);

        /// <summary>
        /// Updates the specified domain model.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <returns>True if the update is successful.</returns>
        Task<bool> UpdateAsync(TD domainModel);

        /// <summary>
        /// Deletes the domain model by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if the deletion is successful.</returns>
        bool DeleteById(int id);

        /// <summary>
        /// Deletes the domain model by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if the deletion is successful.</returns>
        Task<bool> DeleteByIdAsync(int id);

        /// <summary>
        /// Validates the specified domain model.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <returns></returns>
        IEnumerable<ValidationResult> Validate(TD domainModel);

        /// <summary>
        /// Transforms the domain model into a corresponding data model.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <param name="dataModel">The data model to overwrite. If this value is null then a new object is created.</param>
        /// <returns>The data model.</returns>
        TE ToDataModel(TD domainModel, TE dataModel = null);

        /// <summary>
        /// Transforms the domain model into a corresponding data model, including linked entities.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <param name="dataModel">The data model to overwrite. If this value is null then a new object is created.</param>
        /// <returns>The data model.</returns>
        TE ToDataModelWithChildNodes(TD domainModel, TE dataModel = null);

        /// <summary>
        /// Transforms the data model into a corresponding domain model.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <returns>The domain model.</returns>
        TD ToDomainModel(TE dataModel);

        /// <summary>
        /// Transforms the data model into a corresponding domain model, including linked entities.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <returns>The domain model</returns>
        TD ToDomainModelWithChildNodes(TE dataModel);

        IGenericRepository Repository { get; set; }
    }
}
