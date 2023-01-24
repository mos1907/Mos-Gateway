namespace TCPApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtnConnect = new System.Windows.Forms.Button();
            this.TxtLog = new System.Windows.Forms.RichTextBox();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtHeartBeat = new System.Windows.Forms.RichTextBox();
            this.Menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(621, 27);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(75, 23);
            this.BtnConnect.TabIndex = 4;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // TxtLog
            // 
            this.TxtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.TxtLog.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLog.Location = new System.Drawing.Point(6, 22);
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.Size = new System.Drawing.Size(681, 209);
            this.TxtLog.TabIndex = 12;
            this.TxtLog.Text = "";
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.Color.Gray;
            this.Menu.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsMenu,
            this.HelpMenu});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(717, 24);
            this.Menu.TabIndex = 43;
            this.Menu.Text = "menuStrip1";
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(79, 20);
            this.SettingsMenu.Text = "Settings";
            this.SettingsMenu.Click += new System.EventHandler(this.SettingsMenu_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(52, 20);
            this.HelpMenu.Text = "Help";
            this.HelpMenu.Click += new System.EventHandler(this.HelpMenu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtLog);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 237);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rundown Events";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtHeartBeat);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Location = new System.Drawing.Point(12, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(696, 278);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HeartBeat Events";
            // 
            // TxtHeartBeat
            // 
            this.TxtHeartBeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.TxtHeartBeat.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHeartBeat.Location = new System.Drawing.Point(6, 22);
            this.TxtHeartBeat.Name = "TxtHeartBeat";
            this.TxtHeartBeat.Size = new System.Drawing.Size(681, 250);
            this.TxtHeartBeat.TabIndex = 12;
            this.TxtHeartBeat.Text = "";
            this.TxtHeartBeat.TextChanged += new System.EventHandler(this.TxtHeartBeat_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(717, 594);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.BtnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mos Gateway";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.RichTextBox TxtLog;
        public System.Windows.Forms.MenuStrip Menu;
        public System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        public System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox TxtHeartBeat;
    }
}

