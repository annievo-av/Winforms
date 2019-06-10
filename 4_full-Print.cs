using System;
using System.Windows.Forms;

// need for using Color to change/highlight Background, and Brushes class
using System.Drawing;

// hidden controls:
// menuStrip, saveFileDialog, openFileDialog, fontDialog
// printDocument, printReviewDialog, printDialog

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
    /*
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
    */
    /* 
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void PrintReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // choose Document to be displayed in the preview panel
            printPreviewDialog1.Document = printDocument1;
            // show to preview Dialog
            printPreviewDialog1.ShowDialog();
        }

        // printDocument1 above will show empty white page
        // need to set control `print` for printDocument1
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // runs when a page is about to be printed/ not empty one
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, 
                Brushes.Black, e.MarginBounds);
        }
    */
        // set controls - mostly all to true
        // control data/Document from none to printDocument1
        // more controls, more configs then the print option above
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
