using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class MaterialType
{
    public string MaterialTypeName { get; set; } = null!;

    public virtual ICollection<MaterialMaster> MaterialMasters { get; set; } = new List<MaterialMaster>();
}
