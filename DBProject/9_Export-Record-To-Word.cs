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

// this let you open word
using Microsoft.Office.Interop.Word;

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
        private void BtnSaveToTxt_Click(object sender, EventArgs e) { }

        // for word exporting - right click on reference(ctrl+shift+L)
        // COM -> word obj library
        // added: Excel Files|*.xlsx|Text Files|*.txt|Word Files|*.docx
        private void BtnOpenWord_Click(object sender, EventArgs e)
        {
            // make a new word object
            Microsoft.Office.Interop.Word._Application word = new Microsoft.Office.Interop.Word.Application();
            // make a new document
            Document doc = word.Documents.Add();
            Microsoft.Office.Interop.Word.Range rng = doc.Range(0, 0);

            // make anew table based on our data grid view
            Table wdTable = doc.Tables.Add(rng, dataGridView1.Rows.Count, dataGridView1.Columns.Count);
            // make a thick outer border
            wdTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleDouble;
            // mkae the cell lines thin
            wdTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

            try
            {
                // make an active document in word
                doc = word.ActiveDocument;

                // i is the row index from the data grid view
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    // this loop is needed to step through the columns of each row
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)

                        // line below runs several times, each time writing the cell value from the grid to word
                        // because the first row at index 0 is the header row
                        if (i == 0)
                        {
                            // write out the header texts from the grid view to word table
                            // line below runs as many times as there are cells whose text needs to be copied out to word
                            wdTable.Cell(i + 1, j + 1).Range.InsertAfter(dataGridView1.Columns[j].HeaderText);
                        }
                        else
                        {
                            // line below runs when data, not headers, is copied from the datagridview to the word table
                            wdTable.Cell(i + 1, j + 1).Range.InsertAfter(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                }
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    doc.SaveAs(saveFileDialog1.FileName);
                    // open the document in word after the table is made
                    Process.Start("winword.exe", saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // clean up the word object and document object
                word.Quit();
                word = null;
                doc = null;
            }
        }
    }
}
