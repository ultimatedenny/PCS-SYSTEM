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
    public partial class FInfo : Form
    {
        string _title, _sql;
        Common cm = new Common();
        database db = new database();
        public FInfo()
        {
            InitializeComponent();
        }
      
        public FInfo(string title, string sql)
        {
            InitializeComponent();
            _title = title;
            _sql = sql;
        }

        private void FInfo_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        void DisplayData()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                adapter = new SqlDataAdapter(_sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

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

                        header.Add(_title);                        
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
