namespace Book_Store_Interface.Model;

public partial class OrderDetail
{
    public string Id { get; set; } = null!;

    public int OrderId { get; set; }

    public string OrderItem { get; set; } = null!;

    public int ProductId { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Book OrderItemNavigation { get; set; } = null!;
}
