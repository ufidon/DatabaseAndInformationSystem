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
    public partial class EmployeeView : Form
    {

        SQLiteDataAdapter daEmployee = null;
        DataTable dtEmployee = null;
        BindingSource bsEmployee = null;

        public EmployeeView()
        {
            InitializeComponent();

            string constring = @"Data Source=wigcompany.db; 
                                        Version=3; FailIfMissing=True; Foreign Keys=True;";
            string sql = "select * from employee";

            daEmployee = new SQLiteDataAdapter(sql, constring);
            dtEmployee = new DataTable();
            bsEmployee = new BindingSource();

            daEmployee.Fill(dtEmployee);
            bsEmployee.DataSource = dtEmployee;


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
        }

        private void BsEmployee_PositionChanged(object sender, EventArgs e)
        {
            if(bsEmployee.Count>0)
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
    }
}
