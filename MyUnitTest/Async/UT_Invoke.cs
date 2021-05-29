using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_Invoke
    {
    }
}

    /*
    using ConsoleApp.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Threading;

    namespace MyUnitTest
    {
        [TestClass]
        public class UT_Invoke
        {
            // 
            // Problem: 在多執行緒(線程)編程中，我們經常要在工作執行緒(線程)中去更新介面顯示，而在多執行緒(線程)中直接調用介面控制元件的方法是錯誤的做法，
            // Ans: Invoke 和 BeginInvoke 就是為了解決這個問題而出現的，使你在多執行緒(線程)中安全的更新介面顯示。
            // 
            // 正確的做法是將工作執行緒(線程)中涉及更新介面的程式碼封裝為一個方法(method)，通過 Invoke 或者 BeginInvoke 去呼叫，兩者的區別就是一
            // 個導致工作執行緒(線程)等待，而另外一個則不會。
            // 
            // 一般應用：在WorkerThread中修改UI線程（ 主線程 ）中對象的屬性時，調用this.Invoke();
            //  在Multi-Thread中，我們經常要在WorkerThread中去更新界面顯示，而在MainThread中直接調用界面控件的方法是錯誤的做法，
            //  Invoke 和 BeginInvoke 就是為了解決這個問題而出現的，使你在多線程中安全的更新界面顯示。
            //  
            //  invoke和begininvoke的使用有兩種情況, 這兩種情況是不同的: invoke表是Sync、begininvoke表示Async
            //   1. control中的invoke、begininvoke。
            //   2. delegrate中的invoke、begininvoke。
            //   
            //   Control的Invoke和BeginInvoke的委託方法是在主線程，即UI線程上執行。 （也就是說如果你的委託方法用來取花費時間長的數據，然後更新界面什麼的，千萬別在主線程上調用Control.Invoke和Control.BeginInvoke，因為這些是依然阻塞UI線程的，造成界面的假死）
            //   Invoke會阻塞主支線程，BeginInvoke只會阻塞主線程，不會阻塞支線程！因此BeginInvoke的異步執行是指相對於支線程異步，而不是相對於主線程異步。 （從最後一個例子就能看出，程序運行點擊button1）
            //   
            // 
    [TestMethod] // -----------------------------------------------------------------------
    public void TestMethod1()
    {
        // show this msg at Right-Click\Run Tests\
        // Test Detail Summary\Open additional output for this result\Standard Output
        HLog.print("TestMethod1 !!!!!!!!!!!!");
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread NewThread = new Thread(new ThreadStart(NewThreadMethod));   //建立測試用的執行緒
            NewThread.Start();  //啟動測試用的執行緒
        }

        //原執行緒，被其它執行緒呼叫
        static void Method(int Param)
        {
            int i = Param;
        }

        //宣告一個委派，定義參數
        delegate void MyDelegate(int Param);

        //實作委派，指向員執行緒中被呼叫的Method
        MyDelegate ShowData = new MyDelegate(Method);

        //測試用的執行緒，在此呼叫原執行緒
        void NewThreadMethod()
        {
            int i = 0;
            while (true)
            {
                this.Invoke(this.ShowData, i);
                Thread.Sleep(2000);
            }
        }
    }
    }
}

*/
