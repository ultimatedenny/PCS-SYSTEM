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

namespace PCSSystem.Reports
{
    public partial class FOstdReq : Form
    {
        Common cm = new Common();
        database db = new database();
        string sql;
        DataSet ds;
        string errortitle = "", errorsql = "";
        string mypart;


        public FOstdReq()
        {
            InitializeComponent();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDataset();
            if(ds == null)
            {
                MessageBox.Show("There is no data !");
                return;

            }
            if (ds.Tables[2].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("There is no data for JR / Data already send!");
                return;
            }

            if (string.IsNullOrEmpty(ds.Tables[2].Rows[0][4].ToString()))
            {
                MessageBox.Show("Please check AJR file path in TGlobal!");
                return;
            }

            string FileName = "";
            string FileNames = "";
            DataTable dtExp = new DataTable();
            for (int i = 1; i <= Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString()); i++)
            {
                FileName = ds.Tables[2].Rows[0][4].ToString() + ds.Tables[3].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable().Rows[0][1].ToString() + ".XLSX";
                FileNames += ";" + FileName;
                dtExp = null;
                dtExp = ds.Tables[1].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable();
                dtExp.Columns.Remove("SeqNo");
                dtExp.Columns.Remove("GrpNo");
                DataTableToExcel(dtExp, FileName);
            }

            FileNames = FileNames.Substring(1, FileNames.Length - 1);

            SqlConnection conn = null;
            SqlCommand cmd;
            try
            {
                conn = db.GetConnString();
                sql = "exec sp_AJROSTD_MAIL_Test '" + FileNames + "','" + Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString()) + "','" + ds.Tables[2].Rows[0][1].ToString() + "','" + ds.Tables[2].Rows[0][2].ToString() + "','" + ds.Tables[2].Rows[0][3].ToString() + "','" + UserAccount.GetuserID() + "','" + ds.Tables[2].Rows[0][5].ToString() + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("JR Has Been Generated.");
                LoadDataset();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }


        }

        private void cbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetProduct(ref cbProduct, cbPlant.Text);
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProduct.SelectedIndex >= 0)
            {
                db.SetLine(ref cbProdLine, cbPlant.SelectedItem.ToString(), cbProduct.SelectedItem.ToString());
                if (cbProdLine.Items.Count > 0)
                {
                    cbProdLine.Items.Insert(0, "[ALL]");
                    cbProdLine.SelectedIndex = 0;
                }
            }
        }

        private void FOstdReq_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbPlant);
            //LoadDataset();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadDataset();
            viewMode();
        }

        private void dgOstdView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgOstdView.Rows[e.RowIndex].Cells["Remark"].Value.ToString() == "No Issue JR")
                    dgOstdView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }

            catch
            {

            }
        }

        private void dgOstdView_SelectionChanged(object sender, EventArgs e)
        {
            if(dgOstdView.SelectedRows.Count > 0)
            {
                //if(dgOstdView.Columns.Contains("PartCode]"))
                mypart = dgOstdView.SelectedRows[0].Cells["PartCode"].Value.ToString();
            }
        }

        void viewDetailMode()
        {
            cbProduct.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            cbPlant.Enabled = true;
            cbProdLine.Enabled = true;
            btnDetail.Enabled = false;
            btnClose.Enabled = false;
        }


        void viewMode()
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            btnDetail.Enabled = true;
            btnClose.Enabled = true;
        }

        void LoadDataset()
        {
            dgOstdView.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = ( "EXEC AJR_OstdList '"+cbProduct.Text+"','"+cbPlant.Text+"','"+cbProdLine.Text+"'");
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dgOstdView.DataSource = ds.Tables[0];
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
            viewDetailMode();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadDetail()
        {
            dgOstdView.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "select Plant,Product,ProdnLine,[Part Code] as PartCode,[Part Name],Qty,DLPNo,PlantDate,PostBy,PostDate from AJR_RequestOstd where isnull([Job Req],'')='' and [Part Code]='" + mypart + "'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dgOstdView.DataSource = ds.Tables[0];
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {

                if (dgOstdView.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Data: Outstanding JR");
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = saveFileDialog1.FileName.ToString();

                        cm.Export_to_CSV(header, path, dgOstdView);
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
    }
}
