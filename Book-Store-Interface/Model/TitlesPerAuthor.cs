namespace Book_Store_Interface.Model;

public partial class TitlesPerAuthor
{
    public string? Name { get; set; }

    public string? Age { get; set; }

    public int? Titles { get; set; }

    public string TotalInventoryValue { get; set; } = null!;
}
