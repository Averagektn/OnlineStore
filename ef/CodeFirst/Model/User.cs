using System;
using System.Collections.Generic;

namespace CodeFirst.Model;

public partial class User
{
    public int UsrId { get; set; }

    public string UsrEmail { get; set; } = null!;

    public string UsrPassword { get; set; } = null!;

    public string UsrPhone { get; set; } = null!;

    public string UsrFirstname { get; set; } = null!;

    public string UsrLastname { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
