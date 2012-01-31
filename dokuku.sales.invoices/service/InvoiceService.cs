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
        public void ApproveInvoice(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            string invoiceNumber = gen.GenerateInvoiceNumber(invoice.InvoiceDate, ownerId);
            if (invoice.Status != InvoiceStatus.DRAFT)
                throw new Exception(String.Format("Status faktur '{0}' harus {1} sebelum dilakukan ganti status. Sedangkan status saat ini adalah {2}", invoice.InvoiceNo, InvoiceStatus.DRAFT, invoice.Status));
            invoice.InvoiceNo = invoiceNumber;
            invoice.InvoiceStatusBelumBayar();
            invRepo.UpdateInvoices(invoice);

            if (bus != null)
                bus.Publish(new InvoiceApproved { InvoiceJson = invoice.ToJson<Invoices>() });
        }

        private void IsInvoiceStatusDraft(Guid id, string ownerId)
        {
            Invoices invoice = invRepo.Get(id, ownerId);
            if (invoice.Status.ToLower() != "draft")
                throw new Exception("Hapus invoice gagal, status faktur bukan draft");
        }

        private void FailIfInvoiceNumberAlreadyUsed(string invoiceNumber, string ownerId)
        {
            Invoices inv = invRepo.GetInvByNumber(invoiceNumber, ownerId);
            if (inv != null)
                throw new ApplicationException("Faktur " + invoiceNumber + " sudah digunakan!");
        }


        public void InvoiceFullyPaid(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            if (invoice == null)
                throw new ApplicationException("Faktur tidak ditemukan dalam database");
            invoice.InvoiceStatusSudahLunas();
            invRepo.Save(invoice);

            if (bus != null)
                bus.Publish<InvoiceUpdate>(new InvoiceUpdate { Data = invoice.ToJson() });
        }

        public void InvoicePartialyPaid(Guid invoiceId, string ownerId)
        {
            Invoices invoice = invRepo.Get(invoiceId, ownerId);
            if (invoice == null)
                throw new ApplicationException("Faktur tidak ditemukan dalam database");
            invoice.InvoiceStatusBelumLunas();
            invRepo.Save(invoice);

            if (bus != null)
                bus.Publish<InvoiceUpdate>(new InvoiceUpdate { Data = invoice.ToJson() });
        }

        public void Cancel(Guid id, string cancelNote, string ownerId)
        {
            Invoices invoice = cancel(id, cancelNote, ownerId, false);
            if (bus != null)
                bus.Publish(new InvoiceCancelled { Data = invoice.ToJson<Invoices>() });
        }
        public void ForceCancel(Guid id, string cancelNote, string ownerId)
        {
            Invoices invoice = cancel(id, cancelNote, ownerId, true);
            if (bus != null)
                bus.Publish(new InvoiceForceCancelled { Data = invoice.ToJson<Invoices>() });
        }
        private Invoices cancel(Guid id, string cancelNote, string ownerId, bool forceCancel)
        {
            Invoices invoice = invRepo.Get(id, ownerId);
            if (invoice == null)
                throw new Exception("Faktur tidak ditemukan dalam database");
            if (forceCancel && invoice.Status != InvoiceStatus.BELUM_LUNAS && invoice.Status != InvoiceStatus.BELUM_BAYAR && invoice.Status != InvoiceStatus.BATAL && invoice.Status != InvoiceStatus.DRAFT)
                throw new Exception(String.Format("Status faktur {0} ({1}) tidak dapat di batalkan", invoice.InvoiceNo, invoice.Status));
            if (String.IsNullOrWhiteSpace(cancelNote))
                throw new Exception("Mohon catatan untuk membatalkan faktur ini diisi terlebih dahulu.");
            invoice.InvoiceStatusBatal(cancelNote);
            invRepo.UpdateInvoices(invoice);
            return invoice;
        }

    }
}