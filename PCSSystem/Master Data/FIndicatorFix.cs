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
using System.Diagnostics;
using System.IO;

namespace PCSSystem.Master_Data
{
    public partial class FIndicatorFix : Form
    {
        bool NewRecord = false;
        Common cm = new Common();
        database db = new database();
        string MacName = System.Environment.MachineName;
        string appPath = Properties.Settings.Default.AppPath.ToUpper();
        SqlDataAdapter adapter;
        SqlConnection conn;
        string sql = "";
        SqlCommand cmd;
        SqlDataReader reader;
        void AddMode()
        {
            try
            {
                cbbPlant.Enabled = true;
                cbbProduct.Enabled = true;
                cbbLine.Enabled = true;
                cbbModel.Enabled = true;
                cbbFGCode.Enabled = true;
                txtFGDesc.Text = "";

                cbbIndicatorType.Enabled = true;
                txtLotInd.Enabled = true;
                dpStartDate.Enabled = true;
                dpStartTime.Enabled = true;

                dpEndDate.Enabled = true;
                dpEndTime.Enabled = true;

                cbbLine.SelectedIndex = -1;
                cbbModel.SelectedIndex = -1;
                cbbFGCode.SelectedIndex = -1;
                txtFGDesc.Text = "";
                cbbIndicatorType.SelectedIndex = -1;
                txtLotInd.Text = "";

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

                if (cbbLine.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                if (cbbModel.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                if (cbbFGCode.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                if (cbbIndicatorType.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Line!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;

                if (Convert.ToDateTime(PeriodStart) >= Convert.ToDateTime(PeriodEnd))
                {
                    MessageBox.Show("Effective Start must be before End Date!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ok = false;
                    return ok;
                }

                if (Convert.ToDateTime(PeriodEnd) < Convert.ToDateTime(DateTime.Now))
                {
                    MessageBox.Show("Effective End must be before Date now!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string ReqNumber = GetNewReqNumber();

            string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
            string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;

            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = @"INSERT INTO TPCS_INDICATIONFIX (Period, ReqNo, Plant , Product, Line, ProductGroup, Material, MaterialDesc, IndicationType, FGLotInd, Status, EffectiveStart, EffectiveEnd, SubmittedBy, SubmittedDate) VALUES 
                (REPLACE(LEFT(CONVERT(DATE,GETDATE()),7),'-','')
                ,'" + ReqNumber + @"'
                ,'" + cbbPlant.SelectedItem.ToString() + @"'
                ,'" + cbbProduct.SelectedItem.ToString() + @"'
                ,'" + cbbLine.SelectedItem.ToString() + @"'
                ,'" + cbbModel.SelectedItem.ToString() + @"'
                ,'" + cbbFGCode.SelectedItem.ToString() + @"'
                ,'" + txtFGDesc.Text.ToString() + @"'
                ,'" + cbbIndicatorType.SelectedItem.ToString() + @"'
                ,'" + txtLotInd.Text.ToString() + @"'
                ,'OPEN'
                ,CONVERT(DATETIME, '"+ PeriodStart + @"', 101)
                ,CONVERT(DATETIME, '"+ PeriodEnd + @"', 101)
                ,'" + UserAccount.GetuserID() + @"'
                ,GETDATE()
                )";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Indication has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
            string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;

            try
            {
                conn = db.GetConnString();
                sql = @"UPDATE TPCS_INDICATIONFIX SET 
                        IndicationType='"+cbbIndicatorType.SelectedItem.ToString()+ @"'
                        ,FGLotInd='" + txtLotInd.Text.ToString()+@"'
                        ,EffectiveStart=CONVERT(DATETIME, '" + PeriodStart + @"', 101)
                        ,EffectiveEnd=CONVERT(DATETIME, '" + PeriodEnd + @"', 101)
                        ,Updatedby='" + UserAccount.GetuserID() + @"'
                        ,UpdatedDate=GETDATE()
                        WHERE ID='" + dgvReport.SelectedRows[0].Cells["ID"].Value.ToString() + @"'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Indication has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cbbPlant.Enabled = false;
            cbbProduct.Enabled = false;
            cbbLine.Enabled = false;
            cbbModel.Enabled = false;
            cbbFGCode.Enabled = false;

            cbbIndicatorType.Enabled = false;
            txtLotInd.Enabled = false;
            dpStartDate.Enabled = false;
            dpStartTime.Enabled = false;

            dpEndDate.Enabled = false;
            dpEndTime.Enabled = false;

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
                cbbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                cbbProduct.SelectedItem = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                cbbLine.SelectedItem = dgvReport.SelectedRows[0].Cells["Line"].Value.ToString();
                cbbModel.SelectedItem = dgvReport.SelectedRows[0].Cells["ProductGroup"].Value.ToString();
                cbbFGCode.SelectedItem = dgvReport.SelectedRows[0].Cells["Material"].Value.ToString();
                txtFGDesc.Text = dgvReport.SelectedRows[0].Cells["MaterialDesc"].Value.ToString();
                cbbIndicatorType.SelectedItem = dgvReport.SelectedRows[0].Cells["IndicationType"].Value.ToString();
                txtLotInd.Text = dgvReport.SelectedRows[0].Cells["FGLotInd"].Value.ToString();

                dpStartDate.Text = dgvReport.SelectedRows[0].Cells["EffectiveStart"].Value.ToString();
                dpEndDate.Text = dgvReport.SelectedRows[0].Cells["EffectiveEnd"].Value.ToString();

                dpStartTime.Text = dgvReport.SelectedRows[0].Cells["EffectiveStart"].Value.ToString();
                dpEndTime.Text = dgvReport.SelectedRows[0].Cells["EffectiveEnd"].Value.ToString();
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

                sql = @"SELECT 
                        ID,
                        Plant,
                        Product,
                        Line, 
                        ProductGroup, 
                        Material, 
                        MaterialDesc, 
                        IndicationType, 
                        FGLotInd,
                        Status, 
                        EffectiveStart=convert(varchar, EffectiveStart, 120), 
                        EffectiveEnd=convert(varchar, EffectiveEnd, 120), 
                        SubmittedBy, 
                        SubmittedDate=convert(varchar, SubmittedDate, 120) 
                        FROM TPCS_INDICATIONFIX
                        ORDER BY SubmittedDate DESC";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;

                #region formatgrid
                dgvReport.Columns["ID"].Visible = false;
                //dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        public FIndicatorFix()
        {
            InitializeComponent();
        }
        void EditMode()
        {
            try
            {
                cbbPlant.Enabled = false;
                cbbProduct.Enabled = false;
                cbbLine.Enabled = false;
                cbbModel.Enabled = false;
                cbbFGCode.Enabled = false;

                cbbIndicatorType.Enabled = true;
                txtLotInd.Enabled = true;
                dpStartDate.Enabled = true;
                dpStartTime.Enabled = true;

                dpEndDate.Enabled = true;
                dpEndTime.Enabled = true;

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
                sql = "DELETE FROM TPCS_INDICATIONFIX " +
                    " WHERE " +
                    " ID='" + dgvReport.SelectedRows[0].Cells["ID"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Indication has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        header.Add("Master Data: PCS-Fix Indication");
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
        private void FIndicatorFix_Load(object sender, EventArgs e)
        {
            ViewMode();
            DisplayData();

            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }

            SetOptions();
            cbbIndicatorType.SelectedIndex = -1;

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
                cbbLine.Text = "";
                cbbModel.Text = "";
                cbbFGCode.Text = "";
                txtFGDesc.Text = "";

                if (cbbProduct.SelectedIndex >= 0)
                {
                    db.SetLine(ref cbbLine, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbLine.Items.Count > 0)
                    {
                        cbbLine.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLine.SelectedIndex >= 0)
            {
                cbbModel.Text = "";
                cbbFGCode.Text = "";
                txtFGDesc.Text = "";
                db.SetModelLine(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString(), cbbLine.SelectedItem.ToString());
                if (cbbModel.Items.Count > 0)
                {
                    cbbModel.SelectedIndex = -1;
                }
            }
        }

        private void cbbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModel.SelectedIndex >= 0)
            {
                cbbFGCode.Text = "";
                txtFGDesc.Text = "";
                db.SetFG(ref cbbFGCode, cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                if (cbbFGCode.Items.Count > 0)
                {
                    cbbFGCode.SelectedIndex = -1;
                }
            }
        }

        private void cbbFGCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbFGCode.SelectedIndex >= 0)
                {
                    txtFGDesc.Text = db.SetFGName(cbbPlant.SelectedItem.ToString(), cbbFGCode.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SetOptions()
        {
            ArrayList result = new ArrayList();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT IndicationType FROM TPCS_INDICATIONTYPE";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbIndicatorType.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                conn.Dispose();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbIndicatorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                if (cbbIndicatorType.SelectedIndex >= 0)
                {
                    conn = db.GetConnString();
                    sql = "SELECT Indicator FROM TPCS_INDICATIONTYPE where IndicationType='" + cbbIndicatorType.SelectedItem.ToString() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if (cmd.ExecuteScalar() == null)
                        result = "";
                    else
                        result = cmd.ExecuteScalar().ToString();
                    conn.Dispose();

                    txtLotInd.Text = result;

                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        string GetNewReqNumber()
        {
            string result = "";
            string _dplant, _dproduct;

            _dplant = cbbPlant.SelectedItem.ToString();
            _dproduct = cbbProduct.SelectedItem.ToString();

            try
            {
                conn = db.GetConnString();
                sql = @"SELECT MAX(ReqNo) FROM TPCS_INDICATIONFIX
                        WHERE
                        Plant = '" + _dplant + @"'
                        AND Product = '" + _dproduct + @"'
                        AND YEAR(Period+'' + '01')= YEAR(GETDATE())";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    result = "1";
                else if (cmd.ExecuteScalar().ToString() == "")
                    result = "1";
                else
                    result = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1).ToString();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd2 = new SaveFileDialog();
            sfd2.Filter = "Excel Documents (.xlsx)|.xlsx";
            sfd2.FileName = "TemplateFixIndicatorL";
            DataTable table2 = new DataTable("TemplateFixIndicatorL");
            try
            {

                if (sfd2.ShowDialog() == DialogResult.OK)
                {
                    conn = db.GetConnString();
                    string sql = @"SELECT TOP 5 Plant, Product, Line, ProductGroup, Material, MaterialDesc, IndicationType, FGLotInd, Status,EffectiveStart=convert(varchar,EffectiveStart, 20)
                                    , EffectiveEnd=convert(varchar,EffectiveEnd, 20) FROM TPCS_INDICATIONFIX";
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(table2);

                    table2.TableName = "TemplateFixIndicatorL";
                    using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
                    {
                        wb.Worksheets.Add(table2, "Sheet1");
                        wb.SaveAs(sfd2.FileName);
                    }

                    if (MessageBox.Show("Export completed, Would you like to open the file?", "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start(sfd2.FileName);
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = Path.GetFileName(openFileDialog1.FileName);
                        string ext = Path.GetExtension(fileName);

                        string fileSavePath = Path.Combine(appPath + "MasterData", "MasterDataFixIndicator"+ ext);
                        File.Copy(openFileDialog1.FileName, fileSavePath, true);
                        Upload(appPath + "MasterData", "MasterDataFixIndicator" + ext);
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void Upload(string appPath, string strfileName)
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "EXEC SP_PCS_UPLOAD_FIX_INDICATOR '"+ appPath + @"\" + strfileName + @"','" + UserAccount.GetuserName() + @"'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Success Upload Dix Indicator!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
                DisplayData();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
    }
}
