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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ASWI
{
    public partial class Client : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Client()
        {
            InitializeComponent();
        }

        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = "SELECT MAX(ID+1) FROM Client";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                    
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                   

                }
                cc.cmd.Dispose();
                cc.con.Close();
                cc.con.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            auto();
            getData();
        }

        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtemail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtemail.Text))
                {
                    MessageBox.Show("E-mail jo valid", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani emrin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (txtcity.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani qytetin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcity.Focus();
                    return;
                }
                if (txtcontact.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani kontaktin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ins = "INSERT INTO Client(ID,Name,Address,City,Contact,Email) VALUES(@a1,@a2,@a3,@a4,@a5,@a6)";
                cc.cmd = new SqlCommand(ins);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@a1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@a2", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@a3", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@a4", txtcity.Text);
                cc.cmd.Parameters.AddWithValue("@a5", txtcontact.Text);
                cc.cmd.Parameters.AddWithValue("@a6", txtemail.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka regjistruar klientin me emër '" + txtname.Text + "' dhe ID : '" + txtID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u regjistrua klienti", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                getData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reset()
        {
            txtname.Text = "";
            txtaddress.Text = "";
            txtcity.Text = "";
            txtID.Text = "";
            txtemail.Text = "";
            txtcontact.Text = "";
            txtname.Focus();
            btedit.Enabled = false;
            btdelete.Enabled = false;
            btregister.Enabled = true;
            auto();
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "DELETE FROM Client WHERE ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë klientin  '" + txtname.Text + "' me ID : '" + txtID.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Me sukses është fshirë klienti", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Nuk ka të dhëna", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }
                getData();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("A jeni të sigurtë?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }

        private void getData()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT (ID) as [ID],(Name) as [Emri],(Address) as [Adresa],(City) as [Qyteti],(Contact) as [Kontakti],(Email) as [E-mail] FROM Client ORDER BY ID", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Client");
                dataGridView1.DataSource = cc.ds.Tables["Client"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT (ID) as [ID],(Name) as [Emri],(Address) as [Adresa],(City) as [Qyteti],(Contact) as [Kontakti],(Email) as [E-mail] FROM Client WHERE Name LIKE '" + txtsearch.Text + "%' ORDER BY ID", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Client");
                dataGridView1.DataSource = cc.ds.Tables["Client"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dgr = dataGridView1.SelectedRows[0];

            txtID.Text = dgr.Cells[0].Value.ToString();
            txtname.Text = dgr.Cells[1].Value.ToString();
            txtaddress.Text = dgr.Cells[2].Value.ToString();
            txtcity.Text = dgr.Cells[3].Value.ToString();
            txtcontact.Text = dgr.Cells[4].Value.ToString();
            txtemail.Text = dgr.Cells[5].Value.ToString();
            btedit.Enabled = true;
            btdelete.Enabled = true;
            btregister.Enabled = false;
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani emrin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (txtcity.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani qytetin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcity.Focus();
                    return;
                }
                if (txtcontact.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani kontaktin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ins = "UPDATE Client SET Name='" + txtname.Text + "',Address='" + txtaddress.Text + "',City='" + txtcity.Text + "',Contact='" + txtcontact.Text + "',Email='" + txtemail.Text + "' WHERE ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(ins);
                cc.cmd.Connection = cc.con;
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar klientin me emër '" + txtname.Text + "' dhe ID : '" + txtID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u editua klienti", "EDITIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                getData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btprint_Click(object sender, EventArgs e)
        {
           
        }

        private void btexport_Click(object sender, EventArgs e)
        {
            
        }
    }
}
