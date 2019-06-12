using System;
using System.Collections.Generic;
using System.ComponentModel;

// SqlDataAdapter, and DataTable
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// needed for File use
using System.IO;

namespace WindowsFormsApp1
{
    public partial class BizContacts : Form
    {
        // connection string - right click to SQL db
        string connString = @"Data Source=DESKTOP-AV19\SQLEXPRESS;Initial Catalog=AddressBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        // dataAdapter - build connection between the program and the database
        SqlDataAdapter dataAdapter;
        
        // table - hold the information so we can fill datagrid view
        DataTable table;

        // declare a new commandBuilder obj
        SqlCommandBuilder commandBuilder;

        // because using it in multiple places, so leave it here
        SqlConnection conn;

        string selectionStatement = "select * from BizContacts";

        public BizContacts()
        {
            InitializeComponent();
        }

        private void BizContacts_Load(object sender, EventArgs e)
        {
            // display the first option as default
            cboSearch.SelectedIndex = 0;

            // set the source of the data to be displayed in the grid view
            dataGridView1.DataSource = bindingSource1;

            // method GetData() called with
            // the argument is a string represent an sql query
            // select ALL from BizContacts TABLE
            GetData(selectionStatement);
        }

        // GetData be called as the first/last thing to bind 2 sources
        private void GetData(string selectCommand) { }

        private void BtnAdd_Click(object sender, EventArgs e) 
        { 
            // ...
            string insert = @"..., Image) 
                    values(..., @Image)";
            
            // inside try-catch
                // check whether file name is empty
                if(openFileDialog1.FileName!="")
                {
                    // convert image to bytes for saving to sql server
                    command.Parameters.AddWithValue(@"Image",
                        File.ReadAllBytes(openFileDialog1.FileName));
                }
                else
                {
                    // save null to database
                    command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
                }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) { }
    
        private void BtnDelete_Click(object sender, EventArgs e) { }

        private void BtnSearch_Click(object sender, EventArgs e) { }

        // PictureBox properties: SizeMode - StretchImage
        // View Designer in SQL Server Obj Explorer, modified/add another field
        // image datatype recommended ad varbinary(MAX)
        // Update database
        // Add openDialog control, remember to clear FileName in properties
        private void BtnImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // load image using the file name property
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        // this code help to open the image bigger
        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.BackgroundImage = pictureBox1.Image;
            frm.Size = pictureBox1.Image.Size;
            frm.Show();
        }
    }
}
