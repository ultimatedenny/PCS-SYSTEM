using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace PCSSystem.ASP
{
    public partial class FMexclution : Form
    {
        Common cm = new Common();
        database db = new database();
        DataSet ds;
        public FMexclution()
        {
            InitializeComponent();
        }
        //first load
        private void FMexclution_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbPlant);
            db.SetPlant2(ref cbPlantFilter);

            DisplayData(cbPlantFilter.Text);
            fClear();
            Dgvrow();
        }

        void Dgvrow()
        {
            try
            {
                dataGridView2.ColumnCount = 3;

                dataGridView2.Columns[0].Name = "Plant";
                dataGridView2.Columns[1].Name = "Material";
                dataGridView2.Columns[2].Name = "Reason";

              //  dataGridView2.Rows.Add("2300", "101M1001017", "-");
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        void fClear()
        {
            tMaterialtipe.Text = "";
            tMaterial.Text = "";
            tReason.Text = "";
            cbMaterial.Text = "";
            cbPlant.Text = "";

            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            
            btnSave.Visible = false;
            
            button2.Visible = true;
            button2.Enabled = false;         

            btnEdit.Visible = true;
            btnEdit.Enabled = false;

            btnDelete.Enabled = false;

            cbPlant.Enabled = false;

            //tMaterialtipe.Enabled = false;
            //tMaterial.Enabled = false;
            cbMaterial.Enabled = false;
            tReason.Enabled = false;

            cbProduct.Enabled = false;
            cbProduct.Text = "";
            
            
            

        }

        void DisplayData(string plant)
        {
            try
            {
                dgvReport.DataSource = null;
                SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@plant", plant)
               };
                ds = null;
                ds = SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_materialexclution", sqlParams);
                dgvReport.DataSource = ds.Tables[0];

                #region formatgrid
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Product"].Width = 50;
                dgvReport.Columns["Material Type"].Width = 100;
                dgvReport.Columns["Material"].Width = 150;
                dgvReport.Columns["Material Desc"].Width = 300;             
                dgvReport.Columns["Reason"].Width = 250;
                dgvReport.Columns["id"].Visible = false;
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
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
                btnDelete.Enabled = false;
                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Visible = true;

                cbProduct.Enabled = true;
                cbProduct.Text = "";

                cbPlant.Enabled = true;
                //tMaterialtipe.Enabled = true;
                //tMaterial.Enabled = true;
                cbMaterial.Enabled = true;
                tReason.Enabled = true;

                //tMaterial.Text = "";
                cbMaterial.Text = "";
                tReason.Text = "";
                //tMaterialtipe.Text = "";

                button2.Enabled = false;

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validate_data())
            {
                string strresult = MyFunction.Asp_mmaterialexlusion_insert(cbPlant.Text, cbMaterial.Text, cbProduct.Text, tReason.Text, UserAccount.GetuserName());
                MessageBox.Show(strresult);
                DisplayData(cbPlantFilter.Text);
            }
        }
     
        bool validate_data()
        {
            bool result = false;
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {

                if (cbPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbPlant.Focus();
                    return result;
                }

                if (cbMaterial.Text == "")
                {
                    MessageBox.Show("Please material the Material", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tMaterial.Focus();
                    return result;
                }

                if (tReason.Text == "")
                {
                    MessageBox.Show("Please fill up the Reason", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tReason.Focus();
                    return result;
                }

                SqlDataAdapter adapter;
                DataTable dt;
                conn = db.GetConnString();
                sql = "select isactived from asp_mmaterialexlusion where material like '" + cbMaterial.Text + "'";
                adapter = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(cbMaterial.Text+" Already Exist..!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                fClear();
                button2.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Visible = true;
                DisplayValue();
            }
        }

        void DisplayValue()
        {
            try
            {
                //tMaterialtipe.Text = dgvReport.SelectedRows[0].Cells["Material Type"].Value.ToString();
                cbMaterial.Text = dgvReport.SelectedRows[0].Cells["Material"].Value.ToString();
                cbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                tReason.Text = dgvReport.SelectedRows[0].Cells["Reason"].Value.ToString();
                tKey.Text = dgvReport.SelectedRows[0].Cells["id"].Value.ToString();


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

        private void button2_Click(object sender, EventArgs e)
        {
            EditMode();
        }

        void EditMode()
        {
            try
            {
                btnAdd.Enabled = false;
                btnEdit.Visible = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Visible = true;
                button2.Visible = false;

                cbPlant.Enabled = true;
                tMaterialtipe.Enabled = true;
                cbMaterial.Enabled = true;
                tReason.Enabled = true;

                cbProduct.Enabled = true;
                cbProduct.Text = "";
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
               // UpdateRecord(cbPlant.Text, tMaterialtipe.Text,);
                string strresult = MyFunction.Asp_mmaterialexlusion_insert(cbPlant.Text, cbMaterial.Text, cbProduct.Text, tReason.Text, UserAccount.GetuserName());
                MessageBox.Show(strresult);
                DisplayData(cbPlantFilter.Text);

            }
        }

    
        void DeleteData(string id)
        {
            string sql = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = db.GetConnString();
                sql = "Delete from asp_mmaterialexlusion where asp_mmaterialexlusion_key='" + tKey.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                DisplayData(cbPlantFilter.Text);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure Delete di Data ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                DeleteData("");
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

                        header.Add("Master Data: Material Exclution");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            fClear();
        }

        private void dgvReport_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void cbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPlant.SelectedIndex >= 0)
                {
                    db.SetMaterial3(ref cbMaterial, cbPlant.SelectedItem.ToString());
                    if (cbMaterial.Items.Count > 0)
                    {
                        cbMaterial.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData(cbPlantFilter.Text);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string path = "";
            string sqls = "";
            SqlConnection conns = null;
            SqlCommand cmds = null;
            try
            {
                conns = db.GetConnString();
                openFileDialog1.Filter = "CSV files (*.csv)|*.CSV";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    DataTable dt = new DataTable();
                    dt = cm.ReadCsvFile(path);

                    if (dt.Rows.Count > 0)
                    {
                        for (int ix = 0; ix < dt.Rows.Count; ix++)
                        {
                            sqls = "EXEC asp_importexclution '" + dt.Rows[ix].ItemArray.GetValue(0).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(1).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(2).ToString() + "','" + UserAccount.GetuserName() + "'";
                            cmds = new SqlCommand(sqls, conns);
                            cmds.ExecuteNonQuery();
                        }
                    }
                }

                txtStatus.Visible = true;
                btnImport.Enabled = false;
                DisplayData(cbPlantFilter.Text);
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {

                if (dataGridView2.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        path = saveFileDialog1.FileName.ToString();
                        cm.Export_to_CSV(header, path, dataGridView2);
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

        private void cbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetMaterialProduct(ref cbProduct, cbMaterial.SelectedItem.ToString());
            if (cbProduct.Items.Count > 0)
            {
                cbProduct.SelectedIndex = 0;
            }
        }
    }
}
