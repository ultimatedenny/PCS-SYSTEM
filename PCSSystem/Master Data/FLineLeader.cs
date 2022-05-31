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

namespace PCSSystem.Master_Data
{
    public partial class FLineLeader : Form
    {
        Common cm = new Common();
        database db = new database();
        string errorsql = "", errortitle = "";
        bool NewRecord = false;

        public FLineLeader()
        {
            InitializeComponent();
        }

        private void FLineLeader_Load(object sender, EventArgs e)
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
                    
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
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


                    dgvReport.DataSource = db.SelectTables("MMLineLeader", "Plant, Product, ProdnLine,LineDesc,LeaderName, UpdateBy, UpdateDate", field + " LIKE " + cri + " ORDER BY UpdateDate DESC ");
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 100;
                    dgvReport.Columns["Product"].Width = 100;                    
                    dgvReport.Columns["ProdnLine"].Width = 100;
                    dgvReport.Columns["LineDesc"].Width = 100;
                    dgvReport.Columns["LeaderName"].Width =200;
                    dgvReport.Columns["UpdateBy"].Width = 200;                    
                    dgvReport.Columns["UpdateDate"].Width = 300;
                    dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                    #endregion
                    if (dgvReport.SelectedRows.Count > 0)
                        DisplayValue();
                }
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

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            DisplayValue();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    temp = db.GetGlobal("HEADER_ROUTEMP");

                    tableheaders = temp.Split('|');
                    txtStatus.Text = "Check File headers...";
                    if (cm.CheckHeader(fileheaders, tableheaders))
                    {
                        errortitle = "";
                        errorsql = "";
                        //if (Import_Data(path, tableheaders))
                        //{
                        //    txtStatus.Text = "Validating data...";
                        //    if (Validating_Data())
                        //    {
                        //        txtStatus.Text = "Saving...";
                        //        InsertIntoTable();

                        //    }
                        //}
                    }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
            NewRecord = true;
        }

        void AddMode()
        {
            try
            {
                cbbFPlant.Enabled = true;
                cbbPrd.Enabled = true;
                cbbLine.Enabled = true;
                txtLineDesc.Enabled = true;
                txtLeaderName.Enabled = true;                

                cbbFPlant.SelectedIndex = -1;
                cbbPrd.SelectedIndex = -1;
                cbbLine.SelectedIndex = -1;
                
                txtLineDesc.Text = "";
                txtLeaderName.Text = "";
                


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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewMode();
        }

        void ViewMode()
        {
            try
            {
                cbbFPlant.Enabled = false;
                cbbPrd.Enabled = false;
                cbbLine.Enabled = false;
                txtLeaderName.Enabled = false;
                txtLineDesc.Enabled = false;
                

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
                sql = "INSERT INTO MMLineLeader (Plant, Product, ProdnLine, LineDesc, LeaderName,UpdateBy,UpdateDate) VALUES " +
                    "(" +
                    "'" + cbbFPlant.SelectedItem.ToString() + "'," +
                    "'" + cbbPrd.SelectedItem.ToString() + "'," +
                    "'" + cbbLine.SelectedItem.ToString() + "'," +                    
                    "'" + txtLineDesc.Text.ToString() + "'," +
                    "'" + txtLeaderName.Text.ToString() + "'," +                    
                    "'" + UserAccount.GetuserID() + "'," +
                    " GETDATE()" +
                    ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Line Leader has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sql = "UPDATE MMLineLeader SET " +
                    " Plant='" + cbbFPlant.SelectedItem.ToString() + "'," +
                    " Product='" + cbbPrd.SelectedItem.ToString() + "'," +
                    " ProdnLine='" + cbbLine.SelectedItem.ToString() + "'," +                    
                    " LeaderName='" + txtLeaderName.Text.ToString() + "'," +
                    " LineDesc='" + txtLineDesc.Text.ToString() + "'," +                    
                    " Updateby='" + UserAccount.GetuserID() + "'," +
                    " UpdateDate=GETDATE()" +
                    " WHERE " +
                    " Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                    " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                    " ProdnLine='" + dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "' AND"+
                    " LeaderName = '"+ dgvReport.SelectedRows[0].Cells["LeaderName"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Line leader has been updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewMode();
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


                    txtLineDesc.Text = dgvReport.SelectedRows[0].Cells["LineDesc"].Value.ToString();
                    txtLeaderName.Text = dgvReport.SelectedRows[0].Cells["LeaderName"].Value.ToString();
                    

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode();
            NewRecord = false;
        }

        void EditMode()
        {
            try
            {
                cbbFPlant.Enabled = true;
                cbbPrd.Enabled = true;
                cbbLine.Enabled = true;                
                txtLineDesc.Enabled = true;
                txtLeaderName.Enabled = true;
                

                btnAdd.Enabled = false;
                btnCancel.Enabled = false;

                btnSave.Enabled = true;

                btnEdit.Enabled = false;
                btnEdit.Visible = false;

                btnCancelE.Enabled = true;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
                DeleteRecord();
        }

        void DeleteRecord()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE MMLineLeader " +
                    " WHERE " +
                    " Plant='" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                    " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                    " ProdnLine='" + dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "' AND " +
                    " LeaderName='" + dgvReport.SelectedRows[0].Cells["LeaderName"].Value.ToString() + "'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Line leader has been removed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                
                if (txtLineDesc.Text == "")
                {
                    MessageBox.Show("Please input the line desc!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLineDesc.Focus();
                    ok = false;
                    return ok;
                }

                if (txtLeaderName.Text == "")
                {
                    MessageBox.Show("Please input the line leader!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLeaderName.Focus();
                    ok = false;
                    return ok;
                }

                ////if ((Convert.ToDecimal(txtEff.Text) < 0) || (Convert.ToDecimal(txtEff.Text) > 1))
                ////{
                ////    MessageBox.Show("Please input the Efficiency between 0.00 to 1.00!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ////    txtEff.Focus();
                ////    ok = false;
                ////    return ok;
                ////}


                //if (!NewRecord)
                //{
                //    bool pkchanged = true;
                //    string sql = "";
                //    SqlCommand cmd;
                //    SqlConnection conn;


                //    if (dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() == cbbFPlant.SelectedItem.ToString())
                //        if (dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() == cbbPrd.SelectedItem.ToString())
                //            if (dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString() == cbbLine.SelectedItem.ToString())
                //                if (dgvReport.SelectedRows[0].Cells["Model"].Value.ToString() == cbbModel.SelectedItem.ToString())
                //                    pkchanged = false;

                //    conn = db.GetConnString();
                //    sql = "SELECT COUNT(Plant) from TPCS_ROUTEMP where Plant='" + cbbFPlant.SelectedItem.ToString() + "' AND Product = '" +
                //        cbbPrd.SelectedItem.ToString() + "' and SAPWC = '" + cbbLine.SelectedItem.ToString() + "' AND Model='" + cbbModel.SelectedItem.ToString() + "'";
                //    cmd = new SqlCommand(sql, conn);

                //    if ((Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0) && pkchanged)
                //    {
                //        MessageBox.Show("Duplicated Routing and Man Power!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        ok = false;
                //        return ok;
                //    }
                //}
                //else
                //{
                //    string sql = "";
                //    SqlCommand cmd;
                //    SqlConnection conn;

                //    conn = db.GetConnString();
                //    sql = "SELECT COUNT(Plant) from TPCS_ROUTEMP where Plant='" + cbbFPlant.SelectedItem.ToString() + "' AND Product = '" +
                //        cbbPrd.SelectedItem.ToString() + "' and SAPWC = '" + cbbLine.SelectedItem.ToString() + "' AND Model='" + cbbModel.SelectedItem.ToString() + "'";
                //    cmd = new SqlCommand(sql, conn);

                //    if (Convert.ToUInt32(cmd.ExecuteScalar().ToString()) > 0)
                //    {
                //        MessageBox.Show("Duplicated Routing and Man Power!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        ok = false;
                //        return ok;
                //    }
                //}
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }
    }
}
