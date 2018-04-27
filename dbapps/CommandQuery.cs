using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace DbCommand
{
    public partial class Employee_Reader2 : Form
    {
        public Employee_Reader2()
        {
            InitializeComponent();
        }
        // Create a SqlCommmand object
        SQLiteCommand cmdEmployee = new SQLiteCommand();

        private void Employee_Reader2_Load(object sender, EventArgs e)
        {
            // Create a Connection object and specify the connection string
            cmdEmployee.Connection = new SQLiteConnection();
            cmdEmployee.Connection.ConnectionString = @"Data Source=wigcompany.db;Version=3; FailIfMissing=True; Foreign Keys=True;";
            // Add DeptNo's from the Department table, to the Combobox
            cmdEmployee.CommandText = "SELECT DeptNo FROM Department"; // specify SQL
            cmdEmployee.Connection.Open();
            // Call the ExecuteReader method to execute the SQL. 
            SQLiteDataReader drEmployee = cmdEmployee.ExecuteReader();

            while (drEmployee.Read() == true)  // loop through the rows
            {
                cboDeptNo.Items.Add(drEmployee["DeptNo"]);  // add DeptNo to the ComboBox
            }
            drEmployee.Close();
            cmdEmployee.Connection.Close();
        }

        private void cboDeptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdEmployee.CommandText = "SELECT * FROM Employee WHERE DeptNo = "
                                                        + cboDeptNo.SelectedItem;
            cmdEmployee.Connection.Open();
            SQLiteDataReader drEmployee = cmdEmployee.ExecuteReader();

            // display data in a ListView by storing the current row in an array
            lvwEmployee.Items.Clear();
            while (drEmployee.Read() == true)   // loop through the rows
            {
                int colCount = drEmployee.FieldCount;  // get number of columns
                object[] colValuesObj = new object[colCount];  // create an object array

                // Get values from the current row, and store in the array, colValuesObj
                drEmployee.GetValues(colValuesObj);
                // Convert objects to strings and store in the String array, colValues 
                String[] colValues = Array.ConvertAll(colValuesObj, element => element.ToString());

                // Create a ListViewItem using the array, colValues 
                ListViewItem lviEmp = new ListViewItem(colValues);
                lvwEmployee.Items.Add(lviEmp);  // add the ListViewItem to ListView:
            }
            drEmployee.Close();
            cmdEmployee.Connection.Close();
        }
    }
}
