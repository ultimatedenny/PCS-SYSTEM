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
using System.IO;

namespace PCSSystem
{
    public partial class FNewLotInd : Form
    {
        Common cm = new Common();
        database db = new database();

        SqlDataAdapter adapter;
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader reader;
        SqlTransaction trans = null;
        string sql = "";
        string fileName = "";
        string fileAttach = "";
        string ext = "";
        string fileSavePath = "";
        string opFileName = "";

        string MacName = System.Environment.MachineName;

        bool EditMode = false;
        private string _period, _plant, _mat, _indno, _product,_model;
        public static string materialchooice = "";
        public static string _materialchooice = "";

        string appPath = Properties.Settings.Default.AppPath.ToUpper();

        public FNewLotInd()
        {
            InitializeComponent();
        }

        public FNewLotInd(string period, string plant, string mat, string indno, string product,string model)
        {
            InitializeComponent();
            EditMode = true;
            _period = period;
            _plant = plant;
            _mat = mat;
            _indno = indno;
            _product = product;
            _model = model;
        }

        private void FNewLotInd_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
            materialchooice = "";
            _materialchooice = "";
            SetOptions();

            if (EditMode)
                DisplayEdit();
            
        }

        void DisplayEdit()
        {
            ArrayList fg = new ArrayList();
            try
            {
                if (cbbPlant.FindStringExact(_plant) >= 0)
                    cbbPlant.SelectedItem = _plant;
                else
                    cbbPlant.SelectedIndex = -1;

                if (cbbPrd.FindStringExact(_product) >= 0)
                    cbbPrd.SelectedItem = _product;
                else
                    cbbPrd.SelectedIndex = -1;

                //radAll.Enabled = false;
                //radFGCode.Enabled = false;

                cbbModel.Enabled = false;
                cbbModel.SelectedItem = _model;
                               
                //conn = db.GetConnString();
                //sql = "SELECT AffectedChange, ChangeType,  ProbOrigin, convert(nvarchar(20),effstart,101) as 'EffStart', "+
                //    " convert(nvarchar(20),EffEnd,101) as 'EffEnd', ISNULL(ReqQty,'0') as 'ReqQty', ChngDescBefore, ChngDescAfter, ApprovedBy, Status "+
                //    " from tpcs_lot_ind where Period='"+_period+"' and IndNo='"+_indno+"' and Product='"+_product+"' and Plant='"+_plant+"' and Model='"+_model+"'";
                //cmd = new SqlCommand(sql, conn);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                    
                //    cbbAff.Enabled = false;
                //    if (cbbAff.FindStringExact(reader["AffectedChange"].ToString()) >= 0)
                //        cbbAff.SelectedItem = reader["AffectedChange"].ToString();
                //    else
                //        cbbAff.SelectedIndex = -1;

                //    cbbChangeType.Enabled = false;
                //    if (cbbChangeType.FindStringExact(reader["ChangeType"].ToString()) >= 0)
                //        cbbChangeType.SelectedItem = reader["ChangeType"].ToString();
                //    else
                //        cbbChangeType.SelectedIndex = -1;


                //    if (cbbProb.FindStringExact(reader["ProbOrigin"].ToString()) >= 0)
                //        cbbProb.SelectedItem = reader["ProbOrigin"].ToString();
                //    else
                //        cbbProb.SelectedIndex = -1;

                //    dtpStart.Value = DateTime.ParseExact(reader["EffStart"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //    dtpEnd.Value = DateTime.ParseExact(reader["EffEnd"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //    txtQty.Text = reader["ReqQty"].ToString();
                //    txtDescBefore.Text = reader["ChngDescBefore"].ToString();
                //    txtDescAft.Text = reader["ChngDescAfter"].ToString();
                //    txtAppr.Text = reader["ApprovedBy"].ToString();
                //    cbbStatus.SelectedItem = "OPEN";

                //}
                //reader.Close();

                //conn = db.GetConnString();
                //sql = "SELECT Material " +
                //   " from tpcs_lot_ind_det where Period='" + _period + "' and IndNo='" + _indno + "' and Product='" + _product + "' and Plant='" + _plant + "' and Model='" + _model + "'";
                //cmd.CommandText = sql;
                //cmd.Connection = conn;
                //reader = cmd.ExecuteReader();

                //while (reader.Read())
                //{
                //    fg.Add(reader[0].ToString());
                //}
                //reader.Close();

                //cbbFGCode.Items.AddRange(fg.ToArray());
                //cbbFGCode.SelectedIndex = 0;
                //cbbFGCode.Enabled = false;
                //lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void SetOptions()
        {
            ArrayList result = new ArrayList();
            try
            {
                materialchooice = "";
                conn = db.GetConnString();
                sql = "SELECT ChangeItem FROM TPCS_CHANGEITEM";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbChangeItem.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                sql = "SELECT ChangeType from TPCS_CHNGTYP";
                cmd = new SqlCommand(sql, conn);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbChangeType.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                sql = "SELECT FGSTatus FROM TPCS_FGSTATUS";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbFGStatus.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                sql = "SELECT BackgroundImprovement FROM TPCS_BACKGROUNDIMPROVEMENT";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbBgImprovement.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                conn.Dispose();

                //if (!EditMode)
                //{
                //    cbbStatus.Enabled = false;
                //    cbbStatus.SelectedItem = "OPEN";
                //}

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
                    db.SetProduct(ref cbbPrd, cbbPlant.SelectedItem.ToString());
                    if (cbbPrd.Items.Count > 0)
                    {
                        cbbPrd.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbPrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbbPrd.SelectedIndex >= 0)
            //    {
            //        db.SetModel(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString());
            //        if (cbbModel.Items.Count > 0)
            //        {
            //            cbbModel.SelectedIndex = 0;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    db.SaveError(ex.ToString());
            //}
            try
            {
                //dgvReport.Columns.Clear();
                //materialchooice = "";
                //lblTotalFG.Text = "Total : 0";
                btnGetFGCodeList.Enabled = false;
                cbbLine.Text = "";
                cbbModel.Text = "";
                if (cbbPrd.SelectedIndex >= 0)
                {
                    db.SetLine(ref cbbLine, cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString());
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

        private void cbbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //dgvReport.Columns.Clear();
                //materialchooice = "";
                //lblTotalFG.Text = "Total : 0";
                if (cbbModel.SelectedIndex >= 0)
                {
                    btnGetFGCodeList.Enabled = true;
                    //db.SetFG(ref cbbFGCode,cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                    //if (cbbFGCode.Items.Count > 0)
                    //{
                    //    cbbFGCode.Items.Insert(0, "[ALL]");
                    //    cbbFGCode.SelectedIndex = 0;
                    //}
                    //cbbFGCode.Items.Clear();
                    //txtFGDesc.Text = "";
                    //lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
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
                //if (cbbFGCode.SelectedIndex >= 0)
                //{
                //    txtFGDesc.Text = db.SetFGName(cbbPlant.SelectedItem.ToString(), cbbFGCode.SelectedItem.ToString());                    

                //}
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0";
                }
                else
                {
                    t.Text = Convert.ToInt32(t.Text).ToString();                    
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }     
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "0")
                {
                    t.Text = "";
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }   
        }

        private void cbbProb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                //if (cbbProb.SelectedIndex >= 0)
                //    if (cbbProb.SelectedItem.ToString() == "OTHERS")
                //    {
                //        cbbProb.Visible = false;
                //        txtOth.Visible = true;
                //        txtOth.Focus();
                //    }
                //    else
                //    {                               
                //        txtOth.Visible = false;
                //    }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void txtOth_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtOth_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        cbbProb.Items.Insert(0, txtOth.Text);
            //        cbbProb.SelectedIndex = 0;
            //        cbbProb.Visible = true;
            //        txtOth.Visible = false;
            //        txtOth.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    db.SaveError(ex.ToString());
            //}
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (radAll.Checked)
                //{
                //    if (cbbModel.SelectedIndex >= 0)
                //    {
                //        db.SetFG(ref cbbFGCode, cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                //        if (cbbFGCode.Items.Count > 0)
                //        {
                //            cbbFGCode.SelectedIndex = 0;
                //        }
                //        lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
                //    }
                //}
                //else
                //{
                //    cbbFGCode.Items.Clear();
                //    txtFGDesc.Text = "";
                //}
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbChangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                if (cbbChangeType.SelectedIndex >= 0)
                {
                    conn = db.GetConnString();
                    sql = "SELECT Indicator from TPCS_chngtyp where ChangeType='" + cbbChangeType.SelectedItem.ToString() + "'";
                    cmd = new SqlCommand(sql, conn);

                    if (cmd.ExecuteScalar() == null)
                        result = "";
                    else
                        result = cmd.ExecuteScalar().ToString();
                    conn.Dispose();

                    txtInd.Text = result;

                    //if (result == "A")
                    //{
                    //    dtpStart.Enabled = true;
                    //    dtpEnd.Enabled = true;
                    //    txtQty.Enabled = false;
                    //}
                    //else
                    //{
                    //    dtpStart.Enabled = false;
                    //    dtpEnd.Enabled = false;
                    //    txtQty.Enabled = true;
                    //}

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

        private void radFGCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                //if (radFGCode.Checked)
                //{
                //    if (cbbModel.SelectedIndex >= 0)
                //    {
                //        FSelFG f;
                //        f = new FSelFG(cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                //        f.ShowDialog();
                //        if (f.selectedfg.Count > 0)
                //        {
                //            cbbFGCode.Items.Clear();
                //            cbbFGCode.Items.AddRange(f.selectedfg.ToArray());
                //        }
                //        f.Dispose();
                //        lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
                //    }
                //}
                //else
                //{
                //    cbbFGCode.Items.Clear();
                //    txtFGDesc.Text = "";
                //}
                
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
                bool StepConflictLotInd = true;
                bool StepFinish = false;
                if (dgvReport.Rows.Count > 0)
                {
                    if (IsDataValid())
                    {
                        btnSave.Enabled = false;
                        if (CheckConflictLotInd())
                        {
                            string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                            FLotConflictInd f;
                            f = new FLotConflictInd(cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString(), cbbLine.SelectedItem.ToString(), cbbModel.SelectedItem.ToString(), txtInd.Text.ToString(), txtIndInput.Text.ToString(), txtDescAfter.Text.ToString(), PeriodStart);
                            if (f.ShowDialog() != DialogResult.OK)
                                StepConflictLotInd = false;
                            f.Dispose();
                        }

                        if (StepConflictLotInd == true)
                        {
                            if (SaveRecord())
                            {
                                //MASUKKAN FILE NYA KE FOLDER TUJUAN
                                if (opFileName != "")
                                    File.Copy(opFileName, fileSavePath, true);
                                
                                //CHECK JIKA ADA SCHEDULE
                                if (CheckUpdateSchConfirm())
                                {
                                    string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                                    FLotConfirmSchedule f;
                                    f = new FLotConfirmSchedule(cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString(), cbbLine.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                                    f.ShowDialog();
                                    f.Dispose();
                                }
                                StepFinish = true;
                            }

                            if (StepFinish == true)
                                this.DialogResult = DialogResult.OK;
                        }

                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please select the Material!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //dgvReport.Columns.Clear();
                //materialchooice = "";
                //lblTotalFG.Text = "Total : 0";
                btnGetFGCodeList.Enabled = false;
                cbbModel.Text = "";
                if (cbbLine.SelectedIndex >= 0)
                {
                    db.SetModelLine(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString(), cbbLine.SelectedItem.ToString());
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

        private void btnGetFGCodeList_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbModel.SelectedIndex >= 0)
                {
                    FNewSelFG f;
                    f = new FNewSelFG(cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString(), cbbLine.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                    
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        //materialchooice = "";
                        for (int i = 0; i < f.selectedfg.Count; i++)
                        {
                            //materialchooice = materialchooice + "'" + f.selectedfg[i].ToString() + "',";
                            materialchooice = materialchooice + "" + f.selectedfg[i].ToString() + ",";
                        }

                        for (int i = 0; i < f.Noselectedfg.Count; i++)
                        {
                            materialchooice = materialchooice.Replace(f.Noselectedfg[i].ToString(), "");
                        }

                        if (materialchooice.Length > 0)
                        {
                            _materialchooice = materialchooice.Substring(0, materialchooice.Length - 1);
                            DisplayData();
                        }
                    }
                    f.Dispose();

                }
                else
                {
                    MessageBox.Show("Please select the Product Grp!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void ClearControl()
        {
            try
            {
                cbbPlant.SelectedIndex = -1;
                cbbPrd.SelectedIndex = -1;
                cbbModel.SelectedIndex = -1;
                //radAll.Checked = false;
                //radFGCode.Checked = false;
                //cbbFGCode.Items.Clear();
                lblTotalFG.Text = "Total: 0";
                //txtFGDesc.Text = "";
                //cbbAff.SelectedIndex = -1;
                //cbbChangeType.SelectedIndex = -1;
                //txtInd.Text = "";
                //cbbProb.SelectedIndex = -1;
                //txtDescAft.Text = "";
                //txtDescBefore.Text = "";
                //txtAppr.Text = "";
                //txtQty.Text = "0";
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
                {
                    string Matcode = "";
                    string Line = "";
                    string Model = "";
                    string _Matcode = "";
                    if (dgvReport.RowCount > 0)
                    {
                        string Action = dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Matcode = dgvReport.Rows[e.RowIndex].Cells["Material"].Value.ToString();
                        Line = dgvReport.Rows[e.RowIndex].Cells["Line"].Value.ToString();
                        Model = dgvReport.Rows[e.RowIndex].Cells["ProductGrp"].Value.ToString();
                        _Matcode = Line + "|" + Model + "|" + Matcode;
                        if (Action == "System.Drawing.Bitmap")
                        {
                            materialchooice = materialchooice.Replace(_Matcode, "");
                            _materialchooice = materialchooice.Replace(_Matcode, "");
                            DisplayData();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnUploadAttach_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvReport.Rows.Count > 0)
                {
                    using (OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|Pdf Files|*.pdf" })
                    {
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            fileName = Path.GetFileName(openFileDialog1.FileName);
                            ext = Path.GetExtension(fileName);
                            //fileSavePath = Path.Combine(appPath, "test"+ ext);
                            opFileName = openFileDialog1.FileName;
                            lblAttachment.Visible = true;
                            lblAttachment.Text = fileName;
                        }
                        else
                        {
                            lblAttachment.Visible = false;
                            opFileName = "";
                            fileSavePath = "";
                            fileName = "";
                            ext = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select the Material!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool IsDataValid()
        {
            bool result = true;
            string mats = "";
            try
            {
                if (cbbChangeType.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Change Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (txtInd.Text.Length == 0 || txtIndInput.Text.Length == 0)
                {
                    MessageBox.Show("FG Lot Indicator by must be filled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (txtDescBefore.Text.Length == 0)
                {
                    MessageBox.Show("Change Desc Before by must be filled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (txtDescAfter.Text.Length == 0)
                {
                    MessageBox.Show("Change Desc After by must be filled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbChangeItem.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Change Item!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }
                
                if (cbbFGStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the FG Status!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }
                
                if (cbbBgImprovement.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Background of Improvement!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;

                if (Convert.ToDateTime(PeriodStart) >= Convert.ToDateTime(PeriodEnd))
                {
                    MessageBox.Show("Effective Start must be before Effective End Date!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (Convert.ToDateTime(PeriodEnd) < Convert.ToDateTime(DateTime.Now))
                {
                    MessageBox.Show("Effective End must be before Date now!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (txtAppr.Text.Length == 0)
                {
                    MessageBox.Show("Approved by must be filled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    txtAppr.Focus();
                    return result;
                }

                if (txtQty.Text.Length !=0)
                {
                    if (Convert.ToInt32(txtQty.Text) <= 0)
                    {
                        MessageBox.Show("Req Qty must be more than 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                db.SaveError(ex.ToString());
            }
            return result;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        bool SaveRecord()
        {
            bool result = false;
            try
            {
                string ReqNumber = GetNewReqNumber();
                conn = db.GetConnString();

                string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;

                if (ext != "")
                {
                    fileAttach = DateTime.Today.ToString("yyyyMM").ToString() + "-" + ReqNumber + "-" + cbbPlant.SelectedItem.ToString() + "-" + cbbPrd.SelectedItem.ToString() + ext;
                    fileSavePath = Path.Combine(appPath, fileAttach);
                }
                
                string _dplant, _dproduct, _dline, _dmodel;

                _dplant = cbbPlant.SelectedItem.ToString();
                _dproduct = cbbPrd.SelectedItem.ToString();
                _dline = cbbLine.SelectedItem.ToString();
                _dmodel = cbbModel.SelectedItem.ToString();

                if (_dline == "[ALL]")
                    _dline = "%";

                if (_dmodel == "[ALL]")
                    _dmodel = "%";

                trans = conn.BeginTransaction();
                #region QUERY
                //           sql = @"DECLARE 
                //		@Plant NVARCHAR(10)='" + _dplant + @"'
                //		,@Product NVARCHAR(10)='" + _dproduct + @"'
                //		,@ProdLine NVARCHAR(10)='" + _dline + @"'
                //		,@Model NVARCHAR(10)='" + _dmodel + @"'
                //		,@ReqNo NVARCHAR(2)='" + ReqNumber + @"'
                //		,@Status NVARCHAR(10)='OPEN'
                //		,@ChangeType NVARCHAR(50)='" + cbbChangeType.SelectedItem.ToString() + @"'
                //		,@FGLotInd NVARCHAR(50)='" + txtInd.Text.ToString() + @"'
                //		,@FGLotIndNo NVARCHAR(50)='" + txtIndInput.Text.ToString() + @"'
                //		,@DescBefore NVARCHAR(300)='" + txtDescBefore.Text.ToString() + @"'
                //		,@DescAfter NVARCHAR(300)='" + txtDescAfter.Text.ToString() + @"'
                //		,@ChangeItem NVARCHAR(300)='" + cbbChangeItem.SelectedItem.ToString() + @"'
                //		,@FGStatus NVARCHAR(300)='" + cbbFGStatus.SelectedItem.ToString() + @"'
                //		,@BackgroundImprovement NVARCHAR(300)='" + cbbBgImprovement.SelectedItem.ToString() + @"'
                //		,@EffectiveStart NVARCHAR(300)='" + PeriodStart + @"'
                //		,@EffectiveEnd NVARCHAR(300)='" + PeriodEnd + @"'
                //		,@ReqQty NVARCHAR(300)='" + txtQty.Text.ToString() + @"'
                //		,@ApprovedBy NVARCHAR(300)='" + txtAppr.Text.ToString() + @"'
                //		,@Attachment NVARCHAR(300)='" + fileAttach.ToString() + @"'
                //		,@SubmittedBy NVARCHAR(300)='" + UserAccount.GetuserID() + @"'

                //SELECT 
                //                Period=REPLACE(LEFT(CONVERT(DATE,GETDATE()),7),'-','')
                //                ,ReqNo=@ReqNo
                //                   ,a.Plant
                //                   ,a.Product
                //                   ,Line=a.SAPWC
                //                   ,ProductGroup=a.Model
                //                   ,b.Material
                //                   ,Description=b.MaterialDesc
                //                ,Status=@Status
                //                ,ChangeType=@ChangeType
                //                , FGLotInd=@FGLotInd
                //                , FGLotIndNo=@FGLotIndNo
                //                , ChangeDescBefore=@DescBefore
                //                , ChangeDescAfter=@DescAfter
                //                , ChangeItem=@ChangeItem
                //                , FGStatus=@FGStatus
                //                , BackgroundImprovement=@BackgroundImprovement
                //                , EffectiveStart=CONVERT(DATETIME, @EffectiveStart, 101)
                //                , EffectiveEnd=CONVERT(DATETIME, @EffectiveEnd, 101)
                //                , ReqQty=@ReqQty
                //                , ApprovedBy=@ApprovedBy
                //                , Attachment=@Attachment
                //                , SubmittedBy=@SubmittedBy
                //                , SubmittedDate=GETDATE()
                //                INTO #Data
                //                   FROM TPCS_ROUTEMP a
                //                   JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                //                   WHERE
                //                   a.Plant = @Plant
                //                   AND
                //                   a.Product = @Product
                //                   AND
                //                   a.SAPWc LIKE @ProdLine
                //                   AND
                //                   a.Model LIKE @Model
                //                   AND
                //                   b.Material IN (" + materialchooice + @")
                //                   ORDER BY b.Material


                //	---CHECK YG CONFLICT DAN UPDATE STATUS YG CONFLICT
                //		SELECT a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.MaterialDesc,b.Status
                //		,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter,a.EffectiveEnd
                //		INTO #DataReady
                //		FROM TPCS_LOT_IND_NEW a
                //		JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
                //									AND a.ReqNo=b.ReqNo 
                //									AND a.Product=b.Product 
                //									AND a.Plant=b.Plant
                //									AND a.Line=b.Line 
                //									AND a.ProductGroup=b.ProductGroup

                //		SELECT 
                //		a.Plant,
                //		a.Product,
                //		a.Line,
                //		a.ProductGroup,
                //		a.Material,
                //		a.Description
                //		INTO #DataConflict
                //		FROM #Data a
                //		JOIN #DataReady b ON a.Plant=b.Plant 
                //							AND a.Product=b.Product 
                //							AND a.Line=b.Line 
                //							AND a.ProductGroup=b.ProductGroup
                //							AND a.Material=b.Material
                //		WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd


                //		UPDATE a
                //		SET a.Status='CLOSE'
                //			,a.CloseBy=@SubmittedBy
                //			,a.CloseDate=GETDATE()
                //			,a.Remark='CONFLICT LOT INDICATOR NEW PERIOD '+ CONVERT(VARCHAR,REPLACE(LEFT(CONVERT(DATE,GETDATE()),7),'-',''))+'-'+@ReqNo
                //		FROM TPCS_LOT_IND_DET_NEW a
                //		JOIN #DataConflict b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.Line AND a.ProductGroup=b.ProductGroup AND a.Material=b.Material
                //		 WHERE
                //			a.Plant = @Plant
                //			AND
                //			a.Product = @Product
                //			AND
                //			a.Line LIKE @ProdLine
                //                           AND 
                //                           a.Status<>'CLOSE'

                //	--END

                //	----UPDATE STATUS KE CLOSE JIKA SEMUA DETAIL SUDAH CLOSE SEMUA
                //		SELECT a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup
                //		,CountStatus=COUNT(DISTINCT b.Status)
                //		INTO #DataHeaderCountStatus
                //		FROM TPCS_LOT_IND_NEW a
                //		JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
                //										AND a.ReqNo=b.ReqNo 
                //										AND a.Plant=b.Plant 
                //										AND a.Product=b.Product 
                //										AND a.Line=b.Line 
                //										AND a.ProductGroup=b.ProductGroup
                //		GROUP BY a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup


                //		UPDATE a SET a.Status=c.Status FROM TPCS_LOT_IND_NEW a
                //		JOIN #DataHeaderCountStatus b ON a.Period=b.Period 
                //										AND a.ReqNo=b.ReqNo 
                //										AND a.Plant=b.Plant 
                //										AND a.Product=b.Product 
                //										AND a.Line=b.Line 
                //										AND a.ProductGroup=b.ProductGroup
                //		JOIN TPCS_LOT_IND_DET_NEW c  ON a.Period=c.Period 
                //										AND a.ReqNo=c.ReqNo 
                //										AND a.Plant=c.Plant 
                //										AND a.Product=c.Product 
                //										AND a.Line=c.Line 
                //										AND a.ProductGroup=c.ProductGroup
                //		WHERE CountStatus=1
                //	--END UPDATE HEADER

                //	--BARU INSERT SEMUA YA
                //		INSERT INTO TPCS_LOT_IND_NEW 
                //		(Period, ReqNo, Plant, Product, Line, ProductGroup, Status, ChangeType, FGLotInd, FGLotIndNo, ChangeDescBefore, ChangeDescAfter, ChangeItem, FGStatus, BackgroundImprovement, EffectiveStart, EffectiveEnd, ReqQty, ApprovedBy, Attachment, SubmittedBy, SubmittedDate)
                //		SELECT DISTINCT Period, ReqNo, Plant, Product, Line, ProductGroup, Status, ChangeType, FGLotInd, FGLotIndNo, ChangeDescBefore, ChangeDescAfter, ChangeItem, FGStatus, BackgroundImprovement, EffectiveStart, EffectiveEnd, ReqQty, ApprovedBy, Attachment, SubmittedBy, SubmittedDate FROM #Data

                //		INSERT INTO TPCS_LOT_IND_DET_NEW (Period, ReqNo, Plant, Product, Line, ProductGroup, Material, MaterialDesc, Status)
                //		SELECT Period, ReqNo, Plant, Product, Line, ProductGroup, Material,Description,Status FROM #Data

                //                DROP TABLE #Data,#DataReady,#DataConflict,#DataHeaderCountStatus";

                sql = @"DECLARE 
							@Plant NVARCHAR(10)='" + _dplant + @"'
							,@Product NVARCHAR(10)='" + _dproduct + @"'
							,@ProdLine NVARCHAR(10)='" + _dline + @"'
							,@Model NVARCHAR(10)='" + _dmodel + @"'
							,@ReqNo NVARCHAR(2)='" + ReqNumber + @"'
							,@Status NVARCHAR(10)='OPEN'
							,@ChangeType NVARCHAR(50)='" + cbbChangeType.SelectedItem.ToString() + @"'
							,@FGLotInd NVARCHAR(50)='" + txtInd.Text.ToString() + @"'
							,@FGLotIndNo NVARCHAR(50)='" + txtIndInput.Text.ToString() + @"'
							,@DescBefore NVARCHAR(300)='" + txtDescBefore.Text.ToString() + @"'
							,@DescAfter NVARCHAR(300)='" + txtDescAfter.Text.ToString() + @"'
							,@ChangeItem NVARCHAR(300)='" + cbbChangeItem.SelectedItem.ToString() + @"'
							,@FGStatus NVARCHAR(300)='" + cbbFGStatus.SelectedItem.ToString() + @"'
							,@BackgroundImprovement NVARCHAR(300)='" + cbbBgImprovement.SelectedItem.ToString() + @"'
							,@EffectiveStart NVARCHAR(300)='" + PeriodStart + @"'
							,@EffectiveEnd NVARCHAR(300)='" + PeriodEnd + @"'
							,@ReqQty NVARCHAR(300)='" + txtQty.Text.ToString() + @"'
							,@ApprovedBy NVARCHAR(300)='" + txtAppr.Text.ToString() + @"'
							,@Attachment NVARCHAR(300)='" + fileAttach.ToString() + @"'
							,@SubmittedBy NVARCHAR(300)='" + UserAccount.GetuserID() + @"'
					
					SELECT Data=value 
						INTO #Data
						FROM fn_split_string('" + _materialchooice + @"', ',') WHERE ISNULL(value,'')<>''
				
						SELECT DISTINCT
							 REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 1)) AS [Line]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 2)) AS [Model]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 3)) AS [Material]
						INTO #DataOK
						FROM #Data;


					SELECT 
	                    Period=REPLACE(LEFT(CONVERT(DATE,GETDATE()),7),'-','')
	                    ,ReqNo=@ReqNo
                        ,a.Plant
                        ,a.Product
                        ,Line=a.SAPWC
                        ,ProductGroup=a.Model
                        ,b.Material
                        ,Description=b.MaterialDesc
	                    ,Status=@Status
	                    ,ChangeType=@ChangeType
	                    , FGLotInd=@FGLotInd
	                    , FGLotIndNo=@FGLotIndNo
	                    , ChangeDescBefore=@DescBefore
	                    , ChangeDescAfter=@DescAfter
	                    , ChangeItem=@ChangeItem
	                    , FGStatus=@FGStatus
	                    , BackgroundImprovement=@BackgroundImprovement
	                    , EffectiveStart=CONVERT(DATETIME, @EffectiveStart, 101)
	                    , EffectiveEnd=CONVERT(DATETIME, @EffectiveEnd, 101)
	                    , ReqQty=@ReqQty
	                    , ApprovedBy=@ApprovedBy
	                    , Attachment=@Attachment
	                    , SubmittedBy=@SubmittedBy
	                    , SubmittedDate=GETDATE()
	                    INTO #DataSubmit
                        FROM TPCS_ROUTEMP a
                        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                        JOIN #DataOK c ON c.Model=a.Model AND c.Material=b.Material AND c.Line=a.SAPWC


						---CHECK YG CONFLICT DAN UPDATE STATUS YG CONFLICT
							SELECT a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.MaterialDesc,b.Status
							,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter,a.EffectiveEnd
							INTO #DataReady
							FROM TPCS_LOT_IND_NEW a
							JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
														AND a.ReqNo=b.ReqNo 
														AND a.Product=b.Product 
														AND a.Plant=b.Plant
														AND a.Line=b.Line 
														AND a.ProductGroup=b.ProductGroup

							SELECT 
							a.Plant,
							a.Product,
							a.Line,
							a.ProductGroup,
							a.Material,
							a.Description
							INTO #DataConflict
							FROM #DataSubmit a
							JOIN #DataReady b ON a.Plant=b.Plant 
												AND a.Product=b.Product 
												AND a.Line=b.Line 
												AND a.ProductGroup=b.ProductGroup
												AND a.Material=b.Material
							WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd


					
							UPDATE a
							SET a.Status='CLOSE'
								,a.CloseBy=@SubmittedBy
								,a.CloseDate=GETDATE()
								,a.Remark='CONFLICT LOT INDICATOR NEW PERIOD '+ CONVERT(VARCHAR,REPLACE(LEFT(CONVERT(DATE,GETDATE()),7),'-',''))+'-'+@ReqNo
							FROM TPCS_LOT_IND_DET_NEW a
							JOIN #DataConflict b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.Line AND a.ProductGroup=b.ProductGroup AND a.Material=b.Material
							 WHERE
								a.Plant = @Plant
								AND
								a.Product = @Product
								AND
								a.Line LIKE @ProdLine
                                AND 
                                a.Status<>'CLOSE'
						
						--END



						----UPDATE STATUS KE CLOSE JIKA SEMUA DETAIL SUDAH CLOSE SEMUA
							SELECT a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup
							,CountStatus=COUNT(DISTINCT b.Status)
							INTO #DataHeaderCountStatus
							FROM TPCS_LOT_IND_NEW a
							JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
															AND a.ReqNo=b.ReqNo 
															AND a.Plant=b.Plant 
															AND a.Product=b.Product 
															AND a.Line=b.Line 
															AND a.ProductGroup=b.ProductGroup
							GROUP BY a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup


							UPDATE a SET a.Status=c.Status FROM TPCS_LOT_IND_NEW a
							JOIN #DataHeaderCountStatus b ON a.Period=b.Period 
															AND a.ReqNo=b.ReqNo 
															AND a.Plant=b.Plant 
															AND a.Product=b.Product 
															AND a.Line=b.Line 
															AND a.ProductGroup=b.ProductGroup
							JOIN TPCS_LOT_IND_DET_NEW c  ON a.Period=c.Period 
															AND a.ReqNo=c.ReqNo 
															AND a.Plant=c.Plant 
															AND a.Product=c.Product 
															AND a.Line=c.Line 
															AND a.ProductGroup=c.ProductGroup
							WHERE CountStatus=1
						--END UPDATE HEADER




						--BARU INSERT SEMUA YA
							INSERT INTO TPCS_LOT_IND_NEW 
							(Period, ReqNo, Plant, Product, Line, ProductGroup, Status, ChangeType, FGLotInd, FGLotIndNo, ChangeDescBefore, ChangeDescAfter, ChangeItem, FGStatus, BackgroundImprovement, EffectiveStart, EffectiveEnd, ReqQty, ApprovedBy, Attachment, SubmittedBy, SubmittedDate)
							SELECT DISTINCT Period, ReqNo, Plant, Product, Line, ProductGroup, Status, ChangeType, FGLotInd, FGLotIndNo, ChangeDescBefore, ChangeDescAfter, ChangeItem, FGStatus, BackgroundImprovement, EffectiveStart, EffectiveEnd, ReqQty, ApprovedBy, Attachment, SubmittedBy, SubmittedDate FROM #DataSubmit

							INSERT INTO TPCS_LOT_IND_DET_NEW (Period, ReqNo, Plant, Product, Line, ProductGroup, Material, MaterialDesc, Status)
							SELECT Period, ReqNo, Plant, Product, Line, ProductGroup, Material,Description,Status FROM #DataSubmit

	                  DROP TABLE #Data,#DataOK,#DataSubmit,#DataReady,#DataConflict,#DataHeaderCountStatus";
                #endregion


                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                trans.Commit();
                cmd.Dispose();
                conn.Dispose();

                result = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result = false;
                db.SaveError(ex.ToString());

            }
            return result;
        }

        void DisplayData()
        {
            
            DataTable dt = new DataTable();
            try
            {
                string _dplant, _dproduct, _dline, _dmodel;
        
                _dplant = cbbPlant.SelectedItem.ToString();
                _dproduct = cbbPrd.SelectedItem.ToString();
                _dline = cbbLine.SelectedItem.ToString();
                _dmodel = cbbModel.SelectedItem.ToString();

                if (_dline == "[ALL]")
                    _dline = "%";

                if (_dmodel == "[ALL]")
                    _dmodel = "%";
                conn = db.GetConnString();
                //sql = @"SELECT 
                //         a.Plant
                //        ,a.Product
                //        ,Line=a.SAPWC
                //        ,'ProductGrp'=a.Model
                //        ,b.Material
                //        ,'Description'=b.MaterialDesc
                //        FROM TPCS_ROUTEMP a
                //        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                //        WHERE
                //        a.Plant = '" + _dplant + @"'
                //        AND
                //       a.Product = '" + _dproduct + @"'
                //       AND
                //       a.SAPWc LIKE '" + _dline + @"'
                //       AND
                //       a.Model LIKE '" + _dmodel + @"'
                //       AND
                //        b.Material IN ("+ _materialchooice + @")
                //        ORDER BY b.Material";
                sql = @"SELECT Data=value 
						INTO #Data
						FROM fn_split_string('" + _materialchooice + @"', ',') WHERE ISNULL(value,'')<>''
				
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
                        ,'ProductGrp'=a.Model
                        ,b.Material
                        ,'Description'=b.MaterialDesc
						INTO #DataMaster
                        FROM TPCS_ROUTEMP a
                        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model

					
						SELECT b.* FROM #DataOK a
						JOIN #DataMaster b ON a.Material=b.Material AND a.Model=b.ProductGrp AND a.Line=b.Line


						DROP TABLE #Data,#DataOK,#DataMaster";

                adapter = new SqlDataAdapter(sql, conn);

                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                dgvReport.Columns["Plant"].Width = 50;
                dgvReport.Columns["Product"].Width = 80;
                dgvReport.Columns["Line"].Width = 50;
                dgvReport.Columns["ProductGrp"].Width = 80;
                dgvReport.Columns["Material"].Width = 100;
                dgvReport.Columns["Description"].Width = 250;

                if (dgvReport.Columns.Contains("delete") != true)
                {
                    DataGridViewImageColumn btnDell = new DataGridViewImageColumn();
                    btnDell.HeaderText = "Action";
                    btnDell.Name = "delete";
                    btnDell.Image = PCSSystem.Properties.Resources.del;
                    dgvReport.Columns.Add(btnDell);
                }

                lblTotalFG.Text = "Total : " + dgvReport.Rows.Count.ToString();

                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        string GetNewReqNumber()
        {
            string result = "";
            string _dplant, _dproduct;

            _dplant = cbbPlant.SelectedItem.ToString();
            _dproduct = cbbPrd.SelectedItem.ToString();

            try
            {
                conn = db.GetConnString();
                sql = @"SELECT MAX(ReqNo) FROM TPCS_LOT_IND_NEW
                        WHERE
                        Plant = '"+ _dplant + @"'
                        AND Product = '"+ _dproduct + @"'
                        AND YEAR(Period+'' + '01')= YEAR(GETDATE())";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    result = "1";
                else if (cmd.ExecuteScalar().ToString() == "")
                    result = "1";
                else
                    result = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1).ToString();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        bool CheckConflictLotInd()
        {
            bool result = true;
            try
            {

                conn = db.GetConnString();
                string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;
                string _dplant, _dproduct, _dline, _dmodel;

                _dplant = cbbPlant.SelectedItem.ToString();
                _dproduct = cbbPrd.SelectedItem.ToString();
                _dline = cbbLine.SelectedItem.ToString();
                _dmodel = cbbModel.SelectedItem.ToString();

                if (_dline == "[ALL]")
                    _dline = "%";

                if (_dmodel == "[ALL]")
                    _dmodel = "%";
                #region QUERY
                //          sql = @"SELECT 
                //                  a.Plant
                //                  ,a.Product
                //                  ,Line=a.SAPWC
                //                  ,[ProductGroup]=a.Model
                //                  ,b.Material
                //                  ,[Description]=b.MaterialDesc
                //               ,Status='OPEN'
                //               , FGLotInd='" + txtInd.Text.ToString() + @"'
                //               , FGLotIndNo='" + txtIndInput.Text.ToString() + @"'
                //               , ChangeDescAfter='" + txtDescAfter.Text.ToString() + @"'
                //               , EffectiveStart=CONVERT(DATETIME, '"+ PeriodStart + @"', 101)
                //               INTO #Data
                //                  FROM TPCS_ROUTEMP a
                //                  JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                //                  WHERE
                //                  a.Plant = '" + _dplant + @"'
                //                  AND
                //                  a.Product = '" + _dproduct + @"'
                //                  AND
                //                  a.SAPWc LIKE '" + _dline + @"'
                //                  AND
                //                  a.Model LIKE '" + _dmodel + @"'
                //                  AND
                //                  b.Material IN (" + materialchooice + @")
                //                  ORDER BY b.Material

                //SELECT a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.MaterialDesc,b.Status
                //,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter,a.EffectiveEnd
                //INTO #DataReady
                //FROM TPCS_LOT_IND_NEW a
                //JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
                //                                              AND a.ReqNo=b.ReqNo 
                //                                              AND a.Product=b.Product 
                //                                              AND a.Line=b.Line 
                //                                              AND a.ProductGroup=b.ProductGroup

                //SELECT b.Product,
                //b.Line,
                //b.ProductGroup,
                //b.Material,
                //b.MaterialDesc,
                //b.FGLotInd,
                //b.FGLotIndNo,
                //b.ChangeDescAfter,
                //a.FGLotInd,
                //a.FGLotIndNo,
                //a.ChangeDescAfter
                //FROM #Data a
                //JOIN #DataReady b ON a.Plant=b.Plant 
                //					AND a.Product=b.Product 
                //					AND a.Line=b.Line 
                //					AND a.ProductGroup=b.ProductGroup
                //					AND a.Material=b.Material
                //WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd

                //               DROP TABLE #Data,#DataReady";

                sql = @"SELECT Data=value 
						INTO #Data
						FROM fn_split_string('" + _materialchooice + @"', ',') WHERE ISNULL(value,'')<>''
				
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
                        ,'ProductGrp'=a.Model
                        ,b.Material
                        ,'Description'=b.MaterialDesc
						INTO #DataMaster
                        FROM TPCS_ROUTEMP a
                        JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model

					
						SELECT b.*
						,Status='OPEN'
	                    , FGLotInd='" + txtInd.Text.ToString() + @"'
                        , FGLotIndNo='" + txtIndInput.Text.ToString() + @"'
                        , ChangeDescAfter='" + txtDescAfter.Text.ToString() + @"'
                        , EffectiveStart=CONVERT(DATETIME, '" + PeriodStart + @"', 101)
						INTO #DataSubmit
						FROM #DataOK a
						JOIN #DataMaster b ON a.Material=b.Material AND a.Model=b.ProductGrp AND a.Line=b.Line

						SELECT a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.MaterialDesc,b.Status
						,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter,a.EffectiveEnd
						INTO #DataReady
						FROM TPCS_LOT_IND_NEW a
						JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
                                                    AND a.ReqNo=b.ReqNo 
                                                    AND a.Product=b.Product 
                                                    AND a.Line=b.Line 
                                                    AND a.ProductGroup=b.ProductGroup


						SELECT b.Product,
						b.Line,
						b.ProductGroup,
						b.Material,
						b.MaterialDesc,
						b.FGLotInd,
						b.FGLotIndNo,
						b.ChangeDescAfter,
						a.FGLotInd,
						a.FGLotIndNo,
						a.ChangeDescAfter
						FROM #DataSubmit a
						JOIN #DataReady b ON a.Plant=b.Plant 
											AND a.Product=b.Product 
											AND a.Line=b.Line 
											AND a.ProductGrp=b.ProductGroup
											AND a.Material=b.Material
						WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd

						DROP TABLE #Data,#DataOK,#DataMaster,#DataReady,#DataSubmit";

                #endregion QUERY
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    result = false;
                else if (cmd.ExecuteScalar().ToString() == "")
                    result = false;
                else
                    result = true;

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

        bool CheckUpdateSchConfirm()
        {
            bool result = true;
            try
            {

                conn = db.GetConnString();
                string PeriodStart = dpStartDate.Text + " " + dpStartTime.Text;
                string PeriodEnd = dpEndDate.Text + " " + dpEndTime.Text;
                string _dplant, _dproduct, _dline, _dmodel;

                _dplant = cbbPlant.SelectedItem.ToString();
                _dproduct = cbbPrd.SelectedItem.ToString();
                _dline = cbbLine.SelectedItem.ToString();
                _dmodel = cbbModel.SelectedItem.ToString();

                if (_dline == "[ALL]")
                    _dline = "%";

                if (_dmodel == "[ALL]")
                    _dmodel = "%";
                #region QUERY
                //sql = @"DECLARE 
                //         @Plant NVARCHAR(10)='" + _dplant + @"'
                //         ,@Product NVARCHAR(10)='" + _dproduct + @"'
                //         ,@ProdLine NVARCHAR(10)='" + _dline + @"'
                //         ,@Model NVARCHAR(10)='" + _dmodel + @"'

                //        SELECT a.SchId,a.Plant,a.Product,a.ProdnLine,b.Model,a.FGCode,a.FGName,a.SchQty,a.LotNo,a.Status
                //        INTO #DataSchedule
                //        FROM TSCHEDULE a
                //        LEFT JOIN TPCS_MAT_MODEL b ON a.Plant=b.Plant and a.FGCode=b.Material
                //        LEFT JOIN TPCS_ROUTEMP c ON a.Plant=c.Plant AND a.Product=c.Product AND a.ProdnLine=c.SAPWC AND b.Model=c.Model
                //        WHERE a.Status IN (SELECT Cod FROM CodLst WHERE GrpCod='PCS_STATUS')
                //        AND a.Plant=@Plant
                //        AND a.Product=@Product
                //        AND a.ProdnLine LIKE @ProdLine
                //        AND b.Model LIKE @Model
                //        AND a.FGCode IN (" + materialchooice + @")

                //        SELECT a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.materialDesc,b.Status,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter
                //        INTO #DataPCS
                //        FROM TPCS_LOT_IND_NEW a
                //        JOIN TPCS_LOT_IND_DET_NEW b ON 
                //        a.Period=b.Period 
                //        AND a.ReqNo=b.ReqNo 
                //        AND a.Product=b.Product 
                //        AND a.Line=b.Line 
                //        AND a.ProductGroup=b.ProductGroup
                //        WHERE a.Plant=@Plant
                //        AND a.Product=@Product
                //        AND a.Status='OPEN'
                //        AND a.Line LIKE @ProdLine
                //        AND a.ProductGroup LIKE @Model
                //        AND b.Material IN (" + materialchooice + @")

                //        SELECT 
                //        b.SchId,b.Product,b.ProdnLine,b.Model,b.FGCode,b.FGName,b.SchQty
                //        ,OldLotInd=CASE WHEN LEN(b.LotNo)=6 THEN RIGHT(b.LotNo,3) ELSE '' END
                //        ,NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo
                //        ,a.ChangeDescAfter,b.Status
                //        INTO #DataFinal
                //        FROM #DataPCS a
                //        JOIN #DataSchedule b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.ProdnLine AND a.Material=b.FGCode AND a.ProductGroup=b.Model

                //        SELECT * FROM #DataFinal

                //        DROP TABLE #DataSchedule,#DataPCS,#DataFinal";

                sql = @"SELECT Data=value 
						INTO #Data
						FROM fn_split_string('" + _materialchooice + @"', ',') WHERE ISNULL(value,'')<>''
				
						SELECT DISTINCT
							 REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 1)) AS [Line]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 2)) AS [Model]
						   , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 3)) AS [Material]
						INTO #DataOK
						FROM #Data;




                        SELECT a.SchId,a.Plant,a.Product,a.ProdnLine,b.Model,a.FGCode,a.FGName,a.SchQty,a.LotNo,a.Status
                        INTO #DataSchedule
                        FROM TSCHEDULE a
                        LEFT JOIN TPCS_MAT_MODEL b ON a.Plant=b.Plant and a.FGCode=b.Material
                        LEFT JOIN TPCS_ROUTEMP c ON a.Plant=c.Plant AND a.Product=c.Product AND a.ProdnLine=c.SAPWC AND b.Model=c.Model
						JOIN #DataOK d ON d.Model=b.Model AND d.Material=b.Material AND d.Line=a.ProdnLine
                        WHERE a.Status IN (SELECT Cod FROM CodLst WHERE GrpCod='PCS_STATUS')
                        



                        SELECT a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.materialDesc,b.Status,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter
                        INTO #DataPCS
                        FROM TPCS_LOT_IND_NEW a
                        JOIN TPCS_LOT_IND_DET_NEW b ON 
							a.Period=b.Period 
							AND a.ReqNo=b.ReqNo 
							AND a.Product=b.Product 
							AND a.Line=b.Line 
							AND a.ProductGroup=b.ProductGroup
						JOIN #DataOK c ON c.Model=a.ProductGroup AND c.Material=b.Material AND c.Line=a.Line
                        WHERE a.Status='OPEN'
                        



                        SELECT 
                        b.SchId,b.Product,b.ProdnLine,b.Model,b.FGCode,b.FGName,b.SchQty
                        ,OldLotInd=CASE WHEN LEN(b.LotNo)=6 THEN RIGHT(b.LotNo,3) ELSE '' END
                        ,NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo
                        ,a.ChangeDescAfter,b.Status
                        INTO #DataFinal
                        FROM #DataPCS a
                        JOIN #DataSchedule b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.ProdnLine AND a.Material=b.FGCode AND a.ProductGroup=b.Model

                        SELECT * FROM #DataFinal

                       DROP TABLE #Data,#DataOK,#DataSchedule,#DataPCS,#DataFinal";

                #endregion QUERY
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    result = false;
                else if (cmd.ExecuteScalar().ToString() == "")
                    result = false;
                else
                    result = true;

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
    }
}
