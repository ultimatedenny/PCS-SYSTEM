using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PCSSystem.ASP
{
    class MyFunction
    {
        public static string Asp_lock(string ipaddress,string status, string plant, string product,string postby)
        {
            SqlParameter[] sqlparams = {
                new SqlParameter("@plant",plant),
                new SqlParameter("@product",product),
                new SqlParameter("@status",status),
                new SqlParameter("@ipaddress",ipaddress),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
                                        };
            sqlparams[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_lock_update", sqlparams);
            return sqlparams[5].Value.ToString();
        }

        public static string Asp_tmppp57_delete(string plant, string product)
        {
            SqlParameter[] sqlparams = {
                new SqlParameter("@plant",plant),
                new SqlParameter("@product",product),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
                                        };
            sqlparams[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_tmppp57_delete", sqlparams);
            return sqlparams[2].Value.ToString();
        }

        public static string Asp_jr(string ipaddress, string plant, string product,string filename, string postby)
        {
            DataSet DsData;
            DsData = new DataSet();
            SqlParameter[] sqlparams = 
            {
                new SqlParameter("@plant",plant),
                new SqlParameter("@product",product),
                new SqlParameter("@ipaddress",ipaddress),
                new SqlParameter("@filename",filename),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
            };
            sqlparams[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jr_update", sqlparams);
            using (SqlConnection Conn = new SqlConnection(MyGlobal.dbConn))
            {
                Conn.Open();
                using (SqlCommand sCommand = new SqlCommand("asp_jr_update", Conn))
                {
                    sCommand.Parameters.AddRange(sqlparams);
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.CommandTimeout = Convert.ToInt32(Properties.Settings.Default.ConTime);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sCommand
                    };
                    da.Fill(DsData);
                }
                Conn.Close();
            }
            return sqlparams[5].Value.ToString();
        }

        public static string Asp_jr_Manual(string ipaddress, string plant, string product, string filename, string postby)
        {
            DataSet DsData;
            DsData = new DataSet();
            SqlParameter[] sqlparams =
            {
                new SqlParameter("@plant",plant),
                new SqlParameter("@product",product),
                new SqlParameter("@ipaddress",ipaddress),
                new SqlParameter("@filename",filename),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
            };
            sqlparams[5].Direction = ParameterDirection.Output;
            //SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jr_update_MANUAL", sqlparams);
            using (SqlConnection Conn = new SqlConnection(MyGlobal.dbConn))
            {
                Conn.Open();
                using (SqlCommand sCommand = new SqlCommand("asp_jr_update_MANUAL", Conn))
                {
                    sCommand.Parameters.AddRange(sqlparams);
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.CommandTimeout = Convert.ToInt32(Properties.Settings.Default.ConTime);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sCommand
                    };
                    da.Fill(DsData);
                }
                Conn.Close();
            }
            return "SUCCESS";
        }

        public static string Asp_jrlog(string strid,string intmaxrow,string postby)
        {
            SqlParameter[] sqlparams = 
            {
                new SqlParameter("@strid",strid),
                new SqlParameter("@maxrow",intmaxrow),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
            };
            sqlparams[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jrlog_insert", sqlparams);
            return sqlparams[3].Value.ToString();
        }

        public static string Asp_jr_csv_export(string strplant,string strproduct, string strtargetfile, string postby)
        {
            SqlParameter[] sqlparams = {
                new SqlParameter("@plant",strplant),
                new SqlParameter("@product",strproduct),
                new SqlParameter("@targetfile",strtargetfile),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
            };
            sqlparams[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jr_csv_export", sqlparams);
            return sqlparams[4].Value.ToString();
        }

        public static string Asp_jrlog_delete(string plant, string product)
        {
            SqlParameter[] sqlparams = {
                new SqlParameter("@plant",plant),
                new SqlParameter("@product",product),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
                                        };
            sqlparams[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_jrlog_delete", sqlparams);
            return sqlparams[2].Value.ToString();
        }

        public static string Asp_sendemail(string strplant, string strproduct, string strtargetfile, string postby)
        {
            SqlParameter[] sqlparams = 
            {
                new SqlParameter("@plant",strplant),
                new SqlParameter("@product",strproduct),
                new SqlParameter("@targetfile",strtargetfile),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
            };
            sqlparams[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_sendemail", sqlparams);
            return sqlparams[4].Value.ToString();
        }

        public static string Asp_autoreserve()
        {
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "ASP_JR_AUTO_RESERVE", null);
            return "Success";
        }

        public static string Asp_mmaterialexlusion_insert(string strplant,string strmaterial,string strproduct, string strreason, string postby)
        {
            SqlParameter[] sqlparams = {
                new SqlParameter("@plant",strplant),
                new SqlParameter("@material",strmaterial),
                new SqlParameter("@product",strproduct),
                new SqlParameter("@reason",strreason),
                new SqlParameter("@postby",postby),
                new SqlParameter("@spmsg", SqlDbType.VarChar,100)
                                        };
            sqlparams[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.StoredProcedure, "asp_mmaterialexlusion_insert", sqlparams);
            return sqlparams[5].Value.ToString();
        }
    }
}
