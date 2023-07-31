using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarMVC.Entities
{
    public class VehicleType
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required, Column(TypeName = "varchar(40)")]
        public string TypeName { get; set; }
        public int Seats { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
