using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    //een linear function is een lineare funtie waar we kijken of we verder of dichter naar bevoorbeeld een muur gaan en kijken we of een punt te ver uitschiet om bijvoorbeeld een tafelpoot te kunnen negeren.
    public class LinearFunction
    {
        private double yintercept;
        private double slope;
        private double rsquared;
        private int bufferLengt;

        public double Slope 
        {
            get
            {
                return slope;
            }
            set 
            {
                slope = Slope;
            }
        }

        public LinearFunction(double yintercept, double slope, double rsquared, int bufferLengt)
        {
            this.yintercept = yintercept;
            this.slope = slope;
            this.rsquared = rsquared;
            this.bufferLengt = bufferLengt;
        }

        public bool CheckIfOutliar(int y, int maxJumpSize) 
        {
            double yPred = bufferLengt * slope + yintercept;
            if(y > yPred + maxJumpSize || y < yPred - maxJumpSize) return true;
            return false;
        }
    }
}
