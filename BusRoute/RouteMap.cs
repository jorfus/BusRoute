using System;
using System.Collections.Generic;
using System.Text;

namespace BusRoute
{
    class RouteMap
    {
        AllBusStops TheBusStopArray { get; set; }
        StringBuilder TheRouteMap { get; set; } = new StringBuilder(new string(' ', 11000), 11000);
        public int WindowBuffer { get; private set; }

        public RouteMap(int stopsCount)
        {
            TheBusStopArray = new AllBusStops(stopsCount);

            int column, row;
            string[] busStopGraphics;
            int[] mapIndexFormula = new int[3];
            
            foreach (BusStop stop in TheBusStopArray.GetBusStops())
            {
                if (stop == null)
                    break;
                else
                {
                    (column, row) = stop.GetPosition();
                    busStopGraphics = stop.GetBusStopGraphic();

                    mapIndexFormula[0] = 200 * row + column - 201;
                    mapIndexFormula[1] = 200 * row + column - 1;
                    mapIndexFormula[2] = 200 * row + column + 199;

                    for (int index = 0; index < 3; index++)
                        TheRouteMap = TheRouteMap.Replace("   ", busStopGraphics[index], mapIndexFormula[index], 3);
                }
            }

            WindowBuffer = ToString().Length / 200 - 1;
        }

        public string TheDrawnMap()
        {
            return TheRouteMap.ToString();
        }
        public override string ToString()
        {
            return TheBusStopArray.ToString();
        }
    }
}
