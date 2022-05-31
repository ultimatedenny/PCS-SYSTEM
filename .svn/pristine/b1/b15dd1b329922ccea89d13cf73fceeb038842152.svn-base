using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSSystem
{
    public static class UserAccount
    {
        private static string Master = "Admin";
        private static string MasterPassword = "a";

        private static string UserID = "";
        private static string UserGroup = "";
        private static string UserName = "";
        private static string[,] Authorization;
        private static database db = new database();
        public static string GetuserID()
        {
            return UserID;
        }

        public static string GetuserName()
        {
            return UserName;
        }

        public static string GetUserGroup()
        {
            return UserGroup;
        }

        public static string GetMaster()
        {
            return Master;
        }

        public static string GetMasterPass()
        {
            return MasterPassword;
        }


        public static void SetUserID(string _user)
        {
            UserID = _user;
        }

        public static void SetUserName(string _user)
        {
            UserName= _user;
        }

        public static void SetUserGroup(string _user)
        {
            UserGroup= _user;
        }

        public static void SetAuthorization(int totalform,string[] formname, string[] canedit){
            try
            {
                Authorization = new string[totalform, 2];

                for (int i = 0; i < totalform; i++)
                {
                    Authorization[i, 0] = formname[i];
                    Authorization[i, 1] = canedit[i];
                }
                
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
            
        }

        public static string[,] GetAuthorization(){
            return Authorization;
        }

    }
}
