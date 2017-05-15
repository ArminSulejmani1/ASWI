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
    public partial class Billing : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        int intQtyOrdered;

        public Billing()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            lbTime.Text = DateTime.Now.ToLongTimeString();
            lbDate.Text = DateTime.Now.ToLongDateString();
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
                    txtbillID.Text = "Fatura - " + Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                    txtbillID.Text = "Fatura - " + Convert.ToString(Num);
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

        private void Billing_Load(object sender, EventArgs e)
        {
            auto();
          
            AutocompleteN();
            FillCustomer();
            btprint.Enabled = false;
           
         
        }

        private void txtbarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtproduct_TextChanged(object sender, EventArgs e)
        {
           
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
                txtclient.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtclient.AutoCompleteCustomSource = col;
                txtclient.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            cc.cmd.CommandText = "SELECT quantity FROM Product,TempStock WHERE Product.PID=TempStock.PID and PName=@d1";
            cc.cmd.Parameters.AddWithValue("@d1", ItemNo);
            cc.rdr = cc.cmd.ExecuteReader();
            if (cc.rdr.Read())
            {
                txtavailable.Text = cc.rdr.GetValue(0).ToString();

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

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchItemTitle(txtproduct.Text);
                txtquantity.Text = "1";
                txtquantity_TextChanged(this, new System.EventArgs());
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
            cc.cmd.CommandText = "SELECT quantity FROM Product,TempStock WHERE Product.PID=TempStock.PID and PName=@d1";
            cc.cmd.Parameters.AddWithValue("@d1", ItemNo);
            cc.rdr = cc.cmd.ExecuteReader();
            if (cc.rdr.Read())
            {
                txtavailable.Text = cc.rdr.GetValue(0).ToString();

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
                    int.TryParse(txtavailable.Text, out val202);
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
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

        private void txtpayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTOTAL.Text, out val1);
            int.TryParse(txtpayment.Text, out val2);
            int I = (val1 - val2);
            txtdue.Text = I.ToString();
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

        public void Calculate()
        {

            if (txtdiscount.Text != "")
            {
                txtdiscounta.Text = Convert.ToInt32((Convert.ToInt32(txtsubtotal.Text) * Convert.ToDouble(txtdiscount.Text) / 100)).ToString();
            }

            int val2 = 0;
            int val3 = 0;
            int val4 = 0;
            int val5 = 0;


            int.TryParse(txtsubtotal.Text, out val2);
            int.TryParse(txtdiscounta.Text, out val3);
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

        private void CalculateVATTotal()
        {
            try
            {
                double VatTotal = 0;

                for (int f=0; f<dataGridView1.Rows.Count; f++)
                {
                    DataGridViewRow raw = dataGridView1.Rows[f];
                    VatTotal += Convert.ToDouble(raw.Cells[6].Value) * Convert.ToDouble(raw.Cells[3].Value);
                  
                }

                txtVATTOTAL.Text = VatTotal.ToString();
            }
            catch
            {

            }
        }
        
       private void TotalCalculation()
        {
            try
            {
                txtTOTAL.Text = (Convert.ToDouble(txtsubtotal.Text) - Convert.ToDouble(txtdiscounta.Text)).ToString();
               
            }
            catch
            {
            }
       }

  


       private void txtdiscounta_TextChanged(object sender, EventArgs e)
       {
           TotalCalculation();
           Totalos();
       }

       private void txtsubtotal_TextChanged(object sender, EventArgs e)
       {
           TotalCalculation();
           Totalos();
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
           string via = "INSERT INTO Invoice(ID,BillID,Date,Time,Cashier,Client,SubTotal,GrandTotal,VATAmount,Discount,DiscountAmount,Payment,Due,Remarks) VALUES('" + txtID.Text + "','" + txtbillID.Text +"','" + lbDate.Text +"','" + lbTime.Text +"','" + lbUSER.Text +"','" + txtclient.Text +"','" + txtsubtotal.Text + "','" + txtTOTAL.Text + "','" + txtVATTOTAL.Text + "','" + txtdiscount.Text + "','" + txtdiscounta.Text +"','" + txtpayment.Text + "','" + txtdue.Text + "','" + tctcomment.Text +"')";
           cc.cmd = new SqlCommand(via);
           cc.cmd.Connection = cc.con;
           cc.cmd.ExecuteNonQuery();
            if(cc.con.State == ConnectionState.Open)
            {
                cc.con.Close();
            }
            cc.con.Close();

            st1 = lbUSER.Text;
            st2 = "Ka krijuar faturën me numër :  '" + txtbillID.Text + "'";
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


                cc.cmd.Parameters.AddWithValue("@a1", txtbillID.Text);
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

                btregister.Enabled = false;
                btprint.Enabled = true;
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
            if (txtclient.Text == "")
            {
                MessageBox.Show("Ju lutemi shkruani klientin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtclient.Focus();
                return;
            }
            {
                RegisterBill();
            }
        }

        private void btbills_Click(object sender, EventArgs e)
        {
            BillingRecords billrec = new BillingRecords();
            this.Hide();
            billrec.Show();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
      
        }

        private void btedit_Click(object sender, EventArgs e)
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
                string via = "UPDATE Invoice SET SubTotal ='" + txtsubtotal.Text + "',GrandTotal ='" + txtTOTAL.Text + "',VATAmount ='" + txtVATTOTAL.Text + "',Discount ='" + txtdiscount.Text + "',DiscountAmount ='" + txtdiscounta.Text + "',Payment ='" + txtpayment.Text + "',Due ='" + txtdue.Text + "',Remarks ='" + tctcomment.Text + "' WHERE BillID ='" + txtbillID.Text +"'";
                cc.cmd = new SqlCommand(via);
                cc.cmd.Connection = cc.con;
                cc.cmd.ExecuteNonQuery();
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }
                cc.con.Close();

                st1 = lbUSER.Text;
                st2 = "Ka edituar faturën me numër :  '" + txtbillID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);

                MessageBox.Show("Fatura u editua me sukses", "FATURA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btregister.Enabled = false;
                btprint.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
                string ct = "DELETE FROM Invoice WHERE ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    string vama = "DELETE FROM ProductSold WHERE BillID=@d2";
                    cc.cmd = new SqlCommand(vama);
                    cc.cmd.Connection = cc.con;
                    cc.cmd.Parameters.AddWithValue("@d2", txtbillID.Text);
                    cc.cmd.ExecuteNonQuery();
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë faturën:'" + txtbillID.Text + "'";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reset()
        {
            txtbillID.Text = "";
            txtID.Text = "";
            txtclient.Text = "";
            txtproduct.Text = "";
            txtbarcode.Text = "";
            txtdescription.Text = "";
            txtprice.Text = "";
            
            txtsubtotal.Text = "";
            txtdiscount.Text = "";
            txtdiscounta.Text = "";
            txtpayment.Text = "";
            txtVATTOTAL.Text = "";
            txtdue.Text = "";
            tctcomment.Text = "";
            txtproduct.Text = "";
            txtamount.Text = "";
            txtquantity.Text = "";
            txtvat.Text = "";
            txtvatamount.Text = "";
            dataGridView1.Rows.Clear();
            btregister.Enabled = true;
            btedit.Enabled = false;
            auto();
        }

        private void Clear()
        {
            txtproduct.Clear();
            txtbarcode.Clear();
            txtquantity.Clear();
            txtvat.Clear();
            txtvatamount.Clear();
            txtamount.Clear();
            txtclient.Clear();
            txtTOTAL.Clear();
            txtdescription.Clear();   
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            Reset();
            Clear();
            
        }

        private void txtquantity_Validating(object sender, CancelEventArgs e)
        {
            int val101 = 0;
            int val202 = 0;
            int.TryParse(txtquantity.Text, out val101);
            int.TryParse(txtavailable.Text, out val202);
            if (val101 > val202)
            {
                MessageBox.Show("Nuk mundet sasia e caktuar pasiqë nuk ka aq material në Depo", "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtquantity.Text = "";
                txtquantity.Focus();
                return;

            }
        }

        private void txtavailable_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVATTOTAL_TextChanged(object sender, EventArgs e)
        {

        }


    }
}

   

