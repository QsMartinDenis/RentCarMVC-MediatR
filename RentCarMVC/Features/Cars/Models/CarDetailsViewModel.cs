using RentCarMVC.Entities;

namespace RentCarMVC.Features.Cars.Models
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }

        public byte BrandId { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

        public byte VehicleTypeId { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }

        public byte TransmissionId { get; set; }
        public IEnumerable<Transmission> Transmissions { get; set; }

        public byte DriveId { get; set; }
        public IEnumerable<Drive> Drives { get; set; }

        public byte FuelTypeId { get; set; }
        public IEnumerable<FuelType> FuelTypes { get; set; }

        public byte[] Image { get; set; }
        public IFormFile formFile { get; set; }
    }
}
