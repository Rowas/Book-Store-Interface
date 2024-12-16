using Book_Store_Interface.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Interface.GeneralMethods
{
    internal class ListBooks
    {
        public static void ListBook()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("List of Books");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var books = context.Books.Include(b => b.Publisher).Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                if (books.Count == 0)
                {
                    TextCenter.CenterText("No books found.");
                }
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"ISBN: {book.Isbn13}");
                    Console.WriteLine($"Publisher: {book.Publisher.Name}");
                    Console.WriteLine($"Current Price: {book.Price} SEK");
                    Console.WriteLine("Authors:");
                    foreach (var author in book.BooksAuthors)
                    {
                        Console.WriteLine($"- {author.Authors.FirstName} {author.Authors.LastName}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
