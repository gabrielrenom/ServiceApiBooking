using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess
{
    public class ACPInitialiser : DropCreateDatabaseIfModelChanges<ACPContext>
    {
    }
}
