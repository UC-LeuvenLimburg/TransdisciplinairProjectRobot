using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RondleidingRobot.Models
{
    public abstract class RobotAI
    {
        private const int MAXSPEED = 255;
        private const int SPEEDSTEP = 30;

        public static List<Command> CalculateMovements(List<string> commands)
        {
            List<Command> result = new List<Command>();
            foreach (string command in commands) 
            {
                if (command.ToLower().Contains("speak"))
                {
                    string[] colums = command.Split(':');
                    Command c = new Command();
                    c.CommandString = "speak";
                    c.Arguments = new List<string>();
                    c.Arguments.Add(colums[1]);
                    result.Add(c);
                }
                else
                {
                    result.AddRange(CalculateMovement(command));
                }  
            }
            return result;
        }

        private static List<Command> CalculateMovement(string command)
        {
            string[] columns;
            char[] separators = { ':' };
            columns = command.Split(separators);
            string beweging = Convert.ToString(columns[0]);
            int aantal = Convert.ToInt32(columns[1]);
            List<Command> result = new List<Command>();

            for (int i = 0; i < aantal; i++) 
            {
                string decodedBeweging;
                switch (beweging.ToLower())
                {
                    case "links":
                        decodedBeweging = "A";
                        break;
                    case "rechts":
                        decodedBeweging = "D";
                        break;
                    case "vooruit":
                        decodedBeweging = "W";
                        break;
                    case "achteruit":
                        decodedBeweging = "S";
                        break;
                    default:
                        decodedBeweging = "X";
                        break;
                }

                int snelheid = 0;
                if (i < aantal / 2)
                {
                    snelheid = SPEEDSTEP * i;
                }
                else 
                {
                    snelheid = SPEEDSTEP * (aantal - i);                 
                }
                if (snelheid > MAXSPEED) snelheid = MAXSPEED;
                if (snelheid < 0) snelheid = 0;
                Command c = new Command();
                c.CommandString = decodedBeweging;
                c.Arguments = new List<string>();
                c.Arguments.Add(Convert.ToString(snelheid));
                result.Add(c);

            }
            return result;
        }
    }
}
