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

namespace PCSSystem.Reports
{
    public partial class FBalJR : Form
    {
        Common cm = new Common();
        database db = new database();
        string errortitle = "", errorsql = "";
        string Status = "";
        string sql;
        DataSet ds;

        public FBalJR(string PartCode)
        {
            InitializeComponent();
            label2.Text = PartCode;
        }

        private void FBalJR_Load(object sender, EventArgs e)
        {
            LoadDataset();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadDataset();
        }

        void LoadDataset()
        {
            dgView.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            sql = ("EXEC AJRListBal_View '"+label2.Text+"'");
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dgView.DataSource = ds.Tables[0];
        }
    }
}
