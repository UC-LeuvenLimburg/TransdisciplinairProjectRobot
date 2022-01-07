using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RondleidingRobot.Models
{
    //Een command is een beweging of een opdracht van iets wat de robot moet zeggen.
    public class Command
    {
        public string CommandString { get; set; }
        public bool Holding { get; set; }
        public bool GoingTo { get; set; }
        public int SideDistance { get; set; }
        public int DistanceTo { get; set; }
        public string SoundFile { get; set; }
        public Command()
        {
            CommandString = "";
        }

        //als we een Command willen aanmaken vanuit het textbestand moeten we het commando eerst decoderen zodat het programma weet wat hij precies moet doen.
        //we kijken of we een muur moeten aanhouden, of we moeten gaan tot een bepaalde hoek en of we moeten rijden tot aan een maar.
        public Command(string command) 
        {
            command = command.ToLower();
            Holding = false;
            GoingTo = false;
            SideDistance = 0;
            DistanceTo = 0;
            if (command.IndexOf("houd") > -1) 
            {
                Holding = true;
                command = command.Replace("houd ", "");
            }   
            if (command.IndexOf("tot") > -1) GoingTo = true;
            string[] collums = command.Split(':');
            if (Holding) 
            {
                string[] x = collums[0].Split(' ');
                collums[0] = x[0];
                if(x[0] != "vooruit") SideDistance = Convert.ToInt32(x[1]);
            }
            switch (collums[0].ToLower())
            {
                case "links":
                    CommandString = "A";
                    break;
                case "rechts":
                    CommandString = "D";
                    break;
                case "vooruit":
                    CommandString = "W";
                    break;
                case "achteruit":
                    CommandString = "S";
                    break;
                case "speak":
                    CommandString = "SPEAK";
                    SoundFile = collums[1];
                    break;
                default:
                    CommandString = "X";
                    break;
            }
            if(collums.Length > 1 && CommandString != "SPEAK") DistanceTo = Convert.ToInt32(collums[1].Replace("tot ", ""));
        }
    }
}
