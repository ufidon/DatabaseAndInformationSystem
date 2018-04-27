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

namespace DbAdapter
{
    public partial class Employee_Select : Form
    {
        public Employee_Select()
        {
            InitializeComponent();
        }

        // data table, data adapter and binding source for Employee table
        DataTable dtEmployee = new DataTable(); 
        SQLiteDataAdapter daEmployee;  
        BindingSource bsEmployee = new BindingSource();

        // data table and data adapter for Department table
        DataTable dtDept = new DataTable();
        SQLiteDataAdapter daDept;

        private void Employee_Select_Load(object sender, EventArgs e)
        {
            // Fill employee data table, dtEmployee
            string connString = @"Data Source=wigcompany.db;Version=3; FailIfMissing=True; Foreign Keys=True;";
            string empSQLStr = "SELECT * FROM Employee";    
            daEmployee = new SQLiteDataAdapter(empSQLStr, connString);
            daEmployee.Fill(dtEmployee);

            // Display employee data
            bsEmployee.DataSource = dtEmployee; // links binding source to the data table
            DisplayCurrentRow();
            lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;
            bsEmployee.PositionChanged += bsEmployee_PositionChanged;

            // Fill department data table, dtDept
            string DeptSQLStr = "SELECT DeptNo, DeptName FROM Department";
            daDept = new SQLiteDataAdapter(DeptSQLStr, connString); 
            daDept.Fill(dtDept);

            //Display DeptNo's from dtDept in the ComboBox, cboFillByDept: 
            cboFillByDept.DataSource = dtDept;
            cboFillByDept.DisplayMember = "DeptName";
            cboFillByDept.ValueMember = "DeptNo";

            //Display DeptNo's from dtDept in the ComboBox, cboFilterByDept:
            cboFilterByDept.DataSource = dtDept;
            cboFilterByDept.DisplayMember = "DeptName";
            cboFilterByDept.ValueMember = "DeptNo"; 
        }

        private void cboFilterByDept_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bsEmployee.Filter = "DeptNo = " + cboFilterByDept.SelectedValue.ToString();
            if (bsEmployee.Count > 0)
            {
                lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;
                DisplayCurrentRow();    
            }

        }

        private void cboFillByDept_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Specify the SQL
            daEmployee.SelectCommand.CommandText = "SELECT * FROM Employee where DeptNo = " + 
                                                    cboFillByDept.SelectedValue.ToString();
            //Clear and fill the data table
            dtEmployee.Clear();
            daEmployee.Fill(dtEmployee);
        }

        private void btnDisplayAll_Click(object sender, EventArgs e)
        {
            dtEmployee.Clear();
            daEmployee.SelectCommand.CommandText = "SELECT * FROM Employee";
            daEmployee.Fill(dtEmployee);

            bsEmployee.Filter = ""; // remove the filter

            lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;
            DisplayCurrentRow();
        }

        private void DisplayCurrentRow()
        {
            // store the current row of the data table in a DataRow
            int currentRow = bsEmployee.Position;
            DataRowView drvEmployee = (DataRowView)bsEmployee[currentRow];

            // copy data from the datarow to controls on the form:
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
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bsEmployee.MovePrevious();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveLast();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveNext();
        }

        private void bsEmployee_PositionChanged(object sender, System.EventArgs e)
        {
            // display row number and data, whenever a the current record position changes
            if (bsEmployee.Count > 0)
            {
                lblRecordNo.Text = (bsEmployee.Position + 1) + "  of  " + bsEmployee.Count;
                DisplayCurrentRow();
            }
        }
    }
}
