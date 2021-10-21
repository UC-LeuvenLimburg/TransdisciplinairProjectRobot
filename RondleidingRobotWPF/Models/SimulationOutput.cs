using RondleidingRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobotWPF.Models
{
    class SimulationOutput : IOutput
    {
        private Robot robot;

        public SimulationOutput(Robot robot) 
        {
            this.robot = robot;
        }

        public event InputEvent inputEvent;

        public void Output(string output)
        {
            robot.Command = output;
        }
    }
}
