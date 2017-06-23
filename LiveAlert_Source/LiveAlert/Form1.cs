using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using System.Net;

namespace LiveAlert
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
        public string UserName = string.Empty;
        public string UserLocation = string.Empty;
        public string UserTelephone = string.Empty;
        private int UpdateInterval;
        private bool SoundStateChanges = false;
        #endregion

        //Last received webresult text.
        private string LastResult = string.Empty;
        //Last known state. Used for if connection is lost.
        private int LastState = 0;
        //True if failed previous update check.
        private bool FailedPreviousUpdateCheck = false;

        //If true form was hidden due to user clicking to send a message.
        public bool FormForcefullyHidden = false;

        /* Blocks the form from being closed. Used
         * to prevent accidental closes. Form should be closed
         * using system tray right-click menu. */
        private bool BlockClose = true;
        //Reference to submit/send message form.
        private FormSubmit FormSubmit = null;
        //Reference to configuration form.
        private FormConfiguration FormConfiguration = null;

        /// <summary>
        /// Called upon initializing the form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            AnchorForm();
            HideAlert();

            if (ConfigurationIncomplete())
            {
                MessageBox.Show("Program is not yet configured. You will be prompted to configure the program after clicking OK.", "Live Alert", MessageBoxButtons.OK);
                FormConfiguration = new FormConfiguration();
                FormConfiguration.Initialize(this);
                FormConfiguration.Show();
                return;
            }

            CheckForUpdate();
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

            Int32.TryParse(iniFile.Read("UpdateInterval", "LiveAlert"), out UpdateInterval);
            UpdateInterval = MinimumInterval(UpdateInterval);
            IntervalTimer.Interval = (UpdateInterval * 1000);

            bool.TryParse(iniFile.Read("SoundStateChanges", "LiveAlert"), out SoundStateChanges);
        }

        /// <summary>
        /// Makes interval a minimum value.
        /// </summary>
        /// <param name="interval">Current interval.</param>
        /// <returns></returns>
        public int MinimumInterval(int interval)
        {
            return (Math.Max(5, interval));
        }

        /// <summary>
        /// Minimizes application to tray.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DismissButton_Click(object sender, EventArgs e)
        {
            HideAlert();
        }

        /// <summary>
        /// When called checks for server update. Seconds interval specified in INI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntervalTimer_Tick(object sender, EventArgs e)
        {
            IntervalTimer.Enabled = false;
            CheckForUpdate();
        }

        /// <summary>
        /// Checks the server for any updates.
        /// </summary>
        public void CheckForUpdate()
        {
            //url to fetch from.
            string webpageURL = "URL TO WATCH AND PARSE";
            //Make a new webclient and uri for url.
            WebClient client = new WebClient();
            Uri uri = new Uri(webpageURL);

            //Handle completion callback.
            client.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                    HandleUpdateCheckResponse(true, e.Result);
                else
                    HandleUpdateCheckResponse(false, string.Empty);
            };

            //Begin requesting url.
            client.DownloadStringAsync(uri);

        }

        /// <summary>
        /// Handles response from update request.
        /// </summary>
        /// <param name="success"></param>
        private void HandleUpdateCheckResponse(bool success, string result)
        {
            //result string is empty. Shouldn't ever happen.
            if (result == string.Empty)
            {
                UpdateFailed("Warning: results are empty.");
                return;
            }

            //No changes in status. Don't need to proceed.
            if (result == LastResult)
            {
                Debug.Print("Result is unchanged. No display update required.");
                ColorHeader(true);
                UpdateSuccessful();
                return;
            }
            LastResult = result;

            //parse results
            int bodyStart = result.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
            int bodyEnd = result.IndexOf("</body>", StringComparison.OrdinalIgnoreCase);

            if (bodyStart == -1 || bodyEnd == -1)
            {
                UpdateFailed("Warning: couldn't find body tags in results.");
                return;
            }

            //Increase body start to skip past the <body> tag.
            bodyStart += "<body>".Length;
            //Calculate data length by getting difference between body start and end.
            int dataLength = (bodyEnd - bodyStart);
            //get section of result which contains data needed.
            string data = result.Substring(bodyStart, dataLength);
            //split data using regex, since string.split doesn't offer ignoreCase very easily.
            string[] lines = Regex.Split(data, "<br>", RegexOptions.IgnoreCase);

            //Values parsed and stored in these variables.
            int remoteStateResult = -1;
            string remoteMessage = string.Empty;
            string remoteUserName = string.Empty;
            string remoteUserLocation = string.Empty;
            string remoteUserTelephone = string.Empty;
            string remoteDateTime = string.Empty;
            /*  Data should arrive in the following fashion
             *  State: 1
             *  Message: This is a test message of the emergency alert system.
             *  User: Mark Newbegin
             *  Location: main building
             *  Phone: 207-299-4841
             *  DateTime: 2017-06-15 21:02:50 */

            //Go through each line
            int activeLine = 0;
            foreach (string line in lines)
            {

                //Output will contain data needed to fill a specific label.
                string output = string.Empty;
                //Split line from new line chars.
                string[] splitLine = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string split in splitLine)
                {
                    //Remove empty spaces from beginning and end of split.
                    string splitTrimmed = split.Trim();
                    //If split still contains data set it into output or append onto.
                    if (splitTrimmed != string.Empty)
                    {
                        if (output == string.Empty)
                            output = splitTrimmed;
                        else
                            output += Environment.NewLine + splitTrimmed;
                    }
                }
                //Check if output is string.empty, if not we have data for a line.
                if (output != string.Empty)
                {
                    //Determine which line we are setting from data.
                    switch (activeLine)
                    {
                        case 0:
                            Int32.TryParse(output, out remoteStateResult);
                            //Debug.Print("State received: " + output);
                            break;
                        case 1:
                            remoteMessage = output;
                            //Debug.Print("Message received: " + output);
                            break;
                        case 2:
                            remoteUserName = output;
                            //Debug.Print("User received: " + output);
                            break;
                        case 3:
                            remoteUserLocation = output;
                            //debug
                            //Debug.Print("Location received: " + output);
                            break;
                        case 4:
                            remoteUserTelephone = output;
                            //Debug.Print("Telephone received: " + output);
                            break;
                        case 5:
                            remoteDateTime = output;
                            //Debug.Print("DateTime received: " + output);
                            break;
                    }
                    //Increase activeLine
                    activeLine++;
                }

            }

            Debug.Print("Update successfully parsed.");

            //Set as last state.
            LastState = remoteStateResult;

            //Submission isn't from this client. Show alert.
            if ((remoteUserName != UserName) || (remoteUserTelephone != UserTelephone))
            {
                PlayAlertAudio(remoteStateResult);
                UpdateSuccessful();
                ShowAlert();
            }
            /* If username and telephone matches stored ini settings don't show update
             * as this would mean the update came from this client. */
            else
            {
                IntervalTimer.Enabled = true;
            }
            //Update the form display.
            DisplayParsedInformation(remoteMessage, remoteUserName, remoteUserLocation, remoteUserTelephone, remoteDateTime, remoteStateResult);
            //Change tray icon based on state.
            UpdateTrayIcon(remoteStateResult);
        }


        /// <summary>
        /// Updates the forms display with the supplied information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="userLocation"></param>
        /// <param name="userTelephone"></param>
        /// <param name="dateTime"></param>
        /// <param name="state"></param>
        public void DisplayParsedInformation(string message, string userName, string userLocation, string userTelephone, string dateTime, int state)
        {
            MessageRichTextBox.Text = message;
            UserLabel.Text = "User: " + userName;
            LocationLabel.Text = "Location: " + userLocation;
            TelephoneLabel.Text = "Telephone: " + userTelephone;
            DateTimeLabel.Text = dateTime;
            ColorBody(state);
            UpdateTrayIcon(state);
        }

        /// <summary>
        /// Updates system tray icon based on state.
        /// </summary>
        /// <param name="state">Current state.</param>
        private void UpdateTrayIcon(int state)
        {
            if (state == 0)
                NotificationIcon.Icon = Properties.Resources.StatusOkay;
            else if (state >= 1)
                NotificationIcon.Icon = Properties.Resources.StatusOther;
        }



        /// <summary>
        /// Plays an audio file based on state.
        /// </summary>
        /// <param name="state"></param>
        private void PlayAlertAudio(int state)
        {
            //If sounds are disabled exit method
            if (!SoundStateChanges)
                return;

            //Build path to sound file for given state.
            string audioPath = AppDomain.CurrentDomain.BaseDirectory + @"Audio\";
            string soundPath = audioPath + "status" + state.ToString() + ".wav";

            //File isn't found, can't play sound.
            if (!File.Exists(soundPath))
            {
                Debug.Print("Sound file is missing at " + soundPath);
                return;
            }

            //Load and play audio
            SoundPlayer soundPlayer = new SoundPlayer(soundPath);
            soundPlayer.Load();
            soundPlayer.Play();
        }

        /// <summary>
        /// Called after a successful update.
        /// </summary>
        private void UpdateSuccessful()
        {
            ColorHeader(true);
            IntervalTimer.Enabled = true;
        }

        /// <summary>
        /// When an update fails. Announces why in debug and restarts update timer. Also colors header to indicate failure.
        /// </summary>
        /// <param name="message">Debug message to print.</param>
        private void UpdateFailed(string message)
        {
            Debug.Print(message);
            FailedPreviousUpdateCheck = true;
            ColorHeader(false);
            IntervalTimer.Enabled = true;
        }

        /// <summary>
        /// Colors the application header.
        /// </summary>
        /// <param name="updateSuccessful">True if successfully retrieved an update.</param>
        private void ColorHeader(bool updateSuccessful)
        {
            if (updateSuccessful)
            {
                HeaderBackground.BackColor = Color.FromArgb(64, 64, 64);
                //check if current icon is connection error, if so change it to last state.
                if (FailedPreviousUpdateCheck)
                {
                    FailedPreviousUpdateCheck = false;
                    UpdateTrayIcon(LastState);
                }
            }
            else
            {
                HeaderBackground.BackColor = Color.Red;
                NotificationIcon.Icon = Properties.Resources.StatusConnectionError;
            }
        }

        /// <summary>
        /// Colors the application body.
        /// </summary>
        /// <param name="state">Alert state.</param>
        private void ColorBody(int state)
        {
            //default color for unknown state
            Color bodyColor = Color.FromArgb(192, 255, 255);
            //Warning state: Orange 255, 128, 0
            //Normal state: Light-Gray 224, 224, 224
            //Unknown state: Aqua 192, 255, 255
            if (state == 0)
                bodyColor = Color.FromArgb(224, 224, 224);
            else if (state >= 1)
                bodyColor = Color.FromArgb(255, 128, 0);

            InformationBackground.BackColor = bodyColor;
            InformationTable.BackColor = bodyColor;
            MessageRichTextBox.BackColor = bodyColor;
        }

        /// <summary>
        /// Called when system tray icon is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotificationIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                //Only allow this action if application is configured.
                if (ConfigurationIncomplete())
                    return;

                //if FormConfiguration is shown dispose of it.
                if (FormConfiguration != null)
                {
                    FormConfiguration.Dispose();
                    FormConfiguration = null;
                }

                if (this.Visible)
                    FormForcefullyHidden = true;

                HideAlert();
                //If send/submit message form is already open place it on top again.
                if (FormSubmit != null)
                {
                    FormSubmit.AnchorForm();
                    FormSubmit.SendToTop();
                    FormSubmit.Show();
                }
                //If send/submit message form isn't open make a new one and show it.
                else
                {
                    FormSubmit = new FormSubmit();
                    FormSubmit.Initialize(this);
                    FormSubmit.Show();
                }
            }
            //else if (e.Button == MouseButtons.Right)
            //    Debug.Print("Right clicked");
        }

        /// <summary>
        /// Called by FormSubmit when being closed, after a send or cancel.
        /// </summary>
        public void FormSubmitClosed()
        {
            FormSubmit = null;
            //If form was forcefully hidden then reveal it again.
            if (FormForcefullyHidden)
            {
                FormForcefullyHidden = false;
                ShowAlert();
            }
        }

        /// <summary>
        /// Called by FormConfiguration when being closed after a save or cancel.
        /// </summary>
        public void FormConfigurationClosed()
        {
            FormConfiguration = null;
            //If form was forcefully hidden then reveal it again.
            if (FormForcefullyHidden)
            {
                FormForcefullyHidden = false;
                ShowAlert();
            }

            /* Check if interval timer is enabled. If not this may be
             * the first time the app is run. Perform an instant check. */
            if (!IntervalTimer.Enabled)
                CheckForUpdate();
        }

        /// <summary>
        /// Hides Form1 GUI
        /// </summary>
        private void HideAlert()
        {
            //Set form as visible false
            this.Visible = false;
            //Don't show form in system tray
            this.ShowInTaskbar = false;
            //this.WindowState = FormWindowState.Minimized;
            NotificationIcon.Visible = true;
            //Hide form
            this.Hide();
        }

        /// <summary>
        /// Shows Form1 GUI.
        /// </summary>
        private void ShowAlert()
        {
            //Make form visible
            this.Visible = true;
            //Shouldnt be needed but in some off chance the user managed to minimize the app, this is called.
            this.WindowState = FormWindowState.Normal;
            //Show form
            this.Show();
            //Set always on top
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            //Align to bottom right of screen.
            AnchorForm();
        }

        /// <summary>
        /// Forces form to the bottom right of screen.
        /// </summary>
        private void AnchorForm()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
        }

        /// <summary>
        /// Called when form is told to close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BlockClose)
            {
                e.Cancel = true;
                return;
            }
            NotificationIcon.Icon = null;
        }

        /// <summary>
        /// Called when choosing "Show Alerts" from tray menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only allow this action if application is configured
            if (ConfigurationIncomplete())
                return;

            ShowAlert();
        }

        /// <summary>
        /// Called when choosing "Confirm (exit)" from tray menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BlockClose = false;
            Application.Exit();
        }

        /// <summary>
        /// Called when Configuration is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
                FormForcefullyHidden = true;

            HideAlert();

            //if FormSubmit is shown dispose of it.
            if (FormSubmit != null)
            {
                FormSubmit.Dispose();
                FormSubmit = null;
            }

            //If configuration form is already open place it on top again.
            if (FormConfiguration != null)
            {
                FormConfiguration.AnchorForm();
                FormConfiguration.SendToTop();
                FormConfiguration.Show();
            }
            //If configuration form isn't open make a new one and show it.
            else
            {
                FormConfiguration = new FormConfiguration();
                FormConfiguration.Initialize(this);
                FormConfiguration.Show();
            }
        }
    }
}
