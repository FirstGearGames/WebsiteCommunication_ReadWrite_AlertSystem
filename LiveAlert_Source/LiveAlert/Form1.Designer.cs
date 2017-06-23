namespace LiveAlert
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DismissButton = new System.Windows.Forms.Button();
            this.InformationBackground = new System.Windows.Forms.PictureBox();
            this.HeaderBackground = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.MessageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.InformationTable = new System.Windows.Forms.TableLayoutPanel();
            this.UserLabel = new System.Windows.Forms.Label();
            this.TelephoneLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.ActivationDateLabel = new System.Windows.Forms.Label();
            this.IntervalTimer = new System.Windows.Forms.Timer(this.components);
            this.NotificationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.showAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.InformationTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(516, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(350, 136);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // DismissButton
            // 
            this.DismissButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DismissButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DismissButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.33962F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DismissButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DismissButton.Location = new System.Drawing.Point(663, 538);
            this.DismissButton.Name = "DismissButton";
            this.DismissButton.Size = new System.Drawing.Size(200, 61);
            this.DismissButton.TabIndex = 3;
            this.DismissButton.Text = "Dismiss";
            this.DismissButton.UseVisualStyleBackColor = false;
            this.DismissButton.Click += new System.EventHandler(this.DismissButton_Click);
            // 
            // InformationBackground
            // 
            this.InformationBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.InformationBackground.Location = new System.Drawing.Point(-6, 175);
            this.InformationBackground.Name = "InformationBackground";
            this.InformationBackground.Size = new System.Drawing.Size(1015, 357);
            this.InformationBackground.TabIndex = 4;
            this.InformationBackground.TabStop = false;
            // 
            // HeaderBackground
            // 
            this.HeaderBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HeaderBackground.Location = new System.Drawing.Point(-6, -8);
            this.HeaderBackground.Name = "HeaderBackground";
            this.HeaderBackground.Size = new System.Drawing.Size(1024, 184);
            this.HeaderBackground.TabIndex = 7;
            this.HeaderBackground.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.pictureBox5.Location = new System.Drawing.Point(-6, 524);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(1024, 100);
            this.pictureBox5.TabIndex = 8;
            this.pictureBox5.TabStop = false;
            // 
            // MessageRichTextBox
            // 
            this.MessageRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MessageRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageRichTextBox.ForeColor = System.Drawing.Color.Black;
            this.MessageRichTextBox.Location = new System.Drawing.Point(20, 192);
            this.MessageRichTextBox.Name = "MessageRichTextBox";
            this.MessageRichTextBox.ReadOnly = true;
            this.MessageRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.MessageRichTextBox.Size = new System.Drawing.Size(846, 138);
            this.MessageRichTextBox.TabIndex = 9;
            this.MessageRichTextBox.Text = "SUSPICIOUS OBJECT: Statue resembling angel outside window staring in. Don\'t look " +
    "away, don\'t even blink.";
            // 
            // InformationTable
            // 
            this.InformationTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.InformationTable.ColumnCount = 1;
            this.InformationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InformationTable.Controls.Add(this.UserLabel, 0, 1);
            this.InformationTable.Controls.Add(this.TelephoneLabel, 0, 3);
            this.InformationTable.Controls.Add(this.LocationLabel, 0, 2);
            this.InformationTable.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.InformationTable.ForeColor = System.Drawing.Color.Black;
            this.InformationTable.Location = new System.Drawing.Point(20, 355);
            this.InformationTable.Name = "InformationTable";
            this.InformationTable.RowCount = 4;
            this.InformationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InformationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InformationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InformationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InformationTable.Size = new System.Drawing.Size(846, 161);
            this.InformationTable.TabIndex = 10;
            // 
            // UserLabel
            // 
            this.UserLabel.BackColor = System.Drawing.Color.Transparent;
            this.UserLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UserLabel.Location = new System.Drawing.Point(3, 40);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(840, 40);
            this.UserLabel.TabIndex = 5;
            this.UserLabel.Text = "User: Mark Newbegin";
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TelephoneLabel
            // 
            this.TelephoneLabel.BackColor = System.Drawing.Color.Transparent;
            this.TelephoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TelephoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TelephoneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TelephoneLabel.Location = new System.Drawing.Point(3, 120);
            this.TelephoneLabel.Name = "TelephoneLabel";
            this.TelephoneLabel.Size = new System.Drawing.Size(840, 41);
            this.TelephoneLabel.TabIndex = 3;
            this.TelephoneLabel.Text = "Telephone: 207-555-1234";
            this.TelephoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationLabel
            // 
            this.LocationLabel.BackColor = System.Drawing.Color.Transparent;
            this.LocationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LocationLabel.Location = new System.Drawing.Point(3, 80);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(840, 40);
            this.LocationLabel.TabIndex = 2;
            this.LocationLabel.Text = "Location: Main Building";
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.DateTimeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ActivationDateLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 40);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.DateTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DateTimeLabel.Location = new System.Drawing.Point(426, 0);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(417, 40);
            this.DateTimeLabel.TabIndex = 5;
            this.DateTimeLabel.Text = "2017/06/15 10:24 PM";
            this.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ActivationDateLabel
            // 
            this.ActivationDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.ActivationDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActivationDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivationDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ActivationDateLabel.Location = new System.Drawing.Point(3, 0);
            this.ActivationDateLabel.Name = "ActivationDateLabel";
            this.ActivationDateLabel.Size = new System.Drawing.Size(417, 40);
            this.ActivationDateLabel.TabIndex = 4;
            this.ActivationDateLabel.Text = "Activation By:";
            this.ActivationDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IntervalTimer
            // 
            this.IntervalTimer.Interval = 60000;
            this.IntervalTimer.Tick += new System.EventHandler(this.IntervalTimer_Tick);
            // 
            // NotificationIcon
            // 
            this.NotificationIcon.ContextMenuStrip = this.TrayMenu;
            this.NotificationIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotificationIcon.Icon")));
            this.NotificationIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotificationIcon_MouseClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toolStripMenuItem1,
            this.configurationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.showAlertsToolStripMenuItem});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(163, 88);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem1});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.showToolStripMenuItem.Text = "Exit";
            // 
            // confirmToolStripMenuItem1
            // 
            this.confirmToolStripMenuItem1.Name = "confirmToolStripMenuItem1";
            this.confirmToolStripMenuItem1.Size = new System.Drawing.Size(129, 24);
            this.confirmToolStripMenuItem1.Text = "Confirm";
            this.confirmToolStripMenuItem1.Click += new System.EventHandler(this.confirmToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // showAlertsToolStripMenuItem
            // 
            this.showAlertsToolStripMenuItem.Name = "showAlertsToolStripMenuItem";
            this.showAlertsToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.showAlertsToolStripMenuItem.Text = "Show Alerts";
            this.showAlertsToolStripMenuItem.Click += new System.EventHandler(this.showAlertsToolStripMenuItem_Click);
            // 
            // confirmToolStripMenuItem
            // 
            this.confirmToolStripMenuItem.Name = "confirmToolStripMenuItem";
            this.confirmToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(886, 612);
            this.Controls.Add(this.InformationTable);
            this.Controls.Add(this.MessageRichTextBox);
            this.Controls.Add(this.DismissButton);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.InformationBackground);
            this.Controls.Add(this.HeaderBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 20, 20, 30);
            this.ShowInTaskbar = false;
            this.Text = "Emergency Alert System";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.InformationTable.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.TrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button DismissButton;
        private System.Windows.Forms.PictureBox InformationBackground;
        private System.Windows.Forms.PictureBox HeaderBackground;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.RichTextBox MessageRichTextBox;
        private System.Windows.Forms.TableLayoutPanel InformationTable;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label TelephoneLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.Label ActivationDateLabel;
        private System.Windows.Forms.NotifyIcon NotificationIcon;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.Timer IntervalTimer;

        #endregion
        //private System.Windows.Forms.PictureBox pictureBox3;
    }
}

