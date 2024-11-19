﻿using System;
using System.Collections.Generic;

namespace ComputerAPI.Models;

public partial class Comp
{
    public Guid Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Type { get; set; } = null!;

    public double Display { get; set; }

    public int Memory { get; set; }

    public DateTime CreatedTime { get; set; }

    public Guid OsId { get; set; }

    public virtual OSystem? Os { get; set; } = null!;
}
