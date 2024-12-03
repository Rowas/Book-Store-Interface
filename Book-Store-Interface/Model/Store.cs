namespace Book_Store_Interface.Model;

public partial class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? ContactEmail { get; set; }

    public string Address { get; set; } = null!;

    public int PostalCode { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
