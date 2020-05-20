using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadlock.WinFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ctor Start:{DateTime.Now}");

            InitializeComponent();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ctor End:{DateTime.Now}");

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] button1_Click Start:{DateTime.Now}");

            // OK
            //var t1 = DoSomethingHeavy();
            //var t2 = DoSomethingHeavy();
            //await t1;
            //await t2;

            // FREEZE !!!
            DoSomethingHeavy().Wait();

            // OK
            //var t1 = DoSomethingHeavyConfigureAwait();
            //var t2 = DoSomethingHeavyConfigureAwait();
            //Task.WaitAll(t1, t2);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] button1_Click End:{DateTime.Now}");

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
