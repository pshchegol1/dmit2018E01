﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRace.Data.POCOs
{
    public class GetInvoiceID
    {
        public int InvoiceID { get; set; }

        public DateTime Date { get; set; }
        public decimal? Total { get; set; }
    }
}
