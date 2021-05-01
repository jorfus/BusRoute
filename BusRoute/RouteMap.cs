using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BusRoute
{
    class RouteMap
    {
        StringBuilder TheRouteMap { get; set; } = new StringBuilder(new string(' ', 11000), 11000);
        AllRoads TheAllRoads { get; set; }
        AllBusStops TheAllBusStops { get; set; }
        AllBuses TheAllBuses { get; set; }

        public RouteMap() : this(new AllBusStops()) { }
        public RouteMap(AllBusStops anAllBusStops)
        {
            for (int index = 0; index < 10900; index++)
            {
                if (((index + 1) % 10 == 0 && index <= 200) || (index + 200) % 2000 == 0)
                    TheRouteMap.Replace(' ', 'X', index, 1);
            }

            TheAllRoads = new AllRoads(anAllBusStops);
            TheAllBusStops = anAllBusStops;
            TheAllBuses = new AllBuses(TheAllBusStops.GetBusStops(), TheAllRoads.GetRoads());

            int[] busStopPosition;
            int column, row;
            string[] busStopGraphics;
            int[] mapIndexFormula = new int[3];

            foreach (BusStop stop in TheAllBusStops.GetBusStops())
            {
                busStopPosition = stop.GetPosition();
                column = busStopPosition[0];
                row = busStopPosition[1];

                busStopGraphics = stop.GetBusStopGraphics();

                mapIndexFormula[0] = 200 * row + column - 201;
                mapIndexFormula[1] = 200 * row + column - 1;
                mapIndexFormula[2] = 200 * row + column + 199;

                for (int index = 0; index < 3; index++)
                    TheRouteMap.Replace("   ", busStopGraphics[index], mapIndexFormula[index], 3);
            }
        }

        public string PartialDraw()
        {
            StringBuilder aRoadMap = new StringBuilder(TheRouteMap.ToString());

            foreach (Bus aBus in TheAllBuses.GetBuses())
            {
                foreach (int[] roadPosition in aBus.GetCurrentRoad().GetPositions())
                    aRoadMap.Replace(' ', 'O', roadPosition[1] * 200 + roadPosition[0], 1);

                aRoadMap = aBus.DrawBus(aRoadMap);
            }

            return aRoadMap.ToString();
        }
        public string FullDraw()
        {
            StringBuilder aRoadMap = new StringBuilder(TheRouteMap.ToString());

            foreach (Road aRoad in TheAllRoads.GetRoads())
                foreach (int[] roadPosition in aRoad.GetPositions())
                    aRoadMap.Replace(' ', 'O', roadPosition[1] * 200 + roadPosition[0], 1);

            foreach (Bus aBus in TheAllBuses.GetBuses())
                aRoadMap = aBus.DrawBus(aRoadMap);

            return aRoadMap.ToString();
        }
        public RouteMap NewRouteMap(AllBusStops anAllBusStops)
        {
            RouteMap aRouteMap;
            if (anAllBusStops == null)
                aRouteMap = new RouteMap();
            else
                aRouteMap = new RouteMap(anAllBusStops);

            return aRouteMap;
        }
        public override string ToString()
        {
            return $"{TheAllBusStops}";
        }
    }
}
