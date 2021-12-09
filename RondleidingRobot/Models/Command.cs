using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RondleidingRobot.Models
{
    public class Command
    {
        public string CommandString { get; set; }
        public List<string> Arguments { get; set; }

        public Command()
        {
            CommandString = "";
            Arguments = new List<string>();
        }

        public Command(string command) 
        {
            string[] collums = command.Split(':');
            switch (collums[0].ToLower())
            {
                case "links":
                    CommandString = "A";
                    break;
                case "rechts":
                    CommandString = "D";
                    break;
                case "vooruit":
                    CommandString = "W";
                    break;
                case "achteruit":
                    CommandString = "S";
                    break;
                case "speak":
                    CommandString = "SPEAK";
                    break;
                default:
                    CommandString = "X";
                    break;
            }
            Arguments = collums.ToList();
            Arguments.Remove(Arguments.First());
        }

        public Command(string commandString, string argument) 
        {
            CommandString = commandString;
            Arguments = new List<string>();
            Arguments.Add(argument);
        }

        public override string ToString()
        {
            string argString = "";
            foreach (string arg in Arguments)
            {
                argString += ":" + arg;
            }
            return CommandString + argString;
        }
    }
}
