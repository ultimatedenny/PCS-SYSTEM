using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace PCSSystem
{
    public partial class FLogin : Form
    {
        database db = new database();
        bool online = false;
        private string mac = System.Environment.MachineName;
        public FLogin()
        {
            InitializeComponent();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool success = false;
            string username = "";
            string status = Properties.Settings.Default.ServerStatus.ToUpper();
            if (!online)
            {
                var MasterLogin = db.IsMasterLogin(txtUserName.Text.ToUpper(), txtPassword.Text);
                var UserLogin = db.IsUserLogin(txtUserName.Text.ToUpper(), txtPassword.Text);
                if (MasterLogin)
                {
                    MessageBox.Show("Welcome Master!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    success = true;
                    SetAuthorizationFromDB();
                }
                else if (UserLogin)
                {
                    username = UserAccount.GetuserName();
                    MessageBox.Show("Welcome " + username + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    success = true;
                    SetAuthorizationFromDB(UserAccount.GetUserGroup());
                }

                if (success)
                {
                    db.SetUserOnline(UserAccount.GetuserID());
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect User Name and Password!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string sql = "";
                SqlConnection conn;
                SqlCommand cmd;
                SqlDataReader reader;

                sql = "SELECT UserId, GroupId, UserName FROM TUSER WHERE Status='ONLINE' and Station='" + mac + "'";
                conn = db.GetConnString();
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserAccount.SetUserID(reader["UserId"].ToString());
                    UserAccount.SetUserName(reader["UserName"].ToString());
                    UserAccount.SetUserGroup(reader["GroupId"].ToString());
                    SetAuthorizationFromDB(UserAccount.GetUserGroup());
                    db.SetUserOnline(UserAccount.GetuserID());
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //MessageBox.Show("Incorrect User Name and Password!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    online = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            online = db.IsThisOnline();
            if (online)
            {
                btnOK.PerformClick();
            }
        }

        void SetAuthorizationFromDB(string usergroup="")
        {
            string[] access=new string[200];
            string[] canedit = new string[200];
            int i=0;
            string sql = "";
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader reader;
            try
            {
                conn = db.GetConnString();
                if (!(usergroup == ""))
                {
                    sql = "SELECT FormName, ReadOnly from TGROUPACCESS WHERE GroupId='" + usergroup + "' AND System='PCS'";
                    cmd = new SqlCommand(sql, conn);
                    reader = cmd.ExecuteReader();
                    i = 0;
                    while (reader.Read())
                    {
                        access[i] = reader["FormName"].ToString();
                        canedit[i] = reader["ReadOnly"].ToString();
                        i += 1;
                    }
                }
                else
                {
                    sql = "SELECT FormName from TACCESS WHERE System='PCS'";
                    cmd = new SqlCommand(sql, conn);
                    reader = cmd.ExecuteReader();
                    i = 0;
                    while (reader.Read())
                    {
                        access[i] = reader["FormName"].ToString();
                        canedit[i] = "0";
                        i += 1;
                    }
                }

                UserAccount.SetAuthorization(i, access, canedit);
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }
    }
}