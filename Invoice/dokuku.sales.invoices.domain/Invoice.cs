using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.invoices.events;
using System.Diagnostics.Contracts;
using Ncqrs;
using dokuku.sales.config;
namespace dokuku.sales.invoices.domain
{
    public class Invoice: AggregateRootMappedByConvention
    {
        int _rounding;
        decimal _subTotal;
        IDictionary<string,TaxSummary> _taxes=new Dictionary<string,TaxSummary>();
        int _nextItemNumber=0;

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
                TransactionCurrency = customer.Currency.Code,
                Timestamp = DateTime.Now
            });
        }

        public void AddInvoiceItem(Guid itemId,string description, int quantity, decimal price, decimal discountinpercent,Tax tax,string ownerId,string userName)
        {
            decimal totalBeforeDiscount = quantity * price;
            decimal discount = discountinpercent/100 * totalBeforeDiscount;
            decimal totalAfterDiscount = decimal.Round((totalBeforeDiscount - discount), _rounding);
            decimal iteamTaxAmount = decimal.Round(totalAfterDiscount * tax.Rate / 100, _rounding);

            TaxSummary taxSummary = GetTaxSummary(tax.TaxCode);
            taxSummary.TaxAmount += iteamTaxAmount;

            decimal subTotal = _subTotal + totalAfterDiscount;
            decimal taxAmount = SumTaxes();
            decimal netTotal = subTotal + taxAmount;
            int itemNumber = _nextItemNumber + 1;
            
            ApplyEvent(new InvoiceItemAdded
            {
                ItemId = itemId,
                Description = description,
                Quantity = quantity,
                Price = price,
                DiscountInPercent = discountinpercent,
                DiscountAmount = discount,
                Total = totalAfterDiscount,
                Summary = new Summary
                {
                    SubTotal = subTotal,
                    DiscountTotal = 0,
                    NetTotal = netTotal,
                    Charge = 0,
                    Taxes = _taxes.Values.ToArray()
                },
                OwnerId = ownerId,
                UserName = userName,
                TaxCode = tax.TaxCode,
                TaxAmount = taxAmount,
                InvoiceId = EventSourceId,
                ItemNumber = itemNumber,
                Timestamp = DateTime.Now
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
            _nextItemNumber = @event.ItemNumber;
            _taxes = new Dictionary<string, TaxSummary>();
            foreach (TaxSummary taxSummary in @event.Summary.Taxes)
                _taxes.Add(taxSummary.TaxCode, taxSummary);
        }

        public Invoice()
        {
        }

        private TaxSummary GetTaxSummary(string taxCode)
        {
            if (_taxes.ContainsKey(taxCode))
                return _taxes[taxCode];

            TaxSummary taxSummary = new TaxSummary { TaxCode = taxCode, TaxAmount =0 };
            _taxes.Add(taxCode, taxSummary);
            return taxSummary;
        }
        private decimal SumTaxes()
        {
            decimal result = 0;
            foreach (TaxSummary taxSummay in _taxes.Values)
                result += taxSummay.TaxAmount;
            return result;
        }
    }
}