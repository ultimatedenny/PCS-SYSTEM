namespace PCSSystem.ASP
{
    partial class FManualJobRequest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblStatusMB52 = new System.Windows.Forms.Label();
            this.btnUpJR = new System.Windows.Forms.Button();
            this.btnformclose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUncheck = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TcountNotset = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnIssueJR = new System.Windows.Forms.Button();
            this.groupgrid = new System.Windows.Forms.GroupBox();
            this.lblRows = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupgrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(4, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(1153, 50);
            this.groupBox4.TabIndex = 186;
            this.groupBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Fira Code", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1119, 33);
            this.label1.TabIndex = 18;
            this.label1.Text = "Manual Job Request";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbProduct);
            this.groupBox1.Controls.Add(this.cbbPlant);
            this.groupBox1.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.groupBox1.Location = new System.Drawing.Point(14, 59);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(490, 54);
            this.groupBox1.TabIndex = 187;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.label2.Location = new System.Drawing.Point(244, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Product";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Plant";
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(247, 27);
            this.cbbProduct.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(235, 21);
            this.cbbProduct.TabIndex = 17;
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(8, 27);
            this.cbbPlant.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(231, 21);
            this.cbbPlant.TabIndex = 16;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtCategory);
            this.groupBox2.Controls.Add(this.lblStatusMB52);
            this.groupBox2.Controls.Add(this.btnUpJR);
            this.groupBox2.Controls.Add(this.btnformclose);
            this.groupBox2.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.groupBox2.Location = new System.Drawing.Point(511, 59);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(645, 54);
            this.groupBox2.TabIndex = 188;
            this.groupBox2.TabStop = false;
            // 
            // txtCategory
            // 
            this.txtCategory.Enabled = false;
            this.txtCategory.Location = new System.Drawing.Point(250, 19);
            this.txtCategory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(128, 21);
            this.txtCategory.TabIndex = 20;
            this.txtCategory.Text = "Common";
            // 
            // lblStatusMB52
            // 
            this.lblStatusMB52.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusMB52.AutoSize = true;
            this.lblStatusMB52.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.lblStatusMB52.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusMB52.Location = new System.Drawing.Point(393, 23);
            this.lblStatusMB52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusMB52.Name = "lblStatusMB52";
            this.lblStatusMB52.Size = new System.Drawing.Size(91, 13);
            this.lblStatusMB52.TabIndex = 19;
            this.lblStatusMB52.Text = "JR Uploaded!";
            this.lblStatusMB52.Visible = false;
            // 
            // btnUpJR
            // 
            this.btnUpJR.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.btnUpJR.Image = global::PCSSystem.Properties.Resources.upload_2_xxl2;
            this.btnUpJR.Location = new System.Drawing.Point(8, 11);
            this.btnUpJR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpJR.Name = "btnUpJR";
            this.btnUpJR.Padding = new System.Windows.Forms.Padding(23, 0, 35, 0);
            this.btnUpJR.Size = new System.Drawing.Size(234, 37);
            this.btnUpJR.TabIndex = 18;
            this.btnUpJR.Text = "Upload Job Request";
            this.btnUpJR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpJR.UseVisualStyleBackColor = true;
            this.btnUpJR.Click += new System.EventHandler(this.btnUpJR_Click);
            // 
            // btnformclose
            // 
            this.btnformclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnformclose.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.btnformclose.Image = global::PCSSystem.Properties.Resources.cancel;
            this.btnformclose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnformclose.Location = new System.Drawing.Point(514, 11);
            this.btnformclose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnformclose.Name = "btnformclose";
            this.btnformclose.Padding = new System.Windows.Forms.Padding(18, 3, 18, 3);
            this.btnformclose.Size = new System.Drawing.Size(124, 37);
            this.btnformclose.TabIndex = 6;
            this.btnformclose.Text = "CLOSE";
            this.btnformclose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnformclose.UseVisualStyleBackColor = true;
            this.btnformclose.Click += new System.EventHandler(this.btnformclose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUncheck);
            this.groupBox3.Controls.Add(this.btnCheck);
            this.groupBox3.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.groupBox3.Location = new System.Drawing.Point(14, 119);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(220, 54);
            this.groupBox3.TabIndex = 189;
            this.groupBox3.TabStop = false;
            // 
            // btnUncheck
            // 
            this.btnUncheck.Location = new System.Drawing.Point(112, 15);
            this.btnUncheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUncheck.Name = "btnUncheck";
            this.btnUncheck.Size = new System.Drawing.Size(98, 33);
            this.btnUncheck.TabIndex = 190;
            this.btnUncheck.Text = "Uncheck";
            this.btnUncheck.UseVisualStyleBackColor = true;
            this.btnUncheck.Click += new System.EventHandler(this.btnUncheck_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(7, 15);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(98, 33);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "Check All";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.TcountNotset);
            this.groupBox5.Controls.Add(this.btnExport);
            this.groupBox5.Controls.Add(this.btnIssueJR);
            this.groupBox5.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.groupBox5.Location = new System.Drawing.Point(241, 119);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(915, 54);
            this.groupBox5.TabIndex = 190;
            this.groupBox5.TabStop = false;
            // 
            // TcountNotset
            // 
            this.TcountNotset.AutoSize = true;
            this.TcountNotset.Location = new System.Drawing.Point(94, 25);
            this.TcountNotset.Name = "TcountNotset";
            this.TcountNotset.Size = new System.Drawing.Size(21, 13);
            this.TcountNotset.TabIndex = 191;
            this.TcountNotset.Text = "..";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Fira Code", 8.249999F);
            this.btnExport.Location = new System.Drawing.Point(823, 15);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(82, 33);
            this.btnExport.TabIndex = 190;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnIssueJR
            // 
            this.btnIssueJR.Location = new System.Drawing.Point(8, 15);
            this.btnIssueJR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnIssueJR.Name = "btnIssueJR";
            this.btnIssueJR.Size = new System.Drawing.Size(79, 33);
            this.btnIssueJR.TabIndex = 0;
            this.btnIssueJR.Text = "Issue JR";
            this.btnIssueJR.UseVisualStyleBackColor = true;
            this.btnIssueJR.Click += new System.EventHandler(this.btnIssueJR_Click);
            // 
            // groupgrid
            // 
            this.groupgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupgrid.Controls.Add(this.lblRows);
            this.groupgrid.Controls.Add(this.dataGridView1);
            this.groupgrid.Location = new System.Drawing.Point(14, 179);
            this.groupgrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupgrid.Name = "groupgrid";
            this.groupgrid.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupgrid.Size = new System.Drawing.Size(1142, 385);
            this.groupgrid.TabIndex = 191;
            this.groupgrid.TabStop = false;
            this.groupgrid.Visible = false;
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(7, 369);
            this.lblRows.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(98, 13);
            this.lblRows.TabIndex = 137;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MediumTurquoise;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Fira Code", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk2});
            this.dataGridView1.Location = new System.Drawing.Point(7, 11);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1128, 355);
            this.dataGridView1.TabIndex = 183;
            this.dataGridView1.Visible = false;
            // 
            // chk2
            // 
            this.chk2.HeaderText = "Check";
            this.chk2.Name = "chk2";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FManualJobRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 576);
            this.Controls.Add(this.groupgrid);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Fira Code", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FManualJobRequest";
            this.Text = "Manual Job Request";
            this.Load += new System.EventHandler(this.FManualJobRequest_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupgrid.ResumeLayout(false);
            this.groupgrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStatusMB52;
        private System.Windows.Forms.Button btnUpJR;
        private System.Windows.Forms.Button btnformclose;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnUncheck;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnIssueJR;
        private System.Windows.Forms.GroupBox groupgrid;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label TcountNotset;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}