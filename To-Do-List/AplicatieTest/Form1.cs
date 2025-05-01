using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicatieTest
{
    public partial class Form1 : Form
    {
        string filepath = "C:/Users/DELL/source/repos/AplicatieTest/AplicatieTest/bin/Debug/task-uri.txt";
        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskText = textBox1.Text.Trim();
            DateTime selectedDate = monthCalendar1.SelectionStart;

            if (!string.IsNullOrEmpty(taskText))
            {
                string formatteddate = selectedDate.ToString("dd.MM.yyyy");
                string taskwithdate = $"{taskText}-{formatteddate}";
                listBox1.Items.Add(taskwithdate);
                textBox1.Clear();
            }

            else MessageBox.Show("Incearca din nou!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else MessageBox.Show("Selecteaza ceva pentru a sterge!");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(filepath))
            {
                string[] tasks = File.ReadAllLines(filepath);
                listBox1.Items.AddRange(tasks);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> savetasks = new List<string>();
            foreach (var item in listBox1.Items)
                savetasks.Add(item.ToString());
            File.WriteAllLines(filepath, savetasks);
        }
    }
}
