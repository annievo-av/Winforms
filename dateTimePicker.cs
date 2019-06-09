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
        DateTime dt = DateTime.Parse("11/25/2016");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.Date < dt)
            {
                label1.Text = "There is no discount right now";
            }
            else if (dateTimePicker1.Value.Date >= dt)
            {
                label1.Text = "There is discount right now";
            }
        }
    }
}
