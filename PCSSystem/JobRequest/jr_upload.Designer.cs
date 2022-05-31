namespace PCSSystem
{
    partial class jr_upload
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
            this.btnUplBF = new System.Windows.Forms.Button();
            this.btnUpl34 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPP34Rows = new System.Windows.Forms.Label();
            this.lblBFRows = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkPP34 = new System.Windows.Forms.CheckBox();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbPlant = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPP34Update = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btmMB52 = new System.Windows.Forms.Button();
            this.lblStatusMB52 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUplBF
            // 
            this.btnUplBF.Enabled = false;
            this.btnUplBF.Location = new System.Drawing.Point(194, 262);
            this.btnUplBF.Name = "btnUplBF";
            this.btnUplBF.Size = new System.Drawing.Size(118, 29);
            this.btnUplBF.TabIndex = 10;
            this.btnUplBF.Text = "Upload B/F";
            this.btnUplBF.UseVisualStyleBackColor = true;
            this.btnUplBF.Click += new System.EventHandler(this.btnUplBF_Click);
            // 
            // btnUpl34
            // 
            this.btnUpl34.Location = new System.Drawing.Point(202, 129);
            this.btnUpl34.Name = "btnUpl34";
            this.btnUpl34.Size = new System.Drawing.Size(118, 29);
            this.btnUpl34.TabIndex = 4;
            this.btnUpl34.Text = "Upload PP34";
            this.btnUpl34.UseVisualStyleBackColor = true;
            this.btnUpl34.Click += new System.EventHandler(this.btnUpl34_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "3. Upload the PP34";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "3. Upload the B/F Outstanding";
            this.label2.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(203, 181);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 29);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPP34Rows
            // 
            this.lblPP34Rows.AutoSize = true;
            this.lblPP34Rows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPP34Rows.ForeColor = System.Drawing.Color.Blue;
            this.lblPP34Rows.Location = new System.Drawing.Point(205, 161);
            this.lblPP34Rows.Name = "lblPP34Rows";
            this.lblPP34Rows.Size = new System.Drawing.Size(97, 17);
            this.lblPP34Rows.TabIndex = 8;
            this.lblPP34Rows.Text = "PP34 Uploaded!";
            this.lblPP34Rows.Visible = false;
            // 
            // lblBFRows
            // 
            this.lblBFRows.AutoSize = true;
            this.lblBFRows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBFRows.ForeColor = System.Drawing.Color.Blue;
            this.lblBFRows.Location = new System.Drawing.Point(197, 294);
            this.lblBFRows.Name = "lblBFRows";
            this.lblBFRows.Size = new System.Drawing.Size(83, 17);
            this.lblBFRows.TabIndex = 11;
            this.lblBFRows.Text = "B/F Uploaded";
            this.lblBFRows.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Text Files(*.txt)|*.txt";
            // 
            // chkPP34
            // 
            this.chkPP34.AutoSize = true;
            this.chkPP34.Location = new System.Drawing.Point(6, 316);
            this.chkPP34.Name = "chkPP34";
            this.chkPP34.Size = new System.Drawing.Size(129, 21);
            this.chkPP34.TabIndex = 5;
            this.chkPP34.Text = "Use existing PP34";
            this.chkPP34.UseVisualStyleBackColor = true;
            this.chkPP34.Visible = false;
            this.chkPP34.CheckedChanged += new System.EventHandler(this.chkPP34_CheckedChanged);
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(203, 36);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(74, 25);
            this.cbbProduct.TabIndex = 2;
            this.cbbProduct.SelectedIndexChanged += new System.EventHandler(this.cbbProduct_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Product";
            // 
            // cbbPlant
            // 
            this.cbbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPlant.FormattingEnabled = true;
            this.cbbPlant.Location = new System.Drawing.Point(50, 36);
            this.cbbPlant.Name = "cbbPlant";
            this.cbbPlant.Size = new System.Drawing.Size(88, 25);
            this.cbbPlant.TabIndex = 1;
            this.cbbPlant.SelectedIndexChanged += new System.EventHandler(this.cbbPlant_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Plant";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "1. Select the Plant and Product";
            // 
            // lblPP34Update
            // 
            this.lblPP34Update.AutoSize = true;
            this.lblPP34Update.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPP34Update.ForeColor = System.Drawing.Color.Blue;
            this.lblPP34Update.Location = new System.Drawing.Point(132, 317);
            this.lblPP34Update.Name = "lblPP34Update";
            this.lblPP34Update.Size = new System.Drawing.Size(162, 17);
            this.lblPP34Update.TabIndex = 13;
            this.lblPP34Update.Text = "2016-01-18 14:25 updated!";
            this.lblPP34Update.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "2. Upload MB52";
            // 
            // btmMB52
            // 
            this.btmMB52.Enabled = false;
            this.btmMB52.Location = new System.Drawing.Point(202, 76);
            this.btmMB52.Name = "btmMB52";
            this.btmMB52.Size = new System.Drawing.Size(118, 29);
            this.btmMB52.TabIndex = 3;
            this.btmMB52.Text = "Upload MB52";
            this.btmMB52.UseVisualStyleBackColor = true;
            this.btmMB52.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatusMB52
            // 
            this.lblStatusMB52.AutoSize = true;
            this.lblStatusMB52.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMB52.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusMB52.Location = new System.Drawing.Point(205, 107);
            this.lblStatusMB52.Name = "lblStatusMB52";
            this.lblStatusMB52.Size = new System.Drawing.Size(101, 17);
            this.lblStatusMB52.TabIndex = 16;
            this.lblStatusMB52.Text = "MB52 Uploaded!";
            // 
            // FUplData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 217);
            this.Controls.Add(this.lblStatusMB52);
            this.Controls.Add(this.btmMB52);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPP34Update);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbProduct);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbPlant);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkPP34);
            this.Controls.Add(this.lblBFRows);
            this.Controls.Add(this.lblPP34Rows);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUplBF);
            this.Controls.Add(this.btnUpl34);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FUplData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Load Plan: Upload PP34 and B/F Outstanding";
            this.Load += new System.EventHandler(this.FUplData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUplBF;
        private System.Windows.Forms.Button btnUpl34;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPP34Rows;
        private System.Windows.Forms.Label lblBFRows;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkPP34;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbPlant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPP34Update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btmMB52;
        private System.Windows.Forms.Label lblStatusMB52;
    }
}