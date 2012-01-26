using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.domain
{
    public class PaymentDateLessThanInvoiceDateException : ApplicationException
    {
        public PaymentDateLessThanInvoiceDateException()
            : base("Tanggal pembayaran di bawah tanggal invoice!")
        { }
    }
}
