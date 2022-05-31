namespace PCSSystem
{
    partial class FSelFG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSelFG));
            this.txtFGCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.ChkCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlant = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFGCode
            // 
            this.txtFGCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFGCode.Location = new System.Drawing.Point(120, 125);
            this.txtFGCode.MaxLength = 20;
            this.txtFGCode.Name = "txtFGCode";
            this.txtFGCode.Size = new System.Drawing.Size(205, 25);
            this.txtFGCode.TabIndex = 202;
            this.txtFGCode.TextChanged += new System.EventHandler(this.txtFGCode_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 201;
            this.label12.Text = "Search FG Code";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(10, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 25);
            this.label16.TabIndex = 203;
            this.label16.Text = "FG Selection";
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
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChkCol,
            this.Material,
            this.MatDesc});
            this.dgvReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.Location = new System.Drawing.Point(6, 156);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(537, 252);
            this.dgvReport.TabIndex = 204;
            this.dgvReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellContentClick);
            // 
            // ChkCol
            // 
            this.ChkCol.HeaderText = "";
            this.ChkCol.Name = "ChkCol";
            this.ChkCol.ReadOnly = true;
            this.ChkCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChkCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ChkCol.Width = 50;
            // 
            // Material
            // 
            this.Material.DataPropertyName = "Material";
            this.Material.HeaderText = "Material";
            this.Material.Name = "Material";
            this.Material.ReadOnly = true;
            // 
            // MatDesc
            // 
            this.MatDesc.DataPropertyName = "Description";
            this.MatDesc.HeaderText = "Description";
            this.MatDesc.Name = "MatDesc";
            this.MatDesc.ReadOnly = true;
            this.MatDesc.Width = 300;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(426, 412);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(117, 30);
            this.btnClose.TabIndex = 214;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(306, 412);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 30);
            this.btnSave.TabIndex = 213;
            this.btnSave.Text = "&OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlant);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtModel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 82);
            this.groupBox1.TabIndex = 215;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plants and Prdt Group";
            // 
            // txtPlant
            // 
            this.txtPlant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPlant.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtPlant.Location = new System.Drawing.Point(13, 43);
            this.txtPlant.MaxLength = 20;
            this.txtPlant.Name = "txtPlant";
            this.txtPlant.ReadOnly = true;
            this.txtPlant.Size = new System.Drawing.Size(79, 25);
            this.txtPlant.TabIndex = 222;
            this.txtPlant.TextChanged += new System.EventHandler(this.txtPlant_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 221;
            this.label2.Text = "Plant";
            // 
            // txtModel
            // 
            this.txtModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtModel.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtModel.Location = new System.Drawing.Point(98, 43);
            this.txtModel.MaxLength = 20;
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(228, 25);
            this.txtModel.TabIndex = 220;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 219;
            this.label1.Text = "Product Group";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(3, 419);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(86, 17);
            this.lblRows.TabIndex = 216;
            this.lblRows.Text = "Total Rows: 0";
            // 
            // FSelFG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 447);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtFGCode);
            this.Controls.Add(this.label12);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FSelFG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS-FGs Selection";
            this.Load += new System.EventHandler(this.FSelFG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFGCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.DataGridView dgvReport;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatDesc;
    }
}