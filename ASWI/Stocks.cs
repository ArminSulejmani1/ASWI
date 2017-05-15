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
    public partial class Stocks : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Stocks()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            cmbsupplier.SelectedIndex = -1;
            cmbproduct.SelectedIndex = -1;
            txtquantity.Text = "";
            txtstockID.Text = "";
            txtproductID.Text = "";
            txtsupplierID.Text = "";
            txtID.Text = "";
            dtpDate.Text = System.DateTime.Now.ToString();
            txtstockID.Focus();
            btregister.Enabled = true;
            btedit.Enabled = false;
            btdelete.Enabled = false;
            auto();
        }

        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = "SELECT MAX(STID+1) FROM Stock";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                    txtstockID.Text = "DEPONIMI - " + Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                    txtstockID.Text = "DEPONIMI - " + Convert.ToString(Num);
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

        private void btcreate_Click(object sender, EventArgs e)
        {
            Reset();
            Reset1();
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbproduct.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni produktin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbproduct.Focus();
                    return;
                }
                if (cmbsupplier.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni furnizuesin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbsupplier.Focus();
                    return;
                }
                if (txtquantity.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani sasinë", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtquantity.Focus();
                    return;
                }
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string ct = "select PID from TempStock where PID=" + txtproductID.Text + "";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();

                if (cc.rdr.Read())
                {
                    cc.con = new SqlConnection(cs.Connect);
                    cc.con.Open();
                    string cb2 = "Update TempStock set Quantity=Quantity + " + txtquantity.Text + " where PID=" + txtproductID.Text + "";
                    cc.cmd = new SqlCommand(cb2);
                    cc.cmd.Connection = cc.con;
                    cc.cmd.ExecuteReader();
                    cc.con.Close();

                }
                else
                {
                    cc.con = new SqlConnection(cs.Connect);
                    cc.con.Open();
                    string cb1 = "insert into TempStock(PID,Quantity) VALUES (" + txtproductID.Text + "," + txtquantity.Text + ")";
                    cc.cmd = new SqlCommand(cb1);
                    cc.cmd.Connection = cc.con;

                    cc.cmd.ExecuteReader();
                    cc.con.Close();
                }
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "insert into Stock(STID,StockID,PID,SupplierID,Quantity,Date) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@d2", txtstockID.Text);
                cc.cmd.Parameters.AddWithValue("@d3", txtproductID.Text);
                cc.cmd.Parameters.AddWithValue("@d4", txtsupplierID.Text);
                cc.cmd.Parameters.AddWithValue("@d5", txtquantity.Text);
                cc.cmd.Parameters.AddWithValue("@d6", dtpDate.Value);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka bërë deponim të ri të produktit '" + cmbproduct.Text + "' me deponim : '" + txtstockID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u kry deponimi", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                if (cmbproduct.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni produktin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbproduct.Focus();
                    return;
                }
                if (cmbsupplier.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni furnizuesin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbsupplier.Focus();
                    return;
                }
                if (txtquantity.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani sasinë", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtquantity.Focus();
                    return;
                }
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cb = "UPDATE Stock SET PID='" + txtproductID.Text + "',SupplierID='" + txtsupplierID.Text + "',Date=@d1,Quantity='" + txtquantity.Text + "' WHERE STID='" + txtID.Text + "'";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", dtpDate.Value);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar deponimin e produktit '" + cmbproduct.Text + "' me deponi : '" + txtstockID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btedit.Enabled = false;
                MessageBox.Show("Me sukses u kry editimi", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("A jeni të sigurt?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }


        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string cq = "delete from Stock where STID='" + txtID.Text + "'";
                cc.cmd = new SqlCommand(cq);
                cc.cmd.Connection = cc.con;
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë deponimin e produktit '" + cmbproduct.Text + "' me deponim : '" + txtstockID.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Me sukses u bë fshirja", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Nuk ka të dhëna", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = cc.con.CreateCommand();
                cc.cmd.CommandText = "SELECT PID from Product where PName=@d1";
                cc.cmd.Parameters.AddWithValue("@d1", cmbproduct.Text);
                cc.rdr = cc.cmd.ExecuteReader();

                if (cc.rdr.Read())
                {
                    txtproductID.Text = cc.rdr.GetValue(0).ToString().Trim();
                }
                if ((cc.rdr != null))
                {
                    cc.rdr.Close();
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

        private void cmbsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = cc.con.CreateCommand();
                cc.cmd.CommandText = "SELECT SID from Supplier where SName=@d1";
                cc.cmd.Parameters.AddWithValue("@d1", cmbsupplier.Text);
                cc.rdr = cc.cmd.ExecuteReader();

                if (cc.rdr.Read())
                {
                    txtsupplierID.Text = cc.rdr.GetValue(0).ToString().Trim();
                }
                if ((cc.rdr != null))
                {
                    cc.rdr.Close();
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



        public void FillProductName()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "select RTRIM(PName) from Product order by PName";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    cmbproduct.Items.Add(cc.rdr[0]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillSupplier()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "select RTRIM(SName) from Supplier order by SName";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    cmbsupplier.Items.Add(cc.rdr[0]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Stocks_Load(object sender, EventArgs e)
        {
            FillProductName();
            FillSupplier();
            GetData();
            btregister.Enabled = false;
            btedit.Enabled = false;
            btdelete.Enabled = false;
        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(STID) as [ID],RTRIM(StockID) as [Deponim ID],Convert(DateTime,Date,131) as [Data dhe Ora],RTRIM(Stock.PID) as [Produkt ID],RTRIM(Product.PBarcode) as [Barkodi],RTRIM(PName) as [Produkti],RTRIM(Stock.SupplierID) as [Furnizues ID],RTRIM(SName) as [Furnizuesi],RTRIM(Quantity) as [Sasia] from Supplier,Product,Stock WHERE Stock.SupplierID=Supplier.SID and Stock.PID=Product.PID order by date", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Stock");
                dataGridView1.DataSource = cc.ds.Tables["Stock"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtproductsearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(STID) as [ID],RTRIM(StockID) as [Deponim ID],Convert(DateTime,Date,131) as [Data dhe Ora],RTRIM(Stock.PID) as [Produkt ID],RTRIM(Product.PBarcode) as [Barkodi],RTRIM(PName) as [Produkti],RTRIM(Stock.SupplierID) as [Furnizues ID],RTRIM(SName) as [Furnizuesi],RTRIM(Quantity) as [Sasia] from Supplier,Product,Stock WHERE Stock.SupplierID=Supplier.SID and Stock.PID=Product.PID AND PName like '" + txtproductsearch.Text + "%' order by PName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Stock");
                dataGridView1.DataSource = cc.ds.Tables["Stock"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Reset1()
        {
            txtproductsearch.Text = "";
            txtsuppliersearch.Text = "";
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
      
        }

        private void txtsuppliersearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(STID) as [ID],RTRIM(StockID) as [Deponim ID],Convert(DateTime,Date,131) as [Data dhe Ora],RTRIM(Stock.PID) as [Produkt ID],RTRIM(Product.PBarcode) as [Barkodi],RTRIM(PName) as [Produkti],RTRIM(Stock.SupplierID) as [Furnizues ID],RTRIM(SName) as [Furnizuesi],RTRIM(Quantity) as [Sasia] from Supplier,Product,Stock WHERE Stock.SupplierID=Supplier.SID and Stock.PID=Product.PID AND SName like '" + txtsuppliersearch.Text + "%' order by SName", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Stock");
                dataGridView1.DataSource = cc.ds.Tables["Stock"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];

            txtID.Text = dr.Cells[0].Value.ToString();
            txtstockID.Text = dr.Cells[1].Value.ToString();
            dtpDate.Text = dr.Cells[2].Value.ToString();
            txtproductID.Text = dr.Cells[3].Value.ToString();
            txtbarcode.Text = dr.Cells[4].Value.ToString();
            cmbproduct.Text = dr.Cells[5].Value.ToString();
            txtsupplierID.Text = dr.Cells[6].Value.ToString();
            cmbsupplier.Text = dr.Cells[7].Value.ToString();
            txtquantity.Text = dr.Cells[8].Value.ToString();

            btedit.Enabled = true;
            btdelete.Enabled = true;
            btregister.Enabled = false;
        }

         
    }
}
