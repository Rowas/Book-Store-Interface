namespace Book_Store_Interface.Model;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? Initial { get; set; }

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public bool IsDead { get; set; }

    public virtual ICollection<BooksAuthor> BooksAuthors { get; set; } = new List<BooksAuthor>();
}
