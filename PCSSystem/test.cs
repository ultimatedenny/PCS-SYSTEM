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

namespace PCSSystem
{
    public partial class test : Form
    {
        Common cm = new Common();
        database db = new database();
        string sql;
        DataSet ds;
        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            LoadDataSet();
            checAll();
        }

        void checAll()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[0].Value = true;
            }
        }
        void LoadDataSet()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = "EXEC asp_pp57_jobrequest '" + cbbPlant.Text + "','" + cbbProduct.Text + "'";
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            //dataGridView1.Rows[0].Cells[0].Value = true;
            //cb1.TrueValue = true;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];
            //lblRows.Text = "Total Rows: " + dataGridView1.Rows.Count.ToString();
            // dtDetail = null;
            // dtDetail = ds.Tables[1];
            //   checkAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                // row.Cells[0].Value = row.Cells[0].Value == null ? false : !(bool)row.Cells[0].Value;
                row.Cells[0].Value = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[0].Value = true;
            }
        }
    }
}
