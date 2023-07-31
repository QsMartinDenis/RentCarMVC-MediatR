using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarMVC.Features.Fuels.Models
{
    public class FuelTypeViewModel
    {
        public byte Id { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string FuelName { get; set; }
    }
}
