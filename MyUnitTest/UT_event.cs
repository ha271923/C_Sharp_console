using System;
using System.Threading;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    /*
        event是一種特別的delegate:
        也可以說委派是事件的基礎，就算使用.NET提供的EventHandler，但背後實作還是依靠委派delegate。
        比較不同的地方在於，使用event關鍵字來限制委派的部分功能，讓委派不會被外部執行，但本質上還是委派，只是多了一道鎖。
        另外事件的目的也只是單向傳遞，所以委派接受的方法並不會有回傳值，就像郵差派送出去也是送後不理，要回信要自己另外寄。
        當然一方面的原因，也是因為委派本來就不能接收多個方法的回傳值，只能接受一個方法的回傳。
     */
    [TestClass]
    public class UT_event
    {

        [TestMethod]
        public void TestMethod2()
        {
            MyEventPublisher handler = new MyEventPublisher(); // 
            MyEventSubscriber e = new MyEventSubscriber();
            handler.changeNumEvent += new MyEventPublisher.MyEventHandler(e.processEvent); //KEY: monitor Number change event
            handler.setValue(7);
            handler.setValue(11);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var car = new Car(15);
            new Alerter(car);
            HLog.print("TestMethod2 !!!!!!!!!!!!");
            car.Run(120);
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod4()
        {
            訂閱者 農夫 = new 訂閱者() { 名字 = "農夫" };
            訂閱者 商人 = new 訂閱者() { 名字 = "商人" };
            訂閱者 騎士 = new 訂閱者() { 名字 = "騎士" };

            報社 王國日報 = new 報社();
            //訂閱
            王國日報.最新新聞 += 農夫.通知我;
            王國日報.最新新聞 += 商人.通知我;
            王國日報.最新新聞 += 騎士.通知我;

            string 消息1 = "魔王降臨啦!!!";
            王國日報.投稿新聞(消息1);
            string 消息2 = "勇者準備出發";
            王國日報.投稿新聞(消息2);

            // 王國日報.最新新聞.Invoke("假新聞"); // 保護: 多了event keyword, 讓delegate依然能被外部加入( += )方法，又能禁止被外部執行。
        }



    }

    // -----------------------------------------------------------------------------------------
    public class MyEventPublisher
    {
        private int value;
        public delegate void MyEventHandler();
        public    event      MyEventHandler changeNumEvent;
        // event與delegate的關係: event MyEventHandler changeNumEvent += delegate void myEventHandler();

        public MyEventPublisher()
        {
            int n = 5;
            setValue(n);
        }

        public void setValue(int n)
        {
            if (value != n)
            {
                value = n;
                OnNumChanged();
            }
        }

        protected virtual void OnNumChanged()
        {
            if (changeNumEvent != null)
                changeNumEvent(); // event triggering~~~~~
            else
                HLog.print("Event not trigger");
        }
    }
    public class MyEventSubscriber
    {
        public void processEvent()
        {
            HLog.print("Event triggered");
        }
    }

    // -----------------------------------------------------------------------------------------

    class Car
    {
        public delegate void Notify(int value);
        public    event      Notify notifier; // KEY: event EventHandler Event;
        // event與delegate的關係: event Notify notifier += delegate void Notify(int value);

        private int petrol = 0;
        public int Petrol
        {
            get { return petrol; }
            set {
                petrol = value;
                if (petrol < 10) {  //當petrol的值小於10時，出發警報
                    notifier?.Invoke(Petrol); // KEY: Invoke的對象是...變數?
                }
            }
        }

        public Car(int petrol) {
            Petrol = petrol;
        }

        public void Run(int speed) {
            int distance = 0;
            while (Petrol > 0) {
                Thread.Sleep(100);
                Petrol--; // KEY: this value is monitoring by Alerter
                distance += speed;
                HLog.print("Car is running... Distance is " + distance.ToString());
            }
        }
    }

    class Alerter // 警報器
    {
        public Alerter(Car car) {
            car.notifier += new Car.Notify(NotEnoughPetrol); // KEY: event與delegate綁定
        }

        public void NotEnoughPetrol(int value) {
            HLog.print("ALERT! You only have " + value.ToString() + " gallon petrol left..");
        }
    }
    // -----------------------------------------------------------------------------------------

    class 訂閱者
    { // Subscriber
        public string 名字;
        public void 通知我(string 訊息)
        {
            HLog.print($"我是 {名字} ，我已經收到最新新聞： {訊息}");
        }
    }
    public delegate void 通知對象(string 通知內容);

    class 報社
    { // Publisher
        public delegate void 通知對象(string 新聞報導);
        public event 通知對象 最新新聞;
        public void 投稿新聞(string 新聞稿)
        {
            最新新聞.Invoke(新聞稿); //觸發事件
        }
    }

}
