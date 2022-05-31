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
    public partial class FProbOrigin : Form
    {
        bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string MacName = System.Environment.MachineName;
        SqlDataAdapter adapter;
        SqlConnection conn;
        public FProbOrigin()
        {
            InitializeComponent();
        }

        private void FProbOrigin_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        void DisplayData()
        {

            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                conn = db.GetConnString();

                sql = "SELECT ProbOrigin,Description,UpdateBy,UpdateDate,MacName From TPCS_PROBORG " +
                    " ORDER BY ProbOrigin";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;

                #region formatgrid
                dgvReport.Columns["ProbOrigin"].Width = 250;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                txtAff.Text = dgvReport.SelectedRows[0].Cells["ProbOrigin"].Value.ToString();
                txtRemark.Text = dgvReport.SelectedRows[0].Cells["Description"].Value.ToString();


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
                txtAff.Text = "";
                txtAff.Enabled = true;
                txtRemark.Text = "";
                txtRemark.Enabled = true;

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

        void EditMode()
        {
            try
            {
                txtAff.Enabled = true;
                txtRemark.Enabled = true;

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
            txtAff.Enabled = false;
            txtRemark.Enabled = false;

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
            NewRecord = true;
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

        void DeleteRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE TPCS_PROBORG " +
                    " WHERE " +
                    " ProbOrigin='" + dgvReport.SelectedRows[0].Cells["ProbOrigin"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("PV Problem Origin has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
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
                if (txtAff.Text.Length == 0)
                {
                    MessageBox.Show("Please input the Problem Origin!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAff.Focus();
                    ok = false;
                    return ok;
                }


                if (!NewRecord)
                {
                    bool pkchanged = true;
                    string sql = "";
                    SqlCommand cmd;
                    SqlConnection conn;


                    if (dgvReport.SelectedRows[0].Cells["ProbOrigin"].Value.ToString().ToUpper() == txtAff.Text.ToUpper())
                        pkchanged = false;

                    conn = db.GetConnString();
                    sql = "SELECT COUNT(ProbOrigin) from TPCS_PROBORG where ProbOrigin='" + txtAff.Text.ToUpper() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated Problem Origin!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAff.Focus();
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
                    sql = "SELECT COUNT(ProbOrigin) from TPCS_PROBORG where ProbOrigin='" + txtAff.Text.ToUpper() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0))
                    {
                        MessageBox.Show("Duplicated Problem Origin!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAff.Focus();
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
                sql = "INSERT INTO TPCS_PROBORG (ProbOrigin,Description,UpdateBy,UpdateDate,MacName) VALUES " +
                    "(" +
                    "'" + txtAff.Text + "'," +
                    "'" + txtRemark.Text + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    "GETDATE()," +
                    "'" + MacName + "'" +
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Problem Origin has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sql = "UPDATE TPCS_PROBORG SET " +
                    " ProbOrigin='" + txtAff.Text + "'," +
                    " Description='" + txtRemark.Text + "'," +
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE(), " +
                    " MacName='" + MacName + "' " +
                    " WHERE " +
                    " ProbOrigin='" + dgvReport.SelectedRows[0].Cells["ProbOrigin"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Problem Origin has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        header.Add("Master Data: PV-Problem Origin");
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
