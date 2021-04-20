using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_anoymous_Function
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            // show this msg at Right-Click\Run Tests\
            // Test Detail Summary\Open additional output for this result\Standard Output
            HLog.print("TestMethod1 !!!!!!!!!!!!");
        }

        delegate void MyDelegateFunc(string s); // delegate declaration
        static void functionImpl(string s)
        {
            HLog.print(s);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2()
        {
            // Original delegate syntax required
            // initialization with a named method.
            MyDelegateFunc testDelA = new MyDelegateFunc(functionImpl); // need functio & type name, implement at top. no need functionImpl()

            // C# 2.0: A delegate can be initialized with
            // inline code, called an "anonymous method." This
            // method takes a string as an input parameter.
            MyDelegateFunc testDelB = delegate (string s) { HLog.print(s); }; // no funciton name, direct implement at here!

            // C# 3.0. A delegate can be initialized with
            // a lambda expression. The lambda also takes a string
            // as an input parameter (x). The type of x is inferred by the compiler.
            MyDelegateFunc testDelC = (x) => { HLog.print(x); }; // no function & type name, direct implement at here!

            // Invoke the delegates.
            testDelA("Hello. My name is M and I write lines.");
            testDelB("That's nothing. I'm anonymous and ");
            testDelC("I'm a famous author.");
        }

        // public delegate TResult Func<in T, out TResult>(T arg);
        // public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        delegate int FunctionToCall();
        static int IntValue = 5;
        public static int Add2()
        {
            IntValue += 2;
            return IntValue;
        }
        public static int Add3()
        {
            IntValue += 3;
            return IntValue;
        }
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3() {
            FunctionToCall functionDelegate = Add2; // 5+2
            functionDelegate += Add3; // 7+3
            functionDelegate += Add2; // 10+2

            HLog.print("Value: {0}", functionDelegate); // result= 12
        }
    }
}
