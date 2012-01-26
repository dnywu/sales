using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.payment.domain;
using dokuku.sales.payment.domainevents;
using dokuku.sales.domainevents;
namespace dokuku.sales.payment.fixture
{
    [Subject("Bayar invoice partial")]
    public class When_pay_invoice
    {
        static InvoicePayment payment;
        static InvoicePaid paymentRevised;
        Establish context = () => {
            payment = new InvoicePayment(Guid.NewGuid(),"oetawan",new Invoice(Guid.NewGuid(), "INV-1", 10000000,DateTime.Now));
            DomainEvents.Register<InvoicePaid>(p => paymentRevised = p);
        };

        Because of = () =>
        {
            Payment pr = Payment.
                AmountPaid(2000000).
                BankCharge(100000).
                PaymentDate(new DateTime(2012, 1, 20)).
                PaymentMode(Guid.NewGuid()).
                Reference("#001002").
                Notes("test partial payment");

            payment.Pay(pr);
        };

        It seharusnya_invoice_masih_ada_yang_outstanding = () =>
        {
            paymentRevised.BalanceDue.ShouldEqual(8000000);
        };
    }
}