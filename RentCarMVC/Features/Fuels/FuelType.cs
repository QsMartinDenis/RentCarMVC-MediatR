using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentCarMVC.Features.Cars;
using RentCarMVC.Features.DriveTypes;

namespace RentCarMVC.Features.Fuels
{
    [Table(nameof(FuelType))]
    public class FuelType
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string FuelName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
