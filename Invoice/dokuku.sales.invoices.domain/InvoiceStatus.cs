using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.domain
{
    public class InvoiceStatus
    {
        public const string DRAFT = "Draft";
        public const string BELUM_BAYAR = "Belum Bayar";
        public const string BELUM_LUNAS = "Belum Lunas";
        public const string SUDAH_LUNAS = "Sudah Lunas";
        public const string VOID = "Void";
        public const string BATAL = "Batal";
    }
}
