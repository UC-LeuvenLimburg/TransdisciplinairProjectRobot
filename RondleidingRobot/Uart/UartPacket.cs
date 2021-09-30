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

        public static bool Destruct(char[] package, out char cmd, out char[] args) 
        {
            int teller = 0;
            args = new char[package.Length-5];
            if (package[teller++] == (char)2) 
            {
                int crc = 0;
                int len = (int)package[teller++];
                cmd = package[teller++];
                crc += (int)cmd;
                for (int i = 0; i < len - 1; i++) 
                {
                    crc += (int)package[teller];
                    args[i] = package[teller++];
                }
                if (package[teller++] == (char)(crc % 255)) 
                {
                    if (package[teller] == (char)3) 
                    {
                        return true;
                    }
                }
            }
            cmd = '0';
            return false;
        }
    }
}
