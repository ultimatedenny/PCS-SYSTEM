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
    public partial class FNonWD : Form
    {
        bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string errortitle = "",errorsql = "";
        public FNonWD()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FNonWD_Load(object sender, EventArgs e)
        {
            DisplayData();
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

                sql = "SELECT Date, Holiday, UpdateBy, UpdateDate From TPCS_NONWORKDAY "+
                    " where LEFT(PcsDate,2)='"+DateTime.Now.ToString("yy")+"' "+
                    " ORDER BY PCSDate ";
                
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                
                dgvReport.DataSource = dt;


                #region formatgrid
                dgvReport.Columns["Holiday"].Width = 250;
                dgvReport.Columns["Holiday"].HeaderText = "Description";
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

                        header.Add("Master Data: Non Working Day");
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

        private void btnImport_Click(object sender, EventArgs e)
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
                    temp = db.GetGlobal("HEADER_NONWD");

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
                    sql = "DELETE FROM TPCS_NONWORKDAY ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "DELETE t1 FROM TPCS_NONWORKDAY t1 INNER JOIN TPCS_NONWORKDAY_TEMP t2 on " +
                        " t1.Date=t2.Date ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_NONWORKDAY (Date, Holiday, UpdateBy, UpdateDate) " +
                        " SELECT Date,Holiday, '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_NONWORKDAY_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_NONWORKDAY SET PCSDate=RIGHT(Date,2)+SUBSTRING(Date,4,2)+LEFT(Date,2) WHERE ISNULL(PCSDate,'') = ''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_NONWORKDAY_TEMP";
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

                sql = "DELETE FROM TPCS_NONWORKDAY_TEMP";
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
                        sql = "INSERT INTO TPCS_NONWORKDAY_TEMP (" + columnnames + ") " +
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

                    sql = "INSERT INTO TPCS_NONWORKDAY_TEMP (Date, Holiday) " +
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
                
                sql = "UPDATE TPCS_NONWORKDAY_TEMP SET PCSDate=RIGHT(Date,2)+SUBSTRING(Date,4,2)+LEFT(Date,2)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "SELECT Date,Holiday FROM TPCS_NONWORKDAY_TEMP WHERE ISDATE(RTRIM(LTRIM(PCSDATE)))='0'";
                cmd.CommandText = sql;                
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Date! View Error.";
                    errorsql = "SELECT Date,Holiday FROM TPCS_NONWORKDAY_TEMP WHERE ISDATE(PCSDATE)='0'";
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

        private void txtStatus_Click(object sender, EventArgs e)
        {
            if (!(errortitle == ""))
            {
                FInfo f = new FInfo(errortitle, errorsql);
                f.ShowDialog();
                f.Dispose();
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
            string dt = "";
            try
            {
                //Date, Holiday, UpdateBy, UpdateDate 
                dt = dgvReport.SelectedRows[0].Cells["Date"].Value.ToString();
                
                dtpDate.Value = Convert.ToDateTime(dt.Substring(6) + "-" + dt.Substring(3, 2) + "-" + dt.Substring(0, 2));
                txtHoliday.Text = dgvReport.SelectedRows[0].Cells["Holiday"].Value.ToString();

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

        void AddMode()
        {
            try
            {
                dtpDate.Value = DateTime.Today;
                txtHoliday.Text = "";

                dtpDate.Enabled = true;
                txtHoliday.Enabled = true;

                btnAdd.Visible = false;
                btnCancel.Visible = true;
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                dgvReport.Enabled = false;
                btnImport.Enabled = false;
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
                dtpDate.Enabled = true;
                txtHoliday.Enabled = true;

                btnAdd.Enabled= false;
                btnSave.Enabled = true;
                btnEdit.Visible = false;
                btnCancelE.Visible = true;
                btnDelete.Enabled = false;
                dgvReport.Enabled = false;
                btnImport.Enabled = false;
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
            dtpDate.Enabled = false;
            txtHoliday.Enabled = false;

            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            btnCancel.Visible = true;
            btnSave.Enabled = false;
            btnEdit.Enabled= true;
            btnEdit.Visible = true;
            btnCancelE.Visible = true;
            btnDelete.Enabled = true;
            dgvReport.Enabled = true;
            btnImport.Enabled = true;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
                DeleteRecord();
        }


        void SaveNewRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_NONWORKDAY (Date,PCSDate,Holiday,UpdateBy,UpdateDate) VALUES " +
                    "(" +
                    "'" + dtpDate.Value.ToString("dd.MM.yyyy") + "'," +
                    "'" + dtpDate.Value.ToString("yyMMdd") + "'," +
                    "'" + txtHoliday.Text + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    "GETDATE()" +
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Nonworking Day has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sql = "UPDATE TPCS_NONWORKDAY SET " +
                    " Date='" + dtpDate.Value.ToString("dd.MM.yyyy") + "'," +
                    " Holiday='" + txtHoliday.Text + "'," +
                    " PCSDate='" + dtpDate.Value.ToString("yyMMdd") + "'," +
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE()" +
                    " WHERE " +
                    " Date='" + dgvReport.SelectedRows[0].Cells["Date"].Value.ToString() + "'";                    

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Nonworking Day has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
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
                sql = "DELETE TPCS_NONWORKDAY " +
                    " WHERE " +                    
                    " Date='" + dgvReport.SelectedRows[0].Cells["Date"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Nonworking Day has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
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
                

                if (txtHoliday.Text.Length == 0)
                {
                    MessageBox.Show("Please input the Holiday Description!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoliday.Focus();
                    ok = false;
                    return ok;
                }


                if (!NewRecord)
                {
                    bool pkchanged = true;
                    string sql = "";
                    SqlCommand cmd;
                    SqlConnection conn;


                    if (dgvReport.SelectedRows[0].Cells["Date"].Value.ToString() == dtpDate.Value.ToString("dd.MM.yyyy"))                      
                        pkchanged = false;

                    conn = db.GetConnString();
                    sql = "SELECT COUNT(Date) from TPCS_NONWORKDAY where Date='" + dtpDate.Value.ToString("dd.MM.yyyy") + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated Holiday Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    sql = "SELECT COUNT(Date) from TPCS_NONWORKDAY where Date='" + dtpDate.Value.ToString("dd.MM.yyyy") + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0))
                    {
                        MessageBox.Show("Duplicated Holiday Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
