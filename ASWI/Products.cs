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
    public partial class Products : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Products()
        {
            InitializeComponent();
        }


        public void FillCombo()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "SELECT Category FROM Category ORDER BY Category";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    cmbcategory.Items.Add(cc.rdr[0]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            Autocomplete();
            FillCombo();
        
            GetData1();
            btregister.Enabled = false;
            btedit.Enabled = false;
            btdelete.Enabled = false;
        }


        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cq = "DELETE FROM Product WHERE PID='" + txtID.Text + "'";
                cc.cmd = new SqlCommand(cq);
                cc.cmd.Connection = cc.con;
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë produktin  '" + txtbarcode.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    Reset();
                    Autocomplete();
                    MessageBox.Show("Me sukses është fshirë produkti", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Nuk ka të dhëna", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Autocomplete();
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

        private void Autocomplete()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT distinct PName FROM Product", cc.con);
                cc.ds = new DataSet();
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.da.Fill(cc.ds, "Product");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= cc.ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(cc.ds.Tables[0].Rows[i]["PName"].ToString());

                }
                txtname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtname.AutoCompleteCustomSource = col;
                txtname.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Reset()
        {
            txtbarcode.Text = "";
            txtID.Text = "";
            cmbcategory.SelectedIndex = -1;
            cmbtype.SelectedIndex = -1;
            txtname.Text = "";
            cmbtype.Text = "- Lloji -";
            cmbcategory.Text = "Selekto Kategorinë";
            txtquantity.Text = "";
            txtprice.Text = "";
            txtdescription.Text = "";
            txtvat.Text = "0";
            txtvatamount.Text = "0";
            
            pictureBox1.Image = Properties.Resources.no_image;
            btregister.Enabled = true;
            btdelete.Enabled = false;
            btedit.Enabled = false;
            txtbarcode.Focus();

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
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtvat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtauction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                MessageBox.Show("Ju lutemi shënoni emrin e produktit", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtname.Focus();
                return;
            }
            if (cmbcategory.Text == "")
            {
                MessageBox.Show("Ju lutemi zgjidhni kategorinë", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbcategory.Focus();
                return;
            }
            if (cmbtype.Text == "")
            {
                MessageBox.Show("Ju lutemi zgjidhni llojin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbtype.Focus();
                return;
            }
            if (txtprice.Text == "")
            {
                MessageBox.Show("Ju lutemi shkruani çmimin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtprice.Focus();
                return;
            }
            if (txtvat.Text == "")
            {
                MessageBox.Show("Ju lutemi shkruani TVSH-në", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtvat.Focus();
                return;
            }

            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "select PName from Product where PName=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtname.Text);
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    MessageBox.Show("Produkti ekziston", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtname.Text = "";
                    txtname.Focus();
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "insert into Product(PBarcode,PName,PCategory,PQuantity,PType,PDescription,PPrice,PVAT,PVATAmount,PPhoto) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;

                cc.cmd.Parameters.AddWithValue("@a1", txtbarcode.Text);
                cc.cmd.Parameters.AddWithValue("@a2", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@a3", cmbcategory.Text);
                cc.cmd.Parameters.AddWithValue("@a4", txtquantity.Text);
                cc.cmd.Parameters.AddWithValue("@a5", cmbtype.Text);
                cc.cmd.Parameters.AddWithValue("@a6", txtdescription.Text);
                cc.cmd.Parameters.AddWithValue("@a7", txtprice.Text);
                cc.cmd.Parameters.AddWithValue("@a8", txtvat.Text);
                cc.cmd.Parameters.AddWithValue("@a9", txtvatamount.Text);
                
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@a10", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka shtuar një produkt të ri  '" + txtname.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                Autocomplete();
                btregister.Enabled = false;
                MessageBox.Show("Me sukses është bërë regjistrimi", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData1();
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
                    MessageBox.Show("Ju lutemi shënoni emrin e produktit", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtname.Focus();
                    return;
                }
                if (cmbcategory.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidhni kategorinë", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbcategory.Focus();
                    return;
                }
                if (txtprice.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani çmimin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtprice.Focus();
                    return;
                }
                if (txtvat.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani TVSH-në ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtvat.Focus();
                    return;
                }
           
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "Update Product set PBarcode='" + txtbarcode.Text + "',PName=@d1,PDescription=@d2,PPrice=" + txtprice.Text + ",PVAT=" + txtvat.Text + ",PVATAmount=" + txtvatamount.Text + ",PPhoto=@d3 where PID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtdescription.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d3", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar produktin : '" + txtbarcode.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                Autocomplete();
                btedit.Enabled = false;
                MessageBox.Show("Editimi u krye me sukses", "EDITIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void VatCalculate()
        {
            double val1 = 0;
            double val2 = 0;
            double val3 = 0;

            double.TryParse(txtprice.Text, out val1);
            double.TryParse(txtvat.Text, out val2);

            val3 = Convert.ToDouble((val1 * val2) / 100);
            val3 = Math.Round(val3, 2);
            txtvatamount.Text = val3.ToString();

        }

        private void txtvat_TextChanged(object sender, EventArgs e)
        {
            VatCalculate();
        }

        public void GetData1()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT (PID) as [ID],(PBarcode) as [BARKODI],(PName) as [PRODUKTI],(PCategory) as [KATEGORIA],(PQuantity) as [SASIA],(PType) as [LLOJI],(PDescription) as [PËRSHKRIMI],(PPrice) as [ÇMIMI],(PVAT) as [TVSH],(PVATAmount) as [VLERA E TVSH],PPhoto from Product order by PName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Product");
                dataGridView1.DataSource = cc.ds.Tables["Product"].DefaultView;
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
                cc.cmd = new SqlCommand("SELECT RTRIM(PID) as [ID],RTRIM(PBarcode) as [BARKODI],RTRIM(PName) as [PRODUKTI],RTRIM(PCategory) as [KATEGORIA],RTRIM(PQuantity) as [SASIA],RTRIM(PType) as [LLOJI],RTRIM(PDescription) as [PËRSHKRIMI],RTRIM(PPrice) as [Price],RTRIM(PVAT) as [TVSH],RTRIM(PVATAmount) as [VLERA TVSH],PPhoto from Product where PName LIKE '" + txtsearch.Text + "' ORDER BY PName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Product");
                dataGridView1.DataSource = cc.ds.Tables["Product"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];

            txtID.Text = dr.Cells[0].Value.ToString();
            txtbarcode.Text = dr.Cells[1].Value.ToString();
            txtname.Text = dr.Cells[2].Value.ToString();
            cmbcategory.Text = dr.Cells[3].Value.ToString();
            txtquantity.Text = dr.Cells[4].Value.ToString();
            cmbtype.Text = dr.Cells[5].Value.ToString();
            txtdescription.Text = dr.Cells[6].Value.ToString();
            txtprice.Text = dr.Cells[7].Value.ToString();
            txtvat.Text = dr.Cells[8].Value.ToString();
            txtvatamount.Text = dr.Cells[9].Value.ToString();
           
            byte[] data = (byte[])dr.Cells[10].Value;
            MemoryStream ms = new MemoryStream(data);
            pictureBox1.Image = Image.FromStream(ms);
            btedit.Enabled = true;
            btdelete.Enabled = true;
            btregister.Enabled = false;
            lbUSER.Text = lbUSER.Text;
        }

    }
}
