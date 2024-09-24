using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Manager
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
