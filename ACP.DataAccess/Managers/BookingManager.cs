using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class BookingManager : BaseACPManager<BookingModel, Booking>, IBookingManager
    {
        public BookingManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override Task<BookingModel> AddAsync(BookingModel domainModel)
        {
            return base.AddAsync(domainModel);
        }

        public async Task<bool> Paid(int id)
        {
            try
            {
                var result = await this.GetByIdAsync(id);

                result.Status = Business.Enums.StatusType.Paid;

                await this.UpdateAsync(result);

                return true;
           }
           catch (Exception ex)
           {
                return false;
           }


        }

        public async override Task<BookingModel> GetByIdAsync(int id)
        {
     
            return await GetByIdIncludingAsync(id,
                x => x.Car,
                x => x.Customer,
                x => x.Customer.Address,
                x => x.Extras,
                x => x.TravelDetails,
                x => x.Payments,
                x => x.Payments.Select(y => y.CreditCard),
                x => x.Payments.Select(y => y.BankAccount),
                x => x.Payments.Select(y => y.Currency)
                );
        }

        public  override async Task<bool> DeleteByIdAsync(int id)
        {
            var record =  await Repository.GetSingleAsync<Booking>(x => x.Id > 0
                //x => x.Car,
                //x => x.Customer,
                //x => x.Customer.Address,
                //x => x.Extras
                //x => x.TravelDetails
                //x => x.Payments
                //x => x.Payments.Select(y => y.CreditCard),
                //x => x.Payments.Select(y => y.BankAccount),
                //x => x.Payments.Select(y => y.Currency)
                );
            //if (record.Car != null)
            //{
            //    Repository.Delete<Car>(record.Car);
            //}

            //if (record.Customer != null)
            //{
            //    Repository.Delete<Customer>(record.Customer);
            //}

            if (record.TravelDetails != null)
            {
                Repository.Delete<TravelDetails>(record.TravelDetails);
            }

            if (record.Payments != null)
            {
                Repository.DeleteMany<Payment>(record.Payments.ToArray());
            }

            //if (record.Extras!=null )
            //{
            //    Repository.DeleteMany<Extra>(record.Extras.ToArray());
            //}
            if (record.BookingLink!=null )
            {
                Repository.DeleteMany<BookingLink>(record.BookingLink.ToArray());
            }           

            Repository.Delete<Booking>(record);
            Repository.Commit();

            return true;
        }

        public IEnumerable<BookingModel> GetAll()
        {
            return GetListIncluding(x => x.Id > 0, 
                x => x.Car, 
                x=>x.Customer, 
                x=>x.Customer.Address,
                x=>x.Extras, 
                x=>x.TravelDetails, 
                x=>x.Payments, 
                x=>x.Payments.Select(y=>y.CreditCard),
                x => x.Payments.Select(y => y.BankAccount),
                x => x.Payments.Select(y => y.Currency)      
                );
        }

        public async override Task<bool> UpdateAsync(BookingModel domainModel)
        {
            try
            {
                //var dataModel = Repository.GetSingle<Booking>(x => x.Id == domainModel.Id,
                //    x => x.Car,
                //    x => x.Customer,
                //    x => x.Customer.Address,
                //    x => x.Extras,
                //    x => x.TravelDetails,
                //    x => x.Payments,
                //    x => x.Payments.Select(y => y.CreditCard),
                //    x => x.Payments.Select(y => y.BankAccount),
                //    x => x.Payments.Select(y => y.Currency)
                //    );

                var dataModel = await Repository.GetSingleAsync<Booking>(x => x.Id == domainModel.Id,
             x => x.Car,
             x => x.Customer,
             x => x.Customer.Address,
             x => x.Extras,
             x => x.TravelDetails,
             x => x.Payments,
             x => x.Payments.Select(y => y.CreditCard),
             x => x.Payments.Select(y => y.BankAccount),
             x => x.Payments.Select(y => y.Currency)
             );


                dataModel.Created = domainModel.Created;
                dataModel.Id = domainModel.Id;
                dataModel.CreatedBy = domainModel.CreatedBy;
                dataModel.Modified = domainModel.Modified;
                dataModel.ModifiedBy = domainModel.ModifiedBy;
                dataModel.StartDate = domainModel.StartDate;
                dataModel.EndDate = domainModel.EndDate;
                dataModel.Status = (Data.Enums.StatusType)domainModel.Status;
                dataModel.AgentReference = domainModel.AgentReference;
                dataModel.Cost = domainModel.Cost;
                dataModel.BookingReference = domainModel.BookingReference;
                dataModel.CarId = domainModel.CarId;
                dataModel.Car = domainModel.Car != null ? new Car
                {
                    Colour = domainModel.Car.Colour,
                    Id = domainModel.Car.Id,
                    Registration = domainModel.Car.Registration,
                    Model = domainModel.Car.Model,
                    Make = domainModel.Car.Make,
                    Created = domainModel.Car.Created,
                    CreatedBy = domainModel.Car.CreatedBy,
                    ModifiedBy = domainModel.Car.ModifiedBy,
                    Modified = domainModel.Car.Modified,
                    UserId = domainModel.Car.UserId,
                    //User = domainModel.Car.User != null ? new User
                    //{                    
                    //    Id= domainModel.Id,
                    //    Address = null,
                    //    Created = domainModel.Car.User.Created,
                    //    CreatedBy = domainModel.Car.User.CreatedBy,
                    //    ModifiedBy = domainModel.Car.User.ModifiedBy,
                    //    Modified = domainModel.Car.User.Modified,
                    //} : null,
                } : null;

                if (domainModel.TravelDetails != null)
                {
                    Repository.Delete<TravelDetails>(dataModel.TravelDetails);

                    dataModel.TravelDetailsId = domainModel.TravelDetailsId;

                    dataModel.TravelDetails = domainModel.TravelDetails != null ? new TravelDetails
                    {
                        Id = domainModel.TravelDetails.Id,
                        OutboundDate = domainModel.TravelDetails.OutboundDate,
                        ReturnDate = domainModel.TravelDetails.ReturnDate,
                        OutboundFlight = domainModel.TravelDetails.OutboundFlight,
                        OutboundTerminal = domainModel.TravelDetails.OutboundTerminal,
                        ReturnboundTerminal = domainModel.TravelDetails.ReturnboundTerminal,
                        ReturnFlight = domainModel.TravelDetails.ReturnFlight,
                        Created = domainModel.TravelDetails.Created,
                        CreatedBy = domainModel.TravelDetails.CreatedBy,
                        ModifiedBy = domainModel.TravelDetails.ModifiedBy,
                        Modified = domainModel.TravelDetails.Modified,
                    } : null;
                }


                if (domainModel.Payments != null)
                {
                    Repository.DeleteMany<Payment>(dataModel.Payments.ToArray());

                    dataModel.Payments = domainModel.Payments != null ? domainModel.Payments.Select(x => new Payment
                    {
                        BankAccountId = x.BankAccountId,
                        Status = (Data.Enums.StatusType)x.Status,
                        //Customer = dataModel.Customer,
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy,
                        Modified = x.Modified,
                        CreditCardId = x.CreditCardId,
                        CreditCard = x.CreditCard != null ? new CreditCard
                        {
                            Created = x.CreditCard.Created,
                            CreatedBy = x.CreditCard.CreatedBy,
                            ModifiedBy = x.CreditCard.ModifiedBy,
                            Modified = x.CreditCard.Modified,
                            Id = x.CreditCard.Id,
                            Deleted = x.CreditCard.Deleted,
                            ExpiryDate = x.CreditCard.ExpiryDate,
                            GateWayKey = x.CreditCard.GateWayKey,
                            Lock = x.CreditCard.Lock,
                            Type = (Data.Enums.CreditCardTypes)x.CreditCard.Type,
                            Name = x.CreditCard.Name,
                            Number = x.CreditCard.Number,
                            PlainNumber = x.CreditCard.PlainNumber
                        } : null,
                        BankAccount = x.BankAccount != null ? new BankAccount
                        {
                            Created = x.BankAccount.Created,
                            CreatedBy = x.BankAccount.CreatedBy,
                            ModifiedBy = x.BankAccount.ModifiedBy,
                            Modified = x.BankAccount.Modified,
                            Id = x.BankAccount.Id,
                            AbaRouting = x.BankAccount.AbaRouting,
                            AccountName = x.BankAccount.AccountName,
                            BankName = x.BankAccount.BankName,
                            Type = x.BankAccount.Type,
                            Lock = x.BankAccount.Lock,
                            BankAccountNumber = x.BankAccount.BankAccountNumber,
                        } : null,
                        PaymentMethod = (Data.Enums.PaymentMethod)x.PaymentMethod,
                        Currency = x.Currency != null ? new Currency
                        {
                            Created = x.Currency.Created,
                            CreatedBy = x.Currency.CreatedBy,
                            ModifiedBy = x.Currency.ModifiedBy,
                            Modified = x.Currency.Modified,
                            Id = x.Currency.Id,
                            Code = x.Currency.Code,
                            CountryCode = x.Currency.CountryCode,
                            Symbol = x.Currency.Symbol
                        } : null,
                    }).ToList() : null;

                }


                if (domainModel.Customer != null)
                {
                    Repository.Delete<Customer>(dataModel.Customer);

                    //dataModel.CustomerId = domainModel.CustomerId;
                    dataModel.Customer = domainModel.Customer != null ? new Customer
                    {
                        Id = domainModel.Customer.Id,
                        Created = domainModel.Customer.Created,
                        CreatedBy = domainModel.Customer.CreatedBy,
                        ModifiedBy = domainModel.Customer.ModifiedBy,
                        Modified = domainModel.Customer.Modified,
                        AddressId = domainModel.Customer.AddressId,
                        Address = domainModel.Customer.Address != null ? new Address
                        {
                            Id = domainModel.Customer.Address.Id,
                            Address1 = domainModel.Customer.Address.Address1,
                            Address2 = domainModel.Customer.Address.Address2,
                            City = domainModel.Customer.Address.City,
                            Country = domainModel.Customer.Address.Country,
                            County = domainModel.Customer.Address.County,
                            Number = domainModel.Customer.Address.Number,
                            Postcode = domainModel.Customer.Address.Postcode,
                            Created = domainModel.Customer.Address.Created,
                            CreatedBy = domainModel.Customer.Address.CreatedBy,
                            ModifiedBy = domainModel.Customer.Address.ModifiedBy,
                            Modified = domainModel.Customer.Address.Modified,
                        } : null,
                        Email = domainModel.Customer.Email,
                        Fax = domainModel.Customer.Fax,
                        Forename = domainModel.Customer.Forename,
                        Initials = domainModel.Customer.Initials,
                        Mobile = domainModel.Customer.Mobile,
                        Surname = domainModel.Customer.Surname,
                        Telephone = domainModel.Customer.Telephone,
                        Title = domainModel.Customer.Title,

                    } : null;
                }

                dataModel.Price = domainModel.Price;
                dataModel.SourceCode = domainModel.SourceCode;
                dataModel.UserId = domainModel.UserId;
                dataModel.User = domainModel.User != null ? new User
                {
                    Email = domainModel.User.Email,
                    FirstName = domainModel.User.FirstName,
                    LastName = domainModel.User.LastName,
                    Password = domainModel.User.Password,
                    PhoneNumber = domainModel.User.PhoneNumber,
                    Address = domainModel.User.Address != null ? new Address
                    {
                        Id = domainModel.User.Address.Id,
                        Address1 = domainModel.User.Address.Address1,
                        Address2 = domainModel.User.Address.Address2,
                        City = domainModel.User.Address.City,
                        Country = domainModel.User.Address.Country,
                        County = domainModel.User.Address.County,
                        Number = domainModel.User.Address.Number,
                        Postcode = domainModel.User.Address.Postcode,
                        Created = domainModel.User.Address.Created,
                        CreatedBy = domainModel.User.Address.CreatedBy,
                        ModifiedBy = domainModel.User.Address.ModifiedBy,
                        Modified = domainModel.User.Address.Modified,
                    } : null,
                    Id = domainModel.User.Id,
                    Created = domainModel.User.Created,
                    CreatedBy = domainModel.User.CreatedBy,
                    ModifiedBy = domainModel.User.ModifiedBy,
                    Modified = domainModel.User.Modified,
                    DOB = domainModel.User.DOB
                } : null;


                if (dataModel.Extras.Count > 0)
                {
                    Repository.DeleteMany<Extra>(dataModel.Extras.ToArray());

                    dataModel.Extras = domainModel.Extras != null ? domainModel.Extras.Select(x => new Extra
                    {
                        Id = x.Id,
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,
                        Modified = x.Modified,
                        ModifiedBy = x.ModifiedBy,
                        BookingEntityId = x.BookingEntityId,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description
                    }).ToList() : null;
                }


                Repository.Update<Booking>(dataModel);
                var result = Repository.Commit();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
            return  true;
        }

        public override BookingModel ToDomainModel(Booking dataModel)
        {
            BookingModel domainModel = new BookingModel();
            domainModel.Created = dataModel.Created;
            domainModel.Id = dataModel.Id;
            domainModel.CreatedBy = dataModel.CreatedBy;
            domainModel.Modified = dataModel.Modified;
            domainModel.ModifiedBy = dataModel.ModifiedBy;
            domainModel.StartDate = dataModel.StartDate;
            domainModel.EndDate = dataModel.EndDate;
            domainModel.Status = (Business.Enums.StatusType)dataModel.Status;
            domainModel.AgentReference = dataModel.AgentReference;
            domainModel.Cost = dataModel.Cost;
            domainModel.BookingReference = dataModel.BookingReference;
            domainModel.CarId = dataModel.CarId;
            domainModel.Car = dataModel.Car != null ? new CarModel
            {
                Colour = dataModel.Car.Colour,
                Id = dataModel.Car.Id,
                Registration = dataModel.Car.Registration,
                Model = dataModel.Car.Model,
                Make = dataModel.Car.Make,
                Created = dataModel.Car.Created,
                CreatedBy = dataModel.Car.CreatedBy,
                ModifiedBy = dataModel.Car.ModifiedBy,
                Modified = dataModel.Car.Modified,
                UserId = dataModel.Car.UserId,
                //User = dataModel.Car.User != null ? new User
                //{                    
                //    Id= dataModel.Id,
                //    Address = null,
                //    Created = dataModel.Car.User.Created,
                //    CreatedBy = dataModel.Car.User.CreatedBy,
                //    ModifiedBy = dataModel.Car.User.ModifiedBy,
                //    Modified = dataModel.Car.User.Modified,
                //} : null,
            } : null;
            domainModel.CustomerId = dataModel.Id;
            domainModel.Customer = dataModel.Customer != null ? new CustomerModel
            {
                Id = dataModel.Customer.Id,
                Created = dataModel.Customer.Created,
                CreatedBy = dataModel.Customer.CreatedBy,
                ModifiedBy = dataModel.Customer.ModifiedBy,
                Modified = dataModel.Customer.Modified,
                Address = dataModel.Customer.Address != null ? new AddressModel
                {
                    Id = dataModel.Customer.Address.Id,
                    Address1 = dataModel.Customer.Address.Address1,
                    Address2 = dataModel.Customer.Address.Address2,
                    City = dataModel.Customer.Address.City,
                    Country = dataModel.Customer.Address.Country,
                    County = dataModel.Customer.Address.County,
                    Number = dataModel.Customer.Address.Number,
                    Postcode = dataModel.Customer.Address.Postcode,
                    Created = dataModel.Customer.Address.Created,
                    CreatedBy = dataModel.Customer.Address.CreatedBy,
                    ModifiedBy = dataModel.Customer.Address.ModifiedBy,
                    Modified = dataModel.Customer.Address.Modified,
                } : null,
                Email = dataModel.Customer.Email,
                AddressId = dataModel.Customer.AddressId,
                Fax = dataModel.Customer.Fax,
                Forename = dataModel.Customer.Forename,
                Initials = dataModel.Customer.Initials,
                Mobile = dataModel.Customer.Mobile,
                Surname = dataModel.Customer.Surname,
                Telephone = dataModel.Customer.Telephone,
                Title = dataModel.Customer.Title,
            } : null;
            domainModel.Extras = dataModel.Extras != null ? dataModel.Extras.Select(x => new ExtraModel
            {
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                Modified = x.Modified,
                Id = x.Id,
                BookingEntityId = x.BookingEntityId,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
            }).ToList() : null;
            domainModel.Message = dataModel.Message;

            domainModel.Payments = dataModel.Payments != null ? dataModel.Payments.Select(x => new PaymentModel
            {
                BankAccountId = x.BankAccountId,
                Status = (Business.Enums.StatusType)x.Status,
                //Customer = domainModel.Customer,
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                Modified = x.Modified,
                CreditCardId = x.CreditCardId,
                CreditCard = x.CreditCard != null ? new CreditCardModel
                {
                    Created = x.CreditCard.Created,
                    CreatedBy = x.CreditCard.CreatedBy,
                    ModifiedBy = x.CreditCard.ModifiedBy,
                    Modified = x.CreditCard.Modified,
                    Id = x.CreditCard.Id,
                    Deleted = x.CreditCard.Deleted,
                    ExpiryDate = x.CreditCard.ExpiryDate,
                    GateWayKey = x.CreditCard.GateWayKey,
                    Lock = x.CreditCard.Lock,
                    Type = (Business.Enums.CreditCardTypes)x.CreditCard.Type,
                    Name = x.CreditCard.Name,
                    Number = x.CreditCard.Number,
                    PlainNumber = x.CreditCard.PlainNumber
                } : null,
                BankAccount = x.BankAccount != null ? new BankAccountModel
                {
                    Created = x.BankAccount.Created,
                    CreatedBy = x.BankAccount.CreatedBy,
                    ModifiedBy = x.BankAccount.ModifiedBy,
                    Modified = x.BankAccount.Modified,
                    Id = x.BankAccount.Id,
                    AbaRouting = x.BankAccount.AbaRouting,
                    AccountName = x.BankAccount.AccountName,
                    BankName = x.BankAccount.BankName,
                    Type = x.BankAccount.Type,
                    Lock = x.BankAccount.Lock,
                    BankAccountNumber = x.BankAccount.BankAccountNumber,
                } : null,
                PaymentMethod = (Business.Enums.PaymentMethod)x.PaymentMethod,
                Currency = x.Currency != null ? new CurrencyModel
                {
                    Created = x.Currency.Created,
                    CreatedBy = x.Currency.CreatedBy,
                    ModifiedBy = x.Currency.ModifiedBy,
                    Modified = x.Currency.Modified,
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    CountryCode = x.Currency.CountryCode,
                    Symbol = x.Currency.Symbol
                } : null,
            }).ToList() : null;
            domainModel.Price = dataModel.Price;
            domainModel.SourceCode = dataModel.SourceCode;
            domainModel.TravelDetailsId = dataModel.TravelDetailsId;
            domainModel.TravelDetails = dataModel.TravelDetails != null ? new TravelDetailsModel
            {
                Id = dataModel.TravelDetails.Id,
                OutboundDate = dataModel.TravelDetails.OutboundDate,
                ReturnDate = dataModel.TravelDetails.ReturnDate,
                OutboundFlight = dataModel.TravelDetails.OutboundFlight,
                OutboundTerminal = dataModel.TravelDetails.OutboundTerminal,
                ReturnboundTerminal = dataModel.TravelDetails.ReturnboundTerminal,
                ReturnFlight = dataModel.TravelDetails.ReturnFlight,
                Created = dataModel.TravelDetails.Created,
                CreatedBy = dataModel.TravelDetails.CreatedBy,
                ModifiedBy = dataModel.TravelDetails.ModifiedBy,
                Modified = dataModel.TravelDetails.Modified,
            } : null;
            domainModel.UserId = dataModel.UserId;
            domainModel.User = dataModel.User != null ? new UserModel
            {
                Email = dataModel.User.Email,
                FirstName = dataModel.User.FirstName,
                LastName = dataModel.User.LastName,
                Password = dataModel.User.Password,
                PhoneNumber = dataModel.User.PhoneNumber,
                DOB = dataModel.User.DOB,
                Address = dataModel.User.Address != null ? new AddressModel
                {
                    Id = dataModel.User.Address.Id,
                    Address1 = dataModel.User.Address.Address1,
                    Address2 = dataModel.User.Address.Address2,
                    City = dataModel.User.Address.City,
                    Country = dataModel.User.Address.Country,
                    County = dataModel.User.Address.County,
                    Number = dataModel.User.Address.Number,
                    Postcode = dataModel.User.Address.Postcode,
                    Created = dataModel.User.Address.Created,
                    CreatedBy = dataModel.User.Address.CreatedBy,
                    ModifiedBy = dataModel.User.Address.ModifiedBy,
                    Modified = dataModel.User.Address.Modified,
                } : null,
                Id = dataModel.User.Id,
                Created = dataModel.User.Created,
                CreatedBy = dataModel.User.CreatedBy,
                ModifiedBy = dataModel.User.ModifiedBy,
                Modified = dataModel.User.Modified,
            } : null;

            return domainModel;
        }

        public override Booking ToDataModel(BookingModel domainModel, Booking dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Booking();

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.StartDate = domainModel.StartDate;
            dataModel.EndDate = domainModel.EndDate;
            dataModel.Status =(Data.Enums.StatusType) domainModel.Status;
            dataModel.AgentReference = domainModel.AgentReference;
            dataModel.Cost = domainModel.Cost;
            dataModel.BookingReference = domainModel.BookingReference;
            dataModel.CarId = domainModel.CarId;
            dataModel.Car = domainModel.Car != null ? new Car
            {
                Colour = domainModel.Car.Colour,
                Id = domainModel.Car.Id,
                Registration = domainModel.Car.Registration,
                Model = domainModel.Car.Model,
                Make = domainModel.Car.Make,
                Created = domainModel.Car.Created,
                CreatedBy = domainModel.Car.CreatedBy,
                ModifiedBy = domainModel.Car.ModifiedBy,
                Modified = domainModel.Car.Modified,
                UserId = domainModel.Car.UserId,                 
                //User = domainModel.Car.User != null ? new User
                //{                    
                //    Id= domainModel.Id,
                //    Address = null,
                //    Created = domainModel.Car.User.Created,
                //    CreatedBy = domainModel.Car.User.CreatedBy,
                //    ModifiedBy = domainModel.Car.User.ModifiedBy,
                //    Modified = domainModel.Car.User.Modified,
                //} : null,
            } : null;
            dataModel.CustomerId = domainModel.Id;
            dataModel.Customer = domainModel.Customer != null ? new Customer
            {
                Id = domainModel.Customer.Id,                
                Created = domainModel.Customer.Created,
                CreatedBy = domainModel.Customer.CreatedBy,
                ModifiedBy = domainModel.Customer.ModifiedBy,
                Modified = domainModel.Customer.Modified,
                Address = domainModel.Customer.Address != null ? new Address
                {
                    Id = domainModel.Customer.Address.Id,
                    Address1  = domainModel.Customer.Address.Address1,
                    Address2 = domainModel.Customer.Address.Address2,
                    City = domainModel.Customer.Address.City,
                    Country = domainModel.Customer.Address.Country,
                    County = domainModel.Customer.Address.County,
                    Number = domainModel.Customer.Address.Number,
                    Postcode = domainModel.Customer.Address.Postcode,
                    Created = domainModel.Customer.Address.Created,
                    CreatedBy = domainModel.Customer.Address.CreatedBy,
                    ModifiedBy = domainModel.Customer.Address.ModifiedBy,
                    Modified = domainModel.Customer.Address.Modified,
                } : null,
                 Email = domainModel.Customer.Email,
                 AddressId = domainModel.Customer.AddressId,
                 Fax = domainModel.Customer.Fax,
                 Forename = domainModel.Customer.Forename,
                 Initials = domainModel.Customer.Initials,
                 Mobile = domainModel.Customer.Mobile,
                 Surname = domainModel.Customer.Surname,
                 Telephone = domainModel.Customer.Telephone,
                 Title = domainModel.Customer.Title,                           
            } : null;
            dataModel.Extras = domainModel.Extras != null ? domainModel.Extras.Select(x => new Extra
            {
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                Modified = x.Modified,
                Id = x.Id,
                BookingEntityId = x.BookingEntityId,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,                                      
            }).ToList() : null;
            dataModel.Message = domainModel.Message;

            dataModel.Payments = domainModel.Payments != null ? domainModel.Payments.Select(x => new Payment
            {
                BankAccountId = x.BankAccountId,
                Status = (Data.Enums.StatusType)x.Status,
                //Customer = dataModel.Customer,
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                Modified = x.Modified,
                CreditCardId = x.CreditCardId,
                CurrencyId = x.CurrencyId,
                CreditCard = x.CreditCard!=null? new CreditCard {
                    Created = x.CreditCard.Created,
                    CreatedBy = x.CreditCard.CreatedBy,
                    ModifiedBy = x.CreditCard.ModifiedBy,
                    Modified = x.CreditCard.Modified,
                    Id = x.CreditCard.Id,
                    Deleted = x.CreditCard.Deleted,
                    ExpiryDate = x.CreditCard.ExpiryDate,
                    GateWayKey = x.CreditCard.GateWayKey,
                    Lock = x.CreditCard.Lock,
                    Type = (Data.Enums.CreditCardTypes)x.CreditCard.Type,
                    Name = x.CreditCard.Name,
                    Number = x.CreditCard.Number,
                    PlainNumber = x.CreditCard.PlainNumber
                } :null,
                BankAccount = x.BankAccount!=null?new BankAccount
                {
                    Created = x.BankAccount.Created,
                    CreatedBy = x.BankAccount.CreatedBy,
                    ModifiedBy = x.BankAccount.ModifiedBy,
                    Modified = x.BankAccount.Modified,
                    Id = x.BankAccount.Id,
                    AbaRouting = x.BankAccount.AbaRouting,
                    AccountName = x.BankAccount.AccountName,
                    BankName = x.BankAccount.BankName,
                    Type = x.BankAccount.Type,
                    Lock = x.BankAccount.Lock,
                    BankAccountNumber = x.BankAccount.BankAccountNumber,
                } : null,
                PaymentMethod = (Data.Enums.PaymentMethod)x.PaymentMethod,
                Currency =x.Currency!=null?new Currency {
                    Created = x.Currency.Created,
                    CreatedBy = x.Currency.CreatedBy,
                    ModifiedBy = x.Currency.ModifiedBy,
                    Modified = x.Currency.Modified,
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    CountryCode = x.Currency.CountryCode,
                    Symbol = x.Currency.Symbol
                }:null,
            }).ToList() : null;
            dataModel.Price = domainModel.Price;
            dataModel.SourceCode = domainModel.SourceCode;
            dataModel.TravelDetailsId = domainModel.TravelDetailsId;
            dataModel.TravelDetails = domainModel.TravelDetails != null ? new TravelDetails
            {
                Id = domainModel.TravelDetails.Id,
                OutboundDate = domainModel.TravelDetails.OutboundDate,
                ReturnDate = domainModel.TravelDetails.ReturnDate,
                OutboundFlight = domainModel.TravelDetails.OutboundFlight,
                OutboundTerminal = domainModel.TravelDetails.OutboundTerminal,
                ReturnboundTerminal = domainModel.TravelDetails.ReturnboundTerminal,
                ReturnFlight = domainModel.TravelDetails.ReturnFlight,
                Created = domainModel.TravelDetails.Created,
                CreatedBy = domainModel.TravelDetails.CreatedBy,
                ModifiedBy = domainModel.TravelDetails.ModifiedBy,
                Modified = domainModel.TravelDetails.Modified,
            } : null;
            dataModel.UserId = domainModel.UserId;
            dataModel.User = domainModel.User != null ? new User
            {           
                 Email = domainModel.User.Email,
                 FirstName = domainModel.User.FirstName,
                 LastName= domainModel.User.LastName,
                 Password = domainModel.User.Password,
                 PhoneNumber = domainModel.User.PhoneNumber,
                 DOB = domainModel.User.DOB,
                 Address= domainModel.User.Address != null ? new Address
                 {
                    Id = domainModel.User.Address.Id,
                    Address1 = domainModel.User.Address.Address1,
                    Address2 = domainModel.User.Address.Address2,
                    City = domainModel.User.Address.City,
                    Country = domainModel.User.Address.Country,
                    County = domainModel.User.Address.County,
                    Number = domainModel.User.Address.Number,
                    Postcode = domainModel.User.Address.Postcode,
                    Created = domainModel.User.Address.Created,
                    CreatedBy = domainModel.User.Address.CreatedBy,
                    ModifiedBy = domainModel.User.Address.ModifiedBy,
                    Modified = domainModel.User.Address.Modified,
                } : null,
                Id = domainModel.User.Id,
                Created = domainModel.User.Created,
                CreatedBy = domainModel.User.CreatedBy,
                ModifiedBy = domainModel.User.ModifiedBy,
                Modified = domainModel.User.Modified,
            } : null;
            return dataModel;
        }

      
        public async Task<BookingModel> GetByReference(string reference)
        {
            var result = Repository.GetSingle<Booking>(x => x.BookingReference ==reference ,
               x => x.Car,
               x => x.Customer,
               x => x.Customer.Address,
               x => x.Extras,
               x => x.TravelDetails,
               x => x.Payments,
               x => x.Payments.Select(y => y.CreditCard),
               x => x.Payments.Select(y => y.BankAccount),
               x => x.Payments.Select(y => y.Currency)
               );

            return ToDomainModel(result);
        }

      
    }
}
