using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    //de angle sensor kijkt eonder welke hoe we staan
    class AngleSensor
    {

        private double angle;

        //als we de hoek willen veranderen zorgen dat de hoek altijd tussen de 0 en 360 graden is
        public double Angle
        {
            get { return angle; }
            set 
            {
                double x = value;
                if (x < 0) x += 360 * Math.Ceiling(x / 360);
                x = x % 360;
                angle = x; 
            }
        }

        //als we te ver afwijken van de hoek proberen we de richting van de robot te corrigeren
        public int HoldStraight(double angle) 
        {
            if (Angle >= angle + 1) return -5;
            if (Angle <= angle - 1) return 5;
            return 0;
        }
    }
}
