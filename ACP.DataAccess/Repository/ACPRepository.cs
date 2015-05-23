using ACP.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Repository
{
    /// <summary>
    /// Implements the Generic Repository for the Asset Management Context
    /// </summary>
    public class ACPRepository : GenericRepository<ACPContext>, IACPRepository
    {

    }

}
