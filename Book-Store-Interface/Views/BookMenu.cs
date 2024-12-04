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
                        //AddBook();
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

        //private static void AddNewBook(int storeId)
        //{
        //    ClearConsole.ConsoleClear();
        //    Console.WriteLine();
        //    Console.Write("Enter book ISBN: ");
        //    string isbn = Console.ReadLine();
        //    Console.WriteLine("Enter Title: ");
        //    string title = Console.ReadLine();
        //    Console.WriteLine("Is the author in the database?");
        //    Console.WriteLine("1. Yes");
        //    Console.WriteLine("2. No");
        //    int response = int.Parse(Console.ReadLine());
        //    while (response != 0)
        //    {
        //        switch (response)
        //        {
        //            case 1:
        //                AuthorMenu.ListAuthors();
        //                Console.WriteLine("Enter Author first and last name (without middle initial): ");
        //                string authorNameDB = Console.ReadLine();
        //                string[] aNameArrayDB = authorNameDB.Split(" ");
        //                string aFirstNameDB = aNameArrayDB[0];
        //                string aLastNameDB = aNameArrayDB[1];
        //                response = 0;
        //                break;
        //            case 2:
        //                //Console.WriteLine("Enter Author first and last name (without middle initial): ");
        //                //string authorName = Console.ReadLine();
        //                //string[] aNameArray = authorName.Split(" ");
        //                //string aFirstName = aNameArray[0];
        //                //string aLastName = aNameArray[1];
        //                //Console.WriteLine("If the author have a middle initial, enter it now ( or leave blank ): ");
        //                //string middleInitial = Console.ReadLine();
        //                //string initial = string.IsNullOrWhiteSpace(middleInitial) ? null : middleInitial;
        //                //Console.WriteLine("Enter author birthday (Format: YYYY-MM-DD) or leave blank if unknown: ");
        //                //string birthDate = Console.ReadLine().ToLower();
        //                //DateOnly? parsedDate = string.IsNullOrWhiteSpace(birthDate) ? (DateOnly?)null : DateOnly.Parse(birthDate);
        //                //Console.WriteLine("Is the author dead?");
        //                AuthorMenu.AddAuthor();
        //                response = 0;
        //                break;
        //            default:
        //                Console.WriteLine("Invalid choice, try again.");
        //                break;
        //        }
        //    }
        //    Console.WriteLine("Enter year the book was published");
        //    Console.Write("Enter quantity: ");
        //    int quantity = int.Parse(Console.ReadLine());

        //    using (var context = new Labb1BokhandelDemoContext())
        //    {
        //        var inventory = new Inventory
        //        {
        //            StoreId = storeId,
        //            Isbn = isbn,
        //            CurrentInventory = quantity
        //        };
        //        context.Inventories.Add(inventory);
        //        context.SaveChanges();
        //        TextCenter.CenterText("Inventory added.");
        //    }
        //}
    }
}
