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
using System.Globalization;

namespace PCSSystem
{
    public partial class FMasCapSchLine : Form{
    
        private bool IsDLP = false;
        database db = new database();
        Common cm = new Common();
        string mac = System.Environment.MachineName.ToUpper();
        string myPrdnPeriod = "", myPrdnSDate = "", myPrdnEDate = "";

        private string loadfrom, loadto, schfrom, schto, plant, product, line, cap, wd, st, nw;

        public FMasCapSchLine()
        {
            InitializeComponent();
        }

        public FMasCapSchLine(bool _dlp, string _loadfrom, string _loadto, string _schfrom, string _schto, 
            string _plant, string _product, string _line, string _cap, string _wd, string _st, string _nw)
        {
            InitializeComponent();
            IsDLP=_dlp;
            loadfrom = _loadfrom;
            loadto = _loadto;
            schfrom = _schfrom;
            schto = _schto;
            plant = _plant;
            product = _product;
            line = _line;
            cap = _cap;
            wd = _wd;
            st = _st;
            nw = _nw;

        }

        private void FMasCapSchLine_Load(object sender, EventArgs e)
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
                if (!cm.Check_Editable(this.Name)){
                    btnExport.Enabled = false;
                }
                GetPrdnDays();
                if (myPrdnPeriod != "")
                {
                    dtpSchFrom.Value = DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpSchTo.Value = DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {

                }
                //dtpSchFrom.Value = Convert.ToDateTime(DateTime.Today.Year.ToString()+"-" + DateTime.Today.Month.ToString() + "-01");

                if (IsDLP)
                {
                    dtpMCFrom.Enabled = false;
                    dtpMCTo.Enabled = false;
                    dtpSchFrom.Enabled = false;
                    dtpSchTo.Enabled = false;
                    cbbPlant.Enabled = false;
                    cbbProduct.Enabled = false;
                    cbbLine.Enabled = false;
                    cbbCap.Enabled = false;

                    dtpMCFrom.Value = DateTime.ParseExact(loadfrom, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpMCTo.Value = DateTime.ParseExact(loadto, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpSchFrom.Value = DateTime.ParseExact(schfrom, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpSchTo.Value = DateTime.ParseExact(schto, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    cbbPlant.SelectedItem = plant;
                    cbbProduct.SelectedItem = product;
                    cbbLine.SelectedItem = line;
                    cbbCap.SelectedItem = cap;
                    txtWorkDays.Text = wd;
                    txtSaturday.Text = st;
                    txtNonWork.Text = nw;

                    btnView.PerformClick();

                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        void GetPrdnDays()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader reader;

            try
            {
                conn = db.GetConnString();
                sql = "SELECT Period, StartDate, EndDate from tpcs_prodnday where " +
                    " CAST(StartDate AS Date) <= CAST(GETDATE() as Date) and CAST(EndDate AS Date) >= CAST (GetDate() as Date)";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    myPrdnPeriod = reader["Period"].ToString();
                    myPrdnSDate = Convert.ToDateTime(reader["StartDate"]).ToString("MM/dd/yyyy");
                    myPrdnEDate = Convert.ToDateTime(reader["EndDate"]).ToString("MM/dd/yyyy");
                }
                else
                {
                    myPrdnPeriod = "";
                    myPrdnSDate = "";
                    myPrdnEDate = "";
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }


        bool SetSummaryDays()
        {
            bool ok = false;
            string sql = "";
            string periods = "";
            DateTime dto, dfrom;
            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader reader;
            try
            {
                conn = db.GetConnString();
                dto = dtpMCTo.Value.Date;
                dfrom = dtpMCFrom.Value.Date;

                while (dto >= dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','" + mac + "'),";
                    dfrom = dfrom.AddDays(1);
                }

                txtNonWork.Text = "0";
                txtWorkDays.Text = "0";
                txtSaturday.Text = "0";
                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);

                    sql = "DELETE FROM TPCS_TEMPDATE WHERE MacName='"+mac+"'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    
                    sql = "INSERT INTO TPCS_TEMPDATE (TempDate,MacName) VALUES " + periods;
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
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        private void dtpMCFrom_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpMCFrom.Value) >= DateTime.Today)
            {
                SetSummaryDays();
            }
            else
            {
                MessageBox.Show("You cannot select the day before today!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpMCFrom.Value = DateTime.Today;
            } 
        }

        private void dtpMCTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpMCFrom.Value <= dtpMCTo.Value)
            {
                SetSummaryDays();
            }
            else
            {
                MessageBox.Show("Load Plan Date to must be greater than Load Plan Date From!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpMCTo.Value = dtpMCFrom.Value;
            }
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

        private void btnView_Click(object sender, EventArgs e)
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            string plant, product, line, sat, wd, nw;
            string periods = "";
            DateTime dfrom, dto;
            SqlCommand cmd;
            
            try
            {
                conn = db.GetConnString();
                plant = "";
                if (cbbPlant.SelectedIndex >= 0)
                    plant = cbbPlant.SelectedItem.ToString();
                product = "%";
                if (cbbProduct.SelectedIndex > 0)
                    product = cbbProduct.SelectedItem.ToString();
                line = "%";
                if (cbbLine.SelectedIndex > 0)
                    line = cbbLine.SelectedItem.ToString();

                sat = txtSaturday.Text;
                wd = txtWorkDays.Text;
                nw = txtNonWork.Text;

                dto = dtpMCTo.Value.Date;
                dfrom = dtpMCFrom.Value.Date;

                while (dto >= dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','" + mac + "'),";
                    dfrom = dfrom.AddDays(1);
                }

                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);
                    sql = "INSERT INTO TPCS_TEMPDATE (TempDate,MacName) VALUES " + periods;
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                
                sql = "EXEC Master_Cap_Plan_vs_Schedule_byLine @plant='" + plant + "', @line='" + line +
                    "', @product='" + product +
                    "', @wd=" + wd + ", @sat=" + sat + ", @nw=" + nw +
                    ", @dsfrom = '" + dtpSchFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00', " +
                    "@dsto = '" + dtpSchTo.Value.ToString("yyyy-MM-dd") + " 23:59:59', @mac='"+mac+"'";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.DataSource = dt;

                #region formatdatagrid
                dgvReport.Columns["CapN"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN"].Width = 80;
                dgvReport.Columns["CapN"].HeaderText= "PCAPN";
                
                dgvReport.Columns["DiffCapNSchd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["DiffCapNSchd"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["DiffCapNSchd"].Width = 80;
                dgvReport.Columns["DiffCapNSchd"].HeaderText= "PCAPNvsSchd";

                dgvReport.Columns["DiffCapN1Schd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["DiffCapN1Schd"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["DiffCapN1Schd"].Width = 80;
                dgvReport.Columns["DiffCapN1Schd"].HeaderText = "PCAPN1vsSchd";

                dgvReport.Columns["DiffCapN2Schd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["DiffCapN2Schd"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["DiffCapN2Schd"].Width = 80;
                dgvReport.Columns["DiffCapN2Schd"].HeaderText = "PCAPNv2sSchd";

                dgvReport.Columns["DiffCapN12Schd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["DiffCapN12Schd"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["DiffCapN12Schd"].Width = 80;
                dgvReport.Columns["DiffCapN12Schd"].HeaderText = "PCAPN12vsSchd";

                dgvReport.Columns["DiffCapN12SSchd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["DiffCapN12SSchd"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["DiffCapN12SSchd"].Width = 80;
                dgvReport.Columns["DiffCapN12SSchd"].HeaderText = "PCAPN12SvsSchd";

                dgvReport.Columns["CapN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN1"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN1"].Width = 80;
                dgvReport.Columns["CapN1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN1"].HeaderText = "PCAPN1";
                dgvReport.Columns["CapN2"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN2"].Width = 80;
                dgvReport.Columns["CapN2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN2"].HeaderText = "PCAPN2";
                dgvReport.Columns["CapN12"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN12"].Width = 80;
                dgvReport.Columns["CapN12"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN12"].HeaderText = "PCAPN12";
                dgvReport.Columns["CapN12S"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["CapN12S"].Width = 80;
                dgvReport.Columns["CapN12S"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["CapN12S"].HeaderText = "PCAPN12S";
                //dgvReport.Columns["OutputHour"].DefaultCellStyle.Format = "N0";
                //dgvReport.Columns["OutputHour"].HeaderText = "O/H";
                //dgvReport.Columns["OutputHour"].Width = 50;
                //dgvReport.Columns["OutputHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Plant"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Product"].Width = 30;
                dgvReport.Columns["SAPWC"].Width = 80;
                dgvReport.Columns["SAPWC"].HeaderText = "ProdnLine";
                dgvReport.Columns["ShiftRun"].Width = 40;
                //dgvReport.Columns["ManPower"].HeaderText = "MP";
                //dgvReport.Columns["ManPower"].Width = 30;
                //dgvReport.Columns["ManPower"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReport.Columns["Efficiency"].HeaderText = "Effcy";
                //dgvReport.Columns["Efficiency"].Width = 50;
                //dgvReport.Columns["Efficiency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Schedule"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["Schedule"].Width = 80;
                dgvReport.Columns["Schedule"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["Schedule"].HeaderText = "Schd";
                dgvReport.Columns["%CapN"].DefaultCellStyle.Format = "P0";
                dgvReport.Columns["%CapN"].Width = 80;
                dgvReport.Columns["%CapN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["%CapN"].HeaderText = "%PCAPN";
                dgvReport.Columns["%CapN1"].DefaultCellStyle.Format = "P0";
                dgvReport.Columns["%CapN1"].Width = 80;
                dgvReport.Columns["%CapN1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["%CapN1"].HeaderText = "%PCAPN1";
                dgvReport.Columns["%CapN2"].DefaultCellStyle.Format = "P0";
                dgvReport.Columns["%CapN2"].Width = 80;
                dgvReport.Columns["%CapN2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["%CapN2"].HeaderText = "%PCAPN2";
                dgvReport.Columns["%CapN12"].DefaultCellStyle.Format = "P0";
                dgvReport.Columns["%CapN12"].Width = 80;
                dgvReport.Columns["%CapN12"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["%CapN12"].HeaderText = "%PCAPN12";
                dgvReport.Columns["%CapN12S"].DefaultCellStyle.Format = "P0";
                dgvReport.Columns["%CapN12S"].Width = 80;
                dgvReport.Columns["%CapN12S"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["%CapN12S"].HeaderText = "%PCAPN12S";
                #endregion

                dgvReport.Columns["CAPN"].Visible = false;
                dgvReport.Columns["CAPN1"].Visible = false;
                dgvReport.Columns["CAPN2"].Visible = false;
                dgvReport.Columns["CAPN12"].Visible = false;
                dgvReport.Columns["CAPN12S"].Visible = false;
                dgvReport.Columns["%CAPN"].Visible = false;
                dgvReport.Columns["%CAPN1"].Visible = false;
                dgvReport.Columns["%CAPN2"].Visible = false;
                dgvReport.Columns["%CAPN12"].Visible = false;
                dgvReport.Columns["%CAPN12S"].Visible = false;
                dgvReport.Columns["DiffCapNSchd"].Visible = false;
                dgvReport.Columns["DiffCapN1Schd"].Visible = false;
                dgvReport.Columns["DiffCapN2Schd"].Visible = false;
                dgvReport.Columns["DiffCapN12Schd"].Visible = false;
                dgvReport.Columns["DiffCapN12SSchd"].Visible = false;
                if (cbbCap.SelectedIndex > 0)
                {
                    dgvReport.Columns[cbbCap.SelectedItem.ToString()].Visible = true;
                    dgvReport.Columns["%" + cbbCap.SelectedItem.ToString()].Visible = true;
                    dgvReport.Columns["Diff" + cbbCap.SelectedItem.ToString()+"Schd"].Visible = true;
                }
                else
                {
                    dgvReport.Columns["CAPN"].Visible = true;
                    dgvReport.Columns["CAPN1"].Visible = true;
                    dgvReport.Columns["CAPN2"].Visible = true;
                    dgvReport.Columns["CAPN12"].Visible = true;
                    dgvReport.Columns["CAPN12S"].Visible = true;
                    dgvReport.Columns["%CAPN"].Visible = true;
                    dgvReport.Columns["%CAPN1"].Visible = true;
                    dgvReport.Columns["%CAPN2"].Visible = true;
                    dgvReport.Columns["%CAPN12"].Visible = true;
                    dgvReport.Columns["%CAPN12S"].Visible = true;
                    dgvReport.Columns["DiffCapNSchd"].Visible = true;
                }

              
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

                        header.Add("Report: Master Capacity vs Schedule by Line");
                        header.Add("Capacity Plan from: " + dtpMCFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Capacity Plan To: " + dtpMCTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Schedule from: " + dtpSchFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Schedule To: " + dtpSchTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
                        header.Add("Product: " + cbbProduct.SelectedItem.ToString());
                        header.Add("Prodn Line: " + cbbLine.SelectedItem.ToString());
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

        private void dtpSchFrom_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpSchFrom.Value.Month < DateTime.Now.Month)
            //{
            //    dtpSchFrom.Value = DateTime.Today;
            //    MessageBox.Show("You cannot select the previous month!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            if (dtpSchFrom.Value < DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
            {
                dtpSchFrom.Value = DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                MessageBox.Show("You cannot select the previous period!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtpSchTo_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpSchTo.Value < dtpSchFrom.Value)
            //{
            //    dtpSchTo.Value = dtpSchFrom.Value;
            //    MessageBox.Show("Schedule To must be more than Schedule From!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            if (dtpSchTo.Value < dtpSchFrom.Value)
            {
                dtpSchTo.Value = dtpSchFrom.Value;
                MessageBox.Show("Schedule To must be more than Schedule From!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtpSchTo.Value > DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
            {
                dtpSchTo.Value = DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                MessageBox.Show("You cannot select the next period!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
