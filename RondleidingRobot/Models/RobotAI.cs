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
        private const int MAXSPEED = 100;
        private const int SPEEDSTEP = 10;

        public bool Carefull { get; set; }
        public bool Stopped { get; set; }

        private int speed;
        private int distanceToGo;
        private int listIndex;
        private List<Command> commandList;
        private UltraSoundLocation ultraSoundLocation;
        public AudioOutputEvent audioOutputEvent;

        public RobotAI(UltraSoundLocation ultraSoundLocation) 
        {
            this.ultraSoundLocation = ultraSoundLocation;
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

        public string Move() 
        {
            if (Stopped) return ("A:" + Convert.ToString(SPEEDSTEP));
            if (distanceToGo < 0)
            {
                listIndex++;
                if (listIndex >= commandList.Count) listIndex = 0;
                distanceToGo = commandList[listIndex].DistanceTo;
            }
            if (CheckIfSpecialOutput(commandList[listIndex]))
            {
                speed = 0;
                distanceToGo--;
                return "X:0";
            }
            if (speed >= distanceToGo * SPEEDSTEP) speed -= SPEEDSTEP;
            else speed += SPEEDSTEP;
            speed += (SPEEDSTEP - speed % SPEEDSTEP) % SPEEDSTEP;
            if (speed > MAXSPEED) speed = MAXSPEED;
            else if (speed < 0) speed = 0;
            distanceToGo--;
            if (commandList[listIndex].CommandString == "X") speed = 0;
            if (commandList[listIndex].Holding == false && commandList[listIndex].GoingTo == false) return commandList[listIndex].CommandString + ":" + Convert.ToString(speed); 
            if (commandList[listIndex].GoingTo) distanceToGo = ultraSoundLocation.StraightDistance - commandList[listIndex].DistanceTo;
            if (commandList[listIndex].CommandString == "A") return ultraSoundLocation.HoldLeft(commandList[listIndex].SideDistance, speed);
            if (commandList[listIndex].CommandString == "D") return ultraSoundLocation.HoldRight(commandList[listIndex].SideDistance, speed);
            if (commandList[listIndex].CommandString == "W") return ultraSoundLocation.HoldStraight(speed);
            return "X:0";
        }

        private bool CheckIfSpecialOutput(Command commandListItem) 
        {
            if (commandListItem.CommandString == "SPEAK") 
            {
                audioOutputEvent.Invoke(this, commandListItem.SoundFile);
                return true;
            }
            return false;
        }
    }
}
