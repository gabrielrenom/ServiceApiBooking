﻿using ACP.Business.APIs.APH.Models;
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
        private List<Airport> _airportList = new List<Airport>();

        public APH(IRootBookingEntityManager rootBookingEntityManager, IBookingEntityManager bookingEntityManager)
        {
            _bookingEntityManager = bookingEntityManager;
            _rootBookingEntityManager = rootBookingEntityManager;

            #region [ AIRPORTS ]
            _airportList.Add(new Airport("Aberdeen", "ABZ"));
            _airportList.Add(new Airport("Belfast International", "BFS"));
            _airportList.Add(new Airport("Birmingham", "BT1"));
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
            _airportList.Add(new Airport("London Gatwick", "N"));
            _airportList.Add(new Airport("London Gatwick", "S"));
            _airportList.Add(new Airport("London Heathrow", "HT1"));
            _airportList.Add(new Airport("London Heathrow", "HT2"));
            _airportList.Add(new Airport("London Heathrow", "HT3"));
            _airportList.Add(new Airport("London Heathrow", "HT4"));
            _airportList.Add(new Airport("London Heathrow", "HT5"));
            _airportList.Add(new Airport("London Luton", "LTN"));
            _airportList.Add(new Airport("London Southend", "SOU"));
            _airportList.Add(new Airport("London Stansted", "STN"));
            _airportList.Add(new Airport("Liverpool", "LPL"));
            _airportList.Add(new Airport("Prestwick", "PIK"));
            _airportList.Add(new Airport("Manchester", "MT1"));
            _airportList.Add(new Airport("Manchester", "MT2"));
            _airportList.Add(new Airport("Manchester", "MT3"));
            _airportList.Add(new Airport("Newcastle", "NCL"));
            _airportList.Add(new Airport("Southampton Airport", "SOU"));
            _airportList.Add(new Airport("Southampton Port", "SOP"));
            #endregion
        }

        private void GetCarParkCodes(ref List<Airport> airports)
        {
            foreach (var item in airports)
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

                foreach (var carpark in response.CarPark)
                {
                    CarPark carparkitem = new CarPark();
                    carparkitem.Code = carpark.CarParkCode;
                    carparkitem.Name = carpark.CarParkName;
                    carparkitem.ProductName = carpark.productName;
                    carparkitem.Terminals = carpark.Terminals;
                    carparkitem.Commision = carpark.Commission;

                    item.CarParks.Add(carparkitem);
                }
            }
        }

        public bool FillBookingEnties()
        {
            bool result = false;

            GetCarParkCodes(ref _airportList);

            foreach (var airport in _airportList)
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
                            CarParkCode = "LGW1",//carpark.Code,
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

                    API_Reply reply = CarParkInformationDetails(request);

                    if (reply != null)
                    foreach (var item in reply.Info.Details)
                    {
                            carpark.Details += item; ;
                    }
                }
            }


            AddCarParksInDatabase(_airportList);

            return result;
        }

        public async Task AddCarParksInDatabase (List<Airport> airports)
        {
            var allairports =  _rootBookingEntityManager.GetAll();

            DateTime? date = DateTime.Now;

            foreach (var item in allairports)
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

            foreach (var airport in airports)
            {
                var airportwherecarparkwillbeadded = await _rootBookingEntityManager.GetByName(airport.Name);

                if (airportwherecarparkwillbeadded != null)
                {
                    foreach (var carpark in airport.CarParks)
                    {
                        BookingEntityModel airportcarpark = new BookingEntityModel();

                        airportcarpark.Name = carpark.Name;
                        airportcarpark.ModifiedBy = "System";
                        airportcarpark.CreatedBy = "System";
                        airportcarpark.Modified = DateTime.Now;
                        airportcarpark.Created = DateTime.Now;

                        airportcarpark.Properties = new Collection<PropertyModel>();

                        airportcarpark.Properties.Add(new PropertyModel { Key = "Code", Value = carpark.Code, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Direcctions", Value = carpark.Details, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System", });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Provider", Value = "APH", Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Name", Value = carpark.Name, Type = PropertyType.String, Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "System", ModifiedBy = "System" });


                        airportwherecarparkwillbeadded.BookingEntities.Add(airportcarpark);

                        bool result = _rootBookingEntityManager.Update(airportwherecarparkwillbeadded);
                    }
                }
            }
        }

        public API_Reply CarParkAvailability(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);

            return reply;
        }

        public API_Reply CarParkInformation(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);

            return reply;
        }

        public API_Reply CarParkInformationDetails(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);
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
