using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PCSSystem.Reports
{

    public partial class FAddJR : Form
    {
        string name = SystemInformation.ComputerName;
        Common cm = new Common();
        database db = new database();
        string errortitle = "", errorsql = "";
        string Status = "";
        DataSet ds;
        public FAddJR()
        {
            InitializeComponent();
        }

        private void FAddJR_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbPlant);
            getFilter();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
        }

        void getFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("SPCATFIL");
                cbFilter.Items.AddRange(cri.Split('|'));
                if (cbFilter.Items.Count > 0)
                {
                    cbFilter.SelectedIndex = 0;
                }
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
                cbPlant.Enabled = false;
                cbProduct.Enabled = false;
                cbMaterial.Enabled = false;
                txtDesc.Enabled = false;
                txtQty.Enabled = false;
                txtReason.Enabled = false;
                Status = "VIEW";

                cbFilter.Enabled = true;
                txtCriteria.Enabled = true;
                dgvReport.Enabled = true;

                btnAdd.Visible = true;
                btnAdd.Enabled = true;
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;

                //DisplayData();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        private void cbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetProduct(ref cbProduct, cbPlant.Text);
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetMaterial(ref cbMaterial, cbProduct.Text);
        }

        private void cbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //db.SetMaterialName(ref txtDesc, cbProduct.Text, cbMaterial.Text,cbPlant.Text);
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

                cbPlant.Enabled = true;
                cbProduct.Enabled = true;
                groupBox.Enabled = true;
                cbMaterial.Enabled = true;
                txtDesc.Enabled = true;
                txtQty.Enabled = true;
                txtReason.Enabled = true;

                cbFilter.Enabled = false;
                txtCriteria.Enabled = false;
                //dgvReport.Enabled = false;
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
            
            txtDesc.Text = name;

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_AddJR (Plant,Product,Material,Qty,Reason,UpdateBy,UpdateDate,UpdateMac) VALUES " +
                    " ('" + cbPlant.SelectedItem.ToString() + "','" + cbProduct.SelectedItem.ToString() + "','" + cbMaterial.Text + "','" + txtQty.Text + "','"+txtReason.Text+ "','" + UserAccount.GetuserID() + "',GETDATE(),'"+name+"')";
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
                sql = "UPDATE TPCS_AddJR SET Qty='" + txtQty.Text + "',Reason='"+txtReason.Text+"' WHERE Plant='" + cbPlant.Text +
                    "' AND Material='" + cbMaterial.Text +
                    "' AND Product='" + cbProduct.Text + "'";
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

                if (cbMaterial.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Material", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaterial.Focus();
                    return result;
                }

                if (txtQty.Text == "")
                {
                    MessageBox.Show("Please select the Qty", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaterial.Focus();
                    return result;
                }


                conn = db.GetConnString();
                sql = "exec AJR_CheckBal'" + cbMaterial.Text + "'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                {
                    result = true;
                    
                }
                else if ((cmd.ExecuteScalar().ToString() != ""))
                {
                    //MessageBox.Show("Material "+cbMaterial.Text+" under progress in Job Request. Do you want to add ?", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult dialog = FCustMassageBox.Show("Material " + cbMaterial.Text + " under progress in Job Request. Do you want to add ?", "Alert !", "Add", "Cancel", "View Detail",cbMaterial.Text);
                    if(dialog == DialogResult.Yes)
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
                    DialogResult dialog = FCustMassageBox.Show("Material " + cbMaterial.Text + " under progress in Job Request. Do you want to add ?", "Alert !", "Add", "Cancel", "View Detail",cbMaterial.Text);
                    if (dialog == DialogResult.Yes)
                    {
                        result = true;
                    }
                    else
                    {
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return result;
        }

        public DialogResult CusShowDialog()
        {
            DialogResult dr = new DialogResult();
            


            return dr;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql;
            DataSet ds2;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = ("EXEC AJR_AddCreateFile '"+name+"'");
            adapter = new SqlDataAdapter(sql, conn);
            ds2 = new DataSet();
            adapter.Fill(ds2);

            if (ds2 == null)
            {
                MessageBox.Show("There is no data !");
                return;
            }

            if (ds2.Tables[1].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("There is no data for JR / Data already send!");
                return;
            }

            if (string.IsNullOrEmpty(ds2.Tables[1].Rows[0][2].ToString()))
            {
                MessageBox.Show("Please check AJR file path in TGlobal!");
                return;
            }

            //string FileName = "";
            //string FileNames = "";
            //DataTable dtExp = new DataTable();
            //for (int i = 1; i <= Convert.ToInt32(ds2.Tables[1].Rows[0][0].ToString()); i++)
            //{
            //    FileName = ds2.Tables[1].Rows[0][2].ToString()+ds2.Tables[2].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable().Rows[0][2].ToString() + ".XLSX";
            //    FileNames += ";" + FileName;
            //    dtExp = null;
            //    dtExp = ds2.Tables[0].Select("SeqNo='" + i.ToString() + "'").CopyToDataTable();
            //    dtExp.Columns.Remove("SeqNo");
            //    DataTableToExcel(dtExp, FileName);
            //}

            //FileNames = FileNames.Substring(1, FileNames.Length - 1);

            //SqlCommand cmd;
            //try
            //{
            //    conn = db.GetConnString();
            //    sql = "exec sp_AJRADD_MAIL_Test '" + FileNames + "','" + ds2.Tables[1].Rows[0][1].ToString() + "','" + UserAccount.GetuserID() + "'";
            //    cmd = new SqlCommand(sql, conn);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("JR Has Been Generated.");
            //    DisplayData();
            //}

            SqlCommand cmd;
            try
            {
                conn = db.GetConnString();
                sql = "exec sp_AJRSend_Approval '" + ds2.Tables[1].Rows[0][1].ToString() + "','" + UserAccount.GetuserID() + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("JR Has Been Generated.");
                DisplayData();
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

                if (MessageBox.Show("Do you really want to remove this?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn = db.GetConnString();
                    sql = "DELETE FROM TPCS_AddJR WHERE Plant = +'" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
                        " Product='" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "' AND " +
                        " Material='" + dgvReport.SelectedRows[0].Cells["Material"].Value.ToString() + "'";
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

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode();
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

                    cbPlant.Enabled = false;
                    cbProduct.Enabled = false;
                    cbMaterial.Enabled = false;
                    txtQty.Enabled = true;
                    txtReason.Enabled = true;
                    Status = "EDIT";

                    cbFilter.Enabled = false;
                    txtCriteria.Enabled = false;
                    dgvReport.Enabled = false;



                    if (cbPlant.FindStringExact(dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString()) >= 0)
                        cbPlant.SelectedItem = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                    else
                        cbPlant.SelectedIndex = -1;
                    if (cbProduct.FindStringExact(dgvReport.SelectedRows[0].Cells["Product"].Value.ToString()) >= 0)
                        cbProduct.SelectedItem = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                    else
                        cbProduct.SelectedIndex = -1;
                    if (cbMaterial.FindStringExact(dgvReport.SelectedRows[0].Cells["Material"].Value.ToString()) >= 0)
                        cbMaterial.SelectedItem = dgvReport.SelectedRows[0].Cells["Material"].Value.ToString();
                    else
                        cbMaterial.SelectedIndex = -1;

                    txtQty.Text = dgvReport.SelectedRows[0].Cells["Qty"].Value.ToString();
                    txtReason.Text = dgvReport.SelectedRows[0].Cells["Reason"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        void DisplayData()
        {
            dgvReport.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            string sql = "";
            string field = "", cri = "";
            field = cbFilter.SelectedItem.ToString();
            field = "aj." + field;
            cri = " LIKE '%" + txtCriteria.Text + "%'";
            sql = "select aj.Plant,aj.Product,aj.Material,mt.MaterialDesc,aj.Qty,aj.Reason,aj.UpdateBy,aj.UpdateDate,aj.UpdateMac " +
                "from TPCS_AddJR aj left outer join TMaterial mt on aj.Material = mt.Material and aj.Plant=mt.plant where aj.UpdateMac = '" + name+"' and " + field + cri;

            adapter = new SqlDataAdapter(sql, conn);

            ds = new DataSet();
            adapter.Fill(ds);
            dgvReport.DataSource = ds.Tables[0];
            
                #region formatgrid
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Material"].Width = 80;
                dgvReport.Columns["Product"].Width = 40;
                dgvReport.Columns["Qty"].Width = 40;
                dgvReport.Columns["MaterialDesc"].Width = 200;
                dgvReport.Columns["UpdateDate"].Width = 100;
                dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
