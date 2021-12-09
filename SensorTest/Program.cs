using RondleidingRobot.Models;
using System;

namespace SensorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UltraSoundLocation sensors = new UltraSoundLocation();
            while (true) 
            {
                string x = Console.ReadLine();
                sensors.AddMesurement(x);
                Console.WriteLine(sensors.HoldLeft(100,230));
            }
        }
    }
}
