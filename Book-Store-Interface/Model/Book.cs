namespace Book_Store_Interface.Model;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Language { get; set; } = null!;

    public int Price { get; set; }

    public int PublishYear { get; set; }

    public string? Series { get; set; }

    public int? SeriesPart { get; set; }

    public int PublisherId { get; set; }

    public int ProductId { get; set; }

    public DateTime LastChangeTime { get; set; }

    public virtual ICollection<BooksAuthor> BooksAuthors { get; set; } = new List<BooksAuthor>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Publisher Publisher { get; set; } = null!;
}
