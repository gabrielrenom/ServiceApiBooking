using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Models
{
    public class AirportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }
        public virtual AddressViewModel Address { get; set; }
        public virtual StatusViewModel Status { get; set; }
        //public virtual ICollection<BookingEntityModel> BookingEntities { get; set; }
    }
}
