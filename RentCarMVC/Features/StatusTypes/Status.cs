using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentCarMVC.Entities;
using RentCarMVC.Features.DriveTypes;

namespace RentCarMVC.Features.StatusTypes
{
    [Table(nameof(Status))]
    public class Status
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string StatusName { get; set; }

        public virtual ICollection<BookingOrder> BookingOrders { get; set; }
    }
}
