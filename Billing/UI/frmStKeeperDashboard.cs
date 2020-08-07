using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Billing.UI
{
    public partial class frmStKeeperDashboard : Form
    {
        public frmLogin stlg;
        public frmStKeeperDashboard()
        {
            InitializeComponent();
        }
        public static string transactionType;
       // public static string cstype;
        private void puschaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "PURCHASE";
           // cstype = "Dealer Details";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDlr_Cstmr DlrCst = new frmDlr_Cstmr();
            DlrCst.Show();
        }

        private void aboutUstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs au = new AboutUs();
            au.Show();
        }

        private void frmStKeeperDashboard_Load(object sender, EventArgs e)
        {
            
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void frmStKeeperDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            stlg.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "P_Return";
            PReturn pr = new PReturn();
            pr.Show();
        }
    }
}
