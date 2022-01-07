using RondleidingRobot.Uart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RondleidingRobot.Models
{
    //E3 uq45 output is een IOutput die de in en output van een uart connectie regelt
    class UartOutput : IOutput
    {
        private IUartConnection uartConnection;

        //bij het maken van een uartoutput maken we eerst een uart connectie aan en wanner er een bericht op de uart binnen komt roepen we een inputevent op
        public UartOutput(string port) 
        {
            uartConnection = new UartConnectionPortStream(port);
            uartConnection.messageReceived += UartConnection_messageReceived;
        }

        private void UartConnection_messageReceived(object sender, string s)
        {
            if(inputEvent != null) inputEvent.Invoke(this, s);
        }

        public event InputEvent inputEvent;

        public void Output(string output)
        {
            uartConnection.Send(output);
            Console.WriteLine(output);
        }
    }
}
