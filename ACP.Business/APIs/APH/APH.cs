using ACP.Business.APIs.APH.Models;
using ACP.Business.Managers;
using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs.APH
{
    public class APH: IAPH
    {
        private readonly IBookingEntityManager _bookingEntityManager;
        private readonly IRootBookingEntityManager _rootBookingEntityManager;
        private List<Terminal> _terminalList;
        private List<Airport> _airportList;
        private APHAirports _aphairports;

        public APH(IRootBookingEntityManager rootBookingEntityManager, IBookingEntityManager bookingEntityManager)
        {
            _bookingEntityManager = bookingEntityManager;
            _rootBookingEntityManager = rootBookingEntityManager;

            _terminalList = new List<Terminal>();
            _airportList = new List<Airport>();
            _aphairports = new APHAirports();


            #region [ TERMINALS ]
        _terminalList.Add(new Terminal("Aberdeen", "ABZ"));
            _terminalList.Add(new Terminal("Belfast International", "BFS"));
            _terminalList.Add(new Terminal("Birmingham", "BT1"));
            _terminalList.Add(new Terminal("Bristol", "BRS"));
            _terminalList.Add(new Terminal("Cardiff", "CWL"));
            _terminalList.Add(new Terminal("Dover", "DOV"));
            _terminalList.Add(new Terminal("Doncaster Robin Hood", "DSA"));
            _terminalList.Add(new Terminal("Durham Tees Valley", "MME"));
            _terminalList.Add(new Terminal("Edinburgh", "EDI"));
            _terminalList.Add(new Terminal("East Midlands", "EMA"));
            _terminalList.Add(new Terminal("Exeter", "EXT"));
            _terminalList.Add(new Terminal("Glasgow", "GLA"));
            _terminalList.Add(new Terminal("Leeds Bradford", "LBA"));
            _terminalList.Add(new Terminal("London Gatwick", "N"));
            _terminalList.Add(new Terminal("London Gatwick", "S"));
            _terminalList.Add(new Terminal("London Heathrow", "HT1"));
            _terminalList.Add(new Terminal("London Heathrow", "HT2"));
            _terminalList.Add(new Terminal("London Heathrow", "HT3"));
            _terminalList.Add(new Terminal("London Heathrow", "HT4"));
            _terminalList.Add(new Terminal("London Heathrow", "HT5"));
            _terminalList.Add(new Terminal("London Luton", "LTN"));
            _terminalList.Add(new Terminal("London Southend", "SOU"));
            _terminalList.Add(new Terminal("London Stansted", "STN"));
            _terminalList.Add(new Terminal("Liverpool", "LPL"));
            _terminalList.Add(new Terminal("Prestwick", "PIK"));
            _terminalList.Add(new Terminal("Manchester", "MT1"));
            _terminalList.Add(new Terminal("Manchester", "MT2"));
            _terminalList.Add(new Terminal("Manchester", "MT3"));
            _terminalList.Add(new Terminal("Newcastle", "NCL"));
            _terminalList.Add(new Terminal("Southampton Terminal", "SOU"));
            _terminalList.Add(new Terminal("Southampton Port", "SOP"));
            #endregion

            #region [ AIRPORT ]
            _airportList.Add(new Airport("Aberdeen", "ABZ"));
            _airportList.Add(new Airport("Belfast International", "BFS"));
            _airportList.Add(new Airport("Birmingham", "BHX"));
            _airportList.Add(new Airport("Bristol", "BRS"));
            _airportList.Add(new Airport("Cardiff", "CWL"));
            _airportList.Add(new Airport("Dover", "DOV"));
            _airportList.Add(new Airport("Doncaster Robin Hood", "DSA"));
            _airportList.Add(new Airport("Durham Tees Valley", "MME"));
            _airportList.Add(new Airport("Edinburgh", "EDI"));
            _airportList.Add(new Airport("East Midlands", "EMA"));
            _airportList.Add(new Airport("Exeter", "EXT"));
            _airportList.Add(new Airport("Glasgow", "GLA"));
            _airportList.Add(new Airport("Leeds Bradford", "LBA"));
            _airportList.Add(new Airport("London City", "LCY"));
            _airportList.Add(new Airport("London Gatwick", "LGW"));
            _airportList.Add(new Airport("London Heathrow", "LHR"));
            _airportList.Add(new Airport("London Luton", "LTN"));
            _airportList.Add(new Airport("London Southend", "SEN"));
            _airportList.Add(new Airport("London Stansted", "STN"));
            _airportList.Add(new Airport("Liverpool", "LPL"));
            _airportList.Add(new Airport("Prestwick", "PIK"));
            _airportList.Add(new Airport("Manchester", "MAN"));
            _airportList.Add(new Airport("Newcastle", "NCL"));
            _airportList.Add(new Airport("Southampton Airport", "SOU"));
            _airportList.Add(new Airport("Southampton Port", "SOP"));
            #endregion

            _aphairports.AirportList = _airportList;
        }

        private void GetCarParkCodes(ref APHAirports airports)
        {
            foreach (var item in airports.AirportList)
            {
                //API_Request request = new API_Request
                //{
                //    Agent = new Agent
                //    {
                //        ABTANumber = "WA789",
                //        Initials = "thecarparksuper",
                //        Password = ""
                //    },
                //    Itinerary = new Itinerary
                //    {
                //        ArrivalDate = DateTime.Now.AddMonths(1).ToString("dMMMyy"),
                //        DepartDate = DateTime.Now.AddMonths(2).ToString("dMMMyy"),
                //        ArrivalTime = "0600",
                //        DepartTime = "1800",
                //        Location = item.Code,
                //        Terminals = "ALL"
                //    },
                //    System = "APH",
                //    Version = "1.0",
                //    Product = "CarPark",
                //    Customer = "X",
                //    Session = "000000003",
                //    RequestCode = "11"
                //};

                API_Request request = new API_Request
                {
                    Agent = new Agent
                    {
                        ABTANumber = "WA789",
                        Initials = "thecarparksuper",
                        Password = ""
                    },
                    Itinerary = new Itinerary
                    {
                        ArrivalDate = "20Nov15",
                        DepartDate = "27Nov15",
                        ArrivalTime = "0600",
                        DepartTime = "1800",
                        Location = item.Code,
                        Terminals = "ALL"
                    },
                    System = "APH",
                    Version = "1.0",
                    Product = "CarPark",
                    Customer = "X",
                    Session = "000000003",
                    RequestCode = "11"
                };

                var response = this.CarParkAvailability(request);

                if (response.CarPark != null)
                {
                    foreach (var carpark in response.CarPark)
                    {
                        CarPark carparkitem = new CarPark();
                        carparkitem.Code = carpark.CarParkCode;
                        carparkitem.Name = carpark.CarParkName;
                        carparkitem.ProductName = carpark.ProductName;
                        carparkitem.Terminals = carpark.Terminals;
                        carparkitem.Commision = carpark.Commission;
                        carparkitem.ProductCode = carpark.ProductCode;

                        item.CarParks.Add(carparkitem);
                    }
                }
            }
        }



        public bool FillBookingEnties()
        {
            var allcarparks = _bookingEntityManager.GetAll();
            bool result = false;

            //## LIVE
            GetCarParkCodes(ref _aphairports);

            //var stringwriter = new System.IO.StringWriter();
            //var serializer = new XmlSerializer(_aphairports.GetType());
            //serializer.Serialize(stringwriter, _aphairports);
            //var carparcodes= stringwriter.ToString();

            foreach (var airport in _aphairports.AirportList)
            {
                foreach (var carpark in airport.CarParks)
                {
                    #region [ REQUEST ]
                    API_Request request = new API_Request
                    {
                        Agent = new Agent
                        {
                            ABTANumber = "WA789",
                            Initials = "thecarparksuper",
                            Password = ""
                        },
                        Itinerary = new Itinerary
                        {
                            ArrivalDate = "20Nov15",//DateTime.Now.AddMonths(1).ToString("dMMMyy"),
                            CarParkCode = carpark.Code,//"LGW1",//carpark.Code,
                        },
                        Request = new Request
                        {
                             RequestType= "1"
                        },
                        System = "APH",
                        Version = "1.0",
                        Product = "CarPark",
                        Customer = "X",
                        Session = "000000007",
                        RequestCode = "6"
                    };


                    // < API_Request
                    // System = "APH"
                    // Version = "1.0"
                    // Product = "CarPark"
                    // Customer = "X"
                    // Session = "000000007"
                    // RequestCode = "6" >
                    // < Agent >
                    //     < ABTANumber > WA789 </ ABTANumber >
                    //     < Password ></ Password >
                    //     < Initials > thecarparksuper </ Initials >
                    // </ Agent >
                    // < Itinerary >
                    //     < ArrivalDate > 20Nov15 </ ArrivalDate >
                    //        < CarParkCode > LGW1 </ CarParkCode >
                    //    </ Itinerary >
                    //    < Request >
                    //        < RequestType > 1 </ RequestType >
                    //    </ Request >
                    //</ API_Request >
                    #endregion

                    ACP.Business.APIs.APH.Models.Details.API_Reply reply = CarParkInformationDetails(request);

                    if (reply.Info != null)
                    foreach (var item in reply.Info.Details)
                    {
                            carpark.Details += item.Value; ;
                    }
                }
            }


            AddCarParksInDatabase(_aphairports);

            return result;
        }

        public async Task AddCarParksInDatabase (APHAirports airports)
        {
            var allcarparks = _bookingEntityManager.GetAll();

            DateTime? date = null;

            foreach (var item in allcarparks)
            {
                if (item.Properties != null)
                {
                    foreach (var property in item.Properties)
                    {
                        if (property.Key.ToLower() == "provider" && property.Value.ToLower() == "aph")
                        {
                            date = item.Modified;
                            break;
                        }
                    }
                }
            }

            if (date == null) date = DateTime.Now.AddHours(-1);
            if (date.Value.AddHours(1) < DateTime.Now || date==null)
            {
                foreach (var airport in airports.AirportList)
                {
                    var airportwherecarparkwillbeadded = await _rootBookingEntityManager.GetByCode(airport.Code);

                    if (airportwherecarparkwillbeadded != null)
                    {
                        foreach (var carpark in airport.CarParks)
                        {
                            BookingEntityModel airportcarpark = new BookingEntityModel();

                            airportcarpark.Code = carpark.Code;
                            airportcarpark.Name = carpark.Name;
                            airportcarpark.ModifiedBy = "System";
                            airportcarpark.CreatedBy = "System";
                            airportcarpark.Modified = DateTime.Now;
                            airportcarpark.Created = DateTime.Now;

                            airportcarpark.Properties = new Collection<PropertyModel>();

                            airportcarpark.Properties.Add(new PropertyModel { Key = "Direcctions", Value = carpark.Details, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });
                            airportcarpark.Properties.Add(new PropertyModel { Key = "Provider", Value = "APH", Type = PropertyType.String });
                            airportcarpark.Properties.Add(new PropertyModel { Key = "ProductCode", Value = carpark.ProductCode, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });
                            airportcarpark.Properties.Add(new PropertyModel { Key = "ProductName", Value = carpark.ProductName, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });
                            airportcarpark.Properties.Add(new PropertyModel { Key = "Terminals", Value = carpark.Terminals, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });

                            airportwherecarparkwillbeadded.BookingEntities.Add(airportcarpark);
                        }

                        bool result = _rootBookingEntityManager.Update(airportwherecarparkwillbeadded);
                    }
                
                }

            }
            else
            {

                foreach (var updateairport in airports.AirportList)
                {
                    foreach (var carpark in updateairport.CarParks)
                    {
                        var airportcarpark = await _bookingEntityManager.GetByCode(carpark.Code);

                        airportcarpark.Code = carpark.Code;
                        airportcarpark.Name = carpark.Name;
                        airportcarpark.ModifiedBy = "System";
                        airportcarpark.CreatedBy = "System";
                        airportcarpark.Modified = DateTime.Now;
                        airportcarpark.Created = DateTime.Now;

                        foreach (var property in airportcarpark.Properties)
                        {
                            switch (property.Key)
                            {
                                case "Direcctions": property.Value = carpark.Details; break;
                                case "Provider": property.Value = carpark.Details; break;
                                case "ProductCode": property.Value = carpark.ProductCode; break;
                                case "ProductName": property.Value = carpark.ProductCode; break;
                                case "Terminals": property.Value = carpark.ProductCode; break;
                                default:
                                    break;
                            }
                        }
                        _bookingEntityManager.Update(airportcarpark);
                    }

                }
            }
        }

        public ACP.Business.APIs.APH.Models.Availability.API_Reply CarParkAvailability(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<ACP.Business.APIs.APH.Models.Availability.API_Reply>(result);

            return reply;
        }

        public API_Reply CarParkInformation(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);

            return reply;
        }

        public ACP.Business.APIs.APH.Models.Details.API_Reply CarParkInformationDetails(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<ACP.Business.APIs.APH.Models.Details.API_Reply>(result);
            return reply;
        }

        public string PostFormData(string data)
        {
            string result = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://test.parking-quote.co.uk/xmlapi/aphxml.ASP");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //string postData = "Request=<API_Request System=\"APH\" Version=\"1.0\" Product=\"CarPark\" Customer=\"X\" Session=\"000000003\" RequestCode=\"11\"> <Agent> <ABTANumber>WA789</ABTANumber> <Password></Password> <Initials>thecarparksuper</Initials> </Agent> <Itinerary>  <ArrivalDate>20Nov15</ArrivalDate> <DepartDate>27Nov15</DepartDate> <ArrivalTime>0600</ArrivalTime> <DepartTime>1800</DepartTime> <Location>LGW</Location> <Terminals>ALL</Terminals></Itinerary></API_Request>";
            byte[] bytes = Encoding.UTF8.GetBytes("Request="+data);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
                        
            return result;
        }

        private string Serialize<T>(T objecttobeserialize)
        {
            XmlSerializer serialize = new XmlSerializer(objecttobeserialize.GetType());
            using(StringWriter textWriter = new StringWriter())
            {
                serialize.Serialize(textWriter, objecttobeserialize);
                return textWriter.ToString();
            }
        }

        private T Deserialize<T>(string stringtobedeserialized)
        {
            object result;

            try
            {
                XmlSerializer serialize = new XmlSerializer(typeof(T));
                TextReader reader = new StringReader(stringtobedeserialized);
                {
                    result = serialize.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return (T)result;
        }


        public API_Reply CarPrice(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);

            return reply;
        }
    }
}
