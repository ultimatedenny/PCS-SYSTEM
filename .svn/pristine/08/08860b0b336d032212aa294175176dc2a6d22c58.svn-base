using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PCSSystem
{
    public partial class FIndList : Form
    {
        Common cm = new Common();
        database db = new database();

        SqlCommand cmd;
        SqlConnection conn;
        //SqlDataReader reader;
        SqlTransaction trans = null;
        SqlDataAdapter adapter;
        string sql = "";
        string MacName = System.Environment.MachineName;
        bool DetailMode = false;

        public FIndList()
        {
            InitializeComponent();
        }


        private void FIndList_Load(object sender, EventArgs e)
        {
            db.SetPlant(ref cbbPlant);

            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
            cbbStatus.SelectedIndex = 0;
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
                    db.SetModel(ref cbbModel, cbbPlant.SelectedItem.ToString(), cbbProduct.SelectedItem.ToString());
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

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayData();
                DetailMode = false;
                btnEdit.Text = "&Edit";
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayData()
        {
            string plant, product, model, fgcode, fgdesc,status;
            DataTable dt = new DataTable();
            try
            {
                plant = "'%'";
                if (cbbPlant.SelectedIndex >= 0)
                    plant = "'"+cbbPlant.SelectedItem.ToString()+"'";
                product = "'%'";
                if (cbbProduct.SelectedIndex >= 0)
                    product= "'" + cbbProduct.SelectedItem.ToString() + "'";
                model = "'%'";
                if (cbbModel.SelectedIndex >= 0)
                    model = "'" + cbbModel.SelectedItem.ToString() + "'";
                fgcode = "'%"+txtFGCode.Text+"%'";
                fgdesc= "'%" + txtFGName.Text + "%'";
                status = "'%'";
                if (cbbStatus.SelectedIndex > 0)
                    status = "'" + cbbStatus.SelectedItem.ToString() + "'";

                conn = db.GetConnString();

                sql = " select DISTINCT t2.Period,t2.IndNo,t2.Plant,t1.Product,t1.Model,AffectedChange, " +
                    " ChangeType,ReqDate,ProbOrigin,EffStart,EffEnd,ReqQty, " +
                    " ChngDescBefore,ChngDescAfter,ApprovedBy,t1.Status " +
                    " from TPCS_LOT_IND t1 inner join TPCS_LOT_IND_DET t2  ON " +
                    " t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Model=t2.Model and t1.IndNo=t2.IndNo and t1.Period=t2.Period " +
                    " left join tpcs_mat_model t3 on " +
                    " t2.plant = t3.plant and t2.Material=t3.material " +
                    " WHERE t2.Plant like " + plant +
                    " AND t1.Product like " + product +
                    " AND t1.Model like " + model +
                    " AND t2.Material like " + fgcode +
                    " AND MaterialDesc like " + fgdesc +
                    " AND t1.Status like " + status +
                    " order by Plant, Period, IndNo, t1.Product, Model";


                //sql = " select t2.Period,t2.IndNo,t2.Plant,t1.Product,t1.Model,t2.Material,MaterialDesc,AffectedChange, " +
                //    " ChangeType,ReqDate,ProbOrigin,EffStart,EffEnd,ReqQty,OutputQty, " +
                //    " ChngDescBefore,ChngDescAfter,ApprovedBy,t2.Status " +
                //    " from TPCS_LOT_IND t1 inner join TPCS_LOT_IND_DET t2  ON " +
                //    " t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Model=t2.Model and t1.IndNo=t2.IndNo and t1.Period=t2.Period "+
                //    " left join tpcs_mat_model t3 on " +
                //    " t2.plant = t3.plant and t2.Material=t3.material " +                    
                //    " WHERE t2.Plant like " + plant +
                //    " AND t1.Product like " + product +
                //    " AND t1.Model like " + model +
                //    " AND t2.Material like " + fgcode+
                //    " AND MaterialDesc like " + fgdesc+
                //    " AND t2.Status like " + status +
                //    " order by Period, IndNo,t1.Product, t2.Material";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                dgvReport.Columns["Period"].Width = 50;
                dgvReport.Columns["Product"].Width = 30;
                dgvReport.Columns["IndNo"].Width = 50;
                dgvReport.Columns["ReqQty"].Width = 60;
                dgvReport.Columns["ReqQty"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["Plant"].Width = 40;
                dgvReport.Columns["EffStart"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvReport.Columns["EffEnd"].DefaultCellStyle.Format = "yyyy-MM-dd";

                lblRows.Text = "Total Rows: "+dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string period,plant,mat,indno,product,model;
            try
            {

                if (!DetailMode)
                {
                    if (dgvReport.SelectedRows[0].Cells["Status"].Value.ToString() == "OPEN")
                    {
                        period = dgvReport.SelectedRows[0].Cells["Period"].Value.ToString();
                        plant = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
                        indno = dgvReport.SelectedRows[0].Cells["IndNo"].Value.ToString();
                        product = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
                        model = dgvReport.SelectedRows[0].Cells["Model"].Value.ToString();

                        if (DetailMode)
                            mat = dgvReport.SelectedRows[0].Cells["Material"].Value.ToString();
                        else
                            mat = "";

                        if (cm.Check_Authority("flotind"))
                        {
                            FLotInd f = new FLotInd(period, plant, mat, indno, product, model);
                            f.ShowDialog();
                            f.BringToFront();
                        }
                        DisplayData();
                    }
                }
                else
                {
                    
                    if (dgvReport.SelectedRows[0].Cells["Status"].Value.ToString() == "OPEN")
                    {
                        if (MessageBox.Show("Do you really want to close this Material's Indicator?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CloseIndMat();
                            dgvReport.SelectedRows[0].Cells["Status"].Value = "CLOSED";
                        }                        
                    }                        
                    
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void CloseIndMat()
        {
            try
            {
               string period,indno,plant, product, model, mat;
               DataTable dt = new DataTable();

               period = dgvReport.SelectedRows[0].Cells["Period"].Value.ToString();
               plant = dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString();
               indno = dgvReport.SelectedRows[0].Cells["IndNo"].Value.ToString();
               product = dgvReport.SelectedRows[0].Cells["Product"].Value.ToString();
               model = dgvReport.SelectedRows[0].Cells["Model"].Value.ToString();
               mat = dgvReport.SelectedRows[0].Cells["Material"].Value.ToString();

                conn = db.GetConnString();
                trans = conn.BeginTransaction();
                sql = "UPDATE TPCS_LOT_IND_DET SET Status='CLOSED' where Period='"+period+
                    "' AND Plant='"+plant+"' and IndNo='"+indno+"' AND Product='"+product+"' AND Model='"+model+"' AND Material='"+mat+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "UPDATE TPCS_LOT_IND set Status='CLOSED' " +
                    " where " +
                    " (SELECT COUNT(Material) from TPCS_LOT_IND_DET where " +
                    " Plant = '"+plant+"' and Period='" + period + "' and IndNo='" + indno + "' AND Product='" + product+ "' AND Model='" + model + "' AND Status='OPEN') = '0'";
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

        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dgvReport.SelectedRows.Count > 0) && (! DetailMode))
                {
                    DisplayDataDetail();
                    DetailMode = true;
                    btnEdit.Text = "CLOSED Ind";
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayDataDetail()
        {
            string indno, period,plant, product, model, fgcode, fgdesc, status;
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
                                
                plant = "'" + dgvReport.SelectedRows[0].Cells["Plant"].Value.ToString() + "'";
                product = "'" + dgvReport.SelectedRows[0].Cells["Product"].Value.ToString() + "'";
                model = "'" + dgvReport.SelectedRows[0].Cells["Model"].Value.ToString() + "'";
                indno = "'" + dgvReport.SelectedRows[0].Cells["IndNo"].Value.ToString() + "'";
                period= "'" + dgvReport.SelectedRows[0].Cells["Period"].Value.ToString() + "'";
                fgcode = "'%" + txtFGCode.Text + "%'";
                fgdesc = "'%" + txtFGName.Text + "%'";
                status = "'%'";
                if (cbbStatus.SelectedIndex > 0)
                    status = "'" + cbbStatus.SelectedItem.ToString() + "'";

                sql = " select t2.Period,t2.IndNo,t2.Plant,t1.Product,t1.Model,t2.Material, MaterialDesc, " +
                    " ReqQty, OutputQty, " +
                    " t2.Status " +
                    " from TPCS_LOT_IND t1 inner join TPCS_LOT_IND_DET t2  ON " +
                    " t1.Plant=t2.Plant and t1.Product=t2.Product and t1.Model=t2.Model and t1.IndNo=t2.IndNo and t1.Period=t2.Period " +
                    " left join tpcs_mat_model t3 on " +
                    " t2.plant = t3.plant and t2.Material=t3.material " +
                    " WHERE t1.Period like " + period +
                    " AND t1.Plant like " + plant +
                    " AND t1.Product like " + product +
                    " AND t1.Model like " + model +
                    " AND t1.IndNo like " + indno +
                    " AND t2.Material like " + fgcode +
                    " AND MaterialDesc like " + fgdesc +
                    " AND t2.Status like " + status +
                    " order by Plant, Period, IndNo, t1.Product, Model";

                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                dgvReport.Columns["Period"].Width = 50;
                dgvReport.Columns["Product"].Width = 30;
                dgvReport.Columns["IndNo"].Width = 50;
                dgvReport.Columns["ReqQty"].Width = 60;
                dgvReport.Columns["ReqQty"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["Plant"].Width = 40;
                dgvReport.Columns["OutputQty"].Width = 60;
                dgvReport.Columns["OutputQty"].DefaultCellStyle.Format = "N0";
                

                lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

    }
}
