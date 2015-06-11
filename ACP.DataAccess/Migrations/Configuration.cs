namespace ACP.DataAccess.Migrations
{
    using ACP.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ACP.DataAccess.ACPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ACP.DataAccess.ACPContext context)
        {
            Address address1=new Address{ Id=1, Address1="Manchester Airport", Address2="Manchester", Country="UK", County="Greater Manchester", Postcode="M90 3AE"};
            Address address2 = new Address { Id = 2, Address1 = "Manchester Airport", Address2 = "wythenshawe", Country = "UK", County = "Greater Manchester", Postcode = "M90 3EE" };
            Address address3 = new Address { Id = 3, Address1 = "West Sussex", Country = "UK", County = "Geater London", Postcode = "RH6 0NP" };
            //Address address4=new Address{ Id=4, Address1="Manchester Airport", Address2="Manchester", Country="UK", County="Greate Manchester", Postcode="M90 3AE"};
            //Address address5=new Address{ Id=5, Address1="Manchester Airport", Address2="Manchester", Country="UK", County="Greate Manchester", Postcode="M90 3AE"};


            context.BookingEntities.AddOrUpdate(x => x.Id, new BookingEntity { Id = 1, Name = "Speedy Carpark", Price = 12, Address = address1 });
            context.BookingEntities.AddOrUpdate(x => x.Id, new BookingEntity { Id = 2, Name = "Manchester Chauffeur", Price = 16, Address = address2 });
            context.BookingEntities.AddOrUpdate(x => x.Id, new BookingEntity { Id = 3, Name = "Gatwick Premier Parking", Price = 16 , Address=address3});

            //Data Types
            context.BookingPricings.AddOrUpdate(x => x.Id, new BookingPricing { Id = 1, Name = "Summer", Start = new DateTime(2015, 6, 1, 12, 0, 0), End = new DateTime(2015, 9, 1, 12, 0, 0), BookingEntityId=1 });
            context.BookingPricings.AddOrUpdate(x => x.Id, new BookingPricing { Id = 2, Name = "Autum", Start = new DateTime(2015, 9, 1, 12, 0, 0), End = new DateTime(2015, 11, 1, 12, 0, 0) , BookingEntityId=2});
            context.BookingPricings.AddOrUpdate(x => x.Id, new BookingPricing { Id = 3, Name = "Winter", Start = new DateTime(2015, 11, 1, 12, 0, 0), End = new DateTime(2016, 3, 1, 12, 0, 0) , BookingEntityId=2});
            context.BookingPricings.AddOrUpdate(x => x.Id, new BookingPricing { Id = 4, Name = "Spring", Start = new DateTime(2016, 3, 1, 12, 0, 0), End = new DateTime(2015, 6, 1, 12, 0, 0), BookingEntityId=3 });

            //context.BookingEntities.AddOrUpdate(x => x.Id, new BookingEntity { Id=1, });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
