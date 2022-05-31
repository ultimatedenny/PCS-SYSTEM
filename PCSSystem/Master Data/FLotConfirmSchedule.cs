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
    public partial class FLotConfirmSchedule : Form
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
        bool _checked = false;
        string _matNew = "";
        public ArrayList selectedfg = new ArrayList();
        public static string schchooice = "";

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnSelectedAll.Text == "Clear Selection")
            {
                btnSelectedAll.Text = "Select All";
                _checked = false;
            }
            else
            {
                btnSelectedAll.Text = "Clear Selection";
                _checked = true;
            }

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkCol"] as DataGridViewCheckBoxCell);
                checkBox.Value = _checked;
            }
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                    if (e.ColumnIndex == 0)
                        dgvReport.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgvReport.Rows[e.RowIndex].Cells[0].Value);

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        public FLotConfirmSchedule()
        {
            InitializeComponent();
        }

        public FLotConfirmSchedule(string plant, string product,string line, string model)
        {
            InitializeComponent();

            _plant = plant;
            _product = product;
            _line = line;
            _model = model;

            if (_line == "[ALL]")
                _line = "%";

            if (_model == "[ALL]")
                _model = "%";
        }

        private void FLotConfirmSchedule_Load(object sender, EventArgs e)
        {
            _matNew = FNewLotInd.materialchooice;
            _checked = false;
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
                bool success=false;
                for (int i = 0; i < dgvReport.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvReport.Rows[i].Cells["ChkCol"].Value))
                    {
                        selectedfg.Add(dgvReport.Rows[i].Cells["SchId"].Value.ToString());
                    }
                }

                if (selectedfg.Count > 0)
                {
                    schchooice = "";
                    for (int i = 0; i < selectedfg.Count; i++)
                    {
                        schchooice = schchooice + "'" + selectedfg[i].ToString() + "',";
                    }

                    if (schchooice.Length > 0)
                    {
                        schchooice = schchooice.Substring(0, schchooice.Length - 1);
                        if (UpdateSchedule())
                            success = true;
                    }
                    else
                    {
                        MessageBox.Show("You haven't selected any Schedule!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (success == true)
                        this.Close();
                }
                else
                {
                    this.Close();
                    //MessageBox.Show("You haven't selected any FG!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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
                sql = @"SELECT Data=value 
	                        INTO #Data
	                        FROM fn_split_string('" + _matNew + @"', ',') WHERE ISNULL(value,'')<>''
				
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
                            b.SchId,a.Plant,b.Product,b.ProdnLine,b.Model,b.FGCode,b.FGName,b.SchQty
                            ,OldLotInd=CASE WHEN LEN(b.LotNo)=6 THEN RIGHT(b.LotNo,3) ELSE '' END
                            ,NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo
                            ,a.ChangeDescAfter,b.Status
                            INTO #DataFinal
                            FROM #DataPCS a
                            JOIN #DataSchedule b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.ProdnLine AND a.Material=b.FGCode AND a.ProductGroup=b.Model

                            SELECT * FROM #DataFinal

                            DROP TABLE #Data,#DataOK,#DataSchedule,#DataPCS,#DataFinal";

                adapter = new SqlDataAdapter(sql, conn);

                adapter.Fill(dt);
                dgvReport.DataSource = dt;

                dgvReport.Columns["ChkCol"].Width = 20;
                //dgvReport.Columns["Line"].Width = 50;
                //dgvReport.Columns["ProductGroup"].Width = 100;
                //dgvReport.Columns["Material"].Width = 100;
                //dgvReport.Columns["MaterialDesc"].Width = 150;
                //dgvReport.Columns["OldLotInd"].Width = 100;
                //dgvReport.Columns["OldDesc"].Width = 250;
                //dgvReport.Columns["NewLotInd"].Width = 100;
                //dgvReport.Columns["NewDesc"].Width = 150;


                lblTotalFG.Text = "Total : " + dgvReport.Rows.Count.ToString();

                conn.Dispose();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        bool UpdateSchedule()
        {
            bool result = true;
            try
            {
                conn = db.GetConnString();
                #region QUERY
                //          sql = @"DECLARE 
                //                   @Plant NVARCHAR(10)='" + _plant + @"'
                //                   ,@Product NVARCHAR(10)='" + _product + @"'
                //                   ,@ProdLine NVARCHAR(10)='" + _line + @"'
                //                   ,@Model NVARCHAR(10)='" + _model + @"'

                //                  SELECT a.SchId,a.Plant,a.Product,a.ProdnLine,b.Model,a.FGCode,a.FGName,a.SchQty,a.LotNo,a.Status
                //                  INTO #DataSchedule
                //                  FROM TSCHEDULE a
                //                  LEFT JOIN TPCS_MAT_MODEL b ON a.Plant=b.Plant and a.FGCode=b.Material
                //                  LEFT JOIN TPCS_ROUTEMP c ON a.Plant=c.Plant AND a.Product=c.Product AND a.ProdnLine=c.SAPWC AND b.Model=c.Model
                //                  WHERE a.Status IN (SELECT Cod FROM CodLst WHERE GrpCod='PCS_STATUS')
                //                  AND a.Plant=@Plant
                //                  AND a.Product=@Product
                //                  AND a.ProdnLine LIKE @ProdLine
                //                  AND b.Model LIKE @Model
                //                  AND a.SchId IN (" + schchooice + @")

                //                  SELECT a.Period,a.ReqNo,a.Plant,a.Product,a.Line,a.ProductGroup,b.Material,b.materialDesc,b.Status,a.FGLotInd,a.FGLotIndNo,a.ChangeDescAfter
                //                  INTO #DataPCS
                //                  FROM TPCS_LOT_IND_NEW a
                //                  JOIN TPCS_LOT_IND_DET_NEW b ON 
                //                  a.Period=b.Period 
                //                  AND a.ReqNo=b.ReqNo 
                //                  AND a.Product=b.Product 
                //                  AND a.Line=b.Line 
                //                  AND a.ProductGroup=b.ProductGroup
                //                  WHERE a.Plant=@Plant
                //                  AND a.Product=@Product
                //                  AND a.Status='OPEN'
                //                  AND a.Line LIKE @ProdLine
                //                  AND a.ProductGroup LIKE @Model
                //                  AND b.Material IN (" + _matNew + @")

                //                  SELECT 
                //                  b.SchId
                //                  ,NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo
                //                  INTO #DataFinal
                //                  FROM #DataPCS a
                //                  JOIN #DataSchedule b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.ProdnLine AND a.Material=b.FGCode AND a.ProductGroup=b.Model

                //                  UPDATE b SET
                //b.LotNo=LEFT(b.LotNo,3)+''+ a.NewLotInd
                //FROM #DataFinal a
                //JOIN TSCHEDULE b ON a.SchId=b.SchId

                //                  DROP TABLE #DataSchedule,#DataPCS,#DataFinal";
                sql = @"SELECT a.SchId,a.Plant,a.Product,a.ProdnLine,b.Model,a.FGCode,a.FGName,a.SchQty,a.LotNo,a.Status
                        INTO #DataSchedule
                        FROM TSCHEDULE a
                        LEFT JOIN TPCS_MAT_MODEL b ON a.Plant=b.Plant and a.FGCode=b.Material
                        LEFT JOIN TPCS_ROUTEMP c ON a.Plant=c.Plant AND a.Product=c.Product AND a.ProdnLine=c.SAPWC AND b.Model=c.Model
                        WHERE a.Status IN (SELECT Cod FROM CodLst WHERE GrpCod='PCS_STATUS')
                        AND a.SchId IN (" + schchooice + @")


                        SELECT Data=value 
                        INTO #Data
                        FROM fn_split_string('" + _matNew + @"', ',') WHERE ISNULL(value,'')<>''
				
                        SELECT DISTINCT
		                        REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 1)) AS [Line]
	                        , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 2)) AS [Model]
	                        , REVERSE(PARSENAME(REPLACE(REVERSE(Data), '|', '.'), 3)) AS [Material]
                        INTO #DataOK
                        FROM #Data;

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
                        b.SchId
                        ,NewLotInd=a.FGLotInd+'0'+a.FGLotIndNo
                        INTO #DataFinal
                        FROM #DataPCS a
                        JOIN #DataSchedule b ON a.Plant=b.Plant AND a.Product=b.Product AND a.Line=b.ProdnLine AND a.Material=b.FGCode AND a.ProductGroup=b.Model

                        UPDATE b SET
                        b.LotNo=LEFT(b.LotNo,3)+''+ a.NewLotInd
                        FROM #DataFinal a
                        JOIN TSCHEDULE b ON a.SchId=b.SchId

                        DROP TABLE #Data,#DataOK,#DataSchedule,#DataPCS,#DataFinal";
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
    }
}
