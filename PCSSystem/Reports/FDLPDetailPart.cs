using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace PCSSystem.Reports
{
    public partial class FDLPDetailPart : Form
    {

        bool CanClose = true;
        database db = new database();
        Common cm = new Common();
        string errorsql, errortitle;
        string mac = Environment.MachineName.ToUpper();
        string myPlant = "";
        string myProduct = "";
        string myDLPNo = "";
        string myPrdnPeriod = "", myPrdnSDate = "", myPrdnEDate = "";
        private bool timerOn = true;
        private bool AutoClose = false;

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbProduct.SelectedIndex >= 0)
            {
                db.SetLine(ref cbbLine, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                if (cbbLine.Items.Count > 0)
                {
                    cbbLine.Items.Insert(0, "[ALL]");
                    cbbLine.SelectedIndex = 0;
                }
            }
        }

        string sql;
        DataSet ds;
        DataTable dtBOM = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cbbProduct.Text))
            {
                MessageBox.Show("Please select product first.");
                return;
            }
            if (string.IsNullOrEmpty(cbbLine.Text))
            {
                MessageBox.Show("Please select line first.");
                return;
            }

            LoadDataSet();

            

            if (ds.Tables[1].Rows.Count <= 0)
            {
                dgBom.DataSource = null;
                MessageBox.Show("There is no data.");
            }
                

        }

        void LoadDataSet()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "EXEC spDLP_PartStatus_Test  @Product='" + cbbProduct.SelectedItem.ToString() + "', @plant='" + cbbPlant.SelectedItem.ToString() + "',@Line='" + cbbLine.SelectedItem.ToString() + "',@PlanDate='" + schDate.Text + "',@DataType='IN JEQ'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);

            dgPart.DataSource = null;
            dgPart.DataSource = ds.Tables[0];
            dtBOM = null;
            dtBOM = ds.Tables[1];
        }

        private int AutoCloseTime = 0;
        public FDLPDetailPart()
        {
            InitializeComponent();
        }

        private void FDLPDetailPart_Load(object sender, EventArgs e)
        {
            schDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dgPart.AutoGenerateColumns = false;
            dgBom.AutoGenerateColumns = false;
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShow.Checked == true)
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void cbDetail_CheckedChanged(object sender, EventArgs e)
        {

            dgBom.Columns["SBA1"].Visible = cbDetail.Checked;
            dgBom.Columns["AssyStock"].Visible = cbDetail.Checked;
            dgBom.Columns["PSTStock"].Visible = cbDetail.Checked;
            dgBom.Columns["PSTBalOrder"].Visible = cbDetail.Checked;
            dgBom.Columns["MB03Diff"].Visible = cbDetail.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Export to Excel
            saveFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ds.Tables[0].TableName = "Data";
            ds.Tables[1].TableName = "BOM";
            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(ds.Tables[0].Copy());
            dsExport.Tables.Add(ds.Tables[1].Copy());



            saveFileDialog2.Filter = "Excel 97-2003 Workbook | *.xls";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog2.FileName != "")
                {
                    if (saveFileDialog2.FileName.Contains(".xls"))
                    {
                        cm.DataSetToExcel(dsExport, saveFileDialog2.FileName);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataSet();
            if (ds == null)
            {
                MessageBox.Show("There is no data!");
                return;
            }
            if (ds.Tables[4].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("There is no data for JR / Data already send!");
                return;
            }

            if (string.IsNullOrEmpty(ds.Tables[4].Rows[0][4].ToString()))
            {
                MessageBox.Show("Please check AJR file path in TGlobal!");
                return;
            }

            string FileName = "";
            string FileNames = "";
            DataTable dtExp = new DataTable();
            for (int i=1; i<= Convert.ToInt32(ds.Tables[4].Rows[0][0].ToString()); i++)
            {
                FileName = ds.Tables[4].Rows[0][4].ToString() + ds.Tables[5].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable().Rows[0][1].ToString() + ".XLSX";
                FileNames += ";" + FileName;
                dtExp = null;
                dtExp = ds.Tables[3].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable();
                dtExp.Columns.Remove("SeqNo");
                DataTableToExcel(dtExp, FileName);
            }

            FileNames = FileNames.Substring(1, FileNames.Length - 1);

            SqlConnection conn = null;
            SqlCommand cmd;
            try
            {
                conn = db.GetConnString();
                sql = "exec sp_AJR_MAIL_Test '" + FileNames + "','" + Convert.ToInt32(ds.Tables[4].Rows[0][0].ToString()) + "','" + ds.Tables[4].Rows[0][1].ToString() + "','" + ds.Tables[4].Rows[0][2].ToString() + "','" + ds.Tables[4].Rows[0][3].ToString() + "','" + UserAccount.GetuserID() + "','" + ds.Tables[4].Rows[0][5].ToString() + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("JR Has Been Generated.");
                LoadDataSet();
            }
            catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }        
           


        }


        void DataTableToExcel(DataTable dtExport, string FilePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Application.Workbooks.Add(Type.Missing);

            int shtNo = 1;

                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet;
                if (excelApp.Sheets[shtNo] != null)
                {
                    excelWorkSheet = excelApp.Sheets[shtNo];
                }
                else
                {
                    excelWorkSheet = excelApp.Sheets.Add();
                }
                shtNo = shtNo + 1;

                for (int i = 1; i < dtExport.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = dtExport.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < dtExport.Rows.Count; j++)
                {
                    for (int k = 0; k < dtExport.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = dtExport.Rows[j].ItemArray[k].ToString();
                    }
                }

            excelWorkSheet.Columns.AutoFit();
            excelApp.ActiveWorkbook.SaveCopyAs(FilePath);
            excelApp.ActiveWorkbook.Saved = true;

            excelApp.Quit();
            //if (DialogResult.Yes == MessageBox.Show("Your excel file exported successfully at " + FilePath + Environment.NewLine + "Do you wont to open file?", "Export Data-" + DateTime.Now.ToString(), MessageBoxButtons.YesNo))
            //{
            //    if (System.IO.File.Exists(FilePath))
            //    {
            //        System.Diagnostics.Process.Start(FilePath);
            //    }
            //}

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                grdSummary.DataSource = ds.Tables[2];
                grdSummary.Visible = true;
            }
            else
            {
                grdSummary.Visible = false;
            }
        }

        private void dgPart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgPart_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgPart.SelectedRows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dgPart.CurrentRow.Cells[0].Value.ToString()))
                {
                    if (dtBOM != null)
                    {
                        DataView dataView = dtBOM.DefaultView;
                        string filter = "";
                        filter = "RecordId = '" + dgPart.CurrentRow.Cells[0].Value.ToString() + "'";
                        dataView.RowFilter = filter;
                        dgBom.DataSource = dataView;
                    }
                }
            }



        }

        private void dgBom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["MB03Bal"].Value.ToString()) - (-Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["PSTDiffQty"].Value.ToString())) < 0)
                    dgBom.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["PSTDiffQty"].Value.ToString()) < 0)
                    dgBom.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgPart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgPart.Rows[e.RowIndex].Cells["Remark"].Value.ToString() == "Critical Part")
                    dgPart.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
    }
}
