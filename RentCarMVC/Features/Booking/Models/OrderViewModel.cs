using RentCarMVC.Features.Cars;

namespace RentCarMVC.Features.Booking.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public byte StatusId { get; set; }
        public string OrderStatus { get; set; }
    }
}
