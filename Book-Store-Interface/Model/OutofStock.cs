namespace Book_Store_Interface.Model;

public partial class OutofStock
{
    public string? Store { get; set; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Isbn { get; set; }

    public int? CurrentInventory { get; set; }

    public string? Publisher { get; set; }

    public string? OrderFrom { get; set; }

    public string? DaysOutOfStock { get; set; }
}
