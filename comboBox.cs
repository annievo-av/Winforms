using System;
using System.Windows.Forms;

namespace lesson20ternaryoperator1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtAmount_DoubleClick(object sender, EventArgs e)
        {
            lblAmountFormatted.Text = $"Amount:{decimal.Parse(txtAmount.Text):C}";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(txtAmount.Text);//read value from box and save in decimal form
            decimal finalAmount = (comboBox1.SelectedIndex == 0) ? amount * 0.9M : amount * 1.1M;
            lblFinalAmount.Text = $"Final Amount:{finalAmount:C}";
        }
    }
}
