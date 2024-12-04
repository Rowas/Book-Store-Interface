using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Interface
{
    internal class StoreMenu
    {
        public static void StoresMenu()
        {
            ClearConsole.ConsoleClear();
            MenuChoices.StoreMenuChoices();
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
                MenuChoices.StoreMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        private static void AddToStoreInventory()
        {
            ListStores.ListStore();
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());
            ClearConsole.ConsoleClear();
            AddToInventory(storeId);
        }

        private static void AddToInventory(int storeId)
        {
            ListBooks.ListBook();
            Console.WriteLine();
            Console.Write("Enter ISBN from above list of books currently in Database: ");
            var isbn = Console.ReadLine();
            Console.Write("Enter quantity to add: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var isbncheck = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                if (isbncheck != null)
                {
                    Console.WriteLine($"Title already in inventory, do you wish to update the inventory with the requested amount of {quantity}?");
                    var response = Console.ReadLine();
                    switch (response.ToLower())
                    {
                        case "yes":
                            var selectedInventory = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                            selectedInventory.CurrentInventory = selectedInventory.CurrentInventory + quantity;
                            context.SaveChanges();
                            Console.WriteLine("Inventory Updated.");
                            break;
                        default:
                            Console.WriteLine("Cancelling add request.");
                            break;
                    }
                }
                else if (isbncheck != null && isbncheck.Isbn == isbn)
                {
                    var inventory = new Inventory
                    {
                        StoreId = storeId,
                        Isbn = isbn,
                        CurrentInventory = quantity
                    };
                    context.Inventories.Add(inventory);
                    context.SaveChanges();
                    TextCenter.CenterText("Inventory added.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                    Console.WriteLine("Add it through the \"Book Menu\" before adding it to inventory.");
                }
            }
        }

        private static void EditStoreInventory()
        {
            MenuChoices.EditStoreInventoryMenu();
            Console.SetCursorPosition(Console.WindowWidth / 2, 8);
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    EditStoreInventoryStock();
                    break;
                case "2":
                    RemoveFromInventory();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void ListStoresInventory()
        {
            ListStores.ListStore();
            Console.WriteLine("Pick store to list inventory for:");
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());

            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                ClearConsole.ConsoleClear();
                TextCenter.CenterText("List of Store Inventory");
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

        private static void RemoveFromInventory()
        {
            ListStores.ListStore();
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                ClearConsole.ConsoleClear();
                TextCenter.CenterText("Edit Stock Levels");
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
                Console.WriteLine();
                Console.Write("Enter book ISBN to remove: ");
                string isbn = Console.ReadLine();
                var selectedItem = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                context.Remove(selectedItem);
                context.SaveChanges();
                TextCenter.CenterText("Inventory updated.");
            }
        }

        private static void EditStoreInventoryStock()
        {
            ListStores.ListStore();
            Console.WriteLine();
            Console.Write("Enter store ID: ");
            int storeId = int.Parse(Console.ReadLine());
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.Include(s => s.Inventories).Where(s => s.Id == storeId).FirstOrDefault();
                var books = context.Books.Include(b => b.BooksAuthors).ThenInclude(ba => ba.Authors).ToList();
                ClearConsole.ConsoleClear();
                TextCenter.CenterText("Edit Stock Levels");
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
                TextCenter.CenterText("Inventory updated.");
            }
        }
    }
}
