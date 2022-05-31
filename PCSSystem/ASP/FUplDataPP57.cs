using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Collections;

namespace PCSSystem.ASP
{
    public partial class FUplDataPP57 : Form
    {
        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

        database db = new database();
        Common cm = new Common();
        DataSet ds;
        string sql;

        public FUplDataPP57()
        {
            InitializeComponent();
        }

        public void FUplDataPP57_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
        }

        public string Import_Data_Excel(string path,string strip,string strplantname,string strproductname)
        {
            SqlConnection conn = null;
            try
            {

                SqlConnection conns = null;
                conns = db.GetConnString();
              
                string ConnString;
                ConnString = string.Empty;
                string extension = Path.GetExtension(path);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        ConnString = string.Format(Excel03ConString, path);
                        break;

                    case ".xlsx": //Excel 07 to later
                        ConnString = string.Format(Excel07ConString, path);
                        break;
                }

                DataTable dt;
                using (OleDbConnection conne = new OleDbConnection(ConnString))
                {
                    using (OleDbCommand cmde = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            dt = new DataTable();
                            //cmde.CommandText = "SELECT '"+cbbPlant.Text+ "' as Plant,'" + cbbProduct.Text + "' as Product,Old_material,Material,Material_desc,UOM,Req_Qty,MB02,MB03,PBA1,SBA1,Total_Stock,Estimated_Bal From [Sheet1$]";
                            cmde.CommandText = "SELECT '" + cbbPlant.Text + "' as Plant,'" + cbbProduct.Text + "' as Product,[Old material no#] as Old_material,Material,[Material Desc] as Material_desc," +
                            "UOM,[Req Qty] as Req_Qty,MB02,MB03,PBA1,SBA1,[Total Stock] as Total_Stock,[Estimated Bal] as Estimated_Bal From [Sheet1$]";
                            //cmde.CommandText = "SELECT '" + cbbPlant.Text + "' as Plant,'" + cbbProduct.Text + "' as Product,`Material Desc` as Old_material From [Sheet1$]";
                            //cmde.CommandText = "SELECT Old_material,Material,Material_desc,UOM,Req_Qty,MB02,MB03,PBA1,SBA1,Total_Stock,Estimated_Bal From [Sheet1$]";
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
                    conn=db.GetConnString();
                    string ErrNo = MyFunction.Asp_tmppp57_delete(strplantname, strproductname);
                    using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
                    {
                        sbc.DestinationTableName = "asp_tmppp57";
                        sbc.ColumnMappings.Add("Plant", "plant");
                        sbc.ColumnMappings.Add("Product", "product");
                        sbc.ColumnMappings.Add("Old_material", "oldmaterial");
                        sbc.ColumnMappings.Add("Material", "material");
                        sbc.ColumnMappings.Add("Material_desc", "materialdesc");
                        sbc.ColumnMappings.Add("UOM", "uom");
                        sbc.ColumnMappings.Add("Req_Qty", "reqqty");
                        sbc.ColumnMappings.Add("MB02", "mb02");
                        sbc.ColumnMappings.Add("MB03", "mb03");
                        sbc.ColumnMappings.Add("PBA1", "pba1");
                        sbc.ColumnMappings.Add("SBA1", "sba1");
                        sbc.ColumnMappings.Add("Total_Stock", "totalstock");
                        sbc.ColumnMappings.Add("Estimated_Bal", "estimatedbal");
                        sbc.WriteToServer(dt);
                        //sbc.WriteToServer(dtMB52);
                        //trans.Commit();
                        btnUpPP57.Enabled = false;
                        lblStatusMB52.Visible = true;
                    }
                    dataGridView1.Visible = true;
                    groupgrid.Visible = true;
                    groupcontrol.Visible = true;
                    groupbutton.Visible = true;
                    string res = MyFunction.Asp_jr(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text, path, UserAccount.GetuserName());
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
                //MessageBox.Show(ex.ToString());
                //MessageBox.Show("Error on excel file format..!!");
                //MessageBox.Show("Error on excel file format..!!, Please upload with the correct format excel..!!","Error Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning );
                //trans.Rollback();
            }
           
        }

        void LoadDataSet(string strip,string strplantname,string strproductname)
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();

            sql = "EXEC asp_jr_view '" + strip + "'";
            //sql = "EXEC asp_pp57_jobrequest '2300','BB'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];

            //            SqlParameter[] sqlParams = new SqlParameter[] {
            //                new SqlParameter("@ipaddress", strip)
            //            };
            //            DsDjobdata = SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jr_view", sqlParams);
            ////            dataGridView1.DataSource = null;
            //            dataGridView1.DataSource = DsDjobdata.Tables[0];
            //            lblRows.Text = "Total Rows: " + dataGridView1.Rows.Count.ToString();

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

                //MessageBox.Show(valu.ToString());
                if (dataGridView1.Rows[i].Cells[20].Value != null)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[21].Value) <= 0 || dataGridView1.Rows[i].Cells[7].Value.ToString() == "NOT SET")
                    {
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                        for (int ih = 0; ih < dataGridView1.Columns.Count; ih++)
                        {
                            //csv += dt2.Columns[ih].ColumnName.ToString() + ',';
                            //dataGridView1.Rows[i].Cells[ih].Style.BackColor = Color.Gray;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                        }
                        //MessageBox.Show(dataGridView1.Rows[i].Cells[19].Value.ToString()+"=="+valu.ToString()+" dari "+ dataGridView1.Rows[i].Cells[11].Value.ToString());
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

                if(dataGridView1.Rows[i].Cells[6].Value.ToString()==valu.ToString())
                {
                    dataGridView1.Rows[i].Cells[6].Style.ForeColor = Color.Red;
                    countNotset++;
                }

                if (dataGridView1.Rows[i].Cells[7].Value.ToString() == valu.ToString())
                {
                    dataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.Red;
                    countNotset++;
                }

                if (dataGridView1.Rows[i].Cells[7].Value.ToString() == valu.ToString() && Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) > 0)
                {
                    btnconfirm.Enabled = false;
                }

            }

            
            if(countNotset>0)
            {
                TcountNotset.Text = " Count of NOT SET : "+countNotset.ToString();
                
                TcountNotset.ForeColor= System.Drawing.Color.Red;
            }
            else
            {
                TcountNotset.Text = "";
            }
        }

        public void btnUpPP57_Click(object sender, EventArgs e)
        {
            string path = "";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    string res = Import_Data_Excel(path, MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                    string strresult = res;
                    if (strresult == "Success".ToUpper())
                    {
                        LoadDataSet(MyGlobal.strIP,cbbPlant.Text,cbbProduct.Text);
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
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void StartLoad()
        {
            dataGridView1.Visible = false;
            groupgrid.Visible = false;
            groupcontrol.Visible = false;
            groupbutton.Visible = false;
            cbbPlant.Enabled = true;
            cbbProduct.Enabled = true;
            btnUpPP57.Enabled = true;
        }

        void checkAll()
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[21].Value != null)
                {
                   if (Convert.ToInt32(dataGridView1.Rows[i].Cells[21].Value) <= 0 || dataGridView1.Rows[i].Cells[7].Value.ToString()=="NOT SET")
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
            }
        }

        void UncheckAll()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        void SaveMode(string strip,string strplantname,string strproductname)
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
                    if (strexport=="Success")
                    {
                        //string strsendemail = MyFunction.asp_sendemail(strplantname, strproductname, Properties.Settings.Default.FolderExport, UserAccount.GetuserName());
                        //MessageBox.Show("Generate Files is " + strexport + Environment.NewLine + " Send Email is" + strsendemail);
                        //string PckJobNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                        //{
                        //    string PLANT = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        //    string PRODUCT = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        //    string MATERIAL = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        //    string MATERIAL_DES = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        //    string UOM = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        //    string REQQTY = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        //    string autoresertve = MyFunction.asp_jr_autorsv(PckJobNum, strproductname, Properties.Settings.Default.FolderExport, UserAccount.GetuserName());
                        //}
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

        public void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LoadDataSet(MyGlobal.strIP,cbbPlant.Text,cbbProduct.Text);
        }

        public void FUplDataPP57_FormClosed(object sender, FormClosedEventArgs e)
        {
            string ErrNo = MyFunction.Asp_lock(MyGlobal.strIP, "UnLock", cbbPlant.Text, cbbProduct.Text, UserAccount.GetuserName());
            this.Close();
        }

        public void dataGridView1_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            checkAll();
        }

        public void FUplDataPP57_FormClosing(object sender, FormClosingEventArgs e)
        {
            string ErrNo = MyFunction.Asp_lock(MyGlobal.strIP, "UnLock", cbbPlant.Text, cbbProduct.Text, UserAccount.GetuserName());
        }

        public void btnformclose_Click(object sender, EventArgs e)
        {
            string ErrNo = MyFunction.Asp_lock(MyGlobal.strIP, "UnLock", cbbPlant.Text, cbbProduct.Text, UserAccount.GetuserName());
            this.Close();
        }

        public void btncheck_Click(object sender, EventArgs e)
        {
            checkAll();
        }

        public void btnuncheck_Click(object sender, EventArgs e)
        {
            UncheckAll();
        }

        public void btnconfirm_Click(object sender, EventArgs e)
        {
            int jumcheck = 0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chk2"].Value) == true)
                {
                    jumcheck++;
                }
            }
            if (jumcheck > 0)
            {
                DialogResult res = MessageBox.Show("you are Check " + jumcheck + " Records, Are you sure to Process Job Request ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    SaveMode(MyGlobal.strIP, cbbPlant.Text, cbbProduct.Text);
                }
            }
            else
            {
                MessageBox.Show("no Records Selected..!!\nPlease select one or more records..!!", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void button3_Click(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Auto Job Request");
                        //header.Add("Filter by: " + cbbFilter.SelectedItem.ToString());
                        //header.Add("Criteria: " + txtCriteria.Text.ToUpper());
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = saveFileDialog1.FileName.ToString();

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
    }
}
