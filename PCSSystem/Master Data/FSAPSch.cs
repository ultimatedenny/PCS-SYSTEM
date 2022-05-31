using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
namespace PCSSystem
{
    public partial class FSAPSch : Form
    {
        Common cm = new Common();
        database db = new database();
        string errorsql, errortitle;
        Boolean uploaded = false;
        Boolean error = false;
      //  string productuploaded="";
        string myPlant="";
        string myProduct="";
        bool exportonly = false;
        public FSAPSch()
        {
            InitializeComponent();
        }

        public FSAPSch(string exp)
        {
            InitializeComponent();
            exportonly = true;
        }

        public FSAPSch(string plant, string product)
        {
            InitializeComponent();
            myPlant = plant;
            myProduct = product;

        }

        private void FSAPSch_Load(object sender, EventArgs e)
        {
        
            GetFilter();
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.Items.Insert(0, "[ALL]");
                cbbPlant.SelectedIndex=0;
            }
            if (exportonly)
            {
                btnImport.Visible = false;
            }
        }

        

        void GetFilter()
        {
            string cri = "";
            try
            {
                cri = db.GetGlobal("SAPSCHDFILTER");
                cbbFilter.Items.AddRange(cri.Split('|'));
                if (cbbFilter.Items.Count > 0)
                {
                    cbbFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string items = "";
            if (cbbFilter.SelectedIndex >= 0)
            {
                items = cbbFilter.SelectedItem.ToString().ToUpper();
                if (items == "DATE")
                {
                    dtpFrom.Visible = true;
                    dtpTo.Visible = true;
                    txtCriteria.Visible = false;
                    lblAll.Visible = false;
                }
                else
                {
                    dtpFrom.Visible = false;
                    dtpTo.Visible = false;
                    txtCriteria.Visible = true;
                    lblAll.Visible = true;
                }
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

                if (dgvReport.Rows.Count > 0)
                {
                    saveFileDialog1.Filter = "CSV File|*.csv";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        header.Add("Master Data: SAP Schedule");
                        header.Add("Plant: " + cbbPlant.SelectedItem.ToString());
                        header.Add("Filter by: " + cbbFilter.SelectedItem.ToString());                        
                        header.Add("Criteria: " + txtCriteria.Text.ToUpper());
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

        private void txtCriteria_TextChanged(object sender, EventArgs e)
        {
            DisplayData();    
        }
        void DisplayData()
        {
            string sql = "";
            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            string cri = "", field = "";
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
              
                if (cbbFilter.SelectedIndex >= 0)
                {
                    field = cbbFilter.SelectedItem.ToString().ToUpper();
                    
                    cri = "'%"+txtCriteria.Text+"%'";
                    
                    if (field == "DATE")
                    {
                        field = " (RIGHT(BscStart,4)+SUBSTRING(BscStart,4,2)+LEFT(BscStart,2)) >= ";
                        cri = "'" + dtpFrom.Value.ToString("yyyyMMdd") + "' AND (RIGHT(BscStart,4)+SUBSTRING(BscStart,4,2)+LEFT(BscStart,2)) <= '" +
                            dtpTo.Value.ToString("yyyyMMdd")+"'";
                    }else if (field == "PRODUCT"){
                       field = "LEFT(MRPC,2) LIKE ";
                    }
                    else if (field == "MAT DESCRIPTION")
                    {
                        field = "MaterialDesc LIKE ";
                    }
                    else
                    {
                        field = field.Replace(" ", "") + " LIKE ";
                    }


                    if (cbbPlant.SelectedIndex > 0)
                    {
                        cri = cri + " and Plant like '" + cbbPlant.SelectedItem.ToString() + "'";
                    }
                    else if (cbbPlant.SelectedIndex == 0)
                    {
                        cri = cri + " and Plant like '%'";
                    }
                    

                    sql = "SELECT *, RIGHT(BscStart,4)+SUBSTRING(BscStart,4,2)+LEFT(BscStart,2) as 'StartDate', " +
                  " RIGHT(BscFin,4)+SUBSTRING(BscFin,4,2)+LEFT(BscFin,2) as 'FinDate' into #temp1 " +
                  " FROM TPCS_SAPSCHD " +
                  " WHERE " + field + cri +
                  " SELECT Plant, BscStart, BscFin,SchedStart,SchedFin, MRPC, ProdnLine, MaterialDesc, Material, TargetQty,UM, OrderId,Version, " +
                  " UpdateBy, UpdateDate FROM #temp1 " +
                  " DROP TABLE #temp1 ";
                    
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;
                    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

                    #region formatgrid
                    dgvReport.Columns["Plant"].Width = 50;
                    dgvReport.Columns["BscStart"].Width = 80;
                    dgvReport.Columns["BscFin"].Width = 80;
                    dgvReport.Columns["MRPC"].Width = 40;
                    dgvReport.Columns["ProdnLine"].Width = 40;
                    dgvReport.Columns["MaterialDesc"].Width = 120;
                    dgvReport.Columns["Material"].Width = 100;
                    dgvReport.Columns["TargetQty"].Width = 40;
                    dgvReport.Columns["TargetQty"].HeaderText= "Qty";
                    dgvReport.Columns["UM"].Width = 30;
                    dgvReport.Columns["OrderId"].Width = 50;
                    dgvReport.Columns["Version"].Width = 40;
                    dgvReport.Columns["Version"].HeaderText= "PV";                    
                    dgvReport.Columns["UpdateDate"].Width = 100;
                    dgvReport.Columns["UpdateDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                    #endregion

                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btnImport_Click(object sender, EventArgs e)
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
                    temp = db.GetGlobal("HEADER_SAPSCHD");

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
                                txtStatus.Text = "Saving...";
                                if (InsertIntoTable())
                                {
                                    uploaded = true;
                                    this.Close();
                                }
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                uploaded = false;
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
                    sql = "DELETE t1 FROM TPCS_SAPSCHD t1 INNER JOIN TPCS_SAPSCHD_TEMP t2 on " +
                        " t1.Plant=t2.Plant AND t1.MRPC=t2.MRPC ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }
                sql = "INSERT INTO TPCS_SAPSCHD (Plant, BscStart,BscFin,SchedStart,SchedFin,MRPC,ProdnLine,MaterialDesc,Material,TargetQty, "+
                    "UM, OrderId, Version, UpdateBy, UpdateDate) " +
                        " SELECT Plant, BscStart,BscFin,SchedStart,SchedFin,MRPC,ProdnLine,MaterialDesc,Material,TargetQty, " +
                        "UM, OrderId, Version,  '" + UserAccount.GetuserID().ToUpper() + "', GETDATE() from TPCS_SAPSCHD_TEMP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update tpcs_sapschd set model = t1.model " +
                    " from tpcs_mat_model t1 inner join tpcs_sapschd t2 " +
                    " on t1.Material = t2.Material and t1.Plant=t2.Plant";
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
                error = true;
            }
            finally
            {
                conn.Dispose();
            }
            return ok;
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

                sql = "DELETE FROM TPCS_SAPSCHD_TEMP";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
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
                        sqlval = sqlval + lines[i].ToString().Replace(",","") + ",";
                    }
                    
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);
                    sqlval = sqlval + "),";
                    rows++;
                    counts++;

                    txtStatus.Text = "Uploading: " + counts.ToString() + " rows";
                    if (rows == 100)
                    {
                        sqlval = sqlval.Substring(0, sqlval.Length - 1);
                        sql = "INSERT INTO TPCS_SAPSCHD_TEMP (Plant, BscStart,BscFin, SchedStart, SchedFin, MRPC, ProdnLine, MaterialDesc, Material, TargetQty, UM, OrderId, Version) " +
                            " VALUES " +
                            sqlval;
                        //MessageBox.Show(sql);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        rows = 0;
                        sqlval = "";
                    }
                }

                if (rows > 0)
                {
                    sqlval = sqlval.Substring(0, sqlval.Length - 1);

                    sql = "INSERT INTO TPCS_SAPSCHD_TEMP (Plant, BscStart,BscFin, SchedStart, SchedFin, MRPC, ProdnLine, MaterialDesc, Material, TargetQty, UM, OrderId, Version) " +
                        " VALUES " +
                        sqlval;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    rows = 0;
                    sqlval = "";
                }

                sql = "UPDATE TPCS_SAPSCHD_TEMP SET ProdnLine=RTRIM(LTRIM(ProdnLine))";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                
                sql = "UPDATE TPCS_SAPSCHD_TEMP SET ProdnLine=LEFT(ProdnLine,2)+RIGHT('0'+RIGHT(ProdnLine,LEN(ProdnLine)-2),2)";
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
                sql = "SELECT DISTINCT(Plant) from TPCS_SAPSCHD_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                cmd = new SqlCommand(sql, conn);

                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Plant!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Plant! View Error.";
                    errorsql = "SELECT * from TPCS_SAPSCHD_TEMP t1 " +
                    " WHERE t1.Plant not in (SELECT Plant from TPLANT)";
                    errortitle = "Import SAP Schedule-Invalid Plants";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT t1.Plant, LEFT(MRPC,2) as 'Product' from TPCS_SAPSCHD_TEMP t1 WHERE " +
                        " NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(t1.MRPC,'DY','DH'),2)=t2.Product)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid MRPC!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid MRPC! View Error.";
                    errorsql = "SELECT * from TPCS_SAPSCHD_TEMP t1 " +
                   " WHERE NOT EXISTS (SELECT * from TPRODUCT t2 WHERE t1.Plant=t2.Plant and LEFT(REPLACE(t1.MRPC,'DY','DH'),2)=t2.Product)";
                    errortitle = "Import SAP Schedule-Invalid MRPC";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                sql = "SELECT DISTINCT Material from TPCS_SAPSCHD_TEMP t1 WHERE " +
                       " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Material=t2.Material)";
                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Invalid Materials!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Invalid Materials! View Error.";
                    errorsql = "SELECT * from TPCS_SAPSCHD_TEMP t1 WHERE " +
                   " NOT EXISTS (SELECT * from TPCS_MAT_MODEL t2 WHERE t1.Material=t2.Material)";
                    errortitle = "Import SAP Schedule-Invalid Materials";
                    FInfo f = new FInfo(errortitle, errorsql);
                    f.ShowDialog();
                    f.Dispose();
                    return ok;
                }

                if ((myProduct.Length > 0) && (myPlant.Length > 0))
                {
                    sql = "SELECT Plant from TPCS_SAPSCHD_TEMP t1 WHERE " +
                       " t1.Plant='" + myPlant + "' and LEFT(REPLACE(MRPC,'DY','DH'),2)= '" + myProduct + "'";
                    cmd.CommandText = sql;
                    //Console.WriteLine(sql);
                    if ((cmd.ExecuteScalar() == null))
                    {
                        MessageBox.Show(myProduct + " product not found!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ok = false;
                        txtStatus.Text = "Product not found! View Error.";
                        errorsql = "SELECT * from TPCS_SAPSCHD_TEMP t1 ";
                        errortitle = "Import SAP Schedule-Product not found";
                        FInfo f = new FInfo(errortitle, errorsql);
                        f.ShowDialog();
                        f.Dispose();
                        return ok;
                    }
                }

                sql="SELECT DISTINCT t1.Plant, Prodnline, Model from TPCS_SAPSCHD_TEMP t1 "+
                " left join tpcs_mat_model t3 on t1.Plant=t3.Plant and t1.Material=t3.Material "+
                " WHERE "+
                " NOT EXISTS (SELECT * from TPCS_ROUTEMP t2 WHERE t1.Plant=t2.Plant and "+
                " LEFT(REPLACE(RTRIM(LTRIM(t1.mrpc)),'DY','DH'),2)=t2.Product "+
                " and t3.Model=t2.Model and t1.Prodnline=t2.SAPWC)";

                cmd.CommandText = sql;
                if (!(cmd.ExecuteScalar() == null))
                {
                    MessageBox.Show("Routing and Manpower not found!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    txtStatus.Text = "Routing and Manpower not found! View Detail.";
                    errorsql = sql;
                    errortitle = "Import SAP Schedule-Routing and Manpower not found";
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

        private void txtStatus_Click(object sender, EventArgs e)
        {
            if (!(errortitle == ""))
            {
                FInfo f = new FInfo(errortitle, errorsql);
                f.ShowDialog();
                f.Dispose();
            }

        }

        public Boolean IsUploaded()
        {
            return uploaded;
        }

        public Boolean IsError()
        {
            return error;
        }
     
    }
}
