using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace PCSSystem
{
    public partial class FChangeType : Form
    {
        bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string MacName = System.Environment.MachineName;
        SqlDataAdapter adapter;
        SqlConnection conn;
        
        public FChangeType()
        {
            InitializeComponent();
        }

        private void FChangeType_Load(object sender, EventArgs e)
        {
            DisplayData();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void DisplayData()
        {
            
            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                conn = db.GetConnString();

                sql = "SELECT ChangeType,Indicator,PV,UpdateBy,UpdateDate,MacName From TPCS_CHNGTYP " +
                    " ORDER BY ChangeType";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;

                #region formatgrid
                dgvReport.Columns["ChangeType"].Width = 250;
                dgvReport.Columns["Indicator"].Width = 50;
                dgvReport.Columns["PV"].Width = 80;
                dgvReport.Columns["UpdateDate"].Width = 150;
                dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                DisplayValue();
            }
        }


        void DisplayValue()
        {
            try
            {
                //Date, Holiday, UpdateBy, UpdateDate 
                txtChnType.Text = dgvReport.SelectedRows[0].Cells["ChangeType"].Value.ToString();
                txtInd.Text = dgvReport.SelectedRows[0].Cells["Indicator"].Value.ToString();
                txtPV.Text = dgvReport.SelectedRows[0].Cells["PV"].Value.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }


        void AddMode()
        {
            try
            {
                txtChnType.Text = "";
                txtChnType.Enabled = true;
                txtInd.Text = "";
                txtInd.Enabled = true;
                txtPV.Text = "";
                txtPV.Enabled = true;

                btnAdd.Visible = false;
                btnCancel.Visible = true;
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                dgvReport.Enabled = false;
                btnExport.Enabled = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                EditMode();
                NewRecord = false;
            }
        }


        void EditMode()
        {
            try
            {
                txtChnType.Enabled = true;
                txtInd.Enabled = true;
                txtPV.Enabled = true;

                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnEdit.Visible = false;
                btnCancelE.Visible = true;
                btnDelete.Enabled = false;
                dgvReport.Enabled = false;
                btnExport.Enabled = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void ViewMode()
        {
            DisplayValue();
            txtChnType.Enabled = false;
            txtInd.Enabled = false;
            txtPV.Enabled = false;

            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            btnCancel.Visible = true;
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            btnEdit.Visible = true;
            btnCancelE.Visible = true;
            btnDelete.Enabled = true;
            dgvReport.Enabled = true;
            btnExport.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewMode();
            NewRecord = false;
        }

        private void btnCancelE_Click(object sender, EventArgs e)
        {
            ViewMode();
            NewRecord = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
                DeleteRecord();
        }

        void DeleteRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE TPCS_CHNGTYP " +
                    " WHERE " +
                    " ChangeType='" + dgvReport.SelectedRows[0].Cells["ChangeType"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("PV Change Type has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
            NewRecord = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (valid_input())
                {
                    if (NewRecord)
                    {
                        SaveNewRecord();
                    }
                    else
                    {
                        SaveEditedRecord();
                    }
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool valid_input()
        {
            bool ok = true;
            try
            {
                if (txtChnType.Text.Length == 0)
                {
                    MessageBox.Show("Please input the Change Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChnType.Focus();
                    ok = false;
                    return ok;
                }

                if (txtInd.Text.Length == 0)
                {
                    MessageBox.Show("Please input the Indicator!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInd.Focus();
                    ok = false;
                    return ok;
                }

                if (txtPV.Text.Length == 0)
                {
                    MessageBox.Show("Please input the PV!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPV.Focus();
                    ok = false;
                    return ok;
                }


                if (!NewRecord)
                {
                    bool pkchanged = true;
                    string sql = "";
                    SqlCommand cmd;
                    SqlConnection conn;


                    if (dgvReport.SelectedRows[0].Cells["ChangeType"].Value.ToString().ToUpper() == txtChnType.Text.ToUpper())
                        pkchanged = false;

                    conn = db.GetConnString();
                    sql = "SELECT COUNT(ChangeType) from TPCS_CHNGTYP where ChangeType='" + txtChnType.Text.ToUpper() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated Change Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtChnType.Focus();
                        ok = false;
                        return ok;
                    }

                    pkchanged = true;
                    if (dgvReport.SelectedRows[0].Cells["Indicator"].Value.ToString().ToUpper() == txtInd.Text.ToUpper())
                        pkchanged = false;
                    
                    sql = "SELECT COUNT(Indicator) from TPCS_CHNGTYP where Indicator='" + txtInd.Text.ToUpper() + "'";
                    cmd.CommandText = sql;

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated Indicator!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtInd.Focus();
                        ok = false;
                        return ok;
                    }

                    pkchanged = true;
                    if (dgvReport.SelectedRows[0].Cells["PV"].Value.ToString().ToUpper() == txtPV.Text.ToUpper())
                        pkchanged = false;
                    
                    sql = "SELECT COUNT(PV) from TPCS_CHNGTYP where PV='" + txtPV.Text.ToUpper() + "'";
                    cmd.CommandText = sql;

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated PV!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPV.Focus();
                        ok = false;
                        return ok;
                    }

                }
                else
                {
                    string sql = "";
                    SqlCommand cmd;
                    SqlConnection conn;

                    conn = db.GetConnString();
                    sql = "SELECT COUNT(ChangeType) from TPCS_CHNGTYP where ChangeType='" + txtChnType.Text.ToUpper() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0))
                    {
                        MessageBox.Show("Duplicated Change Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtChnType.Focus();
                        ok = false;
                        return ok;
                    }

                    sql = "SELECT COUNT(Indicator) from TPCS_CHNGTYP where Indicator='" + txtInd.Text.ToUpper() + "'";
                    cmd.CommandText = sql;

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0))
                    {
                        MessageBox.Show("Duplicated Indicator!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtInd.Focus();
                        ok = false;
                        return ok;
                    }

                    sql = "SELECT COUNT(PV) from TPCS_CHNGTYP where PV='" + txtPV.Text.ToUpper() + "'";
                    cmd.CommandText = sql;

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0))
                    {
                        MessageBox.Show("Duplicated PV!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPV.Focus();
                        ok = false;
                        return ok;
                    }

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        void SaveNewRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_CHNGTYP (ChangeType,Indicator,PV,UpdateBy,UpdateDate,MacName) VALUES " +
                    "(" +
                    "'" + txtChnType.Text + "'," +
                    "'" + txtInd.Text + "'," +
                    "'" + txtPV.Text + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    "GETDATE()," +
                    "'"+MacName+"'"+
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Change Type has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SaveEditedRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "UPDATE TPCS_CHNGTYP SET " +
                    " ChangeType='" + txtChnType.Text + "'," +
                    " Indicator='" + txtInd.Text + "'," +
                    " PV='" + txtPV.Text + "'," +
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE(), " +
                    " MacName='" +MacName+"' "+
                    " WHERE " +
                    " ChangeType='" + dgvReport.SelectedRows[0].Cells["ChangeType"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Change Type has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {

                if (dgvReport.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Master Data: PV-Change Type");
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = saveFileDialog1.FileName.ToString();

                        cm.Export_to_CSV(header, path, dgvReport);
                    }
                    else
                    {

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
