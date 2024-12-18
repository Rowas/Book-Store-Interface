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
            try
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
                    Console.WriteLine();
                    bool isValid = false;
                    string isbn = null;
                    while (isValid != true)
                    {
                        Console.Write("Enter ISBN for the new book ( Format: ISBN13, no dashes ): ");
                        isbn = Console.ReadLine();
                        if (context.Books.Any(b => b.Isbn13 == isbn))
                        {
                            TextCenter.CenterText("Book already exists.");
                            TextCenter.CenterText("Returning to menu.");
                            return;
                        }
                        if (isbn.Length == 13)
                        {
                            List<int> isbnValid = new List<int>();
                            List<string> isbnSplitList = new List<String>();
                            foreach (char c in isbn)
                            {
                                isbnSplitList.Add(c.ToString());
                            }
                            isbnValid = isbnSplitList.Select(int.Parse).ToList();
                            //isbnValid.ForEach = int.Parse(isbnSplitList);
                            //isbnSplitList.ForEach(int.Parse());
                            var checkNum = isbnValid[12];
                            isbnValid.RemoveAt(12);
                            int even = isbnValid[0] + isbnValid[2] + isbnValid[4] + isbnValid[6] + isbnValid[8] + isbnValid[10];
                            int odd = isbnValid[1] + isbnValid[3] + isbnValid[5] + isbnValid[7] + isbnValid[9] + isbnValid[11];
                            even = even * 1;
                            odd = odd * 3;
                            int checkSum = 10 - ((even + odd) % 10);
                            if (checkSum == 10) { checkSum = 0; }

                            if (checkSum == checkNum)
                            {
                                isValid = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid ISBN13, please check the ISBN13 and try again.");
                                Console.WriteLine();
                            }

                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Too few numbers, please try again.");
                            Console.WriteLine();
                        }
                    }
                    isValid = false;
                    Console.Write("Enter Title for the new book: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter language of the book: ");
                    string language = Console.ReadLine();
                    int price = 0;
                    while (isValid == !true)
                    {
                        Console.Write("Enter the price for the new book: ");
                        string sPrice = Console.ReadLine();
                        bool priceValid = int.TryParse(sPrice, out price);
                        if (priceValid)
                        {
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid price entered, try again.");
                            Console.WriteLine();
                        }
                    }
                    isValid = false;
                    int publishYear = 0;
                    while (isValid == !true)
                    {
                        isValid = false;
                        Console.Write("Enter the year the book was published: ");
                        string sPublishYear = Console.ReadLine();
                        bool yearValid = int.TryParse(sPublishYear, out publishYear);
                        if (yearValid)
                        {
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid price entered, try again.");
                            Console.WriteLine();
                        }
                    }
                    Console.Write("Is the book part of a series? ");
                    string? isSeries = Console.ReadLine();
                    string? series;
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
                        PublishYear = publishYear,
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
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }

        private static void EditBook()
        {
            try
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
                            ChangeAuthor(selectedBook);
                            break;
                        case "2":
                            ChangePrice(selectedBook);
                            break;
                        case "3":
                            AddAuthor(selectedBook, isbn);
                            break;
                        case "4":
                            RemoveAuthor(selectedBook, isbn);
                            break;
                        case "5":
                            selectedBook = ChangeBook();
                            TextCenter.CenterText("Active book changed.");
                            TextCenter.CenterText($"New active book is: {selectedBook.Title}");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    if (choice == "1" || choice == "2" || choice == "3" || choice == "4")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Book updated. ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    MenuChoices.EditBookMenu();
                    TextCenter.CenterText($"Current Active Book: {selectedBook.Title}");
                    Console.WriteLine();
                    Console.SetCursorPosition(Console.WindowWidth / 2, 11);
                    choice = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }
        private static void ChangeAuthor(Book? selectedBook)
        {
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
                context.SaveChanges();
            }
        }

        private static void ChangePrice(Book? selectedBook)
        {
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
                context.SaveChanges();
            }
        }

        private static void AddAuthor(Book? selectedBook, string isbn)
        {
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
                context.SaveChanges();
            }
        }

        private static void RemoveAuthor(Book? selectedBook, string isbn)
        {
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
                context.SaveChanges();
            }
        }
        private static Book? ChangeBook()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
                return null;
            }
        }

        private static void DeleteBook()
        {
            try
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
                    else if (context.Inventories.Where(i => i.Isbn == selectedItem.Isbn13).Count() > 0)
                    {
                        TextCenter.CenterText("Book found in store(s) inventory.");
                        TextCenter.CenterText("Please remove the book from store inventories before removing the book.");
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
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }
    }
}
