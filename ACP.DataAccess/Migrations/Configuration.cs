namespace ACP.DataAccess.Migrations
{
    using ACP.Data;
    using Data.Enums;
    using System;
    using System.Collections.ObjectModel;
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
            context.Statuses.AddOrUpdate(x => x.Id, new Status { Id=1, Name="Active"});
            context.Statuses.AddOrUpdate(x => x.Id, new Status { Id=2, Name="Disable" });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id=1,
                Address = new Address
                {
                    Address1 = "Airport Street",                    
                    Country = "UK",
                    County = "Greater Manchester",
                    Postcode = "M90 1QX"
                },
                Created = DateTime.Now,
                CreatedBy = "LocalUser",
                Modified = DateTime.Now,
                ModifiedBy = "LocalUser",
                Name = "MAN",
                StatusId=1,
                Telephone="01612542018",                     
                BookingEntities = new Collection<BookingEntity>
                {
                    new BookingEntity
                    {
                       Comission = 2,
                       Created = DateTime.Now,
                       CreatedBy = "localuser",
                       Modified = DateTime.Now,
                       ModifiedBy = "localuser",
                       Name = "Local Hazel Grove Parking" + DateTime.Now.ToString(),
                       Address = new Address
                       {
                            Address1 = "16 Battersbay Grove",
                            Postcode = "SK74QW",
                            Country = "UK"
                       },
                       Properties = new Collection<Property>
                       {
                         new Property{ Key = "Provider", Value="APH" },
                         new Property{ Key = "Carpark Code", Value="LGW1" }
                       }
                    },
                    new BookingEntity
                    {
                       Comission = 3,
                       Created = DateTime.Now,
                       CreatedBy = "localuser",
                       Modified = DateTime.Now,
                       ModifiedBy = "localuser",
                       Name = "Manchester Airport Parking" + DateTime.Now.ToString(),
                       Address = new Address
                       {
                            Address1 = "Manchester Airport Street",
                            Postcode = "M13DF",
                            Country = "UK"
                       },
                       Properties = new Collection<Property>
                       {
                         new Property{ Key = "Provider", Value="Purple Parking", Type = PropertyType.String }
                       }
                    }
                }
            });
            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 2,
                Address = new Address
                {
                    Address1 = "Airport Street",
                    Country = "UK",
                    County = "Greater London",
                    Postcode = "W1 1QX"
                },
                Created = DateTime.Now,
                CreatedBy = "LocalUser",
                Modified = DateTime.Now,
                ModifiedBy = "LocalUser",
                Name = "LGW",
                StatusId = 1,
                Telephone = "01612542018",
                BookingEntities = new Collection<BookingEntity>
                {
                    new BookingEntity
                    {
                       Comission = 2,
                       Created = DateTime.Now,
                       CreatedBy = "localuser",
                       Modified = DateTime.Now,
                       ModifiedBy = "localuser",
                       Name = "Local Hazel Grove Parking" + DateTime.Now.ToString(),
                       Address = new Address
                       {
                            Address1 = "16 Battersbay Grove",
                            Postcode = "SK74QW",
                            Country = "UK"
                       },
                       Properties = new Collection<Property>
                       {
                         new Property{ Key = "Provider", Value="APH" },
                         new Property{ Key = "Carpark Code", Value="LGW1" }
                       }
                    },
                    new BookingEntity
                    {
                       Comission = 3,
                       Created = DateTime.Now,
                       CreatedBy = "localuser",
                       Modified = DateTime.Now,
                       ModifiedBy = "localuser",
                       Name = "Manchester Airport Parking" + DateTime.Now.ToString(),
                       Address = new Address
                       {
                            Address1 = "Manchester Airport Street",
                            Postcode = "M13DF",
                            Country = "UK"
                       },
                       Properties = new Collection<Property>
                       {
                         new Property{ Key = "Provider", Value="Purple Parking", Type = PropertyType.String }
                       }
                    }
                }
            });
                            

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
