using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadlock.WinFormApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main Start:{DateTime.Now}");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main End:{DateTime.Now}");

        }
    }
}
