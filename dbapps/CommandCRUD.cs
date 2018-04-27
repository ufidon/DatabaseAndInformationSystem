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
    public partial class Employee_Reader1 : Form
    {
        public Employee_Reader1()
        {
            InitializeComponent();
        }
        // Create a SqlCommmand object
        SQLiteCommand cmdEmployee = new SQLiteCommand();

        private void Employee_Reader1_Load(object sender, EventArgs e)
        {
            // Create a Connection object and assign it to the Connection property:
            cmdEmployee.Connection = new SQLiteConnection();
            // Specify the connection string for the Connection object
            cmdEmployee.Connection.ConnectionString = @"Data Source=wigcompany.db;Version=3; FailIfMissing=True; Foreign Keys=True;";
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {
            // Specify the SQL for the Command object
            cmdEmployee.CommandText = "SELECT Phone FROM Employee WHERE EmpId = "
                                        + txtEmpId.Text;
            cmdEmployee.Connection.Open();  // open the connection
            // Call the ExecuteScalar method to execute the SQL, and 
            // store the returned value in the variable, phone
            string phone = cmdEmployee.ExecuteScalar().ToString();

            lblPhone.Text = phone;
            cmdEmployee.Connection.Close();
        }

        private void btnGetSalary_Click(object sender, EventArgs e)
        {
            // Assign the SQL to the CommandText property 
            cmdEmployee.CommandText = "SELECT LastName, FirstName, Salary FROM Employee WHERE EmpId = "
                                                                     + txtEmpId.Text;
            cmdEmployee.Connection.Open();
            // Call the ExecuteReader method to execute the SQL. 
            SQLiteDataReader drEmployee = cmdEmployee.ExecuteReader();

            // Move the DataReader position to the first (and only) row.
            drEmployee.Read();

            // Get column values from the current row of the DataReader, and display in Labels:
            lblLastName.Text = drEmployee["LastName"].ToString();
            lblFirstName.Text = drEmployee["FirstName"].ToString();
            txtSalary.Text = drEmployee["Salary"].ToString();

            // Alternate methods to get the salary
            // Use the ordinal, 2, in place of the column name, Salary
            txtSalary.Text = drEmployee[2].ToString(); // Salary is the 3rd column of the row
            // or,
            Int32 salaryOrdinal = drEmployee.GetOrdinal("Salary");
            txtSalary.Text = drEmployee[salaryOrdinal].ToString();

            // Get Salary in its original type, double
            double salary = drEmployee.GetDouble(2);
            txtSalary.Text = salary.ToString();

            drEmployee.Close();
            cmdEmployee.Connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmdEmployee.CommandText = "UPDATE Employee SET Salary = " + txtSalary.Text
                                        + " WHERE EmpId = " + txtEmpId.Text;
            cmdEmployee.Connection.Open();
            int count = cmdEmployee.ExecuteNonQuery();
            MessageBox.Show(count + " record updated");
            cmdEmployee.Connection.Close();
        }
    }
}
