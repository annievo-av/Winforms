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
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
