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
        private string v1;
        private string v2;

        public Terminal(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        [XmlAttribute("t")]
        public string t{get;set;}
        public string TerminalCode{get;set;}
        public string TerminalName{get;set;}        
   }
}
