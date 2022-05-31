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

namespace PCSSystem.ASP
{
    public partial class FMemail : Form
    {
        Common cm = new Common();
        database db = new database();
        string Status = "";
        DataSet ds;
        public FMemail()
        {
            InitializeComponent();
        }

        private void FMemail_Load(object sender, EventArgs e)
        {
            fClear();
            DisplayData();
        }
        void DisplayData()
        {
            dgvReport.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            string sql = "";
            sql = "select isactive as 'Active',email as 'Email',name as 'Name',jobtitle as 'Job Title',insertdate as 'Insert Date',insertby as 'Insert By',postdate as 'Post Date',postby as 'Post By',email_key from asp_email order by email";

            adapter = new SqlDataAdapter(sql, conn);

            ds = new DataSet();
            adapter.Fill(ds);
            dgvReport.DataSource = ds.Tables[0];


            /*#region formatgrid
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
            //dgvReport.Columns["From Qty"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            #endregion*/
            lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            fClear();
        }

        void fClear()
        {
            tEmail.Text = "";
            tName.Text = "";
            tJobtitle.Text = "";

            btnSave.Visible = false;
            btnAdd.Visible = true;
            btnAdd.Enabled = true;

            button2.Visible = true;
            button2.Enabled = false;
            btnEdit.Visible = true;
            
            tEmail.Enabled = false;
            tName.Enabled = false;
            tJobtitle.Enabled = false;
            
            btnDelete.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            tEmail.Text = "";
            tName.Text = "";
            tJobtitle.Text = "";


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
                btnCancel.Visible = true;
                Status = "ADD";
                button2.Enabled = false;

                tEmail.Enabled = true;
                tName.Enabled = true;
                tJobtitle.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;

                tEmail.Text = "";
                tName.Text = "";
                tJobtitle.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
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
                string sts;
                if(radioButton1.Checked==true)
                {
                    sts = "1";
                }
                else
                {
                    sts = "0";
                }
                conn = db.GetConnString();
                sql = "INSERT INTO asp_email (isactive,email,name,jobtitle,insertdate,insertby,postdate,postby) VALUES " +
                    " ('"+sts+"','"+tEmail.Text+"','"+tName.Text+"','"+tJobtitle.Text+"',GETDATE(),'" + UserAccount.GetuserID() + "',GETDATE(),'" + UserAccount.GetuserID() + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                DisplayData();
                fClear();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                //MessageBox.Show(ex.ToString());
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
                if (tEmail.Text=="")
                {
                    MessageBox.Show("Please fill up Email!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tEmail.Focus();
                    return result;
                }

                if (tName.Text == "")
                {
                    MessageBox.Show("Please fill up the Name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tName.Focus();
                    return result;
                }

                if(radioButton1.Checked==false && radioButton2.Checked==false)
                {
                    MessageBox.Show("Please Select IsActive", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return result;
                }

                if (tJobtitle.Text == "")
                {
                    MessageBox.Show("Please fill up the Job Title", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tName.Focus();
                    return result;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
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
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Visible = true;
                button2.Visible = false;

                tEmail.Enabled = true;
                tName.Enabled = true;
                tJobtitle.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                //txtCriteria.Enabled = false;
                //dgvReport.Enabled = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        void DisplayValue()
        {            
            try
            {
                tEmail.Text = dgvReport.SelectedRows[0].Cells["Email"].Value.ToString();
                tName.Text = dgvReport.SelectedRows[0].Cells["Name"].Value.ToString();
                tJobtitle.Text = dgvReport.SelectedRows[0].Cells["Job Title"].Value.ToString();
                if(dgvReport.SelectedRows[0].Cells["Active"].Value.ToString()=="1")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                tKey.Text = dgvReport.SelectedRows[0].Cells["email_key"].Value.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //UpdateRecord();
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
                string sts;
                if (radioButton1.Checked == true)
                {
                    sts = "1";
                }
                else
                {
                    sts = "0";
                }
                conn = db.GetConnString();
                sql = "UPDATE asp_email SET isactive='"+sts+ "',email='"+tEmail.Text+ "',name='"+tName.Text+ "',jobtitle='"+tJobtitle.Text+ "',postdate=GETDATE(),postby='1'" +
                       "where email_key='" + tKey.Text + "'";

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
                sql = "Delete from  asp_email where email_key='" + tKey.Text + "'";
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

                        header.Add("Master Data: Email List");
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
            if (dgvReport.SelectedRows.Count > 0)
            {
                fClear();
                button2.Visible = true;
                button2.Enabled = true;
                DisplayValue();
                btnDelete.Enabled = true;
            }
        }
    }
}
