using RondleidingRobot.Uart;
using RondleidingRobotWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace RondleidingRobot.Models
{
    public class MoveModel
    {
        List<string> movementList = new List<string>();
        private string currentCommand = "";
        private System.Timers.Timer moveTimer;
        private System.Timers.Timer updateTimer;
        private IOutput moveConnection;

        public MoveModel(string[] args)
        {
            moveTimer = new System.Timers.Timer(200);
            moveTimer.Elapsed += MoveTimer_Elapsed;

            updateTimer = new System.Timers.Timer(1000);
            updateTimer.Elapsed += UpdateTimer_Elapsed;

            movementList = FileReader.ReadMovement();
            moveConnection = new UartOutput(args[0]);
            moveConnection.inputEvent += MoveConnection_messageReceived;

            Thread.Sleep(2000);
            updateTimer.Start();
            moveTimer.Start();
        }

        private void MoveConnection_messageReceived(object sender, string s)
        {
            Console.WriteLine(s);
        }

        

        private void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (movementList.Count() != 0)
            {
                currentCommand = movementList.First();
                movementList.Remove(movementList.First());
            }
        }

        private void MoveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string command = RobotAI.Move(currentCommand);
            moveConnection.Output(command);
        }
    }
}
