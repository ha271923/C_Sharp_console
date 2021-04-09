using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_var
    {
        // 請務必了解 var 關鍵字不代表 "variant"，也不代表變數是不嚴格規定類型或晚期繫結的。 只代表編譯器會判斷並指派最適當的類型。

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            var i1 = 10; // Implicitly typed.
            int i2 = 10; // Explicitly typed.
            HLog.print("i1={0}, i2={1}", i1, i2);

            // s is compiled as a string
            var s = "Hello";

            // a is compiled as int[]
            var a = new[] { 0, 1, 2 };

            // anon is compiled as an anonymous type
            var anon = new { Name = "Terry", Age = 34 };

            // list is compiled as List<int>
            var list = new List<int>();

            int[] nums = { 0, 1, 2, 3 };
            foreach (var n in nums)
            {
                HLog.print($"n={n}");
            }
        }
    }
}
