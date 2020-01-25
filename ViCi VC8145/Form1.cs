using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VIc8145Lib;
using Vici8145Lib;

namespace ViCi_VC8145
{
    public partial class Form1 : Form
    {

        private Thread workerThread;
        private WorkerUpdateDelegate workerDelegate;
        private delegate void WorkerUpdateDelegate(DisplayData d);
        public static int LogInterval = 400;
        internal static string LogFilename = null;
        private DateTime toWriteTime;
        readonly Vici8145 lib = new Vici8145();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateUi(DisplayData measuredData)
        {

            MeterPanel.Visible = true;

            lblSign.Text        = measuredData.Sign;
            lblMainDisplay.Text = measuredData.MainDisplayValue;
            lblSign2nd.Text     = measuredData.Sign2Nd;
            lblDisplay2nd.Text  = measuredData.SecondDisplayValue;
            lblUnitMain.Text    = measuredData.Unit;
            lblUnit2nd.Text     = measuredData.SecondDisplayValue == "" ? "" : measuredData.Unit1;
            lblSelect.Text      = measuredData.Select;
            lblBarSign.Text     = measuredData.Sign;

            if (measuredData.ShowBar)
            {
                progressBar1.Visible = true;
                progressBar1.Value = Math.Min(measuredData.BarValue, 21);
            }
            else
            {
                progressBar1.Visible = false;
            }

            lblAuto.Text = measuredData.Auto;
            lblHold.Text = measuredData.Hold;
            lblRel.Text  = measuredData.Rel ? "REL" : "";
            lblMax.Text  = measuredData.MinMax;

        }
        private void DoWork()
        {
            StreamWriter writer = null;
            var appSettings = ConfigurationManager.AppSettings;
            string result   = appSettings["Comport"];

            if (string.IsNullOrEmpty(result))
            {
                var frm = new Settings();
                frm.Show();
                result = appSettings["Comport"];
            }

            TimeSpan ts = new TimeSpan(0, 0, 0, LogInterval / 1000, LogInterval % 1000);


            toWriteTime = DateTime.Now;
            if (LogFilename != null)
            {
                writer = new StreamWriter(LogFilename);
            }

            var vcLib = new Vici8145();

            try
            {
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
                            WriteDataToFile(writer, b, ts);
                        }
                        catch (Exception)
                        {
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                writer?.Close();

                if (!ex.Message.Contains("abort"))
                {
                    ErrorMesg mesg = new ErrorMesg { Mesg = ex.Message };
                    mesg.ShowDialog();
                }
            }


        }

        private void WriteDataToFile(StreamWriter writer, DisplayData b, TimeSpan ts)
        {
            if (toWriteTime <= DateTime.Now)
            {
                if (writer != null)
                {
                    writer.WriteLine($"{DateTime.Now:HH:mm:ss.ffff},{b.Sign}{b.MainDisplayValue},{b.Unit1},{b.Sign2Nd}{b.SecondDisplayValue},{b.Unit2}");
                    writer.Flush();
                }

                toWriteTime = DateTime.Now + ts;
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Quit()
        {
            workerThread?.Abort();

            lib.ClosePort();
            Application.Exit();
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger logform = new Logger();
            logform.ShowDialog();

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings form = new Settings();
            form.Show();

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                var menuitem = (ToolStripMenuItem)sender;
                if (menuitem != null)
                {
                    if (menuitem.Text.Contains("Start"))
                    {
                        menuitem.Text = @"&Stop";
                        panel1.Visible = false;
                        this.workerDelegate = this.UpdateUi;
                        workerThread = new Thread(this.DoWork);
                        workerThread.Start();
                    }
                    else
                    {
                        workerThread?.Abort();
                        lib.ClosePort();
                        menuitem.Text = @"&Start";
                    }

                }
                else
                {
                    throw new Exception("Menu error");
                }

            }
            catch (Exception ex)

            {
                ErrorMesg mesg = new ErrorMesg {Mesg = ex.Message};
                mesg.ShowDialog();
            }
        }
    }
}

