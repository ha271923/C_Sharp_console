using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
      Lambda Expression 經常用在委派 ( delegate ) , 原本常見寫一個方法像這樣：
        Bool func( int a,int b)
        {
           return a==b;
        }
      而Lambda的概念是，把上面 改成 輸入 => 黑箱=>輸出
      也就是：
        func= (a,b)=>(a==b)
        (參數)=>{運算式}
      把左邊的(參數) 丟到 右邊的{運算式}去做運算

    這邊有二點重點：
        1. func是什麼並不重要，也就是"匿名方法"的概念
        2. 把原本需要四行寫成的A方法，精簡改成一行：
        輸入【int a,int b 】 => 輸出【int a,int b 】

    以運算式作為主體的運算式 Lambda：
        (input-parameters) => expression
    以陳述式區塊作為主體的陳述式 Lambda：
        (input-parameters) => { <sequence-of-statements> }
    */
    [TestClass]
    public class UT_lambda
    {
        public delegate void func(int x);

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            HLog.print("TestMethod1 !!!!!!!!!!!!");
            // 精簡
            func f = delegate (int input)
            {
                HLog.print("lambda1 =" + input);
            };
            f(1);
            // 精簡+
            f = (int input) =>
            { // 在C# 3.0可以用Lambda「=>」符號來更簡單的表示匿名方法： 省略deleget後在()後面加上 => 就是λ表示的匿名方法，這寫法又稱為λ陳述式
                HLog.print("lambda2 =" + input);
            };
            f(2);
            // 精簡++
            f = (input) =>
            {
                HLog.print("lambda3 =" + input);
            };
            f(3);
            // 精簡+++
            // 傳如值只有一個的話，括號還能再省略
            f = input =>
            {
                HLog.print("lambda4 =" + input);
            };
            f(4);
            // 精簡++++
            // 如果連同{ } 與; 都省略掉並濃縮成一句話便成為λ運算式
            f = input => HLog.print("lambda5 =" + input);
            f(5);
            // 如果不想輸入參數時,使用_
            f = _ => HLog.print("lambda6 No param" );
            f(6);

            
        }

        public delegate int funcRet(int x);

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2()
        {
            HLog.print("TestMethod2 !!!!!!!!!!!!");
            // 精簡
            funcRet f = delegate (int input)
            {
                HLog.print("lambda1 =" + input);
                return (input * 100);
            };
            HLog.print("lambda1 out=" + f(1));
            // 精簡+
            f = (int input) =>
            { // 在C# 3.0可以用Lambda「=>」符號來更簡單的表示匿名方法： 省略deleget後在()後面加上 => 就是λ表示的匿名方法，這寫法又稱為λ陳述式
                HLog.print("lambda2 =" + input);
                return input * 100;
            };
            HLog.print("lambda2 out=" + f(2));
            // 精簡++
            f = (input) =>
            {
                HLog.print("lambda3 =" + input);
                return input * 100;
            };
            HLog.print("lambda3 out=" + f(3));
            // 精簡+++
            // 傳如值只有一個的話，括號還能再省略
            f = input =>
            {
                HLog.print("lambda4 =" + input);
                return input * 100;
            };
            HLog.print("lambda4 out=" + f(4));
            // 精簡++++
            // 如果連同{ } 與; 都省略掉並濃縮成一句話便成為λ運算式
            // f = input => HLog.print("lambda5 =" + input); // one-line can't return
            // f(5);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3()
        {
            GenericDelegate<int, int> myDelegate = new GenericDelegate<int, int>(Cal); // #3 實例
            string res1 = myDelegate(3, 5); // #4 使用
            HLog.print(res1);
            // Func+lambda 只要一行 #1,2,3
            Func<int, int, string> func = (a, b) => (a + b).ToString();
            string res2 = func(4, 6); // #4 使用
            HLog.print(res2);
        }
        // 標準寫法
        private delegate string GenericDelegate<T1, T2>(T1 a, T2 b); // #1 宣告
        private static string Cal(int a, int b) // #2 實作
        {
            return (a + b).ToString();
        }

    }
}