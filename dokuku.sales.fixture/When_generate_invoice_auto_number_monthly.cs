using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices.model;
namespace dokuku.sales.fixture
{
    [Subject("Generate invoice number monthly")]
    public class When_generate_invoice_auto_number_monthly
    {
        static InvoiceAutoNumberMonthly invoiceNumber;
        Establish context = () =>
        {
            invoiceNumber = new InvoiceAutoNumberMonthly("201201","oetawan", 2012, 1);
            invoiceNumber.Reset();
        };

        Because of = () =>
        {
            invoiceNumber.Next();
            invoiceNumber.Next();
        };

        It should_generate_new_invoice_number = () =>
        {
            invoiceNumber.Value.ShouldEqual(2);
            invoiceNumber.InvoiceNumberInStringFormat("INV-").ShouldEqual("INV-20120100002");
        };
    }
}