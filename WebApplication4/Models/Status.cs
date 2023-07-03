using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Status
{
    public string NameStatus { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
