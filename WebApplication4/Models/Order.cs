using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Customer { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public virtual CustomerMaster CustomerNavigation { get; set; } = null!;

    public virtual ICollection<Row> Rows { get; set; } = new List<Row>();

    public virtual Status StatusNavigation { get; set; } = null!;
}
