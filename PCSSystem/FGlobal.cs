using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCSSystem
{
    
    public partial class FGlobal : Form
    {
        database db =new database();
        const string dlp = "DLPCLOSEMINS";

        public FGlobal()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool Valid_Input()
        {
            bool ok = true;
            try
            {
                if (txtMins.Text == "0")
                {
                    MessageBox.Show("Please input the DLP Auto Close minute!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    ok = false;
                    txtMins.Text = "";
                }
            }
            catch (Exception ex)
            {
                ok = false;
                db.SaveError(ex.ToString());
            }
            return ok;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Valid_Input())
            {
                if (db.SetGlobal(dlp, txtMins.Text))
                {
                    MessageBox.Show("The setting has been saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClose.PerformClick();
                }
            }
        }

        private void txtMins_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtMins_Enter(object sender, EventArgs e)
        {
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "0")
                {
                    t.Text = "";
                }

            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }   
        }

        private void txtMins_Leave(object sender, EventArgs e)
        {
            int n;
            TextBox t;
            t = (TextBox)sender;
            try
            {
                if (t.Text == "")
                {
                    t.Text = "0";
                }
                else
                {
                    if (int.TryParse(t.Text, out n))
                    {
                        t.Text = Convert.ToInt32(t.Text).ToString();                        
                    }
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
            }
        }

        private void FGlobal_Load(object sender, EventArgs e)
        {
            string dlpclose = db.GetGlobal(dlp);
            if (dlpclose == "")
                txtMins.Text = "0";
            else
                txtMins.Text = Convert.ToInt32(dlpclose).ToString();
        }
    }
}
