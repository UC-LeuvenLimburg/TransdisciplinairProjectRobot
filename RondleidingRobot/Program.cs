using RondleidingRobot.Models;
using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RondleidingRobot
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //IOutput movementConnetion = new UartOutput("/dev/ttyACM0");
            IOutput printOutput = new PrintOutput();
            //IOutput soundOutput = new SoundOutput();

            MoveModel model = new MoveModel(printOutput, printOutput);
            while (true)
            {

            }
        }
        
    }
}
