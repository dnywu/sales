using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.invoices.events;
namespace dokuku.sales.invoices.domain
{
    public class Invoices : AggregateRootMappedByConvention
    {
        private string Status;
        public Invoices()
        {
        }
        public Invoices(Customer customer, Term term,string invoiceNo, DateTime invoiceDate,string poNo, DateTime dueDate,Guid invoiceId, string ownerId,string transCcyCode,string baseCcyCode,string UserName,decimal exchangeRate)
            : base()
        {
            ApplyEvent(new InvoiceCreated
            {
                InvoiceId = invoiceId,
                TransCcyCode = transCcyCode,
                BaseCcy = baseCcyCode,
                Customer = customer,
                DueDate = dueDate,
                InvoiceDate = invoiceDate,
                InvoiceNo = invoiceNo,
                OwnerId = ownerId,
                PONo = poNo,
                ExchangeRate = exchangeRate,
                Terms = term,
                UserName = UserName,
                Status = InvoiceStatus.DRAFT
            });
        }
        private void OnInvoiceCreated(InvoiceCreated e)
        {
            Status = e.Status;
        }

        public void UpdateInvoiceDate(DateTime invoiceDate, DateTime dueDate, Guid invoiceId, string userName)
        {
            ApplyEvent(new InvoiceDateChanged
            {
                DueDate = dueDate,
                InvoiceDate = invoiceDate,
                InvoiceId = invoiceId,
                UserName = userName
            });
        }
        private void OnInvoiceDateUpdated(InvoiceDateChanged e)
        {
        }
        public void UpdateInvoiceTerm(Guid invoiceId, Term term, DateTime dueDate,string userName)
        {
            ApplyEvent(new InvoiceTermChanged
            {
                InvoiceId = invoiceId,
                Term = term,
                DueDate = dueDate,
                UserName = userName
            });
        }
        private void OnInvoiceTermUpdated(InvoiceTermChanged e)
        {
        }
        public void UpdateInvoicePONo(Guid invoiceId, string poNo,string userName)
        {
            ApplyEvent(new InvoicePONoChanged
            {
                InvoiceId = invoiceId,
                PONo = poNo,
                UserName = userName
            });
        }
        private void OnInvoicePONoUpdated(InvoicePONoChanged e)
        {
        }
        public void UpdateInvoiceExchangeRate(Guid invoiceId, decimal exchangeRate,string userName)
        {
            ApplyEvent(new InvoiceExchangeRateChanged
            {
                InvoiceId = invoiceId,
                ExchangeRate = exchangeRate,
                UserName = userName
            });
        }
        private void OnInvoiceExchangeRateUpdated(InvoiceExchangeRateChanged e)
        {
        }
        public void AddInvoiceItem(Guid invoiceId, InvoiceItem item,string userName)
        {
            ApplyEvent(new InvoiceItemAdded
            {
                InvoiceId = invoiceId,
                Item = item,
                UserName = userName
            });
        }
        private void OnInvoiceItemAdded(InvoiceItemAdded e)
        {
        }
        public void DeleteInvoice(Guid invoiceId, string userName)
        {
            ApplyEvent(new InvoiceDeleted
            {
                InvoiceId = invoiceId,
                UserName = userName
            });
        }
        private void OnInvoiceDeleted(InvoiceDeleted e)
        {
        }
        public void ChangeInvoiceItem(Guid invoiceId, InvoiceItem item, string userName)
        {
            ApplyEvent(new InvoiceItemChanged
            {
                InvoiceId = invoiceId,
                Item = item,
                UserName = userName
            });
        }
        private void OnInvoiceItemChanged(InvoiceItemChanged e)
        {
        }
        public void ChangeDescriptionInvoiceItem(Guid invoiceId, Guid invoiceItemId, string invoiceItemDescription, string userName)
        {
            ApplyEvent(new DescriptionInvoiceItemChanged
            {
                InvoiceId = invoiceId,
                InvoiceitemDescription = invoiceItemDescription,
                InvoiceItemId = invoiceItemId,
                UserName = userName
            });
        }
        private void OnDescriptionInvoiceItemChanged(DescriptionInvoiceItemChanged e)
        {
        }
        public void ChangeQtyInvoiceItem(Guid invoiceId, Guid invoiceItemId, decimal InvoiceItemQty,decimal invoiceItemTotal, string userName)
        {
            ApplyEvent(new QtyInvoiceItemChanged
            {
                InvoiceId = invoiceId,
                InvoiceItemId = invoiceItemId,
                InvoiceItemQty = InvoiceItemQty,
                InvoiceItemTotal = invoiceItemTotal,
                UserName = userName
            });
        }
        private void OnQtyInvoiceItemChanged(QtyInvoiceItemChanged e)
        {
        }
        public void ChangeTotalInvoiceItem(Guid invoiceId, Guid invoiceItemId, decimal invoiceItemPrice, decimal invoiceItemTotal, string userName)
        {
            ApplyEvent(new PriceInvoiceItemChanged
            {
                InvoiceId = invoiceId,
                InvoiceItemId = invoiceItemId,
                InvoiceItemPrice = invoiceItemPrice,
                InvoiceItemTotal = invoiceItemTotal,
                UserName = userName
            });
        }
        private void OnPriceInvoiceItemChanged(PriceInvoiceItemChanged e)
        {
        }
        public void CancelInvoice(Guid invoiceId, string userName)
        {
            ApplyEvent(new InvoiceCanceled
            {
                InvoiceId = invoiceId,
                Status = InvoiceStatus.BATAL,
                UserName = userName
            });
        }
        private void OnInvoiceCanceled(InvoiceCanceled e)
        {
            Status = e.Status;
        }
        public void ApproveInvoce(Guid Id, string ownerId, string invoiceNo, string status,string userName)
        {
            ApplyEvent(new InvoiceApproved
            {
                _id = Id,
                InvoiceNo = invoiceNo,
                OwnerId = ownerId,
                UserName = userName,
                Status = InvoiceStatus.BELUM_BAYAR
            });
        }
        private void OnInvoiceApproved(InvoiceApproved e)
        {
            this.Status = e.Status;
        }
    }
}
