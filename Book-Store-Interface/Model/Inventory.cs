﻿namespace Book_Store_Interface.Model;

public partial class Inventory
{
    public int StoreId { get; set; }

    public string Isbn { get; set; } = null!;

    public int CurrentInventory { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
