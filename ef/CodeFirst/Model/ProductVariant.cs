using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class ProductVariant
{
    public int PrvId { get; set; }

    public int PrvColor { get; set; }

    public int PrvSize { get; set; }

    public int PrvProduct { get; set; }

    public int PrvQuantity { get; set; }

    public string PrvSku { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderProductVariant> OrderProductVariants { get; set; } = new List<OrderProductVariant>();

    public virtual Color PrvColorNavigation { get; set; } = null!;

    public virtual Product PrvProductNavigation { get; set; } = null!;

    public virtual Size PrvSizeNavigation { get; set; } = null!;
}
