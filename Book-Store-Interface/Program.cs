using Book_Store_Interface.GeneralMethods;

namespace Book_Store_Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        PrintMenu();
        Console.SetCursorPosition(Console.WindowWidth / 2, 9);
        string menuChoice = Console.ReadLine();
        while (menuChoice != "4")
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
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            PrintMenu();
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            menuChoice = Console.ReadLine();
        }
    }

    private static void PrintMenu()
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
}