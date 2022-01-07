using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RondleidingRobot.Models
{
    public class UltraSoundSideSensor
    {
        private const int MARGIN = 20;
        private double[] sensorValues;
        private double angle;
        private LinearFunction function;
        private int numberOfOudliars;
        private double[] oudliarSensorValues;

        public UltraSoundSideSensor(double angle, int bufferLengt) 
        {
            this.angle = angle;
            sensorValues = new double[bufferLengt];
            oudliarSensorValues = new double[bufferLengt];
        }

        //om een waarde toe te voegen kijken we of het getal niet een te grote sprong maak en corrigeren we dan
        public bool AddValue(double value) 
        {   
            //als er nog geen functie is berkent berekenen we een nieuwe functie
            double[] newArray;
            if (function == null)
            {
                for (int i = 0; i < sensorValues.Length; i++) { sensorValues[i] = value; }
                function = LinearRegression();
            }
            //dan kijken we of een punt een te grote sprong maakt
            if (function.CheckIfOutliar(calculateY(value), 40))
            {
                if (numberOfOudliars == 0)
                {
                    for (int i = 0; i < oudliarSensorValues.Length; i++) { oudliarSensorValues[i] = value; }
                }
                else
                {
                    newArray = new double[oudliarSensorValues.Length];
                    Array.Copy(oudliarSensorValues, 1, newArray, 0, oudliarSensorValues.Length - 1);
                    oudliarSensorValues = newArray;
                    oudliarSensorValues[oudliarSensorValues.Length - 1] = value;
                }
                numberOfOudliars++;
                //wanneer er een paar punten na elkaar niet passen gaan we er vanuit dat we niet een tafelpoot hebben maar de muur heel snel dichter is gekomen dus gaan we de lijst met waardes aanpassen.
                if (numberOfOudliars >= 2)
                {
                    sensorValues = oudliarSensorValues;
                    function = LinearRegression();
                    return true;
                }
                function = LinearRegression();
                return false;
            }
            newArray = new double[sensorValues.Length];
            Array.Copy(sensorValues, 1, newArray, 0, sensorValues.Length - 1);
            sensorValues = newArray;
            sensorValues[sensorValues.Length - 1] = value;
            function = LinearRegression();
            return true;
        }

        //deze functie probeert een muur te volgen en te corrigeren als we dichter of verder komen
        public int HoldDistance(int distance) 
        {
            if (function == null) function = LinearRegression();
            Console.WriteLine(sensorValues);
            if (sensorValues.Average() > distance + MARGIN && function.Slope <= 0) return -10;
            if (sensorValues.Average() < distance - MARGIN && function.Slope >= 0) return 10;
            return 0;
        }

        private int calculateY(double distance)
        {
            return Convert.ToInt32(distance * Math.Sin(angle));
        }

        //deze functie doet gewoon een lineare regressie van een lijst van punten
        private LinearFunction LinearRegression()
        {
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = sensorValues.Length;

            for (int i = 0; i < sensorValues.Length; i++)
            {
                double x = i;
                double y = calculateY(sensorValues[i]);
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            double rsquared = dblR * dblR;
            double yintercept = meanY - ((sCo / ssX) * meanX);
            double slope = sCo / ssX;
            return new LinearFunction(yintercept, slope, rsquared, sensorValues.Length);
        }
    }
}
