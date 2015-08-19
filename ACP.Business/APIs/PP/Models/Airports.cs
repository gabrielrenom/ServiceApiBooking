
namespace ACP.Business.APIs.PP.Models.Airports
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class response
    {

        private responseAirport[] airportsField;

        private responseMessages messagesField;

        private string parametersField;

        private bool isOKField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("airport", IsNullable = false)]
        public responseAirport[] airports
        {
            get
            {
                return this.airportsField;
            }
            set
            {
                this.airportsField = value;
            }
        }

        /// <remarks/>
        public responseMessages messages
        {
            get
            {
                return this.messagesField;
            }
            set
            {
                this.messagesField = value;
            }
        }

        /// <remarks/>
        public string parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isOK
        {
            get
            {
                return this.isOKField;
            }
            set
            {
                this.isOKField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class responseAirport
    {

        private string nameField;

        private string shortnameField;

        private string[] terminalsField;

        private responseAirportCarPark[] carParksField;

        private string codeField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string shortname
        {
            get
            {
                return this.shortnameField;
            }
            set
            {
                this.shortnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("terminal", IsNullable = false)]
        public string[] terminals
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

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("carPark", IsNullable = false)]
        public responseAirportCarPark[] carParks
        {
            get
            {
                return this.carParksField;
            }
            set
            {
                this.carParksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class responseAirportCarPark
    {

        private string codeField;

        private string airportField;

        private string serviceTypeField;

        private string nameField;

        private string descriptionField;

        private string furtherInformationField;

        private string addressField;

        private string directionsField;

        private string arrivalProcedureField;

        private string departureProcedureField;

        private string openingPeriodsField;

        private string helplineField;

        private string distanceToAirportField;

        private string transferFromAirportField;

        private string transferTimeField;

        private object transferFrequencyField;

        private string cancellationsField;

        private string securityMeasuresField;

        private string maximumCarSizeField;

        private ushort leadTimeHoursField;

        private string facilitiesForDisabledField;

        private object mapURLField;

        private object errataField;

        private string logoURLField;

        /// <remarks/>
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string airport
        {
            get
            {
                return this.airportField;
            }
            set
            {
                this.airportField = value;
            }
        }

        /// <remarks/>
        public string serviceType
        {
            get
            {
                return this.serviceTypeField;
            }
            set
            {
                this.serviceTypeField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string furtherInformation
        {
            get
            {
                return this.furtherInformationField;
            }
            set
            {
                this.furtherInformationField = value;
            }
        }

        /// <remarks/>
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string directions
        {
            get
            {
                return this.directionsField;
            }
            set
            {
                this.directionsField = value;
            }
        }

        /// <remarks/>
        public string arrivalProcedure
        {
            get
            {
                return this.arrivalProcedureField;
            }
            set
            {
                this.arrivalProcedureField = value;
            }
        }

        /// <remarks/>
        public string departureProcedure
        {
            get
            {
                return this.departureProcedureField;
            }
            set
            {
                this.departureProcedureField = value;
            }
        }

        /// <remarks/>
        public string openingPeriods
        {
            get
            {
                return this.openingPeriodsField;
            }
            set
            {
                this.openingPeriodsField = value;
            }
        }

        /// <remarks/>
        public string helpline
        {
            get
            {
                return this.helplineField;
            }
            set
            {
                this.helplineField = value;
            }
        }

        /// <remarks/>
        public string distanceToAirport
        {
            get
            {
                return this.distanceToAirportField;
            }
            set
            {
                this.distanceToAirportField = value;
            }
        }

        /// <remarks/>
        public string transferFromAirport
        {
            get
            {
                return this.transferFromAirportField;
            }
            set
            {
                this.transferFromAirportField = value;
            }
        }

        /// <remarks/>
        public string transferTime
        {
            get
            {
                return this.transferTimeField;
            }
            set
            {
                this.transferTimeField = value;
            }
        }

        /// <remarks/>
        public object transferFrequency
        {
            get
            {
                return this.transferFrequencyField;
            }
            set
            {
                this.transferFrequencyField = value;
            }
        }

        /// <remarks/>
        public string cancellations
        {
            get
            {
                return this.cancellationsField;
            }
            set
            {
                this.cancellationsField = value;
            }
        }

        /// <remarks/>
        public string securityMeasures
        {
            get
            {
                return this.securityMeasuresField;
            }
            set
            {
                this.securityMeasuresField = value;
            }
        }

        /// <remarks/>
        public string maximumCarSize
        {
            get
            {
                return this.maximumCarSizeField;
            }
            set
            {
                this.maximumCarSizeField = value;
            }
        }

        /// <remarks/>
        public ushort leadTimeHours
        {
            get
            {
                return this.leadTimeHoursField;
            }
            set
            {
                this.leadTimeHoursField = value;
            }
        }

        /// <remarks/>
        public string facilitiesForDisabled
        {
            get
            {
                return this.facilitiesForDisabledField;
            }
            set
            {
                this.facilitiesForDisabledField = value;
            }
        }

        /// <remarks/>
        public object mapURL
        {
            get
            {
                return this.mapURLField;
            }
            set
            {
                this.mapURLField = value;
            }
        }

        /// <remarks/>
        public object errata
        {
            get
            {
                return this.errataField;
            }
            set
            {
                this.errataField = value;
            }
        }

        /// <remarks/>
        public string logoURL
        {
            get
            {
                return this.logoURLField;
            }
            set
            {
                this.logoURLField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class responseMessages
    {

        private responseMessagesMessage messageField;

        /// <remarks/>
        public responseMessagesMessage message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class responseMessagesMessage
    {

        private string typeField;

        private string codeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}
