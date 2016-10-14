using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using ACP.Business.Models;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        private IBookingService _bookingservice;
        private IPayPalService _paypalservice;

        public BookingController(IBookingService bookingservice, IPayPalService paypalService)
        {
            _bookingservice = bookingservice;
            _paypalservice = paypalService;
        }

        // GET: Booking
        [HttpGet]
        //public ActionResult Index(QuoteModelView quotemodel)
        public ActionResult Index(int airportId, string discount, string dropoffDate , string returnDate , decimal price, int bookingentityId, string carparkName, string description)       
        {
            BookingGuestViewModel model = new BookingGuestViewModel();

            model.Price = price;
            model.AirportId = airportId;
            model.DropOffDate = Convert.ToDateTime(dropoffDate);
            model.ReturnDate = Convert.ToDateTime(returnDate);
            model.CouponDiscount = discount;
            model.BookingFee = 0;
            model.BookEntityId = bookingentityId;
            model.Description = description;
            model.CarParkName = carparkName;
               
            return View(model);
        }

        [HttpGet]
        [Route("booking/completed")]
        public ActionResult PaymentCompleted(BookingConfirmationView model)
        {

            //if (ModelState.IsValid)
            //{

            //}

            return View(model);
        }



        // GET: Booking
        [HttpPost]
        public async Task<ActionResult> Index(BookingGuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookingservice.AddAsync(ToBookingModel(model));
                var paymentresult = _paypalservice.PaymentWithCreditCard(ToPayPalModel(model), Configuration.GetAPIContext());
                if (paymentresult == "approved")
                {
                    var havebeenpaid = await _bookingservice.Paid(result.Id);
                    if (havebeenpaid) return RedirectToAction("completed", ToBookingConfirmationView(model, result.BookingReference));                                                            
                }
                model.Error = paymentresult;
            }            

            return View(model);
        }

        private BookingConfirmationView ToBookingConfirmationView(BookingGuestViewModel model,string reference)
        {
            return new BookingConfirmationView {
                 BookingReference = reference,
                 CarModel = model.CarModel,
                 CarParkName = model.CarParkName,
                 Color = model.Color,
                 Description = model.Description,
                 DropOffDate = model.DropOffDate,
                 FirstName = model.FirstName,
                 InboundFlight = model.InboundFlight,
                 IsAddCarWashService = model.IsAddCarWashService,
                 IsAddSMSConfirmation = model.IsAddSMSConfirmation,
                 IsCancelationCover = model.IsCancelationCover,
                 LastName = model.LastName,
                 Make = model.Make,
                 Mobile = model.Mobile,
                 OutboundFlight = model.OutboundFlight,
                 Passangers = model.Passangers,
                 Price = model.Price,
                 Registration =model.Registration,
                 ReturnDate = model.ReturnDate,
                 TerminalIn = model.TerminalIn,
                 TerminalOut = model.TerminalOut,
                 Title = model.Title
            };
        }

        private PayPalModel ToPayPalModel(BookingGuestViewModel model)
        {
            return new PayPalModel {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Currency = "GBP",
                Description = model.Description,
                Name = model.CardName,
                Type = model.CreditCardType,
                Total = Convert.ToString(model.Price + model.BookingFee),
                Number = model.CardNumber,
                BillingAddressCity = model.City,
                BillingAddressCountry = "UK",
                BillingAddressLine1 = model.Address,
                BillingAddressPostCode = model.Postcode,
                BillingAddressState = "",
                Price = model.Price.ToString(),
                Quantity = 1,
                ExpireYear = model.ExpiryYear,
                ExpireMonth = Convert.ToInt32(model.ExpiryMonth).ToString(),
                CVV2 = model.CVV,
                SKU = "123",
                InvoiceNumber = new Random().Next(1000, 10000).ToString()

            };
        }

        private BookingModel ToBookingModel(BookingGuestViewModel model)
        {
            //## The client books but it is flaged to false,
            //## Only when he pays it is flagged to true
            BookingModel bookingModel = new BookingModel();

            bookingModel.User = new UserModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Title.ToLower().Contains('s') ? ACP.Business.Enums.Gender.Female : ACP.Business.Enums.Gender.Male,
                DOB = DateTime.Now,
                
            };

            bookingModel.Car = new CarModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Email,
                Model = model.CarModel,
                Make = model.Make,
                Registration = model.Registration,
                Colour = model.Color,
                User = new UserModel
                {
                    Address = new AddressModel
                    {
                        Created = DateTime.Now,
                        CreatedBy = model.Email,
                        Modified = DateTime.Now,
                        ModifiedBy = model.Email,
                    },
                    Created = DateTime.Now,
                    CreatedBy = model.Email,
                    Modified = DateTime.Now,
                    ModifiedBy = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Title.ToLower().Contains('s') ? ACP.Business.Enums.Gender.Female : ACP.Business.Enums.Gender.Male
                }
            };

            bookingModel.TravelDetails = new TravelDetailsModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Email,
                OutboundDate = model.DropOffDate,
                ReturnDate = model.ReturnDate,
                OutboundFlight = model.OutboundFlight,
                OutboundTerminal = model.TerminalOut,
                ReturnboundTerminal = model.TerminalIn,
                ReturnFlight = model.InboundFlight,                
            };

            bookingModel.Cost = Convert.ToDouble(model.Price);
            bookingModel.StartDate = model.DropOffDate;
            bookingModel.EndDate = model.ReturnDate;
            bookingModel.Status = ACP.Business.Enums.StatusType.Processing;
            bookingModel.Modified = DateTime.Now;
            bookingModel.Extras = new List<ExtraModel>
            {
                new ExtraModel {
                    Created = DateTime.Now,
                    CreatedBy = model.Email,
                    Modified = DateTime.Now,
                    ModifiedBy = model.Email,
                    BookingEntityId = model.BookEntityId,
                    Name = "Parking",
                    Price = model.Price                    
                }
            };

            bookingModel.Customer = new CustomerModel
            {
                  Email = model.Email,
                  Initials = model.Title,
                  Forename = model.FirstName,
                  Surname = model.LastName,
                  Mobile = model.Mobile,
                  Created = DateTime.Now,
                  Modified =DateTime.Now,
                  Address = new AddressModel
                  {
                      Created = DateTime.Now,
                      CreatedBy = model.Email,
                      Modified = DateTime.Now,
                      ModifiedBy = model.Email,                      
                  }
            };

            bookingModel.Status = ACP.Business.Enums.StatusType.Processing;
            return bookingModel;
        }

        [HttpPost]
        public ActionResult Login(BookingGuestViewModel model)
        {

            if (ModelState.IsValid)
            {

            }   

            return View(model);
        }
    }
}