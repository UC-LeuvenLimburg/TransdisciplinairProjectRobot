using RondleidingRobot.Uart;
using RondleidingRobotWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RondleidingRobot.Models
{
    public class MoveModel
    {
        List<string> movementList = new List<string>();
        private System.Timers.Timer programClock;
        private IOutput moveConnection;

        public MoveModel(IOutput movementConnetion)
        {
            programClock = new System.Timers.Timer(100);
            programClock.Elapsed += ProgramClock_Elapsed;

            this.moveConnection = movementConnetion;

            movementList = RobotAI.CalculateMovements(FileReader.ReadMovement());
            moveConnection.inputEvent += MoveConnection_messageReceived;

            Thread.Sleep(2000);
            programClock.Start();
        }

        private void ProgramClock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (movementList.Count() != 0)
            {
                string currentCommand = movementList.First();
                movementList.Remove(movementList.First());
                moveConnection.Output(currentCommand);
            }
        }

        private void MoveConnection_messageReceived(object sender, string s)
        {
            Console.WriteLine(s);
        }
    }
}
