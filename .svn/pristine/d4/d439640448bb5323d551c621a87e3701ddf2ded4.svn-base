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

    public partial class FLotNo : Form
    {
        private string myPlant, myProduct, myMat, myMatName;
        database db = new database();
        Common cm = new Common();
       // string  errortitle;
        string mac = Environment.MachineName.ToUpper();

        string sql = "";
        SqlDataAdapter adapter;
        SqlConnection conn;
        DataTable dt;
        public FLotNo()
        {
            InitializeComponent();
        }

        private void FLotNo_Load(object sender, EventArgs e)
        {
            txtPlant.Text = myPlant;
            txtProduct.Text = myProduct;
            txtMatCode.Text = myMat;
            txtMatName.Text = myMatName;
            DisplayLotNo();
        }
        
   
        public FLotNo(string _plant, string _product, string _mat, string _matname)
        {
            InitializeComponent();
            myPlant = _plant;
            myProduct = _product;
            myMat = _mat;
            myMatName = _matname;
        }

        void DisplayLotNo()
        {
            
            try
            {
                conn = db.GetConnString();
                sql = "SELECT SchId, Shift, Leader, LotNo, SchQty, Status, PackerId, PV, UserCreated, DateCreated from tschedule "+
                    " where FGCode ='"+myMat+"' and Plant='"+myPlant+"' and Product='"+
                    myProduct+"' and LEFT(SchId,4)='"+DateTime.Now.ToString("yyMM")+"' ORDER BY SchId Desc, LotNo desc";
                adapter = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                dgvReport.DataSource = dt;
                lblRows.Text = "Total Rows: "+dgvReport.Rows.Count.ToString();

                #region formatgrid
                dgvReport.Columns["Shift"].Width = 40;
                dgvReport.Columns["SchId"].Width = 80;
                dgvReport.Columns["Leader"].Width = 80;
                dgvReport.Columns["LotNo"].Width = 60;
                dgvReport.Columns["SchQty"].Width = 60;
                dgvReport.Columns["SchQty"].DefaultCellStyle.Format = "N0";
                dgvReport.Columns["Status"].Width = 80;
                dgvReport.Columns["PV"].Width = 60;
                dgvReport.Columns["DateCreated"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                #endregion

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

    }
}
