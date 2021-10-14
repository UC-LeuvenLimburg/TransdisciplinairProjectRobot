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
        List<String> movementList = new List<String>();
        private string currentCommand = "";
        private System.Timers.Timer moveTimer;
        private System.Timers.Timer updateTimer;
        private IUartConnection moveConnection;

        public MoveModel(string[] args)
        {
            moveTimer = new System.Timers.Timer(200);
            moveTimer.Elapsed += MoveTimer_Elapsed;

            updateTimer = new System.Timers.Timer(1000);
            updateTimer.Elapsed += UpdateTimer_Elapsed;

            readMovement();
            moveConnection = new UartConnectionPortStream(args[0]);
            moveConnection.messageReceived += MoveConnection_messageReceived;

            Thread.Sleep(2000);
            updateTimer.Start();
            moveTimer.Start();
        }

        private void MoveConnection_messageReceived(object sender, string s)
        {
            Console.WriteLine(s);
        }

        private List<string> readMovement()
        {
            string beweging;

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(folderPath, "bewegingen robot.txt");

            StreamReader reader = File.OpenText(filePath);

            string row;
            string[] columns;
            char[] separators = { ':' };

            row = reader.ReadLine();
            while (row != null)
            {
                columns = row.Split(separators);
                beweging = Convert.ToString(columns[0]);
                movementList.Add(beweging);
                row = reader.ReadLine();
            }
            reader.Close();
            return movementList;
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
            moveConnection.Send(command);
        }
    }
}
