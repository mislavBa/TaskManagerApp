﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TaskSkill> TaskSkills { get; } = new List<TaskSkill>();
}
