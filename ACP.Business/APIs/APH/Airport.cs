﻿using System.Collections.Generic;

namespace ACP.Business.APIs.APH
{
    internal class Airport
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

    internal class CarPark
    {
        public string Code { get; set; }
        public string Commision { get; internal set; }
        public string Name { get; set; }
        public string ProductName { get; internal set; }
        public string Terminals { get; internal set; }
    }
}