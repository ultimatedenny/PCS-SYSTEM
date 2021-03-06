using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace PCSSystem
{
    class Common
    {
        database db = new database();
        public bool Export_to_CSV(ArrayList header, string fname, DataGridView data)
        {
            bool ok = false;
            int i, j;
            string cellvalue, rowline;

            try
            {
                StreamWriter objWriter = new StreamWriter(fname, false);
                rowline = "";
                for (i = 0; i < header.Count; i++)
                {
                    objWriter.WriteLine(header[i].ToString());
                }
                for (i = 0; i < data.Columns.Count; i++)
                {

                    if (data.Columns[i].Visible)
                    {
                        cellvalue = data.Columns[i].HeaderText.ToString();
                        rowline = rowline + cellvalue + ",";
                    }
                }

                objWriter.WriteLine(rowline);
                rowline = "";

                for (j = 0; j < data.Rows.Count; j++)
                {
                    if (data.Rows[j].Visible)
                    {
                        for (i = 0; i < data.Columns.Count; i++)
                        {
                            if (data.Columns[i].Visible)
                            {
                                if (!(Convert.IsDBNull(data.Rows[j].Cells[i].Value)))
                                {
                                    cellvalue = data.Rows[j].Cells[i].Value.ToString();                                 
                                }
                                else
                                {
                                    cellvalue = "";
                                }
                                rowline = rowline + cellvalue + ",";
                            }
                        }
                        objWriter.WriteLine(rowline);
                        rowline = "";
                    }
                }
                objWriter.Close();

                if (MessageBox.Show("Export completed, Would you like to open the file?",
                    "Export to CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(fname);
                }
                ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                db.SaveError(ex.ToString());
            }
            return ok;

        }

        public bool Export_to_CSV_check(ArrayList header, string fname, DataGridView data)
        {
            bool ok = false;
            int i, j;
            string cellvalue, rowline;

            try
            {
                StreamWriter objWriter = new StreamWriter(fname, false);
                rowline = "";
                for (i = 0; i < header.Count; i++)
                {
                    objWriter.WriteLine(header[i].ToString());
                }
                for (i = 1; i < data.Columns.Count; i++)
                {

                    if (data.Columns[i].Visible)
                    {
                        cellvalue = data.Columns[i].HeaderText.ToString();
                        rowline = rowline + cellvalue + ",";
                    }
                }

                objWriter.WriteLine(rowline);
                rowline = "";

                for (j = 0; j < data.Rows.Count; j++)
                {
                    if (data.Rows[j].Visible)
                    {
                        for (i = 1; i < data.Columns.Count; i++)
                        {
                            if (data.Columns[i].Visible)
                            {
                                if (!(Convert.IsDBNull(data.Rows[j].Cells[i].Value)))
                                {
                                    cellvalue = data.Rows[j].Cells[i].Value.ToString();
                                }
                                else
                                {
                                    cellvalue = "";
                                }
                                rowline = rowline + cellvalue + ",";
                            }
                        }
                        objWriter.WriteLine(rowline);
                        rowline = "";
                    }
                }
                objWriter.Close();

                if (MessageBox.Show("Export completed, Would you like to open the file?",
                    "Export to CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(fname);
                }
                ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
                db.SaveError(ex.ToString());
            }
            return ok;

        }

        public void DataSetToExcel(DataSet dsExport, string FilePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Application.Workbooks.Add(Type.Missing);

            int shtNo = 1;

            foreach (DataTable table in dsExport.Tables)
            {
                Excel.Worksheet excelWorkSheet;
                if (excelApp.Sheets[shtNo] != null)
                {
                    excelWorkSheet = excelApp.Sheets[shtNo];
                }
                else
                {
                    excelWorkSheet = excelApp.Sheets.Add();
                }
                excelWorkSheet.Name = table.TableName;
                shtNo = shtNo + 1;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            //excelWorkBook.Save();
            //excelWorkBook.Close();
            excelApp.ActiveWorkbook.SaveCopyAs(FilePath);
            excelApp.ActiveWorkbook.Saved = true;

            //excelApp.Quit();
            if (DialogResult.Yes == MessageBox.Show("Your excel file exported successfully at " + FilePath + Environment.NewLine + "Do you wont to open file?", "Export Data-" + DateTime.Now.ToString(), MessageBoxButtons.YesNo))
            {
                if (System.IO.File.Exists(FilePath))
                {
                    System.Diagnostics.Process.Start(FilePath);
                }
            }

        }

        public bool CheckHeader(string[] fileheaders, string[] headers)
        {
            bool ok = true;
            
            string columnnames = "";
                        
            try
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    columnnames = columnnames+headers[i]+",";
                }
                columnnames = columnnames.Substring(0, columnnames.Length-1);
                
                if (headers.Length <= fileheaders.Length)
                {
                    if (headers.Length == fileheaders.Length)
                    {

                    }
                    else
                    {
                        if (MessageBox.Show("The file you imported has extra columns, Do you really want to continue?",
                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            for (int i = 0; i < headers.Length; i++)
                            {
                                if (headers[i].ToUpper().Replace("[","").Replace("]","") != fileheaders[i].ToUpper())
                                {
                                    if (MessageBox.Show("Incorrect column name! The correct column name is " +
                                        headers[i].ToUpper() + "! Do you want to continue?",
                                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    {
                                        ok = false;
                                        return ok;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ok = false;
                            return ok;
                        }
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("The file you imported missed some columns! The correct columns are " + columnnames+"!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ok = false;
                    return ok;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public string[] GetFileHeaders(string path,char PassDelimiter = '\t')
        {
            string[] result = null;
            StreamReader sr;
            string header="";
            char delimiter = '\t';
            if (PassDelimiter != '\t')
                delimiter = PassDelimiter;

            try
            {
                sr = new StreamReader(path);
                header = sr.ReadLine();
                header = header.Replace("\"","");
                if(PassDelimiter == '\t')
                    header = header.Replace(",", "");
                result = header.Split(delimiter);

                sr.Close();
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return result;
        }

        public bool Check_Authority(string formname)
        {
            bool ok = false;
            int i = 0;
            string[,] autho = UserAccount.GetAuthorization();
            try
            {
                while (!ok && i < autho.GetLength(0))
                {
                    if (autho[i, 0].ToUpper() == formname.ToUpper())
                    {
                        ok = true;
                    }
                    i++;
                }
                if (!ok)
                {
                    MessageBox.Show("You do not have the permission to access this form.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        public bool Check_Editable(string formname)
        {
            bool ok = false;
            int i = 0;
            string[,] autho = UserAccount.GetAuthorization();
           
            try
            {

                while (!ok && i < autho.GetLength(0))
                {
                    if (autho[i, 0].ToUpper() == formname.ToUpper())
                    {
                        if (autho[i, 1].ToUpper() == "TRUE")
                        {
                            ok = false;
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    i++;
                }
              
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        public void Quoting(ref string[] data)
        {
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = "'" + data[i] + "'";
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }



        //=====ASP Source =============================================
        public void ExCSVdatatable(DataTable dt, string path)
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName.ToString() + ',';
            }


            //Add new line.
            csv += "\r\n";

            //Adding the Rows

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() != null)
                    {
                        csv += dt.Rows[i][j].ToString().Replace(",", ";") + ',';
                    }
                }
                csv += "\r\n";
            }
            //Exporting to CSV.
            //string folderPath = "E:\\Data-Project\\Simano\\Dokumen\\";
            string folderPath = path;
            File.WriteAllText(folderPath, csv);
        }


        public DataTable ReadCsvFile(string path)
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;

            string FileSaveWithPath = path;
            using (StreamReader sr = new StreamReader(FileSaveWithPath))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }

        //==============================================================


    }
}
