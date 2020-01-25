using System;
using System.Windows.Forms;

namespace ViCi_VC8145
{
    public partial class ErrorMesg : Form
    {
        public string Mesg
        {
            set { lblMesg.Text = value; }
        }

        public ErrorMesg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
