using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Uart
{
    public delegate void MessageReceived(object sender, string s);
    public interface IUartConnection
    {
        public void Send(string message);
        public event MessageReceived messageReceived;
    }
}
