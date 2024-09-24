using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(5);

        [Required(ErrorMessage = "Please enter Manager ID")]
        public int ManagerId { get; set; }
    }
}
