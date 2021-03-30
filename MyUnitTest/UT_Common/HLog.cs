using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Utils
{
    class HLog
    {
        public static void print(ulong value) {
            Console.WriteLine(value);
        }
        public static void print(string format, params object[] arg) {
            Console.WriteLine(format, arg);
        }
        public static void print()
        {
            Console.WriteLine();
        }
        public static void print(bool value)
        {
            Console.WriteLine(value);
        }
        public static void print(char[] buffer)
        {
            Console.WriteLine(buffer);
        }
        public static void print(char[] buffer, int index, int count)
        {
            Console.WriteLine(buffer, index, count);
        }
        public static void print(decimal value)
        {
            Console.WriteLine(value);
        }
        public static void print(double value)
        {
            Console.WriteLine(value);
        }
        public static void print(uint value)
        {
            Console.WriteLine(value);
        }
        public static void print(int value)
        {
            Console.WriteLine(value);
        }
        public static void print(object value)
        {
            Console.WriteLine(value);
        }
        public static void print(float value)
        {
            Console.WriteLine(value);
        }
        public static void print(string value)
        {
            Console.WriteLine(value);
        }
        public static void print(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }
        public static void print(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }
        public static void print(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }
        public static void print(long value)
        {
            Console.WriteLine(value);
        }
        public static void print(char value)
        {
            Console.WriteLine(value);
        }
    }
}
