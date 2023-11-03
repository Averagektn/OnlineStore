using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Color
{
    public int ColId { get; set; }

    public string ColName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
