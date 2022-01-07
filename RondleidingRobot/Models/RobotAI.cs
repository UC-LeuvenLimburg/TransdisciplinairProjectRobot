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
        private const int MAXSPEED = 150;
        private const int SPEEDSTEP = 20;

        public bool Carefull { get; set; }
        public bool Stopped { get; set; }

        private int speed;
        private double angle;
        private int distanceToGo;
        private int listIndex;
        private List<Command> commandList;
        private UltraSoundLocation ultraSoundLocation;
        public AudioOutputEvent audioOutputEvent;

        //het aanmaken van de robot ai classe en het inlezen van de lijst met functies die de robot moet uitvoeren
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
            distanceToGo = commandList[listIndex].DistanceTo;
            angle = ultraSoundLocation.Angle;
            Carefull = false;
            Stopped = false;
            CheckIfSpecialOutput(commandList[listIndex]);
        }

        //de functie die het commando waar na bewogen moet worden geeft aan de hand van de lijst met commando's
        public string Move() 
        {
            //naar het volgende commando in de lijst gaan
            if (Stopped) return "A:50";
            //bewegen volgens het commando
            //kijken of we tot webewegen tot een bepaalde afstad, draaien tot een bepaalde hoek, of bewegen voor een bepaalde tijd
            if (commandList[listIndex].GoingTo)
            {
                if ((commandList[listIndex].CommandString == "A" || commandList[listIndex].CommandString == "D") && !commandList[listIndex].Holding)
                {
                    //berekenen welke hoe er nog gedraaid moet worden
                    distanceToGo = Convert.ToInt32((commandList[listIndex].DistanceTo - Math.Abs(ultraSoundLocation.Angle - angle)) / 5);
                    //verminderen van de maximum draaisnelheid voor een precieze draai
                    if (speed > 80 - SPEEDSTEP) speed = 80 - SPEEDSTEP;
                }
                //indien er geen hoek gedraaid word berekenen welke afstand er nog gereden moet worden
                else distanceToGo = Convert.ToInt32((ultraSoundLocation.StraightDistance - commandList[listIndex].DistanceTo) / 10);
            }
            //indien we niet tot een afstand rijden maar op tijd gaan we de distance to go verlagen
            else distanceToGo--;
            Console.WriteLine("distance to go: " + distanceToGo);
            if (distanceToGo <= 0)
            {
                listIndex++;
                if (listIndex >= commandList.Count) listIndex = 0;
                distanceToGo = commandList[listIndex].DistanceTo;
                angle = ultraSoundLocation.Angle;
                CheckIfSpecialOutput(commandList[listIndex]);
                return "X:0";
            }
            //We gaan kijken of we iets moeten zeggen, indien ja gaan we dat doen
            
            //nu gaan we de snelheid berkenen
            if (speed >= distanceToGo * SPEEDSTEP) speed -= SPEEDSTEP;
            else speed += SPEEDSTEP;
            speed += (SPEEDSTEP - speed % SPEEDSTEP) % SPEEDSTEP;
            if (speed > MAXSPEED) speed = MAXSPEED;
            else if (speed < 0) speed = 0;

            //nu gaan we kijken welk comando we moeten sturen
            //als het commando stop is kunnen we gewoon stop sturen
            if (commandList[listIndex].CommandString == "X") return "X:0";
            //indien we geen speciefieke lijn aanhouden kunnen we gewoon het commando sturen
            if (commandList[listIndex].Holding == false) return commandList[listIndex].CommandString + ":" + Convert.ToString(speed); 
            //indien we wel een lijn aanhouden gaan we kijken welke lijn en dan die lijn zijn speciefieke afstand aanhouden of kijken dat we recht blijven
            if (commandList[listIndex].CommandString == "A") return ultraSoundLocation.HoldLeft(commandList[listIndex].SideDistance, speed);
            if (commandList[listIndex].CommandString == "D") return ultraSoundLocation.HoldRight(commandList[listIndex].SideDistance, speed);
            if (commandList[listIndex].CommandString == "W") return ultraSoundLocation.HoldStraight(speed, angle);
            //indien we nu nog niets hebben teruggestuurd betekent dat we een incorrect comando hebben dus gaan we voor de zekerheid gewoon stoppen
            return "X:0";
        }

        //een functie die kijkt of we iets moeten zeggen en indein we dit moeten het audioOutput event oproept en anders een false terug geeft
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
