using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime DueDate { get; set; }

    public int ManagerId { get; set; }

    public virtual Manager Manager { get; set; } = null!;

    public virtual ICollection<TakesTask> TakesTasks { get; } = new List<TakesTask>();

    public virtual ICollection<TaskSkill> TaskSkills { get; } = new List<TaskSkill>();
}
