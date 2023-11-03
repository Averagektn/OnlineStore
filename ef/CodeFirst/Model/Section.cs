using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class Section
{
    public int SecId { get; set; }

    public string SecName { get; set; } = null!;

    public virtual ICollection<Category> CategoryCats { get; set; } = new List<Category>();
}
