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
    public partial class Log : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        ListViewItem lst;
        string st1;
        string st2;

        public Log()
        {
            InitializeComponent();
        }

        private void getData()
        {
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Përdorues", 300);
                listView1.Columns.Add("Data", 450);
                listView1.Columns.Add("Operacioni", 600);

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = @"SELECT UserID,Date,Operation FROM LOGS ORDER BY Date DESC";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    lst = listView1.Items.Add(cc.rdr[0].ToString());
                    lst.SubItems.Add(cc.rdr[1].ToString());
                    lst.SubItems.Add(cc.rdr[2].ToString());
                }
                cc.rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogTrail()
        {
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Përdorues", 200);
                listView1.Columns.Add("Data", 250);
                listView1.Columns.Add("Operacioni", 450);

                cc.con = new SqlConnection(cs.Connect);
                cc.con.Open();
                string sql = @"SELECT UserID,Date,Operation FROM LOGS WHERE UserID LIKE '" + cmbselect.Text + "' ORDER BY Date DESC";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    lst = listView1.Items.Add(cc.rdr[0].ToString());
                    lst.SubItems.Add(cc.rdr[1].ToString());
                    lst.SubItems.Add(cc.rdr[2].ToString());
                }
                cc.rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GABIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbselect.Text == "Përgjithshme")
            {
                getData();

            }
            else
            {

                LogTrail();

            }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("A jeni të sigurtë?", "KONFIRMIM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                try
                {
                    cc.con = new SqlConnection(cs.Connect);
                    cc.con.Open();
                    string del = "DELETE FROM LOGS";
                    cc.cmd = new SqlCommand(del);
                    cc.cmd.Connection = cc.con;
                    cc.cmd.ExecuteNonQuery();

                    MessageBox.Show("Me sukses u bë fshirja!");
                    getData();

                    st1 = lbUSER.Text;
                    st2 = "Përdoruesi ka fshirë të dhënat : '" + lbUSER.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nuk ka të dhëna", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }
    }
}
