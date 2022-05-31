namespace PCSSystem.ASP
{
    partial class FUplDataPP57
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblStatusMB52 = new System.Windows.Forms.Label();
            this.btnUpPP57 = new System.Windows.Forms.Button();
            this.btnformclose = new System.Windows.Forms.Button();
            this.dataGridView12 = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblRows = new System.Windows.Forms.Label();
            this.groupcontrol = new System.Windows.Forms.GroupBox();
            this.btnuncheck = new System.Windows.Forms.Button();
            this.btncheck = new System.Windows.Forms.Button();
            this.groupbutton = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TcountNotset = new System.Windows.Forms.Label();
            this.btnconfirm = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupgrid = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView12)).BeginInit();
            this.groupcontrol.SuspendLayout();
            this.groupbutton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupgrid.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx|CSV files (*.csv)|*.csv";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label3.Location = new System.Drawing.Point(14, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(871, 33);
            this.label3.TabIndex = 18;
            this.label3.Text = "PP 57 - Single Part Requirement";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbbProduct);
            this.groupBox1.Controls.Add(this.cbbPlant);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 54);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select the Plant and Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(212, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "PRODUCT :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "PLANT :";
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(289, 19);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(105, 21);
            this.cbbProduct.TabIndex = 17;
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(79, 19);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(110, 21);
            this.cbbPlant.TabIndex = 16;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblStatusMB52);
            this.groupBox2.Controls.Add(this.btnUpPP57);
            this.groupBox2.Controls.Add(this.btnformclose);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(438, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 54);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. Upload PP57";
            // 
            // lblStatusMB52
            // 
            this.lblStatusMB52.AutoSize = true;
            this.lblStatusMB52.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMB52.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusMB52.Location = new System.Drawing.Point(220, 23);
            this.lblStatusMB52.Name = "lblStatusMB52";
            this.lblStatusMB52.Size = new System.Drawing.Size(97, 17);
            this.lblStatusMB52.TabIndex = 19;
            this.lblStatusMB52.Text = "PP57 Uploaded!";
            this.lblStatusMB52.Visible = false;
            // 
            // btnUpPP57
            // 
            this.btnUpPP57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpPP57.Image = global::PCSSystem.Properties.Resources.upload_2_xxl2;
            this.btnUpPP57.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpPP57.Location = new System.Drawing.Point(6, 17);
            this.btnUpPP57.Name = "btnUpPP57";
            this.btnUpPP57.Padding = new System.Windows.Forms.Padding(20, 0, 30, 0);
            this.btnUpPP57.Size = new System.Drawing.Size(203, 31);
            this.btnUpPP57.TabIndex = 18;
            this.btnUpPP57.Text = "   Upload PP57";
            this.btnUpPP57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpPP57.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpPP57.UseVisualStyleBackColor = true;
            this.btnUpPP57.Click += new System.EventHandler(this.btnUpPP57_Click);
            // 
            // btnformclose
            // 
            this.btnformclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnformclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnformclose.Image = global::PCSSystem.Properties.Resources.cancel;
            this.btnformclose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnformclose.Location = new System.Drawing.Point(317, 13);
            this.btnformclose.Name = "btnformclose";
            this.btnformclose.Padding = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.btnformclose.Size = new System.Drawing.Size(112, 35);
            this.btnformclose.TabIndex = 6;
            this.btnformclose.Text = "CLOSE";
            this.btnformclose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnformclose.UseVisualStyleBackColor = true;
            this.btnformclose.Click += new System.EventHandler(this.btnformclose_Click);
            // 
            // dataGridView12
            // 
            this.dataGridView12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView12.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView12.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView12.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk});
            this.dataGridView12.Location = new System.Drawing.Point(17, 342);
            this.dataGridView12.Name = "dataGridView12";
            this.dataGridView12.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView12.Size = new System.Drawing.Size(0, 91);
            this.dataGridView12.TabIndex = 136;
            this.dataGridView12.Visible = false;
            this.dataGridView12.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // chk
            // 
            this.chk.HeaderText = "Check";
            this.chk.Name = "chk";
            this.chk.Width = 44;
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(12, 294);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(73, 13);
            this.lblRows.TabIndex = 137;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // groupcontrol
            // 
            this.groupcontrol.Controls.Add(this.btnuncheck);
            this.groupcontrol.Controls.Add(this.btncheck);
            this.groupcontrol.Location = new System.Drawing.Point(12, 123);
            this.groupcontrol.Name = "groupcontrol";
            this.groupcontrol.Size = new System.Drawing.Size(197, 50);
            this.groupcontrol.TabIndex = 139;
            this.groupcontrol.TabStop = false;
            this.groupcontrol.Text = "Control";
            this.groupcontrol.Visible = false;
            // 
            // btnuncheck
            // 
            this.btnuncheck.Location = new System.Drawing.Point(103, 17);
            this.btnuncheck.Name = "btnuncheck";
            this.btnuncheck.Size = new System.Drawing.Size(75, 23);
            this.btnuncheck.TabIndex = 135;
            this.btnuncheck.Text = "Uncheck All";
            this.btnuncheck.UseVisualStyleBackColor = true;
            this.btnuncheck.Click += new System.EventHandler(this.btnuncheck_Click);
            // 
            // btncheck
            // 
            this.btncheck.Location = new System.Drawing.Point(15, 17);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(75, 23);
            this.btncheck.TabIndex = 134;
            this.btncheck.Text = "Check All";
            this.btncheck.UseVisualStyleBackColor = true;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // groupbutton
            // 
            this.groupbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbutton.BackColor = System.Drawing.SystemColors.Control;
            this.groupbutton.Controls.Add(this.button1);
            this.groupbutton.Controls.Add(this.TcountNotset);
            this.groupbutton.Controls.Add(this.btnconfirm);
            this.groupbutton.Location = new System.Drawing.Point(227, 123);
            this.groupbutton.Name = "groupbutton";
            this.groupbutton.Size = new System.Drawing.Size(656, 50);
            this.groupbutton.TabIndex = 143;
            this.groupbutton.TabStop = false;
            this.groupbutton.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(528, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 27);
            this.button1.TabIndex = 184;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TcountNotset
            // 
            this.TcountNotset.AutoSize = true;
            this.TcountNotset.Location = new System.Drawing.Point(144, 22);
            this.TcountNotset.Name = "TcountNotset";
            this.TcountNotset.Size = new System.Drawing.Size(13, 13);
            this.TcountNotset.TabIndex = 134;
            this.TcountNotset.Text = "..";
            // 
            // btnconfirm
            // 
            this.btnconfirm.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnconfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconfirm.ForeColor = System.Drawing.SystemColors.Window;
            this.btnconfirm.Location = new System.Drawing.Point(6, 13);
            this.btnconfirm.Name = "btnconfirm";
            this.btnconfirm.Size = new System.Drawing.Size(112, 31);
            this.btnconfirm.TabIndex = 133;
            this.btnconfirm.Text = "Issue JR";
            this.btnconfirm.UseVisualStyleBackColor = false;
            this.btnconfirm.Click += new System.EventHandler(this.btnconfirm_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MediumTurquoise;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk2});
            this.dataGridView1.Location = new System.Drawing.Point(15, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(840, 271);
            this.dataGridView1.TabIndex = 183;
            this.dataGridView1.Visible = false;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick_1);
            // 
            // chk2
            // 
            this.chk2.HeaderText = "Check";
            this.chk2.Name = "chk2";
            // 
            // groupgrid
            // 
            this.groupgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupgrid.Controls.Add(this.lblRows);
            this.groupgrid.Controls.Add(this.dataGridView1);
            this.groupgrid.Location = new System.Drawing.Point(12, 179);
            this.groupgrid.Name = "groupgrid";
            this.groupgrid.Size = new System.Drawing.Size(871, 319);
            this.groupgrid.TabIndex = 184;
            this.groupgrid.TabStop = false;
            this.groupgrid.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(-2, -8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(900, 50);
            this.groupBox4.TabIndex = 185;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Location = new System.Drawing.Point(12, 108);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(873, 10);
            this.groupBox5.TabIndex = 186;
            this.groupBox5.TabStop = false;
            // 
            // FUplDataPP57
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 510);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupgrid);
            this.Controls.Add(this.groupbutton);
            this.Controls.Add(this.groupcontrol);
            this.Controls.Add(this.dataGridView12);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FUplDataPP57";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload PP57";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FUplDataPP57_FormClosing);
            this.Load += new System.EventHandler(this.FUplDataPP57_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView12)).EndInit();
            this.groupcontrol.ResumeLayout(false);
            this.groupbutton.ResumeLayout(false);
            this.groupbutton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupgrid.ResumeLayout(false);
            this.groupgrid.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnformclose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStatusMB52;
        private System.Windows.Forms.Button btnUpPP57;
        private System.Windows.Forms.DataGridView dataGridView12;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.GroupBox groupcontrol;
        private System.Windows.Forms.Button btnuncheck;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.GroupBox groupbutton;
        private System.Windows.Forms.Button btnconfirm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupgrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk2;
        private System.Windows.Forms.Label TcountNotset;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}