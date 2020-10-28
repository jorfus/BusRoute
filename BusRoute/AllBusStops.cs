using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace BusRoute
{
    class AllBusStops
    {
        BusStop[] TheBusStopArray { get; set; } = new BusStop[10];
        Random Rand { get; set; } = new Random();

        public AllBusStops()
        {
            int newColumn;
            int newRow;

            int existingColumn;
            int existingRow;

            int differenceColumn;
            int differenceRow;

            for (int index = 0; index < TheBusStopArray.Length; index++)
            {
                bool positionConflict = true;
                while (positionConflict)
                {
                    newColumn = Rand.Next(1, 198 + 1);
                    newRow = Rand.Next(1, 53 + 1);

                    foreach (BusStop busStop in TheBusStopArray)
                    {
                        if (busStop == null)
                        {
                            TheBusStopArray[index] = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
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
                        TheBusStopArray[index] = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
                }
            }
        }

        public BusStop[] GetBusStops()
        {
            return TheBusStopArray;
        }
        public override string ToString()
        {
            string str = "";

            foreach (BusStop stop in TheBusStopArray)
                str += stop;

            return str;
        }
    }
}
