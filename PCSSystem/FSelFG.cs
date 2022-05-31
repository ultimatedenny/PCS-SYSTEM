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
    public partial class FSelFG : Form
    {
        private string _plant, _model;
        Common cm = new Common();
        database db = new database();
        //string errorsql = "", errortitle = "";
        //bool NewRecord = false;
       // SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlConnection conn;
       // SqlDataReader reader;
        string sql = "";

        public ArrayList selectedfg = new ArrayList();

        public FSelFG()
        {
            InitializeComponent();
        }

        public FSelFG(string plant, string model)
        {
            InitializeComponent();
            _plant = plant;
            _model = model;
        }

        private void FSelFG_Load(object sender, EventArgs e)
        {
            txtPlant.Text = _plant.Trim();
            txtModel.Text = _model.ToUpper().Trim();
            DisplayData();
        }

        private void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }

        void DisplayData()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT Material, MaterialDesc as 'Description' from tpcs_mat_model where Plant='"+_plant+ "' and Model='"+_model+"' ORDER BY Material";
                adapter = new SqlDataAdapter(sql, conn);

                adapter.Fill(dt);
                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                conn.Dispose();
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

        private void txtFGCode_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {
                    if (dgvReport.Rows[i].Cells["Material"].Value.ToString().IndexOf(txtFGCode.Text.ToUpper()) >= 0)
                    {
                        dgvReport.Rows[i].Selected = true;
                        break;
                    }
                }
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
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells["ChkCol"].Value))
                    {
                        selectedfg.Add(dgvReport.Rows[i].Cells["Material"].Value.ToString());
                    }
                }

                if (selectedfg.Count <= 0)
                    MessageBox.Show("You haven't selected any FG!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    this.Close();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                    if (e.ColumnIndex == 0)
                        dgvReport.Rows[e.RowIndex].Cells[0].Value = ! Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value);
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

    }
}
