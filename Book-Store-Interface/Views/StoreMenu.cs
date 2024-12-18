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
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "5")
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
                    case "4":
                        ListStores.ListStore();
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
            try
            {
                ListStores.ListStore();
                Console.WriteLine();
                Console.Write("Enter store ID: ");
                int storeId = int.Parse(Console.ReadLine());
                ClearConsole.ConsoleClear();
                AddToInventory(storeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
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
            Console.SetCursorPosition(Console.WindowWidth / 2, 7);
            string choice = Console.ReadLine();
            while (choice != "3")
            {
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
                        Console.WriteLine("Returning with no changes made.");
                        return;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                MenuChoices.EditStoreInventoryMenu();
                Console.SetCursorPosition(Console.WindowWidth / 2, 7);
                choice = Console.ReadLine();
            }
        }

        private static void ListStoresInventory()
        {
            try
            {
                ListStores.ListStore();
                Console.WriteLine("Pick store to list inventory for:");
                Console.WriteLine();
                Console.Write("Enter store ID: ");
                int storeId = int.Parse(Console.ReadLine());
                InventoryList(storeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }

        private static Store InventoryList(int storeId)
        {
            try
            {
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
                        foreach (var author in books.Find(b => b.Isbn13 == inventory.Isbn).BooksAuthors)
                        {
                            Console.WriteLine($"{author.Authors.FirstName} {author.Authors.LastName}");
                        }
                        Console.WriteLine($"Title: {books.Find(b => b.Isbn13 == inventory.Isbn).Title}");
                        Console.WriteLine($"Price: {books.Find(b => b.Isbn13 == inventory.Isbn).Price}");
                        Console.WriteLine($"Quantity: {inventory.CurrentInventory}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    return stores;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
                return null;
            }
        }

        private static void RemoveFromInventory()
        {
            try
            {
                ListStores.ListStore();
                Console.WriteLine();
                Console.Write("Enter store ID: ");
                int storeId = int.Parse(Console.ReadLine());
                using (var context = new Labb1BokhandelDemoContext())
                {
                    var stores = InventoryList(storeId);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Enter book ISBN to remove: ");
                    string isbn = Console.ReadLine();
                    var selectedItem = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                    if (selectedItem != null && selectedItem.Isbn == isbn)
                    {
                        context.Remove(selectedItem);
                        context.SaveChanges();
                        TextCenter.CenterText("Inventory updated.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found, no changes made.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }

        private static void EditStoreInventoryStock()
        {
            try
            {
                ListStores.ListStore();
                Console.WriteLine();
                Console.Write("Enter store ID: ");
                int storeId = int.Parse(Console.ReadLine());
                using (var context = new Labb1BokhandelDemoContext())
                {
                    var stores = InventoryList(storeId);
                    Console.WriteLine();
                    Console.Write("Enter book ISBN to edit: ");
                    string isbn = Console.ReadLine();
                    Console.Write("Enter new quantity: ");
                    int quantity = int.Parse(Console.ReadLine());

                    var selectedItem = stores.Inventories.FirstOrDefault(i => i.Isbn == isbn);
                    if (selectedItem != null && selectedItem.Isbn == isbn)
                    {
                        context.Attach(selectedItem);
                        selectedItem.CurrentInventory = quantity;
                        context.Entry(selectedItem).Property("CurrentInventory").IsModified = true;
                        context.SaveChanges();
                        TextCenter.CenterText("Inventory updated.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found, no changes made.");
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
