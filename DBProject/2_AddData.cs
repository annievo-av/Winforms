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
        private void GetData(string selectCommand) { }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // declares a new sql command obj
            SqlCommand command;

            // thorugh the values(data pass into the table BizContacts)
            string insert = @"insert into BizContacts(Date_Added, Company, Website, 
                            Title, First_Name, Last_Name, Address, City, State, Postal_Code, 
                            Email, Mobile, Notes) 
                    values(@Date_Added, @Company, @Website, 
                            @Title, @First_Name, @Last_Name, @Address, @City, @State, @Postal_Code, 
                            @Email, @Mobile, @Notes)";

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
    }
}
