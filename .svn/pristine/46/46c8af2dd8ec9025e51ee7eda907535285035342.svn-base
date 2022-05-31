using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
namespace PCSSystem.Reports
{
    public partial class FApproval : Form
    {
        Common cm = new Common();
        database db = new database();
        string sql;
        DataSet ds;
        DataTable dtDetail = new DataTable();
        string errortitle = "", errorsql = "";
        string mypart;

        public FApproval()
        {
            InitializeComponent();
        }

        private void FApproval_Load(object sender, EventArgs e)
        {
            LoadDataSet();
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }

        }

       
        private void dgRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgRequest.SelectedRows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dgRequest.CurrentRow.Cells[2].Value.ToString()))
                {
                    if (dtDetail != null)
                    {
                        DataView dataView = dtDetail.DefaultView;
                        string filter = "";
                        filter = "JobReq = '" + dgRequest.CurrentRow.Cells[2].Value.ToString() + "'";
                        dataView.RowFilter = filter;
                        dgRequestDetail.DataSource = dataView;
                    }
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPlant.SelectedIndex >= 0)
            {
                db.SetProduct(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                if (cbbProduct.Items.Count > 0)
                {
                    cbbProduct.SelectedIndex = 0;
                }
            }
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text =="&Select All")
            {
                button3.Text = "#Deselect All";
            }
            else
            {
                button3.Text = "#Select All";
            }
        }

        void LoadDataSet()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "EXEC AJR_Approval_view '"+cbbPlant.Text+"','"+cbbProduct.Text+"','"+textBox1.Text+"'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);

            dgRequest.DataSource = null;
            dgRequest.DataSource = ds.Tables[0];
            dtDetail = null;
            dtDetail = ds.Tables[1];
        }
    }
}
