using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Medium
{
    public int MedId { get; set; }

    public int MedProduct { get; set; }

    public byte[] MedBytes { get; set; } = null!;

    public string MedFiletype { get; set; } = null!;

    public string MedFilename { get; set; } = null!;

    public virtual Product MedProductNavigation { get; set; } = null!;
}
