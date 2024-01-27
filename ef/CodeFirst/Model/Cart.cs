using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Cart
{
    public int CrtUser { get; set; }

    public int CrtProductVariant { get; set; }

    public short CrtQuantity { get; set; }

    public virtual ProductVariant CrtProductVariantNavigation { get; set; } = null!;

    public virtual User CrtUserNavigation { get; set; } = null!;
}
