namespace CarRentingSystem.Identity.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterInputModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [MinLength(5)]
        public string FullName { get; set; }
    }
}
