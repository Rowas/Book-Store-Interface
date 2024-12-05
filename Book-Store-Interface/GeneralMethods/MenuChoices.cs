namespace Book_Store_Interface.GeneralMethods
{
    internal class MenuChoices
    {
        public static void PrintMenu()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Book Store");
            TextCenter.CenterText("Main Menu");
            Console.WriteLine();
            TextCenter.CenterText("1. Authors");
            TextCenter.CenterText("2. Books");
            TextCenter.CenterText("3. Stores");
            TextCenter.CenterText("4. Exit");
            Console.WriteLine();
            TextCenter.CenterText("Enter your choice:");
        }
        public static void AuthorMenuChoices()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Author Menu");
            Console.WriteLine();
            TextCenter.CenterText("1. Add Author");
            TextCenter.CenterText("2. Edit Author");
            TextCenter.CenterText("3. Delete Author");
            TextCenter.CenterText("4. List Authors");
            TextCenter.CenterText("5. Back");
            Console.WriteLine();
            TextCenter.CenterText("Enter your choice:");
        }

        public static void BookMenuChoices()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Book Menu");
            Console.WriteLine();
            TextCenter.CenterText("1. Add Book");
            TextCenter.CenterText("2. Edit Book");
            TextCenter.CenterText("3. Delete Book");
            TextCenter.CenterText("4. List Books");
            TextCenter.CenterText("5. Back");
            Console.WriteLine();
            TextCenter.CenterText("Enter your choice:");
        }

        public static void StoreMenuChoices()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Store Menu");
            Console.WriteLine();
            TextCenter.CenterText("1. Add Store Inventory");
            TextCenter.CenterText("2. Edit Store Inventory");
            TextCenter.CenterText("3. List Store Inventory");
            TextCenter.CenterText("4. Back");
            Console.WriteLine();
            TextCenter.CenterText("Enter your choice:");
        }

        public static void EditStoreInventoryMenu()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Edit Inventory Menu");
            Console.WriteLine();
            TextCenter.CenterText("1. Edit Stock Levels");
            TextCenter.CenterText("2. Remove from Inventory");
            Console.WriteLine();
            TextCenter.CenterText("Enter your choice:");
        }
    }
}
