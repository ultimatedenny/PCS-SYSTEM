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
    public partial class FNewSelFG : Form
    {
        private string _plant,_product,_line, _model;
        Common cm = new Common();
        database db = new database();
        //string errorsql = "", errortitle = "";
        //bool NewRecord = false;
       // SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlConnection conn;
       // SqlDataReader reader;
        string sql = "";
        bool _checked = false;

        public ArrayList selectedfg = new ArrayList();
        public ArrayList Noselectedfg = new ArrayList();
        string[] _matNewCooice;
        bool loadTMP = true;
        public FNewSelFG()
        {
            InitializeComponent();
        }

        public FNewSelFG(string plant, string product, string line, string model)
        {
            InitializeComponent();

            _plant = plant;
            _product = product;
            _line = line;
            _model = model;

            if (_line == "[ALL]")
                _line = "%";
            
            if (_model == "[ALL]")
                _model = "%";
            
        }

        private void FNewSelFG_Load(object sender, EventArgs e)
        {
            txtPlant.Text = _plant.Trim();
            txtModel.Text = _model.ToUpper().Trim();

            string _matNew = FNewLotInd._materialchooice.Replace("'","");
            _matNewCooice = _matNew.Split(',');

            if (Convert.ToInt32(_matNewCooice.Count()) > 1)
            {
                btnSelectedAll.Text = "Clear Selection";
                _checked = true;
            }
            else
            {
                btnSelectedAll.Text = "Select All";
                _checked = false;
            }

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
                //sql = "SELECT Material, MaterialDesc as 'Description' from tpcs_mat_model where Plant='"+_plant+ "' and Model='"+_model+"' ORDER BY Material";
                //sql = @"SELECT 
                //         a.Plant
                //        ,a.Product
                //        ,Line=a.SAPWC
                //        ,a.Model
                //        ,b.Material
                //        ,'Description'=b.MaterialDesc
                //        FROM TPCS_ROUTEMP a
                //        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                //        WHERE
                //        a.Plant = '" + _plant + @"'
                //        AND
                //        a.Product = '" + _product + @"'
                //        AND
                //        a.SAPWc LIKE '"+_line+ @"'
                //        AND
                //        a.Model LIKE '"+_model+ @"'
                //        AND 
                //        b.Material LIKE '" + txtFGCode.Text + @"%'
                //        ORDER BY b.Material";

                sql = @"SELECT Data=value 
						INTO #Data
						FROM fn_split_string('" + FNewLotInd._materialchooice.Replace("'", "") + @"', ',') WHERE ISNULL(value,'')<>''
				
						SELECT DISTINCT
							 REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 1)) AS [Line]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 2)) AS [Model]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 3)) AS [Material]
						INTO #DataOK
						FROM #Data;

                    SELECT 
                         a.Plant
                        ,a.Product
                        ,Line=a.SAPWC
                        ,a.Model
                        ,b.Material
                        ,'Description'=b.MaterialDesc
                        INTO #DataMaster
                        FROM TPCS_ROUTEMP a
                        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                        WHERE
                        a.Plant = '" + _plant + @"'
                        AND
                        a.Product = '" + _product + @"'
                        AND
                        a.SAPWc LIKE '" + _line + @"'
                        AND
                        a.Model LIKE '" + _model + @"'
                        AND 
                        b.Material LIKE '" + txtFGCode.Text + @"%'
                        ORDER BY b.Material

                        SELECT a.*
						,[Check]=CASE WHEN ISNULL(b.Line,'')<>'' THEN 1 ELSE 0 END 
						FROM #DataMaster a
						LEFT JOIN #DataOK b ON a.Material=b.Material AND a.Model=b.Model AND a.Line=b.Line


						DROP TABLE #Data,#DataOK,#DataMaster";


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

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (loadTMP == true)
                {
                    for (int i = 0; i < dgvReport.Rows.Count; i++)
                    {
                        if (dgvReport.Rows[i].Cells["Check"].Value.ToString() == "1")
                            dgvReport.Rows[i].Cells["ChkCol"].Value = true;
                        //for (int x = 0; x < Convert.ToInt32(_matNewCooice.Count()); x++)
                        //{
                        //    if (dgvReport.Rows[i].Cells["Material"].Value.ToString() == _matNewCooice[x].ToString())
                        //        dgvReport.Rows[i].Cells["ChkCol"].Value = true;
                        //}
                    }
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            
        }

        private void btnSelectedAll_Click(object sender, EventArgs e)
        {
            if (btnSelectedAll.Text == "Clear Selection")
            {
                btnSelectedAll.Text = "Select All";
                _checked = false;
            }
            else
            {
                btnSelectedAll.Text = "Clear Selection";
                _checked = true;
            }

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkCol"] as DataGridViewCheckBoxCell);
                checkBox.Value = _checked;
                loadTMP = false;
            }
        }

        private void txtFGCode_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                //for (int i = 0; i < dgvReport.Rows.Count; i++)
                //{
                //    if (dgvReport.Rows[i].Cells["Material"].Value.ToString().IndexOf(txtFGCode.Text.ToUpper()) >= 0)
                //    {
                //        dgvReport.Rows[i].Selected = true;
                //        break;
                //    }
                //}
                DisplayData();
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
                selectedfg.Clear();
                Noselectedfg.Clear();
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells["ChkCol"].Value))
                    {
                        //selectedfg.Add(dgvReport.Rows[i].Cells["Material"].Value.ToString());
                        selectedfg.Add(dgvReport.Rows[i].Cells["Line"].Value.ToString()+"|"+dgvReport.Rows[i].Cells["Model"].Value.ToString() + "|" + dgvReport.Rows[i].Cells["Material"].Value.ToString());
                    }
                    else
                    {
                        Noselectedfg.Add(dgvReport.Rows[i].Cells["Line"].Value.ToString() + "|" + dgvReport.Rows[i].Cells["Model"].Value.ToString() + "|" + dgvReport.Rows[i].Cells["Material"].Value.ToString());
                    }
                }

                if (selectedfg.Count <= 0)
                {
                    MessageBox.Show("You haven't selected any FG!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
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
                loadTMP = false;
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
