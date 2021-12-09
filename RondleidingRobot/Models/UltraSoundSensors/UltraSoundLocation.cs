using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RondleidingRobot.Models
{
    public class UltraSoundLocation
    {
        private UltraSoundSideSensor leftSensor;
        private UltraSoundSideSensor rightSensor;

        public UltraSoundLocation() 
        {
            leftSensor = new UltraSoundSideSensor(30, 5);
            rightSensor = new UltraSoundSideSensor(30, 5);
        }

        public string HoldLeft(int distance, int speed) 
        {
            if (speed > 100) speed = 100;
            int leftmotor = speed + leftSensor.HoldDistance(distance);
            int rightmotor = speed;
            if (leftmotor < 0) leftmotor = 0;
            if (rightmotor < 0) leftmotor = 0;
            return "W:" + leftmotor + ":" + rightmotor;
        }

        public string HoldRight(int distance, int speed)
        {
            if (speed > 100) speed = 100;
            int leftmotor = speed;
            int rightmotor = speed + rightSensor.HoldDistance(distance);
            if (leftmotor < 0) leftmotor = 0;
            if (rightmotor < 0) leftmotor = 0;
            return "W:" + leftmotor + ":" + rightmotor;
        }

        public void AddMesurement(string input) 
        {
            string[] inputs = input.ToLower().Split(';');
            foreach (string s in inputs) 
            {
                if (s.Contains('w')) 
                { 
                
                }
                else if (s.Contains('a'))
                {
                    leftSensor.AddValue(Convert.ToInt32(s.Split(':').Last()));
                }
                else if (s.Contains('d'))
                {
                    rightSensor.AddValue(Convert.ToInt32(s.Split(':').Last()));
                }
                else if (s.Contains('s'))
                {
                    
                }
                else if (s.Contains('h'))
                {

                }
            }
        }
    }
}
