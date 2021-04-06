#define NO_LAMBDA

using System;
using System.Drawing;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
     * �A�ɪ�����Delegate�����{�������X�ʭ��C�A�����F���ʡC
     * delegate, Func<>, Action<>, Predicate<>
     * Func<>�PAction<>���賣�O�e��(delegate)�A�u�O�S�h�]�F�@�h�C
     *  public delegate TResult Func<out TResult>();            Func�N�O���@�Ӧ^�ǭȪ��e��
     *  public delegate void    Action<in T>     (T obj);       Action�O�S���^�ǭȪ��e��
     * �j�x���i�D�A�A�A�����i�H��delegate�z �Ѧ�C�����禡���СA
     * �����\�A�ǻ��@����A����km���t�@����B������A�ϱo��B���������I�s�o�Ӥ�km�A���դF�N�O�i�H���k��@�޼ƶǻ��C
     * ���L delegate�M�禡�����٬O���I�ϧO���Adelegate���\�h�禡���Ф���ƪ��u�I�C
     * 1. �禡���Хu����V�R�A�禡�A��delegate�J�i�H�� ���R�A�禡�A�S�i�H�ޥΫD�R�A�����禡�C
     *    �b�ޥΫD�R�A�����禡�ɡAdelegate�����x�s�F�惡�禡�J�f���Ъ��ޥΡA�ӥB���x�s�F�I�s���禡�����Ҷ����ޥΡC
     * 2. �P�禡���Ь� ��Adelegate�O���V����B���O�w���B�i�a�������]managed�^����C�]�N�O���Aruntime����O��delegate���V
     *    �@�Ӧ��Ī���k�A �A�L�����delegate�|���V�L�Ħa�}(null)�Ϊ̶V�ɦa�}�C
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

        // 1. �e���i�H�N��k���ѼƨӶi��ǻ�
        // �e���ŧi�G
        // [public|private|protected] delegate[void | �^�Ǹ�ƫ��A] �e���W��([�Ѽ�1, �Ѽ�2,�K]);
        public delegate int Timestwo(int x); // delegate 1.�ŧi
        public delegate int Multiply(int x, int y);

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2() 
        {
            Timestwo mul_two = delegate (int x) { return 2 * x; };  // delegate 2.��@(�`�N�禡�W�٦b�쥻type����m) 3. ���w�e��
            Multiply mul     = delegate (int x, int y) { return x * y; };

            HLog.print("TestMethod2 mul_two = " + mul_two(5).ToString()); // delegate 4. �ϥ�
            HLog.print("TestMethod2 mul = " + mul(5, 6).ToString());
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4() // ��ϥ�new����delegate��Ʈ�
        {
            // �إ�delegate����   
            CompareDelegate cd = new CompareDelegate(Compare); // 2. ��@ & 3. �e���� new Object ���󦡼g�k
            // �I�sdelegate   
            HLog.print("TestMethod4 = " + cd(1, 2));
        }

        // �ŧidelegate����   
        public delegate int CompareDelegate(int a, int b); // 1. ���O�ŧi
        // ���ǻ�����k�A���PCompareDelegate�㦳�ۦP���޼ƩM��^�ȫ��O   
        public static int Compare(int a, int b) {
            if (a > b) return 1;
            else if (a == b) return 0;
            else return -1;
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod5() // ���J�ѼƳe���(MyTestDelegate(int i) <--> DelegateFunction(int i)
        {
            //�إ�delegate  
            //                      new delegateObj(method)
            ReceiveDelegateArgsFunc(new MyTestDelegate(DelegateFunction)); // 3. KEY: �_�����B, MyTestDelegate��arg�����Oint i, �b���o�O�禡 DelegateFunction
        }
        public delegate void MyTestDelegate(int i); // delegate 1. ���O�ŧi
        //���ǻ�����k  
        public static void DelegateFunction(int i) // 2. ��@
        {
            HLog.print("�ǹL�Ӫ��޼Ƭ�: {0}.", i);
        }
        //�o�Ӥ�k�����@��delegate���O���޼ơA�]�N�O�����@�Ө禡�@���޼�  
        public static void ReceiveDelegateArgsFunc(MyTestDelegate func)
        {
            func(21);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod8() // ���S��Invoke���G�S�t?
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
            currentIF = functionImpl; // ���ɭԬJ����currentIF�w�g�s�b, �ݭn�����@�\��
            currentIF();
        }

        static Action currentIF;
        public static void functionImpl()
        {
            HLog.print("�A�n");
        }

        static Action<int, int> Calculate;
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod10()
        {
            Calculate = Add;
            Calculate(6, 5); //�ǤJ���A��2��int �|���� Add(5,6)
            //num1 + num2 = 11
            Calculate = Sub;
            Calculate(6, 5); //�|���� Sub(5,6)
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
        public void TestMethod11() // ���a�Ѽƪ�Func<out TResult>
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
        public void TestMethod12() // �a�ǤJ�Ѽ�Func<int,int,string> , �^��������string �ǤJ�ѼƬ�int,int
        {
            Calculate2 = Add2;
            HLog.print(Calculate2(6, 5)); //�ǤJ���A��2��int �|���� Add(5,6)
            //num1 + num2 = 11
            Calculate2 = Sub2;
            HLog.print(Calculate2(6, 5)); //�|���� Sub(5,6)
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

            gossiping.Notify(passenger.ReceiveNews, "�}����޶i�f�F");
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
                HLog.print($"�ڦ���@�h�s�D���e�O:{news}");
            }
        }
        public class FourPercent
        {
            public void Argue(string message)
            {
                HLog.print($"�F���n{message}�A�F��������");
            }
        }
        public class Green
        {
            public void Support(string message)
            {
                HLog.print($"Green:�F���n{message}�AFourPercent�n�F�ԡA�S�����F");
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
