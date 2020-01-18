using System;
using System.Configuration;
using System.Windows.Forms;
using System.Configuration;

namespace ViCi_VC8145
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string[] theSerialPortNames = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.DataSource = theSerialPortNames;
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["Comport"];
            comboBox1.Text = result;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comport = comboBox1.Text;

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["Comport"] == null)
            {
                settings.Add("Comport", comport);
            }
            else
            {
                settings["Comport"].Value = comport;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            this.Hide();
        }
    }
}

