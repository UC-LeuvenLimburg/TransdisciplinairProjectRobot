using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RondleidingRobot.Models
{
    //Ultrasound locatien is de verzameling van alle sensoren.
    public class UltraSoundLocation
    {
        private UltraSoundSideSensor leftSensor;
        private UltraSoundSideSensor rightSensor;
        private UltraSoundStraightSensor forwardSensor;
        private UltraSoundStraightSensor backwardsSensor;
        private AngleSensor angleSensor;

        //aangezien we de hoek en de afstand vooruit direct nodig hebben zijn hier getters voor gemaakt.
        public int StraightDistance 
        {
            get 
            {
                return forwardSensor.Distance;
            }
        }
        public double Angle
        {
            get 
            {
                return angleSensor.Angle;
            }
        }

        //het aanmaken van alle sensoren
        public UltraSoundLocation() 
        {
            leftSensor = new UltraSoundSideSensor(30, 3);
            rightSensor = new UltraSoundSideSensor(30, 3);
            forwardSensor = new UltraSoundStraightSensor();
            backwardsSensor = new UltraSoundStraightSensor();
            angleSensor = new AngleSensor();
        }

        //om links aan te houden gebruiken we de linkers sensor we moeten zorgen dat we altijd vertragen om te corrigeren en niet onder nul komen.
        public string HoldLeft(int distance, int speed) 
        {
            int turningSpeed = leftSensor.HoldDistance(distance);
            int leftmotor = speed;
            int rightmotor = speed;
            if(turningSpeed > 0) leftmotor -= leftSensor.HoldDistance(distance);
            if (turningSpeed < 0) rightmotor += leftSensor.HoldDistance(distance);
            if (leftmotor < 0) leftmotor = 0;
            if (rightmotor < 0) rightmotor = 0;
            return "W:" + leftmotor + ":" + rightmotor;
        }

        //om rechts aan te houden gebruiken we de rechter sensor we moeten zorgen dat we altijd vertragen om te corrigeren en niet onder nul komen.
        public string HoldRight(int distance, int speed)
        {
            int turningSpeed = leftSensor.HoldDistance(distance);
            int leftmotor = speed;
            int rightmotor = speed;
            if (turningSpeed > 0) rightmotor -= turningSpeed;
            if (turningSpeed < 0) leftmotor += turningSpeed;
            if (leftmotor < 0) leftmotor = 0;
            if (rightmotor < 0) rightmotor = 0;
            return "W:" + leftmotor + ":" + rightmotor;
        }

        //om rechtoor te gaan via de snesor gebruiken we de hoeksensor we moeten zorgen dat we altijd vertragen om te corrigeren en niet onder nul komen.
        public string HoldStraight(int speed, double angle)
        {
            int turningSpeed = angleSensor.HoldStraight(angle);
            int leftmotor = speed;
            int rightmotor = speed;
            if (turningSpeed > 0) leftmotor -= turningSpeed;
            if (turningSpeed < 0) rightmotor += turningSpeed;
            if (leftmotor < 0) leftmotor = 0;
            if (rightmotor < 0) leftmotor = 0;
            return "W:" + leftmotor + ":" + rightmotor;
        }

        //wanneer er een bericht binnen komt moeten dit decoderen en aan de juist sensor doorgeven.
        public void AddMesurement(string input) 
        {
            string[] inputs = input.ToLower().Split(';');
            foreach (string s in inputs) 
            {
                if (s.Contains('w')) 
                {
                    forwardSensor.Distance = Convert.ToInt32(s.Split(':').LastOrDefault());
                }
                else if (s.Contains('a'))
                {
                    leftSensor.AddValue(Convert.ToInt32(s.Split(':').LastOrDefault()));
                }
                else if (s.Contains('d'))
                {
                    rightSensor.AddValue(Convert.ToInt32(s.Split(':').LastOrDefault()));
                }
                else if (s.Contains('s'))
                {
                    backwardsSensor.Distance = Convert.ToInt32(s.Split(':').LastOrDefault());
                }
                else if (s.Contains('h'))
                {
                    angleSensor.Angle = Convert.ToDouble(s.Split(':').LastOrDefault().Replace('.',','));
                }
            }
        }
    }
}
