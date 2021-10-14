using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RJCP.IO.Ports;

namespace RondleidingRobot.Uart
{
    class UartConnectionPortStream : IUartConnection
    {
        private SerialPortStream myPort;



        public UartConnectionPortStream(string port)
        {
            Thread readThread = new Thread(Read);
            myPort = new SerialPortStream(port, 9600, 8, Parity.None, StopBits.One);
            myPort.Handshake = Handshake.None;
            myPort.ReadTimeout = 10000;
            myPort.NewLine = "\r\n";
            myPort.Open();
            readThread.Start();
        }

        private void Read(object obj)
        {
            while (true)
            {
                try
                {
                    string message = myPort.ReadLine();
                    Console.WriteLine(message);
                    messageReceived.Invoke(this, message);
                }
                catch (TimeoutException) { }
            }
        }

        public event MessageReceived messageReceived;

        public void Send(string message)
        {
            myPort.WriteLine(message);
        }
    }
}
