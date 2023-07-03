using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Row
{
    public int RowId { get; set; }

    public string Material { get; set; } = null!;

    public int Quantity { get; set; }

    public float Price { get; set; }

    public virtual MaterialMaster MaterialNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
