using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_EventArgs
    {
        [TestMethod]
        public void TestMethod1()
        {
            // show this msg at Right-Click\Run Tests\
            // Test Detail Summary\Open additional output for this result\Standard Output
            HLog.print("TestMethod1 !!!!!!!!!!!!");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var pub = new EventPublisher();
            var sub1 = new Subscriber("sub1", pub);
            var sub2 = new Subscriber("sub2", pub);

            // Call the method that raises the event. ex: the user is clicking the button
            pub.DoSomething();

            // Keep the console window open
            HLog.print("Pause...");
        }
    }

    // Define a class to hold custom event info
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    // Class that publishes an event, ex: a clicked by user
    class EventPublisher
    {
        // Declare the event using EventHandler<T>
        public event EventHandler<CustomEventArgs> RaiseCustomEventHandler;

        public void DoSomething()
        {
            // Write some code that does something useful here
            // then raise the event. You can also raise an event
            // before you execute a block of code.
            OnRaiseCustomEvent(new CustomEventArgs("Event triggered"));
        }

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<CustomEventArgs> raiseEventHandler = RaiseCustomEventHandler;

            // Event will be null if there are no subscribers
            if (raiseEventHandler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                e.Message += $" at {DateTime.Now}";

                // A. Call to raise the event.
                raiseEventHandler(this, e);

                // B. Raise the event in a thread-safe manner using the ?. operator and Invoke
                // raiseEventHandler?.Invoke(this, e);
            }
        }
    }

    //Class that subscribes to an event, ex: handle this click event
    class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, EventPublisher pub)
        {
            _id = id;

            // Subscribe to the event
            pub.RaiseCustomEventHandler += HandleCustomEvent; // A. C# 2.0: 使用加法指派運算子 (+=) 來將事件處理常式附加到事件
            // pub.RaiseCustomEvent += new CustomEventHandler(HandleCustomEvent); // B. C# 1.0: 使用 new 關鍵字明確建立封裝委派
            // pub.RaiseCustomEvent += (s, e) => // C. 使用 lambda
            // {
            //     Console.WriteLine($"{_id} received this message: {e.Message}");
            // };
        }

        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"{_id} received this message: {e.Message}");
        }
    }
}
