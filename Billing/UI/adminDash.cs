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
    public partial class rfmAdminDashboard : Form
    {
        public frmLogin adlg;
        public rfmAdminDashboard()
        {
            InitializeComponent();
        }
        public static string transactionType;
        public static string cusType;

        private void uSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers user = new frmUsers();
            user.Show();
        }

        private void rfmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void catoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategories category = new frmCategories();
            category.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();
        }

        private void dealerCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDlr_Cstmr DeaCust = new frmDlr_Cstmr();
            DeaCust.Show();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransactions transaction = new frmTransactions();
            transaction.Show();
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }

        private void aboutUstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs us = new AboutUs();
            us.Show();
        }
        private void puschaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "PURCHASE";
            //cusType = "Dealer Details";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "SALES";
            //cusType = "Customer Details";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
        }

        private void rfmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            adlg.Show();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "P_Return";
            PReturn pr = new PReturn();
            pr.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            transactionType = "S_Return";
            PReturn Sr = new PReturn();
            Sr.Show();
        }
    }
}
