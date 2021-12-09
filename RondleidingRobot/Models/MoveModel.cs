using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RondleidingRobot.Models
{
    public class MoveModel
    {
        private RobotAI robotAI;
        private System.Timers.Timer programClock;
        private readonly IOutput moveConnection;
        private readonly IOutput speakOutput;
        private readonly UltraSoundLocation utraSoundLocation;

        public MoveModel(IOutput movementConnetion, IOutput speakOutput)
        {
            robotAI = new RobotAI();

            programClock = new System.Timers.Timer(100);
            programClock.Elapsed += ProgramClock_Elapsed;
            utraSoundLocation = new UltraSoundLocation();

            this.moveConnection = movementConnetion;
            this.speakOutput = speakOutput;


            Thread.Sleep(1000);
            moveConnection.inputEvent += MoveConnection_messageReceived;
            robotAI.audioOutputEvent += outputAudio;
            programClock.Start();
        }

        private void outputAudio(object sender, string file) 
        {
            speakOutput.Output(file);
        }

        private void ProgramClock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //moveConnection.Output(robotAI.Move().ToString());
            moveConnection.Output(utraSoundLocation.HoldLeft(200, 230));
        }

        private void MoveConnection_messageReceived(object sender, string message)
        {
            if (message.ToLower().Contains("m"))
            {
                utraSoundLocation.AddMesurement(message);
            }
            else if (message.ToLower().Contains("c"))
            {
                speakOutput.Output("test.mp3");
                robotAI.Carefull = true;

            }
            else if (message.ToLower().Contains("s"))
            {
                speakOutput.Output("test.mp3");
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
