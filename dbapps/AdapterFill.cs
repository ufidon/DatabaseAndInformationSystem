using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;    // add this using directive
using System.Data.SQLite;

namespace DbAdapter
{
    public partial class Employee_Display : Form
    {
        public Employee_Display()
        {
            InitializeComponent();
        }
        DataTable dtEmployee = new DataTable(); // creates data table
        SQLiteDataAdapter daEmployee; //declares a ref. variable for data adapter;
        BindingSource bsEmployee = new BindingSource(); //creates binding source

        private void Employee_Display_Load(object sender, EventArgs e)
        {
            // Create the data adapter
            string connString = @"Data Source=wigcompany.db;Version=3; FailIfMissing=True; Foreign Keys=True;";
            string empSQLStr = "SELECT * FROM Employee";    
            daEmployee = new SQLiteDataAdapter(empSQLStr, connString);
            // Fill the data table, dtEmployere
            daEmployee.Fill(dtEmployee);
            // Link binding source to the data table
            bsEmployee.DataSource = dtEmployee; 

            // call DisplayCurrentRow method to display the current row
            DisplayCurrentRow();

            // Display row number and count of rows 
            lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;

            // Link (subscribe) PositionChanged event to bsEmployee_PositionChanged method
            bsEmployee.PositionChanged += bsEmployee_PositionChanged;
        }
        private void DisplayCurrentRow()
        {
            // store the current row of the data table in a DataRowView
            int currentRow = bsEmployee.Position;
            DataRowView drvEmployee = (DataRowView) bsEmployee[currentRow];

            // copy data from the DataRowView to controls on the form:
            txtEmpId.Text = drvEmployee["EmpId"].ToString();         
                    // Or, use the index as in, drvEmployee[0].ToString();
            txtFirstName.Text = drvEmployee["FirstName"].ToString(); 
            txtLastName.Text = drvEmployee["LastName"].ToString();
            txtDOB.Text = drvEmployee["DateofBirth"].ToString();
            txtPhone.Text = drvEmployee["Phone"].ToString();
            txtCountryId.Text = drvEmployee["CountryId"].ToString();
            txtDepNo.Text = drvEmployee["DeptNo"].ToString();
            txtSalary.Text = drvEmployee["Salary"].ToString();
            chkFullTime.Checked = drvEmployee["FullTime"].ToString()=="Y"?true:false;
            txtPosition.Text = drvEmployee["Position"].ToString();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveFirst();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveLast();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bsEmployee.MovePrevious();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveNext();
        }

        private void bsEmployee_PositionChanged(object sender, System.EventArgs e) 
        {
            // display data & row number whenever a the current record position changes
            if (bsEmployee.Count > 0)
            {
                DisplayCurrentRow();
                lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;
            }
        }
    }
}
