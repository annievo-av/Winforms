using System;
using System.Linq;
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

        private void TextBox1_Click(object sender, EventArgs e)
        {
            //Split() w params - specified a flexible to enter any amout of []
            var values = textBox1.Text.Split(new char[] { ',', ';', ':' }, 
                StringSplitOptions.RemoveEmptyEntries);

            //convert each entry in the values array from a string to decimal
            var valuesNumeric = Array.ConvertAll(values, decimal.Parse);

            label1.Text = $"{valuesNumeric.Sum()}";
        }
    }
}
