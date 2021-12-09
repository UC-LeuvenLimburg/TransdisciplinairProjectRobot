using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    public class PrintOutput : IOutput
    {
        public event InputEvent inputEvent;

        public void Output(string output)
        {
            Console.WriteLine(output);
        }
    }
}
