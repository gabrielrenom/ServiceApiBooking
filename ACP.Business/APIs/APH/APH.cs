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

        public APH(IRootBookingEntityManager rootBookingEntityManager, IBookingEntityManager bookingEntityManager)
        {
            _bookingEntityManager = bookingEntityManager;
            _rootBookingEntityManager = rootBookingEntityManager;
        }

        public bool FillBookingEnties()
        {
            bool result = false;

            var rootentities = _rootBookingEntityManager.GetAll();

            foreach (var airport in rootentities)
            {
                //## Check if the airport has a APH car park
                if (airport.BookingEntities
                    .Where(x=>x.Properties
                        .Where(y => y.Key == "Provider" && y.Value.ToString() == "APH")
                        .Count() > 0)
                            .Count()>0)
                    {
                        foreach (var carpark in airport.BookingEntities)
                        {
                            //## If so, we delete teh car park to fill it again.
                            if (carpark.Properties.Where(y => y.Key == "Provider" && y.Value.ToString() == "APH").Count() > 0)
                            {
                                _bookingEntityManager.DeleteById(carpark.Id);
                            }
                        }    
                    }
                //## START Adding a new carparks
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
                        ArrivalDate = DateTime.Now.AddDays(3).ToString("dMMMyy"),
                        DepartDate = DateTime.Now.AddDays(4).ToString("dMMMyy"),
                        ArrivalTime = "0600",
                        DepartTime = "1800",
                        Location = airport.Name,
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

                if (response.Result == "OK") 
                {
                    foreach (var item in response.CarPark)
                    {
                        API_Request informationrequest = new API_Request
                        {
                            Agent = new Agent
                            {
                                ABTANumber = "WA789",
                                Initials = "thecarparksuper",
                                Password = ""
                            },
                            Itinerary = new Itinerary
                            {
                                ArrivalDate = DateTime.Now.AddDays(3).ToString("dMMMyy"),
                                CarParkCode = item.CarParkCode
                            },
                            System = "APH",
                            Version = "1.0",
                            Product = "CarPark",
                            Customer = "X",
                            Session = "000000004",
                            RequestCode = "6",
                            Request = new Request { RequestType = "1" }
                        };

                        //Ac
                        var information = this.CarParkInformation(informationrequest);
                        if (information.Result == "OK")
                        {
                            airport.BookingEntities.Add(new BookingEntityModel
                            {
                                Name = item.CarParkName,
                                Prices = new Collection<BookingPricingModel>(),
                                Service = new Collection<BookingServiceModel>(),
                                Zone = new Collection<ZoneModel>(),
                                //Availability = new Collection<AvailabilityModel>(),
                                Properties = new Collection<PropertyModel> { 
                                new PropertyModel{Key="Provider", Value="APH"},
                                new PropertyModel{Key="CarParkCode", Value=item.CarParkCode}                                
                            },
                                Address = new AddressModel { }
                            });
                        }
                    }
                }

                //## ?? It does
                _rootBookingEntityManager.Update(airport);
                //## END
            }
            
            return result;
        }

        public API_Reply CarParkAvailability(API_Request request)
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


        public API_Reply CarParkInformation(API_Request request)
        {
            string result = this.PostFormData(this.Serialize(request));
            var reply = this.Deserialize<API_Reply>(result);

            return reply;
        }        
    }
}
