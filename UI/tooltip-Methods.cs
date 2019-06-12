using System;
using System.Windows.Forms;

namespace lesson27methodpart1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString());
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(trackBar2, trackBar2.Value.ToString());
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(checkedListBox1.SelectedIndex)
            {
                case 0: 
                    label1.Text = Add(trackBar1.Value,trackBar2.Value).ToString();//Call Add down below
                    break;
                case 1:
                    label1.Text = Divide(trackBar1.Value,trackBar2.Value).ToString();//Call Divide down below
                    break;
            }
        }

        private static double Add(double x, double y) => x + y;

        private static double Divide(double x, double y)
        {
            return (y != 0) ? x / y : 0.0;
        }
    }
}
