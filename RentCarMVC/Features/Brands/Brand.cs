using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentCarMVC.Features.Cars;

namespace RentCarMVC.Features.Brands
{
    [Table(nameof(Brand))]
    public class Brand
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string BrandName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
