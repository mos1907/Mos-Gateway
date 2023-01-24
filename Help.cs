using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPApp
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:murat@muratdemirci.com.tr");
        }
    }
}
