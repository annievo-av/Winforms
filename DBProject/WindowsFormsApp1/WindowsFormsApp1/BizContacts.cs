﻿using System;
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

//needed for File use
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
                table = new DataTable();
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
    }
}