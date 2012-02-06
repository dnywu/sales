using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.common
{
    [Serializable]
    public class Term
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
    }
}
