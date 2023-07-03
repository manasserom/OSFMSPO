using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Customer { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public int RowId { get; set; }

    public virtual CustomerMaster CustomerNavigation { get; set; } = null!;

    public virtual Row Row { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
