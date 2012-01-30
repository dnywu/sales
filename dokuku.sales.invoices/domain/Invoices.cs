using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.model;
namespace dokuku.sales.invoices.domain
{
    public class Invoices : AggregateRootMappedByConvention
    {
        private string Status;
        public Invoices()
        {
        }
        public Invoices(Guid id, string baseCcy, string cancelNote, string currency,string customer,string customerId,DateTime dueDate,decimal exchangeRate,
                        DateTime invoiceDate,string invoiceNo,dokuku.sales.invoices.events.InvoiceItem[] items,string lateFee,string note,string ownerId,string poNo,decimal subTotal,
                        string termCondition,dokuku.sales.invoices.events.Term term, decimal total)
            : base()
        {
            ApplyEvent(new InvoiceCreated
            {
                _id = id,
                BaseCcy = baseCcy,
                Currency = currency,
                Customer = customer,
                CustomerId = customerId,
                DueDate = dueDate,
                ExchangeRate = exchangeRate,
                InvoiceDate = invoiceDate,
                InvoiceNo = invoiceNo,
                Items = items,
                LateFee = lateFee,
                Note = note,
                OwnerId = ownerId,
                PONo = poNo,
                SubTotal = subTotal,
                TermCondition = termCondition,
                Terms = term,
                Total = total,
                CancelNote = cancelNote,
                Status = InvoiceStatus.DRAFT
            });
        }
        private void OnInvoiceCreated(InvoiceCreated e)
        {
            Status = e.Status;
        }
        public void ReviseInvoice(Guid id, string baseCcy, string cancelNote, string currency, string customer, string customerId, DateTime dueDate, decimal exchangeRate,
                                  DateTime invoiceDate, string invoiceNo, dokuku.sales.invoices.events.InvoiceItem[] items, string lateFee, string note, string ownerId, string poNo, decimal subTotal,
                                  string termCondition, dokuku.sales.invoices.events.Term term, decimal total)
        {
            ApplyEvent(new InvoiceRevised
            {
                _id = id,
                BaseCcy = baseCcy,
                Currency = currency,
                Customer = customer,
                CustomerId = customerId,
                DueDate = dueDate,
                ExchangeRate = exchangeRate,
                InvoiceDate = invoiceDate,
                InvoiceNo = invoiceNo,
                Items = items,
                LateFee = lateFee,
                Note = note,
                OwnerId = ownerId,
                PONo = poNo,
                SubTotal = subTotal,
                TermCondition = termCondition,
                Terms = term,
                Total = total,
                CancelNote = cancelNote,
                Status = InvoiceStatus.BELUM_BAYAR
            });
        }
        private void OnInvoiceRevised(InvoiceRevised e)
        {
            Status = e.Status;
        }
        public void ApproveInvoce(Guid Id, string ownerId, string invoiceNo, string status)
        {
            ApplyEvent(new InvoiceApproved
            {
                _id = Id,
                InvoiceNo = invoiceNo,
                OwnerId = ownerId,
                Status = InvoiceStatus.BELUM_BAYAR
            });
        }
        private void OnInvoiceApproved(InvoiceApproved e)
        {
            this.Status = e.Status;
        }
    }
}
