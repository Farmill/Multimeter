﻿namespace ViCi_VC8145
{
    partial class Vici
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vici));
            this.MeterPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBarSign = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblHold = new System.Windows.Forms.Label();
            this.lblRel = new System.Windows.Forms.Label();
            this.lblAuto = new System.Windows.Forms.Label();
            this.lblUnit2nd = new System.Windows.Forms.Label();
            this.lblSelect = new System.Windows.Forms.Label();
            this.lblUnitMain = new System.Windows.Forms.Label();
            this.lblDisplay2nd = new System.Windows.Forms.Label();
            this.lblSign2nd = new System.Windows.Forms.Label();
            this.lblMainDisplay = new System.Windows.Forms.Label();
            this.lblSign = new System.Windows.Forms.Label();
            this.BrandModel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MeterPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MeterPanel
            // 
            this.MeterPanel.Controls.Add(this.label1);
            this.MeterPanel.Controls.Add(this.lblBarSign);
            this.MeterPanel.Controls.Add(this.progressBar1);
            this.MeterPanel.Controls.Add(this.lblMax);
            this.MeterPanel.Controls.Add(this.lblHold);
            this.MeterPanel.Controls.Add(this.lblRel);
            this.MeterPanel.Controls.Add(this.lblAuto);
            this.MeterPanel.Controls.Add(this.lblUnit2nd);
            this.MeterPanel.Controls.Add(this.lblSelect);
            this.MeterPanel.Controls.Add(this.lblUnitMain);
            this.MeterPanel.Controls.Add(this.lblDisplay2nd);
            this.MeterPanel.Controls.Add(this.lblSign2nd);
            this.MeterPanel.Controls.Add(this.lblMainDisplay);
            this.MeterPanel.Controls.Add(this.lblSign);
            this.MeterPanel.Controls.Add(this.BrandModel);
            this.MeterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MeterPanel.Location = new System.Drawing.Point(12, 42);
            this.MeterPanel.Name = "MeterPanel";
            this.MeterPanel.Size = new System.Drawing.Size(791, 413);
            this.MeterPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 31);
            this.label1.TabIndex = 14;
            this.label1.Text = "Opening the com port";
            this.label1.Visible = false;
            // 
            // lblBarSign
            // 
            this.lblBarSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarSign.Location = new System.Drawing.Point(19, 296);
            this.lblBarSign.Name = "lblBarSign";
            this.lblBarSign.Size = new System.Drawing.Size(57, 62);
            this.lblBarSign.TabIndex = 13;
            this.lblBarSign.Text = "+";
            this.lblBarSign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.ForeColor = System.Drawing.Color.Blue;
            this.progressBar1.Location = new System.Drawing.Point(97, 327);
            this.progressBar1.MarqueeAnimationSpeed = 1;
            this.progressBar1.Maximum = 21;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(679, 21);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 12;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.ForeColor = System.Drawing.Color.Red;
            this.lblMax.Location = new System.Drawing.Point(309, 66);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(92, 31);
            this.lblMax.TabIndex = 11;
            this.lblMax.Text = "label1";
            // 
            // lblHold
            // 
            this.lblHold.AutoSize = true;
            this.lblHold.ForeColor = System.Drawing.Color.Red;
            this.lblHold.Location = new System.Drawing.Point(113, 66);
            this.lblHold.Name = "lblHold";
            this.lblHold.Size = new System.Drawing.Size(92, 31);
            this.lblHold.TabIndex = 10;
            this.lblHold.Text = "label1";
            // 
            // lblRel
            // 
            this.lblRel.AutoSize = true;
            this.lblRel.ForeColor = System.Drawing.Color.Red;
            this.lblRel.Location = new System.Drawing.Point(211, 66);
            this.lblRel.Name = "lblRel";
            this.lblRel.Size = new System.Drawing.Size(92, 31);
            this.lblRel.TabIndex = 9;
            this.lblRel.Text = "label1";
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.ForeColor = System.Drawing.Color.Red;
            this.lblAuto.Location = new System.Drawing.Point(15, 66);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(92, 31);
            this.lblAuto.TabIndex = 8;
            this.lblAuto.Text = "label1";
            // 
            // lblUnit2nd
            // 
            this.lblUnit2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit2nd.Location = new System.Drawing.Point(622, 117);
            this.lblUnit2nd.Name = "lblUnit2nd";
            this.lblUnit2nd.Size = new System.Drawing.Size(139, 51);
            this.lblUnit2nd.TabIndex = 7;
            this.lblUnit2nd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSelect
            // 
            this.lblSelect.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSelect.Location = new System.Drawing.Point(15, 119);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(171, 45);
            this.lblSelect.TabIndex = 6;
            this.lblSelect.Text = "DC";
            this.lblSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnitMain
            // 
            this.lblUnitMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitMain.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUnitMain.Location = new System.Drawing.Point(471, 198);
            this.lblUnitMain.Name = "lblUnitMain";
            this.lblUnitMain.Size = new System.Drawing.Size(284, 97);
            this.lblUnitMain.TabIndex = 5;
            this.lblUnitMain.Text = "V";
            this.lblUnitMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDisplay2nd
            // 
            this.lblDisplay2nd.Font = new System.Drawing.Font("DSEG7 Classic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay2nd.Location = new System.Drawing.Point(484, 51);
            this.lblDisplay2nd.Name = "lblDisplay2nd";
            this.lblDisplay2nd.Size = new System.Drawing.Size(292, 66);
            this.lblDisplay2nd.TabIndex = 4;
            this.lblDisplay2nd.Text = "0.0000";
            this.lblDisplay2nd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSign2nd
            // 
            this.lblSign2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSign2nd.Location = new System.Drawing.Point(433, 46);
            this.lblSign2nd.Name = "lblSign2nd";
            this.lblSign2nd.Size = new System.Drawing.Size(57, 73);
            this.lblSign2nd.TabIndex = 3;
            this.lblSign2nd.Text = "+";
            this.lblSign2nd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMainDisplay
            // 
            this.lblMainDisplay.Font = new System.Drawing.Font("DSEG7 Classic", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainDisplay.Location = new System.Drawing.Point(70, 179);
            this.lblMainDisplay.Name = "lblMainDisplay";
            this.lblMainDisplay.Size = new System.Drawing.Size(430, 107);
            this.lblMainDisplay.TabIndex = 2;
            this.lblMainDisplay.Text = "0.0000";
            // 
            // lblSign
            // 
            this.lblSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSign.Location = new System.Drawing.Point(3, 164);
            this.lblSign.Name = "lblSign";
            this.lblSign.Size = new System.Drawing.Size(97, 122);
            this.lblSign.TabIndex = 1;
            this.lblSign.Text = "+";
            this.lblSign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrandModel
            // 
            this.BrandModel.Location = new System.Drawing.Point(-18, -15);
            this.BrandModel.Name = "BrandModel";
            this.BrandModel.Size = new System.Drawing.Size(809, 48);
            this.BrandModel.TabIndex = 0;
            this.BrandModel.Text = "ViCi VC8145 companion";
            this.BrandModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.connectToolStripMenuItem.Text = "&Start";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.graphToolStripMenuItem.Text = "&Logger";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // Vici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 467);
            this.Controls.Add(this.MeterPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Vici";
            this.Text = "ViCi VC 8145";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MeterPanel.ResumeLayout(false);
            this.MeterPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MeterPanel;
        private System.Windows.Forms.Label BrandModel;
        private System.Windows.Forms.Label lblMainDisplay;
        private System.Windows.Forms.Label lblSign;
        private System.Windows.Forms.Label lblDisplay2nd;
        private System.Windows.Forms.Label lblSign2nd;
        private System.Windows.Forms.Label lblUnitMain;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Label lblUnit2nd;
        private System.Windows.Forms.Label lblHold;
        private System.Windows.Forms.Label lblRel;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblBarSign;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

