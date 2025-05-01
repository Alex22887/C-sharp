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

namespace Monitor_resurse
{
    public partial class Form1 : Form
    {
        PerformanceCounter cpucounter;
        

        public Form1()
        {
            InitializeComponent();

            cpucounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            timer1.Interval = 1000;
            timer1.Start();
        }
              private void UpdateDiskInfo()
            {
                
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach(DriveInfo d in allDrives)
                    if(d.IsReady && (d.Name=="C:\\" || d.Name=="D:\\"))
                    {
                        string driveLabel = $"{d.Name} {d.TotalFreeSpace / (1024 * 1024 * 1024):F1} GB liberi din {d.TotalSize / (1024 * 1024 * 1024)} GB";
                        float usedDisk = d.TotalSize/(1024*1024*1024)-d.TotalFreeSpace / (1024 * 1024 * 1024);

                        if (d.Name == "C:\\")
                        {
                            label8.Text = driveLabel;
                            baradiskC.Value = (int)usedDisk;
                        }

                        if (d.Name == "D:\\")
                        {
                            label10.Text = driveLabel;
                            baradiskD.Value = (int)usedDisk;
                        }
                            
                    }
                
            }
        private void timer1_Tick(object sender, EventArgs e)
        {
            float cpuValue = cpucounter.NextValue();
            ComputerInfo info = new ComputerInfo();
            ulong totalMemory = info.TotalPhysicalMemory;
            ulong availableMemory = info.AvailablePhysicalMemory;
            ulong usedMemory = totalMemory - availableMemory;
            int ramUsage = (int)((usedMemory / (double)totalMemory) * 100);

            UpdateDiskInfo();

            baracpu.Value = (int)cpuValue;
            bararam.Value = (int)ramUsage;

            label1.Text = $"CPU: {cpuValue:F1}%";
            label3.Text = $"RAM: {ramUsage:F1}%";
        }
    }
}
