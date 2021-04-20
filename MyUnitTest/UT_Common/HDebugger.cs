using System;
using System.Collections.Generic;
using System.Text;

namespace MyUnitTest.UT_Common
{
    class HDebugger
    {
        static public void waitForDebugger() {
            Console.WriteLine("Waiting for debugger to attach");
            while (!System.Diagnostics.Debugger.IsAttached)
            {
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine("Debugger attached");
        }
    }
}
