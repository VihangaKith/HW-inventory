using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Billing.BLL
{
    class transactionDetailBLL
    {
        public int Id { get; set; }
        public int product_id { get; set; }
        public decimal rate { get; set; }
        public int qty { get; set; }
        public decimal total { get; set; }
        public int dea_cust_id { get; set; }
        public DateTime added_date { get; set; }
        public string added_by { get; set; }
    }
}
