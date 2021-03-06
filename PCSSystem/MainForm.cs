using PCSSystem.Master_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;


namespace PCSSystem
{
    public partial class MainForm : Form
    {
        database db = new database();
        Common cm = new Common();
        public MainForm()
        {
            InitializeComponent();

        }

        private void sessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSessionForm();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenSessionForm();
        }

        void OpenSessionForm()
        {
            //FSession f = new FSession();
            //f.MdiParent = this;
            //f.Show();
            //f.BringToFront();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName();
            string ip = string.Empty;
            //string ip2 = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            MyGlobal.strIP = endPoint.Address.ToString();
            MyGlobal.dbConn = Properties.Settings.Default.ConnString;
            string status = Properties.Settings.Default.ServerStatus.ToUpper();
            if (status == "TESTING" || status == "LIVE" || status == "QA")
            {
                toolDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PerformLogin();
            }
            else
            {              
                MessageBox.Show("Missing System's Database server status configuration!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();        
            }
        }

        void PerformLogin()
        {
            FLogin f = new FLogin();
            DisableMenu();
            toolUserLogIn.Text = "";
            if (f.ShowDialog() == DialogResult.OK)
            {
                EnableMenu();
                this.WindowState = FormWindowState.Maximized;
                toolUserLogIn.Text = UserAccount.GetuserName() + "[" + UserAccount.GetUserGroup() + "]";
                lblServer.Text = Properties.Settings.Default.ServerStatus.ToUpper() + " SERVER";
                if (lblServer.Text.ToUpper() == "LIVE SERVER")
                {
                    toolStrip1.BackColor = Color.FromArgb(224, 224, 224);
                    PictureBox1.BackColor = Color.FromArgb(224, 224, 224);
                    menuStrip1.BackColor = Color.FromArgb(224, 224, 224);
                    statusStrip1.BackColor = Color.FromArgb(224, 224, 224);
                }
                else
                {
                    toolStrip1.BackColor = Color.LightPink;
                    PictureBox1.BackColor = Color.LightPink;
                    menuStrip1.BackColor = Color.LightPink;
                    statusStrip1.BackColor = Color.LightPink;
                }
            }
        }

        void DisableMenu()
        {
            loginToolStripMenuItem.Text = "&Login";
            toolStrip1.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Visible = false;
        }

        void EnableMenu()
        {
            loginToolStripMenuItem.Text = "&Logout";
            toolStrip1.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Visible = true;
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FLine f = new FLine();
            //f.MdiParent = this;
            //f.Show();
            //f.BringToFront();
        }

        private void machineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FLeader f = new FLeader();
            //f.MdiParent = this;
            //f.Show();
            //f.BringToFront();
        }

        private void userAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Fuser f = new Fuser();
            //f.MdiParent = this;
            //f.Show();
            //f.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loginToolStripMenuItem.Text == "&Logout")
            {
                loginToolStripMenuItem.Text = "&Login";
               db.SystemLogOff();
               lblServer.Text = "OFFLINE";
                DisableMenu();
            }
            PerformLogin();
        }
                 
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                MessageBox.Show("Please close all forms before exiting the application!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                db.SystemLogOff();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAbout f = new FAbout();
            f.ShowDialog();
        }

        private void masterCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FMasterCap")){
                if (CanOpenForm())
                {
                    FMasterCap f = new FMasterCap
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void byLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FMasCapSchLine"))
            {
                if (CanOpenForm())
                {
                    FMasCapSchLine f = new FMasCapSchLine
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void byProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FMasCapSch"))
            {
                if (CanOpenForm())
                {
                    FMasCapSch f = new FMasCapSch
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }


        bool CanOpenForm()
        {
            bool ok = false;
            int formopen=0;
            try
            {
                formopen = this.MdiChildren.Length;
                if (formopen > 6)
                {
                    ok = false;
                    MessageBox.Show("You can only open 6 forms! Please close the other form and reopen again.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                db.SaveError(ex.ToString());
                ok = false;
            }
            return ok;
        }

        private void dailyLoadPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FUplData
            //if (cm.Check_Authority("FDailyPlan"))
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    FUplData f = new FUplData();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void routingManPowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FRouteMP"))
            {
                if (CanOpenForm())
                {
                    FRouteMP f = new FRouteMP();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void wCVsShiftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    FSAPShift f = new FSAPShift();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void nonworkingDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FNonWD"))
            {
                if (CanOpenForm())
                {
                    FNonWD f = new FNonWD();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void routingAndManPowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FRouteMP"))
            {
                if (CanOpenForm())
                {
                    FRouteMP f = new FRouteMP();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void nonWorkingDaysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FNonWD"))
            {
                if (CanOpenForm())
                {
                    FNonWD f = new FNonWD();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void shiftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    FSAPShift f = new FSAPShift();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void errorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanOpenForm())
            {
                FError f = new FError();
                f.ShowDialog();
            }
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPSch"))
            {
                if (CanOpenForm())
                {
                    FSAPSch f = new FSAPSch();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void scheduleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPSch"))
            {
                if (CanOpenForm())
                {
                    FSAPSch f = new FSAPSch();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void f(object sender, EventArgs e)
        {

        }

        private void shiftsVsWorkCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FShiftWC
            if (cm.Check_Authority("FShiftWC"))
            {
                if (CanOpenForm())
                {
                    FShiftWC f = new FShiftWC();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void shiftsWorkCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FShiftWC"))
            {
                if (CanOpenForm())
                {
                    FShiftWC f = new FShiftWC();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void bFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FTobeBF"))
            {
                if (CanOpenForm())
                {
                    FTobeBF f = new FTobeBF();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void bFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FTobeBF"))
            {
                if (CanOpenForm())
                {
                    FTobeBF f = new FTobeBF();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void capacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    FCap f = new FCap();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void capacityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    FCap f = new FCap();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void materialVsModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FMODEL"))
            {
                if (CanOpenForm())
                {
                    FModel f = new FModel();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void materialVsModelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FMODEL"))
            {
                if (CanOpenForm())
                {
                    FModel f = new FModel();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void saveToJEQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FTOBEJEQ"))
            {
                if (CanOpenForm())
                {
                    FTobeJEQ f = new FTobeJEQ();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            //FUplData
            //if (cm.Check_Authority("FDailyPlan"))
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    FUplData f = new FUplData();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void scheduleEportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPSch"))
            {
                if (CanOpenForm())
                {
                    FSAPSch f = new FSAPSch("EXPORT_ONLY");
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void productionDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("fprodnday"))
            {
                if (CanOpenForm())
                {
                    FProdnDay f = new FProdnDay();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void lockedProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("flockprdt"))
            {
                if (CanOpenForm())
                {
                    FLockPrdt f = new FLockPrdt();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void globalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("fglobal"))
            {
                if (CanOpenForm())
                {
                    FGlobal f = new FGlobal();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void pVIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pVIndicatorListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("findlist"))
            {
                if (CanOpenForm())
                {
                    FIndList f = new FIndList();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lotIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("flotind"))
            {
                if (CanOpenForm())
                {
                    FLotInd f = new FLotInd();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void changeTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("fchangetype"))
            {
                if (CanOpenForm())
                {
                    FChangeType f = new FChangeType();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void problemOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("fproborigin"))
            {
                if (CanOpenForm())
                {
                    FProbOrigin f = new FProbOrigin();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void affectedChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("faffchngd"))
            {
                if (CanOpenForm())
                {
                    FAffChngd f = new FAffChngd();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void sPCVsFGCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("fspc_fg"))
            {
                if (CanOpenForm())
                {
                    FSPC_FG f = new FSPC_FG();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void sPCLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("factline"))
            {
                if (CanOpenForm())
                {
                    FActLine f = new FActLine();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void partStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("AJR_Part_Status"))
            {
                if (CanOpenForm())
                {
                    Reports.FDLPDetailPart f = new Reports.FDLPDetailPart();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void jRListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("JR_List"))
            {
                if (CanOpenForm())
                {
                    Reports.FAJRList f = new Reports.FAJRList();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void safetyStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSafStk"))
            {
                if (CanOpenForm())
                {
                    Master_Data.FSafStk f = new Master_Data.FSafStk();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void partCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSPCategory"))
            {
                if (CanOpenForm())
                {
                    Master_Data.FSPCategory f = new Master_Data.FSPCategory();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void jROstdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FOstdReq"))
            {
                if (CanOpenForm())
                {
                    Reports.FOstdReq f = new Reports.FOstdReq();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void additionalJRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    Reports.FAddJR f = new Reports.FAddJR();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void eApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FAddJR"))
            {
                if (CanOpenForm())
                {
                    Reports.FApproval f = new Reports.FApproval();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    FUplData f = new FUplData();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripManualJob_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    ASP.FManualJobRequest f = new ASP.FManualJobRequest
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void pP57ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FUplData"))
            {
                if (CanOpenForm())
                {
                    ASP.FUplDataPP57 f = new ASP.FUplDataPP57
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void subCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void jobRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    ASP.FRepPP57 f = new ASP.FRepPP57
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                    //f.BringToFront();
                }
            }
        }

        private void dataSubCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    ASP.FMSubCat f = new ASP.FMSubCat
                    {
                        MdiParent = this
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void exclutionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exclusionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    ASP.FMexclution f = new ASP.FMexclution { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void emailMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    ASP.FMemail f = new ASP.FMemail { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void dataSubCategoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    ASP.FMSubCat f = new ASP.FMSubCat { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void exclusionListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FCAP"))
            {
                if (CanOpenForm())
                {
                    ASP.FMexclution f = new ASP.FMexclution { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void emailMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    ASP.FMemail f = new ASP.FMemail { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void initialPartCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    ASP.FMInitialPart f = new ASP.FMInitialPart { MdiParent = this };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void jobRequestHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    ASP.FJobRequest f = new ASP.FJobRequest
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void jRProductsLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cm.Check_Authority("FSAPShift"))
            {
                if (CanOpenForm())
                {
                    ASP.FLockproduct f = new ASP.FLockproduct
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void lineLeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FSAPShift"))
            //{
            //    if (CanOpenForm())
            //    {
                    Master_Data.FLineLeader f = new Master_Data.FLineLeader();
                    f.MdiParent = this;
                    f.WindowState = FormWindowState.Maximized;
                    f.Show();
                    f.BringToFront();
            //    }
            //}
        }

        private void linePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlinePriority f = new FlinePriority
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Show();
            f.BringToFront();
        }

        private void productGroupingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master_Data.FProductGrouping f = new Master_Data.FProductGrouping
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Show();
            f.BringToFront();
        }

        private void changeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FChangeItem"))
            //{
                if (CanOpenForm())
                {
                    FChangeItem f = new FChangeItem();
                    f.MdiParent = this;
                    f.Show();
                    f.BringToFront();
                }
            //}
        }

        private void finishGoodStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FFGStatus"))
            //{
            if (CanOpenForm())
            {
                FFGStatus f = new FFGStatus();
                f.MdiParent = this;
                f.Show();
                f.BringToFront();
            }
            //}
        }

        private void backgroundOfImprovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FFGStatus"))
            //{
            if (CanOpenForm())
            {
                FBackgroundImprovement f = new FBackgroundImprovement
                {
                    MdiParent = this
                };
                f.Show();
                f.BringToFront();
            }
            //}
        }

        private void indicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FFGStatus"))
            //{
            if (CanOpenForm())
            {
                FIndicationType f = new FIndicationType();
                f.MdiParent = this;
                f.Show();
                f.BringToFront();
            }
            //}
        }

        private void pVIndicatorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FFGStatus"))
            //{
            if (CanOpenForm())
            {
                FIndicatorList f = new FIndicatorList();
                f.MdiParent = this;
                f.Show();
                f.BringToFront();
                f.WindowState = FormWindowState.Maximized;
            }
            //}
        }

        private void fixIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cm.Check_Authority("FFGStatus"))
            //{
            if (CanOpenForm())
            {
                FIndicatorFix f = new FIndicatorFix
                {
                    MdiParent = this
                };
                f.Show();
                f.BringToFront();
                f.WindowState = FormWindowState.Maximized;
            }
            //}
        }
    }
}
