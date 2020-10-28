using System;

namespace BusRouteLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass test = new TestClass();
            Console.WriteLine($"Före ändring: {test}");
            test.TestMethod();
            Console.WriteLine($"Efter automatiska ändringen: {test}");
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        test.TestMethod();
                        Console.WriteLine($"Efter att metoden avslutats: {test}");
                        break;
                    case ConsoleKey.Spacebar:
                        Console.WriteLine(test);
                        break;
                    default:
                        break;
                }
            }
        }
        
    }
}
