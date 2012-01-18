using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.invoices.model;
namespace dokuku.sales.fixture
{
    [Subject("Generate invoice number default")]
    public class When_generate_invoice_auto_number_default
    {
        static InvoiceAutoNumberDefault invoiceNumber;
        Establish context = () => 
        {
            invoiceNumber = new InvoiceAutoNumberDefault(typeof(InvoiceAutoNumberDefault).Name, "oetawan");
            invoiceNumber.Reset();
        };

        Because of = () =>
        {
            invoiceNumber.Next();
            invoiceNumber.Next();
            invoiceNumber.Next();
        };

        It should_generate_new_invoice_number = () =>
        {
            invoiceNumber.Value.ShouldEqual(3);
            invoiceNumber.InvoiceNumberInStringFormat("INV-").ShouldEqual("INV-3");
        };
    }
}