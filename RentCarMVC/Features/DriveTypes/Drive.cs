﻿using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.Cars;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarMVC.Features.DriveTypes
{
    [Table(nameof(Drive))]
    public class Drive
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required, Column(TypeName = "varchar(30)")]
        public string DriveName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
