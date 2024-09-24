using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class TakesTask
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }

    public DateTime SelectedAt { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
