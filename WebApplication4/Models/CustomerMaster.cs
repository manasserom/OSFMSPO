using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class CustomerMaster
{
    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
