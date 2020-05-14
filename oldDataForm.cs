using DataLogger.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataLogger
{
    public partial class oldDataForm : Form
    {
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        private readonly static string CHANEL1 = "Chanel 1";
        private readonly static string CHANEL2 = "Chanel 2";
        private readonly static string CHANEL3 = "Chanel 3";
        private readonly static string CHANEL4 = "Chanel 4";

        private List<string> chanels = new List<string>() { CHANEL1, CHANEL2, CHANEL3, CHANEL4 };

        private List<OldData> oldDatas = new List<OldData>();

        public oldDataForm()
        {
            InitializeComponent();
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart2.ChartAreas[0].CursorX.AutoScroll = true;
            chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].CursorX.Interval = 1;
            chart2.ChartAreas[0].CursorX.LineColor = Color.Gray;
            chart2.ChartAreas[0].AxisX.ScaleView.MinSize = 1;
            chart2.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            chart2.ChartAreas[0].AxisX.Title = "Mẫu";

            chart2.ChartAreas[0].AxisY.Title = "Điện áp (V )";
            chart2.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart2.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
            chart2.ChartAreas[0].CursorY.AutoScroll = true;
            chart2.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].CursorY.Interval = 0.001;
            chart2.ChartAreas[0].CursorY.LineColor = Color.Gray;
            chart2.MouseWheel += chart1_MouseWheels;
            CheckForIllegalCrossThreadCalls = false;
            
        }
        private void chart1_MouseWheels(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chart2.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart2.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }
                //if (e.Delta > 0)
                //{
                //    double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                //    double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                //    double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                //    double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                //    double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                //    double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                //    double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                //    double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                //    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                //    chart1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                //}
            }
            catch
            {
            }

            // mouseMove
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart2.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        var channel = result.Series.ToString();
                        var y_value = Math.Round(prop.YValues[0], 3);
                        if (Math.Abs(pos.X - pointXPixel) < 2 &&
                            Math.Abs(pos.Y - pointYPixel) < 2)
                        {
                            tooltip.Show(
                                channel.Remove(0, 7) + ", Điểm :" + prop.XValue + ", Giá trị điện áp:" + y_value,
                                this.chart2, pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }
        private readonly string _fullPath = "..\\..\\Files\\8_5_2020_7_6_DataLogger.csv";
       
        private void oldDataForm_Load(object sender, EventArgs e)
        {
            readCSV();
            
            chart2.Series.Clear();

            chanels.ForEach(c =>
            {
                chart2.Series.Add(c);
                chart2.Series[c].ChartType = SeriesChartType.Line;
                chart2.Series[c].Color = c == CHANEL1 ? Color.Red : c == CHANEL2 ? Color.Green : c == CHANEL3 ? Color.Blue : Color.Gray;
                chart2.Series[c].IsVisibleInLegend = false;
            });
        }

        private void readCSV()
        {
            OpenFileDialog getFileDialog = new OpenFileDialog();
            
            getFileDialog.Filter = "CSV files (*.csv)|*.csv";
            getFileDialog.Title = "Select File Location";
            getFileDialog.ShowDialog();
            List<string[]> rows = File.ReadAllLines(getFileDialog.FileName).Select(x => x.Split(',')).ToList();

            rows.RemoveAt(0);

            for(int i = 0; i < rows.Count(); i++)
            {
                for(int j = 1; j < rows[i].Length; j ++)
                {
                    oldDatas.Add(new OldData()
                    {
                        Ms = int.Parse(rows[i][0]),
                        Chanel = chanels[j - 1],
                        Value = float.Parse(rows[i][j])
                    });
                }
            }
        }

        private void draw(string chanel = "", bool all = false)
        {
            if(!all && chanel != "")
            {
                var data = oldDatas.Where(d => d.Chanel == chanel).ToList();
                data.ForEach(d =>
                {
                    
                    chart2.Series[d.Chanel].Points.AddXY(d.Ms, d.Value);
                });
            } 
            else {
                oldDatas.ForEach(d =>
                {
                    
                    chart2.Series[d.Chanel].Points.AddXY(d.Ms, d.Value);
                });
            }

        }

        private void channel1Displays_CheckStateChanged(object sender, EventArgs e)
        {
            allDisplays.Checked = false;
            draw(CHANEL1);
        }

        private void channel2Displays_CheckStateChanged(object sender, EventArgs e)
        {
            allDisplays.Checked = false;
            draw(CHANEL2);
        }

        private void channel3Displays_CheckStateChanged(object sender, EventArgs e)
        {
            allDisplays.Checked = false;
            draw(CHANEL3);
        }

        private void channel4Displays_CheckStateChanged(object sender, EventArgs e)
        {
            allDisplays.Checked = false;
            draw(CHANEL4);
        }

        private void allDisplays_CheckStateChanged(object sender, EventArgs e)
        {
            channel1Displays.Checked = false;
            channel2Displays.Checked = false;
            channel3Displays.Checked = false;
            channel4Displays.Checked = false;
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.Series[3].Points.Clear();
            draw("", true);
        }

        
    }
}
