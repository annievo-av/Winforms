using System;
using System.Windows.Forms;

// need for using Color to change/highlight Background
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    
    /*
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    */
        // TxtOld_DoubleClick should be ENTER action !?!!??
        private void TxtOld_DoubleClick(object sender, EventArgs e)
        {
            // make sure there is an entry in the text box under Edit
            if (txtOld.Text.Trim().Length > 0)
            {
                // search for the location of txtOld.Text in richTextBox1
                int start = richTextBox1.Find(txtOld.Text);

                // Select the foud text in the richTextBox1
                richTextBox1.Select(start, txtOld.Text.Length);

                // color the found text
                richTextBox1.SelectionBackColor = Color.Yellow;
            }
        }
    
        private void TxtNew_DoubleClick(object sender, EventArgs e)
        {
            // make sure there is an entry in the box
            if (txtNew.Text != "")
            {
                richTextBox1.Text = richTextBox1.Text.Replace(txtOld.Text, txtNew.Text);
            }
        }
    }
}
