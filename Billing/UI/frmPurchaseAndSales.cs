using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.DAL;
using Billing.BLL;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;

namespace Billing.UI
{
    public partial class frmPurchaseAndSales : Form
    {
       
       List<productsBLL> producti=new List<productsBLL>();
       List<categoriesBLL> categori = new List<categoriesBLL>();
       static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
       
        string type;
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pdal = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionsDAL tDAL = new transactionsDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();
        transactionDetailBLL tdbll = new transactionDetailBLL();
        DataTable transactionDT = new DataTable();
        //rfmAdminDashboard ad = new rfmAdminDashboard();
        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        categoriesBLL cb = new categoriesBLL();

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            rfmAdminDashboard ad = new rfmAdminDashboard();
            
            // adminDash ad = new adminDash();
            frmUserDashboard us = new frmUserDashboard();
            
            if(frmLogin.loggedIn == "admin")
            {
                type = rfmAdminDashboard.transactionType;
                lblTop.Text = type;
            }
            else if (frmLogin.loggedIn == "cashier")
            {
                type = frmUserDashboard.transactionType;
                lblTop.Text = type;
            }
            else if(frmLogin.loggedIn == "stkeeper")
            {
                type = frmStKeeperDashboard.transactionType;
                lblTop.Text = type;
            }

            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
           
            
            SqlConnection conn = new SqlConnection(myconnstring);
            conn.Open();
            SqlCommand cmdCate = new SqlCommand("SELECT * FROM tbl_categories",conn);
             
            SqlDataReader dr = cmdCate.ExecuteReader();
            while (dr.Read())
            {
                cmbCat.Items.Add(dr["title"]);
                categori.Add(new categoriesBLL()
                {
                Id=((int)dr["Id"]),
                title=dr["title"]as string
                });
            }
            conn.Close();
           conn.Open();
            SqlCommand cmdProd = new SqlCommand("SELECT * FROM tbl_products", conn);
            SqlDataReader dr1 = cmdProd.ExecuteReader();
            while (dr1.Read())
            {
                producti.Add(new productsBLL()
                {   
                    Id=((int)dr1["Id"]), 
                    name=dr1["name"]as string,
                    category=dr1["category"]as string
                    
                });
            }
            conn.Close();
                 
        }
        private string[] GetProByCat(string c)  
        {
            return producti.Where(line => line.category==c).Select(l=>l.name).ToArray();
        
        }
       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransation(keyword);
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;
            if (keyword == "")
            {
                txtNamePro.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }
           
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            try
            {
                /*if(transactionDT.Rows.Count != 0)
                {
                    SqlConnection conn = new SqlConnection(myconnstring);
                    conn.Open();
                    string sqlTrunc = "TRUNCATE TABLE " + transactionDT;
                    SqlCommand cmd = new SqlCommand(sqlTrunc, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }*/
                int a = int.Parse(txtInventory.Text);
                int q=int.Parse(txtQty.Text);
                int i=int.Parse(txtInventory.Text);
                int min = int.Parse(txtmin.Text);

                int check;
                check = a - q;
                if (check <= min && lblTop.Text=="SALES")
                {
                    Alert at = new Alert("Minimum Quntity Reached",AlertType.warning);
                    at.Show();
                }
                if (q > i && lblTop.Text=="SALES")
                {
                    decimal qt = Math.Round(decimal.Parse(txtQty.Text), 2);
                    MassageBox mb = new MassageBox("Exceed Stock!!!", MsgType.clear);
                    mb.BackColor = Color.Crimson;
                    mb.Show(); 
                }
                else {
                    string productName = txtNamePro.Text;
                    decimal rate = decimal.Parse(txtRate.Text);
                    decimal qty = decimal.Parse(txtQty.Text);

                    decimal Total = rate * qty;

                    decimal subTotal = decimal.Parse(txtSubTotal.Text);
                    subTotal = subTotal + Total;


                    if (productName == "")
                    {
                        MassageBox mb = new MassageBox("Select The Product", MsgType.retry);
                        mb.Show();
                        //MessageBox.Show("Select The Product First and Try Again...");
                    }
                    else
                    {

                        transactionDT.Rows.Add(productName, rate, qty, Total);
                        dgvAddedProducts.DataSource = transactionDT;

                        txtSubTotal.Text = subTotal.ToString();

                        txtSearchProduct.Text = "";
                        txtNamePro.Text = "";
                        txtInventory.Text = "0";
                        txtRate.Text = "0.00";
                        txtQty.Text = "0";
                    }
                }
               
                
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = txtDiscount.Text;
                if (value == "")
                {
                    MassageBox mb = new MassageBox("Please Enter Discount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Please Enter Discount First!!!...");
                }
                else
                {
                    decimal subtotal = decimal.Parse(txtSubTotal.Text);
                    decimal discount = decimal.Parse(txtDiscount.Text);
                    decimal grandTotal = Math.Round(((100 - discount) / 100) * subtotal,2);

                    txtGrandTotal.Text = grandTotal.ToString();
                }
            }catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string check = txtGrandTotal.Text;
                if (check == "")
                {
                    MassageBox mb = new MassageBox("Please Enter Discount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Input Discount First to Calculate Grand Total...!!!");
                }
                else
                {
                    decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                    decimal vat = decimal.Parse(txtVat.Text);
                    decimal grandTotalWithVat = Math.Round(((100 + vat) / 100) * previousGT,2);
                    txtGrandTotal.Text = grandTotalWithVat.ToString();
                }
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string paid = txtPaidAmount.Text;
                if (paid == "")
                {
                    MassageBox mb = new MassageBox("Enter Paid Amount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Please Enter Paid Amount First...!!!");
                }
                else
                {
                    //decimal returnVal = decimal.Parse(txtReturnAmount.Text);
                    decimal pid = decimal.Parse(txtPaidAmount.Text);
                    decimal gt = decimal.Parse(txtGrandTotal.Text);
                    decimal ret = (pid - gt);
                    txtReturnAmount.Text = ret.ToString();
                }
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                transactionsBLL transaction = new transactionsBLL();
                transaction.type = lblTop.Text;
                string deaCustName = txtName.Text;

                DeaCustBLL dc = dcDAL.GetDeaCustIdFromName(deaCustName);
                transaction.dea_cust_id = dc.Id;
                transaction.grandTotal = Math.Round( decimal.Parse(txtGrandTotal.Text),2);
                transaction.transaction_date = DateTime.Now;
                transaction.tax = decimal.Parse(txtVat.Text);
                transaction.discount = decimal.Parse(txtDiscount.Text);

                string username = frmLogin.loggedIn;
                userBLL u = uDAL.GetIdFromUsername(username);

                transaction.added_by = username;
                transaction.transactionDetails = transactionDT;

                bool success = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    int transactionId = -1;
                    bool w = tDAL.Insert_Transaction(transaction, out transactionId);

                    for (int i = 0; i <transactionDT.Rows.Count; i++)
                    {
                        transactionDetailBLL transactionDetail = new transactionDetailBLL();
                        string productName = transactionDT.Rows[i][0].ToString();
                        productsBLL p = pdal.GetProductIdFromName(productName);
                        transactionDetail.product_id = p.Id;
                        transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                        transactionDetail.qty = int.Parse(transactionDT.Rows[i][2].ToString());
                        transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                        transactionDetail.dea_cust_id = dc.Id;
                        transactionDetail.added_date = DateTime.Now;
                        string loggedUser = frmLogin.loggedIn;
                        userBLL usr = uDAL.GetIdFromUsername(loggedUser);
                        transactionDetail.added_by = loggedUser;

                        string transactionType = lblTop.Text;
                        
                        bool x = false;
                       
                        if (transactionType == "PURCHASE" )
                        {
                            x = pdal.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                        }
                        else if (transactionType == "SALES")
                        {
                            x = pdal.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                        }
                        bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                        success = w && x && y;
                    }
                    

                    if (success == true && txtPaidAmount.Text!="0")
                    {
                    
                        scope.Complete();
                        MassageBox mb = new MassageBox("Transaction Success", MsgType.success);
                        mb.Show();
                        //MessageBox.Show("Transaction Completed Successfully...");
                        
                        dgvAddedProducts.DataSource = null;
                        dgvAddedProducts.Rows.Clear();

                        txtSearch.Text = "";
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtContact.Text = "";
                        txtAddress.Text = "";
                        txtSearchProduct.Text = "";
                        txtNamePro.Text = "";
                        txtInventory.Text = "0";
                        txtRate.Text = "0";
                        txtQty.Text = "0";
                        txtSubTotal.Text = "0";
                        txtDiscount.Text = "0";
                        txtVat.Text = "0";
                        txtGrandTotal.Text = "0";
                        txtPaidAmount.Text = "0";
                        txtReturnAmount.Text = "0";
                        txtmin.Text = "";


                    }
                    else
                    {
                        MassageBox mb = new MassageBox("Transaction Failed", MsgType.retry);
                        mb.Show();
                    }
                }
            }catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                lblTotal.Text = "";
            }
            else
            {
                decimal r = Math.Round(decimal.Parse(txtRate.Text),2);
                decimal q = Math.Round(decimal.Parse(txtQty.Text),2);
                decimal s = Math.Round(r * q,2);
                lblTotal.Text = (s).ToString();
            }

        }

        private void dgvAddedProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPro.Items.Clear(); 
            string title = categori[cmbCat.SelectedIndex].title.ToString();
            foreach (string nam in GetProByCat(title))
            {
                this.cmbPro.Items.Add(nam);
            }

        }

        private void cmbPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string keyword = cmbPro.Text;
            productsBLL p = pdal.GetProductsForPurchaseSales(keyword);
                txtNamePro.Text = p.name;
                txtInventory.Text = p.qty.ToString();
                txtRate.Text = p.rate.ToString();
                txtmin.Text = p.min.ToString();
        }

        private void cmbPro_SelectedValueChanged(object sender, EventArgs e)
        {
            //productsBLL p = pdal.GetProductsFromTransaction(keyword);
           /* txtNamePro.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
            txtmin.Text = p.min.ToString();*/
        }
        public void Clear()
        {
            txtNamePro.Text = "";
            txtQty.Text = "";
            txtRate.Text = "";
            txtSubTotal.Text = "";
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            try
            {
                tdbll.Id = int.Parse(txtRate.Text);
                bool succes = tdDAL.Delete(tdbll);
                if (succes == true)
                {
                    MassageBox mb = new MassageBox("Removed", MsgType.delete);
                    mb.Show();
                    //MessageBox.Show("Categoty Deleted Succesfully");
                   // Clear();
                    DataTable dt = tdDAL.Select();
                    dgvAddedProducts.DataSource = dt;
                }
                else
                {
                    MassageBox mb = new MassageBox("Remove Failed", MsgType.retry);
                    mb.Show();
                    // MessageBox.Show("Failed to Delete Categorie...!!! Try Again");
                }
            }
            catch (Exception)
            {
                MassageBox mb = new MassageBox("Delete Failed", MsgType.retry);
                mb.Show();
            }
        }

        private void dgvAddedProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           /* int RowIndex = e.RowIndex;
           // txtID.Text =  dgvAddedProducts.Rows[RowIndex].Cells[0].Value.ToString();
            txtRate.Text = dgvAddedProducts.Rows[RowIndex].Cells[2].Value.ToString();
            txtQty.Text = dgvAddedProducts.Rows[RowIndex].Cells[3].Value.ToString();
           // txtDiscount.Text = dgvAddedProducts.Rows[RowIndex].Cells[6].Value.ToString();*/

        }

    }
}
