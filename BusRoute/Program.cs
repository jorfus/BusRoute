using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusRoute
{
    class Program
    {
        static void Main()
        {
            RouteMap aRouteMap = new RouteMap();

            Console.WindowWidth = 200;
            Console.WindowHeight = 56;
            Console.Write(aRouteMap.PartialDraw());
            
            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        RunSim();
                        break;
                    case ConsoleKey.Tab:
                        if (MapInfo(aRouteMap).Key == ConsoleKey.Escape)
                            loop = false;
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(aRouteMap.PartialDraw());
                        }
                        break;
                    case ConsoleKey.N:
                        Console.Clear();
                        aRouteMap = new RouteMap();
                        Console.SetCursorPosition(0, 0);
                        Console.Write(aRouteMap.PartialDraw());
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }

        static void RunSim()
        {

        }
        static ConsoleKeyInfo MapInfo(RouteMap aRouteMap)
        {
            Console.Clear();
            Console.Write(aRouteMap.FullDraw());
            Console.Write(aRouteMap);

            ConsoleKeyInfo input = Console.ReadKey();

            return input;
        }
    }
}
