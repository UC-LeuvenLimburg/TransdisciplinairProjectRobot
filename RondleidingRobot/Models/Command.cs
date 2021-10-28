using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    public class Command
    {
        public string CommandString { get; set; }
        public List<string> Arguments { get; set; }

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
