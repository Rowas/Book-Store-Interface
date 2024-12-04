using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Model;

namespace Book_Store_Interface
{
    internal class AuthorMenu
    {
        private static void AuthorMenuChoices()
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

        public static void AuthorsMenu()
        {
            AuthorMenuChoices();
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
                        //DeleteAuthor();
                        break;
                    case "4":
                        ListAuthors();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                AuthorMenuChoices();
                Console.SetCursorPosition(Console.WindowWidth / 2, 9);
                menuChoice = Console.ReadLine();
            }
        }

        private static void AddAuthor()
        {
            DateOnly? parsedDate;
            string dateInput = "2000-01-01";
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
            Console.Write("Enter author's birth date and year (Format YYYY-MM-DD) or NULL if unknown: ");
            string birthDate = Console.ReadLine().ToLower();
            parsedDate = string.IsNullOrWhiteSpace(birthDate) ? (DateOnly?)null : DateOnly.Parse(birthDate);
            Console.Write("Is the author dead? ");
            string isDead = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var author = new Author
                {
                    FirstName = firstName,
                    Initial = initial,
                    LastName = lastName,
                    BirthDate = parsedDate,
                    IsDead = isDead.ToLower() == "yes" ? true : false
                };
                context.Authors.Add(author);
                var booksauthor = new BooksAuthor
                {
                    AuthorsId = context.Authors.OrderBy(a => a.Id).FirstOrDefault().Id
                };
                context.BooksAuthors.Add(booksauthor);
                TextCenter.CenterText("Author added.");
                context.SaveChanges();
            }
        }

        private static void EditAuthor()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Edit Author");
            Console.WriteLine();
            Console.Write("Enter author's first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter author's last name: ");
            string lastName = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var author = context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);
                if (author == null)
                {
                    TextCenter.CenterText("Author not found.");
                }
                else
                {
                    Console.WriteLine($"Current status: {author.IsDead}");
                    Console.WriteLine("Update death status?");
                    string deathStatus = Console.ReadLine();
                    switch (deathStatus.ToLower())
                    {
                        case "yes":
                            author.IsDead = !author.IsDead;
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
        private static void ListAuthors()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Authors");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var authors = context.Authors.ToList();
                if (authors.Count == 0)
                {
                    TextCenter.CenterText("No authors found.");
                }
                else
                {
                    foreach (var author in authors)
                    {
                        Console.WriteLine($"{author.FirstName} {author.LastName}");
                    }
                }
            }
        }
    }
}