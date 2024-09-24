using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Provide password")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password should have at least 8 characters")]
        public string Password { get; set; } = null;

        public string PwdHash { get; set; } = null!;

        public string PwdSalt { get; set; } = null!;

        [Required(ErrorMessage = "Provide first name")]
        [Display(Name ="First name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Provide last name")]
        [Display(Name ="Last name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Provide email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Provide phone number")]
        public string? Phone { get; set; }
    }
}
