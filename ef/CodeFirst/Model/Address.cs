using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Address
{
    public int AdrId { get; set; }

    public int AdrUser { get; set; }

    public string AdrAddress { get; set; } = null!;

    public int AdrPostcode { get; set; }

    public virtual User AdrUserNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
