using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class ManagerVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide first name")]
        [Display(Name = "First name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters long")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage ="Provide last name")]
        [Display(Name ="Last name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters long")]
        public string LastName { get; set; } = null!;
    }
}
