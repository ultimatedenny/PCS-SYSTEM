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
    public partial class FLotInd : Form
    {
        Common cm = new Common();
        database db = new database();
        
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader reader;
        SqlTransaction trans = null;
        string sql = "";
        string MacName = System.Environment.MachineName;

        bool EditMode = false;
        private string _period, _plant, _mat, _indno, _product,_model;

        public FLotInd()
        {
            InitializeComponent();
        }

        public FLotInd(string period, string plant, string mat, string indno, string product,string model)
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

        private void FLotInd_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }

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

                radAll.Enabled = false;
                radFGCode.Enabled = false;

                cbbModel.Enabled = false;
                cbbModel.SelectedItem = _model;
                               
                conn = db.GetConnString();
                sql = "SELECT AffectedChange, ChangeType,  ProbOrigin, convert(nvarchar(20),effstart,101) as 'EffStart', "+
                    " convert(nvarchar(20),EffEnd,101) as 'EffEnd', ISNULL(ReqQty,'0') as 'ReqQty', ChngDescBefore, ChngDescAfter, ApprovedBy, Status "+
                    " from tpcs_lot_ind where Period='"+_period+"' and IndNo='"+_indno+"' and Product='"+_product+"' and Plant='"+_plant+"' and Model='"+_model+"'";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    cbbAff.Enabled = false;
                    if (cbbAff.FindStringExact(reader["AffectedChange"].ToString()) >= 0)
                        cbbAff.SelectedItem = reader["AffectedChange"].ToString();
                    else
                        cbbAff.SelectedIndex = -1;

                    cbbChangeType.Enabled = false;
                    if (cbbChangeType.FindStringExact(reader["ChangeType"].ToString()) >= 0)
                        cbbChangeType.SelectedItem = reader["ChangeType"].ToString();
                    else
                        cbbChangeType.SelectedIndex = -1;


                    if (cbbProb.FindStringExact(reader["ProbOrigin"].ToString()) >= 0)
                        cbbProb.SelectedItem = reader["ProbOrigin"].ToString();
                    else
                        cbbProb.SelectedIndex = -1;

                    dtpStart.Value = DateTime.ParseExact(reader["EffStart"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    dtpEnd.Value = DateTime.ParseExact(reader["EffEnd"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    txtQty.Text = reader["ReqQty"].ToString();
                    txtDescBefore.Text = reader["ChngDescBefore"].ToString();
                    txtDescAft.Text = reader["ChngDescAfter"].ToString();
                    txtAppr.Text = reader["ApprovedBy"].ToString();
                    cbbStatus.SelectedItem = "OPEN";

                }
                reader.Close();

                conn = db.GetConnString();
                sql = "SELECT Material " +
                   " from tpcs_lot_ind_det where Period='" + _period + "' and IndNo='" + _indno + "' and Product='" + _product + "' and Plant='" + _plant + "' and Model='" + _model + "'";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fg.Add(reader[0].ToString());
                }
                reader.Close();

                cbbFGCode.Items.AddRange(fg.ToArray());
                cbbFGCode.SelectedIndex = 0;
                cbbFGCode.Enabled = false;
                lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();

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
                conn = db.GetConnString();
                sql= "SELECT AffectedChange from TPCS_AFFCHNG";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbAff.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                sql = "SELECT ChangeType from TPCS_CHNGTYP";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbChangeType.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();

                sql = "SELECT ProbOrigin from TPCS_PROBORG";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                cbbProb.Items.AddRange(result.ToArray());
                result.Clear();
                reader.Close();
                conn.Dispose();

                if (!EditMode)
                {
                    cbbStatus.Enabled = false;
                    cbbStatus.SelectedItem = "OPEN";
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
            try
            {
                if (cbbPrd.SelectedIndex >= 0)
                {
                    db.SetModel(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbPrd.SelectedItem.ToString());
                    if (cbbModel.Items.Count > 0)
                    {
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
            try
            {
                if (cbbModel.SelectedIndex >= 0)
                {
                    //db.SetFG(ref cbbFGCode,cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                    //if (cbbFGCode.Items.Count > 0)
                    //{
                    //    cbbFGCode.Items.Insert(0, "[ALL]");
                    //    cbbFGCode.SelectedIndex = 0;
                    //}
                    cbbFGCode.Items.Clear();
                    txtFGDesc.Text = "";
                    lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
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
                if (cbbProb.SelectedIndex >= 0)
                    if (cbbProb.SelectedItem.ToString() == "OTHERS")
                    {
                        cbbProb.Visible = false;
                        txtOth.Visible = true;
                        txtOth.Focus();
                    }
                    else
                    {                               
                        txtOth.Visible = false;
                    }
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
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cbbProb.Items.Insert(0, txtOth.Text);
                    cbbProb.SelectedIndex = 0;
                    cbbProb.Visible = true;
                    txtOth.Visible = false;
                    txtOth.Text = "";
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radAll.Checked)
                {
                    if (cbbModel.SelectedIndex >= 0)
                    {
                        db.SetFG(ref cbbFGCode, cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                        if (cbbFGCode.Items.Count > 0)
                        {
                            cbbFGCode.SelectedIndex = 0;
                        }
                        lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
                    }
                }
                else
                {
                    cbbFGCode.Items.Clear();
                    txtFGDesc.Text = "";
                }
                
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

                    if (result == "A")
                    {
                        dtpStart.Enabled = true;
                        dtpEnd.Enabled = true;
                        txtQty.Enabled = false;
                    }
                    else
                    {
                        dtpStart.Enabled = false;
                        dtpEnd.Enabled = false;
                        txtQty.Enabled = true;
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

        private void radFGCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (radFGCode.Checked)
                {
                    if (cbbModel.SelectedIndex >= 0)
                    {
                        FSelFG f;
                        f = new FSelFG(cbbPlant.SelectedItem.ToString(), cbbModel.SelectedItem.ToString());
                        f.ShowDialog();
                        if (f.selectedfg.Count > 0)
                        {
                            cbbFGCode.Items.Clear();
                            cbbFGCode.Items.AddRange(f.selectedfg.ToArray());
                        }
                        f.Dispose();
                        lblTotalFG.Text = "Total: " + cbbFGCode.Items.Count.ToString();
                    }
                }
                else
                {
                    cbbFGCode.Items.Clear();
                    txtFGDesc.Text = "";
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
                if (IsDataValid())
                {
                    if (EditMode)
                    {
                        if (SaveRecord(EditMode))
                        {
                            MessageBox.Show("The PV Indicator has been edited!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else
                    {                    
                        if (SaveRecord())
                        {
                            MessageBox.Show("The PV Indicator has been saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearControl();
                        }
                    }
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
                radAll.Checked = false;
                radFGCode.Checked = false;
                cbbFGCode.Items.Clear();
                lblTotalFG.Text = "Total: 0";
                txtFGDesc.Text = "";
                cbbAff.SelectedIndex = -1;
                cbbChangeType.SelectedIndex = -1;
                txtInd.Text = "";
                cbbProb.SelectedIndex = -1;
                txtDescAft.Text = "";
                txtDescBefore.Text = "";
                txtAppr.Text = "";
                txtQty.Text = "0";
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
                if (cbbPlant.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbPrd.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Product!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbModel.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Model!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbFGCode.Items.Count <= 0)
                {
                    MessageBox.Show("Please select the FG!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbAff.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Affected Change!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (cbbChangeType.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the Change Type!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    return result;
                }

                if (txtInd.Text == "A")
                {
                    if (cbbProb.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select the Problem Origin!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                        return result;
                    }

                    if (dtpStart.Value > dtpEnd.Value)
                    {
                        MessageBox.Show("Effective Start must be before Effective End Date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                        return result;
                    }
                }
                else
                {
                    if (Convert.ToInt32(txtQty.Text) <= 0)
                    {
                        MessageBox.Show("Req Qty must be more than 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                        return result;
                    }
                }

                if (txtAppr.Text.Length == 0)
                {
                    MessageBox.Show("Approved by must be filled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    result = false;
                    txtAppr.Focus();
                    return result;
                }

                if (!EditMode)
                {
                    for (int i = 0; i < cbbFGCode.Items.Count; i++)
                    {
                        mats = mats + "'" + cbbFGCode.Items[i].ToString() + "',";
                    }

                    mats = mats.Substring(0, mats.Length - 1);
                    conn = db.GetConnString();
                    sql = "SELECT Material from tpcs_lot_ind_det where Plant = '" + cbbPlant.SelectedItem.ToString() + "' AND " +
                        " Material in (" + mats + ") AND Status='OPEN' AND LEFT(IndNo,1)='" + txtInd.Text + "' AND Model = '"+cbbModel.SelectedItem.ToString()+"'";
                    cmd = new SqlCommand(sql, conn);
                    reader = cmd.ExecuteReader();
                    mats = "";
                    while (reader.Read())
                    {
                        mats = mats + reader[0].ToString() + ",";
                    }

                    if (mats.Length > 0)
                    {
                        mats = mats.Substring(0, mats.Length - 1);
                        MessageBox.Show("These materials (" + mats + ") are still OPEN as " + txtInd.Text +
                            " indicator!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        string GetNewIndicator()
        {
            string result = "";
            
            try
            {
                conn = db.GetConnString();
                sql = "SELECT MAX(IndNo) from TPCS_LOT_IND WHERE ChangeType='"+cbbChangeType.SelectedItem.ToString()+
                    "' AND Product='"+cbbPrd.SelectedItem.ToString()+"' AND DatePart(yy,ReqDate)=DatePart(yy,GETDATE()) AND Model='"+cbbModel.SelectedItem.ToString()+"'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    result = txtInd.Text + "01";
                else if (cmd.ExecuteScalar().ToString() == "")
                    result = txtInd.Text + "01";
                else
                    result = txtInd.Text + (Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(1)) + 1).ToString("00");


                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        bool SaveRecord()
        {
            bool result = false;
            string indno="";
            try
            {
                indno = GetNewIndicator();
                conn = db.GetConnString();
                
                sql = "INSERT INTO TPCS_LOT_IND " +
                   " (" +
                   " [Period],[IndNo],[Plant],[Product],[Model],[AffectedChange],[ChangeType], [ReqDate],[ProbOrigin],[EffStart],[EffEnd],[ReqQty] " +
                   " ,[ChngDescBefore],[ChngDescAfter],[ApprovedBy],[Status],[UpdateBy],[UpdateDate],[MacName] ) " +
                   " VALUES " +
                   "("+
                   "'" + DateTime.Today.ToString("yyMM") + "', " +
                   "'"+indno+"', "+
                   "'" + cbbPlant.SelectedItem.ToString() + "', " +
                   "'"+cbbPrd.SelectedItem.ToString()+"', "+
                   "'"+cbbModel.SelectedItem.ToString()+"', "+
                   "'"+cbbAff.SelectedItem.ToString()+"', "+
                   "'"+cbbChangeType.SelectedItem.ToString()+"', "+
                   "GETDATE(), "+
                   "'"+cbbProb.SelectedItem.ToString()+"', "+
                   "'"+dtpStart.Value.ToString("yyyy-MM-dd")+"', "+
                   "'"+dtpEnd.Value.ToString("yyyy-MM-dd")+"', "+
                   "'"+txtQty.Text+"', "+
                   "'"+txtDescBefore.Text+"', "+
                   "'"+txtDescAft.Text+"', "+
                   "'"+txtAppr.Text+"', "+
                   "'OPEN',"+
                   "'"+UserAccount.GetuserID()+"', "+
                   "GETDATE(), "+
                   "'"+MacName+"' "+
                   ")";

                trans = conn.BeginTransaction();
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO TPCS_LOT_IND_DET(Period, IndNo, Plant,Product,Model,Material,Status,OutputQty) VALUES ";
                for (int i = 0; i < cbbFGCode.Items.Count; i++)
                {
                    sql = sql+"('" + DateTime.Today.ToString("yyMM") + "', '"+indno+"','"+cbbPlant.SelectedItem.ToString()+"','"+cbbPrd.SelectedItem.ToString()+"',"+
                        " '"+cbbModel.SelectedItem.ToString()+"',";
                    sql = sql+"'"+cbbFGCode.Items[i].ToString()+"','OPEN','0'),";
                }

                if (sql.Length > 0)
                {
                    sql = sql.Substring(0, sql.Length - 1);
                }

                cmd.CommandText = sql;
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



        bool SaveRecord(bool mode)
        {
            bool result = false;            
            try
            {
                
                conn = db.GetConnString();

                sql = "UPDATE TPCS_LOT_IND SET " +
                   " ProbOrigin = '" + cbbProb.SelectedItem.ToString() + "', " +
                   " EffStart='" + dtpStart.Value.ToString("yyyy-MM-dd") + "', " +
                   " EffEnd='" + dtpEnd.Value.ToString("yyyy-MM-dd") + "', " +
                   " ReqQty='" + txtQty.Text + "', " +
                   " ChngDescBefore='" + txtDescBefore.Text + "', " +
                   " ChngDescAfter='" + txtDescAft.Text + "', " +
                   " ApprovedBy='" + txtAppr.Text + "', " +
                   " Status='" + cbbStatus.SelectedItem.ToString() + "', " +
                   " UpdateBy='" + UserAccount.GetuserID() + "', " +
                   " UpdateDate=GETDATE(), " +
                   " MacName='" + MacName + "' " +
                   " where Period='"+_period+"' AND IndNo = '"+_indno+"' and Product='"+_product+"'";

                trans = conn.BeginTransaction();
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                if (cbbStatus.SelectedItem.ToString() == "CLOSED"){

                    sql = "UPDATE TPCS_LOT_IND_DET set Status='CLOSED' WHERE " +
                    "Plant='"+cbbPlant.SelectedItem.ToString()+"' AND Period='" + _period + "' and IndNO='" + _indno + "' AND Product='" + _product + "' AND Model='"+cbbModel.SelectedItem.ToString()+"'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                

                //sql = "UPDATE TPCS_LOT_IND set Status='CLOSED' "+
                //    " where "+
                //    " (SELECT COUNT(Material) from TPCS_LOT_IND_DET where "+
                //    " Period='"+_period+"' and IndNo='"+_indno+"' AND Product='"+_product+"' AND Model='"+cbbModel.SelectedItem.ToString()+"' AND Status='OPEN') = '0'";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

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


    }
}
