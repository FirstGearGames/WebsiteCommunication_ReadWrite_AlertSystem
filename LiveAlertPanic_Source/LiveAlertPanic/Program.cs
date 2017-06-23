using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveAlertPanic
{

    static class Program
    {

        static Mutex LocalMutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool firstMutex = false;
            LocalMutex = new Mutex(true, "com.distul.livealertpanic", out firstMutex);
            if ((firstMutex))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                LocalMutex.ReleaseMutex();
            }
            else
            {
                Application.Exit();
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form1 = new Form1();
                Application.Run(form1);
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
