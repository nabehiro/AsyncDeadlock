using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDeadlock.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main Start:{DateTime.Now}");

            var t1 = DoSomethingHeavy();
            var t2 = DoSomethingHeavy();
            Task.WaitAll(t1, t2);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            DoSomethingHeavy().Wait();
            DoSomethingHeavy().Wait();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            MainAsync().Wait();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ================================= ConfigureAwait Start");

            var t3 = DoSomethingHeavyConfigureAwait();
            var t4 = DoSomethingHeavyConfigureAwait();
            Task.WaitAll(t3, t4);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            DoSomethingHeavyConfigureAwait().Wait();
            DoSomethingHeavyConfigureAwait().Wait();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            MainAsyncConfigureAwait().Wait();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main End:{DateTime.Now}");
        }

        static async Task MainAsync()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] MainAsync Start:{DateTime.Now}");

            await DoSomethingHeavy();
            await DoSomethingHeavy();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] MainAsync End:{DateTime.Now}");
        }

        static async Task MainAsyncConfigureAwait()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] MainAsyncConfigureAwait Start:{DateTime.Now}");

            await DoSomethingHeavyConfigureAwait();
            await DoSomethingHeavyConfigureAwait();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] MainAsyncConfigureAwait End:{DateTime.Now}");
        }

        static async Task DoSomethingHeavy()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavy Start:{DateTime.Now}");

            await Task.Delay(1000);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavy End:{DateTime.Now}");
        }

        static async Task DoSomethingHeavyConfigureAwait()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavyConfigureAwait Start:{DateTime.Now}");

            await Task.Delay(1000).ConfigureAwait(false);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavyConfigureAwait End:{DateTime.Now}");
        }
    }
}
