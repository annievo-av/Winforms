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
        private void GetData(string selectCommand) 
        { 
            // ...
                            
            // don't want to access ID - no eidt allow
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void BtnAdd_Click(object sender, EventArgs e) { }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            commandBuilder = new SqlCommandBuilder(dataAdapter);

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
    }
}
