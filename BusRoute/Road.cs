using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusRoute
{
    class Road
    {
        List<int[]> ARoad { get; set; } = new List<int[]>();
        int[] OriginDestinationDirection { get; set; }
        int[][] IllegalBusStopPositions { get; set; }
        static Random Rand { get; set; } = new Random();

        public Road(int[] originDestinationData, int[][] illegalBusStopPositions)
        {
            OriginDestinationDirection = originDestinationData;
            ARoad.Add(new int[] { OriginDestinationDirection[0], OriginDestinationDirection[1] });

            IllegalBusStopPositions = illegalBusStopPositions;
            NextPosition();

            ARoad.Add(new int[] { OriginDestinationDirection[2], OriginDestinationDirection[3] });

            OriginDestinationDirection = null;
        }

        void NextPosition()
        {
            while (!(OriginDestinationDirection[0] == OriginDestinationDirection[2] && OriginDestinationDirection[1] == OriginDestinationDirection[3]))
            {
                NewDirection();

                switch (OriginDestinationDirection[4], OriginDestinationDirection[5])
                {
                    case (-1, -1):
                        ThePosition(1, 1, 0, 0, 1, 1, 0);
                        break;
                    case (-1, 0):
                        ThePosition(1, 1, 1, -1, 0, 1, 1);
                        break;
                    case (-1, 1):
                        ThePosition(0, 1, 1, -1, -1, 0, 2);
                        break;
                    case (0, -1):
                        ThePosition(-1, 0, 1, 1, 1, 1, 3);
                        break;
                    case (0, 1):
                        ThePosition(-1, 0, 1, -1, -1, -1, 4);
                        break;
                    case (1, -1):
                        ThePosition(0, -1, -1, 1, 1, 0, 5);
                        break;
                    case (1, 0):
                        ThePosition(-1, -1, -1, 1, 0, -1, 6);
                        break;
                    case (1, 1):
                        ThePosition(-1, -1, 0, 0, -1, -1, 7);
                        break;
                    default:
                        break;
                }
            }
        }
        void NewDirection()
        {
            OriginDestinationDirection[4] = Math.Sign(OriginDestinationDirection[0] - OriginDestinationDirection[2]);
            OriginDestinationDirection[5] = Math.Sign(OriginDestinationDirection[1] - OriginDestinationDirection[3]);
        }
        void ThePosition(int columnOne, int columnTwo, int columnThree, int rowOne, int rowTwo, int rowThree, int direction)
        {
            int[][] positions = new int[3][];
            int originColumn = OriginDestinationDirection[0], originRow = OriginDestinationDirection[1];

            positions[0] = new int[] { originColumn + columnOne, originRow + rowOne };
            positions[1] = new int[] { originColumn + columnTwo, originRow + rowTwo };
            positions[2] = new int[] { originColumn + columnThree, originRow + rowThree };

            int[][] legalPositions = positions.Where(x => !IllegalBusStopPositions.Contains(x)).ToArray();

            int random;
            if (legalPositions.Length == 1)
            {
                ARoad.Add(legalPositions[0]);
                OriginDestinationDirection[0] = legalPositions[0][0];
                OriginDestinationDirection[1] = legalPositions[0][1];
            }
            else if (legalPositions.Length == 2)
            {
                random = Rand.Next(0, 1 + 1);
                if (random == 0)
                    ARoad.Add(new int[] { OriginDestinationDirection[0] = legalPositions[0][0], OriginDestinationDirection[1] = legalPositions[0][1] });
                else
                    ARoad.Add(new int[] { OriginDestinationDirection[0] = legalPositions[1][0], OriginDestinationDirection[1] = legalPositions[1][1] });
            }
            else if (legalPositions.Length == 3)
            {
                random = Rand.Next(0, 100 + 1);
                if (random < 15)
                    ARoad.Add(new int[] { OriginDestinationDirection[0] = legalPositions[0][0], OriginDestinationDirection[1] = legalPositions[0][1] });
                else if (random < 85)
                    ARoad.Add(new int[] { OriginDestinationDirection[0] = legalPositions[1][0], OriginDestinationDirection[1] = legalPositions[1][1] });
                else
                    ARoad.Add(new int[] { OriginDestinationDirection[0] = legalPositions[2][0], OriginDestinationDirection[1] = legalPositions[2][1] });
            }
            else
                switch (direction, Rand.Next(0, 1 + 1))
                {
                    case (0, 0):
                    case (7, 1):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] += 1, OriginDestinationDirection[1] -= 1 });
                        break;
                    case (0, 1):
                    case (7, 0):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] -= 1, OriginDestinationDirection[1] += 1 });
                        break;
                    case (1, 0):
                    case (6, 0):
                        ARoad.Add(new int[] { OriginDestinationDirection[0], OriginDestinationDirection[1] -= 1 });
                        break;
                    case (1, 1):
                    case (6, 1):
                        ARoad.Add(new int[] { OriginDestinationDirection[0], OriginDestinationDirection[1] += 1 });
                        break;
                    case (2, 0):
                    case (5, 0):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] -= 1, OriginDestinationDirection[1] -= 1 });
                        break;
                    case (2, 1):
                    case (5, 1):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] += 1, OriginDestinationDirection[1] += 1 });
                        break;
                    case (3, 0):
                    case (4, 0):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] -= 1, OriginDestinationDirection[1] });
                        break;
                    case (3, 1):
                    case (4, 1):
                        ARoad.Add(new int[] { OriginDestinationDirection[0] += 1, OriginDestinationDirection[1] });
                        break;
                    default:
                        break;
                }
        }
        public List<int[]> GetPositions()
        {
            return ARoad;
        }
        public override string ToString()
        {
            string str = " | ";
            foreach (int[] position in ARoad)
                str += $"({position[0] + 1},{position[1] + 1}) | ";

            return str;
        }
    }
}
