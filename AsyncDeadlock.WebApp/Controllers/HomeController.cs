using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AsyncDeadlock.WebApp.Models;
using System.Threading;

namespace AsyncDeadlock.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ctor Start:{DateTime.Now}");

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Privacy Start:{DateTime.Now}");

            await DoSomethingHeavy();
            await DoSomethingHeavy();

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            DoSomethingHeavy().Wait();
            DoSomethingHeavy().Wait();

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            var t1 = DoSomethingHeavy();
            var t2 = DoSomethingHeavy();
            Task.WaitAll(t1, t2);

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            DoSomethingHeavy().Wait();
            DoSomethingHeavy().Wait();

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ================================= ConfigureAwait Start");

            var t3 = DoSomethingHeavyConfigureAwait();
            var t4 = DoSomethingHeavyConfigureAwait();
            Task.WaitAll(t3, t4);

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] =================================");

            DoSomethingHeavyConfigureAwait().Wait();
            DoSomethingHeavyConfigureAwait().Wait();

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Privacy End:{DateTime.Now}");

            return View();
        }

        static async Task DoSomethingHeavy()
        {
            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavy Start:{DateTime.Now}");

            await Task.Delay(1000);

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavy End:{DateTime.Now}");
        }

        static async Task DoSomethingHeavyConfigureAwait()
        {
            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavyConfigureAwait Start:{DateTime.Now}");

            await Task.Delay(1000).ConfigureAwait(false);

            Debug.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] DoSomethingHeavyConfigureAwait End:{DateTime.Now}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
