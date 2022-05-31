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
    public partial class FSAPShift : Form
    {
        Common cm = new Common();
        database db = new database();
        public FSAPShift()
        {
            InitializeComponent();
        }

        private void FSAPShift_Load(object sender, EventArgs e)
        {
            DisplayData();
            
        }

        void DisplayData()
        {
            string sql = "";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conn;
            try
            {
                conn = db.GetConnString();
                sql = "SELECT DayType, DayDescription, HourShift, NoOfShift, ISNULL(OTHour1,0) as 'OTHour1', " +
                    " ISNULL(OTHour2,0) as 'OTHour2', ISNULL(OTHour3,0) as 'OTHour3' " +
                    " FROM TPCS_SHIFT ";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvReport.SelectedRows.Count > 0)
                {
                    cbbNoShift.SelectedItem = dgvReport.SelectedRows[0].Cells["NoOfShift"].Value.ToString();
                    txtHourShift.Text = dgvReport.SelectedRows[0].Cells["HourShift"].Value.ToString();
                    txtOT1.Text = dgvReport.SelectedRows[0].Cells["OT1"].Value.ToString();
                    txtOT2.Text = dgvReport.SelectedRows[0].Cells["OT2"].Value.ToString();
                    txtOT3.Text = dgvReport.SelectedRows[0].Cells["OT3"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnEdit.Text == "&Edit")
                {
                    
                    EditMode();
                }
                else
                {
                    
                    ViewMode();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void EditMode()
        {
            btnEdit.Text = "&Cancel";
            cbbNoShift.Enabled = true;
            txtHourShift.Enabled = true;
            txtOT1.Enabled = true;
            txtOT2.Enabled = true;
            txtOT3.Enabled = true;
            btnSave.Enabled = true;
        }

        void ViewMode()
        {
            btnEdit.Text = "&Edit";
            cbbNoShift.Enabled = false;
            txtHourShift.Enabled = false;
            txtOT1.Enabled = false;
            txtOT2.Enabled = false;
            txtOT3.Enabled = false;
            btnSave.Enabled = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            string sql = "";
            SqlConnection conn;
            try
            {
                if (validate_input())
                {
                    conn = db.GetConnString();
                    sql = "UPDATE TPCS_SHIFT SET NoOfShift=@noshift, HourShift=@hour, " +
                        " OTHour1=@ot1, OTHour2=@ot2, OTHour3=@ot3, UpdateBy=@user, UpdateDate=GETDATE() " +
                        " WHERE DayType=@type";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@noshift", SqlDbType.NVarChar);
                    cmd.Parameters["@noshift"].Value = cbbNoShift.SelectedItem.ToString();
                    cmd.Parameters.Add("@hour", SqlDbType.NVarChar);
                    cmd.Parameters["@hour"].Value = txtHourShift.Text.ToString();
                    cmd.Parameters.Add("@ot1", SqlDbType.NVarChar);
                    cmd.Parameters["@ot1"].Value = txtOT1.Text.ToString();
                    cmd.Parameters.Add("@ot2", SqlDbType.NVarChar);
                    cmd.Parameters["@ot2"].Value = txtOT2.Text.ToString();
                    cmd.Parameters.Add("@ot3", SqlDbType.NVarChar);
                    cmd.Parameters["@ot3"].Value = txtOT3.Text.ToString();
                    cmd.Parameters.Add("@user", SqlDbType.NVarChar);
                    cmd.Parameters["@user"].Value = UserAccount.GetuserID().ToUpper();
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar);
                    cmd.Parameters["@type"].Value = dgvReport.SelectedRows[0].Cells["DayType"].Value.ToString();
                    cmd.ExecuteNonQuery();
                    
                    DisplayData();
                    ViewMode();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool validate_input()
        {
            bool ok = true;

            try
            {
                if (cbbNoShift.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the shift!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbNoShift.Focus();
                    ok = false;
                    return ok;
                }

                if (Convert.ToDecimal(txtHourShift.Text) < 0)
                {
                    MessageBox.Show("Please input the shift hour!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHourShift.Focus();
                    ok = false;
                    return ok;
                }

                

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return ok;
        }

        private void txtHourShift_Leave(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0.00";
                }
                else
                {
                    t.Text = Convert.ToSingle(t.Text).ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString(), false);
                MessageBox.Show("Wrong number format!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t.Focus();
            }
        }

        private void txtHourShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t;
            try
            {
                t = (TextBox)sender;
                if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.'))
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                
            }
        }

        private void txtOT1_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (Convert.ToSingle(t.Text) == 0)
                {
                    t.Text = "";
                }                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString(), false);
                MessageBox.Show("Wrong number format!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t.Focus();
            }
        }

    }
}
