using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace PCSSystem
{
    public partial class jr_upload : Form
    {

        database db = new database();
        Common cm = new Common();
        //string errorsql, errortitle;
        string mac = Environment.MachineName.ToUpper();

        public jr_upload()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        bool IsProductLocked(string prdt)
        {
            SqlConnection conn;
            SqlCommand cmd;
            string sql = "";
            bool locked = false;
            try
            {
                conn = db.GetConnString();                
                sql = "SELECT Product from tpcs_lockProduct where Plant='" + cbbPlant.SelectedItem.ToString() + "' AND Product='" + prdt + "'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                {
                    locked = false;
                }
                else if (cmd.ExecuteScalar().ToString() == "")
                {
                    locked = false;
                }
                else
                {
                    locked = true;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return locked;
        }

        private string GetRunningDLPMac()
        {
            SqlConnection conn;
            SqlCommand cmd;
            string sql = "";
            string result = "";
            SqlDataReader reader = null;
            string lockedby="", lockeddate="", lockedmac="";
            try
            {
                conn = db.GetConnString();

                
                sql = "SELECT LockedBy, LockedDate, LockedMac from TPCS_LOCKPRODUCT "+
                    " where Plant='" + cbbPlant.SelectedItem.ToString() + "' AND Product='" + cbbProduct.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lockedby = reader["LockedBy"].ToString();
                    lockeddate = reader["LockedDate"].ToString();
                    lockedmac = reader["LockedMac"].ToString();
                }

                if (! (lockedby == ""))
                {
                    MessageBox.Show("You cannot run this because "+lockedby.ToUpper() + " is running the "+cbbProduct.SelectedItem.ToString()+ 
                        " DLP at "+lockedmac + " on "+lockeddate+"! Please choose other product to run."
                    ,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                result = "";
            }
            result = lockedby + "|" + lockeddate + "|" + lockedmac;
            return result;
        }

        private void btnUpl34_Click(object sender, EventArgs e)
        {
            try
            {
                if(btmMB52.Enabled == true)
                {
                    MessageBox.Show("Please upload MB52 first!");
                    return;
                }

                if (!IsProductLocked(cbbProduct.SelectedItem.ToString()))
                {
                    if (cm.Check_Authority("FSAPSch"))
                    {
                        FSAPSch f = new FSAPSch(cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
                        f.ShowDialog();
                        if (f.IsUploaded() && !f.IsError())
                        {
                            if (ProductLocked())
                            {
                                OpenDailyLoadPlan();
                                lblPP34Rows.Visible = true;
                                btnUplBF.Enabled = true;
                            }
                        }
                        f.Dispose();
                    }
                }
                else
                {
                    GetRunningDLPMac();
                }


            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        private bool ProductLocked()
        {
            bool ok = false;
            string sql = "";
            SqlCommand cmd;
            SqlConnection conn;
            bool locked = false;
            try
            {
                conn = db.GetConnString();
                sql = "SELECT Product from tpcs_lockproduct where "+
                    " Plant='"+cbbPlant.SelectedItem.ToString()+"' AND Product='"+cbbProduct.SelectedItem.ToString()+
                    "' AND LockedMac='"+System.Environment.MachineName+"'";
                cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteScalar() == null)
                    locked = false;
                else if (cmd.ExecuteScalar().ToString() == "")
                {
                    locked = false;
                }
                else
                {
                    locked = true;
                }

                if (!locked)
                {
                    sql = "INSERT INTO tpcs_lockproduct (Plant, Product, LockedBy,LockedDate, LockedMac) " +
                        " VALUES " +
                        " (" +
                        "'" + cbbPlant.SelectedItem.ToString() + "'," +
                        "'" + cbbProduct.SelectedItem.ToString() + "'," +
                        "'" + UserAccount.GetuserID() + "'," +
                        "GetDate()," +
                        "'" + System.Environment.MachineName + "'" +
                        ")";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
                else
                {
                    ok = true;
                }

            }
            catch (Exception ex)
            {
                ok = false;
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        private void btnUplBF_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FTobeBF"))
            {
                FTobeBF f = new FTobeBF();
                f.ShowDialog();
                if (f.IsUploaded())
                {
                    OpenDailyLoadPlan();                 
                }
                f.Dispose();
            }
        }

        void OpenDailyLoadPlan()
        {
            try
            {
                if (cm.Check_Authority("FDailyPlan"))
                {
                    FDailyPlan f = new FDailyPlan(cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString()) ;
                    f.MdiParent = this.MdiParent;
                    f.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void FUplData_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);
            PVIndicatorUpdate();  //disabled 
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }

        }
        

        void CheckGlobal()
        {
            SqlDataAdapter adapter;
            SqlConnection conn;
            SqlCommand cmd;
            DataTable dtGlob = new DataTable();
            string sql = "";
            try
            {
                conn = db.GetConnString();
                sql = "select GlobalValue,convert(varchar,getdate(),112) CurDate from TGLOBAL where GlobalSetting = 'PCS_MB52_" + cbbProduct.Text + "'";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dtGlob);
                if (dtGlob.Rows.Count > 0)
                {
                    if(dtGlob.Rows[0][0].ToString() != dtGlob.Rows[0][1].ToString())
                    {
                        btmMB52.Enabled = true;
                        lblStatusMB52.Visible = false;
                    }
                    else
                    {
                        btmMB52.Enabled = false;
                        lblStatusMB52.Visible = true;
                    }
                }
                else
                {
                    sql = @"insert into TGLOBAL (GlobalSetting,GlobalValue,UpdateBy,UpdateOn)
                             Values ('PCS_MB52_" + cbbProduct.Text + "','','" + UserAccount.GetuserName() + "',getdate())";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    btmMB52.Enabled = true;
                    lblStatusMB52.Visible = false;
                }


            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }
        void PVIndicatorUpdate()
        {
            
            SqlCommand cmd;
            SqlConnection conn;
            SqlTransaction trans = null;
            string sql = "";
            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                
                sql = "UPDATE TPCS_LOT_IND set Status='CLOSED' WHERE " +
                    " CONVERT(nvarchar(12),GetDate(),112) > CONVERT(nvarchar(12),EffEnd,112) and EffStart is not null";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_LOT_IND_DET set Status='CLOSED' from tpcs_lot_ind t1 inner join tpcs_lot_ind_det t2 "+
                    " ON t1.Period=t2.Period and t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Indno=t2.IndNo where t1.Status = 'CLOSED'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
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
            string sql = "",result="";
            SqlCommand cmd;
            SqlConnection conn;
            try
            {
                conn = db.GetConnString();
                sql = "SELECT MAX(UpdateDate) from TPCS_SAPSCHD where LEFT(MRPC,2) = '" + cbbProduct.SelectedItem.ToString() +
                    "' AND Plant='" + cbbPlant.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(sql, conn);

                result = cmd.ExecuteScalar().ToString();

                if (result.Length > 0)
                {
                    lblPP34Update.Text = Convert.ToDateTime(result).ToString("yyyy-MM-dd HH:mm") + " updated!";
                    lblPP34Update.Refresh();
                    chkPP34.Enabled = true;
                }

                else
                {
                    chkPP34.Enabled = false;
                    lblPP34Update.Text = "";
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

            if(!string.IsNullOrEmpty(cbbProduct.Text) && !string.IsNullOrEmpty(cbbPlant.Text))
            {
                CheckGlobal();
            }
            
        }

        private void chkPP34_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPP34.Checked)
            {
                btnUpl34.Enabled = false;
                if (MessageBox.Show("Are you sure to use the PP34 and B/F that uploaded on " +
                    lblPP34Update.Text.Substring(0, 16) + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    OpenDailyLoadPlan();
                }
                else
                {
                    btnUpl34.Enabled = true;
                    chkPP34.Checked = false;
                }


                
            }
            else
            {
                btnUpl34.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPSch"))
            {
                string path = "";
                string fileheaders, tableheaders;
                try
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        path = openFileDialog1.FileName;

                        StreamReader sr;
                        string header = "";

                        sr = new StreamReader(path);
                        sr.ReadLine();// skip first line
                        header = sr.ReadLine();
                        header = header.Replace("\"", "");
                        header = header.Replace(",", "");
                        header = header.Replace(" ", "");
                        fileheaders = header;
                        sr.Close();

                        tableheaders = db.GetGlobal("HEADER_MB52");

                        if(fileheaders == tableheaders)
                        {
                            Import_Data(path, tableheaders);
                        }
                        else
                        {
                            MessageBox.Show("File header not match!");
                            return;
                        }
                        //if (cm.CheckHeader(fileheaders, tableheaders))
                        //{
                        //    errortitle = "";
                        //    errorsql = "";
                        //    if (Import_Data(path, tableheaders))
                        //    {
                        //        txtStatus.Text = "Validating data...";
                        //        if (Validating_Data())
                        //        {
                        //            txtStatus.Text = "Saving...";
                        //            if (InsertIntoTable())
                        //            {
                        //                uploaded = true;
                        //                this.Close();
                        //            }

                        //        }
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    db.SaveError(ex.ToString());
                }


            }
        }

        void Import_Data(string path, string tableheaders)
        {
            bool ok = false;
            string sql = "";
            StreamReader sr = null;
            SqlCommand cmd;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            string line = "";
            string[] lines = null;
            char delimiter = '|';

            try
            {
                conn = db.GetConnString();
                trans = conn.BeginTransaction();

                sql = @"DELETE FROM TPCS_MB52 WHERE Product='" + cbbProduct.Text + "' and Plant='" + cbbPlant.Text + @"' and convert(date,PostDate)=convert(date,getdate())
                        UPDATE TGLOBAL set GlobalValue = convert(varchar, getdate(), 112) where GlobalSetting = 'PCS_MB52_" + cbbProduct.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sr = new StreamReader(path);
                sr.ReadLine(); //----
                sr.ReadLine(); //header
                sr.ReadLine(); //----

                DataTable dtMB52 = new DataTable("tMB52");
                dtMB52.Columns.Add("Plant", typeof(string));
                dtMB52.Columns.Add("Product", typeof(string));
                dtMB52.Columns.Add("SAPCode", typeof(string));
                dtMB52.Columns.Add("Description", typeof(string));
                dtMB52.Columns.Add("SLoc", typeof(string));
                dtMB52.Columns.Add("UR", typeof(Double));
                dtMB52.Columns.Add("QI", typeof(Double));
                dtMB52.Columns.Add("Block", typeof(Double));
                dtMB52.Columns.Add("TotalStock", typeof(Double));
                dtMB52.Columns.Add("BalanceStock", typeof(Double));
                dtMB52.Columns.Add("PostBy", typeof(string));
                dtMB52.Columns.Add("PostDate", typeof(DateTime));

                //sqlval = @"insert into TPCS_MB52_TMP (Plant,Product,SAPCode,Description,SLoc,UR,QI,Block) values ";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    line = line.Replace("\"", "");
                    line = line.Replace(",", "");
                    if(line.Substring(0,1)!="-")
                    {
                        lines = line.Split(delimiter);
                        if(lines[1].ToString().Replace(" ","") == "2300")
                        {
                            DataRow drMB52 = dtMB52.NewRow();
                            drMB52["Plant"] = lines[1].ToString().Replace(" ", "");
                            drMB52["Product"] = cbbProduct.Text;
                            drMB52["SAPCode"] = lines[4].ToString().Replace(" ", "");
                            drMB52["Description"] = lines[5].ToString();
                            drMB52["SLoc"] = lines[6].ToString().Replace(" ", "");
                            drMB52["UR"] = lines[8].ToString().Replace(" ", "");
                            drMB52["QI"] = lines[10].ToString().Replace(" ", "");
                            drMB52["Block"] = lines[12].ToString().Replace(" ", "");
                            drMB52["TotalStock"] = lines[8].ToString().Replace(" ", "");
                            drMB52["BalanceStock"] = lines[8].ToString().Replace(" ", "");
                            drMB52["PostBy"] = UserAccount.GetuserName();
                            drMB52["PostDate"] = DateTime.Now;

                            dtMB52.Rows.Add(drMB52);
                            //sqlval = sqlval + @" 
                            //('" + lines[1].ToString().Replace(" ", "") + "','" + cbbProduct.Text + "','" + lines[4].ToString().Replace(" ", "") + "','" + lines[5] + "','" + lines[6].ToString().Replace(" ", "") + "'," + lines[8].ToString().Replace(" ", "") + "," + lines[10].ToString().Replace(" ", "") + "," + lines[12].ToString().Replace(" ", "") + ") ,";
                        }
                    }
                
                }
                //sqlval = sqlval.Substring(0, sqlval.Length-1);

                using (var sbc = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans))
                {
                    sbc.DestinationTableName = "TPCS_MB52";
                    sbc.WriteToServer(dtMB52);
                    trans.Commit();

                    btmMB52.Enabled = false;
                    lblStatusMB52.Visible = true;
                }

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

        }


    }
}
