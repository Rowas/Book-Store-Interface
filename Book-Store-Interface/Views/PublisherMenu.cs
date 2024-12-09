using Book_Store_Interface.GeneralMethods;
using Book_Store_Interface.Model;

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
                        AddPublisher();
                        break;
                    case "2":
                        EditPublisher();
                        break;
                    case "3":
                        DeletePublisher();
                        break;
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

        private static void AddPublisher()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Add Publisher");
            Console.WriteLine();
            Console.Write("Enter name of new publisher: ");
            var pubName = Console.ReadLine();
            Console.Write("Enter contact email to new publisher: ");
            var pubEmail = Console.ReadLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var publisher = new Publisher
                {
                    Name = pubName,
                    ContactEmail = pubEmail
                };
                context.Publishers.Add(publisher);
                TextCenter.CenterText("Publisher added.");
                context.SaveChanges();
            }
        }

        private static void EditPublisher()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Edit Publisher");
            ListPublishers.ListPublisher();
            Console.WriteLine();
            Console.Write("Enter ID of publisher to change: ");
            int pubID = int.Parse(Console.ReadLine());
            using (var context = new Labb1BokhandelDemoContext())
            {
                var publisher = context.Publishers.Find(pubID);
                if (publisher == null) TextCenter.CenterText("Publisher not found.");
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Current E-Mail: {publisher.ContactEmail}");
                    Console.Write("Enter new contact E-Mail to publisher: ");
                    var pubEmail = Console.ReadLine();
                    if (pubEmail == null || pubEmail.Contains("@") == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No value entered or invalid email entered.");
                        Console.Write("Returning to menu.");
                        Console.WriteLine();
                        return;
                    }
                    publisher.ContactEmail = pubEmail;
                    context.SaveChanges();
                    Console.WriteLine();
                    TextCenter.CenterText($"Publisher E-Mail updated.");
                }
            }
        }

        private static void DeletePublisher()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("Delete Publisher");
            ListPublishers.ListPublisher();
            Console.WriteLine();
            Console.Write("Enter ID of publisher to delete: ");
            int pubID = int.Parse(Console.ReadLine());
            if (pubID == 1) { Console.WriteLine("Default publisher not removable"); Console.WriteLine("Returning to menu."); return; }
            using (var context = new Labb1BokhandelDemoContext())
            {
                var publisher = context.Publishers.Find(pubID);
                if (publisher == null) { TextCenter.CenterText("Publisher not found."); TextCenter.CenterText("Returning to menu."); return; }
                context.Publishers.Remove(publisher);
                context.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Publisher sucessfully removed.");
            }
        }
    }
}
