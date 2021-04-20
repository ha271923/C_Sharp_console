using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
        匿名類型提供一個便利的方法，將一組唯讀屬性封裝成一個物件，而不需要事先明確定義類型。 
        類型名稱會由編譯器產生，並且無法在原始程式碼層級使用。 每個屬性的類型會由編譯器推斷。
        您可以搭配物件初始設定式使用 new 運算子，來建立匿名型別。
        匿名型別是直接衍生自 object，並且無法轉換成 object 以外之任何類型的 class 類型。 
    */

    [TestClass]
    public class UT_anoymous
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            int[] nums = { 0, 1, 2, 3 };

            foreach (var n in nums)
            {
                HLog.print("n="+n);
            }

            var v = new { Amount = 108, Message = "Hello" };

            // Rest the mouse pointer over v.Amount and v.Message in the following  
            // statement to verify that their inferred types are int and string.  
            HLog.print(v.Amount + v.Message);

            var anonArray = new[] { new { name = "apple", diam = 4 }, new { name = "grape", diam = 1 } };

        }    
    }
}
