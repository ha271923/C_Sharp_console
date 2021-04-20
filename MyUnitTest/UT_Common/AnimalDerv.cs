using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp.Utils;

namespace MyUnitTest.UT_Common
{
    class AnimalDerv
    {
    }

    public class Animal
    {
        public string Name { get; set; }
    }

    public class Human : Animal
    {
        public string Name { get; set; }

        public Human(string name) { Name = name; }
        public void Say() { HLog.print("Hello everyone!"); }
    }

    public class Dog
    {
        public string Name { get; set; }

        public Dog(string breedName) { Name = breedName; }
        public void Bark() { HLog.print("WonWoonn~~~~~~"); }
    }
}
