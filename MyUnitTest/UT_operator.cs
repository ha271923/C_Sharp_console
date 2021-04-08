using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_operator
    {
        /*
            https://medium.com/@WilliamWhetstone/c-operator-%E7%89%B9%E6%AE%8A%E7%9A%84%E9%81%8B%E7%AE%97%E5%AD%90-%E5%92%8C-85f87da6140b
         */

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            // ?: operator
            // var value = expression1 : expression2;
            // example:
            int test = 10;
            int a = test > 0 ? 5 : -1;
            // if(test>0) a=5; else a=-1;
            HLog.print("a="+a);

            // value ?? expression
            // example:
            int? NullableValue = null;
            int b = NullableValue ?? -1;
            // if(NullableValue==null) b=-1; else b=NullableValue.Value;
            HLog.print("b=" + b);

            // value ?.expression
            // example:
            int[] NullableArrayValue = null;
            int? c = NullableArrayValue?.Length;
            // if(NullableValue==null) c=null; else c=NullableValue.Value;
            HLog.print("c=" + c);

        }
    }
}
