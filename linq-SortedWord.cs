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
        public Form1()
        {
            InitializeComponent();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "";

            // seperate the text by different characters
            var words = richTextBox1.Text.Split(new char[] { ' ', ',', '.', '!' });

            // count length of each word, if >= 5, keep track of that word
            var count = words.Count(word => word.Length >= 5);

            richTextBox2.Text = $"Number of words 5 or more characters: {count}";

            richTextBox2.Text += $"\nSorted words: \n";

            // pick out the corrected-word in words, convert to list then save
            var longWords = words.Where(word => word.Length >= 5).ToList();

            // order from the short, add to list to be able to use `.foreach`
            var longWordsSorted = longWords.OrderBy(word => word.Length).ToList();

            // grap each word in longWordsSorted, and display it to richTextBox2
            longWordsSorted.ForEach(word => richTextBox2.Text += $"{word}\n");
        }
    }
}
