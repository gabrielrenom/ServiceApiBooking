using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.APH.Models.Availability
{


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class API_Reply
    {

        private API_ReplyCarPark[] carParkField;

        private string systemField;

        private decimal versionField;

        private string productField;

        private string customerField;

        private byte sessionField;

        private byte requestCodeField;

        private string resultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CarPark")]
        public API_ReplyCarPark[] CarPark
        {
            get
            {
                return this.carParkField;
            }
            set
            {
                this.carParkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string System
        {
            get
            {
                return this.systemField;
            }
            set
            {
                this.systemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Customer
        {
            get
            {
                return this.customerField;
            }
            set
            {
                this.customerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte RequestCode
        {
            get
            {
                return this.requestCodeField;
            }
            set
            {
                this.requestCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class API_ReplyCarPark
    {

        private string carParkCodeField;

        private string carParkNameField;

        private string productCodeField;

        private string productNameField;

        private byte durationField;

        private decimal totalPriceField;

        private string gatePriceField;

        private byte commissionField;

        private string terminalsField;

        private byte cField;

        /// <remarks/>
        public string CarParkCode
        {
            get
            {
                return this.carParkCodeField;
            }
            set
            {
                this.carParkCodeField = value;
            }
        }

        /// <remarks/>
        public string CarParkName
        {
            get
            {
                return this.carParkNameField;
            }
            set
            {
                this.carParkNameField = value;
            }
        }

        /// <remarks/>
        public string ProductCode
        {
            get
            {
                return this.productCodeField;
            }
            set
            {
                this.productCodeField = value;
            }
        }

        /// <remarks/>
        public string ProductName
        {
            get
            {
                return this.productNameField;
            }
            set
            {
                this.productNameField = value;
            }
        }

        /// <remarks/>
        public byte Duration
        {
            get
            {
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }

        /// <remarks/>
        public decimal TotalPrice
        {
            get
            {
                return this.totalPriceField;
            }
            set
            {
                this.totalPriceField = value;
            }
        }

        /// <remarks/>
        public string GatePrice
        {
            get
            {
                return this.gatePriceField;
            }
            set
            {
                this.gatePriceField = value;
            }
        }

        /// <remarks/>
        public byte Commission
        {
            get
            {
                return this.commissionField;
            }
            set
            {
                this.commissionField = value;
            }
        }

        /// <remarks/>
        public string Terminals
        {
            get
            {
                return this.terminalsField;
            }
            set
            {
                this.terminalsField = value;
            }
        }

       
    }



}

