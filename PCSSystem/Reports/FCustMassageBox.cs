using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCSSystem.Reports
{
    public partial class FCustMassageBox : Form
    {
        database db = new database();
        Common cm = new Common();
        public FCustMassageBox()
        {
            InitializeComponent();
        }

        static FCustMassageBox MsgBox; static DialogResult result =DialogResult.No;

        public static DialogResult Show(string Text, string Caption, string BtnOK, string BtnCancel,string BtnDetail,string PartCode)
        {
            MsgBox = new FCustMassageBox();
            MsgBox.label1.Text = Text;
            MsgBox.button1.Text = BtnOK;
            MsgBox.button2.Text = BtnCancel;
            MsgBox.button3.Text = BtnDetail;
            MsgBox.label2.Text = PartCode;
            MsgBox.ShowDialog();
            
            return result;
        }

        private void FCustMassageBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            MsgBox.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
            
                
                Reports.FBalJR f = new Reports.FBalJR(MsgBox.label2.Text);
                f.ShowDialog();
                
            
        }
       
    }
}
