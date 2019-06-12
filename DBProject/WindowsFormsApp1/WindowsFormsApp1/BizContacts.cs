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

        // GetData always call as the first thing to bind the 2 sources
        private void GetData(string selectCommand)
        {
            try
            {
                // pass in the select command and the connection string
                dataAdapter = new SqlDataAdapter(selectCommand, connString);

                // make new table object
                table = new System.Data.DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;

                // fill the data table
                dataAdapter.Fill(table);

                // set the data source on the binding source (properties) to table
                bindingSource1.DataSource = table;

                // don't want to access ID - no eidt allow
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // declares a new sql command obj
            SqlCommand command;

            // thorugh the values(data pass into the table BizContacts)
            string insert = @"insert into BizContacts(Date_Added, Company, Website, 
                            Title, First_Name, Last_Name, Address, City, State, Postal_Code, 
                            Email, Mobile, Notes, Image) 
                    values(@Date_Added, @Company, @Website, 
                            @Title, @First_Name, @Last_Name, @Address, @City, @State, @Postal_Code, 
                            @Email, @Mobile, @Notes, @Image)";

            // write code to sql = reach to hardware data, need to save
            // `using` allows disposing of low level resources
            // remember to open then close
            // because we use `using` it takes care of closing for us
            using(conn = new SqlConnection(connString))
            {
                conn.Open();
                command = new SqlCommand(insert, conn);

                try
                {
                    // add VALUE to the field in the form
                    command.Parameters.AddWithValue(@"Date_Added", dateTimePicker1.Value.Date);
                    command.Parameters.AddWithValue(@"Company", txtCompany.Text);
                    command.Parameters.AddWithValue(@"Website", txtWebsite.Text);
                    command.Parameters.AddWithValue(@"Title", txtTitle.Text);
                    command.Parameters.AddWithValue(@"First_Name", txtFName.Text);
                    command.Parameters.AddWithValue(@"Last_Name", txtLName.Text);
                    command.Parameters.AddWithValue(@"Address", txtAddress.Text);
                    command.Parameters.AddWithValue(@"City", txtCity.Text);
                    command.Parameters.AddWithValue(@"State", txtState.Text);
                    command.Parameters.AddWithValue(@"Postal_Code", txtPostalCode.Text);
                    command.Parameters.AddWithValue(@"Email", txtEmail.Text);
                    command.Parameters.AddWithValue(@"Mobile", txtMobile.Text);
                    command.Parameters.AddWithValue(@"Notes", txtNotes.Text);

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

                    // pushing data into the table
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // no finally needed because `using` do the job to closing the conn for us
                // prevent data leaking
            }

            // REFRESHING data everytime it's modified
            GetData(selectionStatement);
            // redraw the data grid view so new record is visible on the bottom
            
            // .Refresh() is more powerfull
            dataGridView1.Update();
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            // get the update command,
            // storing in SQL 2014 doesn't work in SQL 2017 - PK needed
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();

            try
            {
                // update the table in the memory
                bindingSource1.EndEdit();

                // update the database
                dataAdapter.Update(table);

                MessageBox.Show("Update Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // grap a reference to the current row
            DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;

            // grap id, fname, lname for confirmation
            string value = row.Cells["ID"].Value.ToString();
            string fName = row.Cells["First_Name"].Value.ToString();
            string lName = row.Cells["Last_Name"].Value.ToString();

            DialogResult result = MessageBox.Show($"Do you really want to delete {fName} {lName}, record {value}?",
                "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // this is the sql to delete the records from the sql table
            string deleteState = @"Delete from BizContacts where id = '" + value + "'";

            if (result == DialogResult.Yes)
            {
                using (conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(deleteState, conn);
                        comm.ExecuteNonQuery();

                        // REFRESHING
                        GetData(selectionStatement);
                        dataGridView1.Update();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            switch(cboSearch.SelectedItem.ToString())
            {
                case "First Name":
                    GetData("select * from bizcontacts where lower(first_name) like '%" + txtSearch.Text.ToLower() + "%'");
                    break;
                case "Last Name":
                    GetData("select * from bizcontacts where lower(last_name) like '%" + txtSearch.Text.ToLower() + "%'");
                    break;
                case "Company":
                    GetData("select * from bizcontacts where lower(company) like '%" + txtSearch.Text.ToLower() + "%'");
                    break;
            }
        }

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

        // for excel exporting - right click on reference(ctrl+shift+L)
        // COM -> excel obj library
        // saveFileDialog1 used
        private void BtnExportOpen_Click(object sender, EventArgs e)
        {
            // make new excel obj
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
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
