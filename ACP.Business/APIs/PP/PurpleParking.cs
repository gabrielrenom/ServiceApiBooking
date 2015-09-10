
using ACP.Business.APIs.PP.Models.Airports;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACP.Business.APIs.PP
{
    public class PurpleParking : IPurpleParking
    {   
        private static string _purple_parking_url = @"http://ws.purple-parking.com/";
        private IRootBookingEntityService _airportService;
        private IStatusService _statusService;

        public PurpleParking(IRootBookingEntityService airportService, IStatusService statusService)
        {
            _airportService = airportService;
            _statusService = statusService;
        }

        public string Url
        {
            get { return _purple_parking_url; }
            set { _purple_parking_url = value; }
        }

        public async Task<bool> FillAirports()
        {
            bool result = false;

            try
            {
                var airports = await GetAirports();
                
                StatusModel status = await _statusService.GetByName("Active");
                if (status == null) status = await _statusService.Add(new StatusModel {  Name="Active", CreatedBy="System", ModifiedBy="System", Modified =DateTime.Now, Created=DateTime.Now });

                foreach (var item in airports)
                {
                    RootBookingEntityModel airport = new RootBookingEntityModel();
                    airport.Name = item.name;
                    airport.Created = DateTime.Now;
                    airport.Modified = DateTime.Now;
                    airport.ModifiedBy = "System";
                    airport.CreatedBy = "System";
                    airport.Properties = new Collection<RootBookingPropertyModel>();
                    airport.Properties.Add(new RootBookingPropertyModel { Key = "Code", Value = item.code, PropertyType = ACP.Business.Enums.RootBookingPropertyType.String });
                    
                    foreach (var terminal in item.terminals)
                    {
                        new RootBookingPropertyModel { Key = "Terminal", Value = terminal, PropertyType = ACP.Business.Enums.RootBookingPropertyType.String };
                    }
                    airport.BookingEntities = item.carParks != null ? new Collection<BookingEntityModel>() : null;
                   
                    foreach (var carpark in item.carParks)
                    {
                        BookingEntityModel airportcarpark = new BookingEntityModel();
                        if (carpark.address != null)
                        {
                            string[] fields = carpark.address.Split(',');
                            airportcarpark.Address = new AddressModel();
                            airportcarpark.Address.Postcode = fields[fields.Length - 1] ?? fields[fields.Length - 1];
                            airportcarpark.Address.City = fields[fields.Length - 2] ?? fields[fields.Length - 2];
                            airportcarpark.Address.Address2 = fields[fields.Length - 3] ?? fields[fields.Length - 3];
                            airportcarpark.Address.Address2 = fields[fields.Length - 3] ?? airportcarpark.Address.Address2 + ", " + fields[fields.Length - 3];                            
                            airportcarpark.Address.Address1 = fields[fields.Length - 4] ?? fields[fields.Length - 4];                            
                        }
                        
                        airportcarpark.Properties = new Collection<PropertyModel>();
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Arrival Procedure", Value = carpark.arrivalProcedure, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Departure Procedure", Value = carpark.departureProcedure, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Description", Value = carpark.description, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Code", Value = carpark.code, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Cancellations", Value = carpark.cancellations, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Direcctions", Value = carpark.directions, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Distance To Airport", Value = carpark.distanceToAirport, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Helpline", Value = carpark.helpline, Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Lead Time Hours", Value = carpark.leadTimeHours.ToString(), Type = PropertyType.Int });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Transfer Frequency", Value = carpark.transferFrequency.ToString(), Type = PropertyType.String });
                        airportcarpark.Properties.Add(new PropertyModel { Key = "Provider", Value = "PP", Type = PropertyType.String });

                        airport.Status = status;
                        airport.StatusId = status.Id;

                        airport.BookingEntities.Add(airportcarpark);

                    }
                    await _airportService.Add(airport);
                }

                result = true;
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);                
            }           
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);                
            }

            return result;
        }
   
        public async Task<List<ACP.Business.APIs.PP.Models.Airports.responseAirport>> GetAirports()
        {
            ACP.Business.APIs.PP.Models.Airports.response response = await GetREST<ACP.Business.APIs.PP.Models.Airports.response>("/r2/rest/xml/GetAirports?params=<request apiKey='G7yNMYkSdX820B1xTM8auQZrlL6yG8o'></request>");

            return response.airports.ToList();
        }

        public async Task<List<ACP.Business.APIs.PP.Models.Airports.responseAirportCarPark>> GetCarParks()
        {
            ACP.Business.APIs.PP.Models.Airports.response response = await GetREST<ACP.Business.APIs.PP.Models.Airports.response>("/r2/rest/xml/GetAirports?params=<request apiKey='G7yNMYkSdX820B1xTM8auQZrlL6yG8o'></request>");

            List<ACP.Business.APIs.PP.Models.Airports.responseAirportCarPark> carparks = new List<ACP.Business.APIs.PP.Models.Airports.responseAirportCarPark>();

            foreach (var airport in response.airports)
            {
                foreach (var carpark in airport.carParks)
                {
                    carparks.Add(carpark);
                }                
            }
           
            return carparks;
        }



        protected async Task<T> GetREST<T>(string parameters)
        {
            object product = new object();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_purple_parking_url);
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    string product2 = await response.Content.ReadAsStringAsync();

                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(product2));
                    T resultingMessage = (T)serializer.Deserialize(memStream);
                    product = resultingMessage;                    
                }
            }

            return (T)Convert.ChangeType(product, typeof(T));
        }
    }
}
