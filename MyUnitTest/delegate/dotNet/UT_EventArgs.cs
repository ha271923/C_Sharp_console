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

        // --------------------------------------------------------
        public class 新聞 : EventArgs { // 將參數集合起來包裝成一個類別來傳遞。這樣以後要傳遞的參數需要修改或是數量增減也比較方便調整。
            public string 標題;
            public string 內容;
        }

        class 報社 {
            public string 名稱;
            public delegate void 通知對象(報社 有間報社, string 新聞報導);
            public event EventHandler 最新新聞;

            public void 投稿新聞(string 訊息) {
                新聞 new新聞 = new 新聞() { 標題 = "最新快訊", 內容 = 訊息 };
                On收到最新新聞時(this, new新聞);
            }

            protected void On收到最新新聞時(報社 報社, 新聞 新聞) {
                最新新聞?.Invoke(報社, 新聞);
            }
        }


        class 訂閱者 {
            public string 名字;
            public void 通知我(object sender, EventArgs eventArgs) {
                報社 報社 = sender as 報社;  // KEY: object to everything
                新聞 新聞 = eventArgs as 新聞; // KEY: EventArgs to everything
                Console.WriteLine($"我是{名字}，我已經收到來自{報社.名稱}的{新聞.標題}：{新聞.內容}");
            }
        }

        

    }
}
