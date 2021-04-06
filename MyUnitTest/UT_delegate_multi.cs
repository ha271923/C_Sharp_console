using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_delegate_multi
    {
        // Define a custom delegate that has a string parameter and returns void.
        delegate void CustomDel(string s);

        // Define two methods that have the same signature as CustomDel.
        static void Hello(string s)
        {
            HLog.print($"  Hello, {s}!");
        }

        static void Goodbye(string s)
        {
            HLog.print($"  Goodbye, {s}!");
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            // Declare instances of the custom delegate.
            CustomDel hiDel, byeDel, multiDel, multiMinusHiDel, addDel, removeDel;

            // In this example, you can omit the custom delegate if you
            // want to and use Action<string> instead.
            //Action<string> hiDel, byeDel, multiDel, multiMinusHiDel;

            // Create the delegate object hiDel that references the
            // method Hello.
            hiDel = Hello;

            // Create the delegate object byeDel that references the
            // method Goodbye.
            byeDel = Goodbye;

            // The two delegates, hiDel and byeDel, are combined to
            // form multiDel.
            multiDel = hiDel + byeDel;

            // Remove hiDel from the multicast delegate, leaving byeDel,
            // which calls only the method Goodbye.
            multiMinusHiDel = multiDel - hiDel;

            // add more delegates to list
            addDel = hiDel;  // assignment required at fist time
            addDel += byeDel;

            // remove delegate from list
            removeDel = addDel;  // assignment required at fist time
            removeDel -= byeDel;

            HLog.print("Invoking delegate hiDel:");
            hiDel("A");
            HLog.print("Invoking delegate byeDel:");
            byeDel("B");
            HLog.print("Invoking delegate multiDel:");
            multiDel("C"); // KEY: call hi+bye at once.
            HLog.print("Invoking delegate multiMinusHiDel:");
            multiMinusHiDel("D");
            HLog.print("Invoking delegate addDel:  +=");
            addDel("E");
            HLog.print("Invoking delegate removeDel:  -=");
            removeDel("F");
        }
    }
}
