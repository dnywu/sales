using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace dokuku.sales.report.Handlers
{
    public static class CollectionName
    {
        public const string CUSTOMER_REPORTS = "CustomerReports";
        public const string ITEM_REPORTS = "ItemReports";
        public const string INVOICE_REPORTS = "InvoiceReports";
        public const string PAYMENT_REPORTS = "PaymentReports";
    }
}