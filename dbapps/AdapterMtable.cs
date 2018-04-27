using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace DbApp20
{
    public partial class DepartmentView : Form
    {
        SQLiteDataAdapter daEmployee = null;
        DataTable dtEmployee = null;
        DataTable dtEmpcb = null;
        BindingSource bsEmployee = null;

        SQLiteDataAdapter daDept = null;
        DataTable dtDept = null;


        public DepartmentView()
        {
            InitializeComponent();

            string constring = @"Data Source=wigcompany.db; 
                                        Version=3; FailIfMissing=True; Foreign Keys=True;";
            string sql = "select * from employee";

            daEmployee = new SQLiteDataAdapter(sql, constring);
            dtEmployee = new DataTable();
            dtEmpcb = new DataTable();
            bsEmployee = new BindingSource();

            daEmployee.Fill(dtEmployee);
            daEmployee.Fill(dtEmpcb);
            bsEmployee.DataSource = dtEmployee;

            string sqldept = "select DeptNo, Name from department";
            daDept = new SQLiteDataAdapter(sqldept, constring);
            dtDept = new DataTable();
            daDept.Fill(dtDept);
        }

        private void DisplayCurrentRow()
        {
            int currentRow = bsEmployee.Position;
            DataRowView drvEmployee = (DataRowView)bsEmployee[currentRow];

            txtFName.Text = drvEmployee["FName"].ToString();
            txtLName.Text = drvEmployee["LName"].ToString();
            picPortrait.Image = Image.FromStream(new MemoryStream((byte[])drvEmployee["portrait"]));

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DisplayCurrentRow();
            lblRecordNo.Text = (bsEmployee.Position + 1) + " of " + bsEmployee.Count;
            bsEmployee.PositionChanged += BsEmployee_PositionChanged;

            cbFill.DataSource = dtDept;
            cbFill.DisplayMember = "Name";
            cbFill.ValueMember = "DeptNo";

            cbFilter.DataSource = dtEmpcb;
            cbFilter.DisplayMember = "City";
            cbFilter.ValueMember = "City";
        }

        private void BsEmployee_PositionChanged(object sender, EventArgs e)
        {
            if (bsEmployee.Count > 0)
            {
                DisplayCurrentRow();
                lblRecordNo.Text = (bsEmployee.Position + 1) + " of " + bsEmployee.Count;
            }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveFirst();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            bsEmployee.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bsEmployee.MoveLast();
        }

        private void btnDispAll_Click(object sender, EventArgs e)
        {
            dtEmployee.Clear();
            daEmployee.SelectCommand.CommandText = "select * from employee";
            daEmployee.Fill(dtEmployee);

            bsEmployee.Filter = "";

            DisplayCurrentRow();
            lblRecordNo.Text = (bsEmployee.Position + 1) + " of " + bsEmployee.Count;
        }

        private void cbFill_SelectionChangeCommitted(object sender, EventArgs e)
        {
            daEmployee.SelectCommand.CommandText = "select * from employee e, workfor w where e.ssn=w.employeessn and deptno="
                +cbFill.SelectedValue.ToString();

            dtEmployee.Clear();
            daEmployee.Fill(dtEmployee);
        }

        private void cbFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bsEmployee.Filter = "City =  '"+cbFilter.SelectedValue.ToString() + "'";

            if (bsEmployee.Count > 0)
            {
                DisplayCurrentRow();
                lblRecordNo.Text = (bsEmployee.Position + 1) + " of " + bsEmployee.Count;
            }
        }
    }
}
