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
    [RoutePrefix("booking")]
    public class BookingController : Controller
    {
        private IBookingService _bookingservice;
        private IPayPalService _paypalservice;
        private IUserService _userService;

        public BookingController(IBookingService bookingservice, IPayPalService paypalService, IUserService userService)
        {
            _bookingservice = bookingservice;
            _paypalservice = paypalService;
            _userService = userService;
        }

        //[HttpGet]
        //[Route("")]
        //public ActionResult GoToSearch()
        //{
        //    return RedirectToAction("index","carparks");
        //}

        // GET: Booking
        [HttpGet]
        //[Route("{airportId:int}/{discount}/{dropoffDate}/{returnDate}/{price:decimal}/{bookingentityId:int}/{carparkName}/{description}")]
        public ActionResult Index(int? airportId, string discount, string dropoffDate, string returnDate, decimal? price, int? bookingentityId, string carparkName, string description)
        {

            if (!airportId.HasValue || !price.HasValue || !bookingentityId.HasValue)
                return RedirectToAction("index", "carparks");

            BookingGuestViewModel model = new BookingGuestViewModel();

            model.Price = (decimal)price;
            model.AirportId = (int)airportId;
            model.DropOffDate = Convert.ToDateTime(dropoffDate);
            model.ReturnDate = Convert.ToDateTime(returnDate);
            model.CouponDiscount = discount;
            model.BookingFee = 0;
            model.BookEntityId = (int)bookingentityId;
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
            if (Convert.ToDateTime(model.DropOffDate) > Convert.ToDateTime(model.ReturnDate))
            {
                ModelState.AddModelError("DropOffDate", "The date supplied is older than the Return Date");
            }

            if (Convert.ToDateTime(model.DropOffDate) < DateTime.Today)
            {
                ModelState.AddModelError("DropOffDate", "The date supplied can be in an earlier date");
            }

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

        private BookingConfirmationView ToBookingConfirmationView(BookingGuestViewModel model, string reference)
        {
            return new BookingConfirmationView
            {
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
                Registration = model.Registration,
                ReturnDate = model.ReturnDate,
                TerminalIn = model.TerminalIn,
                TerminalOut = model.TerminalOut,
                Title = model.Title
            };
        }

        private BookingGuestViewModel ToBookingConfirmationFromUserView(UserModel model, BookingGuestViewModel guestmodel)
        {
            BookingGuestViewModel viewmodel = new BookingGuestViewModel();

            if (model.Bookings != null)
            {
                viewmodel.CarModel = model.Bookings.FirstOrDefault().Car.Model;
                viewmodel.Color = model.Bookings.FirstOrDefault().Car.Colour;
                viewmodel.Make = model.Bookings.FirstOrDefault().Car.Make;
                viewmodel.Registration = model.Bookings.FirstOrDefault().Car.Registration;
            }            
            viewmodel.CarParkName = guestmodel.CarParkName;            
            viewmodel.Description = guestmodel.Description;
            viewmodel.DropOffDate = guestmodel.DropOffDate;
            viewmodel.FirstName = model.FirstName;
            viewmodel.InboundFlight = guestmodel.InboundFlight;
            viewmodel.IsAddCarWashService = guestmodel.IsAddCarWashService;
            viewmodel.IsAddSMSConfirmation = guestmodel.IsAddSMSConfirmation;
            viewmodel.IsCancelationCover = guestmodel.IsCancelationCover;
            viewmodel.LastName = model.LastName;            
            viewmodel.Mobile = model.PhoneNumber;
            viewmodel.OutboundFlight = guestmodel.OutboundFlight;            
            viewmodel.ReturnDate = guestmodel.ReturnDate;
            viewmodel.UserEmail = guestmodel.UserEmail;
            viewmodel.Email = guestmodel.UserEmail;
            viewmodel.ConfirmEmail = guestmodel.UserEmail;
            viewmodel.IsUser = true;
            return viewmodel;
        }


        private PayPalModel ToPayPalModel(BookingGuestViewModel model)
        {
            var paypal = new PayPalModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Currency = "GBP",
                Description = model.Description,
                Name = model.CardName,
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
            if (model.CreditCardType == "0")
                paypal.Type = "visa";
            else if (model.CreditCardType == "1")
                paypal.Type = "mastercard";
            else if (model.CreditCardType == "2")
                paypal.Type = "americanexpress";

            return paypal;
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
                Modified = DateTime.Now,
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
        public async Task<ActionResult> Login(BookingGuestViewModel model)
        {
            if (model.UserEmail.Length > 0 && model.Password.Length > 0)
            {
                var customerDetails = await _userService.GetUserDetails(model.UserEmail, model.Password);
                if (customerDetails!=null)
                    return View("Index",ToBookingConfirmationFromUserView(customerDetails, model));                
            }            
            model.Error = "User or Password incorrect";
            model.ErrorType = 1;
            
            return View("Index",model);
        }
    }
}