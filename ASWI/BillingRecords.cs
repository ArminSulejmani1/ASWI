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
using Excel = Microsoft.Office.Interop.Excel;

namespace ASWI
{
    public partial class BillingRecords : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();

        public BillingRecords()
        {
            InitializeComponent();
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            txtsearch.Text = "";
            groupBox3.Visible = false;
            LoadData();
        }

        private void btexport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Nuk ka të dhëna për të eksportuar...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }  
        }

        private void Autocomplete()
        {
            try
            {
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Client", cc.con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "BillingRecords");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["Name"].ToString());

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

        private void BillingRecords_Load(object sender, EventArgs e)
        {
            Autocomplete();
            LoadData();
        }

        private void Searching()
        {
            try
            {
                groupBox3.Visible = true;
                txtsearch.Text = txtsearch.Text;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(ID) as [ID],RTRIM(BillID) as [Fatura],RTRIM(Date) as [Data],RTRIM(Time) as [Koha],RTRIM(Cashier) as [Përdorues],RTRIM(Client) as [Klienti],RTRIM(SubTotal) as [Vlera],RTRIM(GrandTotal) as [Totali],RTRIM(VATAmount) as [TVSH],RTRIM(Discount) as [Zbritje %],RTRIM(DiscountAmount) as [Zbritje],RTRIM(Payment) as [Pagesa],RTRIM(Due) as [Borxhi],RTRIM(Remarks) as[Komente] FROM Invoice WHERE Client='" + txtsearch.Text + "' ORDER BY Date", cc.con);
                SqlDataAdapter myDA = new SqlDataAdapter(cc.cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice");

                dataGridView1.DataSource = myDataSet.Tables["Invoice"].DefaultView;

                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.dataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[7].Value);
                    Int64 j = Convert.ToInt64(r.Cells[11].Value);
                    Int64 k = Convert.ToInt64(r.Cells[12].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                txtTOTAL.Text = sum.ToString();
                txtPAYMENT.Text = sum1.ToString();
                txtDUE.Text = sum2.ToString();

                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadData()
        {
            try
            {
                groupBox3.Visible = true;
                txtsearch.Text = txtsearch.Text;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT (ID) as [ID],(BillID) as [Fatura],(Date) as [Data],(Time) as [Koha],(Cashier) as [Përdorues],(Client) as [Klienti],(SubTotal) as [Vlera],(GrandTotal) as [Totali],(VATAmount) as [TVSH],(Discount) as [Zbritje %],(DiscountAmount) as [Zbritje],(Payment) as [Pagesa],(Due) as [Borxhi],(Remarks) as[Komente] FROM Invoice ORDER BY Date", cc.con);
                SqlDataAdapter myDA = new SqlDataAdapter(cc.cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice");

                dataGridView1.DataSource = myDataSet.Tables["Invoice"].DefaultView;

                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.dataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[7].Value);
                    Int64 j = Convert.ToInt64(r.Cells[11].Value);
                    Int64 k = Convert.ToInt64(r.Cells[12].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                txtTOTAL.Text = sum.ToString();
                txtPAYMENT.Text = sum1.ToString();
                txtDUE.Text = sum2.ToString();

                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            Searching();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                Billing frm = new Billing();
                frm.Show();
                frm.txtID.Text = dr.Cells[0].Value.ToString();
                frm.txtbillID.Text = dr.Cells[1].Value.ToString();
                frm.lbDate.Text = dr.Cells[2].Value.ToString();
                frm.lbTime.Text = dr.Cells[3].Value.ToString();
                frm.lbUSER.Text = dr.Cells[4].Value.ToString();
                frm.txtclient.Text = dr.Cells[5].Value.ToString();
                frm.txtsubtotal.Text = dr.Cells[6].Value.ToString();
                frm.txtTOTAL.Text = dr.Cells[7].Value.ToString();
                frm.txtVATTOTAL.Text = dr.Cells[8].Value.ToString();
                frm.txtdiscount.Text = dr.Cells[9].Value.ToString();
                frm.txtdiscounta.Text = dr.Cells[10].Value.ToString();
                frm.txtpayment.Text = dr.Cells[11].Value.ToString();
                frm.txtdue.Text = dr.Cells[12].Value.ToString();
                frm.tctcomment.Text = dr.Cells[13].Value.ToString();
                frm.btedit.Enabled = true;
                frm.btdelete.Enabled = true;
                frm.btprint.Enabled = true;
                frm.btregister.Enabled = false;
                frm.lbUSER.Text = lbUSER.Text;

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string vanga = "SELECT Barcode,Product,Description,Quantity,Price,VAT,VATAmount,Amount FROM ProductSold WHERE BillID='" + dr.Cells[1].Value + "'";
                cc.cmd = new SqlCommand(vanga);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                frm.dataGridView1.Rows.Clear();

                while(cc.rdr.Read()== true)
                {
                    frm.dataGridView1.Rows.Add(cc.rdr[0], cc.rdr[1], cc.rdr[2], cc.rdr[3], cc.rdr[4], cc.rdr[5], cc.rdr[6], cc.rdr[7]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BillingRecords_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Billing bila = new Billing();
            bila.Show();
        }


    }
}
