using ACP.Data;
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
            Database.SetInitializer<ACPContext>(new ACPInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new StatusConfig());
            modelBuilder.Configurations.Add(new BookingServiceConfig());
            modelBuilder.Configurations.Add(new BookingEntityConfig());
            modelBuilder.Configurations.Add(new RootBookingEntityConfig());
            modelBuilder.Configurations.Add(new AddressConfig());

        }

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
    }
}
