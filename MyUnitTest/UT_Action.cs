using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
       Action - 接受沒有回傳值的void方法
       Func - 接受有回傳值的方法

       發現Func<> ，Action<> 其實本質就是委託 ，雖然有十幾個重載 但大同小異
        public delegate TResult Func<out TResult>();   // with in, out
        public delegate    void Action<in T>(T obj);   // with in

        總結一次 Generic Delegate, Action, Func 三者的差異。
        1. Generic Delegate
           需定義自己的 Delegate，可傳入多個參數，可以有回傳值。
        2. Generic Action
           不需定義自己的 Delegate，可傳入多個參數，沒有回傳值。
        3. Generic Func
           不需定義自己的 Delegate，可傳入多個參數，可以有回傳值。
    */

    [TestClass]
    public class UT_Action
    {
        private delegate string MyDelegate<T1, T2>(T1 p1, T2 p2);
        [TestMethod] // -----------------------------------------------------------------------
        public void TestDelegate() // 需定義自己的 Delegate，可傳入多個參數，可以有回傳值。
        {
            MyDelegate<int, int> intToString = new MyDelegate<int, int>(MyConvert); // #1
            string s = intToString(10, 20);  // #3
        }

        private static string MyConvert(int v1, int v2)  // #2
        {
            return (v1 + v2).ToString();
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestFunc()  // 不需定義自己的 Delegate，可傳入多個參數，可以有回傳值。
        {
            Func<int, int, string> intToString = new Func<int, int, string>(MyConvert);  // #1
            string s = intToString(10, 20);  // #2
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestAction() // 不需定義自己的 Delegate，可傳入多個參數，沒有回傳值。
        {
            Action<int, int      > intToStringShow = new Action<int, int      >(MyShow);  // #1
            intToStringShow(10, 20);  // #2
        }

        private static void MyShow(int v1, int v2)
        {
            HLog.print((v1 + v2).ToString());
        }
    }
}