using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.fixture
{
    public class FakeInvoiceNumberGenerator : IInvoiceAutoNumberGenerator
    {
        public string GenerateInvoiceNumberDraft(string companyId)
        {
            return "DRAFT-1";
        }

        public string GenerateInvoiceNumber(DateTime transactionDate, string companyId)
        {
            return "INV-1";
        }
    }
}
