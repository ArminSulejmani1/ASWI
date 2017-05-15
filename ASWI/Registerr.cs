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
    public partial class Registerr : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Registerr()
        {
            InitializeComponent();
        }

        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtemail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtemail.Text))
                {
                    MessageBox.Show("Shtypni email adresë valide ", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void Clear()
        {
            txtname.Clear();
            txtpassword.Clear();
            txtsurname.Clear();
            txtuser.Clear();
            txtemail.Clear();
            txtcontact.Clear();
            txtage.Clear();
            txtaddress.Clear();
            cmbrole.SelectedIndex = -1;
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni emrin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if (txtsurname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni mbiemrin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsurname.Focus();
                    return;
                }
                if (txtage.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni moshën", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtage.Focus();
                    return;
                }
                if (txtcontact.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni kontaktin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }
                if (txtemail.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni emailin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni adresën", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (txtuser.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni përdoruesin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuser.Focus();
                    return;
                }
                if (txtpassword.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni fjalëkalimin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpassword.Focus();
                    return;
                }
                if (cmbrole.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni rolin", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbrole.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "SELECT Username FROM Login WHERE Username=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtuser.Text);
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    MessageBox.Show("Përdoruesi egziston", "KUJDES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuser.Text = "";
                    txtuser.Focus();
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    return;
                }
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "INSERT INTO Login(Name,Surname,Age,Contact,Email,Address,Username,Password,Role,JoiningDate) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;

                cc.cmd.Parameters.AddWithValue("@d1", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtsurname.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtage.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtcontact.Text);
                cc.cmd.Parameters.AddWithValue("@d5", txtemail.Text);
                cc.cmd.Parameters.AddWithValue("@d6", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@d7", txtuser.Text);
                cc.cmd.Parameters.AddWithValue("@d8", txtpassword.Text);
                cc.cmd.Parameters.AddWithValue("@d9", cmbrole.Text);
                cc.cmd.Parameters.AddWithValue("@d10", System.DateTime.Now);
                cc.cmd.ExecuteReader();
                cc.con.Close();

                st1 = lbUSER.Text;
                st2 = "Përdoruesi ka regjistruar një përdorues të ri : '" + txtuser.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);

                btregister.Enabled = false;

                MessageBox.Show("Përdoruesi u regjistrua me sukses", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtname_Validating(object sender, CancelEventArgs e)
        {
        }

        private void txtuser_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_]");
            if (txtuser.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtuser.Text))
                {
                    MessageBox.Show("Vetëm shkronja,numbra dhe vija e poshtëm lejohen", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtuser.SelectAll();
                    e.Cancel = true;
                }
            }
        }
    }
}
