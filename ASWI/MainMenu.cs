using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASWI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void përdoruesitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Billing bilko = new Billing();
            bilko.lbUSER.Text = lbUSER.Text;
            bilko.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            BillingRecords bilkorec = new BillingRecords();
            bilkorec.lbUSER.Text = lbUSER.Text;
            bilkorec.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.lbUSER.Text = lbUSER.Text;
            pro.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Client clie = new Client();
            clie.lbUSER.Text = lbUSER.Text;
            clie.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Suppliers supli = new Suppliers();
            supli.lbUSER.Text = lbUSER.Text;
            supli.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Stocks stoc = new Stocks();
            stoc.lbUSER.Text = lbUSER.Text;
            stoc.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Administration admin = new Administration();
            
            admin.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("A jeni të sigurtë që po ç'kyçeni?", "Kujdes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login lg = new Login();
                lg.Show();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("A jeni të sigurtë ta mbyllni aplikacionin?", "Kujdes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
