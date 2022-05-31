using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PCSSystem
{
    public partial class FLockPrdt : Form
    {
        string sql = "";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        database db = new database();
        SqlTransaction trans = null;
        public FLockPrdt()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FLockPrdt_Load(object sender, EventArgs e)
        {
            DisplayData();          
        }

        void DisplayData()
        {
            DataTable dt =new DataTable();
            try
            {
                conn=db.GetConnString();
                sql = "SELECT Plant, Product, LockedBy, LockedDate, LockedMac from TPCS_LockProduct";
                adapter=new SqlDataAdapter(sql,conn);
                adapter.Fill(dt);
                dgvReport.Columns.Clear();
                dgvReport.DataSource=dt;
                lblRows.Text="Total Rows: "+dgvReport.Rows.Count.ToString();

                DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                dgvReport.Columns.Insert(0,col);
                dgvReport.Columns[0].HeaderText = "";
                dgvReport.Columns[0].Width = 30;

                adapter.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReport.SelectedRows.Count > 0)
                {
                    bool selected=false;
                    for (int i = 0; i < dgvReport.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvReport.Rows[i].Cells[0].Value))
                        {
                            selected = true;
                            break;
                        }
                    }

                    if (selected)
                    {
                        if (MessageBox.Show("Do you really want to unlock this product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    == DialogResult.Yes)
                        {
                            UnlockProduct();
                            DisplayData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No rows selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
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
                        sql = sql+" DELETE FROM TPCS_LOCKPRODUCT WHERE " +
                            " Plant='" + dgvReport.Rows[i].Cells["Plant"].Value.ToString() + "' AND " +
                            " Product='" + dgvReport.Rows[i].Cells["Product"].Value.ToString() + "' AND " +
                            " LockedMac='" + dgvReport.Rows[i].Cells["LockedMac"].Value.ToString() + "' " +
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

        private void dgvReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                    if (e.RowIndex >= 0)
                        dgvReport.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value);
                    else
                    {
                        if (dgvReport.Rows.Count > 0)
                        {
                            bool marked = Convert.ToBoolean(dgvReport.Rows[0].Cells[0].Value);
                            for (int i = 0; i < dgvReport.Rows.Count; i++)
                            {
                                dgvReport.Rows[i].Cells[0].Value = !marked;
                            }
                        }                        
                    }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }



    }
}
