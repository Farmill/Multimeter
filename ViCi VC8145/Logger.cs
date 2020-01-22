using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViCi_VC8145
{
    public partial class Logger : Form
    {
        public Logger()
        {
            InitializeComponent();
            lblError.Visible = false;
        }

        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            { 
                txtFile.Text = saveFileDialog1.FileName;
            }
        }

        private void txtInterval_Leave(object sender, EventArgs e)
        {
            if (txtInterval.Text == "" || Convert.ToInt32(txtInterval.Text) < 400)
            {
                lblError.Visible = true;
                txtInterval.Text = @"400";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                listBox1.Items.Add($"Dit is string {i}");
                listBox1.SelectedIndex = i;
            }
        }
    }
}
