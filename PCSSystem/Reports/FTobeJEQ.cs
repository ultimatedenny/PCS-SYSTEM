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
    public partial class FTobeJEQ : Form
    {
        database db = new database();
        Common cm = new Common();
        string mac = System.Environment.MachineName.ToUpper();
        bool detailmode;
        string myDlpNo = "";
        string myPlant, myProduct, myLine, myModel, myCap, myWd, myNwd, mySat, myschfrom, myschto, myloadfrom, myloadto, myPlanBy, myPlanDate;
        public FTobeJEQ()
        {
            InitializeComponent();
        }

        private void FTobeJEQ_Load(object sender, EventArgs e)
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
                                
                if (!cm.Check_Editable(this.Name))
                {
                    btnExport.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayData()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            string plant, product, line, model,cap,user;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();

                plant="%";
                if (cbbPlant.SelectedIndex > 0)
                {
                    plant = cbbPlant.SelectedItem.ToString();
                }

                product = "%";
                if (cbbProduct.SelectedIndex >= 0)
                {
                    product= cbbProduct.SelectedItem.ToString();
                }

                line = "%";
                if (cbbLine.SelectedIndex > 0)
                {
                    line = cbbLine.SelectedItem.ToString();
                }

                model= "%";
                if (cbbModel.SelectedIndex > 0)
                {
                    model = cbbModel.SelectedItem.ToString() ;
                }

                cap = "%";
                if (cbbCap.SelectedIndex > 0)
                {
                    cap = cbbCap.SelectedItem.ToString();
                }

                user = "%" + txtPlanBy.Text + "%";
                

                sql = "SELECT DlpNo,LoadPlanFrom, LoadPlanTo, SchFrom, SchTo, REPLACE(Plant,'%','ALL') as 'Plant', REPLACE(Product,'%','ALL') as 'Product', "+
                    " REPLACE(ProdnLine,'%','ALL') as 'ProdnLine', REPLACE(Model,'%','ALL') as 'Model', Capacity, "+
                    " WD, Sat, NonWD, PlanBy, PlanDate, PlanMac from tpcs_dlp "+
                    " where LoadPlanFrom >= @from and LoadPlanTo <= @to and Plant like @plant and Product like @product and "+
                    " ProdnLine like @line and Model like @model and Capacity like @cap and PlanBy like @user";
                
                adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add("@from", SqlDbType.NVarChar).Value = dtpMCFrom.Value.ToString("yyMMdd");
                adapter.SelectCommand.Parameters.Add("@to", SqlDbType.NVarChar).Value = dtpMCTo.Value.ToString("yyMMdd");
                adapter.SelectCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = plant;
                adapter.SelectCommand.Parameters.Add("@product", SqlDbType.NVarChar).Value = product;
                adapter.SelectCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = line;
                adapter.SelectCommand.Parameters.Add("@model", SqlDbType.NVarChar).Value = model;
                adapter.SelectCommand.Parameters.Add("@cap", SqlDbType.NVarChar).Value = cap;
                adapter.SelectCommand.Parameters.Add("@user", SqlDbType.NVarChar).Value = user;
                
                adapter.Fill(dt);

                dgvReport.DataSource = null;
                dgvReport.DataSource = dt;

                dgvReport.Columns["DlpNo"].Width= 100;
                dgvReport.Columns["Plant"].Width = 40;
                dgvReport.Columns["Product"].Width = 40;
                dgvReport.Columns["Product"].HeaderText = "Prdt";
                dgvReport.Columns["ProdnLine"].Width = 40;
                dgvReport.Columns["ProdnLine"].HeaderText = "Line";
                dgvReport.Columns["PlanDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";                
                dgvReport.Columns["WD"].Width = 40;
                dgvReport.Columns["SAT"].Width = 40;
                dgvReport.Columns["NonWD"].Width = 40;

                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayData();
            detailmode = false;
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
                        cbbProduct.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
         
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

        private void cbbModel_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                if (dgvReport.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Report: Daily Load Plan");
                        header.Add("FILTER CRITERIA");
                        header.Add("Load Plan from: " + dtpMCFrom.Value.ToString("yyyy-MM-dd"));
                        header.Add("Load Plan To: " + dtpMCTo.Value.ToString("yyyy-MM-dd"));
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
                        header.Add("Product: " + cbbProduct.SelectedItem.ToString());
                        header.Add("Prodn Line: " + cbbLine.SelectedItem.ToString());
                        header.Add("Model: " + cbbModel.SelectedItem.ToString());
                        header.Add("Capacity: " + cbbCap.SelectedItem.ToString());
                        header.Add("Plan by: " + txtPlanBy.Text.ToString());
                        header.Add("Exported by: " + UserAccount.GetuserID().ToUpper());
                        header.Add("Exported Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        header.Add("");

                        if (detailmode)
                        {
                            header.Add("HEADER INFORMATION");
                            header.Add("Load Plan Date: " + myloadfrom + " - " + myloadto);
                            header.Add("Schedule Date: " + myschfrom + " - " + myschto);
                            header.Add("Plant: " + myPlant);
                            header.Add("Product: " + myProduct);
                            header.Add("Plant: " + myPlant);
                            header.Add("Model: " + myModel);
                            header.Add("Capacity: " + myCap);
                            header.Add("Working Days: " + myWd);
                            header.Add("Saturdays: " + mySat);
                            header.Add("Nonworking Days: " + myNwd);
                            header.Add("Plan By: " + myPlanBy);
                            header.Add("Plan Date: " + myPlanDate);
                        }
                        
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

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                
                DisplayDetail();
                detailmode = true;
            }
        }

        void DisplayDetail()
        {
            string sql = "";
            SqlDataAdapter adapter;
            SqlConnection conn;
            string selected;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();

                
                if (chkJEQ.Checked)
                    selected = "'1'";
                else
                    selected = "'%'";

                sql = "SELECT DLPNo, RecordId, SchDate, Plant, Product, Material, MaterialDesc, Model, ProdnLine, Shift, LotQty, LotNo,LeaderId, PackerId, ReqHours, ReqDate, " +
                    " PV, "+
                    " CASE Status "+
                    "       WHEN 'IN JEQ' THEN 'IN JEQ'"+
                    "       ELSE Remark "+
                    " END as 'Status', "+
                    " UpdateBy, UpdateDate, MacName from tpcs_tobejeq" +
                    " where DlpNo like '" + myDlpNo + "' and Selected like " + selected + " ";

                adapter = new SqlDataAdapter(sql, conn);
                
                
                adapter.Fill(dt);

                dgvReport.DataSource = null;
                dgvReport.DataSource = dt;

                dgvReport.Columns["Plant"].Width = 40;
                dgvReport.Columns["Product"].Width = 40;
                dgvReport.Columns["DLPNo"].Width = 100;
                dgvReport.Columns["RecordId"].Width = 40;
                dgvReport.Columns["Product"].HeaderText = "Prdt";
                dgvReport.Columns["ProdnLine"].Width = 40;
                dgvReport.Columns["LotQty"].Width = 60;
                dgvReport.Columns["LotNo"].Width = 60;
                dgvReport.Columns["PV"].Width = 60;
                dgvReport.Columns["Shift"].Width = 40;
                dgvReport.Columns["ReqHours"].Width = 40;
                dgvReport.Columns["ReqDate"].Width = 60;
                dgvReport.Columns["ProdnLine"].HeaderText = "Line";                
                dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();


            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count > 0)
            {
                if (dgvReport.Columns.Contains("Capacity"))
                {
                    myDlpNo = dgvReport.SelectedRows[0].Cells["DlpNo"].Value.ToString();
                    myloadfrom= dgvReport.SelectedRows[0].Cells["LoadPlanFrom"].Value.ToString();
                    myloadto = dgvReport.SelectedRows[0].Cells["LoadPlanTo"].Value.ToString();
                    myschfrom = dgvReport.SelectedRows[0].Cells["SchFrom"].Value.ToString();
                    myschto= dgvReport.SelectedRows[0].Cells["SchTo"].Value.ToString();
                    myPlant= dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                    myProduct = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                    myModel = dgvReport.SelectedRows[0].Cells["Model"].Value.ToString();
                    myLine= dgvReport.SelectedRows[0].Cells["ProdnLine"].Value.ToString();
                    myWd = dgvReport.SelectedRows[0].Cells["WD"].Value.ToString();
                    mySat= dgvReport.SelectedRows[0].Cells["Sat"].Value.ToString();
                    myNwd= dgvReport.SelectedRows[0].Cells["NonWD"].Value.ToString();
                    myCap= dgvReport.SelectedRows[0].Cells["Capacity"].Value.ToString();
                    myPlanBy= dgvReport.SelectedRows[0].Cells["PlanBy"].Value.ToString();
                    myPlanDate = dgvReport.SelectedRows[0].Cells["PlanDate"].Value.ToString();                    
                }
            }
        }

        private void chkJEQ_CheckedChanged(object sender, EventArgs e)
        {
            if (detailmode)
            {
                btnDetail.PerformClick();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

    }
}
