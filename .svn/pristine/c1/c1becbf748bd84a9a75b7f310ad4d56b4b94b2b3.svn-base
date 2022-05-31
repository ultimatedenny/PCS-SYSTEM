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

namespace PCSSystem.ASP
{
    public partial class FMInitialPart : Form
    {
        Common cm = new Common();
        database db = new database();
        DataSet ds;
        string sql = "";
        public FMInitialPart()
        {
            InitializeComponent();
        }

        private void FMInitialPart_Load(object sender, EventArgs e)
        {
            /*db.SetInitialCategory(ref cbbPartCategory);
            if (cbbPartCategory.Items.Count > 0)
            {
                cbbPartCategory.SelectedIndex = 0;
            }*/
            fClear();
            DisplayData();
            Dgvrow();
        }

        void Dgvrow()
        {
            try
            {
                dataGridView2.ColumnCount = 2;

                dataGridView2.Columns[0].Name = "Part Category";
                dataGridView2.Columns[1].Name = "Initial";

                dataGridView2.Rows.Add("BIG PART", "BP");
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
                dgvReport.DataSource = null;
                SqlDataAdapter adapter;
                SqlConnection conn;
                conn = db.GetConnString();
                string sql = "";
                sql = "SELECT category as 'Part Category',initial as 'Initial' FROM asp_partcategoryinit";
                adapter = new SqlDataAdapter(sql, conn);

                ds = new DataSet();
                adapter.Fill(ds);
                dgvReport.DataSource = ds.Tables[0];

                #region formatgrid
                dgvReport.Columns["Part Category"].Width = 180;
                dgvReport.Columns["Initial"].Width = 510;
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void fClear()
        {
            //Button status
            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            btnEdit.Visible = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            cbbPartCategory.Text = "";
            cbbPartCategory.Enabled = false;
            //form status
            tInitial.Text = "";
            tInitial.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            fClear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMode();
        }
        void AddMode()
        {
            try
            {
                //Button status
                btnAdd.Visible = false;
                btnEdit.Visible = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Visible = true;

                //Form status
                cbbPartCategory.Text = "";
                tInitial.Text = "";
                cbbPartCategory.Enabled = true;
                tInitial.Enabled = true;

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
                conn = db.GetConnString();
                sql = "INSERT INTO asp_partcategoryinit (category,initial,insertdate,insertby,postdate,postby) VALUES " +
                    " ('" + cbbPartCategory.Text + "','" + tInitial.Text + "',GETDATE(),'" + UserAccount.GetuserID() + "',GETDATE(),'" + UserAccount.GetuserID() + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                DisplayData();
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
            bool result = false;
            try
            {

                if (cbbPartCategory.Text=="")
                {
                    MessageBox.Show("Please Fill up Part Category!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbPartCategory.Focus();
                    return result;
                }

                if (tInitial.Text == "")
                {
                    MessageBox.Show("Please fill up Initial", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tInitial.Focus();
                    return result;
                }

                SqlDataAdapter adapter;
                SqlConnection conn;
                DataTable dt;
                conn = db.GetConnString();
                string sql = "";
                sql = "select * from asp_partcategoryinit where category='" + cbbPartCategory.Text + "'";
                adapter = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                if(dt.Rows.Count >0)
                {
                    MessageBox.Show(cbbPartCategory.Text+ " Already Exist..!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode();
        }
        void EditMode()
        {
            try
            {
                //Button status
                btnAdd.Visible = true;
                btnAdd.Enabled = false;
                btnEdit.Visible = false;
                btnDelete.Enabled = false;
                btnUpdate.Visible = true;
                btnUpdate.Enabled = true;
                btnCancel.Visible = true;

                //Form status
                cbbPartCategory.Enabled = true;
                tInitial.Enabled = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        void DisplayValue()
        {
            try
            {
                cbbPartCategory.Text = dgvReport.SelectedRows[0].Cells["Part Category"].Value.ToString();
                idd.Text = dgvReport.SelectedRows[0].Cells["Part Category"].Value.ToString();
                tInitial.Text = dgvReport.SelectedRows[0].Cells["Initial"].Value.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure Update di Data ...?? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                UpdateRecord();
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
                sql = "UPDATE asp_partcategoryinit SET category='"+cbbPartCategory.Text+"',initial='" + tInitial.Text + "',postdate=GETDATE(),postby='" + UserAccount.GetuserID() + "' where category='" + idd.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                DisplayData();
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

        private void dgvReport_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                sql = "Delete from asp_partcategoryinit where category='" + idd.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                DisplayData();
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            string path = "";
            string sqls = "";
            SqlConnection conns = null;
            SqlCommand cmds = null;
            //string csv = "";
            try
            {
                btnImport.Enabled = false;
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
                            sqls = "EXEC asp_importinitial '" + dt.Rows[ix].ItemArray.GetValue(0).ToString() + "','" + dt.Rows[ix].ItemArray.GetValue(1).ToString() + "','"+ UserAccount.GetuserName() + "','" + UserAccount.GetuserName() + "'";
                            cmds = new SqlCommand(sqls, conns);
                            cmds.ExecuteNonQuery();
                            //csv += dt.Rows[ix].ItemArray.GetValue(0).ToString() + " " + dt.Rows[ix].ItemArray.GetValue(1).ToString();
                        }
                    }
                    txtStatus.Visible = true;
                    btnImport.Enabled = true;
                    DisplayData();
                }

                
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

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                fClear();
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Visible = true;
                DisplayValue();
            }
        }
    }
}
