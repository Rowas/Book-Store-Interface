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
                        EditBook();
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
            Console.Write("Enter ID of the author from the list above: ");
            int bookAuthor = int.Parse(Console.ReadLine());
            ListPublishers.ListPublisher();
            Console.WriteLine();
            Console.Write("Enter ID of the Publisher from above list (if not in list, leave blank): ");
            var publisherID = int.Parse(Console.ReadLine());
            using (var context = new Labb1BokhandelDemoContext())
            {
                int pubID;
                var foundPublisher = context.Publishers.Find(publisherID);
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

                var foundAuthor = context.Authors.Find(bookAuthor);
                if (foundAuthor != null && bookAuthor == foundAuthor.Id)
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

        private static void EditBook()
        {
            ClearConsole.ConsoleClear();
            Console.WriteLine("Edit Book");
            ListBooks.ListBook();
            Console.Write("Enter book ISBN to edit: ");
            string isbn = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var book = context.Books.Find(isbn);
                if (book == null) { Console.WriteLine("Invalid ISBN or no value entered."); Console.WriteLine("Returning."); return; }
                MenuChoices.EditBookMenu();
                Console.WriteLine();
                Console.SetCursorPosition(Console.WindowWidth / 2, 10);
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        ListPublishers.ListPublisher();
                        Console.WriteLine($"Current Publisher: {context.Publishers.Find(book.PublisherId).Name}");
                        Console.Write("Enter new Publisher ID from list above: ");
                        int pubID = int.Parse(Console.ReadLine());
                        book.PublisherId = pubID;
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine($"Edit price of {book.Title}");
                        Console.WriteLine($"Current price: {book.Price}");
                        Console.Write("Enter new price: ");
                        int newPrice = int.Parse(Console.ReadLine());
                        book.Price = newPrice;
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.WriteLine($"Add author to book {book.Title}");
                        Console.WriteLine();
                        ListAuthors.ListAuthor();
                        Console.Write($"Enter ID of author from list above to add as author of {book.Title}: ");
                        int aaID = int.Parse(Console.ReadLine());
                        var addBookAuthor = context.BooksAuthors.FirstOrDefault(ba => ba.BooksId == isbn && ba.AuthorsId == aaID);
                        if (addBookAuthor != null)
                        {
                            Console.WriteLine("Author already associated with selected book.");
                            Console.WriteLine("Returning with no changes made. ");
                            return;
                        }
                        else
                        {
                            var authorbook = new BooksAuthor
                            {
                                AuthorsId = aaID,
                                BooksId = isbn
                            };
                            context.BooksAuthors.Add(authorbook);
                        }
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine($"Add author to book {book.Title}");
                        Console.WriteLine();
                        ListAuthors.ListAuthor();
                        Console.Write($"Enter ID of author from list above to remove as author of {book.Title}: ");
                        int raID = int.Parse(Console.ReadLine());
                        var removeBookAuthor = context.BooksAuthors.FirstOrDefault(ba => ba.BooksId == isbn && ba.AuthorsId == raID);
                        if (removeBookAuthor == null)
                        {
                            Console.WriteLine($"Author not associated with {book.Title}. ");
                            Console.WriteLine("Returning with no changes made. ");
                            return;
                        }
                        else
                        {
                            context.Remove(removeBookAuthor);
                            if (context.BooksAuthors.Where(ba => ba.BooksId == isbn).ToString().Length == 0)
                            {
                                Console.WriteLine($"No Author(s) associated with {book.Title}.");
                                Console.WriteLine("Consider adding an author.");
                            }
                            Console.WriteLine("Book updated. ");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. ");
                        Console.WriteLine("Returning with no changes made. ");
                        Console.WriteLine("Press any key to continue... ");
                        Console.ReadKey();
                        return;


                }
                context.SaveChanges();
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
                    var manymanybook = context.BooksAuthors.FirstOrDefault(ba => ba.BooksId == selectedItem.Isbn13);
                    context.Remove(selectedItem);
                    context.BooksAuthors.Remove(manymanybook);
                    context.SaveChanges();
                    Console.WriteLine($"Book {selectedItem.Title} ({selectedItem.Isbn13}) have been removed.");
                }
            }

        }
    }
}
