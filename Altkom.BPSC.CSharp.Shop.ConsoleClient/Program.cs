using Altkom.BPSC.CSharp.Shop.Models;
using Altkom.BPSC.CSharp.Shop.Printers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{

    class Program
    {
        private static void SendMessage(Message message)
        {
            Console.WriteLine($"{message.From} -> {message.To} : {message.Content}");
        }

        static async Task Main(string[] args)
        {
            Message message = new Message
            {
                From = "marcin.sulecki@altkom.pl",
               // To = "marcin.sulecki@altkom.pl",
                Content = "Hello World"
            };

            ICommand command = new SendMessageCommand(message);

            command.Execute();
            
            LockTest();

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId}");

            CancellationTokenSource cts =
                new CancellationTokenSource(TimeSpan.FromSeconds(3));

            Progress<int> progress = new Progress<int>();

            progress.ProgressChanged += Progress_ProgressChanged;

            LongProcessAsync(cts.Token, progress);

            // Console.WriteLine("Press enter to cancel");
            //Console.ReadLine();
            //cts.Cancel();

            // cts.CancelAfter(TimeSpan.FromSeconds(3));

            //Task.Run(() => GetWebsite("http://www.altkom.pl"))
            //    .ContinueWith(t => SendSmsAsync(t.Result));


            try
            {
                await TestAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //    Task.Run(() => AsyncTest());

            //GetWebsiteAsync("http://www.altkom.pl")
            //    .ContinueWith(t => SendSmsAsync(t.Result));

            // string content1 = GetWebsiteAsync("").Result;

            //string content = GetWebsite("http://www.altkom.pl");
            //SendSms(content);


            // Action
            // Func<string, int>
            // Predicate

            //Task t1 = Task.Run(() => Download("http://www.altkom.pl"));
            //Task t2 = Task.Run(() => Download("http://www.microsoft.com"));
            //Task t3 = Task.Run(() => Download("http://www.intel.com"));

            //for (int i = 0; i < 100; i++)
            //{
            //    Task.Run(() => Download("http://www.altkom.pl"));
            //}


            //Task.WaitAll(t1, t2, t3);

            // ThreadTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            // DelegateTest();
            // ExtensionsTest();
            // GenericMethodTest();
            // CreateOrderTest();
        }

        private static void LockTest()
        {
            object syncLock = new object();

            lock (syncLock)
            {
                // code
            }

            // lock tłumaczony jest do zapisu w następującej postaci:
            Monitor.Enter(syncLock);
            // code
            Monitor.Exit(syncLock);
        }

        private static void Progress_ProgressChanged(object sender, int e)
        {
            Console.Write($"=");
        }

        private static void LongProcess(CancellationToken token, IProgress<int> progress)
        {
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    // token.ThrowIfCancellationRequested();
                    return;
                }

                progress.Report(i);

               // Console.WriteLine(i);
                Thread.Sleep(TimeSpan.FromSeconds(0.2));
            }
        }

        private static Task LongProcessAsync(CancellationToken token, IProgress<int> progress)
        {
            return Task.Run(() => LongProcess(token, progress));
        }


        private static void Test()
        {
            string content = GetWebsite("http://www.altkom.pl");
            SendSms(content);
        }

        private static async Task TestAsync()
        {
            string content = await GetWebsiteAsync("http://www.altkom.pl")
                .ConfigureAwait(false);

            await SendSmsAsync(content);

            Console.WriteLine("Async Test Finished.");
            
        }

        private static void ThreadTest()
        {
            Thread t1 = new Thread(() => Download("http://www.altkom.pl"));
            Thread t2 = new Thread(() => Download("http://www.microsoft.com"));
            Thread t3 = new Thread(() => Download("http://www.intel.com"));

            Thread t4 = new Thread(() => Download("http://www.altkom.pl"));
            Thread t5 = new Thread(() => Download("http://www.microsoft.com"));
            Thread t6 = new Thread(() => Download("http://www.intel.com"));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();

            //Thread t1 = new Thread(SendEmail);
            //t1.Start();

            //Thread t2 = new Thread(SendEmail);
            //t2.Start();
        }

        private static void SendEmail()
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sending...");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sent.");
        }

        private static void Download(string uri)
        {
            using (var client = new WebClient())
            {
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloading... {uri}");

                string content = client.DownloadString(uri);

                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloaded. {content.Length}");
            }
        }

        private static Task<string> GetWebsiteAsync(string uri)
        {
            return Task.Run(() => GetWebsite(uri));
        }

        private static string GetWebsite(string uri)
        {
            using (var client = new WebClient())
            {
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloading... {uri}");

                string content = client.DownloadString(uri);
                Thread.Sleep(5000);

                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloaded. {content.Length}");

                

                return content;
            }
        }

        private static void DelegateTest()
        {
            Printer printer = new Printer();

            printer.log = null;

            printer.Finished += SendSms;

            printer.log += SendTweet;
            printer.log += SendSms;

            printer.log.Invoke("Hej!");

            // metoda anonimowa
            printer.log += delegate (string msg)
            {
                Console.WriteLine($"Send to instagram {msg}");
            };

            var list = printer.log.GetInvocationList();

            printer.log += m => Console.WriteLine(m);

            printer.log += Console.WriteLine;

            printer.Print("Hello World");

            printer.log -= SendSms;
            printer.Print("Hello World 2");

            printer.log = null;
            printer.Print("Hello World 3");


            string message = "Hello World";
            SendSms(message);
            SendTweet(message);

            Send send;

            send = SendSms;
            send += SendTweet;



            send("Hello World");
        }

        public delegate void Send(string message);

        private static Task SendSmsAsync(string message)
        {
            return Task.Run(() => SendSms(message));
        }

        private static void SendSms(string message)
        {
            Console.WriteLine($"sending...");
            Thread.Sleep(TimeSpan.FromSeconds(5));

            // throw new ApplicationException("brak połączenia");

            Console.WriteLine($"send via sms {message}");
        }

        private static void SendTweet(string tweet)
        {
            Console.WriteLine($"send tweet {tweet}");
        }

        private static void SendFacebook(string post, bool like = false)
        {

        }

        private static void GenericMethodTest()
        {
            Printer printer = new Printer();

            printer.Print(DateTime.Now);
        }

        private static void ExtensionsTest()
        {
            DateTimeHelper.IsHoliday(DateTime.Today);

            DateTime.Today.IsHoliday();
        }

        private static void CreateOrderTest()
        {
            Order order;

            Customer customer = new Customer("Marcin", "Sulecki");

            order = new Order("ZAM 001/2018", customer);

            Product product1 = new Product("Monitor", 15);
            product1.UnitPrice = 0;

            string input = "S";

           // Item item = ItemFactory.Create(input);


            Service service1 = new Service("Usługa programistyczna", 100, 1);

            OrderDetail orderDetail1 = new OrderDetail(product1, 5);
            OrderDetail orderDetail2 = new OrderDetail(service1, 1);

            order.Details.Add(orderDetail1);
            order.Details.Add(orderDetail2);


            Console.WriteLine($"{order.ToString()}");
          



        }
    }
}
