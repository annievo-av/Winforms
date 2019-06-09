using System;
using System.Windows.Forms;

namespace lesson23operatorsthatevaluate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //+=2 adds 2 to the current value of trackBar1.Value 
            trackBar1.Value += 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //take the current value of the trackbar
            //divide it by 2
            //store that new, updated value back
            trackBar1.Value /= 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //grab the current value of trackBar1.Value
            //subtract 3 from it
            //store back the updated value
            trackBar1.Value -= 3;
        }
    }
}
