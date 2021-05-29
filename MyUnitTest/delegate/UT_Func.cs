#define NO_LAMBDA

using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    // delegate 己過時，進化成 Action、Func!
    //   Action - 接受沒有回傳值的void方法
    //   Func - 接受有回傳值的方法
    // Problem: 在 Delegate 中，方法的簽章要與 宣告 delegate 的簽章一樣，因次遇到需要傳入多種不同或是型別的參數時，勢必要寫出一堆 delegate。
    // Answer:  因此在 C＃2.0 開始，在.Net Framework 內建了 Action、Func。
    [TestClass]
    public class UT_Func
    {

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1() {
            Func<int, int, int, int, int> funPtr = mymethod; // Func 3. 耦合
            Console.WriteLine(funPtr(10, 100, 1000, 1)); // Func 4. 使用
        }

        public static int mymethod(int s, int d, int f, int g) { // Func 1&2. 宣告&定義(沒有delegate keyword)
            return s * d * f * g;
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2()
        {
            Func<int, int, int, int, int> funPtr = (s,d,f,g) => s * d * f * g;  // Func 1&2&3.
            Console.WriteLine(funPtr(10, 100, 1000, 1)); // Func 4.
        }



        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3() // Func 混用時
        {
            Func<int, int, int> add = Sum; // Func<in int , in int , out int>
            int result = add(12, 18);
            HLog.print("TestMethod6  result =" + result);
        }

        // public delegate TResult Func<in T, out TResult>(T arg); // KEY: Func不是一般函數名字, 且這一行宣告可以拿掉歐
        // Func<int, int, int> sum;
        static int Sum(int x, int y)
        {
            return x + y;
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4()
        {
            //Init一個Person物件
            Person p = new Person()
            {
                Age = 10,
                Name = "Tom"
            };

            #region Func
            // WAY1: 
            // 宣告一個Func<Person,string>委託 funcPointer
            Func<Person, string> funcPointer; // Func<in Person, out string>
            // funcPointer委託指向CheckAge方法
            funcPointer = new Func<Person, string>(CheckAge);
            // 執行funcPointer委託 (執行CheckAge方法)
            string result = funcPointer(p);

            //最後將結果顯示出來
            HLog.print("result =" + result);
            #endregion
            // WAY2: 直接呼叫CheckAge就好了, 幹嘛透過Func<> ??? 
            string result2 = CheckAge(p); // 解耦合, 有時候先定義好了funcPointer, 下面的實作需要被後來的人Impl for this funcPointer
            HLog.print("result2="+result2);
        }

        public static string CheckAge(Person person) // Fuc 2.實作
        {
            string result = "年紀剛剛好";
            if (person.Age >= 60)
                result = "老人";
            return result;
        }

    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }


}
