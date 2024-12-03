namespace Book_Store_Interface.Model;

public partial class BooksAuthor
{
    public int BooksAuthorsId { get; set; }

    public int AuthorsId { get; set; }

    public string BooksId { get; set; } = null!;

    public virtual Author Authors { get; set; } = null!;

    public virtual Book Books { get; set; } = null!;
}
