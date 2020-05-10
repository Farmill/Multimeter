using System;
using System.Windows.Forms;

namespace ViCi_VC8145
{
    public partial class Logger : Form
    {
        public Logger()
        {
            InitializeComponent();
            lblError.Visible = false;
            txtInterval.Text = @"1000";
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

            Vici.LogInterval = Convert.ToInt32(txtInterval.Text);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtFile.Text != "")
            {
                Vici.LogFilename = txtFile.Text;
            }
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Vici.LogFilename = "";
        } 
    }
}
