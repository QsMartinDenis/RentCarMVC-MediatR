using Microsoft.Build.Framework;

namespace RentCarMVC.Features.VehicleTypes.Models
{
    public class VehicleTypeViewModel
    {
        public byte Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public int Seats { get; set; }
    }
}
