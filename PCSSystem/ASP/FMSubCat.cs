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
using System.Data.OleDb;
using Microsoft.ApplicationBlocks.Data;

namespace PCSSystem.ASP
{
    public partial class FMSubCat : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

        Common cm = new Common();
        database db = new database();
        string Status = "", errortitle, errorsql;
        DataSet ds;
        string sql = "";
        Boolean uploaded = false;
        Boolean error = false;
        public FMSubCat()
        {
            InitializeComponent();
        }

        private void FMSubCat_Load(object sender, EventArgs e)
        {
            db.SetPlant2(ref cbFilter);
            
            db.SetPlant(ref cbPlant);

            DisplayData(cbFilter.Text);
            fClear();
            db.SetInitialCategory(ref tCategory);
        //    Dgvrow();

        }
        //void Dgvrow()
        //{
        //    try
        //    {
        //     //   dgvReport.ColumnCount = 8;

        //        dgvReport.Columns[0].Name = "Plant";
        //        dgvReport.Columns[1].Name = "Product";
        //        dgvReport.Columns[2].Name = "Part Category";
        //        dgvReport.Columns[3].Name = "Sub Category";
        //        dgvReport.Columns[4].Name = "Description";
        //        dgvReport.Columns[5].Name = "From Qty";
        //        dgvReport.Columns[6].Name = "To Qty";
        //        dgvReport.Columns[7].Name = "Buffer Percentage";

        //      //  dataGridView2.Rows.Add("2300","BB","STICKER","LS1L","Normal Low Req","0","1000","4");
        //    }
        //    catch (Exception ex)
        //    {
        //        db.SaveError(ex.ToString());
        //    }

        //}

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            //    sql = "select a.asp_msubcategory_key,a.plant as 'Plant',a.product as 'Product',a.partcategory as 'Part Category',b.initial as 'Stands Of',a.subpartcategory as 'Sub Category'" +
            //   ",a.description as Description,a.fromqty as 'From Qty',a.toqty as 'To Qty',a.bufferpercent as 'Buffer Percentage' from asp_msubcategory a left join asp_partcategoryinit b ON a.partcategory=b.category where plant='"+cbFilter.Text+"'";
                DisplayData(cbFilter.Text);
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                // MessageBox.Show(ex.ToString());
            }
        }

        void DisplayData(string strplant)
        {
            dgvReport.DataSource = null;
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@plant", strplant)
               };
            ds = null;
            ds = SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_msubcategory_view", sqlParams);
            dgvReport.DataSource = ds.Tables[0];

            #region formatgrid
            dgvReport.Columns["Plant"].Width = 50;
            dgvReport.Columns["Product"].Width = 70;
            dgvReport.Columns["Part Category"].Width = 150;
            dgvReport.Columns["Sub Category"].Width = 110;
            dgvReport.Columns["Description"].Width = 150;
            dgvReport.Columns["From Qty"].Width = 120;
            dgvReport.Columns["From Qty"].DefaultCellStyle.Format = "N0";
            dgvReport.Columns["To Qty"].Width = 120;
            dgvReport.Columns["To Qty"].DefaultCellStyle.Format = "N0";
            dgvReport.Columns["Buffer Percentage"].Width = 150;
            dgvReport.Columns["asp_msubcategory_key"].Visible = false;

            //dgvReport.Columns["From Qty"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";*/
            #endregion
            lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fClear();
        }

        void fClear()
        {
            tCategory.Text ="";
            tSubcategory.Text = "";
            tRemarks.Text = "";
            tQtyfrom.Text = "";
            tQtyto.Text = "";
            tBuffer.Text = "";
            tStands.Text = "";
            btnSave.Visible = false;
            btnEdit.Enabled = false;
            button2.Enabled = false;
            //btnAdd.Enabled = false;
            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            btnEdit.Visible = true;
            btnDelete.Enabled = false;
            cbPlant.Enabled = false;
            cbProduct.Enabled = false;
            tCategory.Enabled = false;
            tSubcategory.Enabled = false;
            tRemarks.Enabled = false;
            tQtyfrom.Enabled = false;
            tQtyto.Enabled = false;
            tBuffer.Enabled = false;
            button2.Visible = true;
            btnDelete.Enabled = false;
           
            tStands.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddMode();
        }

        void AddMode(){
            try
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Enabled = false;
                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Visible = true;
                Status = "ADD";
                button2.Visible = true;
                button2.Enabled = false;

                cbPlant.Enabled = true;
                cbProduct.Enabled = true;
                tCategory.Enabled = true;
                tSubcategory.Enabled = true;
                tRemarks.Enabled = true;
                tQtyfrom.Enabled = true;
                tQtyto.Enabled = true;
                tBuffer.Enabled = true;
                tStands.Enabled = true;
                

                cbPlant.Text = "";
                cbProduct.Text = "";
                tCategory.Text = "";
                tSubcategory.Text = "";
                tRemarks.Text = "";
                tQtyfrom.Text = "";
                tQtyto.Text = "";
                tBuffer.Text = "";
                tStands.Text = "";
                

                //cbFilter.Enabled = false;
                //txtCriteria.Enabled = false;
                //dgvReport.Enabled = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            }

        private void cbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
           {
               if (cbPlant.SelectedIndex >= 0)
               {
                   db.SetProduct(ref cbProduct, cbPlant.SelectedItem.ToString());
                   if (cbProduct.Items.Count > 0)
                   {
                       cbProduct.SelectedIndex = 0;
                   }
               }
           }
           catch (Exception ex)
           {
               //db.SaveError(ex.ToString());
               MessageBox.Show(ex.ToString());
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(validate_data())
            {
                InsertRecord();
            }
        }

        void InsertRecord()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            //txtDesc.Text = name;
            
            try
            {
                string sts;
                conn = db.GetConnString();
                sql = "INSERT INTO asp_msubcategory (isactived,insertdate,insertby,postdate,postby,plant,product,partcategory,subpartcategory,description,fromqty,toqty,bufferpercent) VALUES " +
                    " ('1',GETDATE(),'"+ UserAccount.GetuserID() + "',GETDATE(),'" + UserAccount.GetuserID() + "','" + cbPlant.SelectedItem.ToString() + "','" + cbProduct.SelectedItem.ToString() + "','" + tCategory.Text+"','"+tSubcategory.Text+"','"+tRemarks.Text+"',"+tQtyfrom.Text+","+tQtyto.Text+","+tBuffer.Text+")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
              //  sql = "select a.asp_msubcategory_key,a.plant as 'Plant',a.product as 'Product',a.partcategory as 'Part Category',b.initial as 'Stands Of',a.subpartcategory as 'Sub Category'" +
              // ",a.description as Description,a.fromqty as 'From Qty',a.toqty as 'To Qty',a.bufferpercent as 'Buffer Percentage' from asp_msubcategory a left join asp_partcategoryinit b ON a.partcategory=b.category";
                DisplayData(cbFilter.Text);
                //ViewMode();
                fClear();
            }
            catch (Exception ex)
            {
                //db.SaveError(ex.ToString());
                MessageBox.Show(ex.ToString());
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

                if (cbPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbPlant.Focus();
                    return result;
                }
                if (cbProduct.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbProduct.Focus();
                    return result;
                }

                if (tCategory.Text == "")
                {
                    MessageBox.Show("Please fill up the Category", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tCategory.Focus();
                    return result;
                }

                

                if (tQtyfrom.Text == "")
                {
                    MessageBox.Show("Please fill up the Qty From", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tQtyfrom.Focus();
                    return result;
                }

                if (tQtyto.Text == "")
                {
                    MessageBox.Show("Please fill up the Qty To ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tQtyto.Focus();
                    return result;
                }

                if (tBuffer.Text == "")
                {
                    MessageBox.Show("Please fill up the Buffer Presentage ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tBuffer.Focus();
                    return result;
                }

                SqlDataAdapter adapter;
                DataTable dt;
                conn = db.GetConnString();
                sql = "select isactived from asp_msubcategory where plant='" + cbPlant.Text + "' and product='"+cbProduct.Text+ "' and partcategory='"+tCategory.Text+"' and subpartcategory='"+ tSubcategory .Text+ "'";
                adapter = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(cbPlant.Text + " and "+ cbProduct.Text + " and "+ tCategory.Text + " Already Exist..!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }

                else
                {
                    result = true;
                }


                /*conn = db.GetConnString();
               sql = "select * from asp_msubcategory"+
               "where plant='"+cbPlant.Text+ "' and product='"+cbProduct.Text+ "' and partcategory='" + tCategory.Text + "' and subpartcategory='"+tSubcategory.Text+"'"+
               "description='"+tRemarks.Text+ "' and fromqty="+tQtyfrom.Text+ " and toqty="+tQtyto.Text+ " and bufferpercent="+tBuffer.Text;
               cmd = new SqlCommand(sql, conn);
               if (cmd.ExecuteScalar() == null)
               {
                   result = true;
               }
              else if ((cmd.ExecuteScalar().ToString() != ""))
               {
                   //MessageBox.Show("Material "+cbMaterial.Text+" under progress in Job Request. Do you want to add ?", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   DialogResult dialog = FCustMassageBox.Show("Material " + cbMaterial.Text + " under progress in Job Request. Do you want to add ?", "Alert !", "Add", "Cancel", "View Detail", cbMaterial.Text);
                   if (dialog == DialogResult.Yes)
                   {
                       result = true;
                   }
                   else
                   {
                       return result;
                   }

               }
               else
               {
                   DialogResult dialog = FCustMassageBox.Show("Material " + cbMaterial.Text + " under progress in Job Request. Do you want to add ?", "Alert !", "Add", "Cancel", "View Detail", cbMaterial.Text);
                   if (dialog == DialogResult.Yes)
                   {
                       result = true;
                   }
                   else
                   {
                       return result;
                   }
               }*/


            }
            catch (Exception ex)
            {
                //db.SaveError(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                fClear();
                button2.Enabled = true;
                btnDelete.Enabled = true;
                DisplayValue();
            }
        }
        void DisplayValue()
        {
            string dt, ed = "";
            try
            {
                tCategory.Text= dgvReport.SelectedRows[0].Cells["Part Category"].Value.ToString();
                tSubcategory.Text = dgvReport.SelectedRows[0].Cells["Sub Category"].Value.ToString();
                cbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                cbProduct.SelectedItem = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                tRemarks.Text = dgvReport.SelectedRows[0].Cells["Description"].Value.ToString();
                tQtyfrom.Text= dgvReport.SelectedRows[0].Cells["From Qty"].Value.ToString();
                tQtyto.Text= dgvReport.SelectedRows[0].Cells["to Qty"].Value.ToString();
                tBuffer.Text = dgvReport.SelectedRows[0].Cells["Buffer Percentage"].Value.ToString();
                tKey.Text = dgvReport.SelectedRows[0].Cells["asp_msubcategory_key"].Value.ToString();
                //tStands.Text = dgvReport.SelectedRows[0].Cells["Stands Of"].Value.ToString();


                              
                //tIndirect.Text = dgvReport.SelectedRows[0].Cells["Indirect"].Value.ToString();


                /*dt = dgvReport.SelectedRows[0].Cells["StartDate"].Value.ToString();
                ed = dgvReport.SelectedRows[0].Cells["EndDate"].Value.ToString();
                cbbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                txtPeriod.Text = dgvReport.SelectedRows[0].Cells["Period"].Value.ToString();
                dtpStart.Value = Convert.ToDateTime(dt);
                dtpEnd.Value = Convert.ToDateTime(ed);*/

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure Update di Data ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                UpdateRecord();
            }
        }

        void EditMode()
        {
            try
            {
                btnAdd.Visible = true;
                btnAdd.Enabled = false;
                btnEdit.Visible = true;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Visible = true;
                button2.Visible = false;
                btnEdit.Enabled = true;

                cbPlant.Enabled = true;
                cbProduct.Enabled = true;
                tCategory.Enabled = true;
                tSubcategory.Enabled = true;
                tRemarks.Enabled = true;
                tQtyfrom.Enabled = true;
                tQtyto.Enabled = true;
                tBuffer.Enabled = true;
                tStands.Enabled = true;
             
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void UpdateRecord()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                string sts;
                conn = db.GetConnString();
                sql = "UPDATE asp_msubcategory SET postdate=GETDATE(),postby='"+ UserAccount.GetuserName() +"',plant='" + cbPlant.Text + "'," +
                       "product='" + cbProduct.Text + "',partcategory='" + tCategory.Text + "',prcode='"+tStands.Text+"',subpartcategory='" + tSubcategory.Text + "'," +
                       "description='" + tRemarks.Text + "',fromqty=" + tQtyfrom.Text + ",toqty=" + tQtyto.Text + ",bufferpercent=" + tBuffer.Text+
                       "where asp_msubcategory_key='"+tKey.Text+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
              //  sql = "select a.asp_msubcategory_key,a.plant as 'Plant',a.product as 'Product',a.partcategory as 'Part Category',b.initial as 'Stands Of',a.subpartcategory as 'Sub Category'" +
              // ",a.description as Description,a.fromqty as 'From Qty',a.toqty as 'To Qty',a.bufferpercent as 'Buffer Percentage' from asp_msubcategory a left join asp_partcategoryinit b ON a.partcategory=b.category";
                DisplayData(cbFilter.Text);
                MessageBox.Show("Update Success...!!");
                fClear();
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

        private void button2_Click(object sender, EventArgs e)
        {
            EditMode();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure Delete di Data ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                DeleteData();
            }
        }

        void DeleteData()
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = db.GetConnString();
                sql = "Delete from asp_msubcategory where asp_msubcategory_key='" + tKey.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
             //   sql = "select a.asp_msubcategory_key,a.plant as 'Plant',a.product as 'Product',a.partcategory as 'Part Category',b.initial as 'Stands Of',a.subpartcategory as 'Sub Category'" +
             //  ",a.description as Description,a.fromqty as 'From Qty',a.toqty as 'To Qty',a.bufferpercent as 'Buffer Percentage' from asp_msubcategory a left join asp_partcategoryinit b ON a.partcategory=b.category";
                DisplayData(cbFilter.Text);
                MessageBox.Show("Delete Success...!!");
                fClear();
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

        private void button3_Click(object sender, EventArgs e)
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

                        header.Add("Master Data Sub Category");
                        //header.Add("Filter by: " + cbbFilter.SelectedItem.ToString());
                        //header.Add("Criteria: " + txtCriteria.Text.ToUpper());
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

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }



        private void tQtyfrom_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tQtyfrom.Text, "  ^ [0-9]"))
            {
                tQtyfrom.Text = "";
            }
        }

        private void tQtyfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tQtyto_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tQtyto.Text, "  ^ [0-9]"))
            {
                tQtyto.Text = "";
            }
        }

        private void tBuffer_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tBuffer.Text, "  ^ [0-9]"))
            {
                tBuffer.Text = "";
            }
        }

        private void tQtyto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
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

        private void tBuffer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string path = "";
            string sqls = "";
            SqlConnection conns = null;
            SqlCommand cmds = null;
            SqlDataAdapter adapter = null;
            string csv = "";
            try
            {
                btnImport.Enabled = false;
                openFileDialog1.Filter = "CSV files (*.csv)|*.CSV";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    DataTable dt = new DataTable();
                    dt = cm.ReadCsvFile(path);
                    //dgvReport.DataSource = dt;
                    
                    for (int ih = 0; ih < dt.Columns.Count; ih++)
                    {
                        //csv += dt.Columns[ih].ColumnName.ToString() + ',';
                    }
                    
                    if (dt.Rows.Count > 0)
                    {
                        for (int ix = 0; ix < dt.Rows.Count; ix++)
                        {
                            //csv+=dt.Rows[ix].ItemArray.GetValue(7).ToString();
                                //csv += dt.Columns[ih].ColumnName.ToString() + ',';
                                conns = db.GetConnString();
                            /*sqls = "INSERT INTO asp_msubcategory(isactived,insertdate,insertby,postdate,postby,plant,product,partcategory,subpartcategory,description," +
                                "fromqty,toqty) VALUES ('1',GETDATE(),'" + UserAccount.GetuserName()+ "',GETDATE(),'" + UserAccount.GetuserName() + "','"+ dt.Rows[0]["Plant"].ToString() + "'," +
                                "'" + dt.Rows[ix]["Product"].ToString() + "','" + dt.Rows[ix]["Part Category"].ToString() + "','" + dt.Rows[ix]["Sub Category"].ToString() + "','" + dt.Rows[ix]["Description"].ToString() + "'," +
                                "'" + dt.Rows[ix]["From Qty"].ToString() + "','" + dt.Rows[ix][7].ToString() + "')";*/
                            /*sqls = "INSERT INTO asp_msubcategory(isactived,insertdate,insertby,postdate,postby,plant,product,partcategory,subpartcategory,description," +
                                "fromqty,toqty,bufferpercent) VALUES ('1',GETDATE(),'" + UserAccount.GetuserName() + "',GETDATE(),'" + UserAccount.GetuserName() + "','" + dt.Rows[ix].ItemArray.GetValue(0).ToString() + "'," +
                                "'" + dt.Rows[ix].ItemArray.GetValue(1).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(2).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(3).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(4).ToString() + "'," +
                                "" + dt.Rows[ix].ItemArray.GetValue(5).ToString() + "," + dt.Rows[ix].ItemArray.GetValue(6).ToString() + "," + dt.Rows[ix].ItemArray.GetValue(7).ToString() + ")";*/
                            sqls = "EXEC asp_importsubcategory '" + dt.Rows[ix].ItemArray.GetValue(0).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(1).ToString() + "','" + UserAccount.GetuserName() + "','"+ UserAccount.GetuserName() + "','" + dt.Rows[ix].ItemArray.GetValue(2).ToString() + "','" + 
                                    dt.Rows[ix].ItemArray.GetValue(3).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(4).ToString() + "'," + dt.Rows[ix].ItemArray.GetValue(5).ToString() + "," + dt.Rows[ix].ItemArray.GetValue(6).ToString() + "," + dt.Rows[ix].ItemArray.GetValue(7).ToString() + "";
                            cmds = new SqlCommand(sqls, conns);
                                cmds.ExecuteNonQuery(); 
                        }
                    }

                    txtStatus.Visible = true;
                    btnImport.Enabled = true;
                }
                btnImport.Enabled = true;
            //    sql = "select a.asp_msubcategory_key,a.plant as 'Plant',a.product as 'Product',a.partcategory as 'Part Category',b.initial as 'Stands Of',a.subpartcategory as 'Sub Category'" +
            //    ",a.description as Description,a.fromqty as 'From Qty',a.toqty as 'To Qty',a.bufferpercent as 'Buffer Percentage' from asp_msubcategory a left join asp_partcategoryinit b ON a.partcategory=b.category";
                DisplayData(cbFilter.Text);
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        

        /*void Import_Data_CSV(string path)
        {
            try
            {
                char delimiter;
                string fileRow;
                string[] fileDataField;
                int count = 0;
                DataTable dt = new DataTable();
                delimiter = ',';
                if (System.IO.File.Exists(path))
                {
                    System.IO.StreamReader fileReader = new StreamReader(path);

                    if (fileReader.Peek() != -1)
                    {
                        fileRow = fileReader.ReadLine();
                        fileDataField = fileRow.Split(delimiter);
                        count = fileDataField.Count();
                        count = count - 1;

                        //Reading Header information
                        for (int i = 0; i <= count; i++)
                        {
                            DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
                            columnDataGridTextBox.Name = fileDataField[i];
                            columnDataGridTextBox.HeaderText = fileDataField[i];
                            columnDataGridTextBox.Width = 120;
                            //dt.Columns.Add(columnDataGridTextBox);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File is Empty!!");
                    }

                    //Reading Data
                    while (fileReader.Peek() != -1)
                    {
                        fileRow = fileReader.ReadLine();
                        fileDataField = fileRow.Split(delimiter);
                        dgvReport.Rows.Add(fileDataField);
                        //DataLoaded = true;
                    }

                    fileReader.Close();
                }
                else
                {
                    MessageBox.Show("No File is Selected!!");
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }*/
    }
}

