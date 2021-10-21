using RondleidingRobot.Models;
using RondleidingRobot.Uart;
using RondleidingRobotWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace RondleidingRobot
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //IOutput movementConnetion = new UartOutput(args[0]);
            IOutput movementConnetion = new PrintOutput();
            MoveModel model = new MoveModel(movementConnetion);
            while (true)
            { }
        }
        
    }
}
