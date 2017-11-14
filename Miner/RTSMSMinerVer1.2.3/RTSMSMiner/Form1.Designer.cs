namespace RTSMSMiner
{
    partial class Form1
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
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.CategorizationStatus = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.IntensityStatus = new System.Windows.Forms.PictureBox();
            this.LocationStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CategorizationStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntensityStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(24, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(133, 12);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 1;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 563);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(857, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(108, 41);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(45, 171);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(762, 368);
            this.listBox1.TabIndex = 4;
            // 
            // CategorizationStatus
            // 
            this.CategorizationStatus.BackColor = System.Drawing.Color.Red;
            this.CategorizationStatus.Location = new System.Drawing.Point(179, 64);
            this.CategorizationStatus.Name = "CategorizationStatus";
            this.CategorizationStatus.Size = new System.Drawing.Size(29, 13);
            this.CategorizationStatus.TabIndex = 7;
            this.CategorizationStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Categorization Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Intensity Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Event Classifier Status";
            // 
            // IntensityStatus
            // 
            this.IntensityStatus.BackColor = System.Drawing.Color.Red;
            this.IntensityStatus.Location = new System.Drawing.Point(179, 91);
            this.IntensityStatus.Name = "IntensityStatus";
            this.IntensityStatus.Size = new System.Drawing.Size(29, 13);
            this.IntensityStatus.TabIndex = 11;
            this.IntensityStatus.TabStop = false;
            // 
            // LocationStatus
            // 
            this.LocationStatus.BackColor = System.Drawing.Color.Red;
            this.LocationStatus.Location = new System.Drawing.Point(179, 117);
            this.LocationStatus.Name = "LocationStatus";
            this.LocationStatus.Size = new System.Drawing.Size(29, 13);
            this.LocationStatus.TabIndex = 12;
            this.LocationStatus.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 585);
            this.Controls.Add(this.LocationStatus);
            this.Controls.Add(this.IntensityStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CategorizationStatus);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Name = "Form1";
            this.Text = "RTSMS Miner";
            ((System.ComponentModel.ISupportInitialize)(this.CategorizationStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntensityStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox CategorizationStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox IntensityStatus;
        private System.Windows.Forms.PictureBox LocationStatus;
    }
}

