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

// this let you open excel directly from our code Process.Start
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class BizContacts : Form
    {
        // connection string - right click to SQL db
        string connString = @"Data Source=DESKTOP-AV19\SQLEXPRESS;Initial Catalog=AddressBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        // dataAdapter - build connection between the program and the database
        SqlDataAdapter dataAdapter;
        
        // table - hold the information so we can fill datagrid view
        System.Data.DataTable table;

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

        private void BtnAdd_Click(object sender, EventArgs e) { }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) { }
    
        private void BtnDelete_Click(object sender, EventArgs e) { }

        private void BtnSearch_Click(object sender, EventArgs e) { }
    
        private void BtnImage_Click(object sender, EventArgs e) { }

        private void PictureBox1_DoubleClick(object sender, EventArgs e) { }
        
        // for excel exporting - right click on reference(ctrl+shift+L)
        // COM -> excel obj library
        // saveFileDialog1 used
        private void BtnExportOpen_Click(object sender, EventArgs e) { }

        // saveFileDialog1 property Filter - Excel Files|*.xlsx|Text Files|*.txt
        private void BtnSaveToTxt_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    // grab each row in the datagridview
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // grab cell in each row
                        foreach (DataGridViewCell cell in row.Cells)
                            // write the value to a txt file
                            sw.Write(cell.Value);
                        // jump to a new line after each row
                        sw.WriteLine();
                    }
                }
                
                // open file in notepad after the file is written to the write
                Process.Start("notepad.exe", saveFileDialog1.FileName);
            }
        }
    }
}
