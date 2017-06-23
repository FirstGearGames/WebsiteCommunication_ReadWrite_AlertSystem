using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LiveAlert
{
    public partial class FormConfiguration : Form
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

        //Reference to form1. To tell it when this form closes.
        Form1 Form1;
        //True if sounds are enabled.
        private bool SoundsEnabled;
        //True if to show panic window.
        private bool ShowPanicWindow;

        public FormConfiguration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes form. Must be called manually.
        /// </summary>
        /// <param name="form1"></param>
        public void Initialize(Form1 form1)
        {
            Form1 = form1;
            AnchorForm();
            SendToTop();
            LoadConfigurationText();
        }

        /// <summary>
        /// Loads textfields and buttons with their current settings.
        /// </summary>
        private void LoadConfigurationText()
        {
            IniFile iniFile = new IniFile("Settings.ini");
            //User name.
            string userName = iniFile.Read("UserName", "LiveAlert");
            UserNameTextbox.Text = userName;
            //User location
            string userLocation = iniFile.Read("UserLocation", "LiveAlert");
            UserLocationTextbox.Text = userLocation;
            //User telephone
            string userTelephone = iniFile.Read("UserTelephone", "LiveAlert");
            UserTelephoneTextbox.Text = userTelephone;
            //Sounds enabled.
            bool soundsEnabled;
            bool.TryParse(iniFile.Read("SoundStateChanges", "LiveAlert"), out soundsEnabled);
            ApplySoundsEnabled(soundsEnabled);
            //Show panic window.
            bool showPanicWindow;
            bool.TryParse(iniFile.Read("ShowPanicWindow", "LiveAlert"), out showPanicWindow);
            ApplyShowPanicWindow(showPanicWindow);
            //Interval
            int updateInterval;
            Int32.TryParse(iniFile.Read("UpdateInterval", "LiveAlert"), out updateInterval);
            updateInterval = Form1.MinimumInterval(updateInterval);
            UpdateIntervalTextbox.Text = updateInterval.ToString();
        }

        /// <summary>
        /// Cancel clicked, close this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (Form1.ConfigurationIncomplete())
            {
                MessageBox.Show("Invalid configuration. Please fill out the configuration before exiting this window.", "Live Alert", MessageBoxButtons.OK);
                //Disable form1 timer on bad configuration
                Form1.IntervalTimer.Enabled = false;
            }
            else
            {
                //Inform Form1 that configuration has been closed.
                Form1.FormConfigurationClosed();
                //close this form
                this.Dispose();
            }
        }


        /// <summary>
        /// Colors a specified button based on selection state.
        /// </summary>
        /// <param name="selected">True if button is selected.</param>
        /// <param name="button"></param>
        private void ColorButton(bool selected, Button button)
        {
            if (selected)
            {
                button.BackColor = Color.FromArgb(8, 134, 213);
                button.ForeColor = Color.White;
            }
            else
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        /// <summary>
        /// Forces form to be on top of other windows.
        /// </summary>
        public void SendToTop()
        {
            //Set always on top
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        /// <summary>
        /// Forces form to the bottom right of screen.
        /// </summary>
        public void AnchorForm()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
        }

        /// <summary>
        /// Enable sounds clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableSoundsYesButton_Click(object sender, EventArgs e)
        {
            ApplySoundsEnabled(true);
        }

        /// <summary>
        /// Disable sounds clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableSoundsNoButton_Click(object sender, EventArgs e)
        {
            ApplySoundsEnabled(false);
        }

        /// <summary>
        /// Hide panic window clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPanicWindowNoButton_Click(object sender, EventArgs e)
        {
            ApplyShowPanicWindow(false);
        }
        /// <summary>
        /// Show panic window clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPanicWindowYesButton_Click(object sender, EventArgs e)
        {
            ApplyShowPanicWindow(true);
        }

        /// <summary>
        /// Applies sound enable state and colors buttons in accordance.
        /// </summary>
        /// <param name="enabled"></param>
        private void ApplySoundsEnabled(bool enabled)
        {
            //Update colors on buttons
            if (enabled)
            {
                ColorButton(true, EnableSoundsYesButton);
                ColorButton(false, EnableSoundsNoButton);
            }
            else
            {
                ColorButton(false, EnableSoundsYesButton);
                ColorButton(true, EnableSoundsNoButton);
            }

            //Update variable
            SoundsEnabled = enabled;
        }

        /// <summary>
        /// Applies show panic window state and colors buttons in accordance.
        /// </summary>
        /// <param name="enabled"></param>
        private void ApplyShowPanicWindow(bool enabled)
        {
            //Update colors on buttons
            if (enabled)
            {
                ColorButton(true, ShowPanicWindowYesButton);
                ColorButton(false, ShowPanicWindowNoButton);
            }
            else
            {
                ColorButton(false, ShowPanicWindowYesButton);
                ColorButton(true, ShowPanicWindowNoButton);
            }

            //Update variable
            ShowPanicWindow = enabled;
        }

        /// <summary>
        /// Called when save changes is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            //Save changes and tell form1 to reload configuration.
            SaveChanges();
            Form1.LoadConfiguration();
            //Check if configuration is complete.
            if (Form1.ConfigurationIncomplete())
            { 
                MessageBox.Show("Invalid configuration. Please fill out the configuration before clicking save.", "Live Alert", MessageBoxButtons.OK);
                //Disable form1 timer on bad configuration
                Form1.IntervalTimer.Enabled = false;
            }
            else
            {
                //Inform Form1 that configuration has been closed.
                Form1.FormConfigurationClosed();
                //Rid of this form.
                this.Dispose();
            }
        }

        /// <summary>
        /// Saves current setting values to ini.
        /// </summary>
        private void SaveChanges()
        {
            IniFile iniFile = new IniFile("Settings.ini");
            //User name.
            iniFile.Write("UserName", UserNameTextbox.Text, "LiveAlert");
            //User location
            iniFile.Write("UserLocation", UserLocationTextbox.Text, "LiveAlert");
            //User telephone
            iniFile.Write("UserTelephone", UserTelephoneTextbox.Text, "LiveAlert");
            //Sounds enabled.
            iniFile.Write("SoundStateChanges", SoundsEnabled.ToString(), "LiveAlert");
            //Show panic window.
            iniFile.Write("ShowPanicWindow", ShowPanicWindow.ToString(), "LiveAlert");
            //Interval
            iniFile.Write("UpdateInterval", UpdateIntervalTextbox.Text, "LiveAlert");
        }

  
    }
}
