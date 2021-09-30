using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobotTest.Uart
{
    [TestClass]
    public class UartPacketTest
    {
        [TestMethod]
        public void ContructTest1()
        {
            char cmd = 'v';
            char[] args = { 'a', 'b', 'c'};
            char[] expected = { (char)2, (char)4, 'v','a','b','c', (char)157, (char)3};
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd,args);
            CollectionAssert.AreEqual(expected,result);
        }

        [TestMethod]
        public void ContructTest2()
        {
            char cmd = 'r';
            char[] args = { 'x'};
            char[] expected = { (char)2, (char)2, 'r', 'x', (char)234, (char)3 };
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContructTest3()
        {
            char cmd = 's';
            char[] args = {};
            char[] expected = { (char)2, (char)1, 's', 's', (char)3 };
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
