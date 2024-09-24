using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Provide password")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password should have at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Provide first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Provide last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Provide email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provide phone number")]
        public string Phone { get; set; }
    }
}
