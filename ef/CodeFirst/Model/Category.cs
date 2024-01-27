using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Category
{
    public int CatId { get; set; }

    public int CatParent { get; set; }

    public string CatName { get; set; } = null!;

    public virtual Category CatParentNavigation { get; set; } = null!;

    public virtual ICollection<Category> InverseCatParentNavigation { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Section> SectionSecs { get; set; } = new List<Section>();
}
