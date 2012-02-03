using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public interface IInvoiceAutoNumberGenerator
    {
        string GenerateInvoiceNumberDraft(string companyId);
        string GenerateInvoiceNumber(DateTime transactionDate, string companyId);
    }
}