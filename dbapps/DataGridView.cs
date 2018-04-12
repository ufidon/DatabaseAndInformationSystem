using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SQLite;

using System.Windows.Forms;
using System.Drawing;

namespace DbApp06
{
    class MForm : Form
    {

        private DataGridView dgv = null;
        private DataSet ds = null;

        public MForm()
        {

            this.Text = "DataGridView";
            this.Size = new Size(450, 350);

            this.InitUI();
            this.InitData();

            this.CenterToScreen();
        }

        void InitUI()
        {
            dgv = new DataGridView();

            dgv.Location = new Point(8, 0);
            dgv.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            dgv.TabIndex = 0;
            dgv.Parent = this;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(dgv != null)
                dgv.Size = this.ClientSize;

        }

        void InitData()
        {
            string cs = @"Data Source=wigcompany.db; 
                                        Version=3; FailIfMissing=True; Foreign Keys=True;";

            //string stm = @"select fname || ' ' || lname as `full name`, country  from employee"; 
            string stm = @"select *  from employee";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                ds = new DataSet();

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(stm, con))
                {
                    da.Fill(ds, "employee");
                    dgv.DataSource = ds.Tables["employee"];
                }

                con.Close();
            }
        }
    }

    class MApplication
    {
        public static void Main()
        {
            Application.Run(new MForm());
        }
    }
}
