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
    public partial class FError : Form
    {
        
        Common cm = new Common();
        database db = new database();
        public FError()
        {
            InitializeComponent();
        }

        private void FError_Load(object sender, EventArgs e)
        {
            DisplayData();
            if (dgvReport.Rows.Count > 0)
            {
                dgvReport.Rows[0].Selected = true;
            }
        }


        void DisplayData()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            string sql="";
            try
            {
                conn = db.GetConnString();

                sql = "EXEC DisplayErrorlog @system='PCS'";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            DisplayDetail();
        }


        void DisplayDetail()
        {
            try
            {
                if (dgvReport.SelectedRows.Count > 0)
                {
                    txtInfo.Text = dgvReport.SelectedRows[0].Cells["Description"].Value.ToString();
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

                        header.Add("Report: Error Log");
                        
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
