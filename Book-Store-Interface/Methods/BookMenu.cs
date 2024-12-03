using Book_Store_Interface.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Interface
{
    internal class BookMenu
    {
        private static void BookMenuChoices()
        {
            Console.Clear();
            CenterText("Book Menu");
            Console.WriteLine();
            CenterText("1. Add Book");
            CenterText("2. Edit Book");
            CenterText("3. Delete Book");
            CenterText("4. List Books");
            CenterText("5. Back");
            Console.WriteLine();
            CenterText("Enter your choice:");
        }

        public static void CenterText(string text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }

        public static void BooksMenu()
        {
            BookMenuChoices();
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "5")
            {
                switch (menuChoice)
                {
                    case "1":
                        //AddBook();
                        break;
                    case "2":
                        //EditBook();
                        break;
                    case "3":
                        //DeleteBook();
                        break;
                    case "4":
                        ListBooks();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                BookMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        private static void ListBooks()
        {
            Console.Clear();
            CenterText("List of Books");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var books = context.Books.Include(b => b.Publisher).Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Publisher: {book.Publisher.Name}");
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
