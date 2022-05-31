using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PCSSystem
{
    public partial class FNonWorkDay : Form
    {
        private string myMac="";
        database db = new database();
        Common cm = new Common();
        string sql = "";
        SqlCommand cmd;
        SqlDataAdapter adapter;
        //SqlTransaction trans;
        SqlConnection conn;
        DataTable dt;

        public FNonWorkDay()
        {
            InitializeComponent();
        }

        public FNonWorkDay(string MacName)
        {
            InitializeComponent();
            myMac = MacName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FNonWorkDay_Load(object sender, EventArgs e)
        {
            DisplayData();
            
        }

        void DisplayData()
        {
            try
            {
                conn = db.GetConnString();
                dt = new DataTable();
                sql = "SELECT IsHoliday, (RIGHT(Tempdate,2)+'.'+SUBSTRING(Tempdate,5,2)+'.'+LEFT(TempDate,4)) as 'NonWD', DATENAME(dw,CAST(tempdate as Date)) as 'Day',"+
                    " ISNULL(Holiday,'') as 'Description', TempDate from tpcs_tempdate_dlp t1 LEFT JOIN TPCS_NONWORKDAY t2 " +
                    " on SUBSTRING(t1.TempDate,3,6)=PCSDate "+
                    " where MacName='"+myMac+"' and IsHoliday is not null "+
                    " ORDER BY TempDate ";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
                dgvReport.Columns["IsHoliday"].HeaderText = "";
                dgvReport.Columns["IsHoliday"].Width = 30;
                dgvReport.Columns["TempDate"].Visible = false;

                conn.Dispose();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                conn = db.GetConnString();
                sql = "UPDATE TPCS_TEMPDATE_DLP SET IsHoliday=@v where MacName='"+myMac+"' and TempDate=@date";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@v", SqlDbType.Bit);
                cmd.Parameters.Add("@date", SqlDbType.NVarChar);
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells[0].Value))
                        cmd.Parameters["@v"].Value = 1;
                    else
                        cmd.Parameters["@v"].Value = 0;

                    cmd.Parameters["@date"].Value = dgvReport.Rows[i].Cells["TempDate"].Value.ToString();

                    cmd.ExecuteNonQuery();
                }
                
                conn.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                if (e.ColumnIndex == 0)
                    dgvReport.Rows[e.RowIndex].Cells[0].Value = ! Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value);
        }

    }
}
