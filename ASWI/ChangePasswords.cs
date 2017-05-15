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
    public partial class ChangePasswords : Form
    {

        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        ConnectionString cs = new ConnectionString();
        string st1;
        string st2;

        public ChangePasswords()
        {
            InitializeComponent();
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            try
            {
                int RowsAffected = 0;
                if ((txtuser.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Ju lutemi shkruani përdoruesin tuaj", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuser.Focus();
                    return;
                }
                if ((txtoldpassword.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Ju lutemi shkruani fjalëkalimin e vjetër", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtoldpassword.Focus();
                    return;
                }
                if ((txtnew.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Ju lutemi shkruani fjalëkalimin e ri", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnew.Focus();
                    return;
                }
                if ((txtnewre.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Ju lutemi konfirmoni fjalëkalimin e ri", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnewre.Focus();
                    return;
                }
                if ((txtnew.TextLength < 5))
                {
                    MessageBox.Show("Fjalëkalimi ri duhet të jetë mbi 5 karaktere", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnew.Text = "";
                    txtnewre.Text = "";
                    txtnew.Focus();
                    return;
                }
                else if ((txtnew.Text != txtnewre.Text))
                {
                    MessageBox.Show("Fjalëkalimi nuk përputhet", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnew.Text = "";
                    txtoldpassword.Text = "";
                    txtnewre.Text = "";
                    txtoldpassword.Focus();
                    return;
                }
                else if ((txtoldpassword.Text == txtnew.Text))
                {
                    MessageBox.Show("Fjalëkalimi është i njejtë me të vjetrin", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnew.Text = "";
                    txtnewre.Text = "";
                    txtnew.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string co = "UPDATE Login SET Password = '" + txtnew.Text + "' WHERE Username=@d1 and Password =@d2";
                cc.cmd = new SqlCommand(co);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtuser.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtoldpassword.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if ((RowsAffected > 0))
                {
                    st1 = txtuser.Text;
                    st2 = "Me sukses u ndryshua fjalëkalimi";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Fjalëkalimi u ndryshua me sukses", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtuser.Text = "";
                    txtnew.Text = "";
                    txtoldpassword.Text = "";
                    txtnewre.Text = "";
                    this.Hide();
                    Login login = new Login();
                    login.txtuser.Text = "";
                    login.txtpassword.Text = "";
                    login.txtuser.Focus();
                    login.Show();

                }
                else
                {
                    MessageBox.Show("Përdoruesi nuk egziston", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtuser.Text = "";
                    txtnew.Text = "";
                    txtoldpassword.Text = "";
                    txtnewre.Text = "";
                    txtuser.Focus();
                }
                if ((cc.con.State == ConnectionState.Open))
                {
                    cc.con.Close();
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
