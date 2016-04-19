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
            Pricing = new List<ItemPriceModel>();
        }

        public DateTime Dropoff { get; set; }
        public DateTime Pickup { get; set; }
        public LocationModel PickupLocation { get; set; }
        public LocationModel DropoffLocation { get; set; }
        public List<BookingServiceModel> BookingServices;
        public List<BookingItemModel> BookingItems;
        public List<ItemPriceModel> Pricing;
        public decimal Price;
    }

    public class ItemPriceModel 
    {
        public BookingPricingModel PriceModel {get;set;}
        public decimal Price { get; set; }
    }
}
