using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Views;

namespace Book_Store_Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        MenuChoices.PrintMenu();
        Console.SetCursorPosition(Console.WindowWidth / 2, 10);
        string menuChoice = Console.ReadLine();
        while (menuChoice != "5")
        {
            switch (menuChoice)
            {
                case "1":
                    AuthorMenu.AuthorsMenu();
                    break;
                case "2":
                    BookMenu.BooksMenu();
                    break;
                case "3":
                    StoreMenu.StoresMenu();
                    break;
                case "4":
                    PublisherMenu.PublishersMenu();
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            MenuChoices.PrintMenu();
            Console.SetCursorPosition(Console.WindowWidth / 2, 10);
            menuChoice = Console.ReadLine();
        }
    }
}