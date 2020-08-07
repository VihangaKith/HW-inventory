using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Billing.DAL;
using Billing.BLL;

namespace Billing.UI
{
    public partial class PReturn : Form
    {
         List<productsBLL> producti=new List<productsBLL>();
       List<categoriesBLL> categori = new List<categoriesBLL>();
       static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string type;
        public PReturn()
        {
            InitializeComponent();
        }
        transactionDetailDAL tdal = new transactionDetailDAL();
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void PReturn_Load(object sender, EventArgs e)
        {
            DataTable dt = tdal.Select();
            dgvReturn.DataSource = dt;
            dgvReturn.Columns["Id"].Visible = false;
            if (frmLogin.loggedIn == "admin")
            {
                type = rfmAdminDashboard.transactionType;
                lblTop.Text = type;
            }
            else if (frmLogin.loggedIn == "cashier")
            {
                type = frmUserDashboard.transactionType;
                lblTop.Text = type;
            }
            else if (frmLogin.loggedIn == "stkeeper")
            {
                type = frmStKeeperDashboard.transactionType;
                lblTop.Text = type;
            }

            //****************************************
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

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPro.Items.Clear();
            string title = categori[cmbCat.SelectedIndex].title.ToString();
            foreach (string nam in GetProByCat(title))
            {
                this.cmbPro.Items.Add(nam);
            }
        }
        }

    
}
