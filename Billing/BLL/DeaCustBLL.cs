﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Billing.BLL
{
    class DeaCustBLL
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public DateTime added_date { get; set; }
        public string added_by { get; set; }
    }
}
