using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace ViCi_VC8145
{
    public partial class Settings : Form
    {
        private Color color;

        public Settings()
        {
            InitializeComponent();
        }


        private void Settings_Load(object sender, EventArgs e)
        {
            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            string[] theSerialPortNames = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.DataSource = theSerialPortNames;
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["Comport"];
            comboBox1.Text = result;
            result = appSettings["Slidercolor"];
            color =  progressBar1.ForeColor = Color.FromName(result);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comport = comboBox1.Text;

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            switch (settings["Comport"])
            {
                case null:
                    settings.Add("Comport", comport);
                    break;
                default:
                    settings["Comport"].Value = comport;
                    break;
            }

            switch (settings["Slidercolor"])
            {
                case null:
                    settings.Add("Slidercolor", color.Name);
                    break;
                default:
                    settings["Slidercolor"].Value = color.Name;
                    break;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.SolidColorOnly = true;
            colorDialog1.Color = color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                {
                    color = colorDialog1.Color;
                    progressBar1.ForeColor = color;
                }
            }
        }
    }
}

