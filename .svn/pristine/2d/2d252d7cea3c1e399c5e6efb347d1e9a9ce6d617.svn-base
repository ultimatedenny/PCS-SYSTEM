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
    public partial class FLotConflictInd : Form
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

        string _matNew = "";

        private string _plant, _product, _line, _model,_lotInd, _lotIndInput, _Desc, _PeriodStart;

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
   
                    row.Cells["OldLotInd"].Style.BackColor = Color.Red;
                    row.Cells["OldLotInd"].Style.ForeColor = Color.White;
                
                    row.Cells["NewLotInd"].Style.BackColor = Color.Green;
                    row.Cells["NewLotInd"].Style.ForeColor = Color.White;
                

            }
        }

        string appPath = Properties.Settings.Default.AppPath.ToUpper();

        public FLotConflictInd()
        {
            InitializeComponent();
        }

        public FLotConflictInd(string plant, string product,string line, string model, string lotInd, string lotIndInput, string Desc, string PeriodStart)
        {
            InitializeComponent();

            _plant = plant;
            _product = product;
            _line = line;
            _model = model;
            _lotInd = lotInd;
            _lotIndInput = lotIndInput;
            _Desc = Desc;
            _PeriodStart = PeriodStart;

            if (_line == "[ALL]")
                _line = "%";

            if (_model == "[ALL]")
                _model = "%";
        }

        private void FLotConflictInd_Load(object sender, EventArgs e)
        {
            _matNew = FNewLotInd.materialchooice;
            DisplayData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        void DisplayData()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = db.GetConnString();
          //      sql = @"SELECT 
          //                  a.Plant
          //                  ,a.Product
          //                  ,Line=a.SAPWC
          //                  ,[ProductGroup]=a.Model
          //                  ,b.Material
          //                  ,[Description]=b.MaterialDesc
          //                  ,Status='OPEN'
          //                  , FGLotInd='" + _lotInd + @"'
          //                  , FGLotIndNo='" + _lotIndInput + @"'
          //                  , ChangeDescAfter='" + _Desc + @"'
          //                  , EffectiveStart=CONVERT(DATETIME, '" + _PeriodStart + @"', 101)
          //                  INTO #Data
          //                  FROM TPCS_ROUTEMP a
          //                  JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
          //                  WHERE
          //                  a.Plant = '" + _plant + @"'
          //                  AND
          //                  a.Product = '" + _product + @"'
          //                  AND
          //                  a.SAPWc LIKE '" + _line + @"'
          //                  AND
          //                  a.Model LIKE '" + _model + @"'
          //                  AND
          //                  b.Material IN (" + _matNew + @")
          //                  ORDER BY b.Material

          //                  SELECT a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.MaterialDesc,b.Status
          //                  ,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter,a.EffectiveEnd
          //                  INTO #DataReady
          //                  FROM TPCS_LOT_IND_NEW a
          //                  JOIN TPCS_LOT_IND_DET_NEW b ON a.Period=b.Period 
          //                                          AND a.ReqNo=b.ReqNo 
          //                                          AND a.Product=b.Product 
          //                                          AND a.Line=b.Line 
          //                                          AND a.ProductGroup=b.ProductGroup

          //                  SELECT b.Product,
          //                  b.Line,
          //                  b.ProductGroup,
          //                  b.Material,
          //                  b.MaterialDesc,
          //                  OldLotInd=b.FGLotInd+'0'+b.FGLotIndNo,
						    //OldDesc=b.ChangeDescAfter,
						    //NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo,
          //                  NewDesc=a.ChangeDescAfter
          //                  FROM #Data a
          //                  JOIN #DataReady b ON a.Plant=b.Plant 
          //      					            AND a.Product=b.Product 
          //      					            AND a.Line=b.Line 
          //      					            AND a.ProductGroup=b.ProductGroup
          //      					            AND a.Material=b.Material
          //                  WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd

          //                     DROP TABLE #Data,#DataReady"; 
                
                sql = @"SELECT Data=value 
	                    INTO #Data
	                    FROM fn_split_string('" + _matNew + @"', ',') WHERE ISNULL(value,'')<>''
				
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
                    ,[ProductGroup]=a.Model
                    ,b.Material
                    ,[Description]=b.MaterialDesc
                    ,Status='OPEN'
                    , FGLotInd='" + _lotInd + @"'
                    , FGLotIndNo='" + _lotIndInput + @"'
                    , ChangeDescAfter='" + _Desc + @"'
                    , EffectiveStart=CONVERT(DATETIME, '" + _PeriodStart + @"', 101)
                    INTO #DataMaster
                    FROM TPCS_ROUTEMP a
                    JOIN TPCS_MAT_MODEL b ON a.Plant = b.Plant AND a.Model = b.Model
                    JOIN #DataOK c ON b.Material=c.Material AND a.Model=c.Model AND a.SAPWC=c.Line

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
                    OldLotInd=b.FGLotInd+'0'+b.FGLotIndNo,
                    'Old Lot Ind Change Desc After'=b.ChangeDescAfter,
                    NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo,
                    'New Old Ind Change Desc After'=a.ChangeDescAfter
                    FROM #DataMaster a
                    JOIN #DataReady b ON a.Plant=b.Plant 
                	                    AND a.Product=b.Product 
                	                    AND a.Line=b.Line 
                	                    AND a.ProductGroup=b.ProductGroup
                	                    AND a.Material=b.Material
                    WHERE b.Status='OPEN' AND a.EffectiveStart <= b.EffectiveEnd

                    DROP TABLE #Data,#DataOK,#DataMaster,#DataReady";

                adapter = new SqlDataAdapter(sql, conn);

                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                dgvReport.Columns["Product"].Width = 80;
                dgvReport.Columns["Line"].Width = 50;
                dgvReport.Columns["ProductGroup"].Width = 100;
                dgvReport.Columns["Material"].Width = 100;
                dgvReport.Columns["MaterialDesc"].Width = 150;
                dgvReport.Columns["OldLotInd"].Width = 100;
                dgvReport.Columns["Old Lot Ind Change Desc After"].Width = 250;
                dgvReport.Columns["NewLotInd"].Width = 100;
                dgvReport.Columns["New Old Ind Change Desc After"].Width = 150;


                lblTotalFG.Text = "Total : " + dgvReport.Rows.Count.ToString();

                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

    }
}
