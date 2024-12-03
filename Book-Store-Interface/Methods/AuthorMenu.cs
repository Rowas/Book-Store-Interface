using Book_Store_Interface.Model;

namespace Book_Store_Interface
{
    internal class AuthorMenu
    {
        private static void AuthorMenuChoices()
        {
            Console.Clear();
            CenterText("Author Menu");
            Console.WriteLine();
            CenterText("1. Add Author");
            CenterText("2. Edit Author");
            CenterText("3. Delete Author");
            CenterText("4. List Authors");
            CenterText("5. Back");
            Console.WriteLine();
            CenterText("Enter your choice:");
        }

        public static void CenterText(string text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
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
                        //AddAuthor();
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

        private static void ListAuthors()
        {
            Console.Clear();
            CenterText("Authors");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var authors = context.Authors.ToList();
                if (authors.Count == 0)
                {
                    CenterText("No authors found.");
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

        private static void EditAuthor()
        {
            Console.Clear();
            CenterText("Edit Author");
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
                    CenterText("Author not found.");
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
                            CenterText("Author updated.");
                            break;
                        default:
                            CenterText("Returning with no changes.");
                            break;
                    }
                }
            }
        }
    }
}