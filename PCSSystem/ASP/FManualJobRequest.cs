using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;

namespace PCSSystem.ASP
{

    public partial class FManualJobRequest : Form
    {
        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        database db = new database();
        Common cm = new Common();
        DataSet ds;
        string sql;

        public FManualJobRequest()
        {
            InitializeComponent();
        }

        public void FManualJobRequest_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
        }

        public void btnUpJR_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    string res = Import_Data_Excel(path, MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                    string strresult = res;
                    if (strresult == "Success".ToUpper())
                    {
                        LoadDataSet(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                        
                        btnCheck.Enabled = false;
                        btnUncheck.Enabled = false;
                        cbbPlant.Enabled = false;
                        cbbProduct.Enabled = false;
                        string ErrNo = MyFunction.Asp_lock(MyGlobal.strIP, "Lock", cbbPlant.Text, cbbProduct.Text, UserAccount.GetuserName());
                        string Err = ErrNo;
                    }
                    else
                    {
                        MessageBox.Show(strresult, "Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        public string Import_Data_Excel(string path, string strip, string strplantname, string strproductname)
        {
            SqlConnection conn = null;
            try
            {
                SqlConnection conns = null;
                conns = db.GetConnString();
                string ConnString;
                ConnString = string.Empty;
                string extension = Path.GetExtension(path);
                string command = "SELECT '" + cbbPlant.Text + "' AS PLANT,'" + cbbProduct.Text + "' AS PRODUCT, [PART CODE] AS PART_CODE, QTY AS QTY, [PART NAME] AS PART_NAME, [JOB REQ] AS JOB_REQ FROM [SHEET1$]";
                if (extension == ".xls")
                {
                    ConnString = string.Format(Excel03ConString, path);
                }
                else if (extension == ".xlsx")
                {
                    ConnString = string.Format(Excel07ConString, path);
                }
                else
                {
                    Exception ex = new Exception();
                    throw ex;
                }

                DataTable dt;
                using (OleDbConnection conne = new OleDbConnection(ConnString))
                {
                    using (OleDbCommand cmde = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            dt = new DataTable();
                            cmde.CommandText = command;
                            cmde.Connection = conne;
                            conne.Open();
                            oda.SelectCommand = cmde;
                            oda.Fill(dt);
                            conne.Close();
                        }
                    }
                }
                if (dt != null)
                {
                    conn = db.GetConnString();
                    string ErrNo = MyFunction.Asp_tmppp57_delete(strplantname, strproductname);
                    //using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
                    //{
                    //    sbc.DestinationTableName = "ASP_JRLOG";
                    //    sbc.ColumnMappings.Add("PLANT", "plant");
                    //    sbc.ColumnMappings.Add("PRODUCT", "product");
                    //    sbc.ColumnMappings.Add("QTY", "tobejr");
                    //    sbc.ColumnMappings.Add("PART_CODE", "material");
                    //    sbc.ColumnMappings.Add("PART_NAME", "materialdesc");
                    //    sbc.ColumnMappings.Add("JOB_REQ", "filename");
                    //    sbc.WriteToServer(dt);
                    //}
                    //using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
                    //{
                    //    sbc.DestinationTableName = "asp_jr_csv";
                    //    sbc.ColumnMappings.Add("PLANT", "plant");
                    //    sbc.ColumnMappings.Add("PRODUCT", "product");
                    //    sbc.ColumnMappings.Add("QTY", "qty");
                    //    sbc.ColumnMappings.Add("PART_CODE", "material");
                    //    sbc.ColumnMappings.Add("PART_NAME", "description");
                    //    sbc.ColumnMappings.Add("JOB_REQ", "csvfilename");
                    //    sbc.WriteToServer(dt);
                    //}

                    using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
                    {
                        sbc.DestinationTableName = "asp_tmppp57";
                        sbc.ColumnMappings.Add("PLANT", "plant");
                        sbc.ColumnMappings.Add("PRODUCT", "product");
                        sbc.ColumnMappings.Add("QTY", "reqqty");
                        sbc.ColumnMappings.Add("PART_CODE", "material");
                        sbc.ColumnMappings.Add("PART_NAME", "materialdesc");
                        sbc.WriteToServer(dt);
                    }

                    dataGridView1.Visible = true;
                    groupgrid.Visible = true;
                    groupBox3.Visible = true;
                    groupBox5.Visible = true;
                    string res = MyFunction.Asp_jr_Manual(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text, path, UserAccount.GetuserName());
                    string strresult = res;
                    return strresult;
                }
                else
                {
                    return "No Result";
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                return "Error on excel file format..!!";
            }
        }

        public void LoadDataSet(string strip, string strplantname, string strproductname)
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "EXEC asp_jr_view '" + strip + "'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].HeaderText = "Plant";
            dataGridView1.Columns[2].HeaderText = "Product";
            dataGridView1.Columns[3].HeaderText = "Material";
            dataGridView1.Columns[4].HeaderText = "Material Desc";
            dataGridView1.Columns[5].HeaderText = "UOM";
            dataGridView1.Columns[6].HeaderText = "Part Category";
            dataGridView1.Columns[7].HeaderText = "Sub Category";
            dataGridView1.Columns[8].HeaderText = "Buffer Stock";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[9].HeaderText = "Req Qty";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].HeaderText = "MB02";
            dataGridView1.Columns[11].HeaderText = "MB03";
            dataGridView1.Columns[12].HeaderText = "PBA1";
            dataGridView1.Columns[13].HeaderText = "SBA1";
            dataGridView1.Columns[14].HeaderText = "OUSTANDING JR FOR INHOUSE";
            dataGridView1.Columns[15].HeaderText = "OUSTANDING JR FOR SUBCON";
            dataGridView1.Columns[16].HeaderText = "WMS Available Stock";
            dataGridView1.Columns[17].HeaderText = "Shorted SBA1";
            dataGridView1.Columns[18].HeaderText = "Safety % SBA1";
            dataGridView1.Columns[19].HeaderText = "Standart Box";
            dataGridView1.Columns[20].HeaderText = "Qty To be Request";
            dataGridView1.Columns[21].HeaderText = "To be JR";
            dataGridView1.Columns[22].HeaderText = "Balance JR";
            dataGridView1.Columns[23].HeaderText = "Balance JR without buffer stock";
            dataGridView1.Columns[24].Visible = false;
            lblRows.Text = "Total Rows: " + dataGridView1.Rows.Count.ToString();
            string valu = "NOT SET";
            int countNotset = 0;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                dataGridView1.Rows[i].Cells[4].ReadOnly = true;
                dataGridView1.Rows[i].Cells[5].ReadOnly = true;
                dataGridView1.Rows[i].Cells[6].ReadOnly = true;
                dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                dataGridView1.Rows[i].Cells[8].ReadOnly = true;
                dataGridView1.Rows[i].Cells[9].ReadOnly = true;
                dataGridView1.Rows[i].Cells[10].ReadOnly = true;
                dataGridView1.Rows[i].Cells[11].ReadOnly = true;
                dataGridView1.Rows[i].Cells[12].ReadOnly = true;
                dataGridView1.Rows[i].Cells[13].ReadOnly = true;
                dataGridView1.Rows[i].Cells[14].ReadOnly = true;
                dataGridView1.Rows[i].Cells[15].ReadOnly = true;
                dataGridView1.Rows[i].Cells[16].ReadOnly = true;
                dataGridView1.Rows[i].Cells[17].ReadOnly = true;
                dataGridView1.Rows[i].Cells[18].ReadOnly = true;
                dataGridView1.Rows[i].Cells[19].ReadOnly = true;
                dataGridView1.Rows[i].Cells[20].ReadOnly = true;
                dataGridView1.Rows[i].Cells[21].ReadOnly = true;
                dataGridView1.Rows[i].Cells[22].ReadOnly = true;
                dataGridView1.Rows[i].Cells[23].ReadOnly = true;

                if (dataGridView1.Rows[i].Cells[20].Value != null)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[21].Value) <= 0 || dataGridView1.Rows[i].Cells[7].Value.ToString() == "NOT SET")
                    {
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                        for (int ih = 0; ih < dataGridView1.Columns.Count; ih++)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                    }
                }
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[17].Value) < 0)
                {
                    dataGridView1.Rows[i].Cells[17].Style.ForeColor = Color.Red;
                }
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[18].Value) < 0)
                {
                    dataGridView1.Rows[i].Cells[18].Style.ForeColor = Color.Red;
                }
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[23].Value) < 0)
                {
                    dataGridView1.Rows[i].Cells[23].Style.ForeColor = Color.Red;
                }
                if (dataGridView1.Rows[i].Cells[6].Value.ToString() == valu.ToString())
                {
                    dataGridView1.Rows[i].Cells[6].Style.ForeColor = Color.Red;
                    countNotset++;
                }
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() == valu.ToString())
                {
                    dataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.Red;
                    countNotset++;
                }
            }
            if (countNotset > 0)
            {
                TcountNotset.Text = " Count of NOT SET : " + countNotset.ToString();
                TcountNotset.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                TcountNotset.Text = "";
            }
        }

        public void FManualJobRequest_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        public void FManualJobRequest_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void btnformclose_Click(object sender, EventArgs e)
        {
            string ErrNo = MyFunction.Asp_lock(MyGlobal.strIP, "UnLock", cbbPlant.Text, cbbProduct.Text, UserAccount.GetuserName());
            this.Close();
        }

        public void btnCheck_Click(object sender, EventArgs e)
        {
            checkAll();
        }

        public void btnUncheck_Click(object sender, EventArgs e)
        {
            UncheckAll();
        }

        public void btnIssueJR_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("you are Check All Records, Are you sure to Process Job Request ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    SaveMode(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                }
            }
            catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        public void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPlant.SelectedIndex >= 0)
                {
                    cbbProduct.Items.Clear();
                    db.SetProduct3(ref cbbProduct, cbbPlant.SelectedItem.ToString());
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

        public void btnExport_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    openFileDialog1.Filter = "CSV File|*.csv";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Auto Job Request");
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = openFileDialog1.FileName.ToString();
                        cm.Export_to_CSV_check(header, path, dataGridView1);
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

        public void SaveMode(string strip, string strplantname, string strproductname)
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                List<string> arrid = new List<string>();
                conn = db.GetConnString();
                sql = "";
                cmd = new SqlCommand(sql, conn);

                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chk2"].Value) == true)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value != null)
                        {
                            arrid.Add(dataGridView1.Rows[i].Cells[24].Value.ToString());
                        }
                    }
                }

                string strid = string.Join(",", arrid.ToArray());
                string res = MyFunction.Asp_jrlog(strid, Properties.Settings.Default.Maxrow, UserAccount.GetuserName());
                string strresult = res;
                if (strresult == "Success".ToUpper())
                {
                    string strexport = MyFunction.Asp_jr_csv_export(strplantname, strproductname, Properties.Settings.Default.FolderExport, UserAccount.GetuserName());
                    if (strexport == "Success")
                    {
                        string AutoReserve = "Success";
                        MessageBox.Show("" +
                            "Generate Files : " + strexport + "\n " +
                            "AutoReserve    : " + AutoReserve + "\n " +
                            "Send Email     : Disabled\n");
                        StartLoad();
                    }
                    else
                    {
                        string strdelete = MyFunction.Asp_jrlog_delete(strplantname, strproductname);
                        MessageBox.Show(strexport);
                    }
                }
                else
                {
                    MessageBox.Show(strresult);
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        public void StartLoad()
        {
            dataGridView1.Visible = false;
            groupgrid.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            cbbPlant.Enabled = true;
            cbbProduct.Enabled = true;
            btnUpJR.Enabled = true;
        }

        public void checkAll()
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
                dataGridView1.Rows[i].Cells[0].ReadOnly = false;
            }
        }

        public void UncheckAll()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[0].Value = false;
            }
        }
    }
}
