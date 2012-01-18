using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices.model;
namespace dokuku.sales.fixture
{
    [Subject("Generate invoice number yearly")]
    public class When_generate_invoice_auto_number_yearly
    {
        static InvoiceAutoNumberYearly invoiceNumber;
        Establish context = () =>
        {
            invoiceNumber = new InvoiceAutoNumberYearly("2012","oetawan",2012);
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
            invoiceNumber.InvoiceNumberInStringFormat("INV-").ShouldEqual("INV-20120000002");
        };
    }
}