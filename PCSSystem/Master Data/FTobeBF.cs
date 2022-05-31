using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace PCSSystem
{
    public partial class FTobeBF : Form
    {
        Common cm = new Common();
        database db = new database();
        string errorsql, errortitle;
        string mac = Environment.MachineName.ToUpper();
        Boolean uploaded = false;
        public FTobeBF()
        {
            InitializeComponent();
        }

        private void FTobeBF_Load(object sender, EventArgs e)
        {
            GetFilter();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {
                
                if (dgvReport.Rows.Count > 0)
                {
                    dgvReport.Columns[0].Visible = false;
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        header.Add("Master Data: To Be B/F");
                        header.Add("Field: " + cbbFilter.SelectedItem.ToString());
                        header.Add("Criteria: " + txtCriteria.Text.ToString());
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
        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("BFFILTER");
                cbbFilter.Items.AddRange(cri.Split('|'));
                if (cbbFilter.Items.Count > 0)
                {
                    cbbFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtCriteria_TextChanged(object sender, EventArgs e)
        {
            DisplayData();  
        }

        void DisplayData()
        {
            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            string cri = "", field = "";
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();

                if (cbbFilter.SelectedIndex >= 0)
                {
                    field = cbbFilter.SelectedItem.ToString().ToUpper();

                    cri = "'%" + txtCriteria.Text + "%'";

                    if (field == "MATERIAL")
                    {
                        field = "FGCode";
                    }
                    else if (field == "Description")
                    {
                        field = "FGDesc";
                    }
                    
                    sql = "SELECT Period, Plant, Product, ProdnLine, FGCode, FGDesc, Qty, PV, UpdateBy, UpdateDate, MacName from TPCS_BF " +
                        " WHERE "+field+" LIKE "+cri;
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);

                    dgvReport.DataSource = dt;
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 50;
                    dgvReport.Columns["Product"].Width = 50;
                    dgvReport.Columns["ProdnLine"].Width = 40;
                    dgvReport.Columns["FGCode"].Width = 120;
                    dgvReport.Columns["FGCode"].HeaderText = "Material";
                    dgvReport.Columns["FGDesc"].HeaderText = "Description";
                    dgvReport.Columns["Qty"].Width = 60;
                    dgvReport.Columns["Qty"].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns["PV"].Width = 40;
                    dgvReport.Columns["UpdateDate"].Width = 100;
                    dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                    #endregion

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnUplBF_Click(object sender, EventArgs e)
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
                    temp = db.GetGlobal("HEADER_BF");

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
                                InsertIntoTable();
                                
                                uploaded = true;
                            }
                            else
                            {
                                Remove_Data();
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
                    //sql = "DELETE FROM TPCS_ROUTEMP ";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                }
                else
                {
                    //sql = "DELETE t1 FROM TPCS_ROUTEMP t1 INNER JOIN TPCS_ROUTEMP_TEMP t2 on " +
                    //    " t1.Plant=t2.Plant AND t1.Product=t2.Product AND t1.Model=t2.Model AND " +
                    //    " t1.SAPWC=t2.SAPWC";
                    sql = "DELETE t1 FROM TPCS_BF t1 INNER JOIN TPCS_BF_TEMP t2 on " +
                        " t1.Plant=t2.Plant AND t1.Product=t2.Product and t2.MacName='" + mac + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_BF (Plant, Product,FGCode,FGDesc,ProdnLine,Qty,PV,MacName) " +
                        " SELECT Plant, Product,FGCode,FGDesc,ProdnLine,Qty,PV,MacName " +
                        " from TPCS_BF_TEMP WHERE MacName='" + mac + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_BF SET Period='" + DateTime.Today.ToString("yyyyMMdd") + "', UpdateBy='" + UserAccount.GetuserID().ToString() +
                    "', UpdateDate=GetDate() WHERE MacName='" + mac + "' and Period is null";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE from TPCS_BF_TEMP WHERE MacName='" + mac + "'";
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

                sql = "DELETE FROM TPCS_BF_TEMP WHERE MacName='" + mac + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

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
                        sql = "INSERT INTO TPCS_BF_TEMP (" + columnnames + ") " +
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

                    sql = "INSERT INTO TPCS_BF_TEMP (" + columnnames + ") " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }

                sql = "UPDATE TPCS_BF_TEMP SET MacName='" + mac + "' where ISNULL(MacName,'') =''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
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
                sql = "SELECT DISTINCT(Plant) from TPCS_BF_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import Tobe B/F-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product from TPCS_BF_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                    errortitle = "Import Tobe B/F-Invalid Products";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT FGCode from TPCS_BF_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.FGCode=t2.Procod2)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid FG Code!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid FG Code! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.FGCode=t2.Procod2)";
                    errortitle = "Import Tobe B/F-Invalid FG Codes";
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

        bool Remove_Data()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE FROM TPCS_BF WHERE MacName='" + mac + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public Boolean IsUploaded()
        {
            return uploaded;
        }
       
    }
}
