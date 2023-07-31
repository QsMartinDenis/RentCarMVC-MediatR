using RentCarMVC.Entities;

namespace RentCarMVC.Features.Cars.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public int Year { get; set; }

        public decimal PricePerDay { get; set; }

        public byte BrandId { get; set; }
        public Brand Brand { get; set; }

        public byte VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public byte TransmissionId { get; set; }
        public Transmission Transmission { get; set; }

        public byte DriveId { get; set; }
        public Drive Drive { get; set; }

        public byte FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        public byte[] Image { get; set; }
    }
}
