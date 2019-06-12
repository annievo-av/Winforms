using System;
using System.Windows.Forms;

namespace lesson15whileloops
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int i = 1;

            while(i<=5)
            {
                richTextBox1.Text += $"i={i}\n";
                i++;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int i = 5;

            while(i>=1)
            {
                richTextBox1.Text += $"i={i}\n";
                i--;
            }
        }
    }
}
