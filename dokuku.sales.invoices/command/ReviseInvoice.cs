using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding;
using dokuku.sales.invoices.events;

namespace dokuku.sales.invoices.command
{
    [MapsToAggregateRootMethod("dokuku.sales.invoices.domain.Invoices,dokuku.sales.invoices.domain","ReviseInvoice")]
    public class ReviseInvoice : CommandBase
    {
        public string Customer { get; private set; }
        public string CustomerId { get; private set; }
        public string InvoiceNo { get; private set; }
        public string PONo { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public Term Terms { get; private set; }
        public DateTime DueDate { get; private set; }
        public string LateFee { get; private set; }
        public string Note { get; private set; }
        public string TermCondition { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public string BaseCcy { get; private set; }
        public string Currency { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Total { get; private set; }
        public InvoiceItem[] Items { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string OwnerId { get; private set; }
        public string CancelNote { get; private set; }

        public ReviseInvoice(string customer, string customerId, string invoiceNo, string poNo, DateTime invoiceDate, Term terms, DateTime dueDate, string lateFee,
                             string note, string termCondition, decimal exchangeRate, string baseCcy, string currency, decimal subTotal, decimal total, InvoiceItem[] items,
                             Guid invoiceId, string ownerId, string cancelNote)
        {
            this.Customer = customer;
            this.CustomerId = customerId;
            this.InvoiceNo = invoiceNo;
            this.PONo = poNo;
            this.InvoiceDate = invoiceDate;
            this.Terms = terms;
            this.DueDate = dueDate;
            this.LateFee = lateFee;
            this.Note = note;
            this.TermCondition = termCondition;
            this.ExchangeRate = exchangeRate;
            this.BaseCcy = baseCcy;
            this.Currency = currency;
            this.SubTotal = subTotal;
            this.Total = total;
            this.Items = items;
            this.InvoiceId = InvoiceId;
            this.OwnerId = ownerId;
            this.CancelNote = cancelNote;
        }

    }
}
