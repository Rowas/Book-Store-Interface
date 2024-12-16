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
            using (var context = new Labb1BokhandelDemoContext())
            {
                Console.WriteLine("Add new book");
                ListAuthors.ListAuthor();
                Console.WriteLine();
                Console.Write("Enter ID of the author from the list above: ");
                int bookAuthor = int.Parse(Console.ReadLine());
                var foundAuthor = context.Authors.Find(bookAuthor);
                if (foundAuthor == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Author not found, or no author entered.");
                    Console.WriteLine("Author required before adding a new book.");
                    Console.WriteLine("Use \"Author Menu\" to add the Author before adding a new book");
                    return;
                }
                //----------------------------------------------------------------------------------------------------------
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
                ListPublishers.ListPublisher();
                Console.WriteLine();
                Console.Write("Enter ID of the Publisher from above list (if not in list, leave blank): ");
                var publisherID = int.Parse(Console.ReadLine());
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

                var authorsBook = new BooksAuthor
                {
                    BooksAuthorsId = default,
                    AuthorsId = foundAuthor.Id,
                    BooksId = isbn
                };
                context.Add(authorsBook);
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
            var selectedBook = ChangeBook();
            string isbn = selectedBook.Isbn13;
            using (var context = new Labb1BokhandelDemoContext())
            {
                var book = context.Books.Find(isbn);
                if (book == null) { Console.WriteLine("Invalid ISBN or no value entered."); Console.WriteLine("Returning."); return; }
                selectedBook = book;
            }
            MenuChoices.EditBookMenu();
            TextCenter.CenterText($"Current Active Book: {selectedBook.Title}");
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 2, 11);
            string choice = Console.ReadLine();
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        using (var context = new Labb1BokhandelDemoContext())
                        {
                            Console.WriteLine();
                            ListPublishers.ListPublisher();
                            Console.WriteLine($"Current Publisher: {context.Publishers.Find(selectedBook.PublisherId).Name}");
                            context.Attach(selectedBook);
                            Console.Write("Enter new Publisher ID from list above: ");
                            int pubID = int.Parse(Console.ReadLine());
                            selectedBook.PublisherId = pubID;
                            context.Entry(selectedBook).Property("PublisherID").IsModified = true;
                            Console.WriteLine();
                            Console.WriteLine("Book updated. ");
                            context.SaveChanges();
                        }
                        break;
                    case "2":
                        using (var context = new Labb1BokhandelDemoContext())
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Edit price of {selectedBook.Title}");
                            Console.WriteLine($"Current price: {selectedBook.Price}");
                            context.Attach(selectedBook);
                            Console.Write("Enter new price: ");
                            int newPrice = int.Parse(Console.ReadLine());
                            selectedBook.Price = newPrice;
                            context.Entry(selectedBook).Property("Price").IsModified = true;
                            Console.WriteLine();
                            Console.WriteLine("Book updated. ");
                            context.SaveChanges();
                        }
                        break;
                    case "3":
                        using (var context = new Labb1BokhandelDemoContext())
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Add author to book {selectedBook.Title}");
                            Console.WriteLine();
                            ListAuthors.ListAuthor();
                            Console.Write($"Enter ID of author from list above to add as author of {selectedBook.Title}: ");
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
                            Console.WriteLine();
                            Console.WriteLine("Book updated. ");
                            context.SaveChanges();
                        }
                        break;
                    case "4":
                        using (var context = new Labb1BokhandelDemoContext())
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Add author to book {selectedBook.Title}");
                            Console.WriteLine();
                            ListAuthors.ListAuthor();
                            Console.Write($"Enter ID of author from list above to remove as author of {selectedBook.Title}: ");
                            int raID = int.Parse(Console.ReadLine());
                            var removeBookAuthor = context.BooksAuthors.FirstOrDefault(ba => ba.BooksId == isbn && ba.AuthorsId == raID);
                            if (removeBookAuthor == null)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Author not associated with {selectedBook.Title}. ");
                                Console.WriteLine("Returning with no changes made. ");
                                return;
                            }
                            else
                            {
                                context.Remove(removeBookAuthor);
                                if (context.BooksAuthors.Where(ba => ba.BooksId == isbn).ToString().Length == 0)
                                {
                                    Console.WriteLine($"No Author(s) associated with {selectedBook.Title}.");
                                    Console.WriteLine("Consider adding an author.");
                                }
                            }
                            Console.WriteLine();
                            Console.WriteLine("Book updated. ");
                            context.SaveChanges();
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                MenuChoices.EditBookMenu();
                TextCenter.CenterText($"Current Active Book: {selectedBook.Title}");
                Console.WriteLine();
                Console.SetCursorPosition(Console.WindowWidth / 2, 11);
                choice = Console.ReadLine();
            }
        }

        private static Book? ChangeBook()
        {
            ClearConsole.ConsoleClear();
            ListBooks.ListBook();
            Console.Write("Enter book ISBN to edit: ");
            string isbn = Console.ReadLine();
            Book? activeBook = null;
            using (var context = new Labb1BokhandelDemoContext())
            {
                var book = context.Books.Find(isbn);
                if (book == null) { Console.WriteLine("Invalid ISBN or no value entered."); Console.WriteLine("Returning."); }
                activeBook = book;
            }
            return activeBook;
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
