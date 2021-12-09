using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RondleidingRobot.Models
{
    public delegate void AudioOutputEvent(object sender, string file);
    public class RobotAI
    {
        private const int MAXSPEED = 255;
        private const int SPEEDSTEP = 30;

        public bool Carefull { get; set; }
        public bool Stopped { get; set; }

        private int speed;
        private int distanceToGo;
        private int listIndex;
        private List<Command> commandList;

        public AudioOutputEvent audioOutputEvent;

        public RobotAI() 
        {
            commandList = new List<Command>();
            List<string> commands = FileHelper.ReadMovement();
            foreach (string command in commands)
            {
                commandList.Add(new Command(command));
            }
            listIndex = 0;
            speed = 0;
            distanceToGo = 0;
            Carefull = false;
            Stopped = false;
        }

        public Command Move() 
        {
            if (Stopped) return new Command("A", Convert.ToString(SPEEDSTEP));
            if (distanceToGo < 0)
            {
                listIndex++;
                if (listIndex >= commandList.Count) listIndex = 0;
                try
                {
                    distanceToGo = Convert.ToInt16(commandList[listIndex].Arguments[0]);
                }
                catch 
                {
                    distanceToGo = 0;
                }

            }
            if (CheckIfSpecialOutput(commandList[listIndex]))
            {
                speed = 0;
                distanceToGo--;
                return new Command("X", "0");
            }
            if (speed >= distanceToGo * SPEEDSTEP) speed -= SPEEDSTEP;
            else speed += SPEEDSTEP;
            speed += (SPEEDSTEP - speed % SPEEDSTEP)%SPEEDSTEP;
            if (speed > MAXSPEED) speed = MAXSPEED;
            else if (speed < 0) speed = 0;
            distanceToGo--;
            if (commandList[listIndex].CommandString == "X") speed = 0;
            return new Command(commandList[listIndex].CommandString, Convert.ToString(speed));
        }

        private bool CheckIfSpecialOutput(Command commandListItem) 
        {
            if (commandListItem.CommandString == "SPEAK") 
            {
                audioOutputEvent.Invoke(this, commandListItem.Arguments[0]);
                return true;
            }
            return false;
        }
    }
}
