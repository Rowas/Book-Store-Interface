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
        Console.Clear();
        CenterText("Book Store");
        CenterText("Main Menu");
        Console.WriteLine();
        CenterText("1. Authors");
        CenterText("2. Books");
        CenterText("3. Stores");
        CenterText("4. Exit");
        Console.WriteLine();
        CenterText("Enter your choice:");
    }

    public static void CenterText(string text)
    {
        Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
        Console.WriteLine(text);
    }
}