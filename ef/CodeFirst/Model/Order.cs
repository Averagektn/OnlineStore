using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Order
{
    public int OrdId { get; set; }

    public int OrdUser { get; set; }

    public int OrdAddress { get; set; }

    public decimal OrdPrice { get; set; }

    public DateOnly OrdDate { get; set; }

    public virtual Address OrdAddressNavigation { get; set; } = null!;

    public virtual User OrdUserNavigation { get; set; } = null!;

    public virtual ICollection<OrderProductVariant> OrderProductVariants { get; set; } = new List<OrderProductVariant>();
}
