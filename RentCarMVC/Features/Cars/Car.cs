using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentCarMVC.Entities;
using RentCarMVC.Features.Brands;
using RentCarMVC.Features.DriveTypes;
using RentCarMVC.Features.Fuels;
using RentCarMVC.Features.Transmissions;
using RentCarMVC.Features.VehicleTypes;

namespace RentCarMVC.Features.Cars
{
    [Table(nameof(Car))]
    public class Car
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string CarName { get; set; }
        public int Year { get; set; }

        [Required, Column(TypeName = "decimal(8,2)")]
        public decimal PricePerDay { get; set; }

        [ForeignKey("Brand")]
        public byte BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("VehicleType")]
        public byte VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        [ForeignKey("Transmission")]
        public byte TransmissionId { get; set; }
        public Transmission Transmission { get; set; }

        [ForeignKey("Drive")]
        public byte DriveId { get; set; }
        public Drive Drive { get; set; }

        [ForeignKey("FuelType")]
        public byte FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        [Column(TypeName = "varbinary(Max)")]
        public byte[] Image { get; set; }

        public virtual ICollection<BookingOrder> BookingOrders { get; set; }
    }
}
