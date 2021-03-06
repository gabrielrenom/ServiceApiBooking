﻿using ACP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{    
    public class RootBookingEntityModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string Code { get; set; }
        public string Website { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual IList<BookingEntityModel> BookingEntities { get; set; }
        public virtual IList<RootBookingPropertyModel> Properties { get; set; }
    }
}
