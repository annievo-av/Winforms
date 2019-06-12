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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            double sum = 0;
            double value;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                // TryParse (s, out) return bool as condition
                // convert string to numerical = 0.0
                sum += double.TryParse((dataGridView1[0, i].Value.ToString()), out value) 
                    ? value : 0.0; ;
            }
            label1.Text = $"{sum}";
        }
    }
}
