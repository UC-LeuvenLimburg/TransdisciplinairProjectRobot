using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RondleidingRobot.Models
{
    //Het movemodel is het echte hoofdprogramme, het kijkt welke instructie hij moet doen en laat dan de robot ai de comando's voor de moteren te generenen
    public class MoveModel
    {
        private RobotAI robotAI;
        private System.Timers.Timer programClock;
        private readonly IOutput moveConnection;
        private readonly IOutput speakOutput;
        private readonly UltraSoundLocation utraSoundLocation;

        //bij het aanmaken van het movemodel moeten we meegeven via wat hij welke output moet geven en welke sensoren hij moet gebruiken
        public MoveModel(IOutput movementConnetion, IOutput speakOutput, UltraSoundLocation utraSoundLocation)
        {
            this.utraSoundLocation = utraSoundLocation;
            robotAI = new RobotAI(utraSoundLocation);

            //elke 100ms word er een nieuwe beweging berekent, dit zou sneller kunnen maar aangezien we zowiso al weinig sensor data krijgen heeft dit niet veel zin
            programClock = new System.Timers.Timer(100);
            programClock.Elapsed += ProgramClock_Elapsed;
            

            this.moveConnection = movementConnetion;
            this.speakOutput = speakOutput;

            //we moeten hier even wachten zodat hij zijn uart kan initialiseren
            Thread.Sleep(100);
            moveConnection.inputEvent += MoveConnection_messageReceived;
            robotAI.audioOutputEvent += outputAudio;
            programClock.Start();
        }

        //een functie om audio af te spelen
        private void outputAudio(object sender, string file) 
        {
            speakOutput.Output(file);
        }

        //wanner we moeten bewegen gaan de via de robot AI berkenen wat we moeten doen en dit dan via de uart naar de arduino sturen
        private void ProgramClock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            moveConnection.Output(robotAI.Move());
        }

        //als er een bericht binnen komt gaan we kijken of dit een meting is of een ander bericht
        private void MoveConnection_messageReceived(object sender, string message)
        {
            //indien het een meting is gaan we die meting toevoegen aan de sensoren
            if (message.ToLower().Contains("m"))
            {
                utraSoundLocation.AddMesurement(message);
            }
            //anders gaan we de juist parameters aanduiden om te zorgen dat de robot moet oppassen, stoppen of alles vrij is
            else if (message.ToLower().Contains("c"))
            {
                robotAI.Carefull = true;

            }
            else if (message.ToLower().Contains("s"))
            {
                robotAI.Stopped = true;

            }
            else if (message.ToLower().Contains("o")) 
            {
                robotAI.Stopped = false;
                robotAI.Carefull = false;
            }
        }
    }
}
