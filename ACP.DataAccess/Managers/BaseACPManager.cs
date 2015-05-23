using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using ACP.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    
    public abstract class BaseACPManager<TD, TE> : BaseManager<TD, TE>, IBaseACPManager<TD, TE>
        where TD : BaseModel, new()
        where TE : BaseEntity, new()
    {
        protected BaseACPManager(IACPRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Exposes the underlying repository and context.
        /// </summary>
        public ACPRepository ACPRepository { get { return Repository as ACPRepository; } }


    }
}
