using System;
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

        private void BtnSummarize_Click(object sender, EventArgs e)
        {
            decimal[] values = {numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value};

            decimal max, min, avg, sum;

            MathMethods.Summarize(values, out max, out min, out avg, out sum);
            lblSummary.Text = $"Sum: {sum}\n";
            lblSummary.Text += $"Min: {min}\n";
            lblSummary.Text += $"Max: {max}\n";
            lblSummary.Text += $"Average: {avg}";
        }
    }
}
