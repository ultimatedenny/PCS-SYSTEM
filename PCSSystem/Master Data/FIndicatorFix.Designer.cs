namespace PCSSystem.Master_Data
{
    partial class FIndicatorFix
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblRows = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancelE = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.cbbFGCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbLine = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbIndicatorType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLotInd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dpStartTime = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.dpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtFGDesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnSave.Location = new System.Drawing.Point(774, 92);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 29);
            this.btnSave.TabIndex = 208;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnDelete.Location = new System.Drawing.Point(774, 160);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(108, 29);
            this.btnDelete.TabIndex = 209;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnExport.Location = new System.Drawing.Point(434, 550);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 31);
            this.btnExport.TabIndex = 207;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRows.Location = new System.Drawing.Point(3, 557);
            this.lblRows.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(85, 17);
            this.lblRows.TabIndex = 206;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(794, 550);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 31);
            this.btnClose.TabIndex = 205;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.Location = new System.Drawing.Point(6, 267);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(911, 271);
            this.dgvReport.TabIndex = 204;
            this.dgvReport.SelectionChanged += new System.EventHandler(this.dgvReport_SelectionChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAdd.Location = new System.Drawing.Point(774, 60);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(108, 29);
            this.btnAdd.TabIndex = 212;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnEdit.Location = new System.Drawing.Point(774, 126);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(108, 29);
            this.btnEdit.TabIndex = 213;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancelE
            // 
            this.btnCancelE.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancelE.Location = new System.Drawing.Point(774, 126);
            this.btnCancelE.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnCancelE.Name = "btnCancelE";
            this.btnCancelE.Size = new System.Drawing.Size(108, 29);
            this.btnCancelE.TabIndex = 211;
            this.btnCancelE.Text = "Ca&ncel";
            this.btnCancelE.UseVisualStyleBackColor = true;
            this.btnCancelE.Visible = false;
            this.btnCancelE.Click += new System.EventHandler(this.btnCancelE_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(774, 60);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 29);
            this.btnCancel.TabIndex = 210;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(8, 5);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 25);
            this.label16.TabIndex = 221;
            this.label16.Text = "Fix Indication";
            // 
            // cbbFGCode
            // 
            this.cbbFGCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbFGCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbFGCode.FormattingEnabled = true;
            this.cbbFGCode.Location = new System.Drawing.Point(116, 184);
            this.cbbFGCode.Name = "cbbFGCode";
            this.cbbFGCode.Size = new System.Drawing.Size(227, 25);
            this.cbbFGCode.TabIndex = 266;
            this.cbbFGCode.SelectedIndexChanged += new System.EventHandler(this.cbbFGCode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 265;
            this.label2.Text = "FG Code";
            // 
            // cbbLine
            // 
            this.cbbLine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbLine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbLine.FormattingEnabled = true;
            this.cbbLine.Location = new System.Drawing.Point(116, 122);
            this.cbbLine.Name = "cbbLine";
            this.cbbLine.Size = new System.Drawing.Size(227, 25);
            this.cbbLine.TabIndex = 264;
            this.cbbLine.SelectedIndexChanged += new System.EventHandler(this.cbbLine_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 17);
            this.label18.TabIndex = 263;
            this.label18.Text = "Line";
            // 
            // cbbModel
            // 
            this.cbbModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(116, 153);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(227, 25);
            this.cbbModel.TabIndex = 262;
            this.cbbModel.SelectedIndexChanged += new System.EventHandler(this.cbbModel_SelectedIndexChanged);
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(116, 90);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(227, 25);
            this.cbbProduct.TabIndex = 260;
            this.cbbProduct.SelectedIndexChanged += new System.EventHandler(this.cbbProduct_SelectedIndexChanged);
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(116, 59);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(227, 25);
            this.cbbPlant.TabIndex = 258;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 261;
            this.label6.Text = "Product Grp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 259;
            this.label1.Text = "Product";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 257;
            this.label3.Text = "Plant";
            // 
            // cbbIndicatorType
            // 
            this.cbbIndicatorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIndicatorType.FormattingEnabled = true;
            this.cbbIndicatorType.Location = new System.Drawing.Point(497, 60);
            this.cbbIndicatorType.Name = "cbbIndicatorType";
            this.cbbIndicatorType.Size = new System.Drawing.Size(220, 25);
            this.cbbIndicatorType.TabIndex = 270;
            this.cbbIndicatorType.SelectedIndexChanged += new System.EventHandler(this.cbbIndicatorType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 269;
            this.label4.Text = "Indication Type";
            // 
            // txtLotInd
            // 
            this.txtLotInd.Location = new System.Drawing.Point(497, 91);
            this.txtLotInd.MaxLength = 1;
            this.txtLotInd.Name = "txtLotInd";
            this.txtLotInd.ReadOnly = true;
            this.txtLotInd.Size = new System.Drawing.Size(86, 25);
            this.txtLotInd.TabIndex = 272;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 271;
            this.label5.Text = "Lot Ind.";
            // 
            // dpEndTime
            // 
            this.dpEndTime.CustomFormat = "HH:mm";
            this.dpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpEndTime.Location = new System.Drawing.Point(654, 153);
            this.dpEndTime.Name = "dpEndTime";
            this.dpEndTime.ShowUpDown = true;
            this.dpEndTime.Size = new System.Drawing.Size(82, 25);
            this.dpEndTime.TabIndex = 278;
            // 
            // dpStartTime
            // 
            this.dpStartTime.CustomFormat = "HH:mm";
            this.dpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStartTime.Location = new System.Drawing.Point(654, 122);
            this.dpStartTime.Name = "dpStartTime";
            this.dpStartTime.ShowUpDown = true;
            this.dpStartTime.Size = new System.Drawing.Size(82, 25);
            this.dpStartTime.TabIndex = 277;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(392, 157);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 17);
            this.label24.TabIndex = 276;
            this.label24.Text = "End Date";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(392, 126);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(66, 17);
            this.label25.TabIndex = 275;
            this.label25.Text = "Start Date";
            // 
            // dpEndDate
            // 
            this.dpEndDate.CustomFormat = "yyyy-MM-dd";
            this.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpEndDate.Location = new System.Drawing.Point(497, 153);
            this.dpEndDate.Name = "dpEndDate";
            this.dpEndDate.Size = new System.Drawing.Size(151, 25);
            this.dpEndDate.TabIndex = 274;
            // 
            // dpStartDate
            // 
            this.dpStartDate.CustomFormat = "yyyy-MM-dd";
            this.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStartDate.Location = new System.Drawing.Point(497, 122);
            this.dpStartDate.Name = "dpStartDate";
            this.dpStartDate.Size = new System.Drawing.Size(151, 25);
            this.dpStartDate.TabIndex = 273;
            // 
            // txtFGDesc
            // 
            this.txtFGDesc.Location = new System.Drawing.Point(116, 215);
            this.txtFGDesc.MaxLength = 1;
            this.txtFGDesc.Name = "txtFGDesc";
            this.txtFGDesc.ReadOnly = true;
            this.txtFGDesc.Size = new System.Drawing.Size(227, 25);
            this.txtFGDesc.TabIndex = 280;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 279;
            this.label7.Text = "FG Name";
            // 
            // btnTemplate
            // 
            this.btnTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnTemplate.Location = new System.Drawing.Point(674, 550);
            this.btnTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(112, 31);
            this.btnTemplate.TabIndex = 281;
            this.btnTemplate.Text = "&Template";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnImport.Location = new System.Drawing.Point(554, 550);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(112, 31);
            this.btnImport.TabIndex = 282;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // FIndicatorFix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 592);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnTemplate);
            this.Controls.Add(this.txtFGDesc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dpEndTime);
            this.Controls.Add(this.dpStartTime);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.dpEndDate);
            this.Controls.Add(this.dpStartDate);
            this.Controls.Add(this.txtLotInd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbbIndicatorType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbFGCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbLine);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbbModel);
            this.Controls.Add(this.cbbProduct);
            this.Controls.Add(this.cbbPlant);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancelE);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FIndicatorFix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS-Fix Indication";
            this.Load += new System.EventHandler(this.FIndicatorFix_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.DataGridView dgvReport;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnEdit;
        internal System.Windows.Forms.Button btnCancelE;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbbFGCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbLine;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbIndicatorType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLotInd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpEndTime;
        private System.Windows.Forms.DateTimePicker dpStartTime;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DateTimePicker dpEndDate;
        private System.Windows.Forms.DateTimePicker dpStartDate;
        private System.Windows.Forms.TextBox txtFGDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.Button btnImport;
    }
}