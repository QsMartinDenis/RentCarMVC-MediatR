namespace RentCarMVC.Features.Users.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string?> Role { get; set; }
    }
}
