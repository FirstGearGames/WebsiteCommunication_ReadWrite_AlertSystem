using System;
using System.Windows.Forms;
using System.Threading;

namespace LiveAlert
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
            LocalMutex = new Mutex(true, "com.distul.livealert", out firstMutex);
            if ((firstMutex))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                LocalMutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("LiveAlert is already running! Cannot open another instance.", "Live Alert", MessageBoxButtons.OK);
                Application.Exit();
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
