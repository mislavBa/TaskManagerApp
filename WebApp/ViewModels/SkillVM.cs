using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class SkillVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide skill name")]
        [Display(Name="Skill")]
        [StringLength(50, MinimumLength = 2, ErrorMessage ="Skill name should have between 2 and 500 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Provide skill description")]
        [Display(Name = "Skill description")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Description should have at least 8 characters")]
        public string? Description { get; set; }
    }
}
