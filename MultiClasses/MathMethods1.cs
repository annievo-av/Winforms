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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lblSum.Text = $"Sum = {MathMethods.Add(numericUpDown1.Value, numericUpDown2.Value)}";
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            lblSum.Text = $"Sum = {MathMethods.Add(numericUpDown1.Value, numericUpDown2.Value)}";
        }
    }
}
