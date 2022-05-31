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
    public partial class FMasterCap : Form
    {
        database db = new database();
        Common cm = new Common();
        string mac = System.Environment.MachineName.ToUpper();
        public FMasterCap()
        {
            InitializeComponent();
        }

        private void FMasterCap_Load(object sender, EventArgs e)
        {
            try
            {
                db.SetPlant(ref cbbPlant);
                if (cbbPlant.Items.Count > 0)
                {
                    cbbPlant.SelectedIndex = 0;
                }

                db.SetCap(ref cbbCap);
                if (cbbCap.Items.Count > 0)
                {
                    cbbCap.Items.Insert(0, "[ALL]");
                    cbbCap.SelectedIndex = 0;
                }
                SetSummaryDays();
                if(!cm.Check_Editable(this.Name)){
                    btnExport.Enabled = false;
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

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPlant.SelectedIndex >= 0)
                {
                    db.SetProduct(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                    if (cbbProduct.Items.Count > 0)
                    {
                        cbbProduct.Items.Insert(0, "[ALL]");
                        cbbProduct.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbProduct.SelectedIndex >= 0)
                {
                    db.SetLine(ref cbbLine, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbLine.Items.Count > 0)
                    {
                        cbbLine.Items.Insert(0, "[ALL]");
                        cbbLine.SelectedIndex = 0;
                    }
                    db.SetModel(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                    if (cbbModel.Items.Count > 0)
                    {
                        cbbModel.Items.Insert(0, "[ALL]");
                        cbbModel.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            string plant, product, model, prodnline, sat, wd, nw,eff;
            try
            {
                conn = db.GetConnString();
                plant = "%";
                if (cbbPlant.SelectedIndex > 0)
                    plant = cbbPlant.SelectedItem.ToString();
                product = "%";
                if (cbbProduct.SelectedIndex > 0)
                    product= cbbProduct.SelectedItem.ToString();
                model = "%";
                if (cbbModel.SelectedIndex > 0)
                    model= cbbModel.SelectedItem.ToString();
                prodnline= "%";
                if (cbbLine.SelectedIndex > 0)
                    prodnline = cbbLine.SelectedItem.ToString();
                sat = txtSaturday.Text;
                wd = txtWorkDays.Text;
                nw = txtNonWork.Text;

                if (chkEff.Checked)
                    eff = "1";
                else
                    eff = "0";

                sql = "EXEC Master_Cap_Plan @plant='"+plant+"', @model='"+model+"', @product='"+product+
                    "', @prodnline='"+prodnline+"', @wd='"+wd+"', @sat='"+sat+"', @nw='"+nw+"',@eff='"+eff+"'";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                #region formatdatagrid
                dgvReport.Columns["CapN"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN"].Width= 80;
                dgvReport.Columns["CapN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN1"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN1"].Width = 80;
                dgvReport.Columns["CapN1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN2"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN2"].Width = 80;
                dgvReport.Columns["CapN2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN12"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN12"].Width = 80;
                dgvReport.Columns["CapN12"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN12S"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN12S"].Width = 80;
                dgvReport.Columns["CapN12S"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["OutputHour"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["OutputHour"].HeaderText = "O/H";
                dgvReport.Columns["OutputHour"].Width = 50;
                dgvReport.Columns["OutputHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Plant"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Product"].Width = 30;
                dgvReport.Columns["Model"].Width = 80;
                dgvReport.Columns["Model"].HeaderText = "Product Group";
                dgvReport.Columns["SAPWC"].Width = 50;
                dgvReport.Columns["SAPWC"].HeaderText = "ProdnLine";
                dgvReport.Columns["ManPower"].HeaderText = "MP";
                dgvReport.Columns["ManPower"].Width = 30;
                dgvReport.Columns["ManPower"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Efficiency"].HeaderText = "Effcy";
                dgvReport.Columns["Efficiency"].Width = 50;
                dgvReport.Columns["Efficiency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                #endregion

                dgvReport.Columns["CAPN"].Visible = false;
                dgvReport.Columns["CAPN1"].Visible = false;
                dgvReport.Columns["CAPN2"].Visible = false;
                dgvReport.Columns["CAPN12"].Visible = false;
                dgvReport.Columns["CAPN12S"].Visible = false;
                if (cbbCap.SelectedIndex > 0)
                {
                    dgvReport.Columns[cbbCap.SelectedItem.ToString()].Visible = true;
                }
                else
                {
                    dgvReport.Columns["CAPN"].Visible = true;
                    dgvReport.Columns["CAPN1"].Visible = true;
                    dgvReport.Columns["CAPN2"].Visible = true;
                    dgvReport.Columns["CAPN12"].Visible = true;
                    dgvReport.Columns["CAPN12S"].Visible = true;
                }

                dgvReport.Columns["Efficiency"].Visible = Convert.ToBoolean( Convert.ToInt16(eff));

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
                        
                        header.Add("Report: Master Capacity");
                        header.Add("Capacity Plan from: " + dtpFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Capacity Plan To: " + dtpTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
                        header.Add("Product: " + cbbProduct.SelectedItem.ToString());
                        header.Add("Prodn Line: " + cbbLine.SelectedItem.ToString());
                        header.Add("Model: " + cbbModel.SelectedItem.ToString());
                        header.Add("Capacity: " + cbbCap.SelectedItem.ToString());
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

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpFrom.Value) >= DateTime.Today)
            {
                SetSummaryDays();
            }
            else
            {
                MessageBox.Show("You cannot select the day before today!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFrom.Value = DateTime.Today;
            }  
        }


        bool SetSummaryDays()
        {
            bool ok = false;
            string sql = "";
            string periods="";
            DateTime dto, dfrom;
            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader reader;
            try
            {
                conn = db.GetConnString();
                dto = dtpTo.Value.Date;
                dfrom = dtpFrom.Value.Date;

                while (dto >=dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','"+mac+"'),";
                    dfrom=dfrom.AddDays(1);
                }
                txtNonWork.Text = "0";
                txtWorkDays.Text = "0";
                txtSaturday.Text = "0";
                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);
                    sql = "DELETE FROM TPCS_TEMPDATE WHERE MacName='" + mac + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    sql = "INSERT INTO TPCS_TEMPDATE (TempDate, MacName) VALUES " + periods;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    sql = "EXEC Get_Day_Summary @mac='"+mac+"'";
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["Type"].ToString() == "WD")
                        {
                            txtWorkDays.Text = reader["Days"].ToString();
                        }
                        else if (reader["Type"].ToString() == "NW")
                        {
                            txtNonWork.Text = reader["Days"].ToString();
                        }
                        else
                        {
                            txtSaturday.Text = reader["Days"].ToString();
                        }
                    }
                }
                      
            }
            catch(Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value <= dtpTo.Value)
            {
                SetSummaryDays();
            }
            else
            {
                MessageBox.Show("Load Plan Date to must be greater than Load Plan Date From!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpTo.Value = dtpFrom.Value;
            }
        }


        
    }
}
