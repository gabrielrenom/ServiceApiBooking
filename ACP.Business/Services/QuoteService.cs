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
                quote.Pricing.Add(new ItemPriceModel
                {
                    PriceModel = item
                });
            }

            return quoteresult;
        }

        public async Task<QuoteModel> GetQuoteWithPrice(Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            var list = _bookingPricingManager.GetAllPricesByPickLocationAndDropLocation(quote.PickupLocation.Name, quote.DropoffLocation.Name, quote.Pickup, quote.Dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

            foreach (var item in list)
            {
                double days = Math.Round((quote.Dropoff - quote.Pickup).TotalDays);               
                quote.Pricing.Add(new ItemPriceModel
                {
                    Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice,
                    PriceModel = item
                });             
            }

            return quoteresult;
        }

        public async Task<QuoteModel> GetQuoteWithPriceAndReviews(Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            var list = await _bookingPricingManager.GetAllPricesAndReviewsByPickLocationAndDropLocationById(quote.PickupLocation.Id, quote.DropoffLocation.Id, quote.Dropoff, quote.Pickup);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

            foreach (var item in list)
            {
                double days = Math.Round((quote.Pickup-quote.Dropoff).TotalDays);
                quote.Pricing.Add(new ItemPriceModel
                {
                    Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice,
                    PriceModel = item,
                    
                });
            }

            return quoteresult;
        }


        public async Task<QuoteModel> GetQuoteWithPriceByBookingEntityId(int Id, Models.QuoteModel quote)
        {
            QuoteModel quoteresult = quote;

            if (quote.Pickup < quote.Dropoff)
            {
                try
                {
                    var list = _bookingPricingManager.GetAllPricesByBookEntity(Id, quote.Pickup, quote.Dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

                    foreach (var item in list)
                    {
                        double days = Math.Round((quote.Dropoff - quote.Pickup).TotalDays);
                        quote.Pricing.Add(new ItemPriceModel
                        {
                            Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice,
                            PriceModel = item
                        });
                        quote.Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice;
                    }
                }
                catch (Exception ex)
                {
                    string e = ex.ToString();
                }
            }
            return quoteresult;
        }

        public async Task<QuoteModel> GetQuoteWithPriceByBookingEntityId(int bookingEntityId, DateTime pickup, DateTime dropoff)
        {
            QuoteModel quoteresult = new QuoteModel
            {
                 Dropoff = dropoff,
                  Pickup = pickup, 
                  Pricing = new List<ItemPriceModel>()
            };

            if (pickup < dropoff)
            {
                try
                {
                    var list = _bookingPricingManager.GetAllPricesByBookEntity(bookingEntityId, pickup, dropoff);//_bookingPricingManager.GetAllPrices().Where(x => quote.Pickup>x.Start  && quote.Dropoff<x.End ).ToList();

                    foreach (var item in list)
                    {
                        double days = Math.Round((dropoff - pickup).TotalDays);
                        quoteresult.PickupLocation = new LocationModel { Name = item.BookingEntity.Name, Address = item.BookingEntity.Address, Id = item.BookingEntity.Id };
                        quoteresult.Pricing.Add(new ItemPriceModel
                        {
                            Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice,
                            PriceModel = item
                        });
                       
                        if (days > 28)
                        {
                            quoteresult.Price += (item.DayPrices.Where(x => x.Day == 29).FirstOrDefault().Dayprice) * Convert.ToDecimal((days - 28));
                        }
                        else
                        {
                            quoteresult.Price = item.DayPrices.Where(x => x.Day == days).FirstOrDefault().Dayprice;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string e = ex.ToString();
                }
            }
            return quoteresult;
        }
    }
}
