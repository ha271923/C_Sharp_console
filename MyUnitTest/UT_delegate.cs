#define NO_LAMBDA

using System;
using System.Drawing;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
     * 適時的善用Delegate能讓程式的耦合性降低，提升靈活性。
     * delegate, Func<>, Action<>, Predicate<>
     * Func<>與Action<>本質都是委派(delegate)，只是又多包了一層。
     *  public delegate TResult Func<out TResult>();            Func就是有一個回傳值的委派
     *  public delegate void    Action<in T>     (T obj);       Action是沒有回傳值的委派
     * 大膽的告訴你，你完全可以把delegate理 解成C中的函式指標，
     * 它允許你傳遞一個類A的方法m給另一個類B的物件，使得類B的物件能夠呼叫這個方法m，說白了就是可以把方法當作引數傳遞。
     * 不過 delegate和函式指標還是有點區別的，delegate有許多函式指標不具備的優點。
     * 1. 函式指標只能指向靜態函式，而delegate既可以引 用靜態函式，又可以引用非靜態成員函式。
     *    在引用非靜態成員函式時，delegate不但儲存了對此函式入口指標的引用，而且還儲存了呼叫此函式的類例項的引用。
     * 2. 與函式指標相 比，delegate是面向物件、型別安全、可靠的受控（managed）物件。也就是說，runtime能夠保證delegate指向
     *    一個有效的方法， 你無須擔心delegate會指向無效地址(null)或者越界地址。
     */

    [TestClass]
    public class UT_delegate
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            int num = 33;
            PrintNumber print;
            if (num % 2 == 1)
                print = PrintOddNumber;
            else
                print = PrintEvenNumber;

            print(num);
            HLog.print(num);
        }

        public delegate void PrintNumber(int num);
        public static void PrintOddNumber(int num)
        {
            HLog.print("input odd number is :" + num);
        }
        public static void PrintEvenNumber(int num)
        {
            HLog.print("input even number is :" + num);
        }

        // 1. 委派可以將方法當成參數來進行傳遞
        // 委派宣告：
        // [public|private|protected] delegate[void | 回傳資料型態] 委派名稱([參數1, 參數2,…]);
        public delegate int Timestwo(int x); // delegate 1.宣告
        public delegate int Multiply(int x, int y);

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2() 
        {
            Timestwo mul_two = delegate (int x) { return 2 * x; };  // delegate 2.實作(注意函式名稱在原本type的位置) 3. 指定委派
            Multiply mul     = delegate (int x, int y) { return x * y; };

            HLog.print("TestMethod2 mul_two = " + mul_two(5).ToString()); // delegate 4. 使用
            HLog.print("TestMethod2 mul = " + mul(5, 6).ToString());
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4() // 當使用new產生delegate函數時
        {
            // 建立delegate物件   
            CompareDelegate cd = new CompareDelegate(Compare); // 2. 實作 & 3. 委派用 new Object 物件式寫法
            // 呼叫delegate   
            HLog.print("TestMethod4 = " + cd(1, 2));
        }

        // 宣告delegate物件   
        public delegate int CompareDelegate(int a, int b); // 1. 類別宣告
        // 欲傳遞的方法，它與CompareDelegate具有相同的引數和返回值型別   
        public static int Compare(int a, int b) {
            if (a > b) return 1;
            else if (a == b) return 0;
            else return -1;
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod5() // 當輸入參數貫穿時(MyTestDelegate(int i) <--> DelegateFunction(int i)
        {
            //建立delegate  
            //                      new delegateObj(method)
            ReceiveDelegateArgsFunc(new MyTestDelegate(DelegateFunction)); // 3. KEY: 奇妙之處, MyTestDelegate的arg明明是int i, 在此卻是函式 DelegateFunction
        }
        public delegate void MyTestDelegate(int i); // delegate 1. 類別宣告
        //欲傳遞的方法  
        public static void DelegateFunction(int i) // 2. 實作
        {
            HLog.print("傳過來的引數為: {0}.", i);
        }
        //這個方法接收一個delegate型別的引數，也就是接收一個函式作為引數  
        public static void ReceiveDelegateArgsFunc(MyTestDelegate func)
        {
            func(21);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod8() // 有沒有Invoke似乎沒差?
        {
            MathCalculation            add_1 = Calculator.AddNumbers;
            Func<float, float, double> add_2 = Calculator.AddNumbers;  // Func

            MathCalculation divide = Calculator.DivideNumbers;
            var result = Calculator.AddNumbers(2,3);
            HLog.print("init =" + result);
            result = add_1.Invoke(2, 3);
            HLog.print("add_1="+result);
            result = add_1(2, 3);
            HLog.print("add_1=" + result);
            result = add_2.Invoke(2, 3);
            HLog.print("add_2=" + result);
            result = add_2(2, 3);
            HLog.print("add_2=" + result);
            result = divide(100, 3);
            HLog.print("divide=" + result);
        }

        public static class Calculator
        {
            public static double AddNumbers(float value1, float value2) => value1 + value2;
            public static double DivideNumbers(float value1, float value2) => value1 / value2;
        }
        delegate double MathCalculation(float value1, float value2);


        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod9()
        {
            currentIF = functionImpl; // 有時候既有的currentIF已經存在, 需要為其實作功能
            currentIF();
        }

        static Action currentIF;
        public static void functionImpl()
        {
            HLog.print("你好");
        }

        static Action<int, int> Calculate;
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod10()
        {
            Calculate = Add;
            Calculate(6, 5); //傳入型態為2個int 會執行 Add(5,6)
            //num1 + num2 = 11
            Calculate = Sub;
            Calculate(6, 5); //會執行 Sub(5,6)
            //num1 - num2 = 1
        }

        public static void Add(int num1, int num2)
        {
            HLog.print($"num1 + num2 = {num1 + num2}");
        }
        public static void Sub(int num1, int num2)
        {
            HLog.print($"num1 - num2 = {num1 - num2}");
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod11() // 不帶參數的Func<out TResult>
        {
            func = GetHandSomeBoy;
            func();
        }
        private static Func<string> func; // <string> is out
        public static string GetHandSomeBoy()
        {
            return "Me";
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod12() // 帶傳入參數Func<int,int,string> , 回傳類型為string 傳入參數為int,int
        {
            Calculate2 = Add2;
            HLog.print(Calculate2(6, 5)); //傳入型態為2個int 會執行 Add(5,6)
            //num1 + num2 = 11
            Calculate2 = Sub2;
            HLog.print(Calculate2(6, 5)); //會執行 Sub(5,6)
            //num1 - num2 = 1
        }

        private static Func<int, int, string> Calculate2;
        public static string Add2(int num1, int num2)
        {
            return $"num1 + num2 = {num1 + num2}";
        }
        public static string Sub2(int num1, int num2)
        {
            return $"num1 - num2 = {num1 - num2}";
        }


        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod13()
        {
            Gossiping gossiping = new Gossiping();
            Passenger passenger = new Passenger();

            gossiping.Notify(passenger.ReceiveNews, "開放美豬進口了");
        }

        public class Gossiping
        {
            public void Notify(Action<string> action, string news)
            {
                action(news);
            }
        }
        public class Passenger
        {
            public void ReceiveNews(string news)
            {
                HLog.print($"我收到一則新聞內容是:{news}");
            }
        }
        public class FourPercent
        {
            public void Argue(string message)
            {
                HLog.print($"政府要{message}，政府有夠爛");
            }
        }
        public class Green
        {
            public void Support(string message)
            {
                HLog.print($"Green:政府要{message}，FourPercent好了拉，又死不了");
            }
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod14()
        {
            // Create an array of Point structures.
            Point[] points = {  new Point(100, 200),
                                new Point(150, 250), new Point(250, 375),
                                new Point(275, 395), new Point(295, 450) };

            // Define the Predicate<T> delegate.
            Predicate<Point> predicate = FindPoints; // KEY:

            // Find the first Point structure for which X times Y is greater than 100000.
            Point first = Array.Find(points, predicate);
         // Point first = Array.Find(points, x => x.X * x.Y > 100000 ); // lambda
            // Display the first structure found.
            HLog.print("Found: X = {0}, Y = {1}", first.X, first.Y);
        }

        private static bool FindPoints(Point obj)
        {
            return obj.X * obj.Y > 100000;
        }


    }
}
