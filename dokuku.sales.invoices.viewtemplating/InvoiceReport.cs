using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dokuku.sales.invoices.model;

namespace dokuku.sales.invoices.viewtemplating
{
    public class InvoiceReport
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
        public List<InvoiceItemReport> Items { get; private set; }
        public Guid _id { get; private set; }
        public string OwnerId { get; private set; }
        public string Status { get; private set; }
        public string CancelNote { get; private set; }

        public InvoiceReport(Invoices invoice)
        {
            Items = new List<InvoiceItemReport>();
            Customer = invoice.Customer;
            CustomerId = invoice.CustomerId;
            InvoiceNo = invoice.InvoiceNo;
            PONo = invoice.PONo;
            InvoiceDate = invoice.InvoiceDate;
            Terms = invoice.Terms;
            DueDate = invoice.DueDate;
            LateFee = invoice.LateFee;
            Note = invoice.Note;
            TermCondition = invoice.TermCondition;
            ExchangeRate = invoice.ExchangeRate;
            BaseCcy = invoice.BaseCcy;
            Currency = invoice.Currency;
            SubTotal = invoice.SubTotal;
            Total = invoice.Total;
            _id = invoice._id;
            OwnerId = invoice.OwnerId;
            Status = invoice.Status;
            CancelNote = invoice.CancelNote;

            foreach (var item in invoice.Items)
            {
                Items.Add(new InvoiceItemReport(item));
            }
        }
    }
    public class InvoiceItemReport
    {
        public InvoiceItemReport(InvoiceItem item)
        {
            ItemId = item.ItemId.ToString();
            PartName = item.PartName;
            Description = item.Description;
            QtyString = item.Qty.ToString("0.##");
            BaseRateString = item.BaseRate.ToString("###,###,###,##0");
            RateString = item.Rate.ToString("###,###,###,##0");
            DiscountString = item.Discount.ToString("###,###,###,##0");
            AmountString = item.Amount.ToString("###,###,###,##0");
            TaxValueSting = item.Tax.Value.ToString("###,###,###,##0");
            Qty = item.Qty;
            BaseRate = item.BaseRate;
            Rate = item.Rate;
            Discount = item.Discount;
            Tax = item.Tax;
            Amount = item.Amount;
        }
        public string ItemId { get; private set; }
        public string PartName { get; private set; }
        public string Description { get; private set; }
        public string QtyString { get; private set; }
        public string BaseRateString { get; private set; }
        public string RateString { get; private set; }
        public string DiscountString { get; private set; }
        public string AmountString { get; private set; }
        public string TaxValueSting { get; private set; }
        public decimal Qty { get; private set; }
        public decimal BaseRate { get; private set; }
        public decimal Rate { get; private set; }
        public decimal Discount { get; private set; }
        public Tax Tax { get; private set; }
        public decimal Amount { get; private set; }
    }
}