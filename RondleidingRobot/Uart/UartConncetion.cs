using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace RondleidingRobot.Uart
{
    class UartConncetion : IUartConnection
    {
        static SerialPort serialPort;


        public UartConncetion(string port) 
        {
            Thread readThread = new Thread(Read);
            serialPort = new SerialPort();
            serialPort.PortName = port;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.Encoding = Encoding.ASCII;

            serialPort.Open();
            readThread.Start();
        }

        private void Read()
        {
            while (true)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    Console.WriteLine(message);
                    messageReceived.Invoke(this, message);
                }
                catch (TimeoutException) { }
            }
        }

        public event MessageReceived messageReceived;

        public void Send(string message)
        {
            try
            {
                serialPort.Write(message);
            }
            catch (Exception) 
            { 
            
            }
        }
    }
}
