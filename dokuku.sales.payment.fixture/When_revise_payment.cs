using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.payment.domain;
using dokuku.sales.domainevents;
using dokuku.sales.payment.domainevents;

namespace dokuku.sales.payment.fixture
{
    [Subject("Edit Paid Invoice")]
    public class When_revise_payment
    {
        static InvoicePayment payment;
        static Guid paymentRecordId;
        static string invoiceNo;
        static Guid invoiceId;
        static InvoicePaid invoicePaid;
        static PaymentRevised paymentRevised;
        Establish context = () =>
        {
            DomainEvents.Register<InvoicePaid>(p => invoicePaid = p);
            DomainEvents.Register<PaymentRevised>( p => paymentRevised = p );
            Invoice invoice = new Invoice(Guid.NewGuid(), "INV-1", 10000000,DateTime.Now);
            payment = new InvoicePayment(Guid.NewGuid(), "oetawan", invoice);
            Payment pr = Payment.
                AmountPaid(2000000).
                BankCharge(100000).
                PaymentDate(new DateTime(2012, 1, 20)).
                PaymentMode(Guid.NewGuid()).
                Reference("#001002").
                Notes("test partial payment");

            paymentRecordId = pr.Id;
            invoiceId = invoice.InvoiceId;
            invoiceNo = invoice.InvoiceNumber;
            payment.Pay(pr);
        };
        Because of = () =>
        {
            Payment newPaymentRecord = Payment.
                AmountPaid(8000000).
                BankCharge(100000).
                PaymentDate(new DateTime(2012, 1, 21)).
                PaymentMode(Guid.NewGuid()).
                Reference("#001002").
                Notes("Test Revised Transaction");
            payment.RevisePayment(invoicePaid.PaymentRecordId, newPaymentRecord);
        };
        It Should = () =>
            {
                paymentRevised.BalanceDue.ShouldEqual(2000000m);
            };
        Cleanup cleanup = () =>
            {
                DomainEvents.ClearCallbacks();
            };
    }
}