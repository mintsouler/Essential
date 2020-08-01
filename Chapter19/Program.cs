using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string url = "https://www.layui.com/doc/modules/util.html";

            Func<string, Task> WritWebRequestSizeAsync =
                async (string webRequestUrl) =>
            {
                Console.WriteLine(url);
                //error handling ommitted
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string text = await reader.ReadToEndAsync();
                    Console.WriteLine("text.Length = " + text.Length);
                }
            };

            Task task = WritWebRequestSizeAsync(url);

            while(!task.Wait(1))
            {
                Console.Write(".");
            }
            Console.WriteLine("\n");
            ContinueWith();
        }

        public static async Task WritWebRequestSizeAsync(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string text = await reader.ReadToEndAsync();
                    Console.WriteLine("text.Length : "+text.Length);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("exception :" + e.Message);
            }
        }

        static public void ContinueWith()
        {
            DisplayStatus("begin...");
            Task taskA = Task.Run(() => DisplayStatus("starting...")).ContinueWith(instance => DisplayStatus("continuing A ..."));
            Task taskB = taskA.ContinueWith(a => DisplayStatus("continuing B..."));
            Task taskC = taskA.ContinueWith(a => DisplayStatus("continuing C..."));
            Task.WaitAll(taskB, taskC);
            DisplayStatus("finished!");

            static void DisplayStatus(string text)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}：{text}");
            }
        }
    }
}
