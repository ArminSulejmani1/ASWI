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
    public partial class CompanyRegister : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public CompanyRegister()
        {
            InitializeComponent();
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
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btremove_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.whiteimage;
        }


        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "DELETE FROM Company WHERE ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë të dhënat e kompanisë";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Me sukses është fshirë", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Nuk ka të dhëna", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
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

        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = "SELECT MAX(ID) FROM Company";
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

        private void CompanyRegister_Load(object sender, EventArgs e)
        {
            auto();
            GetData();
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
                if (txtnum.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani numrin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnum.Focus();
                    return;
                }
        


                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "INSERT INTO Company(ID,Companyname,Address,Contact,Contactt,Email,Webpage,Moto,Logo) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtnum.Text);
                cc.cmd.Parameters.AddWithValue("@d5", txtnum2.Text);
                cc.cmd.Parameters.AddWithValue("@d6", txtemail.Text);
                cc.cmd.Parameters.AddWithValue("@d7", txtwebpage.Text);
                cc.cmd.Parameters.AddWithValue("@d8", txtmoto.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d9", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka regjistruar kompaninë";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u regjistrua", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Nuk mundeni të regjistroni më shumë se një kompani" ,"GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
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
                if (txtnum.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani numrin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnum.Focus();
                    return;
                }



                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "UPDATE Company SET Companyname=@d2,Address=@d3,Contact=@d4,Contactt=@d5,Email=@d6,Webpage=@d7,Moto=@d8,Logo=@d9 WHERE ID=@d1";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtnum.Text);
                cc.cmd.Parameters.AddWithValue("@d5", txtnum2.Text);
                cc.cmd.Parameters.AddWithValue("@d6", txtemail.Text);
                cc.cmd.Parameters.AddWithValue("@d7", txtwebpage.Text);
                cc.cmd.Parameters.AddWithValue("@d8", txtmoto.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d9", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar kompaninë";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u editua", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
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

            this.Close();
        }


        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string fkk = "SELECT Companyname,Address,Contact,Contactt,Email,Webpage,Moto,Logo FROM Company";
                cc.cmd = new SqlCommand(fkk);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                cc.rdr.Read();
                if (cc.rdr.HasRows)
                {
                    txtname.Text = (String)cc.rdr["Companyname"].ToString();
                    txtaddress.Text = (String)cc.rdr["Address"].ToString();
                    txtnum.Text = (String)cc.rdr["Contact"].ToString();
                    txtnum2.Text = (String)cc.rdr["Contactt"].ToString();
                    txtemail.Text = (String)cc.rdr["Email"].ToString();
                    txtwebpage.Text = (String)cc.rdr["Webpage"].ToString();
                    txtmoto.Text = (String)cc.rdr["Moto"].ToString();
                    byte[] data = (byte[])cc.rdr["Logo"];
                    MemoryStream ms = new MemoryStream(data);
                    pictureBox1.Image = Image.FromStream(ms);
                 
                }

                cc.rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            

        }
    }
}
