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
    public partial class ManageUsers : Form
    {
        CommonClasses cc = new CommonClasses();
        ConnectionString cs = new ConnectionString();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        ListViewItem lst;

        public ManageUsers()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txtaddress.Clear();
            txtage.Clear();
            txtcontact.Clear();
            txtemail.Clear();
            txtID.Clear();
            txtname.Clear();
            txtsurname.Clear();
            txtsearch.Clear();
            txtuser.Clear();
        }

        private void getData()
        {
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("ID", 50);
                listView1.Columns.Add("Emri", 100);
                listView1.Columns.Add("Mbiemri", 100);
                listView1.Columns.Add("Mosha", 100);
                listView1.Columns.Add("Kontakti", 100);
                listView1.Columns.Add("E-mail", 200);
                listView1.Columns.Add("Adresa", 150);
                listView1.Columns.Add("Përdoruesi", 100);

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string sql = @"SELECT * FROM Login WHERE Name LIKE '" + txtsearch.Text + "%'";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    lst = listView1.Items.Add(cc.rdr[0].ToString());
                    lst.SubItems.Add(cc.rdr[1].ToString());
                    lst.SubItems.Add(cc.rdr[2].ToString());
                    lst.SubItems.Add(cc.rdr[3].ToString());
                    lst.SubItems.Add(cc.rdr[4].ToString());
                    lst.SubItems.Add(cc.rdr[5].ToString());
                    lst.SubItems.Add(cc.rdr[6].ToString());
                    lst.SubItems.Add(cc.rdr[7].ToString());
                    lst.SubItems.Add(cc.rdr[8].ToString());
                }
                cc.rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            getData();
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Nuk ka përdorues të selektuar", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("A jeni të sigurtë?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    DeleteUser();
                }
            }
        }

        private void DeleteUser()
        {
            try
            {
                listView1.FocusedItem.Remove();

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string del = "DELETE FROM Login WHERE ID='" + txtID.Text + "'";

                cc.cmd = new SqlCommand(del);
                cc.cmd.Connection = cc.con;
                cc.cmd.ExecuteNonQuery();

                st1 = lbUSER.Text;
                st2 = "Përdoruesi ka fshirë përdoruesin : '" + txtuser.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);

                MessageBox.Show("Me sukses u krye fshirja", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();


            }
            catch (Exception)
            {
                MessageBox.Show("Nuk ka të dhëna", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtID.Text = listView1.FocusedItem.Text;
            txtname.Text = listView1.FocusedItem.SubItems[1].Text;
            txtsurname.Text = listView1.FocusedItem.SubItems[2].Text;
            txtage.Text = listView1.FocusedItem.SubItems[3].Text;
            txtcontact.Text = listView1.FocusedItem.SubItems[4].Text;
            txtemail.Text = listView1.FocusedItem.SubItems[5].Text;
            txtaddress.Text = listView1.FocusedItem.SubItems[6].Text;
            txtuser.Text = listView1.FocusedItem.SubItems[7].Text;
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btupdate_Click(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string update = @"UPDATE Login SET [Name] ='" + txtname.Text + "',[Surname] ='" + txtsurname.Text + "',[Age] ='" + txtage.Text + "',[Contact] ='" + txtcontact.Text + "',[Email] ='" + txtemail.Text + "',[Address] ='" + txtaddress.Text + "',[Username] ='" + txtuser.Text + "' WHERE ID ='" + txtID.Text + "'";
                cc.cmd = new SqlCommand(update);
                cc.cmd.Connection = cc.con;
                cc.cmd.ExecuteNonQuery();

                cc.con.Close();

                st1 = lbUSER.Text;
                st2 = "Përdoruesi ka edituar përdoruesin : '" + txtuser.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);


                Clear();
                MessageBox.Show("Përdoruesi u editua me sukses", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            getData();
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
    }
}
