using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    //deze sensor is de sensor vooruit en achteruit en bied de mogelijkheid om extra controles uit te doen maar waren niet echt nodig.
    public class UltraSoundStraightSensor
    {
        private int distance;

        public UltraSoundStraightSensor() 
        {
            distance = 0;
        }

        public int Distance
        {
            get { return distance; }
            set 
            { 
                distance = value; 
            }
        }

    }
}
