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
        //Het hoofdprogramma word gebruikt om de onderdelen aan te maken en dan het MoveModel aan the maken
        //Het moveModel is eigenlijk het volledige brein achter de robot.
        public static void Main(string[] args)
        {
            UltraSoundLocation ultraSoundLocation = new UltraSoundLocation();
            IOutput movementConnetion = new UartOutput("/dev/ttyACM0");
            //Voor te testen is het soms gemakkelijk om iets te printen in de plaats van altijd een arduino en een box aan te sluiten dus daarvoor gebruiken we de PrintOutput.
            //IOutput printOutput = new PrintOutput();
            IOutput soundOutput = new SoundOutput();

            MoveModel model = new MoveModel(movementConnetion, soundOutput, ultraSoundLocation);
            while (true)
            {

            }
        }
        
    }
}
