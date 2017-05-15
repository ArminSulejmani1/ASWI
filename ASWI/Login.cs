using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ASWI
{
    public partial class Login : Form
    {
        ConnectionString cs = new ConnectionString();
        MainMenu aswi = new MainMenu();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Login()
        {
            InitializeComponent();
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btlogin_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                MessageBox.Show("Ju lutemi shkruani Përdoruesin tuaj", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtuser.Focus();
                return;
            }
            if (txtpassword.Text == "")
            {
                MessageBox.Show("Ju lutemi shkruani fjalëkalimin tuaj", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpassword.Focus();
                return;
            }
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = cc.con.CreateCommand();
                cc.cmd.CommandText = "SELECT RTRIM(Username),RTRIM(Password) FROM Login where Username = @d1 and Password=@d2";
                cc.cmd.Parameters.AddWithValue("@d1", txtuser.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtpassword.Text);
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    cc.con = new SqlConnection(cs.Connect);
                    cc.con.Open();
                    cc.cmd = cc.con.CreateCommand();
                    cc.cmd.CommandText = "SELECT Role FROM Login where Username=@d3 and Password=@d4";
                    cc.cmd.Parameters.AddWithValue("@d3", txtuser.Text);
                    cc.cmd.Parameters.AddWithValue("@d4", txtpassword.Text);
                    cc.rdr = cc.cmd.ExecuteReader();
                    if (cc.rdr.Read())
                    {
                        txttype.Text = cc.rdr.GetValue(0).ToString().Trim();
                    }
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    if (cc.con.State == ConnectionState.Open)
                    {
                        cc.con.Close();
                    }
                    if ((txttype.Text == "ADMINISTRATOR"))
                    {

                        aswi.lbUSER.Text = txtuser.Text;
                        aswi.lbtype.Text = txttype.Text;
                        progressBar1.Visible = true;
                        progressBar1.Maximum = 5000;
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 4;
                        progressBar1.Step = 1;
                        for (int i = 0; i <= 5000; i++)
                        {
                            progressBar1.PerformStep();
                        }
                        st1 = txtuser.Text;
                        st2 = "Me sukses jeni lloguar ";
                        cf.LogFunc(st1, System.DateTime.Now, st2);
                        this.Hide();
                        aswi.Show();

                    }
                    if ((txttype.Text == "PUNËTOR"))
                    {

                        aswi.lbUSER.Text = txtuser.Text;
                        aswi.lbtype.Text = txttype.Text;
                        progressBar1.Visible = true;
                        progressBar1.Maximum = 5000;
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 4;
                        progressBar1.Step = 1;
                        for (int i = 0; i <= 5000; i++)
                        {
                            progressBar1.PerformStep();
                        }
                        st1 = txtuser.Text;
                        st2 = "Me sukses jeni lloguar ";
                        cf.LogFunc(st1, System.DateTime.Now, st2);
                        this.Hide();
                        aswi.toolStripButton7.Enabled = false;
                        aswi.toolStripButton5.Enabled = false;
                        aswi.toolStripButton4.Enabled = false;
                        aswi.furnizuesitToolStripMenuItem.Enabled = false;
                        aswi.administrimiToolStripMenuItem.Enabled = false;
                        aswi.punëtorëtToolStripMenuItem.Enabled = false;
                        aswi.Show();

                    }
                
                           if ((txttype.Text == "PËRDORUES"))
                    {

                        aswi.lbUSER.Text = txtuser.Text;
                        aswi.lbtype.Text = txttype.Text;
                        progressBar1.Visible = true;
                        progressBar1.Maximum = 5000;
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 4;
                        progressBar1.Step = 1;
                        for (int i = 0; i <= 5000; i++)
                        {
                            progressBar1.PerformStep();
                        }
                        st1 = txtuser.Text;
                        st2 = "Me sukses jeni lloguar ";
                        cf.LogFunc(st1, System.DateTime.Now, st2);
                        this.Hide();
                        aswi.toolStripButton7.Enabled = false;
                        aswi.administrimiToolStripMenuItem.Enabled = false;
                  
                        aswi.Show();

                    }
                
                }
                else
                {
                    MessageBox.Show("Llogimi dështoi...Provo Përsëri ", "NDALESË", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtuser.Text = "";
                    txtpassword.Text = "";
                    txtuser.Focus();
                }
                cc.cmd.Dispose();
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword fgpass = new ForgotPassword();
            fgpass.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePasswords chpas = new ChangePasswords();
            chpas.Show();
        }
    }
}
