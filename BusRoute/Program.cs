using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusRoute
{
    class Program
    {
        static void Main(string[] args)
        {
            RouteMap theMap = new RouteMap(30);

            Console.WindowWidth = 200;
            Console.WindowHeight = 57;
            
            Console.Write(theMap.TheDrawnMap());

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        theMap = RunSim(theMap);
                        break;
                    case ConsoleKey.Tab:
                        Console.WindowHeight = 57 + theMap.WindowBuffer;
                        Console.Write(theMap);
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        continue;
                }
            }
        }

        static RouteMap RunSim(RouteMap theMap)
        {
            Console.Clear();
            
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Console.SetCursorPosition(0, 0);
                theMap = new RouteMap(30);
                
                Console.Write(theMap.TheDrawnMap());

                Thread.Sleep(2000);
            }

            return theMap;
        }
    }
}
