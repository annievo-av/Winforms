using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// needed to make a web client obj (download from online)
using System.Net;
// make use of Process.Start
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // declare and set a new client obj
        WebClient client = new WebClient();

        public Form1()
        {
            InitializeComponent();
        }

        // saveDialog used
        private void BtnWithFreezing_Click(object sender, EventArgs e)
        {
            // DownloadFile has effect of freezing the interface while the download occurs
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                client.DownloadFile("https://pixabay.com/en/videos/download/video-5194_source.mp4", saveFileDialog1.FileName);
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            // opened with file selected
            Process.Start("explorer.exe", "/select," + saveFileDialog1.FileName);
        }

        private void BtnNoFreezing_Click(object sender, EventArgs e)
        {
            // DownloadFileAsync has NO effect of freezing the interface while the download occurs
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                client.DownloadFileAsync(new Uri ("https://pixabay.com/en/videos/download/video-5194_source.mp4"), saveFileDialog1.FileName);
        }
    }
}
