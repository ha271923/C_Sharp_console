using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_event
    {
        [TestMethod]
        public void TestMethod1()
        {
            // show this msg at Right-Click\Run Tests\
            // Test Detail Summary\Open additional output for this result\Standard Output
            HLog.print("TestMethod1 !!!!!!!!!!!!");
        }
    }
}
