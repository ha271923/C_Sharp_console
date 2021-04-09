using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_format
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            // 格式項目語法:
            // 每個格式項目都會使用下列格式，並由下列元件所組成：
            // { index[ , align][ : digit-format]}
            // ps.成對的大括號("{" 和 "}") 是必要的。
            string name = "Fred";
            HLog.print(String.Format("Name = {0}, hours = {1:hh}", name, DateTime.Now));
            // 強制的 index 元件 (也稱為參數規範) 是用以識別物件清單中對應項目的數字 (從 0 開始)
            HLog.print(String.Format("numbers = {0}, {1}, {2}, {3}", 10, 11, 12, 13)); // out = 10, 11, 12, 13
            // 多個格式項目可以藉由指定相同參數規範來參考物件清單中的相同項目。 
            // 例如，您可以指定複合格式字串（例如： "0x"）來格式化十六進位、科學和數位格式的相同數值 {0:X} {0:E} {0:N} 
            // D=Decimal , X=HEX , N=number , E=expermential , C=Currency
            HLog.print(String.Format("0x{0:X} {0:E} {0:N}", Int64.MaxValue)); // out = 0x7FFFFFFFFFFFFFFF 9.223372E+018 9,223,372,036,854,775,807.00
        }


        /*
            下列範例會定義兩個陣列，一個包含員工的名稱，另一個包含他們在兩週內的工作時數。 
            複合格式字串會在 20 個字元的欄位中，將名稱靠左對齊，並且在 5 個字元的欄位中，將其工作時數靠右對齊。 
            請注意，"N1" 標準格式字串也會用來格式化具有一個小數位數的時數。
         */
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2()
        {
            string[] names = { "Adam", "Bridgette", "Carla", "Daniel", "Ebenezer", "Francine", "George" };
            decimal[] hours = { 40, 6.667m, 40.39m, 82, 40.333m, 80, 16.75m };

            HLog.print("{0,-20} {1,5}\n", "Name", "Hours");
            for (int i = 0; i < names.Length; i++)
                HLog.print("{0,-20} {1,5:N1}", names[i], hours[i]); // "N1" 用來格式化具有一個小數位數的時數。

            // This example displays the following output:
            //    align LEFT      |RIGHT
            //    width=20        | w=5
            //12345678901234567890|54321  
            //Name                 Hours
            //
            //Adam                  40.0
            //Bridgette              6.7
            //Carla                 40.4
            //Daniel                82.0
            //Ebenezer              40.3
            //Francine              80.0
            //George                16.8
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3()
        {
            string FormatString1 = String.Format("{0:dddd MMMM}", DateTime.Now);
            string FormatString2 = DateTime.Now.ToString("dddd MMMM");

            int MyInt = 100;
            HLog.print("{0:C}", MyInt);             //  out = $100.00

            // 使用兩種不同方式來格式化一個物件
            string myName = "Fred";
            HLog.print(String.Format("Name = {0}, hours = {1:hh}, minutes = {1:mm}", myName, DateTime.Now));  //    Name = Fred, hours = 11, minutes = 30

            // 對齊用法
            string myFName = "Hawk", myLName = "Wei";
            int myInt = 100;
            string FormatFName = String.Format("First Name = |{0,10}|", myFName);
            string FormatLName = String.Format("Last Name = |{0,10}|", myLName);
            string FormatPrice = String.Format("Price = |{0,10:C}|", myInt);
            HLog.print(FormatFName);
            HLog.print(FormatLName);
            HLog.print(FormatPrice);
            HLog.print();

            FormatFName = String.Format("First Name = |{0,-10}|", myFName); // 負數為靠LEFT對齊
            FormatLName = String.Format("Last Name = |{0,-10}|", myLName);
            FormatPrice = String.Format("Price = |{0,-10:C}|", myInt);
            HLog.print(FormatFName);
            HLog.print(FormatLName);
            HLog.print(FormatPrice);
            // out=
            // ¤100.00
            // Name = Fred, hours = 03, minutes = 05
            // First Name = | Hawk |
            // Last Name = | Wei |
            // Price = | NT$100.00 |
            // 
            // First Name = | Hawk |
            // Last Name = | Wei |
            // Price = | NT$100.00 |
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4()  // 字串插補$在建立格式化字串時，是比{0}功能更容易理解且方便的語法。
        {
            string name = "Mark";
            var date = DateTime.Now;

            // Composite formatting:
            HLog.print("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
            // String interpolation:
            HLog.print($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
            // out:
            // Hello, Mark! Today is Wednesday, it's 19:40 now.
        }
    }
}
