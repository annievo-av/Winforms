using System;
using System.Text;
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

        private void TextBox1_DoubleClick(object sender, EventArgs e)
        {
            DateTime dt;
            //TryParse used to convert string to DateTime
            string dateFinal = DateTime.TryParse(textBox1.Text, out dt) 
                ? dt.ToLongDateString() : "Cannot convert";
            label1.Text = dateFinal;
        }
    }
}
