using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusRoute
{
    class Bus
    {
        BusStop[] Route { get; set; }
        BusStop PresentAt { get; set; }
        Road CurrentRoad { get; set; }
        int[] Position { get; set; }
        Passenger[] Seats { get; set; } = new Passenger[9];
        char[] BusGraphics { get; set; } = new char[] { '[', '0', ']' };
        
        public Bus(BusStop[] aBusStopArray, Road aRoad, bool first)
        {
            if (first)
                Route = aBusStopArray;
            else
            {
                Route = aBusStopArray.Reverse().ToArray();
                aRoad.GetPositions().Last();
            }

            PresentAt = Route[0];
            CurrentRoad = aRoad;

            switch (PresentAt.GetOrientation())
            {
                case 0:
                    Position = new int[] { PresentAt.GetPosition()[0], PresentAt.GetPosition()[1] - 1 };
                    break;
                case 1:
                    Position = new int[] { PresentAt.GetPosition()[0] + 1, PresentAt.GetPosition()[1] };
                    break;
                case 2:
                    Position = new int[] { PresentAt.GetPosition()[0], PresentAt.GetPosition()[1] + 1 };
                    break;
                case 3:
                    Position = new int[] { PresentAt.GetPosition()[0] - 1, PresentAt.GetPosition()[1] };
                    break;
                default:
                    break;
            }
        }

        void SetPassengerNumber()
        {
            BusGraphics[1] = Convert.ToChar(Seats);
        }
        public int[] GetPosition()
        {
            return Position;
        }
        public Road GetCurrentRoad()
        {
            return CurrentRoad;
        }
        public StringBuilder DrawBus(StringBuilder aRoadMap)
        {
            if (PresentAt.GetOrientation() == 0 || PresentAt.GetOrientation() == 2)
            {
                aRoadMap.Replace(' ', BusGraphics[0], Position[1] * 200 + Position[0] - 1, 1);
                aRoadMap.Replace(' ', BusGraphics[1], Position[1] * 200 + Position[0], 1);
                aRoadMap.Replace(' ', BusGraphics[2], Position[1] * 200 + Position[0] + 1, 1);
            }
            else
            {
                aRoadMap.Replace(' ', BusGraphics[0], Position[1] * 200 + Position[0], 1);
                aRoadMap.Replace(' ', BusGraphics[1], Position[1] * 201 + Position[0], 1);
                aRoadMap.Replace(' ', BusGraphics[2], Position[1] * 199 + Position[0], 1);
            }

            return aRoadMap;
        }
    }
}
