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
    public class When_paid_invoice_edited
    {
        static InvoicePayment payment;
        static Guid paymentRecordId;
        static string invoiceNo;
        static Guid invoiceId;
        static Guid paymentId;
        Establish context = () =>
        {
            DomainEvents.Register<InvoicePaid>( p => paymentId = p.PaymentRecordId );
            payment = new InvoicePayment(Guid.NewGuid(), "oetawan", new Invoice(Guid.NewGuid(), "INV-1", 10000000), Guid.NewGuid());
            PaymentRecord pr = PaymentRecord.
                AmountPaid(2000000).
                BankCharge(100000).
                PaymentDate(new DateTime(2012, 1, 20)).
                PaymentMode(new PaymentMode("Cash")).
                Reference("#001002").
                Notes("test partial payment");

            paymentRecordId = pr.Id;
            invoiceId = payment.Invoice.InvoiceId;
            invoiceNo = payment.Invoice.InvoiceNumber;
            payment.Pay(pr);
        };
        Because of = () =>
        {
            PaymentRecord newPaymentRecord = PaymentRecord.
                AmountPaid(8000000).
                BankCharge(100000).
                PaymentDate(new DateTime(2012, 1, 21)).
                PaymentMode(new PaymentMode("Cash")).
                Reference("#001002").
                Notes("Test Revised Transaction");
            payment.RevisePayment(paymentId, newPaymentRecord);
        };
        It Should = () =>
            {
                payment.PaymentRecords.Count().Equals(3);
                payment.BalanceDue.ShouldEqual(2000000m);
            };
        Cleanup cleanup = () =>
            {
                DomainEvents.ClearCallbacks();
            };
    }
}