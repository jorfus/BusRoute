using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusRoute
{
    class Program
    {
        static void Main()
        {
            RouteMap theRouteMap = new RouteMap();
            
            Console.WindowWidth = 200;
            Console.WindowHeight = 57;
            
            Console.Write(theRouteMap.TheDrawnMap());

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        theRouteMap = theRouteMap.RunSim();
                        break;
                    case ConsoleKey.Tab:
                        Console.WindowHeight = 57 + theRouteMap.WindowBuffer;
                        Console.Write(theRouteMap);
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        continue;
                }
            }
        }

        
    }
}
