namespace Book_Store_Interface.Model;

public partial class Publisher
{
    public int PubId { get; set; }

    public string Name { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
