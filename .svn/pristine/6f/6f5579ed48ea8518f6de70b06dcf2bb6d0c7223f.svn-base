using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PCSSystem.ASP
{
    public partial class FLockproduct : Form
    {
        database db = new database();
        Common cm = new Common();
        DataSet ds;
        string sql;

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlTransaction trans = null;

        public FLockproduct()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FLockproduct_Load(object sender, EventArgs e)
        {
            DisplayData();
        }
        void DisplayData()
        {
            try
            {
                SqlDataAdapter adapter;
                SqlConnection conn;
                conn = db.GetConnString();

                sql = "SELECT plant as Plant, product as Product, insertby as LockedBy, postdate as LockedDate, ipaddress as LockedMac from asp_lock";
                //sql = "EXEC asp_pp57_jobrequest '2300','BB'";
                adapter = new SqlDataAdapter(sql, conn);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvReport.DataSource = null;
                dgvReport.DataSource = ds.Tables[0];
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                for (int i = 0; i < dgvReport.RowCount; i++)
                {
                    dgvReport.Rows[i].Cells[1].ReadOnly = true;
                    dgvReport.Rows[i].Cells[2].ReadOnly = true;
                    dgvReport.Rows[i].Cells[3].ReadOnly = true;
                    dgvReport.Rows[i].Cells[4].ReadOnly = true;
                    dgvReport.Rows[i].Cells[5].ReadOnly = true;
                }
             }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int jumcheck = 0;
            for (int i = 0; i <= dgvReport.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dgvReport.Rows[i].Cells["chk2"].Value) == true)
                {
                    jumcheck++;
                }
            }
            if (jumcheck > 0)
            {
                DialogResult res = MessageBox.Show("you are Check " + jumcheck + " Records, Are you sure to Unlocked Products ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    //SaveMode(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                    UnlockProduct();
                    DisplayData();
                }
            }
            else
            {
                MessageBox.Show("no Records Selected..!!\nPlease select one or more records..!!", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void UnlockProduct()
        {
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {

                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells[0].Value))
                    {
                        sql = sql + " DELETE FROM asp_lock WHERE " +
                            " plant='" + dgvReport.Rows[i].Cells["Plant"].Value.ToString() + "' AND " +
                            " product='" + dgvReport.Rows[i].Cells["Product"].Value.ToString() + "' AND " +
                            " ipaddress='" + dgvReport.Rows[i].Cells["LockedMac"].Value.ToString() + "' " +
                            " ";
                    }
                }

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                MessageBox.Show("The product has been unlocked!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                db.SaveError(ex.ToString());
            }
        }
    }
}
