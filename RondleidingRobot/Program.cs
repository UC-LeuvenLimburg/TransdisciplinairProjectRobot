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
            MoveModel model = new MoveModel(args);
        }

    }
}
