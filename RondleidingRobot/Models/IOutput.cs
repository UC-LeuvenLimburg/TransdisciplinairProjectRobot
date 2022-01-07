using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    public delegate void InputEvent(object sender, string input);
    //de IOuptput interface zorgt dat er makkelijk geswitched kan worden tussen verschillende in en outputs
    public interface IOutput
    {
        public void Output(string output);
        public event InputEvent inputEvent;
    }
}
