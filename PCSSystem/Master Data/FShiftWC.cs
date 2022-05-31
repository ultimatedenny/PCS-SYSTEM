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
    public partial class FShiftWC : Form
    {

        Common cm = new Common();
        database db = new database();
        string errorsql, errortitle;
        string Status = "";

        public FShiftWC()
        {
            InitializeComponent();

        }

        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("WCSHIFTFILTER");
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

       

        private void FShiftWC_Load(object sender, EventArgs e)
        {
            try
            {
                db.SetPlant(ref cbbPlant);
                if (cbbPlant.Items.Count > 0)
                {
                    cbbPlant.SelectedIndex = 0;
                }

                GetFilter();
                LoadPeriod();
            }catch(Exception ex){
                db.SaveError(ex.ToString());
            }
        }

        void LoadPeriod()
        {
            string sql="";
            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader reader;
            try
            {
                conn = db.GetConnString();
                sql = "SELECT DISTINCT(Period) from tpcs_wc_shift where Period >= '"+DateTime.Now.AddMonths(-1).ToString("yyyyMM")+"'";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cbbPeriod.Items.Add(reader[0].ToString());
                }

                if (cbbPeriod.Items.Count == 0)
                {
                    cbbPeriod.Items.Add(DateTime.Now.ToString("yyyyMM"));
                }

                DateTime dt;
                string temp;
                int n = cbbPeriod.Items.Count;
                for (int i = 1; i <= 8-n; i++)
                {
                    temp = cbbPeriod.Items[cbbPeriod.Items.Count - 1].ToString() + "01 00:00:00";
                    temp = temp.Insert(4, "-");
                    temp = temp.Insert(7, "-");
                    dt = Convert.ToDateTime(temp);
                    cbbPeriod.Items.Add(dt.AddMonths(1).ToString("yyyyMM"));
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayData()
        {
            string sql = "";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conn;
            string field="", cri="";
            try
            {
                conn = db.GetConnString();
                field = cbbFilter.SelectedItem.ToString();
                field = "t1." + field;
                cri = " LIKE '%" + txtCriteria.Text + "%'";
                sql = "SELECT Period, t1.Plant, t1.Product, ProdnLine, LineDesc, ShiftRun, UpdateBy, UpdateDate "+
                    " FROM TPCS_WC_SHIFT t1 LEFT JOIN TLINE t2 on t1.Plant = t2.Plant and t1.Product=t2.Product and t1.ProdnLine = t2.LineId "+
                    " WHERE "+field+cri;
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: "+dgvReport.Rows.Count.ToString();

                #region formatgrid
                dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion

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

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPlant.SelectedIndex >= 0)
                {
                    db.SetProduct(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                    if (cbbProduct.Items.Count > 0)
                    {
                        cbbProduct.SelectedIndex = 0;
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
                    db.SetLine(ref cbbProdnLine, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbProdnLine.Items.Count > 0)
                    {
                        cbbProdnLine.SelectedIndex = 0;
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

                        header.Add("Master Data: Work Center vs Shifts");                     
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
                    temp = db.GetGlobal("HEADER_SHIFTWC");

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

                sql = "DELETE FROM TPCS_WC_SHIFT_TEMP";
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
                        sql = "INSERT INTO TPCS_WC_SHIFT_TEMP (" + columnnames + ") " +
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

                    sql = "INSERT INTO TPCS_WC_SHIFT_TEMP (Period, Plant, Product, ProdnLine, ShiftRun) " +
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
                sql = "SELECT DISTINCT(Period) from TPCS_WC_SHIFT_TEMP t1 " +
                    " WHERE t1.Period < '"+DateTime.Today.ToString("yyyyMM")+"'";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Period! Periods cannot be less than this month!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Period! View Error.";
                    errorsql = "SELECT * from TPCS_WC_SHIFT_TEMP t1 " +
                    " WHERE t1.Period < '" + DateTime.Today.ToString("yyyyMM") + "'";
                    errortitle = "Import Work Center and Shifts-Invalid Period";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT(Plant) from TPCS_WC_SHIFT_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_WC_SHIFT_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import Work Center and Shifts-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product from TPCS_WC_SHIFT_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_WC_SHIFT_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                    errortitle = "Import Work Center and Shifts-Invalid Products";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product, ProdnLine from TPCS_WC_SHIFT_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TLINE t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product and t1.ProdnLine=t2.LineID)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Prodn. Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Prodn. Line! View Error.";
                    errorsql = "SELECT * from TPCS_WC_SHIFT_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TLINE t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product and t1.ProdnLine=t2.LineID)";
                    errortitle = "Import Work Center and Shifts v"+
                        "-Invalid Prodn. Line";
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
                    sql = "DELETE FROM TPCS_WC_SHIFT ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "DELETE t1 FROM TPCS_WC_SHIFT t1 INNER JOIN TPCS_WC_SHIFT_TEMP t2 on " +
                        " t1.Period=t2.Period and t1.Plant=t2.Plant AND t1.Product=t2.Product AND t1.ProdnLine=t2.ProdnLine ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_WC_SHIFT (Period, Plant, Product,ProdnLine, ShiftRun, UpdateBy, UpdateDate) " +
                        " SELECT Period, Plant, Product, ProdnLine, ShiftRun, '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_WC_SHIFT_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_WC_SHIFT_TEMP";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
        }

        void AddMode()
        {
            try
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                Status = "ADD";

                cbbPeriod.Enabled = true;
                cbbPlant.Enabled = true;
                cbbProduct.Enabled = true;
                cbbProdnLine.Enabled = true;
                cbbNoShifts.Enabled = true;

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
                if (dgvReport.SelectedRows.Count > 0)
                {
                    btnAdd.Visible = false;
                    btnEdit.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    cbbPeriod.Enabled = false;
                    cbbPlant.Enabled = false;
                    cbbProduct.Enabled = false;
                    cbbProdnLine.Enabled = false;
                    cbbNoShifts.Enabled = true;
                    Status = "EDIT";

                    cbbFilter.Enabled = false;
                    txtCriteria.Enabled = false;
                    dgvReport.Enabled = false;

                    if (cbbPeriod.FindStringExact(dgvReport.SelectedRows[0].Cells["Period"].Value.ToString()) >= 0)
                        cbbPeriod.SelectedItem = dgvReport.SelectedRows[0].Cells["Period"].Value.ToString();
                    else
                        cbbPeriod.SelectedIndex = -1;

                    if (cbbPlant.FindStringExact(dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString()) >=0)
                        cbbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                    else
                        cbbPlant.SelectedIndex=-1;
                    if (cbbProduct.FindStringExact(dgvReport.SelectedRows[0].Cells["Product"].Value.ToString()) >= 0)
                        cbbProduct.SelectedItem = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                    else
                        cbbProduct.SelectedIndex = -1;
                    if (cbbProdnLine.FindStringExact(dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString()) >= 0)
                        cbbProdnLine.SelectedItem = dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString();
                    else
                        cbbProdnLine.SelectedIndex = -1;

                    if (cbbNoShifts.FindStringExact(dgvReport.SelectedRows[0].Cells["ShiftRun"].Value.ToString())>=0)
                        cbbNoShifts.SelectedItem = dgvReport.SelectedRows[0].Cells["ShiftRun"].Value.ToString();
                    else
                        cbbNoShifts.SelectedIndex=-1;
                }
              
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        void ViewMode()
        {
            try
            {
                cbbPeriod.Enabled = false;
                cbbPlant.Enabled = false;
                cbbProduct.Enabled = false;
                cbbProdnLine.Enabled = false;
                cbbNoShifts.Enabled = false;
                Status = "VIEW";

                cbbFilter.Enabled = true;
                txtCriteria.Enabled = true;
                dgvReport.Enabled = true;

                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;

                DisplayData();
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode();
            //if (dgvReport.SelectedRows[0].Cells["Period"].Value.ToString() == DateTime.Today.ToString("yyyyMM"))
            //{
            //    EditMode();
            //}
            //else
            //{
            //    MessageBox.Show("You can only edit current period!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteMode();
        }

        void DeleteMode()
        {
            string sql = "";
            SqlCommand cmd = null;
            SqlConnection conn = null;

            try
            {

                if (MessageBox.Show("Do you really want to remove this?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn = db.GetConnString();
                    sql = "DELETE FROM TPCS_WC_SHIFT WHERE Period = +'" + dgvReport.SelectedRows[0].Cells["Period"].Value.ToString() + "' AND " +
                        " Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                        " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                        " ProdnLine='" + dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    DisplayData();
                 }                                
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Status.ToUpper() == "ADD")
            {
                if (validate_data())
                {
                    InsertRecord();
                }
            }
            else if (Status.ToUpper() == "EDIT")
            {
                UpdateRecord();
            }
        }

        void InsertRecord()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_WC_SHIFT (Period,Plant,Product,ProdnLine,ShiftRun,UpdateBy,UpdateDate) VALUES "+
                    " ('"+cbbPeriod.SelectedItem.ToString()+"','"+cbbPlant.SelectedItem.ToString()+"','"+cbbProduct.SelectedItem.ToString()+
                    "','"+cbbProdnLine.SelectedItem.ToString()+"','"+cbbNoShifts.SelectedItem.ToString()+"','"+UserAccount.GetuserID()+"',GETDATE())";
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

        void UpdateRecord()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = db.GetConnString();
                sql = "UPDATE TPCS_WC_SHIFT SET ShiftRun='" + cbbNoShifts.SelectedItem.ToString() + "' WHERE Period='" + cbbPeriod.SelectedItem.ToString() +
                    "' AND Plant='" + cbbPlant.SelectedItem.ToString() +
                    "' AND Product='" + cbbProduct.SelectedItem.ToString() +
                    "' AND ProdnLine='" + cbbProdnLine.SelectedItem.ToString() + "'";
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
            string sql = "";
            SqlCommand cmd = null;
            SqlConnection conn = null;
            bool result = false;
            try
            {

                if (cbbPeriod.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Period!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbPlant.Focus();
                    return result;
                }

                if (cbbPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbPlant.Focus();
                    return result;
                }
                if (cbbProduct.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbProduct.Focus();
                    return result;
                }

                if (cbbProdnLine.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Prodn. Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbProdnLine.Focus();
                    return result;
                }

                conn = db.GetConnString();
                sql = "SELECT Period from TPCS_WC_SHIFT WHERE Period='"+cbbPeriod.SelectedItem.ToString()+"' AND "+
                    " Plant='"+cbbPlant.SelectedItem.ToString()+"' AND Product='"+cbbProduct.SelectedItem.ToString()+"' AND "+
                    " ProdnLine = '"+cbbProdnLine.SelectedItem.ToString()+"'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                {
                    result = true;
                }
                else if ((cmd.ExecuteScalar().ToString() != ""))
                {
                    MessageBox.Show("Duplicated Records!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return result;
                }
                else
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewMode();
        }

        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
