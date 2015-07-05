using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.Models
{
    public class API_Request
    {
        public Agent Agent { get; set; }
        public Itinerary Itinerary { get; set; }
        public string System { get; set; }
        public string Version { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string Session { get; set; }
        public string RequestCode { get; set; }       
    }
}

//REQUEST EXAMPLE
//    <API_Request
//System="APH"
//Version="1.0"
//Product="CarPark"
//Customer="X"
//Session="000000003"
//RequestCode="11">
//<Agent>
//<ABTANumber>WA789</ABTANumber>
//<Password></Password>
//<Initials>thecarparksuper</Initials>
//</Agent>
//<Itinerary>
//<ArrivalDate>20Nov15</ArrivalDate>
//<DepartDate>27Nov15</DepartDate>
//<ArrivalTime>0600</ArrivalTime>
//<DepartTime>1800</DepartTime>
//<Location>LGW</Location>
//<Terminals>ALL</Terminals>
//</Itinerary>
//</API_Request>
