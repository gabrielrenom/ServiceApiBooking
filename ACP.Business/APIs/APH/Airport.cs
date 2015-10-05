using System.Collections.Generic;

namespace ACP.Business.APIs.APH
{
    public class APHAirports
    {
        public List<Airport> AirportList { get; set; }
        public APHAirports()
        {
            AirportList = new List<Airport>();
        }
    }
    public class Airport
    {
        private string _Name;
        private string _Code;
        public List<CarPark> CarParks;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
         

        public Airport(string name, string code)
        {
            _Name = name;
            _Code = code;
            CarParks = new List<CarPark>();
        }
    }

    public class CarPark
    {
        public string Code { get; set; }
        public byte Commision { get; internal set; }
        public string Details { get; internal set; }
        public string Name { get; set; }
        public string ProductName { get; internal set; }
        public string Terminals { get; internal set; }
        public string ProductCode { get; internal set; }
    }
}