using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {

        public static string se(int a)
        { 
            switch (a)
            {
                case int b when b%1000==0: return "ci";
                case int b when b%100==0: return "cü";
                case int b when b%10==0 && new List<int> { 10, 30 }.Contains(b%100): return "cu";
                case int b when b%10==0 && new List<int> { 20, 50,70,80 }.Contains(b%100): return "ci";
                case int b when b%10==0 && new List<int> { 40, 60,70,90 }.Contains(b%100): return "cı";
                case int b when new List<int> { 1,2,5,7,8 }.Contains(b%10):return "ci";
                case int b when new List<int> { 3,4 }.Contains(b%10):return "cü";
                case int b when b % 10 == 6: return "cü";
                case int b when b % 10 == 9: return "cu";
                default:
                    break;
            }
            return "";
        }


        public class Payload
        {
            public string number { get; set; }
            public string message { get; set; }
        }

    
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;

            //Console.WriteLine(se(45));
            //Console.WriteLine(se(90));
            //Console.WriteLine(se(99));
            //Console.WriteLine(se(568));
            //Console.WriteLine(se(600));
            //Console.WriteLine(se(57));




            //SetTimer();

            //Console.WriteLine("\nPress the Enter key to exit the application...\n");
            //Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            //Console.ReadLine();
            //timer.Stop();
            //timer.Dispose();
            //-----------------------------------------------------

            //musteri = new Queue();
            //musteri = new ConcurrentQueue<string>();
            //musteri.Enqueue("11");
            //musteri.Enqueue("22");
            //musteri.Enqueue("33");
            //musteri.Enqueue("44");
            //musteri.Enqueue("55");
            //musteri.Enqueue("66");
            //musteri.Enqueue("77");
            //musteri.Enqueue("88");
            //musteri.Enqueue("99");

            //Kontrol().Wait();
            //--------------------------------------------------
            //ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
            //AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            //concurrentBag.Add(0);
            //#region Task 1
            //Task.Run(() =>
            //{
            //    Console.WriteLine("****");
            //    for (int i = 1; i < 10; i++) 
            //        concurrentBag.Add(i);
            //    autoResetEvent.Set();
            //});
            //#endregion 
            //#region Task 2 
            //Task.Run(() =>
            //{
            //    Console.WriteLine("----");
            //    while (!concurrentBag.IsEmpty)
            //        if (concurrentBag.TryTake(out int data))
            //            Console.WriteLine(data);
            //    autoResetEvent.WaitOne();
            //});
            //#endregion
            //-------------------------------------------------------------------


            //Task t = new Task(BekleyenThred);
            //t.Start();
            //Thread.Sleep(3000);
            //reset.Reset();
            //----------------------------------------------

            //AutoResetEvent res = new AutoResetEvent(false);
            //StatusChecker st = new StatusChecker(3);
            //Console.WriteLine("timerden evvel");
            //var timer = new Timer(st.CheckStatus, res, 1000, 1000);

            //Console.WriteLine("timerden sonra");
            //res.WaitOne();
            //Console.WriteLine("waitden sonra");
            //timer.Change(0, 500);
            //res.WaitOne();
            //Console.WriteLine("is bitdi");
            //Console.ReadLine();
            //var reset = new AutoResetEvent(false);

            //var timer = new Timer(Timer_Elapsed,reset,2000,2000);
            //Console.ReadLine();

            dynamic TestDinamic = new StatusChecker.TestDinamic1();

            //try { TestDinamic.C = 3; } catch { }
            //try { TestDinamic.A = 4; } catch { }

            //StatusChecker.TestDinamic1 d1 = TestDinamic;

            //Stopwatch st = new Stopwatch();

            //dynamic A = 3;
            //dynamic B = 4;

            //for (int i = 0; i < 100; i++)
            //{
            //    st.Start();
            //    var t = new { A, B };

            //    var r = JsonConvert.SerializeObject(t);

            //    StatusChecker.TestDinamic1 d1 = JsonConvert.DeserializeObject<StatusChecker.TestDinamic1>(r);
            //    Console.WriteLine(st.ElapsedMilliseconds);

            //    st.Reset();
            //}






            //Console.ReadLine();

        }



        static AutoResetEvent reset = new AutoResetEvent(false);
        static void BekleyenThred()
        {
            Console.WriteLine("gozleme basldi");
            reset.WaitOne();
            Console.WriteLine("Gozleme bitdi");
        }


        static ConcurrentQueue<string> musteri;
        static async Task Masa(string masaAdi)
        {
            while (musteri.Count > 0)
            {
                await Task.Delay(1000);
                musteri.TryDequeue(out string a);
                Console.WriteLine($"{masaAdi} - {a}");
            }
        }

        static async Task Kontrol()
        {
            var m1 = Masa("m1");
            var m2 = Masa("m2");
            await Task.WhenAll(m1, m2);
            Console.WriteLine("Masa sırası bitmiştir.");
        }


        public static System.Timers.Timer timer;

        static void SetTimer()
        {
            timer = new System.Timers.Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                         e.SignalTime);
        }

        private static void Timer_Elapsed(object sender)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                         DateTime.Now);
        }
    }




    class StatusChecker
    {
        private int invokeCount;
        private int maxCount;

        public StatusChecker(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Checking status {1,2}.",
                DateTime.Now.ToString("h:mm:ss.fff"),
                (++invokeCount).ToString());

            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }


        public class TestDinamic1
        {
            public int A { get; set; }
            public int B { get; set; }
        }
    }
}
