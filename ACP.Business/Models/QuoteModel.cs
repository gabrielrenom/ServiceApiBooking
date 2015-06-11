using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class QuoteModel
    {
        public QuoteModel()
        {
            BookingServices = new List<BookingServiceModel>();
            BookingPricingItems = new List<BookingPricingModel>();
        }

        public DateTime Dropoff { get; set; }
        public DateTime Pickup { get; set; }
        public LocationModel PickupLocation { get; set; }
        public LocationModel DropoffLocation { get; set; }
        public List<BookingServiceModel> BookingServices;
        public List<BookingItemModel> BookingItems;
        public List<BookingPricingModel> BookingPricingItems;

    }
}
