﻿using ACP.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class StatusModel:BaseModel
    {
        public StatusType StatusType { get; set; }
    }
}
