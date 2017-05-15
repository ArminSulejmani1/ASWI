namespace ASWI
{
    partial class CompanyRegister
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtaddress = new System.Windows.Forms.TextBox();
            this.txtnum = new System.Windows.Forms.TextBox();
            this.txtnum2 = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtwebpage = new System.Windows.Forms.TextBox();
            this.txtmoto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btremove = new System.Windows.Forms.Button();
            this.btsearch = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbUSER = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btdelete = new System.Windows.Forms.Button();
            this.btedit = new System.Windows.Forms.Button();
            this.btregister = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Emri";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adresa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Numër 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Numër 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "E-mail";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 313);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Faqja online";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 364);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "Moto";
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(136, 33);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(246, 27);
            this.txtname.TabIndex = 7;
            this.txtname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtname.TextChanged += new System.EventHandler(this.txtname_TextChanged);
            // 
            // txtaddress
            // 
            this.txtaddress.Location = new System.Drawing.Point(136, 87);
            this.txtaddress.Name = "txtaddress";
            this.txtaddress.Size = new System.Drawing.Size(246, 27);
            this.txtaddress.TabIndex = 8;
            this.txtaddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtnum
            // 
            this.txtnum.Location = new System.Drawing.Point(136, 143);
            this.txtnum.Name = "txtnum";
            this.txtnum.Size = new System.Drawing.Size(246, 27);
            this.txtnum.TabIndex = 9;
            this.txtnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtnum2
            // 
            this.txtnum2.Location = new System.Drawing.Point(136, 199);
            this.txtnum2.Name = "txtnum2";
            this.txtnum2.Size = new System.Drawing.Size(246, 27);
            this.txtnum2.TabIndex = 10;
            this.txtnum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(136, 253);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(246, 27);
            this.txtemail.TabIndex = 11;
            this.txtemail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtemail.Validating += new System.ComponentModel.CancelEventHandler(this.txtemail_Validating);
            // 
            // txtwebpage
            // 
            this.txtwebpage.Location = new System.Drawing.Point(136, 307);
            this.txtwebpage.Name = "txtwebpage";
            this.txtwebpage.Size = new System.Drawing.Size(246, 27);
            this.txtwebpage.TabIndex = 12;
            this.txtwebpage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtmoto
            // 
            this.txtmoto.Location = new System.Drawing.Point(136, 358);
            this.txtmoto.Name = "txtmoto";
            this.txtmoto.Size = new System.Drawing.Size(246, 27);
            this.txtmoto.TabIndex = 13;
            this.txtmoto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(416, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 21);
            this.label8.TabIndex = 14;
            this.label8.Text = "Logo";
            // 
            // btremove
            // 
            this.btremove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btremove.BackColor = System.Drawing.Color.SteelBlue;
            this.btremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btremove.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btremove.ForeColor = System.Drawing.Color.White;
            this.btremove.Location = new System.Drawing.Point(602, 232);
            this.btremove.Name = "btremove";
            this.btremove.Size = new System.Drawing.Size(164, 28);
            this.btremove.TabIndex = 24;
            this.btremove.Text = "Largo";
            this.btremove.UseVisualStyleBackColor = false;
            this.btremove.Click += new System.EventHandler(this.btremove_Click);
            // 
            // btsearch
            // 
            this.btsearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btsearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btsearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btsearch.ForeColor = System.Drawing.Color.White;
            this.btsearch.Location = new System.Drawing.Point(420, 232);
            this.btsearch.Name = "btsearch";
            this.btsearch.Size = new System.Drawing.Size(164, 28);
            this.btsearch.TabIndex = 23;
            this.btsearch.Text = "Kërko";
            this.btsearch.UseVisualStyleBackColor = false;
            this.btsearch.Click += new System.EventHandler(this.btsearch_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbUSER
            // 
            this.lbUSER.AutoSize = true;
            this.lbUSER.Location = new System.Drawing.Point(4, 13);
            this.lbUSER.Name = "lbUSER";
            this.lbUSER.Size = new System.Drawing.Size(0, 21);
            this.lbUSER.TabIndex = 25;
            this.lbUSER.Visible = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(747, 7);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(21, 27);
            this.txtID.TabIndex = 26;
            this.txtID.Visible = false;
            // 
            // btdelete
            // 
            this.btdelete.BackColor = System.Drawing.Color.Red;
            this.btdelete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btdelete.ForeColor = System.Drawing.Color.White;
            this.btdelete.Image = global::ASWI.Properties.Resources.delete_32;
            this.btdelete.Location = new System.Drawing.Point(657, 268);
            this.btdelete.Name = "btdelete";
            this.btdelete.Size = new System.Drawing.Size(109, 116);
            this.btdelete.TabIndex = 29;
            this.btdelete.Text = "Fshij";
            this.btdelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btdelete.UseVisualStyleBackColor = false;
            this.btdelete.Click += new System.EventHandler(this.btdelete_Click);
            // 
            // btedit
            // 
            this.btedit.BackColor = System.Drawing.Color.Black;
            this.btedit.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btedit.ForeColor = System.Drawing.Color.White;
            this.btedit.Image = global::ASWI.Properties.Resources.edit_9_32;
            this.btedit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btedit.Location = new System.Drawing.Point(420, 329);
            this.btedit.Name = "btedit";
            this.btedit.Size = new System.Drawing.Size(231, 55);
            this.btedit.TabIndex = 28;
            this.btedit.Text = "Edito";
            this.btedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btedit.UseVisualStyleBackColor = false;
            this.btedit.Click += new System.EventHandler(this.btedit_Click);
            // 
            // btregister
            // 
            this.btregister.BackColor = System.Drawing.Color.Lime;
            this.btregister.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btregister.ForeColor = System.Drawing.Color.White;
            this.btregister.Image = global::ASWI.Properties.Resources.save_32;
            this.btregister.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btregister.Location = new System.Drawing.Point(420, 268);
            this.btregister.Name = "btregister";
            this.btregister.Size = new System.Drawing.Size(231, 55);
            this.btregister.TabIndex = 27;
            this.btregister.Text = "Regjistro";
            this.btregister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btregister.UseVisualStyleBackColor = false;
            this.btregister.Click += new System.EventHandler(this.btregister_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ASWI.Properties.Resources.whiteimage;
            this.pictureBox1.Location = new System.Drawing.Point(420, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(346, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // CompanyRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 407);
            this.Controls.Add(this.btdelete);
            this.Controls.Add(this.btedit);
            this.Controls.Add(this.btregister);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lbUSER);
            this.Controls.Add(this.btremove);
            this.Controls.Add(this.btsearch);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtmoto);
            this.Controls.Add(this.txtwebpage);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.txtnum2);
            this.Controls.Add(this.txtnum);
            this.Controls.Add(this.txtaddress);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "CompanyRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ndërmarrja";
            this.Load += new System.EventHandler(this.CompanyRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtaddress;
        private System.Windows.Forms.TextBox txtnum;
        private System.Windows.Forms.TextBox txtnum2;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtwebpage;
        private System.Windows.Forms.TextBox txtmoto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Button btremove;
        internal System.Windows.Forms.Button btsearch;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        internal System.Windows.Forms.Label lbUSER;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btregister;
        private System.Windows.Forms.Button btedit;
        private System.Windows.Forms.Button btdelete;
    }
}