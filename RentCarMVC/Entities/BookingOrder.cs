using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarMVC.Entities
{
    public class BookingOrder
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [Required, Column(TypeName = "decimal(8,2)")]
        public decimal TotalAmount { get; set; }

        [ForeignKey("Status")]
        public byte StatusId { get; set; }
        public Status Status { get; set; }
    }
}
