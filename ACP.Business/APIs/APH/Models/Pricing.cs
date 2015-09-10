using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.APH.Models
{
    public class Pricing
    {        
        public string MinPricingDuration{get;set;}
        public string Duration{get;set;}
        public string TotalPrice{get;set;}
        public string AgentComm { get; set; }
    }
}
