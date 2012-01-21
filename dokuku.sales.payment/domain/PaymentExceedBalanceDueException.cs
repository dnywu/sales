using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.payment.domain
{
    public class PaymentExceedBalanceDueException : ApplicationException
    {
        public PaymentExceedBalanceDueException() :
            base("Jumlah yang dibayarkan melebihi jumlah yang harus dibayar")
        {
        }
    }
}
