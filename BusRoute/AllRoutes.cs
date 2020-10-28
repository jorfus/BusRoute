using System;
using System.Collections.Generic;
using System.Text;

namespace BusRoute
{
    class AllRoutes
    {
        List<Route> TheRouteList { get; set; } = new List<Route>();
        
        public AllRoutes(AllBusStops theBusStops)
        {
            Route aRoute = new Route(theBusStops);
            TheRouteList.Add(aRoute);
        }

        public List<Route> GetRoutes()
        {
            return TheRouteList;
        }
        public override string ToString()
        {
            string str = "";
            foreach (Route route in TheRouteList)
                str += route;

            return str;
        }
    }
}
