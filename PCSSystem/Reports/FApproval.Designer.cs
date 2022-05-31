namespace PCSSystem.Reports
{
    partial class FApproval
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
            this.btnApprove = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgRequestDetail = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgRequest = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Plant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobReq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReqDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReqBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRequestDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRequest)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApprove.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnApprove.Location = new System.Drawing.Point(910, 61);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(90, 39);
            this.btnApprove.TabIndex = 2;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgRequestDetail);
            this.groupBox2.Location = new System.Drawing.Point(15, 391);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(989, 271);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dgRequestDetail
            // 
            this.dgRequestDetail.AllowUserToAddRows = false;
            this.dgRequestDetail.AllowUserToDeleteRows = false;
            this.dgRequestDetail.AllowUserToResizeRows = false;
            this.dgRequestDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgRequestDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chckBox,
            this.Plant,
            this.Product,
            this.JobReq,
            this.PartCode,
            this.Qty,
            this.PartName,
            this.Reason,
            this.ReqDate,
            this.ReqBy});
            this.dgRequestDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRequestDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRequestDetail.Location = new System.Drawing.Point(3, 16);
            this.dgRequestDetail.Name = "dgRequestDetail";
            this.dgRequestDetail.Size = new System.Drawing.Size(983, 252);
            this.dgRequestDetail.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label16.Location = new System.Drawing.Point(14, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(158, 25);
            this.label16.TabIndex = 179;
            this.label16.Text = "--E APRROVAL--";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.PaleVioletRed;
            this.button1.Location = new System.Drawing.Point(803, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 39);
            this.button1.TabIndex = 180;
            this.button1.Text = "Not Approve";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // dgRequest
            // 
            this.dgRequest.AllowUserToAddRows = false;
            this.dgRequest.AllowUserToDeleteRows = false;
            this.dgRequest.AllowUserToResizeRows = false;
            this.dgRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgRequest.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRequest.Location = new System.Drawing.Point(6, 13);
            this.dgRequest.MultiSelect = false;
            this.dgRequest.Name = "dgRequest";
            this.dgRequest.ReadOnly = true;
            this.dgRequest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRequest.Size = new System.Drawing.Size(981, 205);
            this.dgRequest.TabIndex = 0;
            this.dgRequest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRequest_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgRequest);
            this.groupBox1.Location = new System.Drawing.Point(13, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(993, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.cbbProduct);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbbPlant);
            this.groupBox3.Location = new System.Drawing.Point(15, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(749, 67);
            this.groupBox3.TabIndex = 198;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(622, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 30);
            this.button2.TabIndex = 200;
            this.button2.Text = "View";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(118, 31);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(93, 21);
            this.cbbProduct.TabIndex = 194;
            this.cbbProduct.SelectedIndexChanged += new System.EventHandler(this.cbbProduct_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 195;
            this.label7.Text = "Plant";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 193;
            this.label2.Text = "Product";
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(19, 32);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(93, 21);
            this.cbbPlant.TabIndex = 196;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 201;
            this.label1.Text = "Job Req";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(217, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(367, 20);
            this.textBox1.TabIndex = 202;
            // 
            // chckBox
            // 
            this.chckBox.HeaderText = "";
            this.chckBox.Name = "chckBox";
            // 
            // Plant
            // 
            this.Plant.DataPropertyName = "Plant";
            this.Plant.HeaderText = "Plant";
            this.Plant.Name = "Plant";
            // 
            // Product
            // 
            this.Product.DataPropertyName = "Product";
            this.Product.HeaderText = "Product";
            this.Product.Name = "Product";
            // 
            // JobReq
            // 
            this.JobReq.DataPropertyName = "JobReq";
            this.JobReq.HeaderText = "JobReq";
            this.JobReq.Name = "JobReq";
            // 
            // PartCode
            // 
            this.PartCode.DataPropertyName = "PartCode";
            this.PartCode.HeaderText = "PartCode";
            this.PartCode.Name = "PartCode";
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // PartName
            // 
            this.PartName.DataPropertyName = "PartName";
            this.PartName.HeaderText = "PartName";
            this.PartName.Name = "PartName";
            // 
            // Reason
            // 
            this.Reason.DataPropertyName = "Reason";
            this.Reason.HeaderText = "Reason";
            this.Reason.Name = "Reason";
            // 
            // ReqDate
            // 
            this.ReqDate.DataPropertyName = "ReqDate";
            this.ReqDate.HeaderText = "ReqDate";
            this.ReqDate.Name = "ReqDate";
            // 
            // ReqBy
            // 
            this.ReqBy.DataPropertyName = "ReqBy";
            this.ReqBy.HeaderText = "ReqBy";
            this.ReqBy.Name = "ReqBy";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(15, 346);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 39);
            this.button3.TabIndex = 199;
            this.button3.Text = "&Select All";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 664);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.groupBox1);
            this.Name = "FApproval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FApproval";
            this.Load += new System.EventHandler(this.FApproval_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRequestDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRequest)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgRequestDetail;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.DataGridView dgRequest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobReq;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReqDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReqBy;
        private System.Windows.Forms.Button button3;
    }
}