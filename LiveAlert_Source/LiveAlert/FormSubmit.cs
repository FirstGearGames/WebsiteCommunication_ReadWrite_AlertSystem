using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LiveAlert
{
    public partial class FormSubmit : Form
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

        //Becomes true when all message requirements are met.
        private bool CanSendMessage = false;
        //Reference to form1. To tell it when this form closes.
        Form1 Form1;

        public FormSubmit()
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
            AddCodeOptions();
            AnchorForm();
            SendToTop();
        }

        /// <summary>
        /// Adds code options to codecombolist and selects the first entry.
        /// </summary>
        private void AddCodeOptions()
        {
            CodeCombolist.Items.Add("Please select a code...");
            CodeCombolist.Items.Add("All Clear");
            CodeCombolist.Items.Add("Building Evacuation");
            CodeCombolist.Items.Add("Dangerous Person");
            CodeCombolist.Items.Add("Police Needed");
            CodeCombolist.Items.Add("Requesting Assistance");
            CodeCombolist.SelectedIndex = 0;
        }

        /// <summary>
        /// Cancel clicked, close this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Form1.FormSubmitClosed();
            this.Dispose();
        }

        /// <summary>
        /// Send Message clicked. Send message if a code is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            //If cannot send message exit method.
            if (!CanSendMessage)
            {
                MessageBox.Show("You must select a code before being able to send a message.", "LiveAlert", MessageBoxButtons.OK);
                return;
            }

            SendMessage();
        }

        /// <summary>
        /// Sends a message using information on this form.
        /// </summary>
        private void SendMessage()
        {
            //Temporarily disable the button so that the user cant spam it.
            SendMessageButton.BackColor = Color.Gray;
            SendMessageButton.Enabled = false;

            //Build url data
            string webpageURL = "URL WITH PHP TO SUBMIT DATA TO";

            string prettyMessage = string.Empty;
            if (MessageOutRichTextBox.Text != string.Empty)
                prettyMessage = ": " + CodeCombolist.SelectedText + MessageOutRichTextBox.Text;

            string id = "id=" + (CodeCombolist.SelectedIndex - 1).ToString();
            string message = "&message=" + CodeCombolist.Text + prettyMessage;
            string user = "&user=" + Form1.UserName;
            string location = "&location=" + Form1.UserLocation;
            string telephone = "&telephone=" + Form1.UserTelephone;

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
                        HandleSendMessageResponse(true, prettyMessage);
                    else
                        HandleSendMessageResponse(false, string.Empty);
                }
                //If error isn't null, an error has occured.
                else
                {
                    HandleSendMessageResponse(false, string.Empty);
                }
            };

            //Begin requesting url.
            client.DownloadStringAsync(uri);
        }

        /// <summary>
        /// Performs actions based on if message was sent successfully.
        /// </summary>
        /// <param name="success"></param>
        private void HandleSendMessageResponse(bool success, string prettyMessage)
        {
            //If send successful close form.
            if (success)
            {
                string dateTimeFormatted = String.Format("{0:u}", DateTime.Now);
                //Remove the "z" from universal time.
                if (dateTimeFormatted.Length > 2)
                    dateTimeFormatted = dateTimeFormatted.Substring(0, dateTimeFormatted.Length - 1);
                //Update form1 immediately with new information.
                Form1.DisplayParsedInformation(CodeCombolist.Text + prettyMessage, Form1.UserName, Form1.UserLocation, Form1.UserTelephone, dateTimeFormatted, (CodeCombolist.SelectedIndex - 1));
                //Tell Form1 this form is closing and close this form.
                Form1.FormSubmitClosed();
                this.Dispose();
            }
            //If result doesn't contain successfully sent information something went wrong.
            else
            {
                SendMessageButton.BackColor = Color.DarkRed;
                SendMessageButton.Enabled = true;
            }
        }

        /// <summary>
        /// Colors the send button.
        /// </summary>
        /// <param name="enabled">True if send can be used.</param>
        private void ColorButton(bool enabled, Button button)
        {
            if (enabled)
            {
                button.BackColor = Color.FromArgb(8, 134, 213);
                button.ForeColor = Color.White;
            }
            else
            {
                button.BackColor = Color.FromArgb(224, 224, 224);
                button.ForeColor = Color.FromArgb(64, 64, 64);
            }

        }

        /// <summary>
        /// Handles Code changes. If a valid code let's user use Send Message button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeCombolist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CodeCombolist.SelectedIndex > 0)
                CanSendMessage = true;
            else
                CanSendMessage = false;

            ColorButton(CanSendMessage, SendMessageButton);
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
    }
}
