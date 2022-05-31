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
using System.Diagnostics;

namespace PCSSystem
{
    public partial class FIndicatorList : Form
    {
        Common cm = new Common();
        database db = new database();

        SqlCommand cmd;
        SqlConnection conn;
        //SqlDataReader reader;
        SqlTransaction trans = null;
        SqlDataAdapter adapter;
        public ArrayList selectedfg = new ArrayList();
        public static string iddetail = "";
        string sql = "";
        string MacName = System.Environment.MachineName;
        bool DetailMode = false;
        string Period = "", ReqNo = "", Plant = "", Product = "", Line = "", ProductGroup = "";
        public FIndicatorList()
        {
            InitializeComponent();
        }

        private void FIndicatorList_Load(object sender, EventArgs e)
        {
            CloseLotIndicator();

            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
            cbbStatus.SelectedIndex = 1;

            DisplayData();
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

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayData();
                DetailMode = false;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayData()
        {
            string plant, product, model, line, status;
            string query_and_date = "";
            DataTable dt = new DataTable();
            try
            {
                plant = "'%'";
                if (cbbPlant.SelectedIndex >= 0)
                    plant = "'" + cbbPlant.SelectedItem.ToString() + "'";

                product = "'%'";
                if (cbbProduct.SelectedIndex >= 0)
                    product = "'" + cbbProduct.SelectedItem.ToString() + "'";

                model = "'%'";
                if (cbbModel.SelectedIndex > 0)
                    model = "'" + cbbModel.SelectedItem.ToString() + "'";

                line = "'%'";
                if (cbbLine.SelectedIndex > 0)
                    line = "'" + cbbLine.SelectedItem.ToString() + "'";

                status = "'%'";
                query_and_date = "AND (CONVERT(DATE,EffectiveStart)>=CONVERT(DATE,@Start) AND CONVERT(DATE,EffectiveEnd)<=CONVERT(DATE,@End))";

                if (cbbStatus.SelectedIndex > 0)
                {
                    status = "'" + cbbStatus.SelectedItem.ToString() + "'";

                    if (status == "'OPEN'")
                    {
                        query_and_date = "";
                    }

                }

                conn = db.GetConnString();

                sql = @"DECLARE 
	                        @Plant NVARCHAR(10)=" + plant + @"
	                        ,@Product NVARCHAR(10)=" + product + @"
	                        ,@ProdLine NVARCHAR(10)=" + line + @"
	                        ,@Model NVARCHAR(10)=" + model + @"
	                        ,@Status NVARCHAR(20)=" + status + @"
	                        ,@Start NVARCHAR(50)='" + dpStartDate.Text + @"'
	                        ,@End NVARCHAR(50)='" + dpEndDate.Text + @"'

                        SELECT 
                        ReqNo, 
                        Period,
                        Status, 
                        Plant, 
                        Product, 
                        Line, 
                        ProductGroup, 
                        ChangeType, 
                        FGLotInd=FGLotInd+'0'+FGLotIndNo, 
                        ChangeDescBefore, 
                        ChangeDescAfter, 
                        ReqQty,
                        EffectiveStart=convert(varchar, EffectiveStart, 120), 
                        EffectiveEnd=convert(varchar, EffectiveEnd, 120), 
                        ApprovedBy, 
                        SubmittedBy, 
                        SubmittedDate=convert(varchar,SubmittedDate, 120) 
                        FROM TPCS_LOT_IND_NEW
                        WHERE
                        Plant=@Plant
                        AND Product=@Product
                        AND Line LIKE @ProdLine
                        AND ProductGroup LIKE @Model
                        AND Status LIKE @Status
                        "+ query_and_date + @"
                        --AND (CONVERT(DATE,EffectiveStart)>=CONVERT(DATE,@Start) AND CONVERT(DATE,EffectiveEnd)<=CONVERT(DATE,@End))
                        ORDER BY Status DESC";
                        


                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                lblrowsheader.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FNewLotInd f;
            f = new FNewLotInd();
            if (f.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Your new lot indicator has been submitted successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            f.Dispose();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if ((dgvReport.SelectedRows.Count > 0) && (! DetailMode))
            //    {
            //        DisplayDataDetail();
            //        DetailMode = true;
            //        btnEdit.Text = "CLOSED Ind";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    db.SaveError(ex.ToString());
            //}
        }

        private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLine.SelectedIndex >= 0)
            {
                db.SetModelLine(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString(), cbbLine.SelectedItem.ToString());
                if (cbbModel.Items.Count > 0)
                {
                    cbbModel.Items.Insert(0, "[ALL]");
                    cbbModel.SelectedIndex = 0;
                }
            }
        }

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    foreach (DataGridViewRow row in dgvReport.Rows)
            //    {
            //        string Status = dgvReport.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            //        if (Status == "OPEN")
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Green;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //        else
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Red;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //    }
            //}
        }

        private void dgvReportDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    foreach (DataGridViewRow row in dgvReportDetail.Rows)
            //    {
            //        string Status = dgvReportDetail.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            //        if (Status == "OPEN")
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Green;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //        else
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Red;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //    }
            //}

        }

        private void cbbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbModel.SelectedIndex >= 0)
            {
                db.SetFG(ref cbbFGCode, cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                if (cbbFGCode.Items.Count > 0)
                {
                    cbbFGCode.Items.Insert(0, "[ALL]");
                    cbbFGCode.SelectedIndex = 0;
                }
            }
        }

        private void dgvReportDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        string IDDetail = "";
            //        if (dgvReportDetail.RowCount > 0)
            //        {
            //            string Action = dgvReportDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //            IDDetail = dgvReportDetail.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            //            if (Action == "System.Drawing.Bitmap")
            //            {
            //                MessageBox.Show(IDDetail, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    db.SaveError(ex.ToString());
            //}
        }

        private void dgvReportDetail_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 1)
                    {
                        string _Attachment = dgvReportDetail.Rows[e.RowIndex].Cells["Attachment"].Value.ToString();
                        if (_Attachment != "")
                        {
                            string appPath = Properties.Settings.Default.AppPath.ToUpper();
                            if (MessageBox.Show("Would you like to open the file?", "Attachment", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                Process.Start(appPath +"\\"+ _Attachment);
                        }
                        else
                        {
                            MessageBox.Show("No File!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        string _Status = dgvReportDetail.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                        if (_Status != "CLOSE")
                            if (e.ColumnIndex == 0)
                                dgvReportDetail.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgvReportDetail.Rows[e.RowIndex].Cells[0].Value);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnCloseInd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvReportDetail.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvReportDetail.Rows[i].Cells["ChkCol"].Value))
                    {
                        selectedfg.Add(dgvReportDetail.Rows[i].Cells["ID"].Value.ToString());
                    }
                }

                if (selectedfg.Count > 0)
                {
                    iddetail = "";
                    for (int i = 0; i < selectedfg.Count; i++)
                    {
                        iddetail = iddetail + "'" + selectedfg[i].ToString() + "',";
                    }

                    if (iddetail.Length > 0)
                    {
                        iddetail = iddetail.Substring(0, iddetail.Length - 1);

                        FReasonClose f;
                        f = new FReasonClose();

                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            if (CloseLotIndicatorByIdDetail())
                                CloseLotIndicator();

                            DisplayDataDetail();
                        }
                        f.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("You haven't selected any FG!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbFGCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbFGCode.SelectedIndex >= 0)
                {
                    txtFGDesc.Text = db.SetFGName(cbbPlant.SelectedItem.ToString(), cbbFGCode.SelectedItem.ToString());
                }
                DisplayDataDetail();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dgvReportDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    foreach (DataGridViewRow row in dgvReportDetail.Rows)
            //    {
            //        string Status = dgvReportDetail.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            //        if (Status == "OPEN")
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Green;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //        else
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Red;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //    }
            //}
        }

        private void dgvReport_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    foreach (DataGridViewRow row in dgvReport.Rows)
            //    {
            //        string Status = dgvReport.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            //        if (Status == "OPEN")
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Green;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //        else
            //        {
            //            row.Cells["Status"].Style.BackColor = Color.Red;
            //            row.Cells["Status"].Style.ForeColor = Color.White;
            //        }
            //    }
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.Filter = "Excel Documents (.xlsx)|.xlsx";
            sfd1.FileName = "Report Indicator List";
            DataTable dt = new DataTable("Report_Indicator_List");
            string path = "";
            try
            {
                if (sfd1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName.ToString();
                    dt = GetData();
                    dt.TableName = "Report_Indicator_List";
                    using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Sheet1");
                        wb.SaveAs(sfd1.FileName);
                    }
                }

                if (MessageBox.Show("Export completed, Would you like to open the file?","Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Process.Start(sfd1.FileName);
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbStatus.SelectedItem.ToString() == "OPEN")
            {
                label24.Visible = false;
                label25.Visible = false;
                dpStartDate.Visible = false;
                dpEndDate.Visible = false;
            }
            else
            {
                label24.Visible = true;
                label25.Visible = true;
                dpStartDate.Visible = true;
                dpEndDate.Visible = true;
            }
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvReport.RowCount > 0)
                {
                    ////DETAIL
                    Period = dgvReport.Rows[e.RowIndex].Cells["Period"].Value.ToString();
                    ReqNo = dgvReport.Rows[e.RowIndex].Cells["ReqNo"].Value.ToString();
                    Plant = dgvReport.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    Product = dgvReport.Rows[e.RowIndex].Cells["Product"].Value.ToString();
                    Line = dgvReport.Rows[e.RowIndex].Cells["Line"].Value.ToString();
                    ProductGroup = dgvReport.Rows[e.RowIndex].Cells["ProductGroup"].Value.ToString();
                    DisplayDataDetail();
                }
            }
        }

        void DisplayDataDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                iddetail = "";
                selectedfg.Clear();

                string fgcode;
                fgcode = "'%'";
                if (cbbFGCode.SelectedIndex > 0)
                    fgcode = "'" + cbbFGCode.SelectedItem.ToString() + "'";

                conn = db.GetConnString();
                sql = @"DECLARE 
	                        @Period NVARCHAR(10)='" + Period + @"'
	                        ,@ReqNo NVARCHAR(10)='" + ReqNo + @"'
	                        ,@Plant NVARCHAR(10)='" + Plant + @"'
	                        ,@Product NVARCHAR(10)='" + Product + @"'
	                        ,@ProdLine NVARCHAR(10)='" + Line + @"'
	                        ,@Model NVARCHAR(10)='" + ProductGroup + @"'
	                        ,@Material NVARCHAR(50)=" + fgcode + @"

                        SELECT 
                                b.ID,
                                FGLotInd=FGLotInd+'0'+FGLotIndNo,
                                b.Status,
                                b.Plant,
                                b.Product,
                                b.Line,
                                b.ProductGroup,
                                b.Material,
                                b.MaterialDesc,
                                a.ChangeItem,
                                a.FGStatus,
                                a.BackgroundImprovement,
                                a.Attachment,
                                b.CLoseBy,
                                CloseDate=convert(varchar, b.CloseDate, 120),
                                b.Remark
                                FROM TPCS_LOT_IND_NEW a
                                JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
								                                AND a.ReqNo=b.ReqNo 
								                                AND a.Product=b.Product 
								                                AND a.Plant=b.Plant
								                                AND a.Line=b.Line 
								                                AND a.ProductGroup=b.ProductGroup
                                WHERE
                                a.Period=@Period
                                AND a.ReqNo=@ReqNo
                                AND a.Plant=@Plant
                                AND a.Product=@Product
                                AND a.Line=@ProdLine
                                AND a.ProductGroup=@Model
                                AND b.Material LIKE @Material";



                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReportDetail.DataSource = dt;

                lblRows.Text = "Total Rows: " + dgvReportDetail.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool CloseLotIndicatorByIdDetail()
        {
            bool result = true;
            try
            {
                string _reason = FReasonClose.reasonclose;
                conn = db.GetConnString();
                #region QUERY
                sql = @"UPDATE a SET 
                        Status='CLOSE'
                        ,CloseBy='" + UserAccount.GetuserID() + @"'
                        ,CloseDate=GETDATE()
                        ,Remark='" + _reason + @"'
                        FROM TPCS_LOT_IND_DET_NEW a WHERE ID IN ("+ iddetail + @")";
                #endregion QUERY
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                result = false;
                db.SaveError(ex.ToString());

            }
            return result;
        }

        void CloseLotIndicator()
        {
            try
            {
                conn = db.GetConnString();
                #region QUERY
                sql = @"EXEC SP_PCS_CLOSE_LOT_INDICATOR";
                #endregion QUERY
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());

            }
        }

        public DataTable GetData()
        {
            string plant, product, model, line, status, fgcode;
            string query_and_date = "";

            plant = "'%'";
            if (cbbPlant.SelectedIndex >= 0)
                plant = "'" + cbbPlant.SelectedItem.ToString() + "'";

            product = "'%'";
            if (cbbProduct.SelectedIndex >= 0)
                product = "'" + cbbProduct.SelectedItem.ToString() + "'";

            model = "'%'";
            if (cbbModel.SelectedIndex > 0)
                model = "'" + cbbModel.SelectedItem.ToString() + "'";

            line = "'%'";
            if (cbbLine.SelectedIndex > 0)
                line = "'" + cbbLine.SelectedItem.ToString() + "'";

            status = "'%'";
            query_and_date = "AND (CONVERT(DATE,a.EffectiveStart)>=CONVERT(DATE,@Start) AND CONVERT(DATE,a.EffectiveEnd)<=CONVERT(DATE,@End))";

            if (cbbStatus.SelectedIndex > 0)
            {
                status = "'" + cbbStatus.SelectedItem.ToString() + "'";

                if (status == "'OPEN'")
                {
                    query_and_date = "";
                }

            }

            fgcode = "'%'";
            if (cbbFGCode.SelectedIndex > 0)
                fgcode = "'" + cbbFGCode.SelectedItem.ToString() + "'";

            conn = db.GetConnString();
            string sql = @"DECLARE 
	                        @Plant NVARCHAR(10)=" + plant + @"
	                        ,@Product NVARCHAR(10)=" + product + @"
	                        ,@ProdLine NVARCHAR(10)=" + line + @"
	                        ,@Model NVARCHAR(10)=" + model + @"
	                        ,@Status NVARCHAR(20)=" + status + @"
	                        ,@Start NVARCHAR(50)='" + dpStartDate.Text + @"'
	                        ,@End NVARCHAR(50)='" + dpEndDate.Text + @"'
                            ,@Material NVARCHAR(50)=" + fgcode + @"

                        SELECT 
	                        a.Period, 
	                        a.ReqNo, 
	                        a.Plant, 
	                        a.Product, 
	                        a.Line, 
	                        a.ProductGroup,
	                        b.Material, 
	                        b.MaterialDesc,
	                        a.ChangeType, 
	                        a.FGLotInd, 
	                        a.FGLotIndNo, 
	                        a.ChangeDescBefore, 
	                        a.ChangeDescAfter, 
	                        a.ChangeItem, 
	                        a.FGStatus, 
	                        a.BackgroundImprovement, 
	                        EffectiveStart=convert(varchar, a.EffectiveStart, 120), 
	                        EffectiveEnd=convert(varchar, a.EffectiveEnd, 120), 
	                        a.ReqQty, 
	                        a.ApprovedBy, 
	                        a.SubmittedBy, 
	                        SubmittedDate=convert(varchar, a.SubmittedDate, 120),
	                        b.Status, 
	                        b.CloseBy, 
	                        CloseDate=convert(varchar, b.CloseDate, 120),
	                        b.Remark
	                        FROM TPCS_LOT_IND_NEW a
	                        JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
									                        AND a.ReqNo=b.ReqNo 
									                        AND a.Plant=b.Plant 
									                        AND a.Product=b.Product 
									                        AND a.Line=b.Line 
									                        AND a.ProductGroup=b.ProductGroup
	                        WHERE a.Plant=@Plant
                                    AND a.Product=@Product
                                    AND a.Line LIKE @ProdLine
                                    AND a.ProductGroup LIKE @Model
                                    AND a.Status LIKE @Status
                                    AND a.Status LIKE @Status
                                    AND b.Material LIKE @Material
                                    "+ query_and_date + @"--AND (CONVERT(DATE,a.EffectiveStart)>=CONVERT(DATE,@Start) AND CONVERT(DATE,a.EffectiveEnd)<=CONVERT(DATE,@End))";
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            return dt;
        }
    }
}
