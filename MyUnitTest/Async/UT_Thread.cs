using System;
using System.Threading;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_Thread
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void Test_Thread()
        {
            // Action action = () => this.doSomething("button3");
            ThreadStart threadStart = () => this.doSomething("button3");//委派
                                                                        //Thread thread =new Thread(action);
            Thread thread = new Thread(threadStart);
            thread.Start();//啟動
            /*
            thread.Suspend();//執行緒暫停  舊方法-被遺棄了-不建議使用-容易造成死鎖
            thread.Resume();//執行緒喚醒   舊方法-被遺棄了-不建議使用
            try {
                thread.Abort();//銷毀，方式是拋棄異常 也不建議 不一定及時
            } catch (Exception) {
                Thread.ResetAbort();//取消異常
            }
            */
            // 執行緒等待, 可以使用以下兩種方式
            // 1.主執行緒判斷子執行緒的狀態是否為停止，如果還沒停止就讓主執行緒sleep
            while (thread.ThreadState != ThreadState.Stopped)
                Thread.Sleep(100);//當前執行緒 休息100ms

            // 2.使用Join，當前執行緒等待thread完成
            thread.Join(500);//最多等待500
            Console.WriteLine("最多等待500ms");
            thread.Join();//當前執行緒等待thread完成

            // IsBackground 是Thread的特點 只有thread可以設定前景，背景
            Console.WriteLine(thread.IsBackground);
            // Default 是前景thread，啟動後一定要先完成任務的，阻止進程退出
            thread.IsBackground = true;//指定後台線程 隨者程式退出

            thread.Priority = ThreadPriority.Highest;//設定線程優先級別
        }

        private void doSomething(string name)
        {
            Console.WriteLine($"doSomething {name}Start 執行緒：{Thread.CurrentThread.ManagedThreadId}");
            long lRestul = 0;
            for (int i = 0; i < 1000000; i++)
            {
                lRestul++;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"doSomething {name}End 執行緒：{Thread.CurrentThread.ManagedThreadId}");
        }
        
        [TestMethod] // -----------------------------------------------------------------------
        public void Test_ThreadPool()
        {
            ThreadPool.QueueUserWorkItem(t => this.doSomething("button4_Click"));
            ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);
            ThreadPool.SetMaxThreads(16, 16);
            ThreadPool.SetMinThreads(8, 8);
        }

    }
}
