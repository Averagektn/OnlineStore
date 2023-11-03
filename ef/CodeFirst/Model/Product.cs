using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Product
{
    public int ProId { get; set; }

    public int ProBrand { get; set; }

    public int ProCategory { get; set; }

    public string ProName { get; set; } = null!;

    public decimal ProPrice { get; set; }

    public int ProAverageRating { get; set; }

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual Brand ProBrandNavigation { get; set; } = null!;

    public virtual Category ProCategoryNavigation { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
