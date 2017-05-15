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
    public partial class Invoice : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        int intQtyOrdered;

        public Invoice()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            txtdate.Text = DateTime.Now.ToLongDateString();
            txttime.Text = DateTime.Now.ToLongTimeString();
        }

        private void txtTOTAL_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtpayment_KeyPress(object sender, KeyPressEventArgs e)
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

        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = "SELECT MAX(ID+1) FROM Invoice";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                    txtbillno.Text = "AS04 - 00" + Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                    txtbillno.Text = "AS04 - 00" + Convert.ToString(Num);
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

        private void Invoice_Load(object sender, EventArgs e)
        {
            auto();
            FillCustomer();
            Fillcombobox();
            AutocompleteN();
            AutocompleteF();
            
        }

        private void cmbclient_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string klient = "SELECT * FROM Client WHERE Name='" + cmbclient.Text + "'";
                cc.cmd = new SqlCommand(klient);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();

                while (cc.rdr.Read())
                {
                    txtaddress.Text = cc.rdr["Address"].ToString();
                    txtcity.Text = cc.rdr["City"].ToString();
                    txtcontact.Text = cc.rdr["Contact"].ToString();
                    txtemail.Text = cc.rdr["Email"].ToString();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FillCustomer()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT Name FROM Client", cc.con);
                cc.ds = new DataSet();
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.da.Fill(cc.ds, "Client");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= cc.ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(cc.ds.Tables[0].Rows[i]["Name"].ToString());

                }
                cmbclient.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbclient.AutoCompleteCustomSource = col;
                cmbclient.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fillcombobox()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();

                string combo = "SELECT Name FROM Client";
                cc.cmd = new SqlCommand(combo);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while(cc.rdr.Read())
                {
                    
                    cmbclient.Items.Add(cc.rdr["Name"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                cmbclient.Enabled = false;
                txtaddress.Enabled = false;
                txtcity.Enabled = false;
                txtcontact.Enabled = false;
                txtemail.Enabled = false;
            }
            if (checkBox1.Checked == false)
            {
                cmbclient.Enabled = true;
                txtaddress.Enabled = true;
                txtcity.Enabled = true;
                txtcontact.Enabled = true;
                txtemail.Enabled = true;
            }
        }

        void SearchItemTitle(string ItemNo)
        {
            double UnitPrice;
            cc.con = new SqlConnection(cs.Connect);
            cc.con.Open();
            cc.cmd = new SqlCommand();


            cc.cmd.CommandText = "SELECT * From Product WHERE PName Like '%" + ItemNo + "%'";
            cc.cmd.Connection = cc.con;
            cc.cmd.CommandType = CommandType.Text;
            cc.rdr = cc.cmd.ExecuteReader();
            cc.rdr.Read();
            if (cc.rdr.HasRows)
            {
                txtbarcode.Text = (String)cc.rdr["PBarcode"].ToString();
                UnitPrice = System.Double.Parse(cc.rdr["PPrice"].ToString());
                txtdescription.Text = (String)cc.rdr["PDescription"].ToString();
                txtvat.Text = (String)cc.rdr["PVAT"].ToString();
                txtvatamount.Text = (String)cc.rdr["PVATAmount"].ToString();
                txtprice.Text = String.Format("{0:0.##}", UnitPrice);
            }
            cc.rdr.Close();

            cc.con = new SqlConnection(cs.Connect);
            cc.con.Open();
            cc.cmd = cc.con.CreateCommand();
            cc.cmd.CommandText = "SELECT Quantity FROM Product,TempStock WHERE Product.PID=TempStock.PID and PName=@d1";
            cc.cmd.Parameters.AddWithValue("@d1", ItemNo);
            cc.rdr = cc.cmd.ExecuteReader();
            if (cc.rdr.Read())
            {
                txtstock.Text = cc.rdr.GetValue(0).ToString();

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

        void SearchItem(string ItemNo)
        {
            double UnitPrice;
            cc.con = new SqlConnection(cs.Connect);
            cc.con.Open();
            cc.cmd = new SqlCommand();


            cc.cmd.CommandText = "SELECT * From Product WHERE PBarcode Like '%" + ItemNo + "%'";
            cc.cmd.Connection = cc.con;
            cc.cmd.CommandType = CommandType.Text;
            cc.rdr = cc.cmd.ExecuteReader();
            cc.rdr.Read();
            if (cc.rdr.HasRows)
            {
                txtproduct.Text = (String)cc.rdr["PName"].ToString();
                UnitPrice = System.Double.Parse(cc.rdr["PPrice"].ToString());
                txtdescription.Text = (String)cc.rdr["PDescription"].ToString();
                txtvat.Text = (String)cc.rdr["PVAT"].ToString();
                txtvatamount.Text = (String)cc.rdr["PVATAmount"].ToString();
                txtprice.Text = String.Format("{0:0.##}", UnitPrice);
            }
            cc.rdr.Close();

            cc.con = new SqlConnection(cs.Connect);
            cc.con.Open();
            cc.cmd = cc.con.CreateCommand();
            cc.cmd.CommandText = "SELECT Quantity FROM Product,TempStock WHERE Product.PID=TempStock.PID and PName=@d1";
            cc.cmd.Parameters.AddWithValue("@d1", ItemNo);
            cc.rdr = cc.cmd.ExecuteReader();
            if (cc.rdr.Read())
            {
                txtstock.Text = cc.rdr.GetValue(0).ToString();

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

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchItem(txtbarcode.Text);
                txtquantity.Text = "1";
                txtquantity_TextChanged(this, new System.EventArgs());
                txtquantity.Focus();

            }
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            decimal Total;


            if (txtquantity.Text.Equals("") || txtprice.Text.Equals(""))
            {
                txtamount.Text = "0.00";
            }
            else
            {
                try
                {
                    int quantity = int.Parse(txtquantity.Text);
                    decimal unitprice = decimal.Parse(txtprice.Text);



                    Total = decimal.Parse(txtprice.Text) * decimal.Parse(txtquantity.Text);
                    txtamount.Text = Total.ToString();
                }
                catch
                { }
            }
        }

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchItemTitle(txtproduct.Text);
                txtquantity.Text = "1";
                txtquantity_TextChanged(this, new System.EventArgs());
                txtquantity.Focus();
            }
            
           
        }

        private void txtpayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTOTAL.Text, out val1);
            int.TryParse(txtpayment.Text, out val2);
            int I = (val1 - val2);
            txtdue.Text = I.ToString();
        }

        public void Calculate()
        {

            if (txtdiscount.Text != "")
            {
                txtdiscountamount.Text = Convert.ToInt32((Convert.ToInt32(txtsubtotal.Text) * Convert.ToDouble(txtdiscount.Text) / 100)).ToString();
            }

            int val2 = 0;
            int val3 = 0;
            int val4 = 0;
            int val5 = 0;


            int.TryParse(txtsubtotal.Text, out val2);
            int.TryParse(txtdiscountamount.Text, out val3);
            int.TryParse(txtTOTAL.Text, out val4);
            int.TryParse(txtpayment.Text, out val5);
            val4 = val2 - val3;
            txtTOTAL.Text = val4.ToString();
            int I = (val4 - val5);
            txtdue.Text = I.ToString();


        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Totalos()
        {
            try
            {
                txtdue.Text = (Convert.ToDouble(txtsubtotal.Text) - Convert.ToDouble(txtdiscount.Text)).ToString();
            }
            catch
            {

            }
        }

        private void TotalCalculation()
        {
            try
            {
                txtTOTAL.Text = (Convert.ToDouble(txtsubtotal.Text) - Convert.ToDouble(txtdiscountamount.Text)).ToString();

            }
            catch
            {
            }
        }

        private void CalculateVATTotal()
        {
            try
            {
                double VatTotal = 0;

                for (int f = 0; f < dataGridView1.Rows.Count; f++)
                {
                    DataGridViewRow raw = dataGridView1.Rows[f];
                    VatTotal += Convert.ToDouble(raw.Cells[6].Value) * Convert.ToDouble(raw.Cells[3].Value);

                }

                txtvattotal.Text = VatTotal.ToString();
            }
            catch
            {

            }
        }


        private void CalculationInGrid()
        {
            try
            {
                double Total = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    Total += Convert.ToDouble(row.Cells[7].Value);

                }
                txtsubtotal.Text = Total.ToString();

            }
            catch
            {
            }
        }

        private void txtquantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbarcode.Text == "" || txtproduct.Text == "")
                {
                    MessageBox.Show("Selektoni një produkt", "ASWI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    for (int row = 0; row < dataGridView1.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView1.Columns.Count; col++)
                        {

                        }
                    }

                    intQtyOrdered = Convert.ToInt32(txtquantity.Text);

                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = txtbarcode.Text;
                    dataGridView1.Rows[n].Cells[1].Value = txtproduct.Text;
                    dataGridView1.Rows[n].Cells[2].Value = txtdescription.Text;
                    dataGridView1.Rows[n].Cells[3].Value = intQtyOrdered;
                    dataGridView1.Rows[n].Cells[4].Value = txtprice.Text;
                    dataGridView1.Rows[n].Cells[5].Value = txtvat.Text;
                    dataGridView1.Rows[n].Cells[6].Value = txtvatamount.Text;
                    dataGridView1.Rows[n].Cells[7].Value = txtamount.Text;
                    dataGridView1.FirstDisplayedScrollingRowIndex = n;
                    dataGridView1.CurrentCell = dataGridView1.Rows[n].Cells[0];
                    dataGridView1.Rows[n].Selected = true;

                    int val101 = 0;
                    int val202 = 0;
                    int.TryParse(txtquantity.Text, out val101);
                    int.TryParse(txtstock.Text, out val202);
                    if (val101 > val202)
                    {
                        MessageBox.Show("Nuk mundet sasia e caktuar pasiqë nuk ka aq material në Depo", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtquantity.Text = "";
                        txtquantity.Focus();
                        return;

                    }

                    txtbarcode.Text = "";
                    txtproduct.Text = "";
                    txtquantity.Text = "";
                    txtprice.Text = "";
                    txtamount.Text = "";
                    txtvat.Text = "";
                    txtdescription.Text = "";
                    txtvatamount.Text = "";
                    txtproduct.Focus();


                    CalculationInGrid();
                    CalculateVATTotal();

                }
            }
        }

        private void txtdiscountamount_TextChanged(object sender, EventArgs e)
        {
            TotalCalculation();
            Totalos();
        }

        private void txtsubtotal_TextChanged(object sender, EventArgs e)
        {
            TotalCalculation();
            Totalos();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    CalculationInGrid();
                    CalculateVATTotal();

                }
                else
                {
                    MessageBox.Show("Selektoni produktin që doni të fshini!");
                }
            }
        }

        private void AutocompleteN()
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
                txtproduct.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtproduct.AutoCompleteCustomSource = col;
                txtproduct.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutocompleteF()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT distinct PBarcode FROM Product", cc.con);
                cc.ds = new DataSet();
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.da.Fill(cc.ds, "Product");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= cc.ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(cc.ds.Tables[0].Rows[i]["PBarcode"].ToString());

                }
                txtbarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtbarcode.AutoCompleteCustomSource = col;
                txtbarcode.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtquantity_Validating(object sender, CancelEventArgs e)
        {
          
        }

        private void RegisterBill()
        {
            try
            {
                string ItemNo = string.Empty;
                string Product = string.Empty;
                string Description = string.Empty;
                string UnitPrice = string.Empty;
                string Qty = string.Empty;
                string Amount = string.Empty;
                string VAT = string.Empty;
                string VATAmount = string.Empty;

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string via = "INSERT INTO Invoice(ID,BillID,Date,Time,Cashier,Client,SubTotal,GrandTotal,VATAmount,Discount,DiscountAmount,Payment,Due,Remarks) VALUES('" + txtID.Text + "','" + txtbillno.Text + "','" + txtdate.Text + "','" + txttime.Text + "','" + lbUSER.Text + "','" + cmbclient.Text + "','" + txtsubtotal.Text + "','" + txtTOTAL.Text + "','" + txtvattotal.Text + "','" + txtdiscount.Text + "','" + txtdiscountamount.Text + "','" + txtpayment.Text + "','" + txtdue.Text + "','" + txtcomment.Text + "')";
                cc.cmd = new SqlCommand(via);
                cc.cmd.Connection = cc.con;
                cc.cmd.ExecuteNonQuery();
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }
                cc.con.Close();

                st1 = lbUSER.Text;
                st2 = "Ka krijuar faturën me numër :  '" + txtbillno.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);

                MessageBox.Show("Fatura u regjistrua me sukses", "FATURA", MessageBoxButtons.OK, MessageBoxIcon.Information);


                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    ItemNo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    Product = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    Description = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    Qty = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    UnitPrice = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    VAT = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    VATAmount = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    Amount = dataGridView1.Rows[i].Cells[7].Value.ToString();

                    cc.con = new SqlConnection(cs.Connect);

                    cc.con.Open();
                    string versus = "INSERT INTO ProductSold(BillID,Barcode,Product,Description,Quantity,Price,VAT,VATAmount,Amount) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)";
                    cc.cmd = new SqlCommand(versus);
                    cc.cmd.Connection = cc.con;


                    cc.cmd.Parameters.AddWithValue("@a1", txtbillno.Text);
                    cc.cmd.Parameters.AddWithValue("@a2", ItemNo);
                    cc.cmd.Parameters.AddWithValue("@a3", Product);
                    cc.cmd.Parameters.AddWithValue("@a4", Description);
                    cc.cmd.Parameters.AddWithValue("@a5", Qty);
                    cc.cmd.Parameters.AddWithValue("@a6", UnitPrice);
                    cc.cmd.Parameters.AddWithValue("@a7", VAT);
                    cc.cmd.Parameters.AddWithValue("@a8", VATAmount);
                    cc.cmd.Parameters.AddWithValue("@a9", Amount);

                    cc.cmd.ExecuteNonQuery();
                    cc.con.Close();

                    
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        cc.con.Close();
                        cc.con = new SqlConnection(cs.Connect);
                        cc.con.Open();
                        string cb2 = "update TempStock set Quantity=Quantity - " + row.Cells[3].Value + " where PID=" + row.Cells[0].Value + "";
                        cc.cmd = new SqlCommand(cb2);
                        cc.cmd.Connection = cc.con;
                        cc.cmd.ExecuteNonQuery();
                        if (cc.con.State == ConnectionState.Open)
                        {
                            cc.con.Close();
                        }
                        cc.con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btregister_Click(object sender, EventArgs e)
        {
            RegisterBill();
        }
    }
}
