using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    public delegate void InputEvent(object sender, string input);
    interface IOutput
    {
        public void Output(string output);
        public event InputEvent inputEvent;

    }
}
