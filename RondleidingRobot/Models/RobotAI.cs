using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RondleidingRobotWPF.Models
{
    public abstract class RobotAI
    {
        private const int MAXSPEED = 255;
        private const int SPEEDSTEP = 30;

        public static List<string> CalculateMovements(List<string> commands)
        {
            List<string> result = new List<string>();
            foreach (string command in commands) 
            {
                result.AddRange(CalculateMovement(command));
            }
            return result;
        }

        private static List<string> CalculateMovement(string command)
        {
            string[] columns;
            char[] separators = { ':' };
            columns = command.Split(separators);
            string beweging = Convert.ToString(columns[0]);
            int aantal = Convert.ToInt32(columns[1]);
            List<string> result = new List<string>();

            for (int i = 0; i < aantal; i++) 
            {
                string decodedBeweging;
                switch (beweging)
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

                int snelheid;
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
                result.Add(decodedBeweging + ':' + snelheid);

            }
            return result;
        }
    }
}
