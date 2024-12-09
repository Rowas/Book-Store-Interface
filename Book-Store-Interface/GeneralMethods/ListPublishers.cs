using Book_Store_Interface.Model;

namespace Book_Store_Interface.GeneralMethods
{
    internal class ListPublishers
    {
        public static void ListPublisher()
        {
            ClearConsole.ConsoleClear();
            TextCenter.CenterText("List of Publishers");
            Console.WriteLine();
            using (var context = new Labb1BokhandelDemoContext())
            {
                var publishers = context.Publishers.ToList();
                if (publishers.Count == 0)
                {
                    TextCenter.CenterText("No publisher found.");
                }
                else
                {
                    foreach (var publisher in publishers)
                    {
                        Console.WriteLine($"{publisher.PubId}. {publisher.Name}");
                    }
                }
            }
        }
    }
}
