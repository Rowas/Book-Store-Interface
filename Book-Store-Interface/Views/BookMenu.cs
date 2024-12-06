using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Model;

namespace Book_Store_Interface
{
    internal class BookMenu
    {
        public static void BooksMenu()
        {
            MenuChoices.BookMenuChoices();
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "5")
            {
                switch (menuChoice)
                {
                    case "1":
                        AddNewBook();
                        break;
                    case "2":
                        //EditBook();
                        break;
                    case "3":
                        DeleteBook();
                        break;
                    case "4":
                        ListBooks.ListBook();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                MenuChoices.BookMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        private static void DeleteBook()
        {
            ClearConsole.ConsoleClear();
            Console.WriteLine("Delete Book");
            ListBooks.ListBook();
            Console.Write("Enter book ISBN to remove: ");
            string isbn = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var books = context.Books.ToList();
                var selectedItem = books.Where(b => b.Isbn13 == isbn).FirstOrDefault();
                if (selectedItem == null)
                {
                    Console.WriteLine("Book not found.");
                }
                else
                {
                    context.Remove(selectedItem);
                    context.SaveChanges();
                    Console.WriteLine($"Book {selectedItem.Title} ({selectedItem.Isbn13}) have been removed.");
                }
            }

        }

        private static void AddNewBook()
        {
            Console.WriteLine("Add new book");
            Console.WriteLine();
            Console.Write("Enter ISBN for the new book ( Format: ISBN13, no dashes ): ");
            string isbn = Console.ReadLine();
            Console.Write("Enter Title for the new book: ");
            string title = Console.ReadLine();
            Console.Write("Enter language of the book: ");
            string language = Console.ReadLine();
            Console.Write("Enter the price for the new book: ");
            int price = int.Parse(Console.ReadLine());
            Console.Write("Enter the year the book was published: ");
            int publishyear = int.Parse(Console.ReadLine());
            Console.Write("Is the book part of a series? ");
            string isSeries = Console.ReadLine();
            string series;
            string? seriesPart;
            switch (isSeries.ToLower())
            {
                case "yes":
                    Console.Write("Enter name of the series: ");
                    series = Console.ReadLine();
                    Console.Write("Enter the number in the series: ");
                    seriesPart = Console.ReadLine();
                    break;
                default:
                    series = null;
                    seriesPart = null;
                    break;
            }
            ListAuthors.ListAuthor();
            Console.WriteLine();
            Console.Write("Enter name of the author from the list above (Format: Firstname Lastname): ");
            string bookAuthor = Console.ReadLine();
            string[] authorName = bookAuthor.Split(" ");
            ListPublishers.ListPublisher();
            Console.WriteLine();
            Console.Write("Enter name of the Publisher from above list (if not in list, leave blank): ");
            string publisher = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                int pubID;
                var foundPublisher = context.Publishers.Where(p => p.Name == publisher).FirstOrDefault();
                if (foundPublisher != null)
                {
                    pubID = foundPublisher.PubId;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Publisher not found or left blank. Setting default.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    pubID = 1;
                }

                var book = new Book
                {
                    Isbn13 = isbn,
                    Title = title,
                    Language = language,
                    Price = price,
                    PublishYear = publishyear,
                    Series = series,
                    SeriesPart = string.IsNullOrWhiteSpace(seriesPart) ? (int?)null : int.Parse(seriesPart),
                    PublisherId = pubID,
                };

                var foundAuthor = context.Authors.Where(a => a.FirstName == authorName[0] && a.LastName == authorName[1]).FirstOrDefault();
                if (foundAuthor != null && foundAuthor.FirstName == authorName[0] && foundAuthor.LastName == authorName[1])
                {
                    var booksAuthor = context.BooksAuthors.Where(ba => ba.BooksId == null && ba.AuthorsId == foundAuthor.Id).FirstOrDefault();
                    if (booksAuthor == null)
                    {
                        var authorsBook = new BooksAuthor
                        {
                            BooksAuthorsId = default,
                            AuthorsId = foundAuthor.Id,
                            BooksId = isbn
                        };
                        context.Add(authorsBook);
                    }
                    else
                    {
                        booksAuthor.BooksId = isbn;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Author not found, or no author entered.");
                    Console.WriteLine("Press any key to continue...");
                    int? id = null;
                    var booksAuthor = new BooksAuthor
                    {
                        BooksAuthorsId = default,
                        AuthorsId = (int)id,
                        BooksId = isbn
                    };
                    context.Add(booksAuthor);
                }
                context.Add(book);
                Console.WriteLine();
                Console.WriteLine("Book Added.");
                context.SaveChanges();
            }
        }
    }
}
