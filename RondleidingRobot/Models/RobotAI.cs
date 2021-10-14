using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RondleidingRobotWPF.Models
{
    public abstract class RobotAI
    {

        public RobotAI()
        {
        }

        public static string Move(string currentCommand)
        {
            switch (currentCommand)
            {
                case "links": //Naar links
                    return "A";
                case "rechts": //Naar rechts
                    return "D";
                case "vooruit": //Naar voor
                    return "W";
                case "achteruit": //Naar achter
                    return "S";
                default:
                    return "X";
            }
        }
    }
}
