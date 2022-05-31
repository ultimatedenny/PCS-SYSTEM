using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PCSSystem
{
    public partial class FDailyPlan : Form
    {
        bool CanClose = true;
        database db = new database();
        Common cm = new Common();
        string errorsql, errortitle;
        string mac = Environment.MachineName.ToUpper();
        string myPlant="";
        string myProduct = "";
        string myDLPNo = "";
        string myPrdnPeriod="", myPrdnSDate="", myPrdnEDate = "";
        private bool timerOn = true;
        private bool AutoClose = false;
        private int AutoCloseTime = 0;
        private void Application_Idle(object sender, EventArgs e)
        {
            //    The application is now idle.
            
            try
            {
                if (timerOn)
                {
                    timer1.Stop();
                    timer1.Enabled = true;
                    timer1.Interval = (Convert.ToInt32(AutoCloseTime) * 60) * 1000;
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }            
        }



        public FDailyPlan(string plant, string product)
        {
            InitializeComponent();
             Application.Idle += Application_Idle;
            myPlant=plant;
            myProduct= product;
        }

        public FDailyPlan()
        {
            InitializeComponent();
            Application.Idle += Application_Idle;
        }

        void DisplayData()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            
            DataTable dt = new DataTable();
            string spc, remark = "";

            try
            {
                conn = db.GetConnString();

                remark = "";
                if (chkProd.Checked)
                {
                    remark = "'IN PRODUCTION',";
                }

                if (chkCompleted.Checked)
                {
                    remark = remark+"'COMPLETED',";
                }

                spc = "";
                if (chkSPC.Checked)
                {
                    spc = " AND ISNULL(MasterCode,'') <> '' ";
                }

                if (remark.Length > 0)
                {
                    remark = " AND Remark NOT IN (" + remark.Substring(0, remark.Length - 1) + ")";
                }

                sql = "SELECT Selected, RecordId, SchDate, Plant, Product, Material, MaterialDesc, Model, "+
                    " ProdnLine, Shift, LotQty, LotNo, PV, LeaderID,PackerId, ReqHours, ReqDate, "+
                    " Remark,MasterCode as 'SPC',MasterName as 'SPCName', ISNULL(MasterId,'') as 'SPCId'" +
                    " FROM TPCS_TOBEJEQ WHERE DLPNo='" + myDLPNo + "'"+remark+spc;
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                dgvReport.Columns.Clear();
                dgvReport.DataSource = dt;

                //DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                //dgvReport.Columns.Insert(0, col);
                //dgvReport.Columns[0].HeaderText = "";
                //dgvReport.Columns[0].Width = 40;

                dgvReport.Columns["Selected"].Width = 40;
                dgvReport.Columns["PV"].Width = 50;
                dgvReport.Columns["Selected"].HeaderText = "";
                dgvReport.Columns["RecordId"].Visible = false;
                dgvReport.Columns["Plant"].Visible = false;
                dgvReport.Columns["Product"].Visible = false;
                dgvReport.Columns["ReqDate"].HeaderText= "SAP ReqDate";
                #region formatdatagrid

                dgvReport.Columns["SPCId"].Width = 60;
                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Product"].Width = 40;
                dgvReport.Columns["Product"].HeaderText = "Prdt";
                dgvReport.Columns["Shift"].Width = 40;
                dgvReport.Columns["Shift"].HeaderText = "Shift";
                dgvReport.Columns["LotNo"].Width = 40;                
                dgvReport.Columns["Model"].Width = 80;
                dgvReport.Columns["Model"].HeaderText = "Product Group";
                dgvReport.Columns["SchDate"].Width = 60;
                dgvReport.Columns["SchDate"].HeaderText = "Period";
                dgvReport.Columns["ProdnLine"].HeaderText = "Line";
                dgvReport.Columns["ProdnLine"].Width = 40;
                dgvReport.Columns["ProdnLine"].Width = 40;
                dgvReport.Columns["MaterialDesc"].Width = 150;
                dgvReport.Columns["LotQty"].Width = 50;
                dgvReport.Columns["LotQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                #endregion
                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string sql = "";
            
            SqlConnection conn;
            SqlCommand cmd;
            SqlTransaction trans = null;
            DataTable dt = new DataTable();
            DateTime dto, dfrom;
            string plant, product, model, sat, wd, nw,line,cap,periods,status;
            bool proceed = false;
            int tempdatecount = 0;
            try
            {

                if (Convert.ToInt32(dtpSchFrom.Value.ToString("yyMMdd")) >= Convert.ToInt32(DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyMMdd")))
                {
                    if (Convert.ToInt32(dtpSchTo.Value.ToString("yyMMdd")) <= Convert.ToInt32(DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyMMdd")))
                    {
                        proceed = true;
                    }
                }

                if (!proceed)
                {
                    dgvReport.Columns.Clear();
                    MessageBox.Show("The Schedule date you selected must be between " + myPrdnSDate + " to " + myPrdnEDate + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conn = db.GetConnString();
                trans = conn.BeginTransaction();

                plant = cbbPlant.Text;
                product = cbbProduct.Text;
                //if (cbbPlant.SelectedIndex > 0)
                //    plant = cbbPlant.SelectedItem.ToString();
                //product = "%";
                //if (cbbProduct.SelectedIndex > 0)
                //    product = cbbProduct.SelectedItem.ToString();
                
                model = "%";
                if (cbbModel.SelectedIndex > 0)
                    model = cbbModel.SelectedItem.ToString();
                line= "%";
                if (cbbLine.SelectedIndex > 0)
                    line = cbbLine.SelectedItem.ToString();
                cap = "%";
                if (cbbCap.SelectedIndex >= 0)
                    cap= cbbCap.SelectedItem.ToString();
                if (cbbStatus.SelectedIndex > 0)
                    status = cbbStatus.SelectedItem.ToString();
                else
                status = "%";
                sat = txtSaturday.Text;
                wd = txtWorkDays.Text;
                nw = txtNonWork.Text;

                dto = dtpMCTo.Value.Date;
                dfrom = dtpMCFrom.Value.Date;
                periods = "";
                 while (dto >= dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','"+mac+"'),";
                    dfrom = dfrom.AddDays(1);
                }

                //txtNonWork.Text = "0";
                //txtWorkDays.Text = "0";
                //txtSaturday.Text = "0";

                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);                 
                }

                sql = "SELECT COUNT(TempDate) from TPCS_TEMPDATE_DLP where MacName='"+System.Environment.MachineName+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                if (cmd.ExecuteScalar() == null)
                {
                    tempdatecount = 0;
                }
                else if (cmd.ExecuteScalar().ToString() == "")
                {
                    tempdatecount = 0;
                }
                else
                {
                    tempdatecount = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (tempdatecount == 0)
                {
                    sql = "DELETE FROM TPCS_TEMPDATE_DLP WHERE MacName='" + mac + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    sql = "INSERT INTO TPCS_TEMPDATE_DLP (TempDate, MacName) VALUES " + periods;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    //sql = " SET DATEFIRST 1 " +
                    //    " UPDATE TPCS_TEMPDATE SET IsHoliday='1' " +
                    //    " where MacName='" + System.Environment.MachineName + "' and Datepart(dw,TempDate)=7 ";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    sql = " UPDATE TPCS_TEMPDATE_DLP SET IsHoliday='1' from " +
                        " TPCS_TEMPDATE_DLP t1 inner join tpcs_nonworkday t2 on substring(t1.tempdate,3,6)=t2.pcsdate " +
                        " where t1.MacName='" + System.Environment.MachineName + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }                
                
                sql = "DELETE FROM TPCS_DLP WHERE DLPNo='" + myDLPNo + "'";
                sql = sql + " DELETE FROM TPCS_TOBEJEQ WHERE DLPNo='" + myDLPNo + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                                
                sql = "SELECT MAX(DLPNo) from TPCS_DLP WHERE Plant='" + cbbPlant.Text + "' AND Product='" + cbbProduct.Text + "'";
                cmd.CommandText = sql;
                myDLPNo = cmd.ExecuteScalar().ToString();


                string schdt = "";
                if (myDLPNo.Length > 0)
                {
                    schdt = myDLPNo.Substring(2, 6) ;
                }

                if (schdt == DateTime.Today.ToString("yyMMdd"))
                {
                    myDLPNo = myDLPNo.Substring(0, myDLPNo.Length - 3) + (Convert.ToInt32(myDLPNo.Substring(8))+1).ToString("000");
                }
                else
                {
                    myDLPNo = cbbProduct.Text + DateTime.Today.ToString("yyMMdd") + "001";
                }

                                
                sql = "EXEC Daily_Load_Plan @plant='" + plant + "', @model='" + model + "', @product='" + product +
                    "',@line='"+line+"', @dschFrom = '" + dtpSchFrom.Value.ToString("yyMMdd") + "', " +
                    "@dschTo = '" + dtpSchTo.Value.ToString("yyMMdd") + "', @dcapFrom= '"+
                    dtpMCFrom.Value.ToString("yyMMdd")+"', @dcapTo='"+dtpMCTo.Value.ToString("yyMMdd")+"', @cap='"+
                    cap+"', @mac='"+mac+"', @status='"+status+"', @dlpno='"+myDLPNo+
                    "', @user='"+UserAccount.GetuserID()+"', @wd='"+txtWorkDays.Text+"', @sat='"+txtSaturday.Text+"', @nwd='"+txtNonWork.Text+"'";

                //Console.WriteLine(sql);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //MessageBox.Show(dt.Rows.Count.ToString());
                //sql = "DELETE FROM TPCS_TEMPDATE WHERE MacName='" + mac + "'";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                trans.Commit();
                DisplayData();

                //dgvReport.Columns.Clear();
                //dgvReport.DataSource = dt;

                //DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                //dgvReport.Columns.Insert(0, col);
                //dgvReport.Columns[0].HeaderText = "";
                //dgvReport.Columns[0].Width= 40;

                //DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
                //dgvReport.Columns.Insert(1, txtcol);
                //dgvReport.Columns[1].HeaderText = "";
                //dgvReport.Columns[1].Width = 40;
                //dgvReport.Columns[1].Visible=false;
                //#region formatdatagrid
                
                //dgvReport.Columns["Plant"].Width = 50;
                //dgvReport.Columns["Product"].Width = 40;
                //dgvReport.Columns["Product"].HeaderText = "Prdt";
                //dgvReport.Columns["Shift"].Width = 40;
                //dgvReport.Columns["Shift"].HeaderText = "Shift";
                //dgvReport.Columns["Model"].Width = 80;
                //dgvReport.Columns["SchDate"].Width = 60;
                //dgvReport.Columns["SchDate"].HeaderText = "Period";
                //dgvReport.Columns["ProdnLine"].HeaderText = "Line";
                //dgvReport.Columns["ProdnLine"].Width = 40;
                //dgvReport.Columns["ProdnLine"].Width = 40;
                //dgvReport.Columns["MaterialDesc"].Width = 150;
                //dgvReport.Columns["LotQty"].Width = 50;
                //dgvReport.Columns["LotQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //#endregion
                CanClose = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
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
            SqlTransaction trans=null;
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                dto = dtpMCTo.Value.Date;
                dfrom = dtpMCFrom.Value.Date;

                while (dto >= dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','"+mac+"'),";
                    dfrom = dfrom.AddDays(1);
                }

                txtNonWork.Text = "0";
                txtWorkDays.Text = "0";
                txtSaturday.Text = "0";
                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);
                    sql = "DELETE FROM TPCS_TEMPDATE_DLP WHERE MacName='" + mac + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();

                    sql = "INSERT INTO TPCS_TEMPDATE_DLP (TempDate, MacName) VALUES " + periods;
                    cmd.CommandText=sql;
                    cmd.ExecuteNonQuery();

                    sql = " UPDATE TPCS_TEMPDATE_DLP SET IsHoliday='1' from " +
                    " TPCS_TEMPDATE_DLP t1 inner join tpcs_nonworkday t2 on substring(t1.tempdate,3,6)=t2.pcsdate " +
                    " where t1.MacName='" + System.Environment.MachineName + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    sql = " UPDATE TPCS_TEMPDATE_DLP SET IsHoliday='1' " +
                        " where DATENAME(dw, CAST(TempDate as Date)) ='Sunday' AND " +
                        " MacName='" + System.Environment.MachineName + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();


                    sql = "EXEC DaySummary @mac='"+mac+"'";
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
                    reader.Close();
                    trans.Commit();
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                db.SaveError(ex.ToString());
            }
            return ok;
        }


        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("DAILYLOADPLAN_STATUS");
                cbbStatus.Items.AddRange(cri.Split('|'));
                if (cbbStatus.Items.Count > 0)
                {
                    cbbStatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void FDailyPlan_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

                grdSummary.AutoGenerateColumns = false;
                dgPart.AutoGenerateColumns = false;
                dgBom.AutoGenerateColumns = false;
                AutoCloseTime = Convert.ToInt32(db.GetGlobal("DLPCLOSEMINS"));
                ClearTempDate();

                GetFilter();
                db.SetPlant(ref cbbPlant);
                cbbPlant.SelectedItem = myPlant;
                cbbPlant.Enabled = false;

                tabControl1.TabPages.Remove(tabPart);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPick);

                db.SetCap(ref cbbCap);
                if (cbbCap.Items.Count > 0)
                {
                    cbbCap.SelectedIndex = 0;
                }

                SetSummaryDays();
                if (!cm.Check_Editable(this.Name))
                {
                    btnExport.Enabled = false;
                }
                
                //dtpSchFrom.Enabled=false;
                //dtpSchFrom.Value = Convert.ToDateTime(DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-01");
                
                GetPrdnDays();
                if (myPrdnPeriod != "")
                {
                    dtpSchFrom.Value = DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpSchTo.Value = DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {

                }
                
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void GetPrdnDays(bool checkdate = false)
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader reader;

            try
            {
                conn = db.GetConnString();
                if ( checkdate == false)
                sql = @"SELECT Period, StartDate, EndDate from tpcs_prodnday where 
                        CAST(StartDate AS Date) <= CAST(GETDATE() as Date) and CAST(EndDate AS Date) >= CAST (GetDate() as Date)";
                else
                    sql = @"SELECT Period, StartDate, EndDate from tpcs_prodnday where 
                        CAST(StartDate AS Date) <= CAST('" + dtpMCFrom.Value.ToString("yyyyMMdd") + "' as Date) and CAST(EndDate AS Date) >= CAST ('" + dtpMCFrom.Value.ToString("yyyyMMdd") + "' as Date)";

                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    myPrdnPeriod = reader["Period"].ToString();
                    //myPrdnSDate = DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString();
                    //myPrdnEDate = DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString();

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

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbPlant.SelectedIndex >= 0)
                {
                    db.SetProduct(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                    if (cbbProduct.Items.Count > 0)
                    {                                                
                        cbbProduct.SelectedItem = myProduct;
                        cbbProduct.Enabled = false;
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

        private void dtpMCFrom_ValueChanged(object sender, EventArgs e)
        {

            GetPrdnDays(true);

            if (Convert.ToDateTime(dtpMCFrom.Value) >= DateTime.Today)
            {
                SetSummaryDays();
            }
            else
            {
                MessageBox.Show("You cannot select the day before today!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ArrayList header = new ArrayList();
            string path = "";
            try
            {
                //btnView.PerformClick();
                if (dgvReport.Rows.Count > 0)
                {
                    dgvReport.Columns[0].Visible = false;
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        header.Add("Report: Daily Load Plan");
                        header.Add("Capacity Plan from: " + dtpMCFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Capacity Plan To: " + dtpMCTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Schedule Plan from: " + dtpSchFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Schedule Plan To: " + dtpSchTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
                        header.Add("Product: " + cbbProduct.SelectedItem.ToString());
                        header.Add("Line: " + cbbLine.SelectedItem.ToString());
                        header.Add("Model: " + cbbModel.SelectedItem.ToString());
                        header.Add("Capacity: " + cbbCap.SelectedItem.ToString());
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        path = saveFileDialog1.FileName.ToString();

                        cm.Export_to_CSV(header, path, dgvReport);

                        //MoveBFToHistory(path);
                        dgvReport.Columns[0].Visible = true;
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

        bool MoveBFToHistory(string path)
        {
            string sql = "";
            SqlCommand cmd=null;
            SqlConnection conn=null;
            SqlTransaction trans = null;
            bool ok = false;
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "INSERT INTO TPCS_BF_HISTORY (Plant, Product, ProdnLine, "+
                    " FGCode, Qty, UpdateBy, UpdateDate, MacName, FileName,SchDate) " +
                    " SELECT Plant, Product, ProdnLine, FGCode,Qty,'"+UserAccount.GetuserID()+"',GETDATE(), "+
                    "'"+mac+"', '"+path+"', GETDATE() FROM TPCS_BF WHERE MacName='"+mac+"'";
                
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_BF_HISTORY SET FGDesc=t1.SPARABB FROM TPRD t1 INNER JOIN "+
                    " TPCS_BF_HISTORY t2 on t1.Procod2 = t2.FGCode WHERE ISNULL(t2.FGDesc,'')=''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_BF WHERE MacName='"+mac+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();

                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        private void btnUplBF_Click(object sender, EventArgs e)
        {
            string path = "";
            string[] fileheaders, tableheaders;
            string temp = "";
            try
            {
                txtStatus.Text = "Select the file...";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;

                    fileheaders = cm.GetFileHeaders(path);
                    temp = db.GetGlobal("HEADER_BF");

                    tableheaders = temp.Split('|');
                    txtStatus.Text = "Check File headers...";
                    if (cm.CheckHeader(fileheaders, tableheaders))
                    {
                        errortitle = "";
                        errorsql = "";
                        if (Import_Data(path, tableheaders))
                        {
                            txtStatus.Text = "Validating data...";
                            if (Validating_Data())
                            {
                                InsertIntoTable();
                                
                            }
                            else {
                                Remove_Data();
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool InsertIntoTable(bool replace = false)
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;

            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;

                if (replace)
                {
                    //sql = "DELETE FROM TPCS_ROUTEMP ";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                }
                else
                {
                    //sql = "DELETE t1 FROM TPCS_ROUTEMP t1 INNER JOIN TPCS_ROUTEMP_TEMP t2 on " +
                    //    " t1.Plant=t2.Plant AND t1.Product=t2.Product AND t1.Model=t2.Model AND " +
                    //    " t1.SAPWC=t2.SAPWC";
                    sql = "DELETE t1 FROM TPCS_BF t1 INNER JOIN TPCS_BF_TEMP t2 on " +
                        " t1.Plant=t2.Plant AND t1.Product=t2.Product and t2.MacName='"+mac+"'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_BF (Plant, Product,FGCode,FGDesc,ProdnLine,Qty,PV,MacName) " +                 
                        " SELECT Plant, Product,FGCode,FGDesc,ProdnLine,Qty,PV,MacName " +
                        " from TPCS_BF_TEMP WHERE MacName='"+mac+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_BF SET Period='" + DateTime.Today.ToString("yyyyMMdd") + "', UpdateBy='" + UserAccount.GetuserID().ToString() +
                    "', UpdateDate=GetDate() WHERE MacName='" + mac + "' and Period is null";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE from TPCS_BF_TEMP WHERE MacName='" + mac + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                txtStatus.Text = "Import Finished!";
                ok = true;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Import failed!";
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        private void btnUpl34_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPSch"))
            {                
                FSAPSch f = new FSAPSch();
                f.ShowDialog();
                f.Dispose();
            }
        }

       

        bool Import_Data(string path, string[] tableheaders)
        {
            bool ok = false;
            string sql = "";
            StreamReader sr = null;
            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            string header = "";
            string line = "";
            string[] lines = null;
            char delimiter = '\t';
            int rows = 0;
            int counts = 0;
            string sqlval = "";
            string columnnames = "";
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();

                sql = "DELETE FROM TPCS_BF_TEMP WHERE MacName='"+mac+"'";                
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                cmd.ExecuteNonQuery();

                sr = new StreamReader(path);
                header = sr.ReadLine();

                for (int i = 0; i < tableheaders.Length; i++)
                {
                    columnnames = columnnames + tableheaders[i] + ",";
                }
                columnnames = columnnames.Substring(0, columnnames.Length - 1);



                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    line = line.Replace("\"", "");
                    line = line.Replace(",", "");
                    lines = line.Split(delimiter);
                    cm.Quoting(ref lines);

                    sqlval = sqlval + "(";
                    for (int i = 0; i < tableheaders.Length; i++)
                    {
                        sqlval = sqlval + lines[i] + ",";
                    }
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);
                    sqlval = sqlval + "),";
                    rows++;
                    counts++;

                    txtStatus.Text = "Uploading: " + counts.ToString() + " rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_BF_TEMP (" + columnnames + ") " +
                            " VALUES " +
                            sqlval;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        rows = 0;
                        sqlval = "";
                    }
                }

                if (rows > 0)
                {
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);

                    sql = "INSERT INTO TPCS_BF_TEMP ("+columnnames+") " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }

                sql = "UPDATE TPCS_BF_TEMP SET MacName='"+mac+"' where ISNULL(MacName,'') =''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                trans.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                sr.Close();
                conn.Dispose();
            }
            return ok;
        }

        bool Validating_Data()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();
                sql = "SELECT DISTINCT(Plant) from TPCS_BF_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import Tobe B/F-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Plant, Product from TPCS_BF_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Product! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and t1.Product=t2.Product)";
                    errortitle = "Import Tobe B/F-Invalid Products";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT FGCode from TPCS_BF_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.FGCode=t2.Procod2)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid FG Code!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid FG Code! View Error.";
                    errorsql = "SELECT * from TPCS_BF_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPRD t2 WHERE t1.FGCode=t2.Procod2)";
                    errortitle = "Import Tobe B/F-Invalid FG Codes";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        bool Remove_Data()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE FROM TPCS_BF WHERE MacName='"+mac+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        private void txtStatus_Click(object sender, EventArgs e)
        {
            if (!(errortitle == ""))
            {
                FInfo f = new FInfo(errortitle, errorsql);
                f.ShowDialog();
                f.Dispose();
            }

        }

        private void dtpSchFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpSchFrom.Value < DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
            {
                dtpSchFrom.Value = DateTime.ParseExact(myPrdnSDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                MessageBox.Show("You cannot select the previous period!", "" , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void dgvReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            string id, msId="";
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                cmd = new SqlCommand(sql, conn);

                if (e.ColumnIndex == 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dgvReport.Rows[e.RowIndex].Cells["Remark"].Value.ToString() == "FOR LOADPLAN")
                        {
                            //schdate = "'" + dgvReport.Rows[e.RowIndex].Cells["SchDate"].Value.ToString() + "'";
                            //plant = "'" + dgvReport.Rows[e.RowIndex].Cells["Plant"].Value.ToString() + "'";
                            //product = "'" + dgvReport.Rows[e.RowIndex].Cells["Product"].Value.ToString() + "'";
                            //mat = "'" + dgvReport.Rows[e.RowIndex].Cells["Material"].Value.ToString() + "'";
                            //desc = "'" + dgvReport.Rows[e.RowIndex].Cells["MaterialDesc"].Value.ToString() + "'";
                            //line = "'" + dgvReport.Rows[e.RowIndex].Cells["ProdnLine"].Value.ToString() + "'";
                            //model = "'" + dgvReport.Rows[e.RowIndex].Cells["Model"].Value.ToString() + "'";
                            //pv = "'" + dgvReport.Rows[e.RowIndex].Cells["PV"].Value.ToString() + "'";
                            //qty = "'" + dgvReport.Rows[e.RowIndex].Cells["LotQty"].Value.ToString() + "'";
                            //shift = "'" + dgvReport.Rows[e.RowIndex].Cells["Shift"].Value.ToString() + "'";
                            //reqhour = "'" + dgvReport.Rows[e.RowIndex].Cells["ReqHours"].Value.ToString() + "'";
                            //reqdate = "'" + dgvReport.Rows[e.RowIndex].Cells["ReqDate"].Value.ToString() + "'";
                            id= dgvReport.Rows[e.RowIndex].Cells["RecordId"].Value.ToString();
                            msId = dgvReport.Rows[e.RowIndex].Cells["SPCId"].Value.ToString();
                            dgvReport.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value);

                            if (Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value))
                            {
                                i=1;
                            }else{
                                i=0;
                            }
                            
                            conn = db.GetConnString();
                            sql = "EXEC Insert_Into_TobeJEQ @shift=''" +
                                ",@line=''" +
                                ",@act='SELECTED',@dlpno='" + myDLPNo + "', @leader='',@selected='" + i.ToString() + "', @id='" + id + "', @lot='',@remark=''" +
                                ",@masterid='"+msId+"'";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            if (msId.Length > 0)
                            {
                                for (int j = 0; j < dgvReport.Rows.Count; j++)
                                {
                                    if (dgvReport.Rows[j].Cells["SPCId"].Value.ToString() == msId)
                                    {
                                        if (dgvReport.Rows[j].Index != e.RowIndex)
                                        {
                                            dgvReport.Rows[j].Cells[0].Value = i;

                                            sql =   "EXEC Insert_Into_TobeJEQ @shift=''" +
                                                    ",@line=''" +
                                                    ",@act='SELECTED',@dlpno='" + myDLPNo + "', @leader='',@selected='" + i.ToString() + "', @id='" + dgvReport.Rows[j].Cells["RecordId"].Value.ToString() + "', @lot='',@remark=''" +
                                                    ",@masterid='" + msId + "'";
                                            cmd.CommandText = sql;
                                            cmd.ExecuteNonQuery();

                                        }
                                    }
                                }
                            }
                            

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void dtpSchTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpSchTo.Value > DateTime.ParseExact(myPrdnEDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
                {
                    MessageBox.Show("You cannot select the next period!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dtpSchTo.Value = DateTime.ParseExact(DateTime.Today.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);                    
                }
                else if (Convert.ToInt32(dtpSchTo.Value.ToString("yyyyMMdd")) < Convert.ToInt32(dtpSchFrom.Value.ToString("yyyyMMdd")))
                {
                    MessageBox.Show("Schedule To must be more than Schedule From!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dtpSchTo.Value = dtpSchFrom.Value;
                }                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }            
        }

        private void btnJEQ_Click(object sender, EventArgs e)
        {
            if (btnJEQ.Text == "&Hide")
            {
                btnJEQ.Text = "&JEQ Outstanding";
                this.Width = 859;
            }
            else
            {
                btnJEQ.Text = "&Hide";
                DisplayJEQData();
                this.Width = 1292;
            }
        }

        void DisplayJEQData()
        {

            string plant, product, model, line,sql;
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                plant = "%";
                if (cbbPlant.SelectedIndex > 0)
                    plant = cbbPlant.SelectedItem.ToString();
                product = "%";
                if (cbbProduct.SelectedIndex > 0)
                    product = cbbProduct.SelectedItem.ToString();
                model = "%";
                if (cbbModel.SelectedIndex > 0)
                    model = cbbModel.SelectedItem.ToString();
                line = "%";
                if (cbbLine.SelectedIndex > 0)
                    line = cbbLine.SelectedItem.ToString();

                sql = "EXEC Get_JEQ_SChedule @plant='"+plant+"',@product='"+product+"', @line='"+line+"', @model='"+model+"'";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvJEQ.DataSource = dt;

                dgvJEQ.Columns["Plant"].Width = 40;
                dgvJEQ.Columns["Product"].Width = 40;
                dgvJEQ.Columns["Plant"].Visible = false;
                dgvJEQ.Columns["Product"].Visible = false;
                dgvJEQ.Columns["ProdnLine"].Width = 40;
                dgvJEQ.Columns["SchQty"].Width = 60;
                dgvJEQ.Columns["SchId"].Width = 80;
                lblJEQ.Text = "JEQ Outstanding: " + dgvJEQ.Rows.Count.ToString();
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        DataTable dtBOM = new DataTable();

        DataSet ds;
        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool selected = false;
            try
            {
                selected = false;
                while ((i < dgvReport.Rows.Count) && (!selected))
                {
                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells[0].Value))
                    {
                        selected = true;
                        
                    }
                    i++;
                }

                if (selected)
                {
                    string plant, product, sql;
                    plant = "%";
                    if (cbbPlant.SelectedIndex > 0)
                        plant = cbbPlant.SelectedItem.ToString();
                    product = "%";
                    if (cbbProduct.SelectedIndex > 0)
                        product = cbbProduct.SelectedItem.ToString();

                    SqlDataAdapter adapter;
                    SqlConnection conn;
                    conn = db.GetConnString();
                    sql = "EXEC spDLP_PartStatus  @Product='" + cbbProduct.SelectedItem.ToString() + "', @plant='" + cbbPlant.SelectedItem.ToString() + "',@Line='" + cbbLine.SelectedItem.ToString() + "',@PlanDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',@DataType='TOBEJEQ'";
                    adapter = new SqlDataAdapter(sql, conn);
                    ds = new DataSet();
                    adapter.Fill(ds);

                    //tabControl1.TabPages.Remove(tabPage1);
                    if (tabControl1.TabPages.IndexOf(tabPart) < 0)
                        tabControl1.TabPages.Add(tabPart);
                    tabControl1.SelectedTab = tabControl1.TabPages["tabPart"];
                    dgPart.DataSource = null;
                    dgPart.DataSource = ds.Tables[0];
                    dtBOM = null;
                    dtBOM = ds.Tables[1];



                }
                else
                {
                    MessageBox.Show("No record selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void InitiateFilters()
        {
            try
            {
                txtLeaderLoadPlanFrom.Text = dtpMCFrom.Value.ToString("yyyy-MM-dd");
                txtLeaderLoadPlanTo.Text = dtpMCTo.Value.ToString("yyyy-MM-dd");
                txtLeaderSchFrom.Text = dtpSchFrom.Value.ToString("yyyy-MM-dd");
                txtLeaderSchTo.Text = dtpSchTo.Value.ToString("yyyy-MM-dd");
                txtLeaderPlant.Text = cbbPlant.SelectedItem.ToString();
                txtLeaderProduct.Text = cbbProduct.SelectedItem.ToString();
                txtLeaderProdnLine.Text = cbbLine.SelectedItem.ToString();
                txtLeaderModel.Text = cbbModel.SelectedItem.ToString();
                txtLeaderCap.Text = cbbCap.SelectedItem.ToString();
                txtLeaderNWD.Text = txtNonWork.Text;
                txtLeaderSat.Text = txtSaturday.Text;
                txtLeaderWD.Text = txtWorkDays.Text;
                //tabControl1.TabPages.Remove(tabPage1);
                if (tabControl1.TabPages.IndexOf(tabPage3) < 0)
                    tabControl1.TabPages.Add(tabPage3);
                tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
                db.SetLeader(ref cbbLeader, txtLeaderPlant.Text, txtLeaderProduct.Text);
                db.SetPacker(ref cbbPacker, txtLeaderPlant.Text, txtLeaderProduct.Text);
                DisplayTobeJEQLeaders();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void InitiateFiltersLotNo()
        {
            try
            {
                txtLoadPlanFrom.Text = dtpMCFrom.Value.ToString("yyyy-MM-dd");
                txtLoadPlanTo.Text = dtpMCTo.Value.ToString("yyyy-MM-dd");
                txtSchFrom.Text = dtpSchFrom.Value.ToString("yyyy-MM-dd");
                txtSchTo.Text = dtpSchTo.Value.ToString("yyyy-MM-dd");
                txtPlant.Text = cbbPlant.SelectedItem.ToString();
                txtProduct.Text = cbbProduct.SelectedItem.ToString();
                txtProdnLine.Text = cbbLine.SelectedItem.ToString();
                txtModel.Text = cbbModel.SelectedItem.ToString();
                txtCap.Text = cbbCap.SelectedItem.ToString();
                txtJEQNONWD.Text = txtNonWork.Text;
                txtJEQSat.Text = txtSaturday.Text;
                txtJEQWD.Text = txtWorkDays.Text;

                //tabControl1.TabPages.Remove(tabPage3);
                if (tabControl1.TabPages.IndexOf(tabPage2) < 0)
                    tabControl1.TabPages.Add(tabPage2);

                tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
                
                DisplayTobeJEQ();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void InitiateFiltersPick()
        {
            try
            {
                txtPickLoadPlanFrom.Text = dtpMCFrom.Value.ToString("yyyy-MM-dd");
                txtPickLoadPlanTo.Text = dtpMCTo.Value.ToString("yyyy-MM-dd");
                txtPickSchFrom.Text = dtpSchFrom.Value.ToString("yyyy-MM-dd");
                txtPickSchTo.Text = dtpSchTo.Value.ToString("yyyy-MM-dd");
                txtPickPlant.Text = cbbPlant.SelectedItem.ToString();
                txtPickProduct.Text = cbbProduct.SelectedItem.ToString();
                txtPickProdnLine.Text = cbbLine.SelectedItem.ToString();
                txtPickModel.Text = cbbModel.SelectedItem.ToString();
                txtPickCap.Text = cbbCap.SelectedItem.ToString();
                txtPickWD.Text = txtNonWork.Text;
                txtPickSat.Text = txtSaturday.Text;
                txtPickNONWD.Text = txtWorkDays.Text;
                SetPickSchDate();                
                //tabControl1.TabPages.Remove(tabPage3);
                if (tabControl1.TabPages.IndexOf(tabPick) < 0)
                    tabControl1.TabPages.Add(tabPick);

                tabControl1.SelectedTab = tabControl1.TabPages["tabPick"];

                //DisplayTobeJEQPick();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SetPickSchDate()
        {
            string sql="";
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader reader;
            ArrayList res = new ArrayList();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT DISTINCT SUBSTRING(SchDate,3,6)  from TPCS_TOBEJEQ WHERE DlpNo='" + myDLPNo + "' AND Selected='1' ORDER BY SUBSTRING(SchDate,3,6) ";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(reader[0].ToString());
                }
                cbbPickSchDate.Items.Clear();
                cbbPickLine.Items.Clear();
                cbbPickSchDate.Items.AddRange(res.ToArray());
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SetPickLine()
        {
            string sql = "";
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader reader;
            ArrayList res = new ArrayList();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT Distinct ProdnLine from TPCS_TOBEJEQ WHERE "+
                    " DlpNo='" + myDLPNo + "' AND SUBSTRING(SchDate,3,6) = '"+cbbPickSchDate.SelectedItem.ToString()+"' "+
                    "  AND Selected='1' "+
                    " ORDER BY ProdnLine";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(reader[0].ToString());
                }
                cbbPickLine.Items.Clear();
                cbbPickLine.Items.AddRange(res.ToArray());
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnBackDLP_Click(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Add(tabPage3);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.SelectedTab = tabControl1.TabPages["tabPick"];
        }


        void DisplayTobeJEQ()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                //sql = "SELECT RecordId, ROW_NUMBER() OVER (Partition BY SchDate,ProdnLine order by SchDate, ProdnLine, RecordIdReqDate, Shift, Material, LotQty desc) as 'Pick Order', " +
                //"    SchDate, Plant, Product, Material, MaterialDesc, Shift,ProdnLine, LotNo, " +
                //    " LeaderId, PackerId, Model, LotQty, ReqHours, ReqDate, PV,MasterCode as 'SPC',MasterName as 'SPCName' " +
                //    " from " +
                //    "tpcs_tobejeq where DLPNo = '" + myDLPNo + "' and Selected='1' ";
                //After implementing Pick Sequence 15/6/16 
                sql = "SELECT RecordId, ROW_NUMBER() OVER (Partition BY SchDate,ProdnLine order by SchDate, ProdnLine, RecordId) as 'Pick Order', " +
                "    SchDate, Plant, Product, Material, MaterialDesc, Shift,ProdnLine, LotNo,JEQRemark as 'Remark', " +
                    " LeaderId, PackerId, Model, LotQty, ReqHours, ReqDate, PV,MasterCode as 'SPC',MasterName as 'SPCName',MasterId as 'SPCId' " +
                    " from " +
                    "tpcs_tobejeq where DLPNo = '" + myDLPNo + "' and Selected='1' ";
                //END of 15/6/16
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvJEQPreview.DataSource=null;
                dgvJEQPreview.DataSource=dt;

                dgvJEQPreview.Columns[0].Visible = false;
                dgvJEQPreview.Columns["Pick Order"].Width = 40;
                dgvJEQPreview.Columns["Plant"].Width = 40;
                dgvJEQPreview.Columns["Product"].Width = 40;
                dgvJEQPreview.Columns["Plant"].Visible = false;
                dgvJEQPreview.Columns["Product"].Visible = false;
                dgvJEQPreview.Columns["ProdnLine"].Width = 60;
                dgvJEQPreview.Columns["LotQty"].Width = 60;
                lblJEQRows.Text="Total Rows: "+dgvJEQPreview.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayTobeJEQPick()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT RecordId, ROW_NUMBER() OVER (Partition BY SchDate,ProdnLine order by SchDate, ProdnLine, RecordId,ReqDate, Shift, Material, LotQty, RecordId desc) as 'Pick Order', " +
                "    SchDate, Plant, Product, Material, MaterialDesc, Shift,ProdnLine, LotNo, " +
                    " LeaderId, PackerId, Model, LotQty, ReqHours, ReqDate, PV,MasterCode as 'SPC',MasterName as 'SPCName' " +
                    " from " +
                    " tpcs_tobejeq where DLPNo = '" + myDLPNo + "' and Selected='1' "+
                    " AND SchDate like '%"+cbbPickSchDate.SelectedItem.ToString()+"' AND ProdnLine='"+cbbPickLine.SelectedItem.ToString()+"'";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvJEQPick.DataSource = null;
                dgvJEQPick.DataSource = dt;

                dgvJEQPick.Columns[0].Visible = false;
                dgvJEQPick.Columns["Pick Order"].Width = 40;
                dgvJEQPick.Columns["Plant"].Width = 40;
                dgvJEQPick.Columns["Product"].Width = 40;
                dgvJEQPick.Columns["Plant"].Visible = false;
                dgvJEQPick.Columns["Product"].Visible = false;
                dgvJEQPick.Columns["ProdnLine"].Width = 60;
                dgvJEQPick.Columns["LotQty"].Width = 60;
                lblJEQRows.Text = "Total Rows: " + dgvJEQPick.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayTobeJEQLeaders()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                sql = "SELECT SchDate, Plant, Product, Shift,ProdnLine, MAX(LeaderId) as 'LeaderId', MAX(PackerId) as 'PackerId' from " +
                    " tpcs_tobejeq where DLPNo='" + myDLPNo + "' AND Selected='1' " +
                    " GROUP BY SchDate, Plant, Product, Shift,ProdnLine ";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvJEQLeaders.DataSource = dt;

                dgvJEQLeaders.Columns["Plant"].Visible = false;
                dgvJEQLeaders.Columns["Product"].Visible = false;

                lblLeaderRows.Text = "Total Rows: " + dgvJEQLeaders.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtLotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            //bool complete = false;
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (txtLotNo.Text.Length > 0)
            //    {
            //        if (txtLotNo.Text.Length != 3)
            //        {
            //            MessageBox.Show("Lot No Indicator must be 3 characters!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtLotNo.Focus();
            //            return;
            //        }
            //    }

            //    dgvJEQPreview.SelectedRows[0].Cells["LotNo"].Value = txtLotNo.Text;
            //    SaveLotNo();

            //    if (dgvJEQPreview.SelectedRows[0].Index < dgvJEQPreview.RowCount-1 )
            //    {
            //        dgvJEQPreview.Rows[dgvJEQPreview.SelectedRows[0].Index + 1].Selected = true;
            //    }
            //    else
            //    {
                    
            //        complete = true;

            //        for (int i = 0; i < dgvJEQPreview.Rows.Count; i++)
            //        {
            //            if (dgvJEQPreview.Rows[i].Cells["LotNo"].Value.ToString() == "")
            //            {
            //                complete = false;
            //                break;
            //            }
            //        }

            //        if (!complete)
            //        {
            //            dgvJEQPreview.Rows[0].Selected = true;                        
            //        }

                    
            //    }

                
            //    txtLotNo.Focus();
            //    txtLotNo.SelectAll();
            //}
        }

        private void dgvJEQPreview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtLotNo.Text = dgvJEQPreview.SelectedRows[0].Cells["LotNo"].Value.ToString();
                txtLotNo.Focus();
                txtLotNo.SelectAll();
            }
        }

        private void btnJEQClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        void DeleteDailyPlan()
        {
            string sql;
            SqlCommand cmd;
            SqlConnection conn;
            SqlTransaction trans=null;
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "DELETE FROM TPCS_TOBEJEQ WHERE DlpNo='"+myDLPNo+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM TPCS_DLP where DlpNo='"+myDLPNo+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
            }
        }


        void SaveLotNo()
        {            
            string schdate, plant, product, mat, desc, line, model, pv, qty, reqhour, reqdate, shift,remark,msId;
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;
            try
            {
                conn = db.GetConnString();
                cmd = new SqlCommand(sql, conn);

                schdate = "'" + dgvJEQPreview.SelectedRows[0].Cells["SchDate"].Value.ToString() + "'";
                plant = "'" + dgvJEQPreview.SelectedRows[0].Cells["Plant"].Value.ToString() + "'";
                product = "'" + dgvJEQPreview.SelectedRows[0].Cells["Product"].Value.ToString() + "'";
                mat = "'" + dgvJEQPreview.SelectedRows[0].Cells["Material"].Value.ToString() + "'";
                desc = "'" + dgvJEQPreview.SelectedRows[0].Cells["MaterialDesc"].Value.ToString() + "'";
                line = "'" + dgvJEQPreview.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "'";
                model = "'" + dgvJEQPreview.SelectedRows[0].Cells["Model"].Value.ToString() + "'";
                pv = "'" + dgvJEQPreview.SelectedRows[0].Cells["PV"].Value.ToString() + "'";
                qty = "'" + dgvJEQPreview.SelectedRows[0].Cells["LotQty"].Value.ToString() + "'";
                shift = "'" + dgvJEQPreview.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                reqhour = "'" + dgvJEQPreview.SelectedRows[0].Cells["ReqHours"].Value.ToString() + "'";
                reqdate = "'" + dgvJEQPreview.SelectedRows[0].Cells["ReqDate"].Value.ToString() + "'";
                remark = "'" + dgvJEQPreview.SelectedRows[0].Cells["Remark"].Value.ToString() + "'";
                msId = "'"+dgvJEQPreview.SelectedRows[0].Cells["SPCId"].Value.ToString()+"'";
                
                sql = "EXEC Insert_Into_TobeJEQ @shift=" + shift +
                    ",@line='',@act='LOT',@dlpno='"+myDLPNo+"',"+
                    " @id='" + dgvJEQPreview.SelectedRows[0].Cells["RecordId"].Value.ToString() + 
                    "', @lot='" + txtLotNo.Text + "',@leader='', @selected='0', @remark="+remark+",@masterid="+msId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvJEQPreview_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvJEQPreview.SelectedRows.Count > 0)
            {
                txtLotNo.Text = dgvJEQPreview.SelectedRows[0].Cells["LotNo"].Value.ToString();
                txtMat.Text = dgvJEQPreview.SelectedRows[0].Cells["Material"].Value.ToString();
                txtDesc.Text = dgvJEQPreview.SelectedRows[0].Cells["MaterialDesc"].Value.ToString();
                txtLotNo.Focus();
                txtLotNo.SelectAll();
            }
        }

        private void btnLeaderClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLeaders_Click(object sender, EventArgs e)
        {
            string plant, product, line, schdate,shift;
            bool complete = false;
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;
            try
            {

                if (cbbLeader.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the leader!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbbPacker.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Packer!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                conn = db.GetConnString();
                plant = "'" + dgvJEQLeaders.SelectedRows[0].Cells["Plant"].Value.ToString() + "'";
                shift = "'" + dgvJEQLeaders.SelectedRows[0].Cells["Shift"].Value.ToString() + "'";
                product = "'" + dgvJEQLeaders.SelectedRows[0].Cells["Product"].Value.ToString() + "'";
                line = "'" + dgvJEQLeaders.SelectedRows[0].Cells["ProdnLine"].Value.ToString() + "'";
                schdate = "'" + dgvJEQLeaders.SelectedRows[0].Cells["SchDate"].Value.ToString() + "'";

                sql = "UPDATE TPCS_TOBEJEQ set LeaderId = '" + cbbLeader.SelectedItem.ToString() + "',PackerID = '" + cbbPacker.SelectedItem.ToString() + "' WHERE " +
                    " Plant = "+plant +" and Product = "+product+" and Shift = "+shift+" and ProdnLine = "+line+" and SchDate="+schdate;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                dgvJEQLeaders.SelectedRows[0].Cells["LeaderID"].Value = cbbLeader.SelectedItem.ToString();
                dgvJEQLeaders.SelectedRows[0].Cells["PackerID"].Value = cbbPacker.SelectedItem.ToString();
                if (dgvJEQLeaders.SelectedRows[0].Index < dgvJEQLeaders.Rows.Count-1)
                {
                    dgvJEQLeaders.Rows[dgvJEQLeaders.SelectedRows[0].Index + 1].Selected = true;
                }
                else
                {
                    complete = true;
                    for (int i = 0; i < dgvJEQLeaders.Rows.Count; i++)
                    {
                        if (dgvJEQLeaders.Rows[i].Cells["LeaderID"].Value.ToString() == "")
                        {
                            complete = false;
                            break;
                        }
                    }


                    if (!complete)
                    {
                        dgvJEQLeaders.Rows[0].Selected = true;
                        
                    }
                   
                    
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvJEQLeaders_SelectionChanged(object sender, EventArgs e)
        {
            string leader,packer;
            try
            {
                if (dgvJEQLeaders.SelectedRows.Count > 0)
                {
                    leader = dgvJEQLeaders.SelectedRows[0].Cells["LeaderID"].Value.ToString();
                    if (cbbLeader.FindStringExact(leader) >= 0)
                    {
                        cbbLeader.SelectedItem = leader;
                    }
                    else
                    {
                        cbbLeader.Text = "";
                        cbbLeader.SelectedIndex = -1;
                    }


                    packer = dgvJEQLeaders.SelectedRows[0].Cells["PackerID"].Value.ToString();
                    if (cbbPacker.FindStringExact(packer) >= 0)
                    {
                        cbbPacker.SelectedItem = packer;
                    }
                    else
                    {
                        cbbPacker.Text = "";
                        cbbPacker.SelectedIndex = -1;
                    }

                }


            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
       
        }

        private void btnNextLotNo_Click(object sender, EventArgs e)
        {
            bool proceed = true;
            try
            {

                for (int i = 0; i < dgvJEQLeaders.Rows.Count; i++)
                {
                    if (dgvJEQLeaders.Rows[i].Cells["LeaderID"].Value.ToString() == "")
                    {
                        proceed = false;
                        break;
                    }
                }

                if (proceed)
                {
                    InitiateFiltersPick();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnBacktoDLP_Click(object sender, EventArgs e)
        {
            RemoveAllTabExcaptMain();
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
                InitiateFiltersLotNo();
            else if (tabControl1.SelectedTab == tabPage3)
                InitiateFilters();
            else if (tabControl1.SelectedTab == tabPage1)
            {
                tabControl1.TabPages.Remove(tabPart);
                tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
                DisplayData();
            }
                
            

        }

        void RemoveAllTabExcaptMain()
        {
            if (tabControl1.TabPages.IndexOf(tabPart) >=0)
            {
                tabControl1.TabPages.Remove(tabPart);
            }
            if (tabControl1.TabPages.IndexOf(tabPart) >= 0)
            {
                tabControl1.TabPages.Remove(tabPart);
            }
            if (tabControl1.TabPages.IndexOf(tabPage3) >= 0)
            {
                tabControl1.TabPages.Remove(tabPage3);
            }
            if (tabControl1.TabPages.IndexOf(tabPick) >= 0)
            {
                tabControl1.TabPages.Remove(tabPick);
            }
            if (tabControl1.TabPages.IndexOf(tabPage2) >= 0)
            {
                tabControl1.TabPages.Remove(tabPage2);
            }
        }

        private void btnJEQNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validating_JEQData())
                {
                    InsertIntoJEQ();                    
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }


        int GetLastNumbers(string period)
        {
            int result = 0;
            SqlConnection conn;
            SqlCommand cmd;
            string sql="";
            try
            {
                conn = db.GetConnString();
                sql="Select top 1 SchId from TSCHEDULE WHERE LEFT(SchID,6) = '" + period + "' ORDER BY SchID desc";
                cmd = new SqlCommand(sql,conn);
                
                if (cmd.ExecuteScalar() == null)
                    result = 0;
                else
                    result= Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(6));
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        void InsertIntoJEQ()
        {
            string sql, period, product, shift, line, leader, plant, fgcode, qty, lotno, pickorder, remark,packer,pv,reqdate,spccode,spcname,spcid;
            SqlTransaction trans = null;
            SqlCommand cmd;
            string values = "";
            string lastperiod = "";
            int rn = 0;
            SqlConnection conn;
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "";
                plant = txtPlant.Text;
                product = txtProduct.Text;                
                cmd = new SqlCommand(sql, conn);
                lastperiod = DateTime.Today.ToString("ddMMyy");
                remark = "";
                values = "";
                rn = GetLastNumbers(lastperiod);
                cmd.Transaction = trans;


                for (int i = 0; i < dgvJEQPreview.Rows.Count; i++)
                {
                    period = dgvJEQPreview.Rows[i].Cells["SchDate"].Value.ToString().Substring(2, 6);
                    if (period != lastperiod)
                    {
                        rn = GetLastNumbers(period);
                    }
                    lastperiod = period;
                    rn++;
                    
                    period = period + rn.ToString("000");
                    shift = dgvJEQPreview.Rows[i].Cells["Shift"].Value.ToString();
                    line = dgvJEQPreview.Rows[i].Cells["ProdnLine"].Value.ToString();
                    leader = dgvJEQPreview.Rows[i].Cells["LeaderID"].Value.ToString();
                    fgcode = dgvJEQPreview.Rows[i].Cells["Material"].Value.ToString();
                    qty = dgvJEQPreview.Rows[i].Cells["LotQty"].Value.ToString();
                    lotno = rn.ToString("000")+dgvJEQPreview.Rows[i].Cells["LotNo"].Value.ToString();
                    packer = dgvJEQPreview.Rows[i].Cells["PackerId"].Value.ToString();
                    pv = dgvJEQPreview.Rows[i].Cells["PV"].Value.ToString();
                    reqdate = dgvJEQPreview.Rows[i].Cells["ReqDate"].Value.ToString();
                    spccode = dgvJEQPreview.Rows[i].Cells["SPC"].Value.ToString();
                    spcname= dgvJEQPreview.Rows[i].Cells["SPCName"].Value.ToString();
                    remark = dgvJEQPreview.Rows[i].Cells["Remark"].Value.ToString();
                    spcid = dgvJEQPreview.Rows[i].Cells["SPCId"].Value.ToString();
                    pickorder=(Convert.ToInt32(GetPickOrder(period.Substring(0, 6), line) + Convert.ToInt32(dgvJEQPreview.Rows[i].Cells["Pick Order"].Value.ToString())).ToString());

                    values = values + "(" +
                    "'" + period + "', '" + product + "', '" + shift + "', '" + line + "', '" + leader + "', '" + plant + "', '" +
                    fgcode + "', '" + qty + "', '" + lotno + "','OPEN', '" + myDLPNo + "', '" +
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                    "','0','" + pickorder + "','" + remark + "','"+packer+"','"+pv+"','"+reqdate+"','"+spccode+"','"+spcname+"','"+spcid+"'),";

                }
                values = values.Substring(0, values.Length - 1);
                //sql="INSERT INTO TSCHEDULE (SchId, Product, Shift, ProdnLine, Leader, Plant, FGCode, SchQty, LotNo, "+
                //    " Status, UserCreated, DateCreated, Selected, PickOrder, Remark, PackerId,PV,ReqDate,MasterCode,MasterName,MasterId) VALUES " + 
                //    values;

                sql = @"Declare @MaxID Bigint=''
                        select  @MaxID=convert(varchar,convert(bigint,isnull(Max(MasterID),LEFT(convert(varchar,GETDATE(),12),4)+'0000'))) from Tschedule
                        where LEFT(MasterID,4)=LEFT(convert(varchar,GETDATE(),12),4)

                        select top 0 SchId, Product, Shift, ProdnLine, Leader, Plant, FGCode, SchQty, LotNo,  Status, UserCreated, DateCreated, Selected, PickOrder, Remark, PackerId,PV,ReqDate,MasterCode,MasterName,MasterId 
                        into #Data1
                        from TSCHEDULE

                        INSERT INTO #Data1 
                        (SchId, Product, Shift, ProdnLine, Leader, Plant, FGCode, SchQty, LotNo,  Status, UserCreated, DateCreated, Selected, PickOrder, Remark, PackerId,PV,ReqDate,MasterCode,MasterName,MasterId) 
                        VALUES " + values + @"

                        UPDATE #Data1 set MasterId=case when MasterId='' then null else MasterId end
									,MasterCode=case when MasterCode='' then null else MasterCode end
									,MasterName=case when MasterName='' then null else MasterName end

                        select MasterID,@MaxID + ROW_NUMBER() OVER (order by MasterID) as Num
                        into #Data2
                        from (
	                        select distinct MasterID from #Data1
	                        where isnull(MasterID,'')<>''
                        ) a

                        UPDATE a set a.MasterId=b.Num FROM #Data1 a
                        inner join #Data2 b on a.MasterID=b.MasterID

                        INSERT INTO TSCHEDULE
                        (SchId, Product, Shift, ProdnLine, Leader, Plant, FGCode, SchQty, LotNo,  Status, UserCreated, DateCreated, Selected, PickOrder, Remark, PackerId,PV,ReqDate,MasterCode,MasterName,MasterId)
                        select SchId, Product, Shift, ProdnLine, Leader, Plant, FGCode, SchQty, LotNo,  Status, UserCreated, DateCreated, Selected, PickOrder, Remark, PackerId,PV,ReqDate,MasterCode,MasterName,MasterId from #Data1

                        drop table #Data1,#Data2";
                cmd.CommandText=sql;
                cmd.ExecuteNonQuery();

               //' sql="UPDATE TSCHEDULE SET FGName = t2.SPARABB from tschedule t1 inner join tprd t2 on t1.FGCode = t2.Procod2 "+
               //      " WHERE t1.FGName is null ";
                sql = "UPDATE TSCHEDULE SET FGName = t2.MaterialDesc " +
                " FROM TSCHEDULE t1 INNER JOIN tpcs_mat_model t2 ON t1.FGCode = t2.Material and t1.Plant = t2.Plant " +
                " WHERE ISNULL(t1.FGName,'') =''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                
                sql = "UPDATE TPCS_TOBEJEQ SET Status='IN JEQ' where DLPNo='" + myDLPNo + "' AND SELECTED='1'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_TOBEJEQ SET LotNo='', LeaderId='', PackerId='' where DLPNo='" + myDLPNo + "' AND SELECTED='0' and Remark='FOR LOADPLAN'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TSCHEDULE SET Status = 'DELIVERED' " +
               " WHERE ISNULL(PV,'1000') not in ('1000','2000') and status = 'OPEN'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = " update t1 set t1.OutputQty=t1.OutputQty+t2.Qty " +
                    " from tpcs_lot_ind_det t1 inner join " +
                    " (select Plant, Product, Model, Material, LotNO, SUM(LotQty) as 'Qty' from tpcs_tobejeq" +
                    " where DLPNo='"+myDLPNo+"' and Selected='1' and Status='IN JEQ' and ISNULL(LotNo,'') <> ''" +
                    " group by Plant, Product, Model, Material,LotNo) as t2" +
                    " on t1.Plant=t2.plant and t1.Product=t2.Product and t1.Model=t2.Model and t1.Material=t2.Material" +
                    " and t1.IndNo=t2.LotNo";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE t1 SET t1.Status='CLOSED' from tpcs_lot_ind_det t1 inner join tpcs_lot_ind t2 " +
                    " on t1.Period=t2.Period and t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Model=t2.Model " +
                    " where ISNULL(t2.ReqQty,0) <= ISNULL(t1.OutputQty,0) and ChangeType<>'IMPROVEMENT'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE t1 set t1.Status='CLOSED' FROM "+
                    " tpcs_lot_ind t1 inner join " +
                    " (SELECT Period, Plant, Product, Model, Indno, COUNT(Material) as 'C' "+
                    " from TPCS_LOT_IND_DET where Status='OPEN' " +
                    " Group by Period, Plant, Product, Model, Indno "+
                    " HAVING COUNT(Material) = 0 "+
                    " ) AS t2 ON "+
                    " t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Period=t2.Period and t1.IndNo=t2.IndNo and t1.Model=t2.Model ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();

                MessageBox.Show("The Daily load plan has been transferred to JEQ Schedule!","",MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAll();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                trans.Rollback();
            }

        }

        void ResetAll()
        {
            try
            {
                myDLPNo = "";
                CanClose = true;
                tabControl1.TabPages.Remove(tabControl1.TabPages["tabPage2"]);
                tabControl1.TabPages.Remove(tabControl1.TabPages["tabPage3"]);
                tabControl1.TabPages.Remove(tabControl1.TabPages["tabPick"]);
                dgvJEQLeaders.DataSource = null;
                dgvJEQPreview.DataSource = null;
                dgvReport.DataSource = null;
                tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        int GetPickOrder(string period, string line)
        {
            int result = 0;
            SqlCommand cmd;
            SqlConnection conn;
            string sql = "";
            try
            {
                conn = db.GetConnString();
                sql = "Select COUNT(SchId) from TSCHEDULE WHERE LEFT(SchID,6) = '" + period +
                      "' and ProdnLine = '" + line + "'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                {
                    result = 0;
                }
                else
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        bool Validating_JEQData()
        {
            bool ok = false;
            string sql = "";

            SqlCommand cmd;
            SqlConnection conn = null;

            try
            {
                conn = db.GetConnString();
                //DISBLED BECAUSE LOTNO using SCHID
                //25/4/19
                //for (int i = 0; i < dgvJEQPreview.Rows.Count; i++)
                //{
                //    if (dgvJEQPreview.Rows[i].Cells["LotNo"].Value.ToString() == "")
                //    {
                //        MessageBox.Show("You have to fill all Lot No before saving to JEQ","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //        ok = false;
                //        return ok;                        
                //    }
                //}
                //END 25/4/19
                sql = "SELECT (Material) from TPCS_TOBEJEQ t1 " +
                    " WHERE DLPNo='"+myDLPNo+"' and Selected='1' AND Material not in (SELECT FGCode from TBOMLIST)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Material not in the Bom List!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Materials not in the BOM List. View Records.";
                    errorsql = "SELECT DISTINCT Plant, product, Material, Prodnline from TPCS_TOBEJEQ t1 " +
                    " WHERE DLPNo='" + myDLPNo + "' and Selected='1' AND Material not in (SELECT FGCode from TBOMLIST)";
                    errortitle = "Material not in the Bom List";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    //ok = true;
                    return ok;
                }

                /* Disable because lotno is from JEQ Schd ID
                 * 25/4/16
                sql = "SELECT SchDate, Material,Plant,Product, LotNo from TPCS_TOBEJEQ t1 " +
                    " WHERE DLPNo='" + myDLPNo + "' and Selected='1' "+
                    " GROUP BY SchDate, Material, Plant, Product, LotNo "+
                    " HAVING COUNT(LotNo) > 1 ";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Lot No!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Lot No is duplicated!. View Records.";
                    errorsql = sql;
                    errortitle = "Duplicated Lot No";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                //" WHERE exists (Select SchID,Product, Plant, FGCode, LotNo FROM tschedule as B where LEFT(B.SchId,4) = LEFT(A.SchId,4) and " +
                //" B.Product = A.Product and B.Plant = A.Plant and B.FGCode = A.FGCode and " +
                //" B.LotNo = A.LotNo and Status <> 'CLOSED') "

                sql = "SELECT SchDate, Plant, Product, Material, LotNo from TPCS_TOBEJEQ t1  " +
                    " WHERE DLPNo='"+myDLPNo+"' and Selected='1' "+
                    " AND EXISTS (SELECT LEFT(SchID,4) as 'SchDate',Product, Plant, FGCode, LotNo from tschedule t2 WHERE  SUBSTRING(t1.SchDate,3,4) = LEFT(t2.SchId,4) AND "+
                    " t1.Product=t2.Product and t1.Plant=t2.Plant and Material=FGCode and t1.LotNo=t2.LotNo and t2.Status <> 'CLOSED')";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid LotNo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid LotNo! View Error.";
                    errorsql = "SELECT SchDate, Plant, Product, Material, ProdnLine, LotNo from tpcs_tobejeq t1 " +
                   " WHERE DLPNo='" + myDLPNo + "' and Selected='1' " +
                    " AND EXISTS (SELECT  SchID,Product, Plant, FGCode, LotNo from tschedule t2 WHERE  SUBSTRING(t1.SchDate,3,4) = LEFT(t2.SchId,4) AND " +
                    " t1.Product=t2.Product and t1.Plant=t2.Plant and Material=FGCode and t1.LotNo=t2.LotNo and t2.Status <> 'CLOSED')";
                    errortitle = "Invalid Lot No!";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }
                *END 25/4/16
                 */
                ok = true;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
        }

        void UnlockProduct()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE FROM TPCS_LOCKPRODUCT WHERE "+
                    " Plant='"+cbbPlant.SelectedItem.ToString()+"' AND "+
                    " Product='" + cbbProduct.SelectedItem.ToString() + "' AND " +
                    " LockedMac='" +System.Environment.MachineName + "'"+
                    "";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void ClearTempDate()
        {
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;

            try
            {
                conn = db.GetConnString();
                sql = "DELETE FROM TPCS_TEMPDATE_DLP WHERE " +
                    " MacName='" + System.Environment.MachineName + "'";                 
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void FDailyPlan_FormClosing(object sender, FormClosingEventArgs e)
        {      
            if (!CanClose)
            {
                if (!AutoClose)
                {
                    if (MessageBox.Show("You're going to close the Daily Load Plan which hasn't been saved in JEQ. Do you really want to close it?",
                "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        DeleteDailyPlan();
                        UnlockProduct();
                        ClearTempDate();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    UnlockProduct();
                    ClearTempDate();
                }
            }
            else
            {
                UnlockProduct();
                ClearTempDate();
            }
        }

        private void btnLotNo_Click(object sender, EventArgs e)
        {
            string plant, product, fgcode, fgname;
            try
            {
                if (dgvJEQPreview.SelectedRows.Count > 0)
                {

                    plant = dgvJEQPreview.SelectedRows[0].Cells["Plant"].Value.ToString();
                    product = dgvJEQPreview.SelectedRows[0].Cells["Product"].Value.ToString();
                    fgcode = dgvJEQPreview.SelectedRows[0].Cells["Material"].Value.ToString();
                    fgname = dgvJEQPreview.SelectedRows[0].Cells["MaterialDesc"].Value.ToString();

                    FLotNo f;
                    f = new FLotNo(plant, product, fgcode, fgname);
                    f.ShowDialog();
                    f.Dispose();

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AutoClose = true;
            this.Close();            
        }

        private void btnCapvsSchd_Click(object sender, EventArgs e)
        {
            try
            {
                FMasCapSchLine f;
                f = new FMasCapSchLine(true, dtpMCFrom.Value.ToString("MM/dd/yyyy"), dtpMCTo.Value.ToString("MM/dd/yyyy"),
                    dtpSchFrom.Value.ToString("MM/dd/yyyy"), dtpSchTo.Value.ToString("MM/dd/yyyy"), cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString(),
                    cbbLine.SelectedItem.ToString(), cbbCap.SelectedItem.ToString(), txtWorkDays.Text, txtSaturday.Text, txtNonWork.Text);
                f.ShowDialog();
                f.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private void btnNW_Click(object sender, EventArgs e)
        {
            //string periods = "";
            //DateTime dto, dfrom;
            string sql = "";
            SqlCommand cmd;
            SqlTransaction trans=null;
            SqlConnection conn;
            SqlDataReader reader;
            try
            {
                FNonWorkDay f;
                /* Disbaled 18/5/16
                 * 
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                dto = dtpMCTo.Value.Date;
                dfrom = dtpMCFrom.Value.Date;
                periods = "";
                while (dto >= dfrom)
                {
                    periods = periods + "('" + dfrom.ToString("yyyyMMdd") + "','" + mac + "'),";
                    dfrom = dfrom.AddDays(1);
                }

                //txtNonWork.Text = "0";
                //txtWorkDays.Text = "0";
                //txtSaturday.Text = "0";

                if (periods.Length > 0)
                {
                    periods = periods.Substring(0, periods.Length - 1);
                }

                sql = "DELETE FROM TPCS_TEMPDATE WHERE MacName='" + mac + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO TPCS_TEMPDATE (TempDate, MacName) VALUES " + periods;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //sql = " SET DATEFIRST 1 " +
                //    " UPDATE TPCS_TEMPDATE SET IsHoliday='1' " +
                //    " where MacName='" + System.Environment.MachineName + "' and Datepart(dw,TempDate)=7 ";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                sql = " UPDATE TPCS_TEMPDATE SET IsHoliday='1' from " +
                    " tpcs_tempdate t1 inner join tpcs_nonworkday t2 on substring(t1.tempdate,3,6)=t2.pcsdate " +
                    " where t1.MacName='" + System.Environment.MachineName + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = " UPDATE TPCS_TEMPDATE SET IsHoliday='1' " +
                    " where DATENAME(dw, CAST(TempDate as Date)) ='Sunday' AND " +
                    " MacName='" + System.Environment.MachineName + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                END 18/5/16 */
                f = new FNonWorkDay(System.Environment.MachineName);
                f.ShowDialog();
                f.Dispose();

                sql = "EXEC DaySummary @mac='"+mac+"'";
                conn = db.GetConnString();
                cmd = new SqlCommand(sql, conn);
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
            catch (Exception ex) 
            {
                trans.Rollback();
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((dgvReport.Rows[e.RowIndex].Cells["ReqDate"].Value.ToString() == "OVRREQ") || (dgvReport.Rows[e.RowIndex].Cells["ReqDate"].Value.ToString() == "NOREQT"))
                    dgvReport.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnNextLot_Click(object sender, EventArgs e)
        {
            bool proceed = true;
            try
            {

                //for (int i = 0; i < dgvJEQLeaders.Rows.Count; i++)
                //{
                //    if (dgvJEQLeaders.Rows[i].Cells["LeaderID"].Value.ToString() == "")
                //    {
                //        proceed = false;
                //        break;
                //    }
                //}

                if (proceed)
                {
                    InitiateFiltersLotNo();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnBackLeaders_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPick);
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }

        private void cbbPickSchDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPickSchDate.SelectedIndex >= 0)
            {
                SetPickLine();
            }
        }

        private void cbbPickLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPickLine.SelectedIndex >= 0)
            {
                DisplayTobeJEQPick();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            
            string curRec = "";
            string SwapRec = "";
            int rIndex = 0;
            try
            {                

                if (dgvJEQPick.SelectedRows.Count > 0)
                {
                    rIndex=dgvJEQPick.SelectedRows[0].Index ;
                    if (rIndex > 0)
                    {
                        curRec = dgvJEQPick.SelectedRows[0].Cells["RecordId"].Value.ToString();
                        SwapRec = dgvJEQPick.Rows[rIndex-1].Cells["RecordId"].Value.ToString();
                        SwapRecord(curRec, SwapRec);
                        SelectRows((Convert.ToInt32(curRec)-1).ToString());
                    }

                }                
            }
            catch (Exception ex)
            {                
                db.SaveError(ex.ToString());
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string curRec = "";
            string SwapRec = "";
            int rIndex = 0;
            try
            {

                if (dgvJEQPick.SelectedRows.Count > 0)
                {
                    rIndex = dgvJEQPick.SelectedRows[0].Index;
                    if (rIndex < (dgvJEQPick.Rows.Count-1))
                    {
                        curRec = dgvJEQPick.SelectedRows[0].Cells["RecordId"].Value.ToString();
                        SwapRec = dgvJEQPick.Rows[rIndex + 1].Cells["RecordId"].Value.ToString();
                        SwapRecord(curRec, SwapRec);
                        SelectRows((Convert.ToInt32(curRec) + 1).ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SwapRecord(string cur, string swap)
        {
            string sql = "";
            SqlConnection conn;
            SqlCommand cmd;
            SqlTransaction trans = null;
            
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "UPDATE TPCS_TOBEJEQ SET RecordId='0' WHERE RecordId='" + cur + "' AND DlpNo='" + myDLPNo + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_TOBEJEQ SET RecordId='" + cur + "' WHERE RecordId='" + swap + "' AND DlpNo='" + myDLPNo + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_TOBEJEQ SET RecordId='" + swap + "' WHERE RecordId='0' AND DlpNo='" + myDLPNo + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                DisplayTobeJEQPick();
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                db.SaveError(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveAllTabExcaptMain();
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        }

        private void dgPart_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgPart.SelectedRows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dgPart.CurrentRow.Cells[0].Value.ToString()))
                {
                    if (dtBOM != null)
                    {
                        DataView dataView = dtBOM.DefaultView;
                        string filter = "";
                        filter = "RecordId = '" + dgPart.CurrentRow.Cells[0].Value.ToString() + "'";
                        dataView.RowFilter = filter;
                        dgBom.DataSource = dataView;
                    }
                }
            }
            


        }

        private void dgBom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["MB03Bal"].Value.ToString()) - (-Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["PSTDiffQty"].Value.ToString())) < 0)
                    dgBom.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                else if (Convert.ToDouble(dgBom.Rows[e.RowIndex].Cells["PSTDiffQty"].Value.ToString()) < 0)
                    dgBom.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
               
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgPart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgPart.Rows[e.RowIndex].Cells["Remark"].Value.ToString()== "Critical Part")
                    dgPart.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.IndexOf(tabPage3) < 0)
                tabControl1.TabPages.Add(tabPage3);
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Export to Excel
            saveFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ds.Tables[0].TableName = "Data";
            ds.Tables[1].TableName = "BOM";

            saveFileDialog2.Filter = "Excel 97-2003 Workbook | *.xls";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog2.FileName != "")
                {
                    if(saveFileDialog2.FileName.Contains(".xls"))
                    {
                        cm.DataSetToExcel(ds, saveFileDialog2.FileName);
                    }
                }
            }
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShow.Checked == true)
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void cbDetail_CheckedChanged(object sender, EventArgs e)
        {

                dgBom.Columns["SBA1"].Visible = cbDetail.Checked;
                dgBom.Columns["AssyStock"].Visible = cbDetail.Checked;
                dgBom.Columns["PSTStock"].Visible = cbDetail.Checked;
                dgBom.Columns["PSTBalOrder"].Visible = cbDetail.Checked;
                dgBom.Columns["MB03Diff"].Visible = cbDetail.Checked;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                grdSummary.DataSource = ds.Tables[2];
                grdSummary.Visible = true;
            }
            else
            {
                grdSummary.Visible = false;
            }
        }

        void SelectRows(string cur)
        {
            try
            {
                for (int i = 0; i < dgvJEQPick.Rows.Count; i++)
                {
                    if (dgvJEQPick.Rows[i].Cells["RecordId"].Value.ToString() == cur)
                    {
                        dgvJEQPick.Rows[i].Selected = true;
                        dgvJEQPick.FirstDisplayedScrollingRowIndex = i;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool IsValidInput()
        {
            bool ok = true;

            try
            {
                if (txtLotNo.Text.Length > 0)
                {
                    if (txtLotNo.Text.Length != 3)
                    {
                        MessageBox.Show("Lot No Indicator must be 3 characters!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ok = false;
                        txtLotNo.Focus();
                        return ok;
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            return ok;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool complete = false;

            if (IsValidInput())
            {

                dgvJEQPreview.SelectedRows[0].Cells["LotNo"].Value = txtLotNo.Text;
                dgvJEQPreview.SelectedRows[0].Cells["Remark"].Value = txtRemark.Text;
                SaveLotNo();

                if (dgvJEQPreview.SelectedRows[0].Index < dgvJEQPreview.RowCount - 1)
                {
                    dgvJEQPreview.Rows[dgvJEQPreview.SelectedRows[0].Index + 1].Selected = true;
                }
                else
                {

                    complete = true;

                    for (int i = 0; i < dgvJEQPreview.Rows.Count; i++)
                    {
                        if (dgvJEQPreview.Rows[i].Cells["LotNo"].Value.ToString() == "")
                        {
                            complete = false;
                            break;
                        }
                    }

                    if (!complete)
                    {
                        dgvJEQPreview.Rows[0].Selected = true;
                    }


                }

                txtLotNo.Focus();
                txtLotNo.SelectAll();
            }                        
            
        }

    }
}
