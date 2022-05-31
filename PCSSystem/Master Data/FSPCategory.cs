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

namespace PCSSystem.Master_Data
{
    public partial class FSPCategory : Form
    {
        Common cm = new Common();
        database db = new database();
        string errortitle = "", errorsql = "";
        string Status = "";
        public FSPCategory()
        {
            InitializeComponent();
        }

        private void cbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetProduct(ref cbProduct, cbPlant.Text);
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.SetMaterial2(ref cbMaterial, cbProduct.Text,cbPlant.Text);
        }

        private void FSPCategory_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbPlant);
            //DisplayData();
            getFilter();
        }

        void DisplayData()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            string sql = "";
            string field = "", cri = "";
            try
            {
                conn = db.GetConnString();
                field = cbFilter.SelectedItem.ToString();
                field = "sp." + field;
                cri = " LIKE '%" + tbCriteria.Text + "%'";
                sql = "select sp.Plant,sp.Product,sp.Material,mt.MaterialDesc,sp.PartCategory,sp.UpdateBy,sp.UpdateDate " +
                    "from TPCS_SPCATEGORY sp left outer join TMaterial mt on sp.Material = mt.Material and sp.Plant=mt.plant where " + field + cri;


                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;


                #region formatgrid
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Material"].Width = 80;
                dgvReport.Columns["Product"].Width = 50;
                dgvReport.Columns["PartCategory"].Width = 90;
                dgvReport.Columns["MaterialDesc"].Width = 200;
                dgvReport.Columns["UpdateDate"].Width = 150;
                dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
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

        private void tbCriteria_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
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
                btnSave.Visible = true;
                btnCancel.Visible = true;
                Status = "ADD";

                cbPlant.Enabled = true;
                cbProduct.Enabled = true;
                cbMaterial.Enabled = true;
                cbCategory.Enabled = true;

                cbFilter.Enabled = false;
                tbCriteria.Enabled = false;
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
                cbPlant.Enabled = false;
                cbProduct.Enabled = false;
                cbMaterial.Enabled = false;
                cbCategory.Enabled = false;
                Status = "VIEW";

                cbFilter.Enabled = true;
                tbCriteria.Enabled = true;
                dgvReport.Enabled = true;

                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;

                DisplayData();

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

            try
            {
                conn = db.GetConnString();
                sql = "INSERT INTO TPCS_SPCATEGORY (Plant,Material,Product,PartCategory,UpdateBy,UpdateDate) VALUES " +
                    " ('" + cbPlant.SelectedItem.ToString() + "','" + cbMaterial.SelectedItem.ToString() + "','" + cbProduct.SelectedItem.ToString() +
                    "','" + cbCategory.SelectedItem.ToString() + "','" + UserAccount.GetuserID() + "',GETDATE())";
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
                sql = "UPDATE TPCS_SPCATEGORY SET Partcategory='" + cbCategory.SelectedItem.ToString() + "' WHERE Plant='" + cbPlant.Text +
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

                conn = db.GetConnString();
                sql = "Select Material from TMaterial WHERE MATERIAL='" + cbMaterial.Text + "' and Plant='"+cbPlant.Text+"' and Product='" + cbProduct.Text + "'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("Material not Founds!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }
                else if ((cmd.ExecuteScalar().ToString() != ""))
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Material not Founds!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return result;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteMode();
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
                    cbCategory.Enabled = true;
                    Status = "EDIT";

                    cbFilter.Enabled = false;
                    tbCriteria.Enabled = false;
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

                    if (cbCategory.FindStringExact(dgvReport.SelectedRows[0].Cells["PartCategory"].Value.ToString()) >= 0)
                        cbCategory.SelectedItem = dgvReport.SelectedRows[0].Cells["PartCategory"].Value.ToString();
                    else
                        cbCategory.SelectedIndex = -1;
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string path = "";
            string[] fileheaders, tableheaders;
            string temp = "";

            try
            {
                txtStatus.Text = "Select the file ..";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    fileheaders = cm.GetFileHeaders(path,',');
                    temp = db.GetGlobal("HEADER_SPCAT");

                    tableheaders = temp.Split('|');
                    txtStatus.Text = "Check file headers ...";
                    if(cm.CheckHeader(fileheaders,tableheaders))
                    {
                        errortitle = "";
                        errorsql = "";
                        if (Import_Data(path, tableheaders))
                        {
                            txtStatus.Text = "Validating Data ..";
                            if (Validating_Data())
                            {
                                txtStatus.Text = "Saving ...";
                                InsertIntoTable();
                        }
                    }
                    }

                }
            }
            catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool Validating_Data()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();
                sql = "SELECT DISTINCT(Material) from TPCS_SPCATEGORY_TEMP t1 " +
                    " WHERE t1.Material not in (SELECT Material from TMaterial)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Material!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Material! View Error.";
                    errorsql = "SELECT * from TPCS_SPCATEGORY_TEMP t1 " +
                    "WHERE t1.Material not in (SELECT Material from TMaterial)";
                    errortitle = "Import WSingle Part Category-Invalid Material";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT(Plant) from TPCS_SPCATEGORY_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_SPCATEGORY_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import Single Part Category-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                //sql = "SELECT DISTINCT Plant, Product from TPCS_SPCATEGORY_TEMP t1 WHERE " +
                //        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                //cmd.CommandText = sql;
                //if (!(cmd.ExecuteScalar() == null))
                //{
                //    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    ok = false;
                //    txtStatus.Text = "Invalid Product! View Error.";
                //    errorsql = "SELECT * from TPCS_SPCATEGORY_TEMP t1 " +
                //   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                //    errortitle = "Import Single Part Category-Invalid Products";
                //    FInfo f = new FInfo(errortitle, errorsql);
                //    f.ShowDialog();
                //    f.Dispose();
                //    return ok;
                //}


                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }


        bool Import_Data(string path, string[] tableheaders)
        {
            bool ok = false;
            string sql = "";
            StreamReader sr = null;
            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            string header = "";
            string line = "";
            string[] lines = null;
            char delimiter = ',';
            int rows = 0;
            int counts = 0;
            string sqlval = "";
            string columnnames = "";
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();

                sql = "DELETE FROM TPCS_SPCATEGORY_TEMP";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sr = new StreamReader(path);
                header = sr.ReadLine();

                for (int i = 0; i < tableheaders.Length; i++)
                {
                    columnnames = columnnames + tableheaders[i] + ",";
                }
                columnnames = columnnames.Substring(0, columnnames.Length - 1);



                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    line = line.Replace("\"", "");
                    //line = line.Replace(",", "");
                    lines = line.Split(delimiter);
                    cm.Quoting(ref lines);

                    sqlval = sqlval + "(";
                    for (int i = 0; i < tableheaders.Length; i++)
                    {
                        sqlval = sqlval + lines[i] + ",";
                    }
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);
                    sqlval = sqlval + "),";
                    rows++;
                    counts++;

                    txtStatus.Text = "Uploading: " + counts.ToString() + " rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_SPCATEGORY_TEMP (" + columnnames + ") " +
                            " VALUES " +
                            sqlval;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        rows = 0;
                        sqlval = "";
                    }
                }

                if (rows > 0)
                {
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);

                    sql = "INSERT INTO TPCS_SPCATEGORY_TEMP (Plant, Material, Product, PartCategory) " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }
                trans.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                sr.Close();
                conn.Dispose();
            }
            return ok;
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

                        header.Add("Master Data: Single Part Category");
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
                    sql = "DELETE FROM TPCS_SPCATEGORY WHERE Plant = +'" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "' AND " +
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

        bool InsertIntoTable(bool replace = false)
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;

            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;

                if (replace)
                {
                    sql = "DELETE FROM TPCS_SPCATEGORY ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "DELETE t1 FROM TPCS_SPCATEGORY t1 INNER JOIN TPCS_SPCATEGORY_TEMP t2 on " +
                        " t1.Plant=t2.Plant and t1.Material=t2.Material AND t1.Product=t2.Product ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_SPCATEGORY (Plant,Material, Product,PartCategory, UpdateBy, UpdateDate) " +
                        " SELECT Plant,Material, Product,PartCategory, '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_SPCATEGORY_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_SPCATEGORY_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                txtStatus.Text = "Import Finished!";
                ok = true;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Import failed!";
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }


    }
}
