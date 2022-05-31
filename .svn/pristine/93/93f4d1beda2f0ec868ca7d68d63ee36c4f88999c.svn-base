using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.SqlClient;

namespace PCSSystem
{
    public partial class FRouteMP : Form
    {
        Common cm = new Common();
        database db = new database();
        string errorsql="", errortitle="";
        bool NewRecord = false;
        public FRouteMP()
        {
            InitializeComponent();
        }

        private void FRouteMP_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.Items.Insert(0, "[ALL]");
                cbbPlant.SelectedIndex = 0;
            }

            db.SetPlant(ref cbbFPlant);
            if (cbbFPlant.Items.Count > 0)
            {
                cbbFPlant.SelectedIndex = 0;
            }
            
            GetFilter();
        }

        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("ROUTEMPFILTER");
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
            try
            {
                string cri = "", field = "";
                if (cbbFilter.SelectedIndex >= 0)
                {
                    field = cbbFilter.SelectedItem.ToString();

                    field = field.Replace(" ", "");
                    cri = txtCriteria.Text.ToUpper();

                    cri = "'%" + cri + "%'";

                    if (cbbPlant.SelectedIndex > 0)
                    {
                        cri = cri + " and Plant like '" + cbbPlant.SelectedItem.ToString() + "'";
                    }
                    else if (cbbPlant.SelectedIndex == 0)
                    {
                        cri = cri + " and Plant like '%'";
                    }


                    dgvReport.DataSource = db.SelectTables("TPCS_ROUTEMP", "Plant, Product, Model, SAPWC as 'ProdnLine',OMH, "+
                        " ManPower,OutputHour,  Efficiency, (OutputHour*Efficiency) as 'O/H', "+
                    "UpdateBy, UpdateDate", field + " LIKE " + cri + " ORDER BY UpdateDate DESC ");
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 50;
                    dgvReport.Columns["Product"].Width = 40;
                    dgvReport.Columns["Model"].Width = 80;
                    dgvReport.Columns["Model"].HeaderText = "Product Group";
                    dgvReport.Columns["ProdnLine"].Width = 40;
                    dgvReport.Columns["OMH"].Width = 40;
                    dgvReport.Columns["OMH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvReport.Columns["OMH"].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns["OutputHour"].Width = 40;
                    dgvReport.Columns["OutputHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvReport.Columns["ManPower"].Width = 40;
                    dgvReport.Columns["ManPower"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvReport.Columns["Efficiency"].Width = 40;
                    dgvReport.Columns["Efficiency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvReport.Columns["O/H"].Width = 60;
                    dgvReport.Columns["O/H"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvReport.Columns["O/H"].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns["O/H"].HeaderText = "SBM-O/H";
                    dgvReport.Columns["UpdateDate"].Width = 80;
                    dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                    #endregion
                    if (dgvReport.SelectedRows.Count>0)
                        DisplayValue();
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

                        header.Add("Master Data: Routing and Man Power");
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
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
            string[] fileheaders,tableheaders;
            string temp="";
            try
            {
                txtStatus.Text = "Select the file...";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;

                    fileheaders = cm.GetFileHeaders(path);
                    temp = db.GetGlobal("HEADER_ROUTEMP");

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

        bool InsertIntoTable(bool replace=false)
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
                    sql = "DELETE FROM TPCS_ROUTEMP ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();                                      
                }
                else
                {
                    sql = "DELETE t1 FROM TPCS_ROUTEMP t1 INNER JOIN TPCS_ROUTEMP_TEMP t2 on "+
                        " t1.Plant=t2.Plant AND t1.Product=t2.Product AND t1.Model=t2.Model AND "+
                        " t1.SAPWC=t2.SAPWC";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_ROUTEMP (Plant, Product, Model, SAPWC, OMH, ManPower,OutputHour, Efficiency, UpdateBy, UpdateDate) " +
                        " SELECT Plant, Product, Model, SAPWC, OMH, ManPower, OutputHour, Efficiency, '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_ROUTEMP_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


                sql = "DELETE FROM TPCS_ROUTEMP_TEMP";
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
            SqlConnection conn=null;
            SqlTransaction trans = null;
            string header="";
            string line="";
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

                sql = "DELETE FROM TPCS_ROUTEMP_TEMP";
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

                    txtStatus.Text = "Uploading: "+counts.ToString()+" rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_ROUTEMP_TEMP ("+columnnames+") " +
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
                    
                    sql = "INSERT INTO TPCS_ROUTEMP_TEMP (Plant, Product, Model, SAPWC, OMH, ManPower, Efficiency) " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }
                sql = "UPDATE TPCS_ROUTEMP_TEMP SET SAPWC=LEFT(SAPWC,2)+RIGHT('0'+RIGHT(SAPWC,LEN(SAPWC)-2),2)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_ROUTEMP_TEMP SET Outputhour=OMH*ManPower";
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
                sql = "SELECT DISTINCT(Plant) from TPCS_ROUTEMP_TEMP t1 "+
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_ROUTEMP_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import Routing and ManPower-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product from TPCS_ROUTEMP_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null)) 
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_ROUTEMP_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                    errortitle = "Import Routing and ManPower-Invalid Products";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product, Model from TPCS_ROUTEMP_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Plant=t2.Plant and t1.Product=LEFT(REPLACE(t2.MRPC,'DY','DH'),2) and t1.Model=t2.Model)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Model!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Model! View Error.";
                    errorsql = "SELECT * from TPCS_ROUTEMP_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Plant=t2.Plant and t1.Product=LEFT(REPLACE(t2.MRPC,'DY','DH'),2) and t1.Model=t2.Model)";
                    errortitle="Import Routing and ManPower-Invalid Models";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product, Model, SAPWC, Efficiency from TPCS_ROUTEMP_TEMP t1 WHERE " +
                       " Efficiency < 1";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Efficiency! Cannot be lower than 1", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Efficiency! Cannot be lower than 1. View Error.";
                    sql = "SELECT DISTINCT Plant, Product, Model, SAPWC, Efficiency from TPCS_ROUTEMP_TEMP t1 WHERE " +
                       " Efficiency < 1";
                    errortitle = "Import Routing and ManPower-Invalid Efficiency. Cannot be lower than 1";
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
            if (!(errortitle==""))
            {
                FInfo f = new FInfo(errortitle, errorsql);
                f.ShowDialog();
                f.Dispose();
            }

        }

        private void cbbFPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbFPlant.SelectedIndex >= 0)
                {
                    db.SetProduct(ref cbbPrd, cbbFPlant.SelectedItem.ToString());
                    if (cbbPrd.Items.Count > 0)
                    {
                        cbbPrd.SelectedIndex = 0;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbPrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPrd.SelectedIndex >= 0)
                {
                    db.SetLine(ref cbbLine, cbbFPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString());
                    if (cbbLine.Items.Count > 0)
                    {
                        cbbLine.SelectedIndex = 0;
                    }

                    db.SetModel(ref cbbModel, cbbFPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString());
                    if (cbbModel.Items.Count > 0)
                    {
                        cbbModel.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayValue()
        {
            try
            {
                if (dgvReport.SelectedRows.Count > 0)
                {
                    cbbFPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                    cbbPrd.SelectedItem = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                    cbbLine.SelectedItem = dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString();


                    txtOMH.Text = Convert.ToInt32(dgvReport.SelectedRows[0].Cells["OMH"].Value).ToString();
                    txtOH.Text = dgvReport.SelectedRows[0].Cells["OutputHour"].Value.ToString();
                    txtMP.Text = dgvReport.SelectedRows[0].Cells["ManPower"].Value.ToString();
                    txtEff.Text = dgvReport.SelectedRows[0].Cells["Efficiency"].Value.ToString();


                    if (cbbModel.FindStringExact(dgvReport.SelectedRows[0].Cells["Model"].Value.ToString().ToUpper().Trim()) >= 0)
                    {
                        cbbModel.SelectedItem = dgvReport.SelectedRows[0].Cells["Model"].Value.ToString().Trim();
                    }
                    else
                    {
                        cbbModel.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            DisplayValue();
        }

        void ViewMode()
        {
            try
            {
                cbbFPlant.Enabled = false ;
                cbbPrd.Enabled = false;
                cbbLine.Enabled = false;
                cbbModel.Enabled = false;
                txtOMH.Enabled = false;
                txtOH.Enabled = false;
                txtMP.Enabled = false;
                txtEff.Enabled = false;

                btnAdd.Enabled = true;
                btnAdd.Visible = true;

                btnCancel.Enabled = false;
                btnCancel.Visible = false;

                btnSave.Enabled = false;
                
                btnEdit.Enabled = true;
                btnEdit.Visible = true;

                btnCancelE.Enabled = false;
                btnCancelE.Visible = false;
                    
                btnDelete.Enabled = true;
                btnImport.Enabled = true;
                btnExport.Enabled = true;

                cbbPlant.Enabled = true;
                cbbFilter.Enabled = true;
                txtCriteria.Enabled = true;
                dgvReport.Enabled = true;

                DisplayData();
                
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
                cbbFPlant.Enabled = true;
                cbbPrd.Enabled = true;
                cbbLine.Enabled = true;
                cbbModel.Enabled = true;
                txtOMH.Enabled = true;
                txtOH.Enabled = true;
                txtMP.Enabled = true;
                txtEff.Enabled = true;

                cbbFPlant.SelectedIndex = -1;
                cbbPrd.SelectedIndex = -1;
                cbbLine.SelectedIndex = -1;
                cbbModel.SelectedIndex = -1;
                txtOMH.Text = "0";
                txtOH.Text = "0";
                txtMP.Text = "0";
                txtEff.Text = "0";


                btnAdd.Enabled = false;
                btnAdd.Visible = false;

                btnCancel.Enabled = true;
                btnCancel.Visible = true;

                btnSave.Enabled = true;

                btnEdit.Enabled = false;
                btnEdit.Visible = true;

                btnCancelE.Enabled = false;
                btnCancelE.Visible = true;

                btnDelete.Enabled = false;
                btnImport.Enabled = false;
                btnExport.Enabled = false;

                cbbPlant.Enabled = false;
                cbbFilter.Enabled = false;
                txtCriteria.Enabled = false;
                dgvReport.Enabled = false;
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
                cbbFPlant.Enabled = true;
                cbbPrd.Enabled = true;
                cbbLine.Enabled = true;
                cbbModel.Enabled = true;
                txtOMH.Enabled = true;
                txtOH.Enabled = true;
                txtMP.Enabled = true;
                txtEff.Enabled = true;

                btnAdd.Enabled = false;
                btnCancel.Enabled = false;

                btnSave.Enabled = true;

                btnEdit.Enabled = false;
                btnEdit.Visible = false;

                btnCancelE.Enabled = true;
                btnCancelE.Visible= true;

                btnDelete.Enabled = false;
                btnImport.Enabled = false;
                btnExport.Enabled = false;

                cbbPlant.Enabled = false;
                cbbFilter.Enabled = false;
                txtCriteria.Enabled = false;
                dgvReport.Enabled = false;

                
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewMode();
        }

        private void btnCancelE_Click(object sender, EventArgs e)
        {
            ViewMode();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode();
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

        void SaveNewRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_ROUTEMP (Plant, Product, SAPWC, Model, OMH, Outputhour, ManPower, Efficiency,UpdateBy,UpdateDate) VALUES " +
                    "("+
                    "'" + cbbFPlant.SelectedItem.ToString() + "'," +
                    "'" + cbbPrd.SelectedItem.ToString() + "'," +
                    "'" + cbbLine.SelectedItem.ToString() + "'," +
                    "'" + cbbModel.SelectedItem.ToString() + "'," +
                    "'" + txtOMH.Text.ToString() + "'," +
                    "'" + txtOH.Text.ToString() + "'," +
                    "'" + txtMP.Text.ToString() + "'," +
                    "'" + txtEff.Text.ToString() + "'," +
                    "'" + UserAccount.GetuserID() + "'," +
                    " GETDATE()" +
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Routing and Manpower has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sql = "UPDATE TPCS_ROUTEMP SET " +
                    " Plant='" + cbbFPlant.SelectedItem.ToString() + "'," +
                    " Product='" + cbbPrd.SelectedItem.ToString() + "'," +
                    " SAPWC='" + cbbLine.SelectedItem.ToString() + "'," +
                    " Model='" + cbbModel.SelectedItem.ToString() + "'," +
                    " OMH='" + txtOMH.Text.ToString() + "'," +
                    " Outputhour='" + txtOH.Text.ToString() + "'," +
                    " Manpower='" + txtMP.Text.ToString() + "'," +
                    " Efficiency='" + txtEff.Text.ToString() + "'," +
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE()" +
                    " WHERE " +
                    " Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                    " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                    " SAPWC='" + dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "' AND " +
                    " Model='" + dgvReport.SelectedRows[0].Cells["Model"].Value.ToString() + "'";
                    
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Routing and Manpower has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
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
                sql = "DELETE TPCS_ROUTEMP " +
                    " WHERE " +
                    " Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                    " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                    " SAPWC='" + dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "' AND " +
                    " Model='" + dgvReport.SelectedRows[0].Cells["Model"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Routing and Manpower has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
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
                if (cbbFPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbFPlant.Focus();
                    ok = false;
                    return ok;
                }

                if (cbbPrd.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbPrd.Focus();
                    ok = false;
                    return ok;
                }

                if (cbbLine.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbLine.Focus();
                    ok = false;
                    return ok;
                }

                if (cbbModel.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Model!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbModel.Focus();
                    ok = false;
                    return ok;
                }

                if (Convert.ToInt32(txtOMH.Text) <= 0)
                {
                    MessageBox.Show("Please input the OMH!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOMH.Focus();
                    ok = false;
                    return ok;
                }

                if (Convert.ToInt32(txtOH.Text) <= 0)
                {
                    MessageBox.Show("Please input the Output per Hour!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOH.Focus();
                    ok = false;
                    return ok;
                }

                if (Convert.ToInt32(txtMP.Text) <= 0)
                {
                    MessageBox.Show("Please input the Man Power!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMP.Focus();
                    ok = false;
                    return ok;
                }

                double n;
                if (!double.TryParse(txtEff.Text, out n))
                {
                    MessageBox.Show("Invalid Efficiency!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEff.Focus();
                    ok = false;
                    return ok;
                }

                //if ((Convert.ToDecimal(txtEff.Text) < 0) || (Convert.ToDecimal(txtEff.Text) > 1))
                //{
                //    MessageBox.Show("Please input the Efficiency between 0.00 to 1.00!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtEff.Focus();
                //    ok = false;
                //    return ok;
                //}

                if (Convert.ToDecimal(txtEff.Text) < 1)
                {
                    MessageBox.Show("Efficiency cannot be lower than 1.00!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEff.Focus();
                    ok = false;
                    return ok;
                }

                if (!NewRecord)
                {
                    bool pkchanged = true;
                    string sql = "";
                    SqlCommand cmd;
                    SqlConnection conn;


                    if (dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() == cbbFPlant.SelectedItem.ToString())
                        if (dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() == cbbPrd.SelectedItem.ToString())
                            if (dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() == cbbLine.SelectedItem.ToString())
                                if (dgvReport.SelectedRows[0].Cells["Model"].Value.ToString() == cbbModel.SelectedItem.ToString())
                                    pkchanged = false;

                    conn = db.GetConnString();
                    sql = "SELECT COUNT(Plant) from TPCS_ROUTEMP where Plant='" + cbbFPlant.SelectedItem.ToString() + "' AND Product = '" +
                        cbbPrd.SelectedItem.ToString() + "' and SAPWC = '" + cbbLine.SelectedItem.ToString() + "' AND Model='" + cbbModel.SelectedItem.ToString() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                    {
                        MessageBox.Show("Duplicated Routing and Man Power!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    sql = "SELECT COUNT(Plant) from TPCS_ROUTEMP where Plant='" + cbbFPlant.SelectedItem.ToString() + "' AND Product = '" +
                        cbbPrd.SelectedItem.ToString() + "' and SAPWC = '" + cbbLine.SelectedItem.ToString() + "' AND Model='" + cbbModel.SelectedItem.ToString() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if (Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0)
                    {
                        MessageBox.Show("Duplicated Routing and Man Power!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?","",MessageBoxButtons.YesNo, MessageBoxIcon.Information) 
                == DialogResult.Yes)
                DeleteRecord();
        }

        private void txtMP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtOH_Leave(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0";
                }
                else
                {
                    t.Text = Convert.ToInt32(t.Text).ToString();
                    txtOH.Text = Math.Ceiling(Convert.ToDouble(t.Text) * Convert.ToDouble(txtOMH.Text)).ToString();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }            
        }

        private void txtOH_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "0")
                {
                    t.Text = "";
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }     
        }

        private void txtEff_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "0")
                {
                    t.Text = "";
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }  
        }

        private void txtEff_Leave(object sender, EventArgs e)
        {
            double n;
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0";
                }
                else
                {
                    if (double.TryParse(t.Text,out n))
                    {
                        t.Text = Convert.ToDouble(t.Text).ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtEff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)) && (!(e.KeyChar == '.')))
            {
                e.Handled = true;
            }
        }

        private void txtMP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtOMH_Leave(object sender, EventArgs e)
        {
            int n;
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0";
                }
                else
                {
                    if (int.TryParse(t.Text, out n))
                    {
                        t.Text = Convert.ToInt32(t.Text).ToString();
                        txtOH.Text = Math.Ceiling(Convert.ToDouble(t.Text) * Convert.ToDouble(txtMP.Text)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtOMH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtOMH_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "0")
                {
                    t.Text = "";
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }   
        }

        

    }
}
