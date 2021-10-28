using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RondleidingRobot.Models
{
    public class MoveModel
    {
        List<Command> CommandList = new List<Command>();
        private System.Timers.Timer programClock;
        private readonly IOutput moveConnection;
        private readonly IOutput speakOutput;

        public MoveModel(IOutput movementConnetion, IOutput speakOutput)
        {
            programClock = new System.Timers.Timer(100);
            programClock.Elapsed += ProgramClock_Elapsed;

            this.moveConnection = movementConnetion;
            this.speakOutput = speakOutput;

            CommandList = RobotAI.CalculateMovements(FileHelper.ReadMovement());
            moveConnection.inputEvent += MoveConnection_messageReceived;

            Thread.Sleep(2000);
            programClock.Start();
        }

        private void ProgramClock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (CommandList.Count() != 0)
            {
                Command currentCommand = CommandList.First();
                CommandList.Remove(CommandList.First());
                if (currentCommand.CommandString == "speak")
                {
                    speakOutput.Output(currentCommand.Arguments[0]);
                }
                else
                {
                    moveConnection.Output(currentCommand.ToString());
                }
            }
        }

        private void MoveConnection_messageReceived(object sender, string s)
        {
            Console.WriteLine(s);
        }
    }
}
