using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.UI;

namespace Billing
{
    public partial class frmUserDashboard : Form
    {
        public frmLogin uslg;
        public frmUserDashboard()
        {
            InitializeComponent();
        }
        public static string transactionType;
        frmPurchaseAndSales ps = new frmPurchaseAndSales();
       
       // public static string cType;


        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;

        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDlr_Cstmr DlrCst = new frmDlr_Cstmr();
            DlrCst.Show();
        }

        private void puschaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "PURCHASE";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "SALES";
           // cType = "Customer Details";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
           
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }

        private void aboutUstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs au = new AboutUs();
            au.Show();
        }

        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            uslg.Show();
        }

        private void puschaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void ReturntoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            transactionType = "S_Return";
            PReturn Sr = new PReturn();
            Sr.Show();
        }
    }
}
