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
using System.IO;

namespace PCSSystem
{
    public partial class FProdnDay : Form
    {
       // bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string errortitle = "", errorsql = "";

        public FProdnDay()
        {
            InitializeComponent();
        }

        private void FProdnDay_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);
            DisplayData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void DisplayData()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                conn = db.GetConnString();

                sql = "SELECT Plant, Period, StartDate, EndDate, UpdateBy, UpdateDate From TPCS_PRODNDAY where LEFT(Period,4)>='" + DateTime.Now.ToString("yyyy") + "'";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;


                #region formatgrid
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Period"].Width = 80;
                dgvReport.Columns["StartDate"].Width = 150;
                dgvReport.Columns["StartDate"].DefaultCellStyle.Format = "dd-MM-yyyy";
                dgvReport.Columns["EndDate"].Width = 150;
                dgvReport.Columns["EndDate"].DefaultCellStyle.Format = "dd-MM-yyyy";
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
            string dt,ed = "";
            try
            {
                
                dt = dgvReport.SelectedRows[0].Cells["StartDate"].Value.ToString();
                ed = dgvReport.SelectedRows[0].Cells["EndDate"].Value.ToString();
                cbbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                txtPeriod.Text = dgvReport.SelectedRows[0].Cells["Period"].Value.ToString();
                dtpStart.Value = Convert.ToDateTime(dt);
                dtpEnd.Value = Convert.ToDateTime(ed);

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

                        header.Add("Master Data: Production Days");
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                EditMode();                
            }
        }

        void EditMode()
        {
            try
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;

                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                
                dgvReport.Enabled = false;
                btnUpload.Enabled = false;
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
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;

            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            dgvReport.Enabled = true;
            btnUpload.Enabled = true;
            btnExport.Enabled = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string path = "";
            string[] fileheaders, tableheaders;
            string temp = "";
            try
            {
                txtStatus.Text = "Select the file...";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;

                    fileheaders = cm.GetFileHeaders(path);
                    temp = db.GetGlobal("HEADER_PRODNDAY");

                    tableheaders = temp.Split('|');
                    txtStatus.Text = "Check File headers...";
                    if (cm.CheckHeader(fileheaders, tableheaders))
                    {
                        errortitle = "";
                        errorsql = "";
                        if (Import_Data(path, tableheaders))
                        {
                            txtStatus.Text = "Validating data...";
                            if (Validating_Data())
                            {
                                txtStatus.Text = "Saving...";
                                InsertIntoTable();

                                DisplayData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool Import_Data(string path, string[] tableheaders)
        {
            bool ok = false;
            string sql = "";
            StreamReader sr = null;
            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            string header = "";
            string line = "";
            string[] lines = null;
            char delimiter = '\t';
            int rows = 0;
            int counts = 0;
            string sqlval = "";
            string columnnames = "";
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();

                sql = "DELETE FROM TPCS_PRODNDAY_TEMP";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sr = new StreamReader(path);
                header = sr.ReadLine();

                for (int i = 0; i < tableheaders.Length; i++)
                {
                    columnnames = columnnames + tableheaders[i] + ",";
                }
                columnnames = columnnames.Substring(0, columnnames.Length - 1);



                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    line = line.Replace("\"", "");
                    line = line.Replace(",", "");
                    lines = line.Split(delimiter);
                    cm.Quoting(ref lines);

                    sqlval = sqlval + "(";
                    for (int i = 0; i < tableheaders.Length; i++)
                    {
                        sqlval = sqlval + lines[i] + ",";
                    }
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);
                    sqlval = sqlval + "),";
                    rows++;
                    counts++;

                    txtStatus.Text = "Uploading: " + counts.ToString() + " rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_PRODNDAY_TEMP (" + columnnames + ") " +
                            " VALUES " +
                            sqlval;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        rows = 0;
                        sqlval = "";
                    }
                }

                if (rows > 0)
                {
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);

                    sql = "INSERT INTO TPCS_PRODNDAY_TEMP (Plant, Period, StartDate, EndDate) " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }
                trans.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                sr.Close();
                conn.Dispose();
            }
            return ok;
        }

        bool Validating_Data()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();

                sql = "SELECT Plant, Period,StartDate, EndDate FROM TPCS_PRODNDAY_TEMP "+
                    " WHERE ISDATE(CONVERT(DateTime,RTRIM(LTRIM(StartDate)),105))='0' OR ISDATE(CONVERT(DateTime,RTRIM(LTRIM(EndDate)),105))='0'";
                cmd = new SqlCommand(sql, conn);
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Date! View Error.";
                    errorsql = sql;
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        bool InsertIntoTable(bool replace = false)
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;

            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;

                if (replace)
                {
                    sql = "DELETE FROM TPCS_PRODNDAY ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "DELETE t1 FROM TPCS_PRODNDAY t1 INNER JOIN TPCS_PRODNDAY_TEMP t2 on " +
                        " t1.Period=t2.Period and t1.Plant=t2.Plant ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_PRODNDAY (Period, Plant, StartDate,EndDate, UpdateBy, UpdateDate) " +
                        " SELECT Period, Plant, CONVERT(DateTime,LTRIM(RTRIM(StartDate)),105), CONVERT(DateTime,LTRIM(RTRIM(EndDate)),105), '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_PRODNDAY_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_PRODNDAY_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                txtStatus.Text = "Import Finished!";
                ok = true;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Import failed!";
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewMode();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (validate_data())
                {
                    UpdateRecord();
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void UpdateRecord()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = db.GetConnString();
                sql = "UPDATE TPCS_PRODNDAY SET StartDate='" + dtpStart.Value.ToString("yyyy-MM-dd") + "', " +
                    " EndDate='" + dtpEnd.Value.ToString("yyyy-MM-dd") + "' " +
                    " WHERE Period='" + txtPeriod.Text.ToString() +
                    "' AND Plant='" + cbbPlant.SelectedItem.ToString() + "' ";                    

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                DisplayData();
                ViewMode();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        bool validate_data()
        {
            bool result = false;
            try
            {

                if (dtpStart.Value > dtpEnd.Value)
                {
                    MessageBox.Show("Start Date cannot be greater than End Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpStart.Focus();
                    return result;
                }
                result = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return result;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
