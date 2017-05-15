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

namespace ASWI
{
    public partial class Suppliers : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Suppliers()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            txtemail.Text = "";
            txtname.Text = "";
            txtcontact.Text = "";
            cmbstate.Text = "";
            txtaddress.Text = "";
            txtsuppid.Text = "";
            txtID.Text = "";
            txtname.Focus();
            btregister.Enabled = true;
            btedit.Enabled = false;
            btdelete.Enabled = false;
            Picture.Image = Properties.Resources.no_image;
            auto();
        }

        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "DELETE FROM Supplier WHERE SID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë furnizuesin  '" + txtname.Text + "' me ID : '" + txtsuppid.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Me sukses është fshirë", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = "SELECT MAX(SID+1) FROM Supplier";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                    txtsuppid.Text = "Furnizuesi - " + Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                    txtsuppid.Text = "Furnizuesi - " + Convert.ToString(Num);
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

        private void btregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani emrin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (cmbstate.Text == "")
                {
                    MessageBox.Show("Ju lutemi selektoni shtetin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstate.Focus();
                    return;
                }
                if (txtcontact.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani kontakt nr.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }


                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "INSERT INTO Supplier(SID,SupplierID,SName,SAddress,SState,SContact,SEmail,SPhoto) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtsuppid.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@d5", cmbstate.Text);
                cc.cmd.Parameters.AddWithValue("@d6", txtcontact.Text);
                cc.cmd.Parameters.AddWithValue("@d7", txtemail.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(Picture.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d8", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka shtuar një furnizues  '" + txtname.Text + "' me ID : '" + txtsuppid.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u regjistrua", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
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
                if (txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani emrin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (cmbstate.Text == "")
                {
                    MessageBox.Show("Ju lutemi selektoni shtetin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstate.Focus();
                    return;
                }
                if (txtcontact.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani kontakt nr.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "UPDATE Supplier SET SupplierID=@d2,SName=@d3,SAddress=@d4,SState=@d5,SContact=@d6,SEmail=@d7,SPhoto=@d8 WHERE SID=@d1";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtsuppid.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@d5", cmbstate.Text);
                cc.cmd.Parameters.AddWithValue("@d6", txtcontact.Text);
                cc.cmd.Parameters.AddWithValue("@d7", txtemail.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(Picture.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d8", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar furnizuesin  '" + txtname.Text + "' me ID : '" + txtsuppid.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btedit.Enabled = false;
                MessageBox.Show("Me sukses u editua", "EDITIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btcreate_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            try
            {
                var _with1 = openFileDialog1;

                _with1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                _with1.FilterIndex = 4;
                //Reset the file name
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Picture.Image = Image.FromFile(openFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("A jeni të sigurtë?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }

        private void btremove_Click(object sender, EventArgs e)
        {
            Picture.Image = Properties.Resources.no_image;
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            btregister.Enabled = false;
            btedit.Enabled = false;
            btdelete.Enabled = false;
            Reset1();
           
        
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT (SID) as [ID],(SupplierID) as [Furnizues ID],(SName) as [Emri],(SAddress) as [Adresa],(SState) as [Shteti],(SContact) as [Kontakti],(SEmail) as [E-mail],SPhoto FROM Supplier ORDER BY SName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Supplier");
                dataGridView1.DataSource = cc.ds.Tables["Supplier"].DefaultView;
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
                cc.cmd = new SqlCommand("SELECT RTRIM(SID) as[ID],RTRIM(SupplierID) as [Furnizues ID],RTRIM(SName) as [Emri],RTRIM(SAddress) as [Adresa],RTRIM(SState) as [Shteti],RTRIM(SContact) as [Kontakt],RTRIM(SEmail) as [E-mail],SPhoto FROM Supplier WHERE SName LIKE '" + txtsearch.Text + "%' ORDER BY SName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Supplier");
                dataGridView1.DataSource = cc.ds.Tables["Supplier"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Reset1()
        {
            txtsearch.Text = "";
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
           
            txtID.Text = dr.Cells[0].Value.ToString();
            txtsuppid.Text = dr.Cells[1].Value.ToString();
            txtname.Text = dr.Cells[2].Value.ToString();
            txtaddress.Text = dr.Cells[3].Value.ToString();
            cmbstate.Text = dr.Cells[4].Value.ToString();
            txtcontact.Text = dr.Cells[5].Value.ToString();
            txtemail.Text = dr.Cells[6].Value.ToString();
            byte[] data = (byte[])dr.Cells[7].Value;
            MemoryStream ms = new MemoryStream(data);
            Picture.Image = Image.FromStream(ms);
            btedit.Enabled = true;
            btdelete.Enabled = true;
            btregister.Enabled = false;
          
        }



    }
}
