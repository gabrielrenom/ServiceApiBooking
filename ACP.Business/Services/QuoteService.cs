using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class QuoteService: IQuoteService
    {
        private IBookingManager _bookingManager;
        private IBookingPricingManager _bookingPricingManager;

        public QuoteService(IBookingManager bookingManager, IBookingPricingManager bookingPricingManager)
        {
            _bookingManager = bookingManager;
            _bookingPricingManager = bookingPricingManager;
        }

        public async Task<Models.BookingModel> Add(Models.BookingModel service)
        {
            throw new NotImplementedException();
        }

        public async Task<QuoteModel> GetQuote(Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            var list = _bookingPricingManager.GetAllPricesByPickLocationAndDropLocation(quote.PickupLocation.Name, quote.DropoffLocation.Name, quote.Pickup, quote.Dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

            foreach (var item in list)
            {
                quote.BookingPricingItems.Add(item);
            }

            return quoteresult;
        }

        public async Task<QuoteModel> GetQuoteWithPrice(Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            var list = _bookingPricingManager.GetAllPricesByPickLocationAndDropLocation(quote.PickupLocation.Name, quote.DropoffLocation.Name, quote.Pickup, quote.Dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

            foreach (var item in list)
            {
                quote.Price = item.DayPrices.Where(x => x.Day == (quote.Pickup - quote.Dropoff).TotalDays).FirstOrDefault().Dayprice;
                quote.BookingPricingItems.Add(item);
            }

            return quoteresult;
        }

        public async Task<QuoteModel> GetQuoteWithPriceByBookingEntityId(int Id, Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            var list = _bookingPricingManager.GetAllPricesByBookEntity(Id, quote.Pickup, quote.Dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

            foreach (var item in list)
            {
                quote.Price = item.DayPrices.Where(x => x.Day == (quote.Pickup - quote.Dropoff).TotalDays).FirstOrDefault().Dayprice;
                quote.BookingPricingItems.Add(item);
            }

            return quoteresult;
        }
    }
}
