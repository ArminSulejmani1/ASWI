namespace ASWI
{
    partial class BillingRecords
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btexport = new System.Windows.Forms.Button();
            this.btreset = new System.Windows.Forms.Button();
            this.lbUSER = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDUE = new System.Windows.Forms.TextBox();
            this.txtPAYMENT = new System.Windows.Forms.TextBox();
            this.txtTOTAL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtsearch);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klienti";
            // 
            // txtsearch
            // 
            this.txtsearch.Location = new System.Drawing.Point(17, 27);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(250, 27);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btexport);
            this.groupBox2.Controls.Add(this.btreset);
            this.groupBox2.Location = new System.Drawing.Point(308, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Menu";
            // 
            // btexport
            // 
            this.btexport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btexport.BackColor = System.Drawing.Color.ForestGreen;
            this.btexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btexport.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btexport.ForeColor = System.Drawing.Color.White;
            this.btexport.Image = global::ASWI.Properties.Resources.excel_2_32;
            this.btexport.Location = new System.Drawing.Point(150, 27);
            this.btexport.Name = "btexport";
            this.btexport.Size = new System.Drawing.Size(130, 51);
            this.btexport.TabIndex = 29;
            this.btexport.Text = "Eksporto";
            this.btexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btexport.UseVisualStyleBackColor = false;
            this.btexport.Click += new System.EventHandler(this.btexport_Click);
            // 
            // btreset
            // 
            this.btreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btreset.BackColor = System.Drawing.Color.Firebrick;
            this.btreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btreset.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btreset.ForeColor = System.Drawing.Color.White;
            this.btreset.Image = global::ASWI.Properties.Resources.undo_5_32;
            this.btreset.Location = new System.Drawing.Point(14, 27);
            this.btreset.Name = "btreset";
            this.btreset.Size = new System.Drawing.Size(130, 51);
            this.btreset.TabIndex = 28;
            this.btreset.Text = "Rifillo";
            this.btreset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btreset.UseVisualStyleBackColor = false;
            this.btreset.Click += new System.EventHandler(this.btreset_Click);
            // 
            // lbUSER
            // 
            this.lbUSER.AutoSize = true;
            this.lbUSER.Location = new System.Drawing.Point(629, 25);
            this.lbUSER.Name = "lbUSER";
            this.lbUSER.Size = new System.Drawing.Size(0, 21);
            this.lbUSER.TabIndex = 2;
            this.lbUSER.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtDUE);
            this.groupBox3.Controls.Add(this.txtPAYMENT);
            this.groupBox3.Controls.Add(this.txtTOTAL);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(801, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(371, 432);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Të dhënat e faturave";
            // 
            // txtDUE
            // 
            this.txtDUE.BackColor = System.Drawing.Color.Black;
            this.txtDUE.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDUE.ForeColor = System.Drawing.Color.White;
            this.txtDUE.Location = new System.Drawing.Point(177, 352);
            this.txtDUE.Name = "txtDUE";
            this.txtDUE.Size = new System.Drawing.Size(188, 47);
            this.txtDUE.TabIndex = 5;
            this.txtDUE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPAYMENT
            // 
            this.txtPAYMENT.BackColor = System.Drawing.Color.Black;
            this.txtPAYMENT.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAYMENT.ForeColor = System.Drawing.Color.White;
            this.txtPAYMENT.Location = new System.Drawing.Point(177, 219);
            this.txtPAYMENT.Name = "txtPAYMENT";
            this.txtPAYMENT.Size = new System.Drawing.Size(188, 47);
            this.txtPAYMENT.TabIndex = 4;
            this.txtPAYMENT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTOTAL
            // 
            this.txtTOTAL.BackColor = System.Drawing.Color.Black;
            this.txtTOTAL.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTOTAL.ForeColor = System.Drawing.Color.White;
            this.txtTOTAL.Location = new System.Drawing.Point(177, 104);
            this.txtTOTAL.Name = "txtTOTAL";
            this.txtTOTAL.Size = new System.Drawing.Size(188, 47);
            this.txtTOTAL.TabIndex = 3;
            this.txtTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 358);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 41);
            this.label3.TabIndex = 2;
            this.label3.Text = "BORXHI :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "PAGESA :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "TOTALI :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 104);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(768, 495);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // BillingRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbUSER);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "BillingRecords";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Faturat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BillingRecords_FormClosing);
            this.Load += new System.EventHandler(this.BillingRecords_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btexport;
        private System.Windows.Forms.Button btreset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtDUE;
        private System.Windows.Forms.TextBox txtPAYMENT;
        private System.Windows.Forms.TextBox txtTOTAL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lbUSER;
    }
}