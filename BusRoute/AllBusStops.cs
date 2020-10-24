using System;
using System.Collections.Generic;
using System.Text;
using HelperLibrary;

namespace BusRoute
{
    class AllBusStops
    {
        BusStop[] TheBusStops { get; set; }
        Random Rand { get; set; } = new Random();

        public AllBusStops(int stopsCount)
        {
            TheBusStops = new BusStop[stopsCount];

            BusStop newBusStop;

            int newColumn;
            int newRow;

            int existingColumn;
            int existingRow;

            int differenceColumn;
            int differenceRow;

            for (int index = 0; index < TheBusStops.Length; index++)
            {
                bool positionConflict = true;
                while (positionConflict)
                {
                    newColumn = Rand.Next(1, 198 + 1);
                    newRow = Rand.Next(1, 53 + 1);

                    foreach (BusStop busStop in TheBusStops)
                    {
                        if (busStop == null)
                        {
                            newBusStop = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
                            TheBusStops[index] = newBusStop;
                            break;
                        }
                        else
                        {
                            (existingColumn, existingRow) = busStop.GetPosition();

                            differenceColumn = newColumn - existingColumn;
                            differenceRow = newRow - existingRow;

                            if (differenceColumn < -9 || differenceColumn > 9 || differenceRow < -9 || differenceRow > 9)
                                positionConflict = false;
                            else
                            {
                                positionConflict = true;
                                break;
                            }
                        }
                    }

                    if (positionConflict == false)
                    {
                        newBusStop = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
                        TheBusStops[index] = newBusStop;
                    }
                }
            }
        }
 
        public BusStop[] GetBusStops()
        {
            return TheBusStops;
        }
        public override string ToString()
        {
            string str = "";

            foreach (BusStop busStop in TheBusStops)
                str += $"{busStop}";

            return str;
        }
    }
}
