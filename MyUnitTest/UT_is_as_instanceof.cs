using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUnitTest.UT_Common;

namespace MyUnitTest
{
    /*
        類型測試keyword is:
        A: 類型模式，它會測試運算式是否可轉換為指定的型別，如果可以的話，會將變數轉換成該類型的變數。
        B: 常數模式，測試運算式是否評估為指定的常數值。
        C: var 模式，比對一定會成功，而且會將運算式的值繫結至新的區域變數。
     */

    public interface IExample { }
    public class BaseClass : IExample { }
    public class DerivedClass : BaseClass { }

    [TestClass]
    public class UT_is_as_instanceof
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod0_instanceof()
        {
            int[] arr = new int[11];

            var base1 = new BaseClass();
            var derived1 = new DerivedClass();

            var base1Type = base1.GetType();
            var derived1Type = derived1.GetType();

            var interfaceType = typeof(IExample);

            HLog.print("------------------- var ---------------------");
            HLog.print("Is int[] an instance of the Array class? {0}.",
                        typeof(Array).IsInstanceOfType(arr));

            HLog.print("Is derived1 an instance of BaseClass? {0}.",
                        base1.GetType().IsInstanceOfType(derived1));

            HLog.print("Is base1 an instance of IExample? {0}.",
                        interfaceType.IsInstanceOfType(base1));
            HLog.print("Is derived1 an instance of IExample? {0}.",
                        interfaceType.IsInstanceOfType(derived1));


            HLog.print("------------------- Object ---------------------");
            Object baseObj1 = new BaseClass();
            Object derivedObj1 = new DerivedClass();

            HLog.print("Object: Is derived1 an instance of BaseClass? {0}.",
                        baseObj1.GetType().IsInstanceOfType(derivedObj1));

            HLog.print("Object: Is base1 an instance of IExample? {0}.",
                        interfaceType.IsInstanceOfType(baseObj1));
            HLog.print("Object: Is derived1 an instance of IExample? {0}.",
                        interfaceType.IsInstanceOfType(derivedObj1));

        }


        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1_is_instanceof()
        {
            Object baseObj = new Animal();

            var obj = new Human("Hawk");
            bool isAnimal, isHuman;

            // A. is
            isAnimal = obj is Animal;
            isHuman = obj is Human;
            HLog.print("is                 isAnimal=" + isAnimal + "    isHuman=" + isHuman);

            // B. IsAssignableFrom()
            isAnimal = obj.GetType().IsAssignableFrom(typeof(Animal));
            isHuman = obj.GetType().IsAssignableFrom(typeof(Human));
            HLog.print("IsAssignableFrom() isAnimal=" + isAnimal + "    isHuman="+isHuman);

            // C. IsInstanceOfType()
            isAnimal = baseObj.GetType().IsInstanceOfType(obj);
            HLog.print("IsInstanceOfType() isAnimal=" + isAnimal);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1_check_obj()
        {
            Object obj = new Human("Hawk");
            ShowValue(obj);
            obj = new Dog("Corgi");
            ShowValue(obj);

            if (obj is Animal)
                HLog.print(" obj is Animal");
            else
                HLog.print(" obj is NOT Animal XXXXXXXXXXX");
        }
        public static void ShowValue(object o) {
            if (o is Human p)   // KEY: is Type
                HLog.print(p.Name);
            else if (o is Dog d)
                HLog.print(d.Name);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2_constant()
        {
            var d1 = new Dice();
            for(int i=0; i<10; i++)
                showDice(d1);
        }

        private static void showDice(object o) {
            const int MAX_DICE_VALUE = 6;

            Dice d = new Dice();

            if (d is null)
                throw new NullReferenceException();

            if (o is Dice && d.Roll() is MAX_DICE_VALUE) // KEY: is CONSTANT
                HLog.print($"The dice roll is MAX number == {MAX_DICE_VALUE} !");
            else
                HLog.print($"The dice number is {d.currentNumber}!");
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3_var()
        {
            int[] testSet = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            var primes = testSet.Where(n => Factor(n).ToList() is var factors // KEY: is var
                                        && factors.Count == 2
                                        && factors.Contains(1)
                                        && factors.Contains(n));

            foreach (int prime in primes)
            {
                HLog.print($"Found prime: {prime}");
            }
        }
        
        static IEnumerable<int> Factor(int number) {
            int max = (int)Math.Sqrt(number);
            for (int i = 1; i <= max; i++) {
                if (number % i == 0) {
                    yield return i;
                    if (i != number / i) {
                        yield return number / i;
                    }
                }
            }
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4_as_cast_obj()
        {
            object[] objArray = new object[6];
            objArray[0] = new Human("Hawk");
            objArray[1] = new Dog("Corgi");
            objArray[2] = "Hello";
            objArray[3] = 123;
            objArray[4] = 123.4;
            objArray[5] = null;

            for (int i = 0; i < objArray.Length; ++i) {
                string s = objArray[i] as string;  // KEY: as
                HLog.print("{0}:", i);
                if (s != null)
                    HLog.print("'" + s + "'");
                else
                    HLog.print("not a string");
            }
        }

    }

    public class Dice {
        Random rnd = new Random();

        public int number = 1;

        public int currentNumber { 
            get { return number; }
            set { number = value; }
        }

        public int Roll() {
            number = rnd.Next(1, 7);
            return number;
        }
    }
}