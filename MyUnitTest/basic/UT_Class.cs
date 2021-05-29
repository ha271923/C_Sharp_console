using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest.basic
{
    /*
    private:   只限於本類成員訪問，子類，實例都不能訪問。(default) 
    public:    不受任何限制。 
    protected: 只限於本類和子類訪問，實例不能訪問。 
    internal:  只限於本項目內訪問，其他不能訪問。 
    protected internal: 內部保護訪問。只限於本項目或是子類訪問，其他不能訪問 
    sealed密封: 可以用來限制擴展性，如果密封了某個類，則其他類不能從該類繼承
     */
    class UT_Class
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void Test_sealed_func()
        {
            Z z = new Z();
            z.F3();
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void Test_sealed_class()
        {
            var sc = new SealedClass();
            sc.x = 110;
            sc.y = 150;
            Console.WriteLine($"x = {sc.x}, y = {sc.y}");
        }

}

    class X { // protected keyword in func
        protected virtual void F() { Console.WriteLine("X.F"); }
        protected virtual void F2() { Console.WriteLine("X.F2"); }
        public virtual void F3() { Console.WriteLine("X.F3"); }
    }

    class Y : X { // sealed+protected keyword in func
        sealed protected override void F() { Console.WriteLine("Y.F"); }
        protected override void F2() { Console.WriteLine("Y.F2"); }
        public sealed override void F3() { Console.WriteLine("Y.F3"); }
    }

    class Z : Y {
        // Function無法被複寫: Attempting to override F causes compiler error CS0239.
        // protected override void F() { Console.WriteLine("Z.F"); }

        // Overriding F2 is allowed.
        protected override void F2() { Console.WriteLine("Z.F2"); }
        // not allow to inherited sealed function
        // public  override void F3() { Console.WriteLine("Z.F3"); }

    }
    // **********************************************
    sealed class SealedClass
    {
        public int x;
        public int y;
    }

    /*
    sealed class MyClass : SealedClass // ERROR: can't derive from sealed type
    {
        public int z;
    }
    */
}
