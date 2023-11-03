using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class OrderTransaction
{
    public int OrtId { get; set; }

    public DateOnly OrtUpdatedAt { get; set; }

    public virtual Order Ort { get; set; } = null!;
}
