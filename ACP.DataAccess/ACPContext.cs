﻿using ACP.Data;
using ACP.Data.Classes;
using ACP.DataAccess.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess
{
    public class ACPContext: DbContext
    {
        public ACPContext(): base("ACP")
        { 

        }

        static ACPContext()
        {
            //Database.SetInitializer<ACPContext>(new ACPInitialiser());
            Database.SetInitializer<ACPContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RootBookingPropertiesConfig());
            modelBuilder.Configurations.Add(new PropertiesConfig());
            modelBuilder.Configurations.Add(new RootBookingEntityConfig());
            modelBuilder.Configurations.Add(new BookingServiceConfig());
            modelBuilder.Configurations.Add(new AddressConfig());
            modelBuilder.Configurations.Add(new BookingConfig());
            modelBuilder.Configurations.Add(new BookingEntityConfig());
            ///## BOOKINGPRICING
            modelBuilder.Configurations.Add(new HourPriceConfig());
            modelBuilder.Configurations.Add(new DayPriceConfig());
            modelBuilder.Configurations.Add(new StatusConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new CarConfig());
            modelBuilder.Configurations.Add(new SlotConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new PaymentConfig());
            modelBuilder.Configurations.Add(new TravelDetailsConfig());
            modelBuilder.Configurations.Add(new BookingLinkConfig());
            modelBuilder.Configurations.Add(new CreditCardConfig());
            modelBuilder.Configurations.Add(new BankAccountConfig());
            modelBuilder.Configurations.Add(new CurrencyConfig());
            modelBuilder.Configurations.Add(new ReviewConfig());

        }

        public DbSet<RootBookingProperty> RootBookingProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<RootBookingEntity> RootBookingEntity { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingEntity> BookingEntities { get; set; }
        public DbSet<BookingPricing> BookingPricings { get; set; }
        public DbSet<HourPrice> HourPrices { get; set; }
        public DbSet<DayPrice> DayPrices { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TravelDetails> TravelDetails { get; set; }
        public DbSet<BookingLink> BookingLinks { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
