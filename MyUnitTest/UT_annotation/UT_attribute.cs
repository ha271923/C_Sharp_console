using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_attribute
{
    /*
    特性attribute，和注釋有什麼區別:
    特性 本身是沒用的
    程序運行的過程中，可以透過反射找到特性
    在沒有破壞類型封裝前提下 可以加點額外的訊息和行為
    任何一個可以生效的特性，都是因為有地方主動使用他的
     */
    [TestClass]
    public class UT_attribute
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            TestObsolete.NewMethod();
            TestObsolete.OldMethod();
            // TestObsolete.ErrorMethod();
        }
    }

    public static class TestObsolete {
        public static void NewMethod() { HLog.print("NewMethod"); }

        [System.Obsolete("OldMethod, pls use NewMethod instead of OldMethod!", false)]
        public static void OldMethod() { HLog.print("OldMethod"); }

        [System.Obsolete("ErrorMethod, you can't use ErrorMethod!", true)]
        public static void ErrorMethod() { HLog.print("ErrorMethod"); }
    }

    // AttributeTargets=>只定可以被哪個類型修飾
    // Inherited = true =>可不可以被繼承，默認是true
    // AllowMultiple => 多重修飾，默認是flase 通常不推薦使用
    [Custom]
    public class Student {
        public int Id;
        public string Name;
        public void Study() { HLog.print($"這裡是{this.Name}跟著老師學習"); }
        public string Answer(string name) { return $"This is {name}"; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)] // =>讓聲明可以多重修飾某個元素
    public class CustomAttribute : Attribute     // 一個class繼承Attribute就是"特性"
    {
        public CustomAttribute() { }
        public CustomAttribute(int id) { }
        public string Description { get; set; }
        public string Remark = null;
        public void Show() { Console.WriteLine($"This is{nameof(CustomAttribute)}"); }
    }

    [Custom]    // 完全一樣的，表示都是使用無參數的建構子
    [Custom()]  // 完全一樣的，表示都是使用無參數的建構子
    [Custom(123)] // 帶參數的構造函數
    [Custom(123), Custom(123, Description = "123")] // 多重修飾
    [Custom(123, Description = "123", Remark = "2345")] // 方法不行
    public class Student2
    {
        //範圍10001~999999999999
        [LongAttribute(10001, 999999999999)]
        public int Id { get; set; }
        public string Name { get; set; }
        public void Study() { Console.WriteLine($"這裡是{this.Name}跟者老師學習"); }

        [Custom] //方法加上特性
        [return: Custom()] //給方法返回值加上特性
        public string Answer([Custom] string name) //給參數也加上特性
        {
            return $"This is {name}";
        }
    }
    public class LongAttribute : Attribute
    {
        private long _Min = 0;
        private long _Max = 0;
        public LongAttribute(long min, long max)
        {
            _Min = min;
            _Max = max;
        }

        public bool Validate(object value)
        {
            if (value != null && string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (long.TryParse(value.ToString(), out long lResult))
                {
                    if (lResult > this._Min && lResult < this._Max)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
