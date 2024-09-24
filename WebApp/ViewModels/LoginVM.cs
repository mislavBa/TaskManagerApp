using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Provide username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Provide password")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password should have at least 8 characters")]
        public string Password { get; set; }
    }
}
