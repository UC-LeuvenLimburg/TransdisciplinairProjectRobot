using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Uart
{
    public abstract class UartPacket
    {
        public static char[] Construct(char cmd, char[] args) 
        {
            char[] pakket = new char[args.Length + 5];
            pakket[0] = (char)2;
            pakket[1] = (char)(args.Length + 1);
            pakket[2] = cmd;
            int crc = (int)cmd;
            int teller = 3;
            foreach (char c in args) 
            {
                crc += (int)c;
                pakket[teller++] = c;
            }
            pakket[pakket.Length - 2] = (char)(crc%255);
            pakket[pakket.Length - 1] = (char)3;
            return pakket;
        }
    }
}
