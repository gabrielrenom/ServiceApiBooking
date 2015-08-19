
using ACP.Business.APIs.PP.Models.Airports;
using System;
using System.Collections.Generic;
using System.IO;
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

        public string Url
        {
            get { return _purple_parking_url; }
            set { _purple_parking_url = value; }
        }

        public async Task<ACP.Business.APIs.PP.Models.Airports.response> GetAirports()
        {
            return await GetREST<ACP.Business.APIs.PP.Models.Airports.response>("/r2/rest/xml/GetAirports?params=<request apiKey='G7yNMYkSdX820B1xTM8auQZrlL6yG8o'></request>");
        }

        public List<responseAirportCarPark> GetCarParks()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetREST<T>(string parameters)
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
