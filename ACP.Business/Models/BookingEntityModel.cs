﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class BookingEntityModel: BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Code { get; set; }
        public byte[] Image { get; set; }
        public byte[] Logo { get; set; }
        public decimal Price { get; set; }
        public decimal Comission { get; set; }
        public bool Sameday { get; set; }
        public int RootBookEntityId { get; set; }
        public EntityType EntityType { get; set; }

        public virtual ICollection<PropertyModel> Properties { get; set; }        

        public virtual RootBookingEntityModel RootBookingEntity { get; set; }
        public virtual ICollection<ExtraModel> Extras { get; set; }
        public virtual ICollection<BookingPricingModel> Prices { get; set; }
        public virtual AddressModel Address { get; set; }
        public virtual ICollection<BookingServiceModel> Service { get; set; }
        public virtual ICollection<SlotModel> Slot { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
    }
}
