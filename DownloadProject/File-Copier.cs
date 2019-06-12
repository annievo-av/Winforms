using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// anything to do with Files as copy
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // btn, checkBox, checkListBox, progressBar, lbl, 
        // openFileDialog and FolderBrowserDialog are used
        private void BtnChooseFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                    checkedListBox1.Items.Add(openFileDialog1.FileNames[i]);

                chkAll.Enabled = true;
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            // restore the progress bar between the copy operation
            // properties: min = 1, step = 1
            progressBar1.Value = progressBar1.Minimum;

            progressBar1.Maximum = checkedListBox1.CheckedItems.Count;

            // grab each item in the list
            foreach(var item in checkedListBox1.CheckedItems)
            {
                progressBar1.PerformStep();

                // Path.GetFileName gets the file name only
                // Path.Combine creates a new path where the file is saved
                // true at the end to override
                File.Copy(item.ToString(),
                    Path.Combine(folderBrowserDialog1.SelectedPath,
                    Path.GetFileName(item.ToString())), true);
            }
        }

        private void BtnChooseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = folderBrowserDialog1.SelectedPath;
                btnCopy.Enabled = true;
            }
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }
    }
}
