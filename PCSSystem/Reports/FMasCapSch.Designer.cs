namespace PCSSystem
{
    partial class FMasCapSch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMasCapSch));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNonWork = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSaturday = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorkDays = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbbCap = new System.Windows.Forms.ComboBox();
            this.dtpMCTo = new System.Windows.Forms.DateTimePicker();
            this.dtpMCFrom = new System.Windows.Forms.DateTimePicker();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpSchTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSchFrom = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNonWork);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSaturday);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtWorkDays);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(6, 354);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 142);
            this.groupBox1.TabIndex = 95;
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
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(238, 602);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(86, 17);
            this.lblRows.TabIndex = 94;
            this.lblRows.Text = "Total Rows: 0";
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
            this.dgvReport.Location = new System.Drawing.Point(238, 43);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(596, 552);
            this.dgvReport.TabIndex = 93;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(114, 533);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(118, 29);
            this.btnExport.TabIndex = 92;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(114, 177);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(118, 25);
            this.cbbPlant.TabIndex = 91;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 90;
            this.label7.Text = "Plant";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(114, 564);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 29);
            this.btnClose.TabIndex = 89;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(114, 502);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(118, 29);
            this.btnView.TabIndex = 88;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 86;
            this.label6.Text = "Capacity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 85;
            this.label5.Text = "Cap. Plan to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 83;
            this.label4.Text = "Cap. Plan from";
            // 
            // cbbCap
            // 
            this.cbbCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCap.FormattingEnabled = true;
            this.cbbCap.Location = new System.Drawing.Point(114, 276);
            this.cbbCap.Name = "cbbCap";
            this.cbbCap.Size = new System.Drawing.Size(118, 25);
            this.cbbCap.TabIndex = 87;
            // 
            // dtpMCTo
            // 
            this.dtpMCTo.CustomFormat = "yyyy-MM-dd";
            this.dtpMCTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMCTo.Location = new System.Drawing.Point(114, 81);
            this.dtpMCTo.Name = "dtpMCTo";
            this.dtpMCTo.Size = new System.Drawing.Size(118, 25);
            this.dtpMCTo.TabIndex = 84;
            this.dtpMCTo.ValueChanged += new System.EventHandler(this.dtpMCTo_ValueChanged);
            // 
            // dtpMCFrom
            // 
            this.dtpMCFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpMCFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMCFrom.Location = new System.Drawing.Point(114, 48);
            this.dtpMCFrom.Name = "dtpMCFrom";
            this.dtpMCFrom.Size = new System.Drawing.Size(118, 25);
            this.dtpMCFrom.TabIndex = 82;
            this.dtpMCFrom.ValueChanged += new System.EventHandler(this.dtpMCFrom_ValueChanged);
            // 
            // cbbModel
            // 
            this.cbbModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(114, 243);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(118, 25);
            this.cbbModel.TabIndex = 81;
            this.cbbModel.SelectedIndexChanged += new System.EventHandler(this.cbbModel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 80;
            this.label3.Text = "Product Group";
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(114, 210);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(118, 25);
            this.cbbProduct.TabIndex = 77;
            this.cbbProduct.SelectedIndexChanged += new System.EventHandler(this.cbbProduct_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 76;
            this.label1.Text = "Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 99;
            this.label2.Text = "Schedule to";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 17);
            this.label11.TabIndex = 97;
            this.label11.Text = "Schedule from";
            // 
            // dtpSchTo
            // 
            this.dtpSchTo.CustomFormat = "yyyy-MM-dd";
            this.dtpSchTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchTo.Location = new System.Drawing.Point(114, 145);
            this.dtpSchTo.Name = "dtpSchTo";
            this.dtpSchTo.Size = new System.Drawing.Size(118, 25);
            this.dtpSchTo.TabIndex = 98;
            this.dtpSchTo.ValueChanged += new System.EventHandler(this.dtpSchTo_ValueChanged);
            // 
            // dtpSchFrom
            // 
            this.dtpSchFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpSchFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchFrom.Location = new System.Drawing.Point(114, 112);
            this.dtpSchFrom.Name = "dtpSchFrom";
            this.dtpSchFrom.Size = new System.Drawing.Size(118, 25);
            this.dtpSchFrom.TabIndex = 96;
            this.dtpSchFrom.ValueChanged += new System.EventHandler(this.dtpSchFrom_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(8, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(421, 25);
            this.label16.TabIndex = 172;
            this.label16.Text = "Report: Master Capacity vs Schedule by Model";
            // 
            // FMasCapSch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 630);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dtpSchTo);
            this.Controls.Add(this.dtpSchFrom);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cbbPlant);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbCap);
            this.Controls.Add(this.dtpMCTo);
            this.Controls.Add(this.dtpMCFrom);
            this.Controls.Add(this.cbbModel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbProduct);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FMasCapSch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS-Master Capacity Plan vs Schedule by Model";
            this.Load += new System.EventHandler(this.FMasCapSch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNonWork;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSaturday;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorkDays;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRows;
        internal System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cbbCap;
        private System.Windows.Forms.DateTimePicker dtpMCTo;
        private System.Windows.Forms.DateTimePicker dtpMCFrom;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpSchTo;
        private System.Windows.Forms.DateTimePicker dtpSchFrom;
        private System.Windows.Forms.Label label16;
    }
}