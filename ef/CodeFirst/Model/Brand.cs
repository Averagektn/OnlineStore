using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Brand
{
    public int BraId { get; set; }

    public string? BraName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
