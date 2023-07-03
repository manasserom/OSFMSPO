using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class MaterialMaster
{
    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string MaterialType { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual MaterialType MaterialTypeNavigation { get; set; } = null!;

    public virtual ICollection<Row> Rows { get; set; } = new List<Row>();
}
