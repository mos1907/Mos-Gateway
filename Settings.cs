using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ini;

namespace TCPApp
{
    public partial class Settings : Form
    {
        IniFile ini = new IniFile(Environment.CurrentDirectory + "\\Config.ini");
        public Settings()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("Settings", "NCSNAME", "" + TxtNcsName.Text + "");
            ini.IniWriteValue("Settings", "MOSID", "" + TxtMosId.Text + "");
            ini.IniWriteValue("Settings", "MOSSERVERIP", "" + TxtMosServerIP.Text + "");
            ini.IniWriteValue("Settings", "PATH", "" + TxtPath.Text + "");
            MessageBox.Show("Settings Saved to Config File");

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            TxtNcsName.Text = ini.IniReadValue("Settings", "NCSNAME");
            TxtMosId.Text = ini.IniReadValue("Settings", "MOSID");
            TxtMosServerIP.Text = ini.IniReadValue("Settings", "MOSSERVERIP");
            TxtPath.Text = ini.IniReadValue("Settings", "PATH");
        }
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowse.ShowDialog();
            FolderBrowse.Description = "Select Video Folder";
            FolderBrowse.ShowNewFolderButton = false;
            FolderBrowse.RootFolder = Environment.SpecialFolder.MyComputer;
            if (result == DialogResult.OK)
            {
                TxtPath.Text = FolderBrowse.SelectedPath;
            }
        }
    }
}
