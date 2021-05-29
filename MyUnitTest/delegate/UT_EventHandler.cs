using System;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    // 委派是事件的基礎(event)，可以利用委派來呼叫不同的事件，以便觸發其他控制項事件來完成互動性強大的應用程式。
    // event 語法：
    // public event ClickEventHandler ClickEvent;
    //        [存取修飾詞]
    //        event 委派名稱 事件名稱(事件變數) 

    [TestClass]
    public class UT_EventHandler
    {
        [TestMethod]
        public void TestMethod1()
        {
            // show this msg at Right-Click\Run Tests\
            // Test Detail Summary\Open additional output for this result\Standard Output
            HLog.print("TestMethod1 !!!!!!!!!!!!");
        }

        // 委派可以將方法當成參數來進行傳遞
        [TestMethod]
        public void Sample1()
        {
            Police p = new Police("台灣隊長");                // 美國隊長  
            Thief thief1 = new Thief("小吳");                 // 小偷1
            Thief thief2 = new Thief("阿肥");                 // 小偷2

            // 實例化委託事件: 分別註冊小偷1 & 2快跑RunAway
            // += 相當於Add_PoliceCatchThiefEvent
            p.PoliceCatchThiefEvent += new Police.PoliceCatchThiefHandler(thief1.RunAway);
            p.PoliceCatchThiefEvent += new Police.PoliceCatchThiefHandler(thief2.RunAway);

            // 找到壞人, 觸發事件:PoliceCatchThiefEvent
            p.FindBadGuys();
        }

        // delegate名稱: void PoliceCatchThiefHandler(object se nder, PoliceCatchThiefEventArgs args)
        // event名稱: PoliceCatchThiefEvent
        [TestMethod]
        public void Sample2() //  事件加入EventArgs: 可以知道誰觸發(sender/obj)以及觸發時間(args.CurrentTime)
        {
            PoliceV2 p = new PoliceV2("台灣隊長");                // 美國隊長  
            ThiefV2 thief1 = new ThiefV2("小吳");                 // 小偷1
            ThiefV2 thief2 = new ThiefV2("阿肥");                 // 小偷2
        
            // 實例化委託事件: 分別註冊小偷1 & 2快跑RunAway
            // += 相當於Add_PoliceCatchThiefEvent
            p.PoliceCatchThiefEvent += new PoliceV2.PoliceCatchThiefHandler(thief1.RunAway);
            p.PoliceCatchThiefEvent += new PoliceV2.PoliceCatchThiefHandler(thief2.RunAway);
        
            // 找到壞人, 觸發事件:PoliceCatchThiefEvent
            p.FindBadGuys();
        }

        // Delegate方法，可用於向某個Class傳遞註冊過的方法（註冊的Method的參數必須和Delegate方法完全一致）
        [TestMethod]
        public void Sample3()
        {
            // show this msg at Right-Click\Run Tests\
            // Test Detail Summary\Open additional output for this result\Standard Output
            HLog.print("TestMethod1 !!!!!!!!!!!!");
        }


    }

    class Police
    {
        private string name;
        public Police(string name)
        {
            this.name = name;
        }
        // 警察抓小偷委派(方法類別)
        public delegate void PoliceCatchThiefHandler();
        // 警察抓小偷事件(方法變數)
        public event PoliceCatchThiefHandler PoliceCatchThiefEvent;
    
        public void FindBadGuys()
        {
            HLog.print("喂! 我是{0}", name);
            if(PoliceCatchThiefEvent != null)
            {
                PoliceCatchThiefEvent();
            }
      }
    }

    class Thief
    {
        private string name;
        public Thief(string name)
        {
            this.name = name;
        }
        public void RunAway()
        {
            HLog.print("警察來了! {0}快跑", name);
        }
    }

    class PoliceCatchThiefEventArgs : EventArgs
    {
        string name;
        DateTime dtime;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime CurrentTime
        {
            get { return dtime; }
            set { dtime = value; }
        }
    }

    class PoliceV2
    {
        string name;
        public PoliceV2(string name)
        {
            this.name = name;
        }
        // 警察抓小偷委派(方法類別)
        public delegate void PoliceCatchThiefHandler(object obj, PoliceCatchThiefEventArgs args);
        // 警察抓小偷事件(方法變數)
        public event PoliceCatchThiefHandler PoliceCatchThiefEvent;
        public void FindBadGuys()
        {
            Console.WriteLine("喂! 我是{0}", name);
            if (PoliceCatchThiefEvent != null)
            {
                PoliceCatchThiefEventArgs args = new PoliceCatchThiefEventArgs();
                args.Name = name;
                args.CurrentTime = DateTime.Now;
                PoliceCatchThiefEvent(this, args);
            }
      }
    }

    class ThiefV2
    {
        string name;
        public ThiefV2(string name)
        {
            this.name = name;
        }
        public void RunAway(object sender, PoliceCatchThiefEventArgs args)
        {
            HLog.print("{0} 警察 \"{1}\"來了!, \"{2}\"快跑", args.CurrentTime.ToString("yyyy/MM/dd HH:mm:ss"), args.Name, name);
        }
    }
}
