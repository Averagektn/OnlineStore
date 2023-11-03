using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Size
{
    public int SizId { get; set; }

    public string SizName { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
