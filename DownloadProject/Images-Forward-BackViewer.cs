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
        // used for storing position of image list
        // set equal to 0 at first because of the image list
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // treeView control used with NodeMouseDoubleClick function
        // pictureBox control is used (sizeMode: StretchImage)
        // openFile used, remember to delete FileName in properties, filter if needed
        // imageList control used, Dept32Bit and size of 255, 255 min
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // clear the list whenever the new year picked
            imageList1.Images.Clear();

            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // grab each image from the open file dialog array of images
                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    imageList1.Images.Add(Image.FromFile(openFileDialog1.FileNames[i]));
                }
                // pictureBox displays an image after the Dialog box closed
                pictureBox1.Image = imageList1.Images[currentIndex];
            }
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            // make sure that you don't step beyond the last index
            // && make sure that some images have actually added
            if (currentIndex != imageList1.Images.Count - 1 && imageList1.Images.Count > 0)
            {
                pictureBox1.Image = imageList1.Images[++currentIndex];
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (currentIndex != 0)
            {
                pictureBox1.Image = imageList1.Images[--currentIndex];
            }
        }
    }
}
