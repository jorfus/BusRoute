using System;
using System.Collections.Generic;
using System.Text;

namespace BusRoute
{
    class BusStop
    {
        int[] Position { get; set; } = new int[2];
        int Orientation { get; set; }
        Bus ABus { get; set; }
        List<Passenger> Passengers { get; set; } = new List<Passenger>();
        string[] BusStopGraphics { get; set; } = new string[3];

        public BusStop(int column, int row, int orientation)
        {
            Position[0] = column;
            Position[1] = row;
            Orientation = orientation;

            for (int index = 0; index < BusStopGraphics.Length; index++)
            {
                switch (Orientation)
                {
                    case 0:
                        BusStopGraphics[0] = "   ";
                        BusStopGraphics[1] = "S S";
                        BusStopGraphics[2] = "SSS";
                        break;
                    case 1:
                        BusStopGraphics[0] = "SS ";
                        BusStopGraphics[1] = "S  ";
                        BusStopGraphics[2] = "SS ";
                        break;
                    case 2:
                        BusStopGraphics[0] = "SSS";
                        BusStopGraphics[1] = "S S";
                        BusStopGraphics[2] = "   ";
                        break;
                    case 3:
                        BusStopGraphics[0] = " SS";
                        BusStopGraphics[1] = "  S";
                        BusStopGraphics[2] = " SS";
                        break;
                    default:
                        break;
                }
            }
        }
        
        public int[] GetPosition()
        {
            return Position;
        }
        public int GetOrientation()
        {
            return Orientation;
        }
        public string[] GetBusStopGraphics()
        {
            return BusStopGraphics;
        }
        public override string ToString()
        {
            return $"{Position[0] + 1},{Position[1] + 1}: {Orientation + 1} | ";
        }
    }
}
