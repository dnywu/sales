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
        int _rounding;
        decimal _subTotal;
        public Invoice(Guid invoiceId,string invoiceNo,Customer customer, Currency baseCurrency, string poNo, string ownerId, string userName)
            : base(invoiceId)
        {
            ApplyEvent(new InvoiceCreated
            {
                InvoiceId = EventSourceId,
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

        public void AddInvoiceItem(Guid itemId,string description, int quantity, decimal price, decimal discountinpercent,string ownerId,string userName)
        {
            decimal totalBeforeDiscount = quantity * price;
            decimal discount = discountinpercent/100 * totalBeforeDiscount;
            decimal totalAfterDiscount = decimal.Round((totalBeforeDiscount - discount), _rounding);
            decimal subTotal = _subTotal + totalAfterDiscount;
            decimal netTotal = subTotal;
            ApplyEvent(new InvoiceItemAdded
            {
                ItemId = itemId,
                Description = description,
                Quantity = quantity,
                Price = price,
                DiscountInPercent = discountinpercent,
                Total = totalAfterDiscount,
                Summary = new Summary
                {
                    SubTotal = subTotal,
                    DiscountTotal = 0,
                    NetTotal = netTotal,
                    Charge = 0,
                    Taxes = new Tax[] { }
                },
                OwnerId = ownerId,
                UserName = userName,
                TaxCode = "PPn",
                InvoiceId = EventSourceId
            });
        }
        private void OnInvoiceCreated(InvoiceCreated @event)
        {
            _rounding = @event.DecimalPlace;
            _subTotal = 0;
        }
        private void OnInvoiceItemAdded(InvoiceItemAdded @event)
        {
            _subTotal = @event.Summary.SubTotal;
        }
        public Invoice()
        {
        }
    }
}