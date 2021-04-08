﻿using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_operator_Nullable
    {
        /*
          當您需要代表基礎實值型別的未定義值時，通常會使用可為 null 的實值型別。 
          例如，布林值或 bool 變數只能是 true 或 false 。 不過，在某些應用程式中，變數值不能定義或遺失。 
          例如，資料庫欄位可能包含 true 或 false ，或可能完全不包含任何值，也就是 NULL 。 bool?在該案例中，您可以使用類型。
            https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/nullable-value-types
         */

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            // 任何可為 null 的實值型別都是泛型結構的實例 System.Nullable<T> 
            // int? 可為 null 的實數值型別, 可以使用 is 運算子搭配型別模式來檢查可為 null 實值型別的實例 null ，並取出基礎型別的值
            int i = 10;
            int? m = i;
            double? d = 3.14;
            char? c = 'a';
            bool? flag = null;
            int?[] arr = new int?[10]; // An array of a nullable value type:

            if (m is int valueOfInt)
                HLog.print($"m is {valueOfInt}");
            else
                HLog.print("m does not have a value");

            if (m.HasValue)
                HLog.print($"m is {m.Value}");
            else
                HLog.print("m does not have a value");

            if (m != null)
                HLog.print($"m is {m.Value}");
            else
                HLog.print("m does not have a value");
            
            // 從可為 null 的實值型別 轉換 為基礎類型
            int? m1 = 28;
            int i1 = m1 ?? -1;
            HLog.print($"i1 is {i1}");  // output: i1 is 28

            int? m2 = null;
            int i2 = m2 ?? -1;
            HLog.print($"i2 is {i2}");  // output: i2 is -1

            int? m3 = null;
            //int i3 = m3;    // Doesn't compile
            int i3 = (int)m3; // Compiles, but throws an exception if n is null
            HLog.print($"i3 is {i3}");

            // 如何判斷這是 可為 null 的實值型別
            HLog.print($"int? is {(IsNullable(typeof(int?)) ? "nullable" : "non nullable")} value type");
            HLog.print($"int is {(IsNullable(typeof(int)) ? "nullable" : "non-nullable")} value type");
            // Output:
            // int? is nullable value type
            // int is non-nullable value type
            
            int? a = 17;
            Type typeOfA = a.GetType();
            HLog.print(typeOfA.FullName);
            // Output:
            // System.Int32

            // 請勿使用 is 運算子來判斷實例是否為可為 null 的實值型別。 
            // 如下列範例所示，您無法使用運算子來區別可為 null 的實值型別實例及其基礎型別實例的類型 is
            int? m5 = 14;
            if (m5 is int)
                HLog.print("int? instance is compatible with int");

            int i6 = 17;
            if (i6 is int?)
                HLog.print("int instance is compatible with int?");
            // Output:
            // int? instance is compatible with int
            // int instance is compatible with int?

            int? m8 = 14;
            HLog.print(IsOfNullableType(m8));  // output: True

            int i8 = 17;
            HLog.print(IsOfNullableType(i8));  // output: False

            bool IsOfNullableType<T>(T o)
            {
                var type = typeof(T);
                return Nullable.GetUnderlyingType(type) != null;
            }
        }

        private bool IsNullable(Type type) { throw new NotImplementedException(); }
    }
}
