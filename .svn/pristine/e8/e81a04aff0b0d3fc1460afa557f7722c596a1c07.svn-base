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
    public partial class FModel : Form
    {
        Common cm = new Common();
        database db = new database();
        string errorsql, errortitle;
        string mac = Environment.MachineName;
        public FModel()
        {
            InitializeComponent();
        }

        private void FModel_Load(object sender, EventArgs e)
        {
            GetFilter();
        }

        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("MATMODELFILTER");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

                    if (field == "PRODUCT")
                    {
                        field = "LEFT(MRPC,2) LIKE ";
                    }
                    else if (field == "DESCRIPTION")
                    {
                        field = "MaterialDesc LIKE ";
                    }
                    else
                    {
                        field = field.Replace(" ", "") + " LIKE ";
                    }

                    sql = "SELECT Plant, MRPC, Material, MaterialDesc, Model, OldMaterialNo, UpdateBy, UpdateDate " +
                        " FROM TPCS_MAT_MODEL where " + field + cri;



                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 50;                    
                    dgvReport.Columns["MRPC"].Width = 40;
                    dgvReport.Columns["Model"].Width = 60;
                    dgvReport.Columns["Model"].HeaderText = "Product Group";
                    dgvReport.Columns["Material"].Width = 100;                    
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

                        header.Add("Master Data: Material vs Product Group");
                        header.Add("Filter by: " + cbbFilter.SelectedItem.ToString());                        
                        header.Add("Criteria: " + txtCriteria.Text.ToUpper());
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
                    temp = db.GetGlobal("HEADER_MATMODEL");

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
                    sql = "DELETE t1 FROM TPCS_MAT_MODEL t1 INNER JOIN TPCS_MAT_MODEL_TEMP t2 on " +
                        " t1.Plant=t2.Plant AND t1.MRPC=t2.MRPC and t1.Material=t2.Material";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_MAT_MODEL (Plant, MRPC,Material,MaterialDesc,OldMaterialNo,Model,UpdateBy,UpdateDate,MacName) " +
                        " SELECT Plant, MRPC,Material,MaterialDesc,OldMaterialNo,Model, " +
                        " '" + UserAccount.GetuserID().ToUpper() + "', GETDATE(),'"+mac+"' from TPCS_MAT_MODEL_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_MODEL";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO TPCS_MODEL (Plant, Product, Model) "+
                    " SELECT DISTINCT Plant, LEFT(REPLACE(MRPC,'DY','DH'),2), Model FROM TPCS_MAT_MODEL";
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

                sql = "DELETE FROM TPCS_MAT_MODEL_TEMP";
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
                        sqlval = sqlval + lines[i].ToString().Replace(",", "") + ",";
                    }

                    sqlval = sqlval.Substring(0, sqlval.Length - 1);
                    sqlval = sqlval + "),";
                    rows++;
                    counts++;

                    txtStatus.Text = "Uploading: " + counts.ToString() + " rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_MAT_MODEL_TEMP (" + columnnames + ") " +
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

                    sql = "INSERT INTO TPCS_MAT_MODEL_TEMP (" + columnnames + ") " +
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
                sql = "SELECT DISTINCT(Plant) from TPCS_MAT_MODEL_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_MAT_MODEL_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import SAP Material vs Model-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT t1.Plant, LEFT(MRPC,2) as 'Product' from TPCS_MAT_MODEL_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(MRPC,'DY','DH'),2)=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_MAT_MODEL_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(t1.MRPC,2)=t2.Product)";
                    errortitle = "Import SAP Material vs Model-Invalid MRPC";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Material from TPCS_MAT_MODEL_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.Material=t2.Procod2)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Materials!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Materials! View Error.";
                    errorsql = "SELECT * from TPCS_MAT_MODEL_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.Material=t2.Procod2)";
                    errortitle = "Import SAP Material vs Model-Invalid Materials";
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
    }
}
