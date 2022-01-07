using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    //een IOutpu die gewoon de output print voor debugging
    public class PrintOutput : IOutput
    {
        public event InputEvent inputEvent;

        public void Output(string output)
        {
            Console.WriteLine(output);
        }
    }
}
