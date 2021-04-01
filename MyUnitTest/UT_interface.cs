using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_interface
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestInterface()
        {
            IOperation op = new interface_Add(); // interface 3. 實體化介面
            HLog.print(op.GetResult(1, 2)); // interface 4. 使用介面函數
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestDelegate()
        {
            Operation op = delegate_Add; // delegate 3. 指定委派耦合
            HLog.print("int ret=" + op(1, 2));        // delegate 4. 使用委派函數
            // Tips: delegate的缺點, 因為type不同, 需要重新指定耦合, 而Func改善這個缺點了
            OperationD opD = delegate_Add; // delegate 3. 指定委派耦合
            double ret = opD(1.0, 2.0);    // delegate 4. 使用委派函數
            HLog.print("double ret=" + ret);
        }
        public static int delegate_Add(int a, int b) // delegate 2. 實作(在別的地方實作)
        {
            return a + b;
        }

        public static double delegate_Add(double a, double b) // delegate 2. 實作(在別的地方實作)
        {
            return a + b;
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestFunc()
        {
            // Func<int,int,int> is a delegate which accepts two int parameters and returns int as a result
            Func<int, int, int> op; // Func 1.宣告
            Func<double, double, double> opD; // Func 1.宣告
            op = Func_Add;          // Func 3.指定
            HLog.print(op(1, 2));   // Func 4. 使用Func函數
            opD = Func_Add;             // Func 3.指定
            double ret = opD(1.0, 2.0); // Func 4. 使用Func函數
            HLog.print("double ret="+ret);   
            /*
            // lambda
            Func<int, int, int> op = (a, b) => a + b; // Func 1.宣告+2.實作+3.指定
            HLog.print(op(1, 2)); // Func 4. 使用Func函數
            */
        }
        public static T Func_Add<T>(T a, T b) // Func 2. 實作(在別的地方實作)
        {
            // C# 4.0 new keyword 'dynamic' for add condition
            dynamic temp_a = a;
            dynamic temp_b = b;
            return temp_a + temp_b;
        }
    }

    // interface -----------------------------------------------------
    public interface IOperation // interface 1. 宣告
    {
        int GetResult(int a, int b);
    }

    public class interface_Add : IOperation // interface 2. 實作(在別的地方實作)
    {
        public int GetResult(int a, int b)
        {
            return a + b;
        }
    }
    // delegate -----------------------------------------------------
    // it's a bit simpler than the interface definition.
    public delegate    int Operation (int a, int b);       // delegate 1. 宣告
    public delegate double OperationD(double a, double b); // delegate 1. 宣告

    // Func -----------------------------------------------------
    public delegate T FuncOperation<T>(T a, T b); // delegate 1. 宣告

}
