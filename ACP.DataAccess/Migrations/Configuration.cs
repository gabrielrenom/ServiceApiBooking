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


            int testid = 2;

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 1,
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
                                                                    Status =  AvailabilityStatus.Free
                                                            },
                                                                 new Availability {
                                                                    Created = DateTime.Now,
                                                                    CreatedBy = "localuser",
                                                                    Modified = DateTime.Now,
                                                                    ModifiedBy = "localuser",
                                                                    StartDate = new DateTime(2015,10,3),
                                                                    EndDate = new DateTime(2015,10,7),
                                                                    Status =  AvailabilityStatus.Occupied
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
                                            Status =  AvailabilityStatus.Free
                                    },
                                       new Availability {
                                            Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                            StartDate = new DateTime(2015,10,4),
                                            EndDate = new DateTime(2015,10,18),
                                            Status =  AvailabilityStatus.Free
                                    },
                                         new Availability {
                                            Created = DateTime.Now,
                                            CreatedBy = "localuser",
                                            Modified = DateTime.Now,
                                            ModifiedBy = "localuser",
                                            StartDate = new DateTime(2015,10,5),
                                            EndDate = new DateTime(2015,10,19),
                                            Status =  AvailabilityStatus.Free
                                    }
                        }
                        }
                       }
                    }
                }
            });


            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 1+ testid,
                Telephone = "+441415856000",
                Name = "Aberdeen Airport",
                Code = "ABZ",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Dyce",
                    City = "Aberdeen",
                    Postcode = "AB21 7DU",
                    Country = "Scotland",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });
            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 2+ testid,
                Telephone = "+441415856000",
                Name = "Barra Airport",
                Website = "www.hial.co.uk",
                Code = "BRR",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Human Resources Department",
                    Address2 = "Highlands & Islands Airports Limited,Head Office Inverness Airport",
                    City = "Inverness",
                    Postcode = "IV2 7JB",
                    Country = "Scotland",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });
            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 3+ testid,
                Telephone = "+442890939093",
                Name = "Belfast City Airport",
                Code = "BHD",
                Website = "www.belfastcityairport.com",
                StatusId = 1,
                Address = new Address
                {
                    City = "Belfast",
                    Postcode = "BT3 9JH",
                    Country = "Northem Ireland",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 4+ testid,
                Telephone = "+442890939093",
                Name = "Belfast International Airport",
                Code = "BFS",
                Website = "www.belfastairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Human Resources",
                    Address2 = "Belfast International Airport",
                    City = "Belfast",
                    Postcode = "Belfast International Airport",
                    Country = "Northem Ireland",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 5+ testid,
                Telephone = "+441212542600",
                Name = "Birmingham International Airport",
                Code = "BHX",
                Website = "www.bhx.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    City = "Birmingham",
                    Postcode = "B26 3QJ",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });
            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 6 + testid,
                Telephone = "+441212542600",
                Name = "Coventry Airport",
                Code = "CVT",                
                Website = "www.coventryairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1= "Passenger Terminal",
                    Address2= "Siskin Parkway West",
                    City = "Coventry",
                    Postcode = "CV3 4PB",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 7 + testid,
                Telephone = "+442476762220",
                Name = "Coventry Airport",
                Code = "CVT",
                Website = "www.coventryairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Passenger Terminal",
                    Address2 = "Siskin Parkway West",
                    City = "Coventry",
                    Postcode = "CV3 4PB",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 8 + testid,
                Telephone = "+441452857700 ",
                Name = "Gloucestershire Airport",
                Code = "GLO",
                Website = "www.gloucestershireairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Staverton",
                    Address2 = "Cheltenham",
                    City = "Gloucestershire",
                    Postcode = "GL51 6SR",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 9 + testid,
                Telephone = "+448719199000",
                Name = "Nottingham East Midlands Airport",
                Code = "EMA",
                Website = "www.eastmidlandsairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Castle Donington",
                    City = "Derbyshire",
                    Postcode = "DE34 5HH",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 10 + testid,
                Telephone = "+441223373765",
                Name = "Cambridge City Airport",
                Code = "CBG",
                Website = "www.cambridgecityairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Postcode="",
                    City = "Cambridge",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 11 + testid,
                Telephone = " +441652688456",
                Name = "Humberside International Airport",
                Code = "HUY",
                Website = "www.humbersideairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Kirmington ",
                    Address2 = "North Lincolnshire",
                    Postcode = "DN39 6YH",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 12 + testid,
                Telephone = "+441603411923",
                Name = "Norwich International Airport",
                Code = "NWI",
                Website = "www.norwichairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Amsterdam Way",
                    Postcode = "NR6 6JA",
                    City= "Norwich",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
            });

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 13 + testid,
                Telephone = "+448701212747",
                Name = "Bristol International Airport",
                Code = "BRS",
                Website = "www.bristolairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Postcode = "BS48 3DY",
                    City = "Bristol",
                    Country = "England",
                    CreatedBy = "localuser",
                    Modified = DateTime.Now,
                    ModifiedBy = "localuser",
                    Created = DateTime.Now,
                },
                CreatedBy = "localuser",
                Modified = DateTime.Now,
                ModifiedBy = "localuser",
                Created = DateTime.Now,
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
