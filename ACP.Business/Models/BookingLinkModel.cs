namespace ACP.Business.Models
{
    public class BookingLinkModel:BaseModel
    {
        public int BookingId { get; set; }
        public int SlotId { get; set; }
        public int AvailabilityId { get; set; }

        public virtual BookingModel Booking { get; set; }
        public virtual SlotModel Slot { get; set; }
        public virtual AvailabilityModel Availability { get; set; }
    }
}