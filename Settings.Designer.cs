namespace TCPApp
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.lblmosID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtNcsName = new System.Windows.Forms.TextBox();
            this.TxtMosId = new System.Windows.Forms.TextBox();
            this.TxtMosServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.FolderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(42, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "NCS NAME :";
            // 
            // lblmosID
            // 
            this.lblmosID.AutoSize = true;
            this.lblmosID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmosID.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblmosID.Location = new System.Drawing.Point(60, 43);
            this.lblmosID.Name = "lblmosID";
            this.lblmosID.Size = new System.Drawing.Size(62, 16);
            this.lblmosID.TabIndex = 1;
            this.lblmosID.Text = "MOS ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "MOS SERVER IP :";
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnSave.Location = new System.Drawing.Point(289, 149);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(123, 28);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "SAVE SETTINGS";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtNcsName
            // 
            this.TxtNcsName.Location = new System.Drawing.Point(139, 9);
            this.TxtNcsName.Name = "TxtNcsName";
            this.TxtNcsName.Size = new System.Drawing.Size(273, 20);
            this.TxtNcsName.TabIndex = 4;
            // 
            // TxtMosId
            // 
            this.TxtMosId.Location = new System.Drawing.Point(139, 42);
            this.TxtMosId.Name = "TxtMosId";
            this.TxtMosId.Size = new System.Drawing.Size(273, 20);
            this.TxtMosId.TabIndex = 5;
            // 
            // TxtMosServerIP
            // 
            this.TxtMosServerIP.Location = new System.Drawing.Point(139, 73);
            this.TxtMosServerIP.Name = "TxtMosServerIP";
            this.TxtMosServerIP.Size = new System.Drawing.Size(273, 20);
            this.TxtMosServerIP.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(30, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Export Path :";
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(139, 107);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(273, 20);
            this.TxtPath.TabIndex = 8;
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnBrowse.Location = new System.Drawing.Point(418, 106);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(71, 23);
            this.BtnBrowse.TabIndex = 9;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(494, 208);
            this.Controls.Add(this.BtnBrowse);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtMosServerIP);
            this.Controls.Add(this.TxtMosId);
            this.Controls.Add(this.TxtNcsName);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblmosID);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblmosID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtNcsName;
        private System.Windows.Forms.TextBox TxtMosId;
        private System.Windows.Forms.TextBox TxtMosServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowse;
    }
}