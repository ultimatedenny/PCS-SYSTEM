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

namespace PCSSystem.Master_Data
{
    public partial class FFGStatus : Form
    {
        bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string MacName = System.Environment.MachineName;
        SqlDataAdapter adapter;
        SqlConnection conn;
        void AddMode()
        {
            try
            {
                txtFGStatus.Text = "";
                txtFGStatus.Enabled = true;
                txtDesc.Text = "";
                txtDesc.Enabled = true;

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
        bool valid_input()
        {
            bool ok = true;
            try
            {
                if (txtFGStatus.Text.Length == 0)
                {
                    MessageBox.Show("Please input the FG Status!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFGStatus.Focus();
                    ok = false;
                    return ok;
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
                sql = "INSERT INTO TPCS_FGSTATUS (FGStatus,FGStatusDescription,UpdateBy,UpdateDate,MacName) VALUES " +
                    "(" +
                    "'" + txtFGStatus.Text + "'," +
                    "'" + txtDesc.Text + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    "GETDATE()," +
                    "'" + MacName + "'" +
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("FG Status has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ViewMode();
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
                sql = "UPDATE TPCS_FGSTATUS SET " +
                    " FGStatus='" + txtFGStatus.Text + "'," +
                    " FGStatusDescription='" + txtDesc.Text + "'," +
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE(), " +
                    " MacName='" + MacName + "' " +
                    " WHERE " +
                    " FGStatus='" + dgvReport.SelectedRows[0].Cells["FGStatus"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("FG Status has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ViewMode();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        void ViewMode()
        {
            //DisplayValue();
            txtFGStatus.Enabled = false;
            txtDesc.Enabled = false;

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
        void DisplayValue()
        {
            try
            { 
                txtFGStatus.Text = dgvReport.SelectedRows[0].Cells["FGStatus"].Value.ToString();
                txtDesc.Text = dgvReport.SelectedRows[0].Cells["FGStatusDescription"].Value.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        void DisplayData()
        {

            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                conn = db.GetConnString();

                sql = "SELECT FGStatus,FGStatusDescription,UpdateBy,UpdateDate,MacName From TPCS_FGSTATUS " +
                    " ORDER BY FGStatus";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;

                #region formatgrid
                dgvReport.Columns["FGStatus"].Width = 250;
                dgvReport.Columns["FGStatusDescription"].Width = 50;
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
        public FFGStatus()
        {
            InitializeComponent();
        }
        void EditMode()
        {
            try
            {
                txtFGStatus.Enabled = true;
                txtDesc.Enabled = true;

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
        void DeleteRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE FROM TPCS_FGSTATUS " +
                    " WHERE " +
                    " FGStatus='" + dgvReport.SelectedRows[0].Cells["FGStatus"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("FG Status has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    DisplayData();
                }

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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
                DeleteRecord();
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

                        header.Add("Master Data: PV-FG Status");
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = saveFileDialog1.FileName.ToString();

                        cm.Export_to_CSV(header, path, dgvReport);
                    }

                }
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
        private void FFGStatus_Load(object sender, EventArgs e)
        {
            DisplayData();
        }
        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                DisplayValue();
            }
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
    }
}
