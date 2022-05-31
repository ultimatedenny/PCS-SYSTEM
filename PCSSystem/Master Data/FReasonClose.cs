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

namespace PCSSystem
{
    public partial class FReasonClose : Form
    {
        private string _plant,_product,_line, _model;
        Common cm = new Common();
        database db = new database();
        //string errorsql = "", errortitle = "";
        //bool NewRecord = false;
       // SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlConnection conn;
       // SqlDataReader reader;
        string sql = "";
        bool _checked = false;

        public ArrayList selectedfg = new ArrayList();
        string[] _matNewCooice;
        bool loadTMP = true;
        public static string reasonclose = "";
        public FReasonClose()
        {
            InitializeComponent();
        }

        private void chkReason_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReason.Checked == true)
            {
                cbbReason.Enabled = false;
                txtReason.ReadOnly = false;
            }
            else
            {
                cbbReason.Enabled = true;
                txtReason.ReadOnly = true;
                txtReason.Text = "";
            }
        }

        private void FReasonClose_Load(object sender, EventArgs e)
        {
            db.SetReason(ref cbbReason);
            cbbReason.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string reason = "";
                if (chkReason.Checked == true)
                {
                    reason = txtReason.Text;
                }
                else
                {
                    reason = cbbReason.SelectedItem.ToString();
                }

                if (reason != "")
                {
                    reasonclose = reason;
                    this.DialogResult = DialogResult.OK;
                }
                    
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

    }
}
