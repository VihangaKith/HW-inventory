using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Billing.BLL
{
    class productsBLL
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public decimal rate { get; set; }
        public int qty { get; set; }
        public int min { get; set; }
        public DateTime added_date { get; set; }
        public string added_by { get; set; }
    }
}
