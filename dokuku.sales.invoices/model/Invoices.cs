using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.item;

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
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public InvoiceItems[] Items { get; set; }
        public string _id { get; set; }
        public string OwnerId { get; set; }
        public string Status { get; private set; }

        public void InvoiceStatusBelumBayar()
        {
            this.Status = InvoiceStatus.BELUM_BAYAR;
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
        public void InvoiceStatusBatal()
        {
            this.Status = InvoiceStatus.BATAL;
        }
    }

    public class InvoiceItems
    {
        public Guid ItemId { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public Tax Tax { get; set; }
        public decimal Amount { get; set; }     
    }

    public class Term
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class InvoiceStatus
    {
        public const string DRAFT = "Draft";
        public const string BELUM_BAYAR = "Belum Bayar";
        public const string BELUM_LUNAS = "Belum Lunas";
        public const string SUDAH_LUNAS = "Sudah Lunas";
        public const string VOID = "Void";
        public const string BATAL = "Batal";
    }
}