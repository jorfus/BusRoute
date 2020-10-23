using System;
using System.Collections.Generic;
using System.Text;

namespace BusRoute
{
    class BusStop
    {
        int[] Position { get; set; } = new int[2];
        int Orientation { get; set; }
        string[] BusStopGraphic { get; set; } = new string[3];
        List<Passenger> Passengers { get; set; } = new List<Passenger>();
        
        public BusStop(int column, int row, int orientation)
        {
            Position[1] = column;
            Position[0] = row;
            Orientation = orientation;

            for (int index = 0; index < BusStopGraphic.Length; index++)
            {
                switch (Orientation)
                {
                    case 0:
                        BusStopGraphic[0] = "   ";
                        BusStopGraphic[1] = "S S";
                        BusStopGraphic[2] = "SSS";
                        break;
                    case 1:
                        BusStopGraphic[0] = "SS ";
                        BusStopGraphic[1] = "S  ";
                        BusStopGraphic[2] = "SS ";
                        break;
                    case 2:
                        BusStopGraphic[0] = "SSS";
                        BusStopGraphic[1] = "S S";
                        BusStopGraphic[2] = "   ";
                        break;
                    case 3:
                        BusStopGraphic[0] = " SS";
                        BusStopGraphic[1] = "  S";
                        BusStopGraphic[2] = " SS";
                        break;
                    default:
                        break;
                }
            }
        }
        
        public (int, int) GetPosition()
        {
            return (Position[1], Position[0]);
        }
        public string[] GetBusStopGraphic()
        {
            return BusStopGraphic;
        }
        public override string ToString()
        {
            return $"{Position[1]},{Position[0]}: {Orientation} |";
        }
    }
}
