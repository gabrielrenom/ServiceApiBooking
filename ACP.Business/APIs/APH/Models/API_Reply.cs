using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs.APH.Models
{
    [XmlRoot("API_Reply"), Serializable]  
    public class API_Reply
    {
        [XmlAttribute("System")]
        public string System { get; set; }
        
        [XmlAttribute("Version")]
        public string Version { get; set; }

        [XmlAttribute("Product")]
        public string Product { get; set; }

        [XmlAttribute("Customer")]
        public string Customer { get; set; }

        [XmlAttribute("Session")]
        public string Session { get; set; }

        [XmlAttribute("RequestCode")]
        public string RequestCode { get; set; }

        [XmlAttribute("Result")]
        public string Result { get; set; }        

        [XmlElement("CarPark")]       
        public List <CarPark> CarPark { get; set; }

        [XmlElement("Terminal")]       
        public List<Terminal> Terminal { get; set; }

        [XmlElement("Pricing")]
        public Pricing Pricing { get; set; }

        [XmlElement("Info")]
        public Info Info { get; set; }

        [XmlElement("CarParkCodeField")]
        public string CarParkCodeField { get; set; }

        [XmlElement("CarParkCodeField")]
        public string CarParkNameField { get; set; }

        [XmlElement("ProductCodeField")]
        public string ProductCodeField;

        [XmlElement("ProductNameField")]
        public string ProductNameField { get; set; }

        [XmlElement("DurationField")]
        public byte DurationField { get; set; }

        [XmlElement("TotalPriceField")]
        public decimal TotalPriceField { get; set; }

        [XmlElement("GatePriceField")]
        public object GatePriceField { get; set; }

        [XmlElement("CommissionField")]
        public byte CommissionField { get; set; }

        [XmlElement("TerminalsField")]
        public string TerminalsField { get; set; }

    }

    public class Book
    {

        public string Title { get; set; }

    }
}
