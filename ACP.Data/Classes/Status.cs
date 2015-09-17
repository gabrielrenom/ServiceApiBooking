using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class Status: BaseEntity
    {
        public StatusType StatusType { get; set; }
    }
}
