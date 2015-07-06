using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ACP.Business.APIs.APH.Models
{
    public class Info
    {
        [XmlElement("Details")]
        public List<String> Details { get; set; }
    }
}
