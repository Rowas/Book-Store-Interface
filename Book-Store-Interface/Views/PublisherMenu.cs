using Book_Store_Interface.GeneralMethods;

namespace Book_Store_Interface.Views
{
    internal class PublisherMenu
    {
        public static void PublishersMenu()
        {
            ClearConsole.ConsoleClear();
            MenuChoices.PublisherMenuChoices();
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "5")
            {
                switch (menuChoice)
                {
                    case "1":
                        //AddToStoreInventory();
                        break;
                    case "2":
                        //EditStoreInventory();
                        break;
                    case "3":
                    //DeletePublisher();
                    case "4":
                        ListPublishers.ListPublisher();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                MenuChoices.PublisherMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }
    }
}
