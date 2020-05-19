using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using Serial_Oscilloscope;
using System.Text.RegularExpressions;
using DataLogger.Model;

namespace DataLogger
{
    public partial class Form1 : Form
    {
        private float[] channel = new float[6];
        private string asciiBuff = "";
        SerialPort serialPort = new SerialPort();
        private int chart1_axisX_max = 100;
        private int chart1_axisX_min = 0;
        private double chart1_axisY_max = 2;
        private double chart1_axisY_min = 0;
        private int Point_axisX = 0;
        private int Data_Counter = 0;
        private int Count_Draw_max = 1;
        private long ms = 0;
        private CsvFileWriter csvFileWriter = null;
        List<string> stringListDataToWrite = new List<string>();
        private List<float> channel1_list = new List<float>();
        private List<float> channel2_list = new List<float>();
        private List<float> channel3_list = new List<float>();
        private List<float> channel4_list = new List<float>();
        private List<float> operand1st = new List<float>();
        private List<float> operand2st = new List<float>();
        private int Data_Sample = 0;
        private Series Channel1_Series;
        private Series Channel2_Series;
        private Series Channel3_Series;
        private Series Channel4_Series;
        private Series interactive_Series;
        private string _startTime_Excel;
        private static double timeSaveData = 0;
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        public Form1()
        {
            InitializeComponent();
            //sey up chart
            //chart1.ChartAreas[0].AxisX.Maximum = chart1_axisX_max;
            chart1.ChartAreas[0].AxisX.Minimum = chart1_axisX_min;
            //chart1.ChartAreas[0].AxisY.Maximum = chart1_axisY_max;
            //chart1.ChartAreas[0].AxisY.Minimum = chart1_axisY_min;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;

            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorX.Interval = 1;
            chart1.ChartAreas[0].CursorX.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.ScaleView.MinSize = 1;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            chart1.ChartAreas[0].AxisX.Title = "Mẫu";
            chart1.ChartAreas[0].AxisY.Title = "Điện áp (V )";

            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.Interval = 0.001;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Gray;

            Channel1_Series = chart1.Series[0];
            Channel2_Series = chart1.Series[1];
            Channel3_Series = chart1.Series[2];
            Channel4_Series = chart1.Series[3];
            interactive_Series = chart1.Series[4];
            Status_connect.Text = "Thiết bị chưa kết nối";
            Channel_display.Text = "Không có dữ liệu";
            Channel1_Series.Enabled = false;
            Channel2_Series.Enabled = false;
            Channel3_Series.Enabled = false;
            Channel4_Series.Enabled = false;
            interactive_Series.Enabled = false;
            chart1.MouseWheel += Chart1_MouseWheel;
            CheckForIllegalCrossThreadCalls = false;
        }
        
        //chart zoom in zoom out
        private void Chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
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
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
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
                                this.chart1, pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        // com connect
        private void Connect_Click(object sender, EventArgs e)
        {
            if (Connect.Text == "Kết nối")
            {
                try
                {
                    string[] myPort = SerialPort.GetPortNames();
                    serialPort.PortName = myPort[0];
                    serialPort.Encoding = Encoding.GetEncoding(28591);
                    serialPort.BaudRate = 115200;
                    serialPort.Open();
                    serialPort.DataReceived +=
                        new SerialDataReceivedEventHandler(serialPort_DataReceiveHardler); //function for receive data
                    Connect.Text = "Ngắt kết nối";
                    Status_connect.Text = "Thiết bị đang kết nối";
                    Channel_display.Text = "Tất cả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kết nối thiết bị của bạn với máy tính");
                    Status_connect.Text = "Thiết bị chưa kết nối";
                }
            }
            else
            {
                serialPort.Close();
                Connect.Text = "Kết nối";
                Status_connect.Text = "Đã ngắt kết nối";
            }
        }

        //function for receive data
        private void serialPort_DataReceiveHardler(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                int byteToRead = serialPort.BytesToRead;
                byte[] readBuff = new byte[byteToRead];
                serialPort.Read(readBuff, 0, byteToRead);
                foreach (byte b in readBuff)
                {
                    if (asciiBuff.Length > 128)
                    {
                        asciiBuff = "";
                    }

                    if (b != '\r')
                    {
                        asciiBuff += (char) b;
                    }
                    else
                    {
                        if (asciiBuff != "" && asciiBuff != null)
                        {
                            string[] cvsv = (new System.Text.RegularExpressions.Regex(@"[^0-9\-,.]"))
                                .Replace(asciiBuff, "").Split(',');
                            int channelIndex = 0;
                            foreach (string csv in cvsv)
                            {
                                if (csv != "" && channelIndex < 6)
                                {
                                    channel[channelIndex++] = float.Parse(csv);
                                    ;
                                }
                            }

                            asciiBuff = "";
                            if (channelIndex > 0)
                            {
                                GraphDraw();
                                DataSave();
                                Data_Sample = (int) channel[0];
                                channelIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                serialPort.Close();
            }
        }

        //Chart axis X mode
        private int Mode = 0;

        private void ModeAxisX_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in ModeAxisX.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }
        
        private void chếĐộCuộnToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Mode = 1;
        }
        private void chếĐộCốĐịnhToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Mode = 0;
        }
        ////////////////////////////////////////
        
        
        private void loadDataToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            oldDataForm formOldData = new oldDataForm();
            formOldData.ShowDialog();
        }
        //
        int Mode2 = 0;
        private string functionToCalculate = "";
        int type = 0;

        #region interactive

        private void operand1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in operand1.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }

        private void operate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in operate.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in derivateToolStripMenuItem.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            interactive_Series.Enabled = true;
            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }

        private void operand2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in operand2.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }

        private void derivateToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in derivateToolStripMenuItem.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in operate.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            interactive_Series.Enabled = true;
            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }

        private int a = 0;
        private int b = 0;
        private int c = 0;
        private void InteractiveO1Channel1_CheckStateChanged(object sender, EventArgs e) => a = 1;

        private void InteractiveO1Channel2_CheckStateChanged(object sender, EventArgs e) => a = 2;

        private void InteractiveO1Channel3_CheckStateChanged(object sender, EventArgs e) => a = 3;

        private void kênh4ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) => a = 4;

        private void InteractiveO2Channel1_CheckStateChanged(object sender, EventArgs e) => b = 1;

        private void InteractiveO2Channel2_CheckStateChanged(object sender, EventArgs e) => b = 2;

        private void InteractiveO2Channel3_CheckStateChanged(object sender, EventArgs e) => b = 3;

        private void InteractiveO2Channel4_CheckStateChanged(object sender, EventArgs e) => b = 4;

        private void plus_CheckStateChanged(object sender, EventArgs e) => c = 0;

        private void SubToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) => c = 1;

        private void multiplication_CheckStateChanged(object sender, EventArgs e) => c = 2;

        private void Channel1_derivativeToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) => c = 3;

        private void Channel2_derivativeToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) => c = 4;

        private void Channel3_derivativeToolStripMenuItem_CheckStateChanged(object sender, EventArgs e) => c = 5;

        private void Channel4_derivativeToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e) => c = 6;

        #endregion

        //draw chart
        private float max1 = 0;
        private float max2 = 0;
        private float max3 = 0;
        private float maxAxisY(float max, float a, float b, float c, float d,float e)
        {
            max = Math.Max(max, a);
            max = Math.Max(a, max);
            max = Math.Max(b, max);
            max = Math.Max(c, max);
            max = Math.Max(d, max);
            return Math.Max(e,max);
        }
        private List<int> maxY = new List<int>();
        float max = 0;
        private void GraphDraw()
        {
            float x = 0;

            Data_Counter++;
            channel1_list.Add(channel[5]);
            channel2_list.Add(channel[4]);
            channel3_list.Add(channel[3]);
            channel4_list.Add(channel[2]);
            channel1List.Add(channel[5]);
            channel2List.Add(channel[4]);
            channel3List.Add(channel[3]);
            channel4List.Add(channel[2]);
            
            if (Data_Counter == Count_Draw_max)
            {
                
                Data_Counter = 0;
                chart1.ChartAreas[0].AxisX.Maximum = chart1_axisX_max;
                chart1.ChartAreas[0].AxisY.Maximum = chart1_axisY_max;
                chart1.ChartAreas[0].AxisY.Minimum = chart1_axisY_min;
                BeginInvoke((Action) (() =>
                {
                    for (int i = 0; i < channel1_list.Count; i++)
                    {
                        Point_axisX++;
                        
                        
                        if (a == 1)
                        {
                            channelX = channel[5];
                        }

                        if (a == 2)
                        {
                            channelX = channel[4];
                        }

                        if (a == 3)
                        {
                            channelX = channel[3];
                        }

                        if (a == 4)
                        {
                            channelX = channel[2];
                        }

                        if (b == 1)
                        {
                            channelY = channel[5];
                        }

                        if (b == 2)
                        {
                            channelY = channel[4];
                        }

                        if (b == 3)
                        {
                            channelY = channel[3];
                        }

                        if (b == 4)
                        {
                            channelY = channel[2];
                        }

                        if (c == 0)
                        {
                            x = channelX + channelY;
                        }

                        else if (c == 1)
                        {
                            x = channelX - channelY;
                        }

                        else if (c == 2)
                        {
                            x = channelX * channelY;
                        }

                        else if (c == 3)
                        {
                            x = (channel1List[i + 1] - channel1List[i]) * channel[0];
                        }

                        else if (c == 4)
                        {
                            x = (channel2List[i + 1] - channel2List[i]) * channel[0];
                        }

                        else if (c == 5)
                        {
                            x = (channel3List[i + 1] - channel3List[i]) * channel[0];
                        }

                        else if (c == 6)
                        {
                            x = (channel4List[i + 1] - channel4List[i]) * channel[0];
                        }
                        else{ 
                            x = 0;
                            
                        }
                        max = maxAxisY(max, channel1_list[i], channel2_list[i], channel3_list[i], channel4_list[i], x);
                        
                        Channel1_Series.Points.AddXY(Point_axisX, channel1_list[i]);
                        Channel2_Series.Points.AddXY(Point_axisX, channel2_list[i]);
                        Channel3_Series.Points.AddXY(Point_axisX, channel3_list[i]);
                        Channel4_Series.Points.AddXY(Point_axisX, channel4_list[i]);
                        chart1.Series[4].Points.AddXY(Point_axisX, x);
                        //Channel1_Series.CustomProperties = "IsXAxisQuantitative=True";
                        chart1.ChartAreas[0].AxisY.Maximum = (int)max + 1;
                        chart1.ChartAreas[0].AxisY.Minimum = -(int)max - 1;
                        if (Mode == 1)
                        {
                            if (Point_axisX == chart1_axisX_max)
                            {
                                chart1_axisX_max = chart1_axisX_max + 100;
                            }

                            chart1.ChartAreas[0].AxisX.Maximum = chart1_axisX_max;
                        }
                        
                        if (Mode == 0)
                        {
                            //chart1.ChartAreas[0].AxisX.Maximum = chart1_axisX_max;
                            if (Point_axisX > chart1_axisX_max)
                            {
                                chart1_axisX_max = 100;
                                chart1_axisY_max=2;
                                Point_axisX = 0;
                                Channel1_Series.Points.Clear();
                                Channel2_Series.Points.Clear();
                                Channel3_Series.Points.Clear();
                                Channel4_Series.Points.Clear();
                                chart1.Series[4].Points.Clear();
                            }
                            chart1.ChartAreas[0].RecalculateAxesScale();
                        }
                    }

                    channel1_list.Clear();
                    channel2_list.Clear();
                    channel3_list.Clear();
                    channel4_list.Clear();
                    channel1List.RemoveRange(0, channel1List.Count - 2);
                    channel2List.RemoveRange(0, channel2List.Count - 2);
                    channel3List.RemoveRange(0, channel3List.Count - 2);
                    channel4List.RemoveRange(0, channel4List.Count - 2);
                }));
            }
        }
        
        private int Data_Counter2 = 0;
        private int Point_axisX2 = 0;
        private float channelX = 0;
        private float channelY = 0;
        private List<float> channel1List = new List<float>();
        private List<float> channel2List = new List<float>();
        private List<float> channel3List = new List<float>();
        private List<float> channel4List = new List<float>();

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time_now.Text = DateTime.Now.ToString();
            Data_Rate.Text = Data_Sample.ToString() + " mẫu/s";
        }

        //close form
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.Close();
        }

        #region select channel to display

        private void allToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (allToolStripMenuItem.Checked)
            {
                channel1ToolStripMenuItem.Checked = false;
                channel2ToolStripMenuItem.Checked = false;
                channel3ToolStripMenuItem.Checked = false;
                channel4ToolStripMenuItem.Checked = false;
                interactive_displays_chartToolStripMenuItem.Checked = false;
                Channel1_Series.Enabled = true;
                Channel2_Series.Enabled = true;
                Channel3_Series.Enabled = true;
                Channel4_Series.Enabled = true;
                interactive_Series.Enabled = true;
            }
            else
            {
                Channel1_Series.Enabled = false;
                Channel2_Series.Enabled = false;
                Channel3_Series.Enabled = false;
                Channel4_Series.Enabled = false;
                interactive_Series.Enabled = false;
            }
        }

        private void channel1ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (channel1ToolStripMenuItem.Checked)
            {
                allToolStripMenuItem.Checked = false;
                Channel1_Series.Enabled = true;
            }
            else
            {
                Channel1_Series.Enabled = false;
            }
        }

        private void channel2ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (channel2ToolStripMenuItem.Checked)
            {
                allToolStripMenuItem.Checked = false;
                Channel2_Series.Enabled = true;
            }
            else
            {
                Channel2_Series.Enabled = false;
            }
        }

        private void channel3ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (channel3ToolStripMenuItem.Checked)
            {
                allToolStripMenuItem.Checked = false;
                Channel3_Series.Enabled = true;
            }
            else
            {
                Channel3_Series.Enabled = false;
            }
        }

        private void channel4ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (channel4ToolStripMenuItem.Checked)
            {
                allToolStripMenuItem.Checked = false;
                Channel4_Series.Enabled = true;
            }
            else
            {
                Channel4_Series.Enabled = false;
            }
        }

        private void interactive_displays_chartToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (interactive_displays_chartToolStripMenuItem.Checked)
            {
                allToolStripMenuItem.Checked = false;
                interactive_Series.Enabled = true;
            }
            else
            {
                interactive_Series.Enabled = false;
            }
        }

        #endregion

        #region select time to save data

        private void DropDown_TimeToSaveData_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ToolStripMenuItem) e.ClickedItem) == OrtherToolStripMenuItem)
            {
                GetValueToSaveData getValueToSaveData = new GetValueToSaveData();
                getValueToSaveData.ShowDialog();
                timeSaveData = getValueToSaveData.timeSave;
                ((ToolStripMenuItem) e.ClickedItem).Text = getValueToSaveData.value;
                ((ToolStripMenuItem) e.ClickedItem).Checked = false;
            }

            if (((ToolStripMenuItem) e.ClickedItem).Checked)
            {
                return;
            }

            foreach (ToolStripMenuItem toolStripMenuItem in DropDown_TimeToSaveData.DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }

            ((ToolStripMenuItem) e.ClickedItem).Checked = true;
        }

        //function for structure of name file and index of data in excel
        private void structure()
        {
            string fileName = "";
            //string subString_FilePath = "";

            string[] timeToNameFile = new string[5];
            timeToNameFile[0] = DateTime.Now.Day.ToString();
            timeToNameFile[1] = "_" + DateTime.Now.Month.ToString();
            timeToNameFile[2] = "_" + DateTime.Now.Year.ToString();
            timeToNameFile[3] = "_" + DateTime.Now.Hour.ToString();
            timeToNameFile[4] = "_" + DateTime.Now.Minute.ToString();

            fileName = String.Concat(timeToNameFile);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select File Location";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.FileName = String.Concat(fileName, "_DataLogger");

            _startTime_Excel = DateTime.Now.ToString("HH:mm:ss");
            //subString_FilePath = String.Concat("E:\\", fileName, "_DataLogger", ".csv");
            string fileFormat = "Time, Channel 1, Channel 2, Channel 3, Channel 4";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvFileWriter = new CsvFileWriter(saveFileDialog.FileName.ToString(), _startTime_Excel, fileFormat);
            }
        }

        private double timeremain;

        private double time;

        //time save data :30 second
        private void TwentySecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * 30;
        }

        //time save data :1 minustes
        private void MinutesToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * 60;
        }

        //time save data :5 minutes
        private void FiveMinutesToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * 300;
        }

        //time save data :30 minutes
        private void TwentyMinutesToolStripMenuItem2_CheckStateChanged(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * 1800;
        }

        //time save data :1 hour
        private void HourToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * 3600;
            

        }
        //time save data : optional

        private void OrtherToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            structure();
            timeremain = Data_Sample * timeSaveData;
            //TimeSpan timeRemainToSaveData = TimeSpan.FromSeconds(timeSaveData);
            //string time1remain = timeRemainToSaveData.ToString(@"dd\:hh\:mm\:ss");

            //realDateTime = DateTime.Now;
            //preDateTime = realDateTime + timeRemainToSaveData;
            //MessageBox.Show(time1remain);
            //MessageBox.Show(preDateTime.ToShortTimeString());
            // MessageBox.Show(preDateTime.ToLongDateString());
        }

        //function data save
        private DateTime realDateTime;
        private DateTime preDateTime;
        private int n = 0;

        private void DataSave()
        {
            if (csvFileWriter != null)
            {
                ms++;
                if (timeremain == 0)
                {
                    csvFileWriter.CloseFile();
                    csvFileWriter = null;
                    MessageBox.Show("Chưa lưu");
                }
                string stringDataToWrite = String.Concat(DateTime.Now.Second.ToString(), "," + channel[5].ToString(),
                    "," + channel[4].ToString(), "," + channel[3].ToString(), "," + channel[2].ToString());
                csvFileWriter.WriteCSVline(stringListDataToWrite);
                stringListDataToWrite.Clear();
                stringListDataToWrite.Add(stringDataToWrite);
                if (ms == timeremain)
                {
                    csvFileWriter.CloseFile();
                    csvFileWriter = null;
                    MessageBox.Show("Dữ liệu đã được lưu");
                }
            }
            #endregion
        }
    }
}
