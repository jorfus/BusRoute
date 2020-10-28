using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace BusRoute
{
    class RouteMap
    {
        StringBuilder TheRouteMap { get; set; } = new StringBuilder(new string(' ', 11000), 11000);
        AllBusStops TheBusStops { get; set; } = new AllBusStops();
        AllRoutes TheRoutes { get; set; }
        public int WindowBuffer { get; private set; }

        public RouteMap()
        {
            for (int index = 0; index < 10900; index++)
            {
                if (((index + 1) % 10 == 0 && index <= 200) || (index + 200) % 2000 == 0)
                    TheRouteMap.Replace(' ', 'X', index, 1);
            }

            int column, row;
            string[] busStopGraphics;
            int[] mapIndexFormula = new int[3];

            foreach (BusStop stop in TheBusStops.GetBusStops())
            {
                (column, row) = stop.GetPosition();
                busStopGraphics = stop.GetBusStopGraphic();

                mapIndexFormula[0] = 200 * row + column - 201;
                mapIndexFormula[1] = 200 * row + column - 1;
                mapIndexFormula[2] = 200 * row + column + 199;

                for (int index = 0; index < 3; index++)
                    TheRouteMap.Replace("   ", busStopGraphics[index], mapIndexFormula[index], 3);
            }

            TheRoutes = new AllRoutes(TheBusStops);
            int theRouteMapIndex;

            foreach (Route route in TheRoutes.GetRoutes())
                foreach (int[] coordinates in route.GetRoute())
                {
                    theRouteMapIndex = coordinates[0] * 200 + coordinates[1]; 
                    TheRouteMap = TheRouteMap.Replace(' ', 'X', theRouteMapIndex, 1);
                }

            WindowBuffer = ToString().Length / 200 - 1;
        }

        public string TheDrawnMap()
        {
            return TheRouteMap.ToString();
        }
        public RouteMap RunSim()
        {
            RouteMap newMap = new RouteMap();

            Console.SetCursorPosition(0, 0);
            Console.Write(newMap.TheDrawnMap());
            Thread.Sleep(2000);
            
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                newMap = new RouteMap();

                Console.SetCursorPosition(0, 0);
                Console.Write(newMap.TheDrawnMap());
                Thread.Sleep(2000);
            }

            return newMap;
        }
        public override string ToString()
        {
            return $"{TheBusStops}...{TheRoutes}";
        }
    }
}
