using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{    
      /// <summary>
    /// Extends base manager. Methods common to all Amt managers should be defined here
    /// </summary>
    /// <typeparam name="TD"></typeparam>
    /// <typeparam name="TE"></typeparam>
    public interface IBaseACPManager<TD, TE> : IBaseManager<TD, TE>
        where TD : BaseModel
        where TE : BaseEntity
    {
        
    }
}
