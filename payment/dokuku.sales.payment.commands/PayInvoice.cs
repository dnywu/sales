﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using dokuku.sales.payment.common;
namespace dokuku.sales.payment.commands
{
    [Serializable]
    [MapsToAggregateRootMethod("dokuku.sales.payment.domain.InvoicePayment, dokuku.sales.payment.domain", "PayInvoice")]
    public class PayInvoice : PaymentCommand
    {
        public decimal AmountPaid { get; set; }
        public decimal BankCharge { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }
}