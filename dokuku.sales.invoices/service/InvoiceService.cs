using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.config;
using dokuku.sales.invoice.messages;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.model;
using dokuku.sales.invoices.query;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using NServiceBus;
namespace dokuku.sales.invoices.service
{
    public class InvoiceService : IInvoiceService
    {
        IInvoicesRepository invRepo;
        IInvoiceAutoNumberGenerator gen;
        MongoConfig mongo;
        IBus bus;

        public InvoiceService(IInvoicesRepository invoiceRepository, IInvoiceAutoNumberGenerator invNumberGenerator, MongoConfig mongo, IBus bus)
        {
            this.invRepo = invoiceRepository;
            this.gen = invNumberGenerator;
            this.mongo = mongo;
            this.bus = bus;
        }

        public Invoices Create(string jsonInvoice, string ownerId)
        {
            string data = jsonInvoice;
            Invoices invoice = JsonConvert.DeserializeObject<Invoices>(data);
            string invoiceNumber = gen.GenerateInvoiceNumberDraft(ownerId);
            FailIfInvoiceNumberAlreadyUsed(invoiceNumber, ownerId);

            invoice._id = Guid.NewGuid();
            invoice.OwnerId = ownerId;
            invoice.InvoiceNo = invoiceNumber;
            invRepo.Save(invoice);

            if (bus != null)
                bus.Publish<InvoiceCreated>(new InvoiceCreated { InvoiceJson = invoice.ToJson<Invoices>() });

            return invoice;
        }

        public void Update(string jsonInvoice, string ownerId)
        {
            string data = jsonInvoice;
            Invoices invoice = JsonConvert.DeserializeObject<Invoices>(data);
            invoice.OwnerId = ownerId;
            invRepo.UpdateInvoices(invoice);

            if (bus != null)
                bus.Publish<InvoiceUpdate>(new InvoiceUpdate { Data = invoice.ToJson() });
        }
        public void Delete(Guid id, string ownerId)
        {
            IsInvoiceStatusDraft(id, ownerId);
            invRepo.Delete(id, ownerId);
        }
        public void UpdateStatusToAprrove(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            if (invoice.Status != InvoiceStatus.DRAFT)
                throw new Exception(String.Format("Status invoice '{0}' harus {1} sebelum dilakukan ganti status. Sedangkan status saat ini adalah {2}", invoice.InvoiceNo, InvoiceStatus.DRAFT, invoice.Status));

            invoice.InvoiceStatusBelumBayar();
            invRepo.UpdateInvoices(invoice);

            if (bus != null)
                bus.Publish(new InvoiceApproved { Data = invoice.ToJson<Invoices>() });
        }

        private void IsInvoiceStatusDraft(Guid id, string ownerId)
        {
            Invoices invoice = invRepo.Get(id, ownerId);
            if (invoice.Status.ToLower() != "draft")
                throw new Exception("Hapus invoice gagal, status invoice bukan draft");
        }

        private void FailIfInvoiceNumberAlreadyUsed(string invoiceNumber, string ownerId)
        {
            Invoices inv = invRepo.GetInvByNumber(invoiceNumber, ownerId);
            if (inv != null)
                throw new ApplicationException("Invoice " + invoiceNumber + " sudah digunakan!");
        }


        public void InvoiceFullyPaid(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            if (invoice == null)
                throw new ApplicationException("Invoice tidak ditemukan dalam database");
            invoice.InvoiceStatusSudahLunas();
            invRepo.Save(invoice);
        }

        public void InvoicePartialyPaid(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            if (invoice == null)
                throw new ApplicationException("Invoice tidak ditemukan dalam database");
            invoice.InvoiceStatusBelumLunas();
            invRepo.Save(invoice);
        }
    }
}