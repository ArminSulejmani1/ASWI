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
    public partial class Categorys : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Categorys()
        {
            InitializeComponent();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            DataGridViewRow dr = dataGridView1.SelectedRows[0];

            txtID.Text = dr.Cells[0].Value.ToString();
            txtsearch.Text = dr.Cells[1].Value.ToString();
            btdelete.Enabled = true;
            btedit.Enabled = true;
            txtsearch.Focus();
            btregister.Enabled = false;
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            delete_records();
        }

        private void Categorys_Load(object sender, EventArgs e)
        {
            Autocomplete();
            GetData();

            btregister.Enabled = false;
            btedit.Enabled = false;
            btdelete.Enabled = false;
        }


        private void delete_records()
        {
            if (MessageBox.Show("A jeni të sigurtë?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int RowsAffected = 0;
                    cc.con = new SqlConnection(cs.Connect);
                    cc.con.Open();
                    string cq = "DELETE FROM Category WHERE CID=" + txtID.Text + "";
                    cc.cmd = new SqlCommand(cq);
                    cc.cmd.Connection = cc.con;
                    RowsAffected = cc.cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        st1 = lbUSER.Text;
                        st2 = "Ka fshi kategorinë  '" + txtsearch.Text + "'";
                        cf.LogFunc(st1, System.DateTime.Now, st2);
                        Reset();
                        Autocomplete();
                        GetData();
                        MessageBox.Show("Me sukses u fshij", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nuk ka të dhëna", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        Autocomplete();
                        GetData();
                    }
                    if (cc.con.State == ConnectionState.Open)
                    {
                        cc.con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Autocomplete()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT DISTINCT Category FROM Category", cc.con);
                cc.ds = new DataSet();
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.da.Fill(cc.ds, "Category");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= cc.ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(cc.ds.Tables[0].Rows[i]["Category"].ToString());

                }
                txtsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtsearch.AutoCompleteCustomSource = col;
                txtsearch.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            txtsearch.Text = "";
            txtID.Text = "";
            btregister.Enabled = true;
            btdelete.Enabled = false;
            btedit.Enabled = false;
            txtsearch.Focus();
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                MessageBox.Show("Ju lutemi shënoni kategorinë", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsearch.Focus();
                return;
            }
            try
            {

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "SELECT Category FROM Category WHERE Category=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtsearch.Text);
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    MessageBox.Show("Kategoria egziston", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtsearch.Text = "";
                    txtsearch.Focus();
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "INSERT INTO Category(Category) VALUES (@d1)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtsearch.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Krijoi kategori të re  '" + txtsearch.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                Autocomplete();
                GetData();
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u regjistrua kategoria", "KRIJIM", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    MessageBox.Show("Ju lutemi shënoni emrin e kategorisë", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsearch.Focus();
                    return;
                }
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "UPDATE Category SET Category=@d1 WHERE CID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtsearch.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Editoi kategorinë  '" + txtsearch.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                Autocomplete();
                GetData();
                btedit.Enabled = false;
                MessageBox.Show("Me sukse u editua kategoria", "EDITIM", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                String sql = "SELECT CID,Category FROM Category ORDER BY Category";
                cc.cmd = new SqlCommand(sql, cc.con);
                cc.rdr = cc.cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (cc.rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(cc.rdr[0], cc.rdr[1]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
