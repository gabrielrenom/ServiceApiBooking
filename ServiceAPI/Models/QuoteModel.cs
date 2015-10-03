using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Models
{
    class QuoteViewModel
    {    
        public DateTime Dropoff { get; set; }
        public DateTime Pickup { get; set; }
        public LocationVidewModel PickupLocation { get; set; }
        public LocationVidewModel DropoffLocation { get; set; }       
        public double Price { get; set; }
    }


    public class LocationVidewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public AddressViewModel Address
        {
            get; set;
        }
    }
}
