namespace Book_Store_Interface.GeneralMethods
{
    internal class ClearConsole
    {
        public static void ConsoleClear()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }
    }
}
