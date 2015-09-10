using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs.APH.Models
{
    public class Terminal
    {
        [XmlAttribute("t")]
        public string t{get;set;}
        public string TerminalCode{get;set;}
        public string TerminalName{get;set;}        
   }
}
