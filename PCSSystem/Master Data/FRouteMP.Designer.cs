namespace PCSSystem
{
    partial class FRouteMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRouteMP));
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblRows = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbbFilter = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCriteria = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblAll = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancelE = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbbFPlant = new System.Windows.Forms.ComboBox();
            this.cbbPrd = new System.Windows.Forms.ComboBox();
            this.cbbLine = new System.Windows.Forms.ComboBox();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.txtOH = new System.Windows.Forms.TextBox();
            this.txtMP = new System.Windows.Forms.TextBox();
            this.txtEff = new System.Windows.Forms.TextBox();
            this.txtOMH = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
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
            this.dgvReport.Location = new System.Drawing.Point(6, 258);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(785, 239);
            this.dgvReport.TabIndex = 29;
            this.dgvReport.SelectionChanged += new System.EventHandler(this.dgvReport_SelectionChanged);
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(7, 507);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(85, 17);
            this.lblRows.TabIndex = 30;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(676, 501);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 29);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbbFilter
            // 
            this.cbbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFilter.FormattingEnabled = true;
            this.cbbFilter.Location = new System.Drawing.Point(189, 226);
            this.cbbFilter.Name = "cbbFilter";
            this.cbbFilter.Size = new System.Drawing.Size(118, 25);
            this.cbbFilter.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(147, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Filter";
            // 
            // txtCriteria
            // 
            this.txtCriteria.Location = new System.Drawing.Point(369, 226);
            this.txtCriteria.Name = "txtCriteria";
            this.txtCriteria.Size = new System.Drawing.Size(300, 25);
            this.txtCriteria.TabIndex = 27;
            this.txtCriteria.TextChanged += new System.EventHandler(this.txtCriteria_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "Criteria";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(565, 501);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 29);
            this.btnExport.TabIndex = 33;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(454, 501);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 29);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Text Files(*.txt)|*.txt";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Location = new System.Drawing.Point(246, 507);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(204, 18);
            this.txtStatus.TabIndex = 31;
            this.txtStatus.TabStop = false;
            this.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStatus.Click += new System.EventHandler(this.txtStatus_Click);
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblAll.Location = new System.Drawing.Point(671, 229);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(119, 17);
            this.lblAll.TabIndex = 28;
            this.lblAll.Text = "Type % to Show All";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(12, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(224, 25);
            this.label16.TabIndex = 0;
            this.label16.Text = "Routing and ManPower";
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(49, 226);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(88, 25);
            this.cbbPlant.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Plant";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(669, 74);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(669, 140);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(117, 30);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(668, 41);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(117, 30);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(668, 107);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(117, 30);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(669, 41);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 30);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCancelE
            // 
            this.btnCancelE.Location = new System.Drawing.Point(669, 107);
            this.btnCancelE.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancelE.Name = "btnCancelE";
            this.btnCancelE.Size = new System.Drawing.Size(117, 30);
            this.btnCancelE.TabIndex = 21;
            this.btnCancelE.Text = "Ca&ncel";
            this.btnCancelE.UseVisualStyleBackColor = true;
            this.btnCancelE.Visible = false;
            this.btnCancelE.Click += new System.EventHandler(this.btnCancelE_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Plant";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Prodn Line";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Product Grp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(238, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Output Hour";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Efficiency";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(238, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "Manpower";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // cbbFPlant
            // 
            this.cbbFPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFPlant.Enabled = false;
            this.cbbFPlant.FormattingEnabled = true;
            this.cbbFPlant.Location = new System.Drawing.Point(93, 50);
            this.cbbFPlant.Name = "cbbFPlant";
            this.cbbFPlant.Size = new System.Drawing.Size(135, 25);
            this.cbbFPlant.TabIndex = 1;
            this.cbbFPlant.SelectedIndexChanged += new System.EventHandler(this.cbbFPlant_SelectedIndexChanged);
            // 
            // cbbPrd
            // 
            this.cbbPrd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrd.Enabled = false;
            this.cbbPrd.FormattingEnabled = true;
            this.cbbPrd.Location = new System.Drawing.Point(93, 81);
            this.cbbPrd.Name = "cbbPrd";
            this.cbbPrd.Size = new System.Drawing.Size(135, 25);
            this.cbbPrd.TabIndex = 3;
            this.cbbPrd.SelectedIndexChanged += new System.EventHandler(this.cbbPrd_SelectedIndexChanged);
            // 
            // cbbLine
            // 
            this.cbbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLine.Enabled = false;
            this.cbbLine.FormattingEnabled = true;
            this.cbbLine.Location = new System.Drawing.Point(93, 144);
            this.cbbLine.Name = "cbbLine";
            this.cbbLine.Size = new System.Drawing.Size(135, 25);
            this.cbbLine.TabIndex = 7;
            // 
            // cbbModel
            // 
            this.cbbModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbModel.Enabled = false;
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(93, 113);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(135, 25);
            this.cbbModel.TabIndex = 5;
            // 
            // txtOH
            // 
            this.txtOH.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtOH.Enabled = false;
            this.txtOH.Location = new System.Drawing.Point(330, 113);
            this.txtOH.MaxLength = 4;
            this.txtOH.Name = "txtOH";
            this.txtOH.ReadOnly = true;
            this.txtOH.Size = new System.Drawing.Size(135, 25);
            this.txtOH.TabIndex = 13;
            this.txtOH.Text = "0";
            this.txtOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMP
            // 
            this.txtMP.Enabled = false;
            this.txtMP.Location = new System.Drawing.Point(330, 81);
            this.txtMP.MaxLength = 3;
            this.txtMP.Name = "txtMP";
            this.txtMP.Size = new System.Drawing.Size(135, 25);
            this.txtMP.TabIndex = 11;
            this.txtMP.Text = "0";
            this.txtMP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMP.TextChanged += new System.EventHandler(this.txtMP_TextChanged);
            this.txtMP.Enter += new System.EventHandler(this.txtOH_Enter);
            this.txtMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMP_KeyPress);
            this.txtMP.Leave += new System.EventHandler(this.txtOH_Leave);
            // 
            // txtEff
            // 
            this.txtEff.Enabled = false;
            this.txtEff.Location = new System.Drawing.Point(330, 144);
            this.txtEff.MaxLength = 6;
            this.txtEff.Name = "txtEff";
            this.txtEff.Size = new System.Drawing.Size(135, 25);
            this.txtEff.TabIndex = 15;
            this.txtEff.Text = "0.00";
            this.txtEff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEff.Enter += new System.EventHandler(this.txtEff_Enter);
            this.txtEff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEff_KeyPress);
            this.txtEff.Leave += new System.EventHandler(this.txtEff_Leave);
            // 
            // txtOMH
            // 
            this.txtOMH.Enabled = false;
            this.txtOMH.Location = new System.Drawing.Point(330, 50);
            this.txtOMH.MaxLength = 3;
            this.txtOMH.Name = "txtOMH";
            this.txtOMH.Size = new System.Drawing.Size(135, 25);
            this.txtOMH.TabIndex = 9;
            this.txtOMH.Text = "0";
            this.txtOMH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOMH.Enter += new System.EventHandler(this.txtOMH_Enter);
            this.txtOMH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOMH_KeyPress);
            this.txtOMH.Leave += new System.EventHandler(this.txtOMH_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(238, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "OMH";
            // 
            // FRouteMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 535);
            this.Controls.Add(this.txtOMH);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEff);
            this.Controls.Add(this.txtMP);
            this.Controls.Add(this.txtOH);
            this.Controls.Add(this.cbbModel);
            this.Controls.Add(this.cbbLine);
            this.Controls.Add(this.cbbPrd);
            this.Controls.Add(this.cbbFPlant);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCancelE);
            this.Controls.Add(this.cbbPlant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblAll);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCriteria);
            this.Controls.Add(this.cbbFilter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvReport);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FRouteMP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS-Routing and Man Power";
            this.Load += new System.EventHandler(this.FRouteMP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbbFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCriteria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnEdit;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnCancelE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbFPlant;
        private System.Windows.Forms.ComboBox cbbPrd;
        private System.Windows.Forms.ComboBox cbbLine;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.TextBox txtOH;
        private System.Windows.Forms.TextBox txtMP;
        private System.Windows.Forms.TextBox txtEff;
        private System.Windows.Forms.TextBox txtOMH;
        private System.Windows.Forms.Label label11;
    }
}