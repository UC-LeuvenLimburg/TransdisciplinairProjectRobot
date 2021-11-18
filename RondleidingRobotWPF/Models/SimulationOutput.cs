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
            //4. (create a delegate object) subscribe to the event
            robot.ObstacleSeenEvent += Robot_ObstacleSeenEvent;
        }

        public event InputEvent inputEvent;

        //2. create a method that matches the signature of the delegate
        public void Robot_ObstacleSeenEvent(object sender, string s)
        {
            //Hier zetten wat die moet doen als de robot iets ziet.
            inputEvent.Invoke(this, s);
        }

        public void Output(string output)
        {
            robot.Command = output;
        }
    }
}
