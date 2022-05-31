using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace PCSSystem
{    
    class database
    {
        private SqlConnection Conn;
        private string mac = System.Environment.MachineName;
        public SqlConnection GetConnString()
        {
            SqlConnection conn=null;
            try
            {
                conn = new SqlConnection(Properties.Settings.Default.ConnString);
                conn.Open();
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }

            return conn;
        }

        private void OpenSqlConnection()
        {
            try
            {
                Conn = new SqlConnection(Properties.Settings.Default.ConnString);
                Conn.Open();
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }
        }

        public bool SetPlant(ref ComboBox cbb)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result=new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Plant from TPLANT";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Plant"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetPlant2(ref ComboBox cbb)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Plant from TPLANT";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                result.Add("All");
                while (reader.Read())
                {
                    result.Add(reader["Plant"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public bool SetInitialCategory(ref ComboBox cbb)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT category from asp_partcategoryinit order by category";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["category"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetProduct(ref ComboBox cbb, string plant)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Product from TPRODUCT WHERE Plant='"+plant+"'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Product"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetMaterialProduct(ref ComboBox cbb, string material)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT distinct(Product) as Product from [TMATERIAL] WHERE Material='" + material + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Product"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public bool SetProduct2(ref ComboBox cbb, string plant)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Product from TPRODUCT WHERE Plant='" + plant + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                result.Add("[All]");
                while (reader.Read())
                {
                    result.Add(reader["Product"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetProduct3(ref ComboBox cbb, string plant)
        {
            bool ok = false;
            //SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                DataSet DsSelect;
                string Sql = "Exec asp_TPRoduct_view '" + plant + "'";
                DsSelect = SqlHelper.ExecuteDataset(MyGlobal.dbConn, CommandType.Text, Sql);
                foreach (DataRow dr in DsSelect.Tables[0].Rows)
                {
                    result.Add(dr["product"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public bool SetMaterial(ref ComboBox cbb, string Product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Material from TMaterial WHERE Product='" + Product + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Material"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetMaterial2(ref ComboBox cbb, string Product, string Plant)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Material from TMaterial WHERE Product='" + Product + "' and Plant='" + Plant+"'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Material"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetMaterial3(ref ComboBox cbb, string Plant)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT Material from TMaterial WHERE Plant like '" + Plant + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Material"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetMaterialName(ref Label cbb, string Product, string Material, string Plant)
        {
            string sql = "";
            bool ok = false;
            string MaterialDesc;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Text="";
                sql = "SELECT MaterialDesc from TMaterial WHERE Product='" + Product + "' and Material='"+Material+"' and Plant ='"+Plant+"'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                
                    MaterialDesc = reader["MaterialDesc"].ToString();
                
                cbb.Text = MaterialDesc;
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public int NomoUrut(string plant,string product,ref Label nilai)
        {

            SqlDataAdapter adapter = null;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            string sql = "";
            int urutanterakhir = 1;
            conn = GetConnString();
            try
            {
                //OpenSqlConnection();
                sql = "SELECT isnull(MAX(sequennumber),0) as sequennumber from asp_jrlog WHERE plant='" + plant + "' and product='" + product + "' and CAST(insertdate AS DATE)='"+ DateTime.Now.ToString("yyyy-MM-dd") +"'";
                adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    urutanterakhir = Convert.ToInt32(dt.Rows[0]["sequennumber"].ToString()) + 1;
                }
                else
                {
                    urutanterakhir = 1;
                }
                nilai.Text = urutanterakhir.ToString()+" "+ plant+" "+product;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                urutanterakhir = 1;
            }
            return urutanterakhir;
        }

        public bool SetLeader(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT LeaderId from TLEADER WHERE Plant='" + plant + "' and Product = '"+product+"'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["LeaderId"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetPacker(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();
                sql = "SELECT PackerID from TPACKER WHERE Plant='" + plant + "' and Product = '" + product + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["PackerID"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetLine(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (product == "[ALL]")
                    product = "%";

                sql = "SELECT LineId from TLINE WHERE Plant='" + plant + "' AND Product LIKE '"+product+"'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["LineId"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetSubModel(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (product == "DH")
                    product = "DY";

                sql = "SELECT Model from TPCS_MAT_MODEL WHERE Plant='" + plant + "' AND left(MRPC,2) LIKE '" + product + "' GROUP BY MODEL";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Model"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetGroupingModel(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (product == "DH")
                    product = "DY";

                sql = "SELECT GroupName from MMProductGrouping WHERE Plant='" + plant + "' AND Product LIKE '" + product + "' GROUP BY GroupName";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["GroupName"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public bool SetModel(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (product == "[ALL]")
                    product = "%";

                sql = "SELECT LTRIM(RTRIM(Model)) as 'Model' from TPCS_MODEL WHERE Plant='" + plant + "' AND Product LIKE '" + product + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Model"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetFG(ref ComboBox cbb, string plant, string model)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (model == "[ALL]")
                    model = "%";

                sql = "SELECT LTRIM(RTRIM(Material)) as 'Material' from TPCS_MAT_MODEL WHERE Plant like '"+plant+"' and Model LIKE '" + model + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Material"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SetFGByProduct(ref ComboBox cbb, string plant, string product)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (product== "[ALL]")
                    product = "%";

                sql = "SELECT LTRIM(RTRIM(Material)) as 'Material' from TPCS_MAT_MODEL WHERE Plant like '" + plant + "' and LEFT(REPLACE(MRPC,'DY','DH'),2) LIKE '" + product + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Material"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public string SetFGName(string plant, string fgcode)
        {
            string sql = "";            
            SqlCommand cmd;
            SqlDataReader reader = null;
            string result = "";
            try
            {

                if (fgcode.ToUpper() == "[ALL]")
                {
                    return "";
                }

                OpenSqlConnection();
                
                sql = "SELECT LTRIM(RTRIM(MaterialDesc)) as 'Material' from TPCS_MAT_MODEL WHERE Plant like '" + plant + "' and Material LIKE '" + fgcode + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader[0].ToString().ToUpper();
                }
                
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());                
            }
            return result;
        }

        public bool SetCap(ref ComboBox cbb)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                sql = "SELECT Capacity from TPCS_CAPACITY";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Capacity"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        public bool SaveError(string log, bool alert=true)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            
            try
            {
                if (Conn.State != ConnectionState.Open)
                {
                    MessageBox.Show(log);
                    return false;
                }
                   

                OpenSqlConnection();
                if (alert)
                {
                    //MessageBox.Show(log);
                    MessageBox.Show("Ooops! The system needs some maintenances, Please refer to File > Error Log!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                
                sql = "INSERT INTO TERRORLOG (Description, UserId, DateOccured, Station, System) VALUES "+
                    " (@log,'" + UserAccount.GetuserID().ToUpper() + "',GETDATE(),'" + mac + "','PCS')";
                cmd = new SqlCommand(sql, Conn);
                cmd.Parameters.Add("@log", SqlDbType.NVarChar);
                cmd.Parameters["@log"].Value = log;
                cmd.ExecuteNonQuery();
                
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }
        
        public bool IsMasterLogin(string username, string pass)
        {
            bool login = false;
            
            if ((username == UserAccount.GetMaster().ToUpper()) && (pass == UserAccount.GetMasterPass()))
            {
                login = true;
            }

            return login;
        }

        public bool IsUserLogin(string _username, string pass)
        {
            bool login = false;
            ArrayList user;
            user = SelectTable("TUSER", "UserName", "UPPER(UserID) = '" + _username + "' AND PWDCOMPARE('" + pass + "', Password)=1");
            if (user.Count > 0)
            {
                UserAccount.SetUserID(_username);
                UserAccount.SetUserName(user[0].ToString());

                user = SelectTable("TUSER", "GroupId", "UPPER(UserID) = '" + _username + "'");

                UserAccount.SetUserGroup(user[0].ToString());
                login = true;
            }

            return login;
        }


        public ArrayList SelectTable(string tablename, string field, string criteria = "", bool multifield = false)
        {
            ArrayList result = new ArrayList();
            string sql = "";
            SqlCommand cmd;
            SqlDataReader reader;
            try
            {
                OpenSqlConnection();
                sql = "SELECT " + field + " FROM " + tablename;
                if (!(criteria == ""))
                    sql = sql + " WHERE " + criteria;

                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();

                if (multifield)
                {
                    if (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result.Add(reader[i].ToString());
                        }

                    }
                }
                else
                {
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }
            finally
            {
                Conn.Dispose();
            }

            return result;
        }
        //public ArrayList SelectTable(string tablename, string field, string criteria = "")
        //{
        //    ArrayList result = new ArrayList();
        //    string sql = "";
        //    SqlCommand cmd;
        //    SqlDataReader reader;
        //    try
        //    {
        //        OpenSqlConnection();
        //        sql = "SELECT " + field + " FROM " + tablename;
        //        if (!(criteria == ""))
        //            sql = sql + " WHERE " + criteria;

        //        cmd = new SqlCommand(sql, Conn);
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            result.Add(reader[0].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        SaveError(ex.ToString());
        //    }
        //    finally
        //    {
        //        Conn.Dispose();
        //    }

        //    return result;
        //}

        public DataTable SelectTables(string tablename, string field, string criteria = "")
        {
            DataTable result = new DataTable();
            string sql = "";
            SqlDataAdapter adapter;
            
            try
            {
                OpenSqlConnection();
                sql = "SELECT " + field + " FROM " + tablename;
                if (!(criteria == ""))
                    sql = sql + " WHERE " + criteria;

                adapter = new SqlDataAdapter(sql, Conn);
                adapter.Fill(result);

            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }
            finally
            {
                Conn.Dispose();
            }

            return result;
        }

        public Boolean DeleteFromTable(string tablename, string primaryfield, string primaryvalue)
        {
            bool result = false;
            string sql = "";
            SqlCommand cmd;
            try
            {
                OpenSqlConnection();
                sql = "DELETE FROM " + tablename + " WHERE " + primaryfield +  " = '" + primaryvalue + "'";

                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }
            finally
            {
                Conn.Dispose();
            }

            return result;
        }

        public Boolean AddRecord(string tablename, string fields, string values)
        {
            bool result = false;
            string sql = "";
            SqlCommand cmd;
            try
            {
                OpenSqlConnection();
                sql = "INSERT INTO " + tablename + " (" + fields + ") VALUES (" + values + ")" ;
                
                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                int primary = -1;
                
                primary = ex.ToString().IndexOf("PRIMARY KEY");
                if (primary >= 0)
                    MessageBox.Show("Cannot add in the new record because of duplicated IDs", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    SaveError(ex.ToString());
            }
            finally
            {
                Conn.Dispose();
            }

            return result;
        }

        public Boolean EditRecord(string tablename, string updatedfield, string criteria)
        {
            bool result = false;
            string sql = "";
            SqlCommand cmd;
            try
            {
                OpenSqlConnection();
                
               
                sql = "UPDATE " + tablename + " SET " + updatedfield + " WHERE " + criteria;
             
                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                int primary = -1;

                primary = ex.ToString().IndexOf("PRIMARY KEY");
                if (primary >= 0)
                    MessageBox.Show("Cannot update the record because of duplicated IDs", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    SaveError(ex.ToString());
            }
            finally
            {
                Conn.Dispose();
            }

            return result;
        }

        public bool IsThisOnline()
        {
            bool ol = false;
            string sql="";
            SqlCommand cmd;
            
            try
            {
                OpenSqlConnection();
                sql = "SELECT UserId from tuser_online where Station='"+mac+"'";
                cmd = new SqlCommand(sql, Conn);

                if (cmd.ExecuteScalar() == null)
                {
                    ol = false;
                }
                else
                {
                    ol = true;
                }
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ol = false;
            }
            return ol;
        }

        public bool SetUserOnline(string userid)
        {
            bool ok=false;
            string sql="";
            SqlCommand cmd;
            SqlTransaction trans = null;
            try
            {
                OpenSqlConnection();
                trans = Conn.BeginTransaction();
                sql = "UPDATE TUSER SET Status='ONLINE', LastLogin=GETDATE(), Station='"+mac+"' WHERE UserId='"+userid+"'";
                cmd = new SqlCommand(sql, Conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO TUSER_ONLINE (UserId, Program, Station, UpdateDate) VALUES "+
                    "('"+userid+"', 'PCS','"+mac+"', GETDATE())";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                SaveError(ex.ToString());
            }
            return ok;
        }

        public bool SystemLogOff()
        {
            bool ok = false;
            string sql = "";
            SqlCommand cmd;
            SqlTransaction trans=null;
            string userid = UserAccount.GetuserID();
            try
            {
                OpenSqlConnection();
                trans = Conn.BeginTransaction();
                sql = "DELETE FROM TUSER_ONLINE WHERE UserId='" + userid + 
                    "' AND Program='PCS' AND Station='" + mac + "'";
                cmd = new SqlCommand(sql, Conn);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                sql = "SELECT UserId from TUSER_ONLINE WHERE UserId='" + userid + "'";
                cmd.CommandText = sql;
                if (cmd.ExecuteScalar() == null)
                {
                    sql = "UPDATE TUSER SET Status='OFFLINE', LastLogOut=GetDate() WHERE UserId='"+userid+"'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                ok = true;
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                ok = false;
                SaveError(ex.ToString());
            }
            return ok;
        }

        public string GetGlobal(string setting)
        {
            string result = "";
            string sql = "";
            SqlCommand cmd;
            
            try
            {
                OpenSqlConnection();
                sql = "SELECT GlobalValue from TGLOBAL Where GlobalSetting='" + setting + "'";
                cmd = new SqlCommand(sql, Conn);
                if (cmd.ExecuteScalar() == null)
                {
                    result = "";
                }
                else
                {
                    result = cmd.ExecuteScalar().ToString();
                }

               
            }
            catch (Exception ex)
            {

                SaveError(ex.ToString());
            }
            return result;
        }

        public bool SetGlobal(string setname, string setvalue)
        {
            bool result = false;
            string sql = "";
            SqlCommand cmd;

            try
            {
                OpenSqlConnection();
                sql = "UPDATE TGLOBAL SET GlobalValue='"+setvalue+"' WHERE GlobalSetting='"+setname+"'";
                cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();
                result = true;

            }
            catch (Exception ex)
            {
                result = false;
                SaveError(ex.ToString());
            }
            return result;
        }


        public bool SetModelLine(ref ComboBox cbb, string plant, string product, string Wc)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                if (Wc == "[ALL]")
                    Wc = "%";

                sql = "SELECT LTRIM(RTRIM(Model)) as 'Model' FROM TPCS_ROUTEMP WHERE Plant='" + plant + "' AND Product='" + product + "' AND SAPWC LIKE '"+ Wc + "'";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Model"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }


        public bool SetReason(ref ComboBox cbb)
        {
            string sql = "";
            bool ok = false;
            SqlCommand cmd;
            SqlDataReader reader = null;
            ArrayList result = new ArrayList();
            try
            {
                OpenSqlConnection();
                cbb.Items.Clear();

                sql = "SELECT Cod FROM CodLst WHERE GrpCod='PCS_REASON' ORDER BY CodAbb ASC";
                cmd = new SqlCommand(sql, Conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Cod"].ToString());
                }
                cbb.Items.AddRange(result.ToArray());
                ok = true;
            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }



    }
}
