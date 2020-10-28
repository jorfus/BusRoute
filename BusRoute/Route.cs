using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelperLibrary;

namespace BusRoute
{
    class Route
    {
        List<int[]> ARoute { get; set; } = new List<int[]>();
        Random Rand { get; set; } = new Random();

        public Route(AllBusStops allTheStops)
        {
            int column, row;

            int targetColumn;
            int targetRow;

            int columnDifference;
            int rowDifference;

            double[] hypotenuseArray;
            double shortestPath;
            int targetIndex;

            BusStop[] allTheStopsCopy = new BusStop[10];
            allTheStops.GetBusStops().CopyTo(allTheStopsCopy, 0);

            int index = 0;
            while (index < allTheStopsCopy.Length)
            {
                BusStop originStop = allTheStopsCopy[index];
                (column, row) = originStop.GetPosition();

                hypotenuseArray = new double[9];

                for (int jindex = 1; jindex < allTheStopsCopy.Length; jindex++)
                {
                    if (jindex == index)
                        hypotenuseArray[jindex - 1] = 198;
                    else
                    {
                        (targetColumn, targetRow) = allTheStopsCopy[jindex].GetPosition();

                        columnDifference = column - targetColumn;
                        rowDifference = row - targetRow;

                        hypotenuseArray[jindex - 1] = Math.Sqrt(Math.Pow(columnDifference, 2) + Math.Pow(rowDifference, 2));
                    }

                }

                shortestPath = hypotenuseArray.Min();
                targetIndex = Array.FindIndex(hypotenuseArray, x => x == shortestPath) + 1;

                if (shortestPath < 198)
                {
                    BusStop targetStop = allTheStopsCopy[targetIndex];
                    (targetColumn, targetRow) = targetStop.GetPosition();

                    columnDifference = column - targetColumn;
                    rowDifference = row - targetRow;

                    int orientation = originStop.GetOrientation();
                    (column, row) = DepartureOrArrivalPosition(orientation, columnDifference, rowDifference, false, column, row);
                    ARoute.Add(new int[2] { row, column });

                    orientation = targetStop.GetOrientation();
                    (column, row) = DepartureOrArrivalPosition(orientation, columnDifference, rowDifference, true, targetColumn, targetRow);
                    ARoute.Add(new int[2] { row, column });
                }

                index++;
            }
        }

        (int column, int row) DepartureOrArrivalPosition(int orientation, int columnDifference, int rowDifference, bool invertDifferenceSign, int column, int row)
        {
            if (invertDifferenceSign)
            {
                columnDifference *= -1;
                rowDifference *= -1;
            }
            switch (orientation, Math.Sign(columnDifference), Math.Sign(rowDifference))
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
        public List<int[]> GetRoute()
        {
            return ARoute;
        }
        public override string ToString()
        {
            string str = "| ";
            foreach (int[] routeStop in ARoute)
                str += $"({routeStop[1] + 1},{routeStop[0] + 1}) |";
            
            return str;
        }
    }
}
