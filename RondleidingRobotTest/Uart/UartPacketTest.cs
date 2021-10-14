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
            char[] args = { 'a', 'b', 'c' };
            char[] expected = { (char)2, (char)4, 'v', 'a', 'b', 'c', (char)157, (char)3 };
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContructTest2()
        {
            char cmd = 'r';
            char[] args = { 'x' };
            char[] expected = { (char)2, (char)2, 'r', 'x', (char)234, (char)3 };
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ContructTest3()
        {
            char cmd = 's';
            char[] args = { };
            char[] expected = { (char)2, (char)1, 's', 's', (char)3 };
            char[] result = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DestructTest1()
        {
            char[] package = { (char)2, (char)1, 's', 's', (char)3 };
            char cmd;
            char[] args;
            Assert.IsTrue(RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out args));
        }

        [TestMethod]
        public void DestructTest2()
        {
            char[] package = { (char)2, (char)4, 'v', 'a', 'b', 'c', (char)157, (char)3 };
            char cmd;
            char[] args;
            Assert.IsTrue(RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out args));
        }

        [TestMethod]
        public void DestructTest3()
        {
            char[] package = { (char)2, (char)3, 'v', 'a', 'c', (char)157, (char)3 };
            char cmd;
            char[] args;
            Assert.IsFalse(RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out args));
        }

        [TestMethod]
        public void DestructTest4()
        {
            char[] package = { (char)2, (char)1, 's', 's', (char)3 };
            char[] expected = { };
            char cmd;
            char[] args;
            RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out args);
            CollectionAssert.AreEqual(expected, args);
        }

        [TestMethod]
        public void DestructTest5()
        {
            char[] package = { (char)2, (char)4, 'v', 'a', 'b', 'c', (char)157, (char)3 };
            char[] expected = { 'a', 'b', 'c' };
            char cmd;
            char[] args;
            RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out args);
            CollectionAssert.AreEqual(expected, args);
        }

        [TestMethod]
        public void ContructEnDestructTest1()
        {
            char cmd = 'v';
            char[] args = { 'a', 'b', 'c' };
            char[] package = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            char[] arguments;
            RondleidingRobot.Uart.UartPacket.Destruct(package, out cmd, out arguments);
            CollectionAssert.AreEqual(args, arguments);
        }

        [TestMethod]
        public void ContructEnDestructTest2()
        {
            char cmd = 'r';
            char[] args = { 'x' };
            char[] package = RondleidingRobot.Uart.UartPacket.Construct(cmd, args);
            char command;
            RondleidingRobot.Uart.UartPacket.Destruct(package, out command, out args);
            Assert.AreEqual(cmd, command);
        }


    }
}
