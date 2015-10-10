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
                Telephone = "+441667464000",
                Name = "Inverness Airport",
                Website = "www.hial.co.uk",
                Code = "INV",
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

            context.RootBookingEntity.AddOrUpdate(x => x.Id, new RootBookingEntity
            {
                Id = 14 + testid,
                Telephone = "+441392367433",
                Name = "Exeter International Airport",
                Code = "EXT",
                Website = "www.exeter-airport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Exeter",
                    Address2 = "Devon",
                    Postcode = "EX5 2BD",
                    City = "Exeter",
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
                Id = 14 + testid,
                Telephone = "+441720422677",
                Name = "Isles of Scilly",
                Code = "ISC",
                Website = "www.islesofscilly-travel.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "St. Marys Airport",
                    Address2 = "St. Mary's",
                    Postcode = "TR21 0NG",
                    City = "Isles Of Scilly",
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
                Id = 15 + testid,
                Telephone = "+448457105555",
                Name = "Lands End Airport",
                Code = "LEQ",
                Website = "www.islesofscilly-travel.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "St Just",
                    Address2 = "Cornwall",
                    Postcode = "TR21 0NG",
                    City = "St Just",
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
                Id = 16 + testid,
                Telephone = "+441637860600",
                Name = "Newquay Cornwall Airport",
                Code = "NQY",
                Website = "www.newquaycornwallairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "St. Mawgan",
                    Address2 = "Cornwall",
                    Postcode = "TR8 4RQ",
                    City = "Newquay",
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
                Id = 17 + testid,
                Telephone = "+441752204090",
                Name = "Plymouth",
                Code = "",
                Website = "www.plymouthairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Plymouth City Airport",
                    Address2 = "Crownhill",
                    Postcode = "PL6 8BW",
                    City = "Plymouth",
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
                Id = 17 + testid,
                Telephone = "+441202364000",
                Name = "Bournemouth International Airport",
                Code = "BOH",
                Website = "www.flybournemouth.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Hurn",
                    Address2 = "Christchurch",
                    Postcode = "BH23 6SE",
                    City = "Dorset",
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
                Id = 17 + testid,
                Telephone = "+448700400009",
                Name = "Southampton Airport",
                Code = "SOU",
                Website = "www.southampton-airport-guide.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Postcode = "SO18 2NL",
                    City = "Southampton",
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
                Id = 18 + testid,
                Telephone = "+441843823600",
                Name = "Kent International Airport",
                Code = "MSE",
                Website = "www.kentinternationalairport-manston.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1= "Manston",
                    Address2 = "Folkestone",
                    Postcode = "CT12 5BP",
                    City = "Kent",
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
                Id = 19 + testid,
                Telephone = "+441415856000",
                Name = "London Gatwick Airport",
                Code = "",
                Website = "www.baa.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "West Sussex",
                    Postcode = "RH6 0JH",
                    City = "Gatwick",
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
                Id = 20 + testid,
                Telephone = "+442076460000",
                Name = "London City Airport",
                Code = "LCY",
                Website = "www.londoncityairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Hartmann Rd",
                    Postcode = "E16 2PX",
                    City = "London",
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
                Id = 21 + testid,
                Telephone = "+441415856000",
                Name = "London Heathrow Airport",
                Code = "LHR",
                Website = "www.baa.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Bath Rd",
                    Address2= "Middlesex",
                    Postcode = "TW4 7DE",
                    City = "Hounslow",
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
                Id = 22 + testid,
                Telephone = "+441582405100",
                Name = "London Luton Airport",
                Code = "LTN",
                Website = "www.london-luton.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Human Resources Department, London Luton Airport",
                    Address2 = "Navigation House, Airport Way,Beds",
                    Postcode = "LU2 9LY",
                    City = "Luton",
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
                Id = 23 + testid,
                Telephone = "+441702608100",
                Name = "London Southend Airport",
                Code = "SEN",
                Website = "www.southendairport.net",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Southend on Sea",
                    City = "Essex",
                    Postcode = "SS2 6YF",
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
                Id = 24 + testid,
                Telephone = "+441279662066",
                Name = "London Stansted Airport",
                Code = "STN",
                Website = "www.stanstedairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Bassingbourne Road",
                    Address2= "Essex",
                    City = "Stansted",
                    Postcode = "CM24 1QW",
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
                Id = 25 + testid,
                Telephone = "+441273296900",
                Name = "Shoreham (Brighton City) Airport",
                Code = "ESH",
                Website = "www.shorehamairport.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "West Essex",
                    City = "Shoreham by Sea",
                    Postcode = "BN43 5FF",
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
                Id = 26 + testid,
                Telephone = "+441870602051",
                Name = "Benbecula Airport - Western Isles, Scotland",
                Code = "BEB",
                Website = "www.hial.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 ="Benbecula Airport, Balivanich, Western Isles",
                    City = "Benbecula",
                    Postcode = "HS7 5LW",
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
                Id = 26 + testid,
                Telephone = "+441870602051",
                Name = "Benbecula Airport - Western Isles, Scotland",
                Code = "BEB",
                Website = "www.hial.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Benbecula Airport, Balivanich, Western Isles",
                    City = "Benbecula",
                    Postcode = "HS7 5LW",
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
                Id = 27 + testid,
                Telephone = "+441382662200",
                Name = "Dundee Airport",
                Code = "DND",
                Website = "www.hial.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Dundee Airport",
                    Address2 = "Riverside",
                    City = "Dundee",
                    Postcode = "DD2 1UH",
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
                Id = 28 + testid,
                Telephone = "+441586553797 ",
                Name = "Campbeltown Airport - Kintyre",
                Code = "BEB",
                Website = "www.hial.co.uk",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Campbeltown Airport",
                    City = "Argyll",
                    Postcode = "PA28 6NU",
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
                Id = 29 + testid,
                Telephone = "+448700400007",
                Name = "Edinburgh Airport",
                Code = "EDI",
                Website = "www.edinburghairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "BAA Edinburgh",
                    City = "Edinburgh",
                    Postcode = "EH12 9DN",
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
                Id = 30 + testid,
                Telephone = "+448700400008",
                Name = "Glasgow Airport",
                Code = "GLA",
                Website = "www.glasgowairport.com",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "BAA Glasgow",
                    Address2= "Paisley",
                    City = "Renfrewshire",
                    Postcode = "PA3 2SW",
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
                Id = 31 + testid,
                Telephone = "+448712230700",
                Name = "Glasgow Prestwick International Airport",
                Code = "PIK",
                Website = "www.gpia.co.uk ",
                StatusId = 1,
                Address = new Address
                {
                    City = "Prestwick",
                    Postcode = "PA3 2SW",
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
                Id = 32 + testid,
                Telephone = "+441871890220",
                Name = "Barra Airport - Western Isles",
                Website = "www.hial.co.uk",
                Code = "BRR",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd, ",
                    Address2 = "Barra Airport, Isle of Barra,",
                    City = "Eoligarry",
                    Postcode = "HS9 5YD",
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
                Id = 33 + testid,
                Telephone = "+441496302361",
                Name = "Islay Airport - Island of Islay",
                Website = "www.hial.co.uk",
                Code = "BRR",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Islay Airport, Isle of Islay",
                    City = "Glenegedale",
                    Postcode = "PA42 7AS",
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
                Id = 34 + testid,
                Telephone = "+441856872421",
                Name = "Kirkwall Airport- Orkney",
                Website = "www.hial.co.uk",
                Code = "KOI",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Kirkwall Airport",
                    City = "Orkney",
                    Postcode = "KW15 1TH",
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
                Id = 35 + testid,
                Telephone = "+441851702256",
                Name = "Stornoway Airport- Isle of Lewis",
                Website = "www.hial.co.uk",
                Code = "SYY",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Stornoway Airport",
                    City = "Isle of Lewis",
                    Postcode = "HS2 0BN",
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
                Id = 36 + testid,
                Telephone = "+441950461000",
                Name = "Sumburgh Airport- Shetland Isles",
                Website = "www.hial.co.uk",
                Code = "LSI",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Sumburgh Airport",
                    City = "Shetland Isles",
                    Postcode = "ZE3 9JP",
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
                Id = 36 + testid,
                Telephone = "+441879220456",
                Name = "Tiree Airport - Island of Tiree",
                Website = "www.hial.co.uk",
                Code = "TRE",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Tiree  Airport",
                    City = "Isle of Tiree",
                    Postcode = "PA77 6UW",
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
                Id = 37 + testid,
                Telephone = "+441955602215",
                Name = "Wick Airport",
                Website = "www.hial.co.uk",
                Code = "WIC",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Highlands and Islands Airports Ltd",
                    Address2 = "Wick Airport",
                    City = "Caithness",
                    Postcode = "KW1 4QP",
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
                Id = 38 + testid,
                Telephone = "+442871810784",
                Name = "City of Derry Airport",
                Website = "www.cityofderryairport.com",
                Code = "LDY",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Airport Road",
                    Address2 = " Eglinton. Co.",
                    City = "Derry",
                    Postcode = "BT47 3GY",
                    Country = "Northern Ireland",
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
                Id = 39 + testid,
                Telephone = "+353214313131",
                Name = "Cork International Airport",
                Website = "www.corkairport.com",
                Code = "ORK",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Kinsale Road",
                    City = "Cork",
                    Country = "Ireland",
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
                Id = 40 + testid,
                Telephone = "+0749548284",
                Name = "Donegal Airport",
                Website = "www.donegalairport.ie",
                Code = "CFN",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Carrickfinn",
                    Address2= "Kincasslagh, Letterkenny, Co.",
                    City = "Donegal",
                    Country = "Ireland",
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
                Id = 41 + testid,
                Telephone = "+35318141111",
                Name = "Dublin Airport",
                Website = "www.dublinairport.ie",
                Code = "DUB",
                StatusId = 1,
                Address = new Address
                {
                    City = "Dublin",
                    Country = "Ireland",
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
                Id = 42 + testid,
                Telephone = "+35391755569",
                Name = "Galway Airport",
                Website = "www.galwayairport.com",
                Code = "GWY",
                StatusId = 1,
                Address = new Address
                {
                    City = "Galway",
                    Country = "Ireland",
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
                Id = 43 + testid,
                Telephone = "+351850672222",
                Name = "Knock International Airport",
                Website = "www.knockairport.com ",
                Code = "NOC",
                StatusId = 1,
                Address = new Address
                {
                    City = "Knock",
                    Country = "Ireland",
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
                Id = 44 + testid,
                Telephone = "+35361712000",
                Name = "Shannon International Airport",
                Website = "www.shannonairport.ie",
                Code = "SNN",
                StatusId = 1,
                Address = new Address
                {
                    City = "County Clare",
                    Country = "Ireland",
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
                Id = 45+ testid,
                Telephone = "+441446711111",
                Name = "Cardiff International Airport",
                Website = "www.cwlfly.com",
                Code = "CWL",
                StatusId = 1,
                Address = new Address
                {
                    Address1= "Vale of Glamorgan",
                    City = "Cardiff",
                    Postcode= "CF62 3BD",
                    Country = "UK",
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
                Id = 46 + testid,
                Telephone = "+441792204063",
                Name = "Swansea",
                Website = "www.swanseaairport.com",
                Code = "SWS",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Swansea Airport",
                    Address2= "Fairwood",
                    City = "Swansea",
                    Postcode = "SA2 7JU",
                    Country = "UK",
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
                Id = 47 + testid,
                Telephone = "+441253343434",
                Name = "Backpool",
                Website = "www.blackpoolairport.com ",
                Code = "BLK",
                StatusId = 1,
                Address = new Address
                {
                    City = "Backpool",
                    Country = "UK",
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
                Id = 48 + testid,
                Telephone = "+441624821600",
                Name = "Isle of Man - Ronaldsway",
                Website = "www.iom-airport.com",
                Code = "IOM",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Isle of Man Airport,Isle of Man",
                    Address2 = " British Isles",
                    City = "Ballasalla",
                    Postcode = "IM9 2AS",
                    Country = "UK",
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
                Id = 49 + testid,
                Telephone = "+448715218484",
                Name = "Liverpool John Lennon Airport",
                Website = "www.liverpoolairport.com",
                Code = "LIV",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Merseyside",
                    City = "Liverpool",
                    Postcode = "L24 1YD",
                    Country = "UK",
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
                Id = 50 + testid,
                Telephone = "+441325332811",
                Name = "Durham Tees Valley Airport",
                Website = "www.durhamteesvalleyairport.com",
                Code = "MME",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Darlington",
                    Address2 = "Tees Valley",
                    City = "Durham",
                    Postcode = "DL2 1LU",
                    Country = "UK",
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
                Id = 51 + testid,
                Telephone = "+441132509696",
                Name = "Leeds Bradford International Airport",
                Website = "www.lbia.co.uk",
                Code = "LBA",
                StatusId = 1,
                Address = new Address
                {
                    City = "Leeds",
                    Postcode = "LS19 7TU",
                    Country = "UK",
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
                Id = 52 + testid,
                Telephone = "+44870122488",
                Name = "Newcastle International Airport",
                Website = "www.newcastleairport.com",
                Code = "NCL",
                StatusId = 1,
                Address = new Address
                {

                    Address1= "Newcastle upon Tyne",
                    City = "Woolsington",
                    Postcode = "NE13 8BZ",
                    Country = "UK",
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
                Id = 53 + testid,
                Telephone = "+4408708332210",
                Name = "Robin Hood Airport Doncaster Sheffield",
                Website = "www.robinhoodairport.com",
                Code = "DSA",
                StatusId = 1,
                Address = new Address
                {

                    Address1 = "First Avenue",
                    City = "Doncaster",
                    Postcode = "DN9 3RH",
                    Country = "UK",
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
                Id = 54 + testid,
                Telephone = "+448707335511",
                Name = "Birmingham International Airport",
                Website = "www.birminghamairport.co.uk",
                Code = "BHX",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Birmingham International Airport Ltd",
                    City = "Birmingham",
                    Postcode = "B26 3QJ",
                    Country = "UK",
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
                Id = 55 + testid,
                Telephone = "+442476762220",
                Name = "Coventry Airport",
                Website = "www.coventryairport.co.uk",
                Code = "CVT",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Passenger Terminal",
                    Address2= "Siskin Parkway West",
                    City = "Coventry",
                    Postcode = "CV3 4PB",
                    Country = "UK",
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
                Id = 56 + testid,
                Telephone = "+441452857700",
                Name = "Gloucestershire Airport",
                Website = "www.coventryairport.co.uk",
                Code = "GLO",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Staverton",
                    Address2 = "Cheltenham",
                    City = "Gloucestershire",
                    Postcode = "GL51 6SR",
                    Country = "UK",
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
                Id = 56 + testid,
                Telephone = "+441959578500",
                Name = "London Biggin Hill Airport",
                Website = "www.bigginhillairport.com",
                Code = "BQH",
                StatusId = 1,
                Address = new Address
                {
                    Address1 = "Passenger & Executive Terminal",
                    Address2 = "Biggin Hill",
                    City = "Kent",
                    Postcode = "TN16 3BN",
                    Country = "UK",
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
