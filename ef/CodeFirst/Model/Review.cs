using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Review
{
    public int RevId { get; set; }

    public int RevProduct { get; set; }

    public int RevAuthor { get; set; }

    public string RevComment { get; set; } = null!;

    public short RevRating { get; set; }

    public string RevTitle { get; set; } = null!;

    public DateOnly RevDate { get; set; }

    public virtual User RevAuthorNavigation { get; set; } = null!;

    public virtual Product RevProductNavigation { get; set; } = null!;
}
