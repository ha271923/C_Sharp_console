using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
/*
    // using 指示詞有三個用途：
    1. 若要允許在命名空間中使用類型，使得您不需限定在該命名空間中使用的類型：
    using System.Text;

    2. 允許您存取類型的靜態成員和巢狀類型，但不必以類型名稱限定存取。
    using static System.Math;

    3. 若要建立命名空間或類型的'別名'。 這稱為 using alias 指示詞。
    using Project = PC.MyCompany.Project;
*/

// Using alias directive for a class.
using AliasToMyClass = MyUnitTest.NameSpace1.MyClass;

// Using alias directive for a generic class.
using UsingAlias = MyUnitTest.NameSpace2.MyClass<int>;



using Project = MyUnitTest.MyCompany.Project;


namespace MyUnitTest
{
    [TestClass]
    public class UT_using
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            var instance1 = new AliasToMyClass();
            Console.WriteLine(instance1);

            var instance2 = new UsingAlias();
            Console.WriteLine(instance2);
        }
    }

    // -------------------------------------------------
    namespace NameSpace1
    {
        public class MyClass
        {
            public override string ToString()
            {
                return "You are in NameSpace1.MyClass.";
            }
        }
    }

    namespace NameSpace2
    {
        class MyClass<T>
        {
            public override string ToString()
            {
                return "You are in NameSpace2.MyClass.";
            }
        }
    }

// ----------------------------------------------------------
    // Define an alias for the nested namespace.

    class A
    {
        void M()
        {
            // Use the alias
            var mc = new Project.MyClass();
        }
    }
    namespace MyCompany
    {
        namespace Project
        {
            public class MyClass { }
        }
    }
}