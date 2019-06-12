using System;
using System.Windows.Forms;
// allows using Directory class => get all files
using System.IO;

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

        private void TxtFilePath_MouseClick(object sender, MouseEventArgs e)
        {
            // DialogResult.OK prevents the path still empty if users cancel
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
                btnListFiles.Enabled = true;
            }
        }

        private void BtnListFiles_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Text += $"{folderBrowserDialog1.SelectedPath}";
            string[] files = Directory.GetFiles(txtFilePath.Text);
            foreach (var file in files)
            {
                richTextBox1.Text += $"{file}\n";
            }
        }
    }
}
