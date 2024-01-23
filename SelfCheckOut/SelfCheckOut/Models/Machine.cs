using System;
using System.Collections.Generic;

namespace SelfCheckOutAPI.Models;

public partial class Machine
{
    public int Id { get; set; }

    public int StoreId { get; set; }

    public string? Code { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ShopStore Store { get; set; } = null!;
}
