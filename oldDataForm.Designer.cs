namespace DataLogger
{
    partial class oldDataForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(oldDataForm));
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.channelDisplays = new System.Windows.Forms.ToolStripDropDownButton();
            this.channel1Displays = new System.Windows.Forms.ToolStripMenuItem();
            this.channel2Displays = new System.Windows.Forms.ToolStripMenuItem();
            this.channel3Displays = new System.Windows.Forms.ToolStripMenuItem();
            this.channel4Displays = new System.Windows.Forms.ToolStripMenuItem();
            this.allDisplays = new System.Windows.Forms.ToolStripMenuItem();
            this.timeOfData = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart2
            // 
            this.chart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(4, 28);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Kênh 1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Kênh 2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Kênh 3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Kênh 4";
            this.chart2.Series.Add(series1);
            this.chart2.Series.Add(series2);
            this.chart2.Series.Add(series3);
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(812, 421);
            this.chart2.TabIndex = 0;
            this.chart2.Text = ".";
            this.chart2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseWheels);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelDisplays,
            this.timeOfData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(828, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // channelDisplays
            // 
            this.channelDisplays.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channel1Displays,
            this.channel2Displays,
            this.channel3Displays,
            this.channel4Displays,
            this.allDisplays});
            this.channelDisplays.Image = ((System.Drawing.Image)(resources.GetObject("channelDisplays.Image")));
            this.channelDisplays.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.channelDisplays.Name = "channelDisplays";
            this.channelDisplays.Size = new System.Drawing.Size(106, 22);
            this.channelDisplays.Text = "Kênh hiển thị";
            // 
            // channel1Displays
            // 
            this.channel1Displays.CheckOnClick = true;
            this.channel1Displays.Name = "channel1Displays";
            this.channel1Displays.Size = new System.Drawing.Size(110, 22);
            this.channel1Displays.Text = "Kênh 1";
            this.channel1Displays.CheckStateChanged += new System.EventHandler(this.channel1Displays_CheckStateChanged);
            // 
            // channel2Displays
            // 
            this.channel2Displays.CheckOnClick = true;
            this.channel2Displays.Name = "channel2Displays";
            this.channel2Displays.Size = new System.Drawing.Size(110, 22);
            this.channel2Displays.Text = "Kênh 2";
            this.channel2Displays.CheckStateChanged += new System.EventHandler(this.channel2Displays_CheckStateChanged);
            // 
            // channel3Displays
            // 
            this.channel3Displays.CheckOnClick = true;
            this.channel3Displays.Name = "channel3Displays";
            this.channel3Displays.Size = new System.Drawing.Size(110, 22);
            this.channel3Displays.Text = "Kênh 3";
            this.channel3Displays.CheckStateChanged += new System.EventHandler(this.channel3Displays_CheckStateChanged);
            // 
            // channel4Displays
            // 
            this.channel4Displays.CheckOnClick = true;
            this.channel4Displays.Name = "channel4Displays";
            this.channel4Displays.Size = new System.Drawing.Size(110, 22);
            this.channel4Displays.Text = "Kênh 4";
            this.channel4Displays.CheckStateChanged += new System.EventHandler(this.channel4Displays_CheckStateChanged);
            // 
            // allDisplays
            // 
            this.allDisplays.CheckOnClick = true;
            this.allDisplays.Name = "allDisplays";
            this.allDisplays.Size = new System.Drawing.Size(110, 22);
            this.allDisplays.Text = "Tất cả";
            this.allDisplays.CheckStateChanged += new System.EventHandler(this.allDisplays_CheckStateChanged);
            // 
            // timeOfData
            // 
            this.timeOfData.Image = ((System.Drawing.Image)(resources.GetObject("timeOfData.Image")));
            this.timeOfData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.timeOfData.Name = "timeOfData";
            this.timeOfData.Size = new System.Drawing.Size(114, 22);
            this.timeOfData.Text = "toolStripButton1";
            this.timeOfData.ToolTipText = "Chọn thời gian";
            // 
            // oldDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 472);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.chart2);
            this.Name = "oldDataForm";
            this.Text = "Dữ liệu";
            this.Load += new System.EventHandler(this.oldDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton channelDisplays;
        private System.Windows.Forms.ToolStripMenuItem channel1Displays;
        private System.Windows.Forms.ToolStripMenuItem channel2Displays;
        private System.Windows.Forms.ToolStripMenuItem channel3Displays;
        private System.Windows.Forms.ToolStripMenuItem channel4Displays;
        private System.Windows.Forms.ToolStripMenuItem allDisplays;
        private System.Windows.Forms.ToolStripButton timeOfData;
    }
}