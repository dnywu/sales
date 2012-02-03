using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.invoices.events;
using System.Diagnostics.Contracts;
using Ncqrs;
using MongoDB.Driver.Builders;
using dokuku.sales.config;
using MongoDB.Bson;
namespace dokuku.sales.invoices.domain
{
    public class Invoice: AggregateRootMappedByConvention
    {
        string _status;
        public Invoice(Guid invoiceId,string invoiceNo,Customer customer, Currency baseCurrency, string poNo, string ownerId, string userName)
            : base()
        {
            ApplyEvent(new InvoiceCreated
            {
                InvoiceId = invoiceId,
                CustomerId = customer.Id,
                DecimalPlace = customer.Currency.Rounding,
                BaseCurrency = baseCurrency.Code,
                PONo = poNo,
                OwnerId = ownerId,
                UserName = userName,
                Status = InvoiceStatus.DRAFT,
                TermCode = customer.Term.Code,
                InvoiceDate = DateTime.Now.Date,
                DueDate = DateTime.Now.Date.AddDays(customer.Term.Value),
                InvoiceNo = invoiceNo,
                Rate = 1,
                TransactionCurrency = customer.Currency.Code
            });
        }
        private void OnInvoiceCreated(InvoiceCreated @event)
        {
        }
        public Invoice()
        {
        }
    }
}