using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            foreach (var letter in textBox1.Text)
            {
                if (char.IsLetter(letter))
                {
                    label3.Text += $"{letter}\n";
                }
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            foreach (var letter in textBox1.Text)
            {
                if (char.IsDigit(letter))
                {
                    label4.Text += $"{letter}\n";
                }
            }
        }
    }
}
