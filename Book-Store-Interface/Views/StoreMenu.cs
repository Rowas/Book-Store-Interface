using Book_Store_Interface.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Interface
{
    internal class StoreMenu
    {
        private static void StoreMenuChoices()
        {
            Console.Clear();
            CenterText("Store Menu");
            Console.WriteLine();
            CenterText("1. Add Store Inventory");
            CenterText("2. Edit Store Inventory");
            CenterText("3. List Store Inventory");
            CenterText("4. Back");
            Console.WriteLine();
            CenterText("Enter your choice:");
        }

        public static void CenterText(string text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }

        public static void StoresMenu()
        {
            StoreMenuChoices();
            Console.SetCursorPosition(Console.WindowWidth / 2, 8);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "4")
            {
                switch (menuChoice)
                {
                    case "1":
                        AddToStoreInventory();
                        break;
                    case "2":
                        EditStoreInventory();
                        break;
                    case "3":
                        ListStoresInventory();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                StoreMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        private static void AddToStoreInventory()
        {
            ListStores();
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Pick from current current range of books or add a new one?");
            Console.WriteLine();
            Console.WriteLine("1. Current range");
            Console.WriteLine("2. New book");
            Console.WriteLine("3. Back");
            string menuChoice = Console.ReadLine();
            while (menuChoice != "3")
            {
                switch (menuChoice)
                {
                    case "1":
                        ListBooks();
                        break;
                    case "2":
                        AddCurrentRange(storeId);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            ListBooks();
        }

        private static void AddCurrentRange(int storeId)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Enter book ISBN: ");
            string isbn = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            using (var context = new Labb1BokhandelDemoContext())
            {
                var inventory = new Inventory
                {
                    StoreId = storeId,
                    Isbn = isbn,
                    CurrentInventory = quantity
                };
                context.Inventories.Add(inventory);
                context.SaveChanges();
                CenterText("Inventory added.");
            }
        }

        private static void ListStoresInventory()
        {
            ListStores();
            Console.WriteLine("Pick store to list inventory for:");
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());

            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                Console.Clear();
                CenterText("List of Store Inventory");
                Console.WriteLine();
                Console.WriteLine($"Store ID: {stores.Id}");
                Console.WriteLine($"Store Name: {stores.StoreName}");
                Console.WriteLine($"Store Address: {stores.Address}");
                Console.WriteLine();
                Console.WriteLine("Inventory:");
                foreach (var inventory in stores.Inventories)
                {
                    Console.WriteLine($"Book ID: {inventory.Isbn}");
                    Console.Write("Author(s):");
                    foreach (var author in books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().BooksAuthors)
                    {
                        Console.WriteLine($"{author.Authors.FirstName} {author.Authors.LastName}");
                    }
                    Console.WriteLine($"Title: {books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().Title}");
                    Console.WriteLine($"Price: {books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().Price} SEK");
                    Console.WriteLine($"Quantity: {inventory.CurrentInventory}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        private static void ListBooks()
        {
            using (var context = new Labb1BokhandelDemoContext())
            {
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                Console.Clear();
                CenterText("List of Books");
                Console.WriteLine();
                int x = 1;
                int y = 0;
                List<(string, int)> bookIDNumber = new List<(string, int)>();
                foreach (var book in books)
                {
                    //bookIDNumber.Add((book.Isbn13, x));
                    //Console.WriteLine($"Book#: {bookIDNumber.ElementAt(y).Item2}");
                    //x++;
                    Console.WriteLine($"Book ID: {book.Isbn13}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.Write("Author(s):");
                    foreach (var author in book.BooksAuthors)
                    {
                        Console.WriteLine($"{author.Authors.FirstName} {author.Authors.LastName}");
                    }
                    Console.WriteLine($"Price: {book.Price} SEK");
                    Console.WriteLine();
                }
            }
        }

        private static void EditStoreInventory()
        {
            ListStores();
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                Console.Clear();
                CenterText("Edit Store Inventory");
                Console.WriteLine();
                Console.WriteLine($"Store ID: {stores.Id}");
                Console.WriteLine($"Store Name: {stores.StoreName}");
                Console.WriteLine($"Store Address: {stores.Address}");
                Console.WriteLine("----------------------------");
                Console.WriteLine();
                Console.WriteLine("Inventory:");
                foreach (var inventory in stores.Inventories)
                {
                    Console.WriteLine($"Book ID: {inventory.Isbn}");
                    Console.Write("Author(s):");
                    foreach (var author in books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().BooksAuthors)
                    {
                        Console.WriteLine($"{author.Authors.FirstName} {author.Authors.LastName}");
                    }
                    Console.WriteLine($"Title: {books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().Title}");
                    Console.WriteLine($"Price: {books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().Price} SEK");
                    Console.WriteLine($"Author(s): {books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().BooksAuthors.FirstOrDefault().Authors.FirstName} " +
                        $"{books.Where(b => b.Isbn13 == inventory.Isbn).FirstOrDefault().BooksAuthors.FirstOrDefault().Authors.LastName}");
                    Console.WriteLine($"Quantity: {inventory.CurrentInventory}");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.Write("Enter book ISBN to edit: ");
                string isbn = Console.ReadLine();
                Console.Write("Enter new quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                var selectedInventory = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                selectedInventory.CurrentInventory = quantity;
                context.SaveChanges();
                CenterText("Inventory updated.");
            }
        }

        private static void ListStores()
        {
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.ToList();
                Console.Clear();
                CenterText("List of Stores");
                Console.WriteLine();
                foreach (var store in stores)
                {
                    Console.WriteLine($"Store ID: {store.Id}");
                    Console.WriteLine($"Store Name: {store.StoreName}");
                    Console.WriteLine($"Store Address: {store.Address}");
                    Console.WriteLine();
                }
            }
        }
    }
}
