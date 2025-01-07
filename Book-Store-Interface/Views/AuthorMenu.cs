using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Model;

namespace Book_Store_Interface
{
    internal class AuthorMenu
    {
        public static void AuthorsMenu()
        {
            MenuChoices.AuthorMenuChoices();
            Console.SetCursorPosition(Console.WindowWidth / 2, 9);
            string menuChoice = Console.ReadLine();
            while (menuChoice != "5")
            {
                switch (menuChoice)
                {
                    case "1":
                        AddAuthor();
                        break;
                    case "2":
                        EditAuthor();
                        break;
                    case "3":
                        DeleteAuthor();
                        break;
                    case "4":
                        ListAuthors.ListAuthor();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                MenuChoices.AuthorMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        public static void AddAuthor()
        {
            try
            {
                using (var context = new Labb1BokhandelDemoContext())
                {
                    ClearConsole.ConsoleClear();
                    TextCenter.CenterText("Add Author");
                    Console.WriteLine();
                    Console.Write("Enter author's first name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter author's middle initial (if any): ");
                    string middleInitial = Console.ReadLine();
                    string initial = string.IsNullOrWhiteSpace(middleInitial) ? null : middleInitial;
                    Console.Write("Enter author's last name: ");
                    string lastName = Console.ReadLine();
                    if (context.Authors.Any(a => a.FirstName == firstName && a.Initial == initial && a.LastName == lastName || a.FirstName == firstName && a.LastName == lastName))
                    {
                        TextCenter.CenterText("Author already exists");
                        TextCenter.CenterText("Returning to menu.");
                        return;
                    }
                    bool isParsed = false;
                    string birthDate;
                    DateOnly? parsedDate = null;
                    while (isParsed != true)
                    {
                        DateOnly ignoreMe;
                        Console.Write("Enter author's birth date and year (Format YYYY-MM-DD) or leave blank if unknown: ");
                        birthDate = Console.ReadLine().ToLower();
                        isParsed = DateOnly.TryParse(birthDate, out ignoreMe);
                        if (isParsed == true)
                        {
                            parsedDate = string.IsNullOrWhiteSpace(birthDate) ? (DateOnly?)null : DateOnly.Parse(birthDate);
                        }
                        else if (isParsed == false && birthDate == null)
                        {
                            isParsed = true;
                            parsedDate = null;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid date format, or field not left blank.");
                            Console.WriteLine("Try again.");
                            Console.WriteLine();
                        }

                    }
                    Console.Write("Is the author dead? ");
                    string isDead = Console.ReadLine();
                    var author = new Author
                    {
                        FirstName = firstName,
                        Initial = initial,
                        LastName = lastName,
                        BirthDate = parsedDate,
                        IsDead = isDead.ToLower() == "yes" ? true : false
                    };
                    context.Authors.Add(author);
                    TextCenter.CenterText("Author added.");
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }

        private static void EditAuthor()
        {
            try
            {
                using (var context = new Labb1BokhandelDemoContext())
                {
                    ClearConsole.ConsoleClear();
                    TextCenter.CenterText("Edit Author");
                    ListAuthors.ListAuthor();
                    Console.WriteLine();
                    Console.Write("Enter ID of the author from the list above: ");
                    int bookAuthor = int.Parse(Console.ReadLine());
                    var foundAuthor = context.Authors.Find(bookAuthor);
                    if (foundAuthor == null)
                    {
                        TextCenter.CenterText("Author not found.");
                    }
                    else
                    {
                        Console.WriteLine($"Current status: {foundAuthor.IsDead}");
                        Console.WriteLine("Update death status?");
                        string deathStatus = Console.ReadLine();
                        switch (deathStatus.ToLower())
                        {
                            case "yes":
                                foundAuthor.IsDead = !foundAuthor.IsDead;
                                context.SaveChanges();
                                TextCenter.CenterText("Author updated.");
                                break;
                            default:
                                TextCenter.CenterText("Returning with no changes.");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, Please try again. ");
            }
        }

        private static void DeleteAuthor()
        {
            try
            {
                using (var context = new Labb1BokhandelDemoContext())
                {
                    ClearConsole.ConsoleClear();
                    TextCenter.CenterText("Edit Author");
                    ListAuthors.ListAuthor();
                    Console.WriteLine();
                    Console.Write("Enter ID of the author from the list above: ");
                    int bookAuthor = int.Parse(Console.ReadLine());
                    var foundAuthor = context.Authors.Find(bookAuthor);
                    if (foundAuthor == null)
                    {
                        TextCenter.CenterText("Author not found.");
                    }
                    else if (context.BooksAuthors.Where(ba => ba.AuthorsId == foundAuthor.Id).Count() > 0)
                    {
                        TextCenter.CenterText("Author have books associated with him/her.");
                        TextCenter.CenterText("Please remove all books associated with the author before removing the author.");
                    }
                    else
                    {
                        TextCenter.CenterText("Author removed successfully");
                        context.Remove(foundAuthor);
                        context.SaveChanges();
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