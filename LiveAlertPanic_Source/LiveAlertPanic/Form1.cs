using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LiveAlertPanic
{

    public partial class Form1 : Form
    {

        #region Used to force form on top.
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion

        #region Alert submission variables.
        private string UserName = string.Empty;
        private string UserLocation = string.Empty;
        private string UserTelephone = string.Empty;
        #endregion

        //If true will display form.
        private bool ShowPanicWindow = false;
        //If true panic has been successfully sent.
        private bool PanicSent = false;

        public Form1()
        {
            this.Visible = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfiguration();

            //If configuration hasn't been completed alert user.
            if (ConfigurationIncomplete())
            {
                MessageBox.Show("Configuration has not been completed within Live Alert! Your panic message has not been sent!", "Live Alert Panic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
            //Configuration is okay, send panic message.
            else
            {
                SendPanicMessage();
            }

        }

        /// <summary>
        /// Hides panic window from user view.
        /// </summary>
        private void HidePanicWindow()
        {
            //Set form as visible false
            this.Visible = false;
            //Don't show form in system tray
            this.ShowInTaskbar = false;
            //Minimize
            this.WindowState = FormWindowState.Minimized;
            //Hide form
            this.Hide();
        }

        private void DisplayPanicWindow()
        {
            //Set form as visible false
            this.Visible = true;
            //Don't show form in system tray
            this.ShowInTaskbar = false;
            //Minimize
            this.WindowState = FormWindowState.Normal;
            //Hide form
            this.Show();
        }

        /// <summary>
        /// Send a panic message with user information.
        /// </summary>
        private void SendPanicMessage()
        {

            //Build url data
            string webpageURL = "URL WITH PHP TO SEND TO";

            string prettyMessage = "Panic button pressed!";
            string id = "id=99";
            string message = "&message=" + prettyMessage;
            string user = "&user=" + UserName;
            string location = "&location=" + UserLocation;
            string telephone = "&telephone=" + UserTelephone;

            string fullURL = webpageURL + id + message + user + location + telephone;

            //Make a new webclient and uri for url.
            WebClient client = new WebClient();
            Uri uri = new Uri(fullURL);

            //Handle completion callback.
            client.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    string result = e.Result;
                    if (result.Contains("success"))
                        HandlePanicResponse(true);
                    else
                        HandlePanicResponse(false);
                }
                //If error isn't null, an error has occured.
                else
                {
                    HandlePanicResponse(false);
                }
            };

            //Begin requesting url.
            client.DownloadStringAsync(uri);

        }

        /// <summary>
        /// Parses response after sending panic code.
        /// </summary>
        /// <param name="success"></param>
        private void HandlePanicResponse(bool success)
        {
            if (success)
            {
                OkayButton.BackColor = Color.FromArgb(8, 134, 213);
                OkayButton.ForeColor = Color.White;
                PanicTextLabel.Text = "Panic message has been sent!";
                RetryPanicTimer.Enabled = false;
                PanicSent = true;
                //If window isn't shown automatically exit application.
                if (!ShowPanicWindow)
                    Application.Exit();
            }
            //If send successfully allow user to close form.
            else
            {
                RetryPanicTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Return if configuration has been completed for vital information.
        /// </summary>
        /// <returns>True if vital information is configured.</returns>
        public bool ConfigurationIncomplete()
        {
            return (UserName == string.Empty || UserLocation == string.Empty || UserTelephone == string.Empty);
        }

        /// <summary>
        /// Loads configuration from settings.ini.
        /// </summary>
        public void LoadConfiguration()
        {
            IniFile iniFile = new IniFile("Settings.ini");
            UserName = iniFile.Read("UserName", "LiveAlert");
            UserLocation = iniFile.Read("UserLocation", "LiveAlert");
            UserTelephone = iniFile.Read("UserTelephone", "LiveAlert");

            //Get show panic window bool.
            bool.TryParse(iniFile.Read("ShowPanicWindow", "LiveAlert"), out ShowPanicWindow);
            //If not show panic window, hide form.
            if (!ShowPanicWindow)
                HidePanicWindow();
            //If show panic window, display form.
            else
                DisplayPanicWindow();
        }

        /// <summary>
        /// Called when Okay is pressed. Exits program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkayButton_Click(object sender, EventArgs e)
        {
            if (PanicSent)
                Application.Exit();
        }

        /// <summary>
        /// Cancel clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Timer for automatically retrying panic on failed send.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetryPanicTimer_Tick(object sender, EventArgs e)
        {
            SendPanicMessage();
        }
    }
}
