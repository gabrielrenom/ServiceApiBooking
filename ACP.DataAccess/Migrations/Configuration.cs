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
            context.Statuses.AddOrUpdate(x => x.Id, new Status { Id=1, StatusType= StatusType.Active});
            context.Statuses.AddOrUpdate(x => x.Id, new Status { Id=2, StatusType = StatusType.Inactive });

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
                       },
                        Slot = new Collection<Slot>
                        {
                            new Slot {  Number=13, IsOccupied=false},
                             new Slot {  IsOccupied = false,
                                        Number = 12,
                                                        Created = DateTime.Now,
                                                                    CreatedBy = "localuser",
                                                                    Modified = DateTime.Now,
                                                                    ModifiedBy = "localuser",

                                        Availability = new Collection<Availability> {
                                                            new Availability {
                                                                    Created = DateTime.Now,
                                                                    CreatedBy = "localuser",
                                                                    Modified = DateTime.Now,
                                                                    ModifiedBy = "localuser",
                                                                    StartDate = new DateTime(2015,10,2),
                                                                    EndDate = new DateTime(2015,10,8),
                                                                    StatusId = 1
                                                            },
                                                                 new Availability {
                                                                    Created = DateTime.Now,
                                                                    CreatedBy = "localuser",
                                                                    Modified = DateTime.Now,
                                                                    ModifiedBy = "localuser",
                                                                    StartDate = new DateTime(2015,10,3),
                                                                    EndDate = new DateTime(2015,10,7),
                                                                    StatusId = 2
                                                            }
                          }
                                       
                        }
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
                       },
                       Slot = new Collection<Slot>
                       {
                        new Slot {
                                        Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                IsOccupied = false,
                                Number= 3,
                                Availability = new Collection<Availability> {
                                    new Availability {
                                            Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                            StartDate = new DateTime(2015,10,2),
                                            EndDate = new DateTime(2015,10,15),
                                            StatusId = 1
                                    },
                                       new Availability {
                                            Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                            StartDate = new DateTime(2015,10,4),
                                            EndDate = new DateTime(2015,10,18),
                                            StatusId = 1
                                    },
                                         new Availability {
                                            Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                            StartDate = new DateTime(2015,10,5),
                                            EndDate = new DateTime(2015,10,19),
                                            StatusId = 2
                                    }
                        }
                        }
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
