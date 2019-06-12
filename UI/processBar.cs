using System;
using System.Windows.Forms;

namespace lesson17forloops
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            for(int i=progressBar1.Minimum;i<=progressBar1.Maximum;i++)
            {
                progressBar1.PerformStep();
                richTextBox1.Text += $"{i}\n";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            progressBar1.Value = 0;
        }
    }
}
