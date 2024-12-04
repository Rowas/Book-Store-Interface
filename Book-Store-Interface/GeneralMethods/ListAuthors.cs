using Book_Store_Interface.Model;

namespace Book_Store_Interface.GeneralMethods
{
    internal class ListAuthors
    {
        public static void ListAuthor()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Authors");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var authors = context.Authors.ToList();
                if (authors.Count == 0)
                {
                    TextCenter.CenterText("No authors found.");
                }
                else
                {
                    foreach (var author in authors)
                    {
                        Console.WriteLine($"{author.FirstName} {author.LastName}");
                    }
                }
            }
        }
    }
}
