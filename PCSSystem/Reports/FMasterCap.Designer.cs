namespace PCSSystem
{
    partial class FMasterCap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMasterCap));
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.cbbLine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cbbCap = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblRows = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNonWork = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSaturday = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorkDays = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label16 = new System.Windows.Forms.Label();
            this.chkEff = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product";
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(115, 144);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(118, 25);
            this.cbbProduct.TabIndex = 1;
            this.cbbProduct.SelectedIndexChanged += new System.EventHandler(this.cbbProduct_SelectedIndexChanged);
            // 
            // cbbLine
            // 
            this.cbbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLine.FormattingEnabled = true;
            this.cbbLine.Location = new System.Drawing.Point(115, 177);
            this.cbbLine.Name = "cbbLine";
            this.cbbLine.Size = new System.Drawing.Size(118, 25);
            this.cbbLine.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prodn Line";
            // 
            // cbbModel
            // 
            this.cbbModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(115, 210);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(118, 25);
            this.cbbModel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Product Group";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(115, 45);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(118, 25);
            this.dtpFrom.TabIndex = 6;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cap. Plan from";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cap. Plan to";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "yyyy-MM-dd";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(115, 78);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(118, 25);
            this.dtpTo.TabIndex = 8;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // cbbCap
            // 
            this.cbbCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCap.FormattingEnabled = true;
            this.cbbCap.Location = new System.Drawing.Point(115, 243);
            this.cbbCap.Name = "cbbCap";
            this.cbbCap.Size = new System.Drawing.Size(118, 25);
            this.cbbCap.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Capacity";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(115, 460);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(118, 29);
            this.btnView.TabIndex = 12;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(115, 522);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 29);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(115, 111);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(118, 25);
            this.cbbPlant.TabIndex = 15;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Plant";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(115, 491);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(118, 29);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.Location = new System.Drawing.Point(239, 45);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(596, 507);
            this.dgvReport.TabIndex = 73;
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(239, 559);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(86, 17);
            this.lblRows.TabIndex = 74;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNonWork);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSaturday);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtWorkDays);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(7, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 142);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            // 
            // txtNonWork
            // 
            this.txtNonWork.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtNonWork.Location = new System.Drawing.Point(149, 96);
            this.txtNonWork.Name = "txtNonWork";
            this.txtNonWork.ReadOnly = true;
            this.txtNonWork.Size = new System.Drawing.Size(44, 25);
            this.txtNonWork.TabIndex = 86;
            this.txtNonWork.Text = "0";
            this.txtNonWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 17);
            this.label10.TabIndex = 85;
            this.label10.Text = "Nonworking Days";
            // 
            // txtSaturday
            // 
            this.txtSaturday.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSaturday.Location = new System.Drawing.Point(149, 63);
            this.txtSaturday.Name = "txtSaturday";
            this.txtSaturday.ReadOnly = true;
            this.txtSaturday.Size = new System.Drawing.Size(44, 25);
            this.txtSaturday.TabIndex = 84;
            this.txtSaturday.Text = "0";
            this.txtSaturday.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 17);
            this.label9.TabIndex = 83;
            this.label9.Text = "Saturdays";
            // 
            // txtWorkDays
            // 
            this.txtWorkDays.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtWorkDays.Location = new System.Drawing.Point(149, 31);
            this.txtWorkDays.Name = "txtWorkDays";
            this.txtWorkDays.ReadOnly = true;
            this.txtWorkDays.Size = new System.Drawing.Size(44, 25);
            this.txtWorkDays.TabIndex = 82;
            this.txtWorkDays.Text = "0";
            this.txtWorkDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 81;
            this.label8.Text = "Working Days";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(12, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(268, 25);
            this.label16.TabIndex = 173;
            this.label16.Text = "Report: Master Capacity Plan";
            // 
            // chkEff
            // 
            this.chkEff.AutoSize = true;
            this.chkEff.Location = new System.Drawing.Point(12, 277);
            this.chkEff.Name = "chkEff";
            this.chkEff.Size = new System.Drawing.Size(125, 21);
            this.chkEff.TabIndex = 174;
            this.chkEff.Text = "Include Efficiency";
            this.chkEff.UseVisualStyleBackColor = true;
            // 
            // FMasterCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 585);
            this.Controls.Add(this.chkEff);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cbbPlant);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.cbbCap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbbModel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbProduct);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FMasterCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS-Master Capacity Plan";
            this.Load += new System.EventHandler(this.FMasterCap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.ComboBox cbbLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox cbbCap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExport;
        internal System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNonWork;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSaturday;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorkDays;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkEff;
    }
}

