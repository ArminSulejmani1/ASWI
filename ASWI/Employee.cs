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
    public partial class Employee : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public Employee()
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
                string sql = "SELECT MAX(ID+1) FROM Employee";
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

        private void Employee_Load(object sender, EventArgs e)
        {
            auto();
            getData();
        }

        private void btsearchp_Click(object sender, EventArgs e)
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

        private void btremovep_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.no_image;
        }

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btregister_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani emrin","INFO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }
                if(txtsurname.Text =="")
                {
                    MessageBox.Show("Ju lutemi shkruani mbiemrin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsurname.Focus();
                    return;
                }
                if(cmbsex.Text =="")
                {
                    MessageBox.Show("Ju lutemi zgjidheni gjininë", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbsex.Focus();
                    return;
                }
                if(dtpbirth.Text =="")
                {
                    MessageBox.Show("Ju lutemi zgjidheni daten e lindjes", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpbirth.Focus();
                    return;
                }
                if(cmbstatus.Text =="")
                {
                    MessageBox.Show("Ju lutemi zgjidheni statusin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstatus.Focus();
                    return;
                }
                if(txtaddress.Text =="")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if(cmbstate.Text =="")
                {
                    MessageBox.Show("Ju lutemi zgjidheni shtetin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstate.Focus();
                    return;
                }
                if(txtsalary.Text =="")
                {
                    MessageBox.Show("Ju lutemi shkruani rrogën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsalary.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ins = "INSERT INTO Employee(ID,Name,Surname,Gender,BDate,Status,Address,State,Contact,Email,Salary,JoinDate,Photo) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13)";
                cc.cmd = new SqlCommand(ins);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@a1", txtID.Text);
                cc.cmd.Parameters.AddWithValue("@a2", txtname.Text);
                cc.cmd.Parameters.AddWithValue("@a3", txtsurname.Text);
                cc.cmd.Parameters.AddWithValue("@a4", cmbsex.Text);
                cc.cmd.Parameters.AddWithValue("@a5", dtpbirth.Text);
                cc.cmd.Parameters.AddWithValue("@a6", cmbstatus.Text);
                cc.cmd.Parameters.AddWithValue("@a7", txtaddress.Text);
                cc.cmd.Parameters.AddWithValue("@a8", cmbstate.Text);
                cc.cmd.Parameters.AddWithValue("@a9", txtcontact.Text);
                cc.cmd.Parameters.AddWithValue("@a10", txtemail.Text);
                cc.cmd.Parameters.AddWithValue("@a11", txtsalary.Text);
                cc.cmd.Parameters.AddWithValue("@a12", dtpstarted.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@a13", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka regjistruar punëtorin me emër '" + txtname.Text + "' dhe ID : '" + txtID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u regjistrua punëtori", "REGJISTRIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                getData();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message,"GABIM",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Reset()
        {
            txtname.Text = "";
            txtsurname.Text = "";
            txtsalary.Text = "";
            txtID.Text = "";
            txtemail.Text = "";
            txtcontact.Text = "";
            txtaddress.Text = "";
            txtsearch.Text = "";
            pictureBox1.Image = Properties.Resources.no_image;
            cmbsex.SelectedIndex = -1;
            cmbstate.SelectedIndex = -1;
            cmbstatus.SelectedIndex = -1;
            btedit.Enabled = false;
            btdelete.Enabled = false;
            btregister.Enabled = true;
            auto();
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            Reset();
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
                if (txtsurname.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani mbiemrin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsurname.Focus();
                    return;
                }
                if (cmbsex.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni gjininë", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbsex.Focus();
                    return;
                }
                if (dtpbirth.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni daten e lindjes", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpbirth.Focus();
                    return;
                }
                if (cmbstatus.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni statusin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstatus.Focus();
                    return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani adresën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                if (cmbstate.Text == "")
                {
                    MessageBox.Show("Ju lutemi zgjidheni shtetin", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbstate.Focus();
                    return;
                }
                if (txtsalary.Text == "")
                {
                    MessageBox.Show("Ju lutemi shkruani rrogën", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsalary.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ins = "UPDATE Employee SET Name='" + txtname.Text +"',Surname='" + txtsurname.Text + "',Gender='" + cmbsex.Text + "',BDate='" + dtpbirth.Text + "',Status=@a1,Address='" + txtaddress.Text + "',State='" + cmbstate.Text + "',Contact='" + txtcontact.Text + "',Email='" + txtemail.Text +"',Salary='" + txtsalary.Text + "',JoinDate='" + dtpstarted.Text + "',Photo=@a2 WHERE ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(ins);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@a1", cmbstatus.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@a2", SqlDbType.Image);
                p.Value = data;
                cc.cmd.Parameters.Add(p);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lbUSER.Text;
                st2 = "Ka edituar punëtorin me emër '" + txtname.Text + "' dhe ID : '" + txtID.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btregister.Enabled = false;
                MessageBox.Show("Me sukses u editua punëtori", "EDITIM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                getData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string ct = "DELETE FROM Employee WHERE ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lbUSER.Text;
                    st2 = "Ka fshirë punëtorin  '" + txtname.Text + "' me ID : '" + txtID.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Me sukses është fshirë punëtori", "FSHIRJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cc.cmd = new SqlCommand("SELECT (ID) as [ID],(Name) as [Emri],(Surname) as [Mbiemri],(Gender) as[Gjinia],(BDate) as [Ditëlindja],(Status) as [Statusi],(Address) as [Adresa],(State) as [Shteti],(Contact) as [Kontakti],(Email) as [E-mail],(Salary) as [Rroga],(JoinDate) as [Punësuar më],(Photo) as [Foto] FROM Employee ORDER BY ID", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Employee");
                dataGridView1.DataSource = cc.ds.Tables["Employee"].DefaultView;
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
                cc.cmd = new SqlCommand("SELECT (ID) as [ID],(Name) as [Emri],(Surname) as [Mbiemri],(Gender) as[Gjinia],(BDate) as [Ditëlindja],(Status) as [Statusi],(Address) as [Adresa],(State) as [Shteti],(Contact) as [Kontakti],(Email) as [E-mail],(Salary) as [Rroga],(JoinDate) as [Punësuar më],(Photo) as [Foto] FROM Employee WHERE Name LIKE '" + txtsearch.Text + "%' ORDER BY ID", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Employee");
                dataGridView1.DataSource = cc.ds.Tables["Employee"].DefaultView;
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
            txtsurname.Text = dgr.Cells[2].Value.ToString();
            cmbsex.Text = dgr.Cells[3].Value.ToString();
            dtpbirth.Text = dgr.Cells[4].Value.ToString();
            cmbstatus.Text = dgr.Cells[5].Value.ToString();
            txtaddress.Text = dgr.Cells[6].Value.ToString();
            cmbstate.Text = dgr.Cells[7].Value.ToString();
            txtcontact.Text = dgr.Cells[8].Value.ToString();
            txtemail.Text = dgr.Cells[9].Value.ToString();
            txtsalary.Text = dgr.Cells[10].Value.ToString();
            dtpstarted.Text = dgr.Cells[11].Value.ToString();
            byte[] data = (byte[])dgr.Cells[12].Value;
            MemoryStream ms = new MemoryStream(data);
            pictureBox1.Image = Image.FromStream(ms);
            btedit.Enabled = true;
            btdelete.Enabled = true;
            btregister.Enabled = false;
        }

        private void btexport_Click(object sender, EventArgs e)
        {
            
        }

        private void btprint_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
