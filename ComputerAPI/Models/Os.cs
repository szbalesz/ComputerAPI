using System;
using System.Collections.Generic;

namespace ComputerAPI.Models;

public partial class Os
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Comp> Comps { get; set; } = new List<Comp>();
}
