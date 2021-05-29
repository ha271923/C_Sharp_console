using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using ConsoleApp.Utils;

namespace MyUnitTest
{
    [TestClass]
    public class UT_Task
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1()
        {
            ShowThreadInfo("Application");

            var t = Task.Run(() => ShowThreadInfo("Task"));
            t.Wait();
        }

        static void ShowThreadInfo(String s)
        {
            Console.WriteLine("{0} Thread ID: {1}",
                              s, Thread.CurrentThread.ManagedThreadId);
        }


        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod2()  // lambda
        {
            Console.WriteLine("Application thread ID: {0}",
                              Thread.CurrentThread.ManagedThreadId);
            var t = Task.Run(() =>
            {
                Console.WriteLine("Task thread ID: {0}",
                   Thread.CurrentThread.ManagedThreadId);
            });
            t.Wait();
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod3()
        {
            var list = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();
            foreach (var dirName in dirNames)
            {
                Task t = Task.Run(() => {
                    foreach (var path in Directory.GetFiles(dirName))
                        list.Add(path);
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
            foreach (Task t in tasks)
                Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

            Console.WriteLine("Number of files read: {0}", list.Count);
        }


        [TestMethod] // -----------------------------------------------------------------------
        public async Task TestMethod4Async()
        {
            HLog.print("TestMethod4Async");
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var files = new List<Tuple<string, string, long, DateTime>>();

            var t = Task.Run(() => {
                string dir = "C:\\Windows\\System32\\";
                object obj = new Object();
                if (Directory.Exists(dir))
                {
                    Parallel.ForEach(Directory.GetFiles(dir),
                    f => {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                        var fi = new FileInfo(f);
                        lock (obj)
                        {
                            files.Add(Tuple.Create(fi.Name, fi.DirectoryName, fi.Length, fi.LastWriteTimeUtc));
                        }
                    });
                }
            }
                              , token);
            await Task.Yield();
            tokenSource.Cancel();
            try
            {
                await t;
                Console.WriteLine("Retrieved information for {0} files.", files.Count);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);

                Console.WriteLine("\nTask status: {0}", t.Status);
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }
}
