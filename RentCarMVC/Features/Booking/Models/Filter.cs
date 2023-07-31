namespace RentCarMVC.Features.Booking.Models
{
    public class Filter
    {
        public string OrderBy { get; set; }
        public List<string> Brand { get; set; }
        public List<string> VehicleType { get; set; }
        public List<string> Transmission { get; set; }
        public List<string> Drive { get; set; }
        public List<string> FuelType { get; set; }

        public bool IsValid()
        {
            return OrderBy != null || Brand != null || VehicleType != null
                   || Transmission != null || Drive != null || FuelType != null;
        }
    }
}
