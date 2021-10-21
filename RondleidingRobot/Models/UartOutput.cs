using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    class UartOutput : IOutput
    {
        private IUartConnection uartConnection;

        public UartOutput(string port) 
        {
            uartConnection = new UartConnectionPortStream(port);
            uartConnection.messageReceived += UartConnection_messageReceived;
        }

        private void UartConnection_messageReceived(object sender, string s)
        {
            inputEvent.Invoke(this, s);
        }

        public event InputEvent inputEvent;

        public void Output(string output)
        {
            uartConnection.Send(output);
        }
    }
}
