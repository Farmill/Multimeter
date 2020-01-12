using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private int interval = 200;

        Vici8145Lib.Vici8145 lib = new Vici8145Lib.Vici8145();
        public Form1()
        {
            InitializeComponent();
            this.workerDelegate = new WorkerUpdateDelegate(this.UpdateUi);
            workerThread = new Thread(new ThreadStart(this.DoWork));
            workerThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateUi(DisplayData measuredData)
        {
            if (!MeterPanel.Visible)
            {
                MeterPanel.Visible = true;
            }
            
            lblSign.Text = measuredData.Sign;
            lblMainDisplay.Text = measuredData.MainDisplayValue;


            lblSign2nd.Text = measuredData.Sign2nd;
            lblDisplay2nd.Text = measuredData.SecondDisplayValue;

            if (measuredData.Unit == "temp")
             {
                 lblUnitMain.Text = measuredData.Unit;
                 lblUnit2nd.Text = measuredData.Unit2;
             }
             else
             {
                 if (measuredData.Entities == EntitiesEnum.Resistance)
                 {
                     switch (measuredData.Prefixis)
                     {
                         case PrefixEnum.Kilo:
                             lblUnitMain.Text = "k" + measuredData.Unit;
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
             }

             //progressBar1.Value = Math.Min(measuredData.BarValue, 21);
             lblAuto.Text = measuredData.Auto;
             lblHold.Text = measuredData.Hold;
             lblRel.Text = measuredData.Rel ? "REL" : "";
             lblMax.Text = measuredData.MinMax;

        }
        private void DoWork()
        {
            var lib = new Vici8145Lib.Vici8145();
            lib.Openport("COM10");
            while (true)
            {

                DisplayData b = lib.GetData(null, RespondingCommands.MainDisplayValue);
                b = lib.GetData(b, RespondingCommands.SecondDisplayValue);
                b = lib.GetData(b, RespondingCommands.AnalogeBarValue);
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
            workerThread.Abort();
            lib.ClosePort();

        }
    }
}
