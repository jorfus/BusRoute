using System;
using System.Collections.Generic;
using System.Text;

namespace BusRoute
{
    class AllBuses
    {
        Bus[] TheBuses { get; set; } = new Bus[2];

        public AllBuses(BusStop[] aBusStopArray, Road[] aRoadArray)
        {
            TheBuses[0] = new Bus(aBusStopArray, aRoadArray[0], true);
            TheBuses[1] = new Bus(aBusStopArray, aRoadArray[aRoadArray.GetUpperBound(0)], false);
        }

        public Bus[] GetBuses()
        {
            return TheBuses;
        }
    }
}
