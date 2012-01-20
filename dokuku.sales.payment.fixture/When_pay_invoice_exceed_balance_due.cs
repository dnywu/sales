using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.fixture
{
    [Subject("Bayar invoice partial")]
    public class When_pay_invoice_exceed_balance_due
    {
        static InvoicePayment payment;
        static PaymentExceedBalanceDueException exception;
        Establish context = () => {
            payment = new InvoicePayment(new Invoice("INV-1", 10000000),new Customer(Guid.NewGuid(),"Matahari"));
        };

        Because of = () =>
        {
            try
            {
                PaymentRecord pr = PaymentRecord.
                    AmountPaid(20000000).
                    BankCharge(100000).
                    PaymentDate(new DateTime(2012, 1, 20)).
                    PaymentMode(new PaymentMode("Cash")).
                    Reference("#001002").
                    Notes("test partial payment");

                payment.Pay(pr);
            }
            catch (PaymentExceedBalanceDueException ex)
            {
                exception = ex;
            }
        };

        It should_throw_amount_paid_exceeded = () =>
        {
            exception.ShouldNotBeNull();
        };
    }
}