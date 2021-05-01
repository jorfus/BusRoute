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
        BusStop[] TheBusStops { get; set; } = new BusStop[10];
        static Random Rand { get; set; } = new Random();

        public AllBusStops()
        {
            int[] position;
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

                    foreach (BusStop abusStop in TheBusStops)
                    {
                        if (abusStop == null)
                        {
                            TheBusStops[index] = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
                            break;
                        }
                        else
                        {
                            position = abusStop.GetPosition();

                            existingColumn = position[0];
                            existingRow = position[1];
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
                        TheBusStops[index] = new BusStop(newColumn, newRow, Rand.Next(0, 3 + 1));
                }
            }
        }

        public BusStop[] GetBusStops()
        {
            return TheBusStops;
        }
        public void SetBusStops(BusStop[] aBusStopArray)
        {
            TheBusStops = aBusStopArray;
        }
        public int[][] GetAllPositions()
        {
            return TheBusStops.Select(x => x.GetPosition()).ToArray();
        }
        public override string ToString()
        {
            string str = "";

            foreach (BusStop stop in TheBusStops)
                str += stop;

            return str;
        }
    }
}
