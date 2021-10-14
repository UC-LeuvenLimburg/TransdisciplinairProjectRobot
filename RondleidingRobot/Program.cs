using RondleidingRobot.Uart;
using System;
using System.Threading;

namespace RondleidingRobot
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            Console.WriteLine("Hello World!");
            IUartConnection uartConnection = new UartConnectionWindowsIOT(args[0]);
            uartConnection.messageReceived += UartConnection_messageReceived;
            Thread.Sleep(10000);
            uartConnection.Send("W");
            Console.WriteLine("sended test");
            Thread.Sleep(10000);
            uartConnection.Send("A");
            Console.WriteLine("sended test");
            Thread.Sleep(10000);
            uartConnection.Send("S");
            Console.WriteLine("sended test");
            Thread.Sleep(10000);
            uartConnection.Send("D");
            Console.WriteLine("sended test");
        }

        private static void UartConnection_messageReceived(object sender, string s)
        {
            Console.WriteLine(s);
        }
    }
}
