using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.model
{
    public class Invoices
    {
        public Invoices()
        {
            Status = InvoiceStatus.DRAFT;
        }
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Term Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string LateFee { get; set; }
        public string Note { get; set; }
        public string TermCondition { get; set; }
        public decimal ExchangeRate { get; set; }
        public string BaseCcy { get; set; }
        public string Currency { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public InvoiceItem[] Items { get; set; }
        public Guid _id { get; set; }
        public string OwnerId { get; set; }
        public string Status { get; private set; }
        public string CancelNote { get; private set; }
        public TaxSummary[] TaxSummary { get; set; }

        public void InvoiceStatusBelumBayar()
        {
            this.Status = InvoiceStatus.BELUM_BAYAR;
            //TotalTaxItem = new Dictionary<string, decimal>();
        }
        public void InvoiceStatusBelumLunas()
        {
            this.Status = InvoiceStatus.BELUM_LUNAS;
        }
        public void InvoiceStatusSudahLunas()
        {
            this.Status = InvoiceStatus.SUDAH_LUNAS;
        }
        public void InvoiceStatusVoid()
        {
            this.Status = InvoiceStatus.VOID;
        }
        public void InvoiceStatusBatal(string note)
        {
            this.Status = InvoiceStatus.BATAL;
            this.CancelNote = note;
        }
    }
}