using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusRoute
{
    class AllRoads
    {
        Road[] TheRoads { get; set; }
        static int[] OriginDestinationDirection { get; set; } = new int[6];
        static Random Rand { get; set; } = new Random();

        public AllRoads() : this(new AllBusStops()) { }
        public AllRoads(AllBusStops anAllBusStops)
        {
            BusStop[] aBusStopArray = anAllBusStops.GetBusStops();
            TheRoads = new Road[aBusStopArray.Length - 1];

            int originColumn, originRow, destinationColumn, destinationRow, columnDirection, rowDirection, index, columnModifier = -1, rowModifier = -1, count = 0;
            double shortestPath;

            int[] originPosition, destinationPosition;
            double[] hypotenuseArray = new double[aBusStopArray.Length];
            int[][] illegalRoadPositions = new int[18][];

            BusStop originStop = aBusStopArray[0], destinationStop;

            while (true)
            {
                originPosition = originStop.GetPosition();
                OriginDestinationDirection[0] = originColumn = originPosition[0];
                OriginDestinationDirection[1] = originRow = originPosition[1];

                for (index = 0; index < aBusStopArray.Length; index++)
                {
                    if (index == Array.FindIndex(aBusStopArray, x => x == originStop))
                        hypotenuseArray[index] = 198;
                    else if (hypotenuseArray[index] == 198)
                        continue;
                    else
                    {
                        originPosition = aBusStopArray[index].GetPosition();
                        OriginDestinationDirection[2] = destinationColumn = originPosition[0];
                        OriginDestinationDirection[3] = destinationRow = originPosition[1];

                        columnDirection = originColumn - destinationColumn;
                        rowDirection = originRow - destinationRow;
                        OriginDestinationDirection[4] = Math.Sign(columnDirection);
                        OriginDestinationDirection[5] = Math.Sign(rowDirection);

                        hypotenuseArray[index] = Math.Sqrt(Math.Pow(columnDirection, 2) + Math.Pow(rowDirection, 2));
                    }
                }

                shortestPath = hypotenuseArray.Min();

                if (shortestPath < 198)
                {
                    destinationStop = aBusStopArray[Array.FindIndex(hypotenuseArray, x => x == shortestPath)];
                    destinationPosition = destinationStop.GetPosition();

                    (OriginDestinationDirection[0], OriginDestinationDirection[1]) = DepartureOrArrivalPosition(originStop.GetOrientation(), true);
                    (OriginDestinationDirection[2], OriginDestinationDirection[3]) = DepartureOrArrivalPosition(destinationStop.GetOrientation(), false);

                    foreach (int[] illegals in illegalRoadPositions)
                    {
                        illegalRoadPositions
                    }

                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] - 1, 0);
                    originPosition.SetValue(originPosition[1] - 1, 1);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] + 1, 0);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] + 1, 0);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] - 2, 0);
                    originPosition.SetValue(originPosition[1] + 1, 1);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] + 2, 0);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] - 2, 0);
                    originPosition.SetValue(originPosition[1] + 1, 1);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] + 1, 0);
                    illegalRoadPositions[index++] = originPosition;

                    originPosition.SetValue(originPosition[0] + 1, 0);
                    illegalRoadPositions[index++] = originPosition;

                    TheRoads[count] = new Road(OriginDestinationDirection, illegalRoadPositions);

                    originStop = destinationStop;
                    count++;
                }
                else
                    break;
            }

            anAllBusStops.SetBusStops(aBusStopArray);
        }

        (int, int) DepartureOrArrivalPosition(int orientation, bool isOrigin)
        {
            int column, row, columnDirection, rowDirection;

            if (isOrigin)
            {
                column = OriginDestinationDirection[0];
                row = OriginDestinationDirection[1];
                columnDirection = OriginDestinationDirection[4];
                rowDirection = OriginDestinationDirection[5];
            }
            else
            {
                column = OriginDestinationDirection[2];
                row = OriginDestinationDirection[3];
                columnDirection = OriginDestinationDirection[4] * -1;
                rowDirection = OriginDestinationDirection[5] * -1;
            }
            switch (orientation, columnDirection, rowDirection)
            {
                case (0, 0, -1):
                case (0, 0, 1):
                    (column, row) = FiftyFiftyPosition(orientation, column, row);
                    break;
                case (0, -1, -1):
                case (0, -1, 0):
                case (0, -1, 1):
                case (1, -1, -1):
                case (1, -1, 1):
                case (1, 1, -1):
                    column += 1;
                    row -= 1;
                    break;
                case (1, -1, 0):
                case (1, 1, 0):
                    (column, row) = FiftyFiftyPosition(orientation, column, row);
                    break;
                case (0, 1, -1):
                case (0, 1, 0):
                case (0, 1, 1):
                case (3, -1, 1):
                case (3, 0, 1):
                case (3, 1, 1):
                    column -= 1;
                    row -= 1;
                    break;
                case (2, 0, -1):
                case (2, 0, 1):
                    (column, row) = FiftyFiftyPosition(orientation, column, row);
                    break;
                case (2, 1, -1):
                case (2, 1, 0):
                case (2, 1, 1):
                case (3, -1, -1):
                case (3, 0, -1):
                case (3, 1, -1):
                    column -= 1;
                    row += 1;
                    break;
                case (3, -1, 0):
                case (3, 1, 0):
                    (column, row) = FiftyFiftyPosition(orientation, column, row);
                    break;
                case (1, 0, -1):
                case (1, 0, 1):
                case (1, 1, 1):
                case (2, -1, -1):
                case (2, -1, 0):
                case (2, -1, 1):
                    column += 1;
                    row += 1;
                    break;
                default:
                    break;
            }

            return (column, row);
        }
        (int column, int row) FiftyFiftyPosition(int orientation, int column, int row)
        {
            int randInt = Rand.Next(0, 1 + 1);

            switch (orientation, randInt)
            {
                case (0, 0):
                case (3, 1):
                    column -= 1;
                    row -= 1;
                    break;
                case (0, 1):
                case (1, 0):
                    column += 1;
                    row -= 1;
                    break;
                case (2, 1):
                case (3, 0):
                    column -= 1;
                    row += 1;
                    break;
                case (1, 1):
                case (2, 0):
                    column += 1;
                    row += 1;
                    break;
                default:
                    break;
            }

            return (column, row);
        }
        public Road[] GetRoads()
        {
            return TheRoads;
        }
        public override string ToString()
        {
            string str = "";
            foreach (Road road in TheRoads)
                str += road;

            return str;
        }
    }
}
