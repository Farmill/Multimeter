using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using VIc8145Lib;
using Vici8145Lib;
using System.Windows.Forms.DataVisualization.Charting;
namespace ViCi_VC8145
{
    public partial class Form1 : Form
    {

        private readonly Thread workerThread;
        private readonly WorkerUpdateDelegate workerDelegate;
        private delegate void WorkerUpdateDelegate(DisplayData d);

        private readonly int interval = 200;

        readonly Vici8145 lib = new Vici8145Lib.Vici8145();
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            this.workerDelegate = this.UpdateUi;
            workerThread = new Thread(this.DoWork);
            workerThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateUi(DisplayData measuredData)
        {

            MeterPanel.Visible = true;
            
            lblSign.Text = measuredData.Sign;
            lblMainDisplay.Text = measuredData.MainDisplayValue;


            lblSign2nd.Text = measuredData.Sign2nd;
            lblDisplay2nd.Text = measuredData.SecondDisplayValue;

            if (measuredData.Entities == EntitiesEnum.Resistance)
            {
                switch (measuredData.Prefixis)
                {
                    case PrefixEnum.Kilo:
                        lblUnitMain.Text = @"k" + measuredData.Unit;
                        break;
                    case PrefixEnum.Mega:
                        lblUnitMain.Text = "M" + measuredData.Unit;
                        break;
                    case PrefixEnum.Micro:
                        lblUnitMain.Text = "μ" + measuredData.Unit;
                        break;
                    case PrefixEnum.Milli:
                        lblUnitMain.Text = "m" + measuredData.Unit;
                        break;
                    case PrefixEnum.Pica:
                        lblUnitMain.Text = "p" + measuredData.Unit;
                        break;
                    default:
                        lblUnitMain.Text = measuredData.Unit;
                        break;
                }

            }

            else
            {
                lblUnitMain.Text = measuredData.Unit;
                lblUnit2nd.Text = measuredData.SecondDisplayValue == "" ? "" : measuredData.Unit1;
            }

            lblSelect.Text = measuredData.Select;
            lblBarSign.Text = measuredData.Sign;
            if (measuredData.ShowBar)
            {
                progressBar1.Visible = true;
                progressBar1.Value = Math.Min(measuredData.BarValue, 21);
                trackBar1.Value = Math.Min(measuredData.BarValue, 21);
            }
            else
            {
                progressBar1.Visible = false;
            }

            lblAuto.Text = measuredData.Auto;
            lblHold.Text = measuredData.Hold;
            lblRel.Text = measuredData.Rel ? "REL" : "";
            lblMax.Text = measuredData.MinMax;

        }
        private void DoWork()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["Comport"];
            if (string.IsNullOrEmpty(result))
            {
                var frm = new Settings();
                frm.Show();  
                result = appSettings["Comport"];
            }
            
            var vcLib = new Vici8145Lib.Vici8145();
            vcLib.Openport(result);
            while (true)
            {

                DisplayData b = vcLib.GetData(null, RespondingCommands.MainDisplayValue);
                b = vcLib.GetData(b, RespondingCommands.SecondDisplayValue);
                b = vcLib.GetData(b, RespondingCommands.AnalogeBarValue);
                if (workerThread.IsAlive)
                {
                    try
                    {
                        Invoke(workerDelegate, b);
                    }
                    catch (Exception e)
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }
                Thread.Sleep(interval);
            }

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            Quit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();

        }

        private void Quit()
        {
            workerThread.Abort();
            lib.ClosePort();
            Application.Exit();
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}

