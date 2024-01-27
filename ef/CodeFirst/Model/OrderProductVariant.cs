using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class OrderProductVariant
{
    public int OpvOrder { get; set; }

    public int OpvProductVariant { get; set; }

    public short OpvQuantity { get; set; }

    public virtual Order OpvOrderNavigation { get; set; } = null!;

    public virtual ProductVariant OpvProductVariantNavigation { get; set; } = null!;
}
