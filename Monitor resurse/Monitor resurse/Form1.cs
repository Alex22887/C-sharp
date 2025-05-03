using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Monitor_resurse
{
    public partial class Form1 : Form
    {
        PerformanceCounter CPUcounter;
        

        public Form1()
        {
            InitializeComponent();

            cpuChart.Series.Clear();
            cpuChart.ChartAreas[0].AxisX.Title = "Timp";
            cpuChart.ChartAreas[0].AxisY.Title = "Usage %";
            Series cpuSeries = new Series("CPU");
            cpuSeries.ChartType = SeriesChartType.Line;
            cpuSeries.Color = Color.CornflowerBlue;
            cpuSeries.BorderWidth = 2;
            cpuChart.Series.Add(cpuSeries);
            


            CPUcounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            timer1.Interval = 1000;
            timer1.Start();
        }
              private void UpdateDiskInfo()
            {
                
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach(DriveInfo d in allDrives)
                    if(d.IsReady && (d.Name=="C:\\" || d.Name=="D:\\"))
                    {
                        string DriveLabel = $"{d.Name} {d.TotalFreeSpace / (1024 * 1024 * 1024):F1} GB liberi din {d.TotalSize / (1024 * 1024 * 1024)} GB";
                        float UsedDisk = d.TotalSize/(1024*1024*1024)-d.TotalFreeSpace / (1024 * 1024 * 1024);

                        if (d.Name == "C:\\")
                        {
                            label8.Text = DriveLabel;
                            BaraDiskC.Value = (int)UsedDisk;
                        }

                        if (d.Name == "D:\\")
                        {
                            label10.Text = DriveLabel;
                            BaraDiskD.Value = (int)UsedDisk;
                        }
                            
                    }
            label1.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label10.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            label8.Parent = pictureBox1;
            label9.Parent = pictureBox1;
            cpuChart.Parent = pictureBox1;
            cpuChart.ChartAreas[0].AxisX.LineColor = Color.LightGray;
            cpuChart.ChartAreas[0].AxisY.LineColor = Color.LightGray;
            cpuChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            cpuChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            cpuChart.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            cpuChart.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            cpuChart.Series["CPU"].Color = Color.LimeGreen;


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            float cpuValue = CPUcounter.NextValue();
            ComputerInfo info = new ComputerInfo();
            ulong totalMemory = info.TotalPhysicalMemory;
            ulong availableMemory = info.AvailablePhysicalMemory;
            ulong usedMemory = totalMemory - availableMemory;
            int ramUsage = (int)((usedMemory / (double)totalMemory) * 100);

            UpdateDiskInfo();

            BaraCPU.Value = (int)cpuValue;
            BaraRAM.Value = (int)ramUsage;
            cpuChart.Series["CPU"].Points.AddY(cpuValue);
            if (cpuChart.Series["CPU"].Points.Count > 100)
                cpuChart.Series["CPU"].Points.RemoveAt(0);

            label1.Text = $"CPU: {cpuValue:F1}%";
            label3.Text = $"RAM: {ramUsage:F1}%";
        }
    }
}
