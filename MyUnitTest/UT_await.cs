using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyUnitTest
{
    [TestClass]
    public class UT_await
    {
        [TestMethod] // -----------------------------------------------------------------------
        public void TestMethod1() { 

        }

        [TestMethod] // -----------------------------------------------------------------------
        public async Task TestMethod2Async()
        {
            HLog.print("TestMethod2Async");
            Task<int> downloading = DownloadDocsMainPageAsync();
            Console.WriteLine($"{nameof(TestMethod2Async)}: Launched downloading.");

            int bytesLoaded = await downloading;
            Console.WriteLine($"{nameof(TestMethod2Async)}: Downloaded {bytesLoaded} bytes.");
        }

        private static async Task<int> DownloadDocsMainPageAsync()
        {
            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
            return content.Length;
        }
    }
}
