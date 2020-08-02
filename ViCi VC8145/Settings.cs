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
            if (result != null)
                        {
                GetColor(progressBar1, result);
            }
            else
            {
                progressBar1.ForeColor = Color.Blue;
            }
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

            var colorSpecs = $"{color.A},{color.B},{color.G},{color.R}";

            switch (settings["Slidercolor"])
            {
                case null:
                    settings.Add("Slidercolor", colorSpecs );
                    break;
                default:
                    settings["Slidercolor"].Value = colorSpecs;
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

        private void GetColor(ProgressBar progressBar, string result)
        {
            var colorspecs = result.Split(',');
            progressBar.ForeColor = Color.FromArgb(Convert.ToInt32(colorspecs[0]),
                Convert.ToInt32(colorspecs[1]), Convert.ToInt32(colorspecs[2]), Convert.ToInt32(colorspecs[3]));
        }
    }
}

