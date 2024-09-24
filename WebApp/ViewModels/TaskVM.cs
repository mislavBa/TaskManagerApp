using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class TaskVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide task name")]
        [Display(Name = "Task")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Task name should be between 2 and 50 characters long")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Provide task description")]
        [Display(Name = "Task description")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Task description should be at least 8 characters long")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Provide date")]
        [Display(Name="Date when task was created")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Provide date")]
        [Display(Name="Date when task is due")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage ="Provide ManagerID")]
        [Display(Name="Manager")]
        [RegularExpression("([1-9]+)", ErrorMessage = "ManagerID should be integer, starting with 1")]
        public int ManagerId { get; set; }

        public IEnumerable<Manager> Managers { get; set; }
    }
}
