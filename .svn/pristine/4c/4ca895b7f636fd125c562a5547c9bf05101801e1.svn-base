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
    public partial class FCap : Form
    {
        Common cm = new Common();
        database db = new database();
        
        public FCap()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FCap_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        void DisplayData()
        {
            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();

                sql = "SELECT Capacity, Description, UpdateBy,UpdateDate from tpcs_capacity";
                
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

                        header.Add("Master Data: Capacity");
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
    }
}



