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
    public partial class FActLine : Form
    {
        Common cm = new Common();
        database db = new database();
        string errortitle = "", errorsql = "";
        string mac = Environment.MachineName;
        public FActLine()
        {
            InitializeComponent();
        }

        private void FActLine_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);
            GetFilter();
        }
        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("SPCACTLINEFILTER");
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

                    if (field == "PRODUCT")
                    {
                        field = "LEFT(REPLACE(MRPC,'DY','DH'),2) LIKE ";
                    }
                    else if (field == "DESCRIPTION")
                    {
                        field = "MaterialDesc LIKE ";
                    }
                    else
                    {
                        field = field.Replace(" ", "") + " LIKE ";
                    }

                    sql = "SELECT Plant, MRPC, Material, MaterialDesc, ActualLine, UpdateBy, UpdateDate,UpdateMac " +
                        " FROM TPCS_SPC_ACTLINE where " + field + cri;



                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 50;
                    dgvReport.Columns["MRPC"].Width = 40;
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

                        header.Add("Master Data: SPC Actual Line");
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
                    temp = db.GetGlobal("HEADER_SPCACTLINE");

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
                    sql = "DELETE t1 FROM TPCS_SPC_ACTLINE t1 INNER JOIN TPCS_SPC_ACTLINE_TEMP t2 on " +
                        " t1.Plant=t2.Plant AND t1.MRPC=t2.MRPC and t1.Material=t2.Material";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_SPC_ACTLINE (Plant, MRPC,Material,ActualLine,UpdateBy,UpdateDate,UpdateMac) " +
                        " SELECT Plant, MRPC,Material,ActualLine, " +
                        " '" + UserAccount.GetuserID().ToUpper() + "', GETDATE(),'" + mac + "' from TPCS_SPC_ACTLINE_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //update macname

                sql = "UPDATE TPCS_SPC_ACTLINE set MaterialDesc=t2.MaterialDesc from TPCS_SPC_ACTLINE t1 inner join "+
                    " tpcs_mat_model t2 on t1.plant=t2.plant and t1.Material=t2.Material";
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

                sql = "DELETE FROM TPCS_SPC_ACTLINE_TEMP";
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
                        sql = "INSERT INTO TPCS_SPC_ACTLINE_TEMP (" + columnnames + ") " +
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

                    sql = "INSERT INTO TPCS_SPC_ACTLINE_TEMP (" + columnnames + ") " +
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
                sql = "SELECT DISTINCT(Plant) from TPCS_SPC_ACTLINE_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_SPC_ACTLINE_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import SPC Actual Line";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT t1.Plant, LEFT(REPLACE(MRPC,'DY','DH'),2) as 'Product' from TPCS_SPC_ACTLINE_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(MRPC,'DY','DH'),2)=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_SPC_ACTLINE_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(t1.MRPC,'DY','DH'),2)=t2.Product)";
                    errortitle = "Import SPC Actual Line";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Material from TPCS_SPC_ACTLINE_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Material=t2.Material)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Material!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Materials! View Error.";
                    errorsql = "SELECT * from TPCS_SPC_ACTLINE_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Material=t2.Material)";
                    errortitle = "Import SPC Actula Line";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT ActualLine from TPCS_SPC_ACTLINE_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TLINE t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(t1.MRPC,'DY','DH'),2)=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid ProdnLine!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid ProdnLine! View Error.";
                    errorsql = "SELECT * from TPCS_SPC_ACTLINE_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TLINE t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(t1.MRPC,'DY','DH'),2)=t2.Product)";
                    errortitle = "Import SPC Actual Line";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPlant.SelectedIndex >= 0)
                {
                    db.SetProduct(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                    if (cbbProduct.Items.Count > 0)
                    {
                        cbbProduct.SelectedIndex = -1;                        
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbProduct.SelectedIndex >= 0)
                {
                    db.SetFGByProduct(ref cbbMaterial, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbMaterial.Items.Count > 0)
                    {
                        cbbMaterial.Text = "";
                        cbbMaterial.SelectedIndex = -1;
                    }

                    db.SetLine(ref cbbLine, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbLine.Items.Count > 0)
                    {
                        cbbLine.SelectedIndex = 0;
                    }
                }
                else
                {
                    cbbMaterial.Text = "";
                    cbbMaterial.Items.Clear();
                    cbbLine.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    if (AddSPCLine()){

                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool IsValidInput()
        {
            bool ok = true;
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                if (cbbPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }
                if (cbbProduct.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }
                if (cbbMaterial.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Material!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }
                if (cbbLine.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                conn=db.GetConnString();
                sql = "SELECT ActualLine from TPCS_SPC_ACTLINE " +
                    " where Plant='" + cbbPlant.SelectedItem.ToString() + "' AND Material='" + cbbMaterial.SelectedItem.ToString() +
                    "' AND ActualLine='" + cbbLine.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(sql, conn);

                if (cmd.ExecuteScalar() == null)
                {
                }
                else if (cmd.ExecuteScalar().ToString() == "")
                {
                }
                else
                {
                    MessageBox.Show("One material cannot more than one line!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    return ok;
                }
            }
            catch (Exception ex)
            {
                ok = false;
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                DeleteSPCLine();
            }
        }

        void DeleteSPCLine()
        {
            
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn = null;
   
            try
            {
                if (MessageBox.Show("Are you sure to delete this?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn = db.GetConnString();
                    sql = "DELETE FROM TPCS_SPC_ACTLINE WHERE Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' " +
                        " AND Material = '" + dgvReport.SelectedRows[0].Cells["Material"].Value.ToString() + "' " +
                        " AND ActualLine = '" + dgvReport.SelectedRows[0].Cells["ActualLine"].Value.ToString() + "' ";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The line has been deleted!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    dgvReport.Rows.RemoveAt(dgvReport.SelectedRows[0].Index);
                }                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool AddSPCLine()
        {
            bool ok = true;
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans=null;
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "INSERT INTO TPCS_SPC_ACTLINE (Plant, Material,ActualLine, UpdateBy, UpdateDate, UpdateMac) VALUES " +
                    "('" + cbbPlant.SelectedItem.ToString() + "'," +
                    "'" + cbbMaterial.SelectedItem.ToString() + "'," +
                    "'" + cbbLine.SelectedItem.ToString() + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    " GETDATE()," +
                    "'" + mac + "')";
                cmd=new SqlCommand(sql,conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_SPC_ACTLINE set MRPC=t1.MRPC, MaterialDesc=t1.MaterialDesc from tpcs_mat_model t1 "+
                    " inner join tpcs_spc_actline t2 on t1.Plant=t2.Plant and t1.Material=t2.Material where ISNULL(t2.MaterialDesc,'')='' ";
                cmd.CommandText=sql;
                cmd.ExecuteNonQuery();
                trans.Commit();
                MessageBox.Show("The line has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                ok = false;
                trans.Rollback();
                db.SaveError(ex.ToString());
            }
            return ok;
        }

    }
}
