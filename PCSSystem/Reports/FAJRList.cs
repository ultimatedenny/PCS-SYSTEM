using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace PCSSystem.Reports
{
    public partial class FAJRList : Form
    {

        bool CanClose = true;
        database db = new database();
        Common cm = new Common();
        string errorsql, errortitle;
        string mac = Environment.MachineName.ToUpper();
        string sql;
        DataSet ds;
        DataTable dtBOM = new DataTable();

        public FAJRList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            dgJobReq.DataSource = null;
            SqlDataAdapter adapter;
            SqlConnection conn;
            conn = db.GetConnString();
            //sql = string.Format("select * from AJR_Request where Plant='{0}' and Product='{1}' and PlanDate='{2}' and JobReq like '%{3}%' and PartCode like '%{4}%'",cbbPlant.Text,cbbProduct.Text,planDate.Text,tbJobReq.Text,tbPartCode.Text);
            sql = string.Format("exec AJRList_View '{0}','{1}','{2}','{3}','{4}'", cbbProduct.Text, cbbPlant.Text, planDate.Text, tbJobReq.Text, tbPartCode.Text);
            adapter = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            adapter.Fill(ds);
            dgJobReq.DataSource = ds.Tables[0];
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FAJRList_Load(object sender, EventArgs e)
        {
            planDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dgJobReq.AutoGenerateColumns = false;
            db.SetPlant(ref cbbPlant);
            if (cbbPlant.Items.Count > 0)
            {
                cbbPlant.SelectedIndex = 0;
            }

            LoadProduct();

        }

        private void cbbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        void LoadProduct()
        {
            cbbProduct.Items.Clear();
            if (cbbPlant.SelectedIndex >= 0)
            {
                db.SetProduct2(ref cbbProduct, cbbPlant.SelectedItem.ToString());
                if (cbbProduct.Items.Count > 0)
                {
                    cbbProduct.SelectedIndex = 0;
                }
            }
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            //Export to Excel
            saveFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            saveFileDialog2.Filter = "Excel 97-2003 Workbook | *.xls";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog2.FileName != "")
                {
                    if (saveFileDialog2.FileName.Contains(".xls"))
                    {
                        cm.DataSetToExcel(ds, saveFileDialog2.FileName);
                    }
                }
            }
        }
    }
}
