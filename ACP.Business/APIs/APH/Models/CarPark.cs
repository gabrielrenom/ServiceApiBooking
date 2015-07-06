using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs.APH.Models
{
    public class CarPark
    {
        
        public string CarParkCode{get;set;}
        
        public string CarParkName { get; set; }
        
        public string ProductCode { get; set; }
        
        public string productName { get; set; }
        
        public string Duration { get; set; }
        
        public string TotalPrice { get; set; }
        
        public string GatePrice { get; set; }
        
        public string Commission { get; set; }
        
        public string Terminals { get; set; }
    }
}
