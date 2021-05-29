using System;
using System.Threading;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_polymethods
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestNotPolymorphism()
        {
            int iValue = 123;
            string sValue = "123";
            DateTime dtValue = DateTime.Now;
            CommonMethod.ShowInt(iValue);
            CommonMethod.ShowString(sValue);
            CommonMethod.ShowDateTime(dtValue);
        }


        [TestMethod] // -----------------------------------------------------------------------
        public void TestObjectCls()
        {
            int iValue = 123;
            string sValue = "123";
            DateTime dtValue = DateTime.Now;
            ObjectMethod.ShowObject(iValue);
            ObjectMethod.ShowObject(sValue);
            ObjectMethod.ShowObject(dtValue);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestGeneric() // C# 2.0
        {
            try
            {
                int iValue = 123;
                string sValue = "456";
                DateTime dtValue = DateTime.Now;
                GenericMethod.Show<int>(iValue);
                GenericMethod.Show<string>(sValue);
                GenericMethod.Show<DateTime>(dtValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestGenericCache() // C# 2.0
        {
            try
            {
                GenericCache.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class CommonMethod // Not polymorphism
        {
            public static void ShowInt(int iParameter)
            {
                Console.WriteLine("This is {0},parameter={1},type={2}",
                    typeof(CommonMethod).Name, iParameter.GetType().Name, iParameter);
            }
            public static void ShowString(string iParameter)
            {
                Console.WriteLine("This is {0},parameter={1},type={2}",
                    typeof(CommonMethod).Name, iParameter.GetType().Name, iParameter);
            }
            public static void ShowDateTime(DateTime iParameter)
            {
                Console.WriteLine("This is {0},parameter={1},type={2}",
                    typeof(CommonMethod).Name, iParameter.GetType().Name, iParameter);
            }
        }

        public class ObjectMethod // Not polymorphism
        {
            public static void ShowObject(object oParameter) // 這方式會有一些效能上的問題
            {
                Console.WriteLine("This is {0},parameter={1},type={2}",
                    typeof(ObjectMethod), oParameter.GetType().Name, oParameter);
            }
        }

        public class GenericMethod
        {
            public static void Show<myName>(myName tParameter)
            {
                Console.WriteLine("This is {0},parameter={1},type={2}",
                typeof(GenericMethod), tParameter.GetType().Name, tParameter.ToString());
            }
        }

        public class GenericCache
        {
            public static void Show()
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(GenericCache<int>.GetCache());
                    Thread.Sleep(10);
                    Console.WriteLine(GenericCache<long>.GetCache());
                    Thread.Sleep(10);
                    Console.WriteLine(GenericCache<DateTime>.GetCache());
                    Thread.Sleep(10);
                    Console.WriteLine(GenericCache<string>.GetCache());
                    Thread.Sleep(10);
                }
            }
        }

        // 泛型緩存(Static)
        // a. 每個不同的Ｔ 都會生成一份不同的副本
        // b. 適合不同類型,需要緩存一份數據的場景 效率高
        // c. 比字典緩存效率高，但只能為不同類型保存一次
        public class GenericCache<T>
        {
            private static string _TypeTime = "";
            static GenericCache()
            {
                Console.WriteLine("{0}  靜態建構子 +++", typeof(T).FullName);
                _TypeTime = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString());
                Console.WriteLine("{0}  靜態建構子 ---");
            }

            public static string GetCache()
            {
                return _TypeTime;
            }
        }

    }
}
