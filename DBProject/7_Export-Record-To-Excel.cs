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
        private void BtnExportOpen_Click(object sender, EventArgs e)
        {
            // make new excel obj
            _Application excel = new Microsoft.Office.Interop.Excel.Application();
            // make a new workbook
            _Workbook workbook = excel.Workbooks.Add(Type.Missing);
            // make a worksheet and for now set it to null
            _Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Business Contacts";

                // because both data grids and excel sheets are tabular
                // use nested loops to write from one to the other
                
                // this loop controls the row number
                for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count - 1; rowIndex++)
                {
                    // because there is column header so no need -1
                    for (int colIndex = 0; colIndex < dataGridView1.Columns.Count; colIndex++)
                    {
                        if (rowIndex == 0) // header row
                        {
                            // in excel, row and column indexes begin at 1, 1
                            // write out the header texts from the grid view to excel sheet
                            worksheet.Cells[rowIndex + 1, colIndex + 1] = dataGridView1.Columns[colIndex].HeaderText;
                        }
                        else
                        {
                            // fix the row index at 1, then change the index over its possible values
                            worksheet.Cells[rowIndex + 1, colIndex + 1] = dataGridView1.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        }
                    }
                } // end for
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // save file to drive
                    workbook.SaveAs(saveFileDialog1.FileName);
                    // open in excel right after
                    Process.Start("excel.exe", saveFileDialog1.FileName);
                }
            } // end try
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }
    }
}
