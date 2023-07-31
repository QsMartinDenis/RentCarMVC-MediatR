using System.ComponentModel.DataAnnotations;

namespace RentCarMVC.Features.Account.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password), MinLength(1)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password), MinLength(1)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
