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
namespace PCSSystem.ASP
{
    public partial class FRepPP57 : Form
    {
        Common cm = new Common();
        database db = new database();
        string sql;
        DataSet ds;
        public FRepPP57()
        {
            InitializeComponent();
        }

     
        private void FRepPP57_Load(object sender, EventArgs e)
        {
           
            db.SetPlant(ref cbbPlant);
            //PVIndicatorUpdate();  //disabled 
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
            LoadDataSet();
            //checkAll();
            
        }

        void LoadDataSet()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "EXEC asp_pp57_jobrequest '" + cbbPlant.Text + "','" + cbbProduct.Text + "'";
            //sql = "EXEC asp_pp57_jobrequest '2300','BB'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            //dataGridView1.Rows[0].Cells[0].Value = true;
            //cb1.TrueValue = true;
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
            string valu = "0";
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                //MessageBox.Show(valu.ToString());
                if (dataGridView1.Rows[i].Cells[21].Value != null) {
                    if (dataGridView1.Rows[i].Cells[21].Value.ToString() == valu.ToString())
                    {
                        //dataGridView1.Rows[i].Visible= false;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                        //dataGridView1.Rows[i].Cells[0];
                        
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.DarkGray;

                        //MessageBox.Show(dataGridView1.Rows[i].Cells[19].Value.ToString()+"=="+valu.ToString()+" dari "+ dataGridView1.Rows[i].Cells[11].Value.ToString());
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                    }

                    if(int.Parse(dataGridView1.Rows[i].Cells[20].Value.ToString())<0)
                    {
                        dataGridView1.Rows[i].Cells[20].Style.ForeColor = Color.Red;
                    }
                    
                }
                //MessageBox.Show(dataGridView1.Rows[i].Cells[19].Value.ToString());
            }
        }

        void checkAll()
        {
            string valu = "0";
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[21].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[21].Value.ToString() == valu.ToString())
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                    }
                }  
            }
                /*foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (row.Cells[19].Value.ToString() == valu.ToString())
                    {
                        row.Cells[0].Value = false;
                    }
                    else
                    {
                        row.Cells[0].Value = true;
                    }


                }*/
        }

        void UncheckAll()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        /*void Displaydata()
        {
            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();

                sql = "SELECT ROW_NUMBER() OVER (ORDER BY plant ASC) AS no,plant as Plant, product as Product,oldmaterial as 'Old Material',material as 'Material',materialdesc as 'Material Desc',uom as UOM,reqqty as 'Request Qty', mb02 as MB02,mb03 as MB03,pba1 as PBA1,sba1 as SBA1,totalstock as 'Total Stock',estimatedbal as 'Estimate Balance' from ASP_pp57";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                lblRows.Text = "Total Rows: " + dataGridView1.Rows.Count.ToString();
            }

            catch (Exception ex)
            {
                //db.SaveError(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        } */

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
                LoadDataSet();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
               // MessageBox.Show(ex.ToString());
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            string path = "";
            //string folder = "E:\\Data-Project\\Simano\\Dokumen\\JR\\";
            string folder = Properties.Settings.Default.FolderExport;
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            //path = "E:\\Data-Project\\Simano\\Dokumen\\JR01S.csv";
            conn = db.GetConnString();
            
            //tampilkan kategori yang ada
            string sql = "";
            sql = "SELECT DISTINCT PartCategory as 'cat' from asp_jrlog";
            adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //bagi berdasarkan kategori yang ada
                string sql2 = "";
                SqlDataAdapter adapter2 = null;
                DataTable dt2 = new DataTable();
                sql2 = "EXEC asp_jrlog_bypartcategory '" + dt.Rows[i]["cat"].ToString() + "'";
                adapter2 = new SqlDataAdapter(sql2, conn);
                adapter2.Fill(dt2);
                path = folder  + "CAT-"+dt.Rows[i]["cat"].ToString() + ".csv";
                cm.ExCSVdatatable(dt2, path);

                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    //attended_type += dt2.Rows[j]["material"].ToString();
                    
                }
            }

                //cm.ExCSVdatatable(dt, path);
                /* SqlDataAdapter adapter = null;
                 SqlConnection conn = null;
                 DataTable dt = new DataTable();
                 conn = db.GetConnString();
                 sql = "SELECT DISTINCT PartCategory as 'cat' from asp_jrlog";
                 adapter = new SqlDataAdapter(sql, conn);
                 adapter.Fill(dt);            
                 dataGridView3.DataSource = dt;

                 string path = "";
                 path = "E:\\Data-Project\\Simano\\Dokumen\\JR01.csv";
                 cm.writeCSV(dataGridView3, path);*/

                //Tampilkan Datatable
                /*SqlDataAdapter adapter = null;
                SqlDataAdapter adapter2 = null;
                SqlConnection conn = null;
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                int numRows = 0;
                string attended_type = "",sql2="";

                conn = db.GetConnString();
                sql = "SELECT DISTINCT PartCategory as 'cat' from asp_jrlog";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                numRows = dt.Rows.Count;
                for (int i = 0; i < numRows; i++)
                {
                    //attended_type += dt.Rows[i]["cat"].ToString();
                    sql2 = "EXEC asp_jrlog_bypartcategory '" + dt.Rows[i]["cat"].ToString() + "'";
                    adapter2 = new SqlDataAdapter(sql2, conn);
                    adapter2.Fill(dt2);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        attended_type += dt2.Rows[j]["material"].ToString();
                    }
                }
                label2.Text = attended_type.ToString();


                */

                /*ArrayList header = new ArrayList();
                string path = "";
                try
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                            saveFileDialog1.Filter = "CSV File|*.csv";
                            header.Add("Master Data: Production Days");
                            header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                            header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        //path = saveFileDialog1.FileName.ToString();
                        path = "E:\\Data-Project\\Simano\\Dokumen\\JR01.csv";
                        cm.Export_to_CSV(header, path, dataGridView2);

                    }
                }
                catch (Exception ex)
                {
                    //db.SaveError(ex.ToString());
                    MessageBox.Show(ex.ToString());
                }*/

                //ExportCSV();
                /*string sql = "";
                SqlDataAdapter adapter = null;
                SqlConnection conn = null;
                DataTable dt = new DataTable();
                string path = "";
                try
                {
                    conn = db.GetConnString();
                    sql = "SELECT * from asp_jrlog";
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                    DataGridView dgv = new DataGridView();
                    //dataGridView3.DataSource = dt;
                    dgv.DataSource = dt;
                    path = "E:\\Data-Project\\Simano\\Dokumen\\JR01X2345.csv";
                    cm.writeCSV(dgv, path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }*/
            }


        /*void ExCSV()
        {
            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            string path = "";

            conn = db.GetConnString();
            sql = "SELECT * from asp_jrlog";
            adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            DataGridView dgv = new DataGridView();
            //dataGridView3.DataSource = dt;
            dgv.DataSource = dt;

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                csv += column.HeaderText + ',';
            }

            //Add new line.
            csv += "\r\n";

            //Adding the Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //Add the Data rows.
                    if (cell.Value != null)
                    {
                        csv += cell.Value.ToString().Replace(",", ";") + ',';
                    }
                }

                //Add new line.
                csv += "\r\n";
            }

            //Exporting to CSV.
            string folderPath = "E:\\Data-Project\\Simano\\Dokumen\\";
            File.WriteAllText(folderPath + "DataGridViewExport.csv", csv);
        }*/

        /*void ExportCSV()
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = GetData();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                    sb.Append(FormatCSV(dr[dc.ColumnName].ToString()) + ",");
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            File.WriteAllText("E:\\Data-Project\\Simano\\Dokumen\\SampleCSV2.csv", sb.ToString());
        }
        DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID", typeof(int));
            dt.Columns.Add("StudentName", typeof(string));
            dt.Columns.Add("RollNumber", typeof(int));
            dt.Columns.Add("TotalMarks", typeof(int));
            dt.Rows.Add(1, "Jame's", 101, 900);
            dt.Rows.Add(2, "Steave, Smith", 105, 820);
            dt.Rows.Add(3, "Mark\"Waugh", 109, 850);
            dt.Rows.Add(4, "Steave,\"Waugh", 110, 950);
            dt.Rows.Add(5, "Smith", 111, 910);
            dt.Rows.Add(6, "Williams", 115, 864);

            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT * from asp_jrlog";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                DataGridView dgv = new DataGridView();
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public static string FormatCSV(string input)
        {
            try
            {
                if (input == null)
                    return string.Empty;

                bool containsQuote = false;
                bool containsComma = false;
                int len = input.Length;
                for (int i = 0; i < len && (containsComma == false || containsQuote == false); i++)
                {
                    char ch = input[i];
                    if (ch == '"')
                        containsQuote = true;
                    else if (ch == ',')
                        containsComma = true;
                }

                if (containsQuote && containsComma)
                    input = input.Replace("\"", "\"\"");

                if (containsComma)
                    return "\"" + input + "\"";
                else
                    return input;
            }
            catch
            {
                throw;
            }
        }*/



        private void button3_Click_1(object sender, EventArgs e)
        {
            checkAll();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            UncheckAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int jumcheck = 0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chk2"].Value) == true)
                {
                    jumcheck++;
                }
            }

            DialogResult res = MessageBox.Show("you are Check " + jumcheck + " Records, Are you sure to Process Job Request ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                SaveMode();
            }
        }

        static int batasan(int j,int count)
        {
            int batasbs;
            int ambang;
            ambang = j + 20;

            if(ambang>count)
            {
                batasbs = count;
            }
            else
            {
                batasbs = j + 20;
            }
            
            return batasbs;
        }

        
        static string  sequenNumber(int no)
        {
            string newNo="";
            if(no<9)
            {
                newNo = "00" + no.ToString();
            }
            else if(no<100)
            {
                newNo = "0" + no.ToString();
            }
            else
            {
                newNo = no.ToString();
            }
            return newNo;
        }

        void ExportJR()
        {
            string path = "";
            //string folder = "E:\\Data-Project\\Simano\\Dokumen\\JR\\";
            //string folder = "E:\\Data-Project\\Simano\\Job Request\\PCS\\PCS\\Files\\";
            string folder = Properties.Settings.Default.FolderExport;
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            //path = "E:\\Data-Project\\Simano\\Dokumen\\JR01S.csv";
            conn = db.GetConnString();

            //tampilkan kategori yang ada
            string sql = "";
            sql = "SELECT DISTINCT PartCategory as 'cat',product from asp_jrlog";
            adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            string tampil = "";
            string lihat = "";
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //bagi berdasarkan kategori yang ada
                string sql2 = "";
                SqlDataAdapter adapter2 = null;
                DataTable dt2 = new DataTable();
                sql2 = "EXEC asp_jrlog_bypartcategory '" + dt.Rows[i]["cat"].ToString() + "'";
                adapter2 = new SqlDataAdapter(sql2, conn);
                adapter2.Fill(dt2);
                //path = folder + "CAT-" + dt.Rows[i]["cat"].ToString() + ".csv";
                //cm.ExCSVdatatable(dt2, path);

                //tandai yang sudah di export
                //                SqlConnection conn = null;
                int namaFile=1;
                
                if (dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        if (dt2.Rows[j]["id"].ToString() != null)
                        {
                         if (j % 20 == 0)
                            {
                                int hj = j;
                                tampil += j;
                                string csv = string.Empty;
                                string fileName = "MB03-" + DateTime.Now.ToString("yMMdd") + "" + sequenNumber(namaFile) + "-" +
                                dt.Rows[i]["product"].ToString() + "(" + dt2.Rows[i]["prcode"].ToString() + ")-SBA1";
                                string folderPath = folder + fileName + ".csv";


                                //Add the Header row for CSV file.
                                /*for (int ih = 0; ih < dt2.Columns.Count; ih++)
                                {
                                    csv += dt2.Columns[ih].ColumnName.ToString() + ',';
                                }*/
                                //csv += dt2.Columns[8].ColumnName.ToString() + ',';
                                //csv += dt2.Columns[9].ColumnName.ToString() + ',';
                                //csv += dt2.Columns[26].ColumnName.ToString() + ',';
                                csv += "Part Code,";
                                csv += "qty,";
                                csv += "Part Name,";
                                csv += "Job Req,";
                                //Add new line.
                                csv += "\r\n";

                                //Adding the Rows
                                //for (int ir = j; ir < dt2.Rows.Count; ir++)

                                for (int ir = j; ir < batasan(j, dt2.Rows.Count); ir++)
                                //for (int ir = j; ir < dt2.Rows.Count; ir++)
                                {
                                    /*for (int jr = 0; jr < dt2.Columns.Count; jr++)
                                    {
                                        if (dt2.Rows[ir][jr].ToString() != null)
                                        {
                                            csv += dt2.Rows[ir][jr].ToString().Replace(",", ";") + ',';
                                        }
                                    }*/
                                    csv += dt2.Rows[ir][8].ToString().Replace(",", ";") + ',';
                                    csv += dt2.Rows[ir][9].ToString().Replace(",", ";") + ',';
                                    csv += dt2.Rows[ir][26].ToString().Replace(",", ";") + ',';
                                    csv += fileName + ',';
                                    csv += "\r\n";
                                }
                                //string folderPath = folder + "MB03-" + dt.Rows[i]["cat"].ToString() + "-" + namaFile + ".csv";
                                
                                File.WriteAllText(folderPath, csv);
                                namaFile++;
                            }

                            string sqls = "";
                            SqlConnection conns = null;
                            SqlCommand cmds = null;

                            conns = db.GetConnString();
                            sqls = "UPDATE asp_jrlog SET isexported=1 WHERE asp_jrlog_key='" + dt2.Rows[j]["id"].ToString() + "'";
                            cmds = new SqlCommand(sqls, conns);
                            cmds.ExecuteNonQuery();
                            //tampil += dt2.Rows[j]["id"].ToString() + "\r\n";
                        }
                    }
                }
            }
            //label2.Text = lihat;
        }
        void SaveMode()
        {
            
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                    conn = db.GetConnString();
                    sql = "";
                    cmd = new SqlCommand(sql, conn);
                    string plant, product, material = "", materialdesc = "", uom = "", PartCategory = "", subpartcategory = "", bufferstock, reqqty, mb02, mb03, pba1, sba1, jrforwh, jrforsubcon, wmsavailableqty, sortedsba, safetysba1percen, standardboxqty, toberequestqty, tobejr, balancejr, jrwithoutbuffer,prcode;
                    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chk2"].Value) == true)
                        {
                            if (dataGridView1.Rows[i].Cells[1].Value != null)
                            {
                                plant = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                product = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                material = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                materialdesc = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                uom = dataGridView1.Rows[i].Cells[5].Value.ToString();
                                PartCategory = dataGridView1.Rows[i].Cells[6].Value.ToString();
                                subpartcategory = dataGridView1.Rows[i].Cells[7].Value.ToString();
                                bufferstock = dataGridView1.Rows[i].Cells[8].Value.ToString();
                                reqqty = dataGridView1.Rows[i].Cells[9].Value.ToString();
                                mb02 = dataGridView1.Rows[i].Cells[10].Value.ToString();
                                mb03 = dataGridView1.Rows[i].Cells[11].Value.ToString();
                                pba1 = dataGridView1.Rows[i].Cells[12].Value.ToString();
                                sba1 = dataGridView1.Rows[i].Cells[13].Value.ToString();
                                jrforwh = dataGridView1.Rows[i].Cells[14].Value.ToString();
                                jrforsubcon = dataGridView1.Rows[i].Cells[15].Value.ToString();
                                wmsavailableqty = dataGridView1.Rows[i].Cells[16].Value.ToString();
                                sortedsba = dataGridView1.Rows[i].Cells[17].Value.ToString();
                                safetysba1percen = dataGridView1.Rows[i].Cells[18].Value.ToString();
                                standardboxqty = dataGridView1.Rows[i].Cells[19].Value.ToString();
                                toberequestqty = dataGridView1.Rows[i].Cells[20].Value.ToString();
                                tobejr = dataGridView1.Rows[i].Cells[21].Value.ToString();
                                balancejr = dataGridView1.Rows[i].Cells[22].Value.ToString();
                                jrwithoutbuffer = dataGridView1.Rows[i].Cells[23].Value.ToString();
                                prcode = dataGridView1.Rows[i].Cells[24].Value.ToString();

                             sql = "INSERT INTO asp_jrlog (insertdate,insertby,plant,product,material,materialdesc,uom,PartCategory,subpartcategory,bufferstock,reqqty,mb02,mb03," +
                                       "pba1,sba1,jrforwh,jrforsubcon,wmsavailableqty,sortedsba,safetysba1percen,standardboxqty,toberequestqty,tobejr,balancejr,jrwithoutbuffer,prcode) VALUES " +
                                       "(GETDATE(),'0','" + plant + "','" + product + "','" + material + "', '" + materialdesc + "','" + uom + "','" + PartCategory + "','" + subpartcategory + "'," + Int32.Parse(bufferstock) + "," + reqqty + "," + mb02 + "," + mb03 + "," + pba1 +
                                       "," + sba1 + "," + jrforwh + "," + jrforsubcon + "," + wmsavailableqty + "," + sortedsba + "," + safetysba1percen + "," + standardboxqty + "," + toberequestqty + "," + tobejr + "," + balancejr + "," + jrwithoutbuffer + ",'"+prcode+"')";
                                                        
                            cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    //dataGridView1.Enabled = false;

                    sql = "DELETE FROM asp_tmppp57";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    LoadDataSet();
                    ExportJR();
            }
            catch (Exception ex)
            {
                //txtStatus.Text = "Import failed!";
                db.SaveError(ex.ToString());
                //MessageBox.Show(ex.ToString());
                //trans.Rollback();
            }
            /*finally
            {
                conn.Dispose();
            }*/
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataSet();
        }
    }
}
