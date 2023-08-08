using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentCarMVC.Features.Cars;
using RentCarMVC.Features.DriveTypes;

namespace RentCarMVC.Features.Transmissions
{
    [Table(nameof(Transmission))]
    public class Transmission
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required, Column(TypeName = "varchar(40)")]
        public string TransmissionName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
