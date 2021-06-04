
using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest.basic
{
    class UT_get_set
    {
        public int a;
        public int b { get; }  // read-only
        public int c { get; set; }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            a = 1;
            // b = 2;   
            c = 3;

        }
    }
}
