using Book_Store_Interface.Model;

namespace Book_Store_Interface.GeneralMethods
{
    internal class ListStores
    {
        public static void ListStore()
        {
            using (var context = new Labb1BokhandelDemoContext())
            {
                var stores = context.Stores.ToList();
                ClearConsole.ConsoleClear();
                TextCenter.CenterText("List of Stores");
                Console.WriteLine();
                foreach (var store in stores)
                {
                    Console.WriteLine($"Store ID: {store.Id}");
                    Console.WriteLine($"Store Name: {store.StoreName}");
                    Console.WriteLine($"Store Address: {store.Address}");
                    Console.WriteLine();
                }
            }
        }
    }
}
