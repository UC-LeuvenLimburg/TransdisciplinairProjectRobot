using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RondleidingRobotTest
{
    [TestClass]
    public class UnitTest1
    {

        private const string Expected = "Hello World!";
        [TestMethod]
        public void TestMethod1()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                string[] args = new string[0];
                RondleidingRobot.Program.Main(args);

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        
        }
    }
}
