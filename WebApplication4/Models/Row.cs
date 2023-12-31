﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Row
{
    public int RowId { get; set; }

    public string Material { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int? OrderId { get; set; }

    public virtual MaterialMaster MaterialNavigation { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
