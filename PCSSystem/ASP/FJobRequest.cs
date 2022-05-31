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
using System.IO;
using Microsoft.ApplicationBlocks.Data;

namespace PCSSystem.ASP
{
    
    public partial class FJobRequest : Form
    {
        Common cm = new Common();
        database db = new database();
        //string sql;
        //DataSet ds;
        public DataSet DsDjobdata = new DataSet("data");
        public FJobRequest()
        {
            InitializeComponent();
        }

        private void FJobRequest_Load(object sender, EventArgs e)
        {
            lblRows2.Text = "Total Rows: 0";
            /* sql = "select insertdate as 'Date',plant as 'Plant',product as 'Product',material as 'Material',materialdesc as 'Material Des',uom as 'UOM'"+
                 ",PartCategory as 'Part Category',prcode as 'Init',subpartcategory as 'Sub Category',bufferstock as 'Buffer Stock',reqqty as 'Req QTY',mb02 as 'MB02'"+
                 ",mb03 as 'MB03',pba1 as 'PBA1',sba1 as 'SBA1',jrforwh as 'OUSTANDING JR FOR INHOUSE',jrforsubcon as 'OUSTANDING JR FOR SUBCON'" +
                 ",wmsavailableqty as 'WMS Available Stock',sortedsba as 'Shorted SBA1',safetysba1percen as 'Safety % SBA1',standardboxqty as 'Standart Box',toberequestqty as 'Qty To be Request',tobejr as 'To be JR',balancejr as 'Balance JR',jrwithoutbuffer as 'Balance JR without buffer stock' from asp_jrlog";
             LoadDataSet(sql);*/
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }
          //  LoadDataSet(dtFrom.Text, dtTo.Text, cbbPlant.Text, cbbProduct.Text, txtsearch.Text);
        }
        
        void LoadDataSet(string fromdate,string todate,string plant,string product,string txtsearch)
        {
            try
            {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@plant", plant),
                new SqlParameter("@product", product),
                new SqlParameter("@FromDate", Convert.ToDateTime(fromdate)),
                new SqlParameter("@ToDate", Convert.ToDateTime(todate)),
                 new SqlParameter("@txtsearch", txtsearch),
            };
            DsDjobdata = SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jrlog_view", sqlParams);
            dgvReport.DataSource = null;
            dgvReport.DataSource = DsDjobdata.Tables[0];
            lblRows2.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();

            //SqlDataAdapter adapter;
            //SqlConnection conn;
            //DataTable dt = new DataTable();
            //try
            //{
            //    conn = db.GetConnString();
            //    adapter = new SqlDataAdapter(sqls, conn);
            //    adapter.Fill(dt);
            //    dgvReport.DataSource = null;
            //    dgvReport.DataSource = dt;
                
            //    lblRows.Text = "Total Rows: " + dgvReport.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadDataSet(dtFrom.Text,dtTo.Text, cbbPlant.Text, cbbProduct.Text, txtsearch.Text);
            //sql = "select CAST(insertdate AS DATE) as 'Date',plant as 'Plant',product as 'Product',material as 'Material',materialdesc as 'Material Des',uom as 'UOM'" +
            //   ",PartCategory as 'Part Category',prcode as 'Init',subpartcategory as 'Sub Category',bufferstock as 'Buffer Stock',reqqty as 'Req QTY',mb02 as 'MB02'" +
            //   ",mb03 as 'MB03',pba1 as 'PBA1',sba1 as 'SBA1',jrforwh as 'OUSTANDING JR FOR INHOUSE',jrforsubcon as 'OUSTANDING JR FOR SUBCON'" +
            //   ",wmsavailableqty as 'WMS Available Stock',sortedsba as 'Shorted SBA1',safetysba1percen as 'Safety SBA1',standardboxqty as 'Standart Box',toberequestqty as 'Qty To be Request',tobejr as 'To be JR',balancejr as 'Balance JR',jrwithoutbuffer as 'Balance JR without buffer stock' from asp_jrlog"+
            //   " where (CAST(insertdate AS DATE) BETWEEN '" + this.dtFrom.Value + "' AND '" + this.dtTo.Value + "') and plant='"+cbbPlant.Text+"' and product='"+cbbProduct.Text+"'";
            
            //LoadDataSet(sql);
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

        private void button2_Click(object sender, EventArgs e)
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

                        header.Add("Master Data Sub Category");
                        //header.Add("Filter by: " + cbbFilter.SelectedItem.ToString());
                        //header.Add("Criteria: " + txtCriteria.Text.ToUpper());
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (txtsearch.Text == "" || txtsearch.Text == "%")
                //{
                //    sql = "select CAST(insertdate AS DATE) as 'Date',plant as 'Plant',product as 'Product',material as 'Material',materialdesc as 'Material Des',uom as 'UOM'" +
                //       ",PartCategory as 'Part Category',prcode as 'Init',subpartcategory as 'Sub Category',bufferstock as 'Buffer Stock',reqqty as 'Req QTY',mb02 as 'MB02'" +
                //       ",mb03 as 'MB03',pba1 as 'PBA1',sba1 as 'SBA1',jrforwh as 'OUSTANDING JR FOR INHOUSE',jrforsubcon as 'OUSTANDING JR FOR SUBCON'" +
                //       ",wmsavailableqty as 'WMS Available Stock',sortedsba as 'Shorted SBA1',safetysba1percen as 'Safety SBA1',standardboxqty as 'Standart Box',toberequestqty as 'Qty To be Request',tobejr as 'To be JR',balancejr as 'Balance JR',jrwithoutbuffer as 'Balance JR without buffer stock' from asp_jrlog" +
                //       " order by product";
                //}
                //else
                //{
                //    sql = "select CAST(insertdate AS DATE) as 'Date',plant as 'Plant',product as 'Product',material as 'Material',materialdesc as 'Material Des',uom as 'UOM'" +
                //       ",PartCategory as 'Part Category',prcode as 'Init',subpartcategory as 'Sub Category',bufferstock as 'Buffer Stock',reqqty as 'Req QTY',mb02 as 'MB02'" +
                //       ",mb03 as 'MB03',pba1 as 'PBA1',sba1 as 'SBA1',jrforwh as 'OUSTANDING JR FOR INHOUSE',jrforsubcon as 'OUSTANDING JR FOR SUBCON'" +
                //       ",wmsavailableqty as 'WMS Available Stock',sortedsba as 'Shorted SBA1',safetysba1percen as 'Safety SBA1',standardboxqty as 'Standart Box',toberequestqty as 'Qty To be Request',tobejr as 'To be JR',balancejr as 'Balance JR',jrwithoutbuffer as 'Balance JR without buffer stock' from asp_jrlog" +
                //       " where plant like '%" + txtsearch.Text + "%' or product like '%" + txtsearch.Text + "%' or material like '%" + txtsearch.Text + "%'" +
                //       " or materialdesc like '%" + txtsearch.Text + "%' or uom like '%" + txtsearch.Text + "%' or PartCategory like '%" + txtsearch.Text + "%'" +
                //       " or prcode like '%" + txtsearch.Text + "%' or subpartcategory like '%" + txtsearch.Text + "%'  order by product";
                //}
                LoadDataSet(dtFrom.Text, dtTo.Text, cbbPlant.Text, cbbProduct.Text, txtsearch.Text);
              //  LoadDataSet(sql);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
